using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace Blackjack {
    /// <summary>Classe d'une partie de Blackjack.</summary>
    [Serializable]
    public class Partie : IDisposable {
        #region Données membres

        [NonSerialized] private readonly Salon salon;
        private Sabot sabot;
        private readonly List<Participant> participants;

        private readonly double initial;
        private readonly double min;

        [NonSerialized] private int actif;
        [NonSerialized] private int local;
        [NonSerialized] private readonly Reseau reseau;

        /// <summary>Constante utilisée pour représenter l'hôte.</summary>
        [NonSerialized] private const int HOTE = 0;

        #endregion

        #region Constructeurs

        /// <summary>Héberge une partie.</summary>
        /// <param name="nom">Nom de l'hôte.</param>
        /// <param name="nombre">Nombre de joueurs dans la partie.</param>
        /// <param name="initial">Montant initial des joueurs.</param>
        /// <param name="min">Mise minimale des joueurs.</param>
        /// <param name="virtuel">Définie si la partie sera jouée avec des joueurs virtuels ou distants.</param>
        /// <exception cref="ArgumentOutOfRangeException">Le nombre de joueur dans la partie doit être entre 1 et 4.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Le montant initial des joueurs doit être plus grand que 0.</exception>
        /// <exception cref="ArgumentOutOfRangeException">La mise minimale des joueurs doit être plus grande que 0.</exception>
        /// <exception cref="ArgumentException">La mise minimale des joueurs doit être plus petite ou égale au montant initial de ceux-ci.</exception>
        public Partie(string nom, int nombre, double initial, double min, bool virtuel) {
            salon = new Salon(this);
            NouveauSabot();

            this.initial = initial > 0 ? initial : throw new ArgumentOutOfRangeException("initial", "Le montant initial des joueurs doit être plus grand que 0.");
            this.min = min > 0 ? min <= initial ? min : throw new ArgumentException("La mise minimale des joueurs doit être plus petite ou égale au montant initial de ceux-ci.", "min") : throw new ArgumentOutOfRangeException("min", "La mise minimale des joueurs doit être plus grande que 0.");
            local = HOTE;

            Joueur hote = new Joueur(nom, initial);
            // Initialise la liste de participants avec une taille du nombre de joueurs voulues + 1 pour le croupier
            participants = new List<Participant>(nombre > 0 && nombre <= 4 ? nombre + 1 : throw new ArgumentOutOfRangeException("nombre", "Le nombre de joueur dans la partie doit être entre 1 et 4."));
            AjouterJoueur(hote);

            if (virtuel)
                while (Compte < nombre)
                    AjouterJoueur(new JoueurVirtuel("Joueur virtuel " + Compte, initial));

            if (Compte < nombre) {
                reseau = new Reseau();
                new Thread(AttendreJoueur) { IsBackground = true }.Start();
            } else {
                AjouterCroupier();
                Jouer();
            }
        }

        /// <summary>Rejoint une partie.</summary>
        /// <param name="nom">Nom du client.</param>
        /// <param name="ip">Adresse IP de l'hôte.</param>
        public Partie(string nom, IPAddress ip) {
            reseau = new Reseau(ip);
            Partie hote = reseau.ObtenirPartie();

            // Obtient les informations de la partie de l'hôte
            sabot = hote.sabot;
            participants = hote.participants;
            initial = hote.initial;
            min = hote.min;

            salon = new Salon(this);
            local = Compte;

            // Ajoute les joueurs déjà reçus par l'hôte
            foreach (Joueur joueur in participants)
                salon.AjouterJoueur(joueur);

            Joueur client = new Joueur(nom, initial);
            AjouterJoueur(client);
            reseau.EnvoyerJoueur(client);

            if (Compte < Nombre)
                new Thread(AttendreJoueur) { IsBackground = true }.Start();
            else {
                AjouterCroupier();
                Jouer();
            }
        }

        #endregion

        #region Propriétés

        /// <summary>Obtient le nombre de joueurs initial de la partie.</summary>
        public int Nombre { get => participants.Capacity - 1; }

        /// <summary>Obtient le compte de joueurs actuellement dans la partie.</summary>
        public int Compte { get => participants.Count(participant => participant is Joueur); }

        /// <summary>Obtient l'indice du croupier dans la liste de participants.</summary>
        private int Croupier { get => participants.Count - 1; }

        /// <summary>Évalue si le le joueur local est l'hôte de la partie.</summary>
        private bool EstHote { get => local == HOTE; }

        /// <summary>Évalue si le joueur local est le joueur actif de la partie.</summary>
        private bool EstActif { get => local == actif; }

        #endregion

        #region Méthodes publiques

        /// <summary>Affiche le salon de jeu de la partie à l'utilisateur.</summary>
        public void Afficher() => salon.Show();

        /// <summary>Effectue une mise du montant spécifié.</summary>
        /// <param name="mise">Mise du joueur.</param>
        public void Miser(double mise) {
            if (EstActif && reseau != null)
                reseau.EnvoyerMise(mise);

            participants[actif].Actif = false;

            (participants[actif++] as Joueur).Miser(mise);

            MiseSuivante();
        }

        /// <summary>Effectue l'action de tirer une carte (HIT).</summary>
        public void Tirer() {
            if (EstActif && reseau != null)
                reseau.EnvoyerCoup(true);

            try {
                participants[actif].Tirer(sabot.Piocher());
            } catch (InvalidOperationException) { // Si le sabot est vide
                NouveauSabot();
            }

            if (participants[actif].Total >= 21) // Si le joueur saute ou compte déjà 21
                FinTour();
            else if (participants[actif] is JoueurVirtuel)
                CoupJoueurVirtuel();
            else if (participants[actif] is Croupier)
                CoupCroupier();
            else if (!EstActif)
                new Thread(AttendreCoup) { IsBackground = true }.Start();
        }

        /// <summary>Effectue l'action de rester (STAND).</summary>
        public void Rester() {
            if (EstActif && reseau != null)
                reseau.EnvoyerCoup(false);

            participants[actif].Rester();
            FinTour();
        }

        #endregion

        #region Méthodes privées

        /// <summary>Démarre une partie de Blackjack.</summary>
        private void Jouer() {
            foreach (Participant participant in participants)
                participant.Defausser();

            actif = HOTE;
            participants[actif].Actif = true;

            if (EstActif && participants[actif] is Joueur joueur)
                salon.AfficherMise(joueur.Montant < min ? joueur.Montant : min, joueur.Montant);
            else
                new Thread(AttendreMise) { IsBackground = true }.Start();
        }

        /// <summary>Distribue les cartes aux joueurs et au croupier.</summary>
        private void DistribuerCartes() {
            for (actif = 0; actif < Croupier; actif++)
                for (byte i = 0; i < 2; i++) {
                    try {
                        participants[actif].Piocher(sabot.Piocher());
                    } catch (InvalidOperationException) { // Si le sabot est vide
                        NouveauSabot();
                    }
                }

            try {
                participants[Croupier].Piocher(sabot.Piocher());
            } catch (InvalidOperationException) { // Si le sabot est vide
                NouveauSabot();
            }

            actif = HOTE;
        }

        /// <summary>Passe à la ma mise suivante.</summary>
        private void MiseSuivante() {
            if (participants[actif] is Joueur suivant)
                suivant.Actif = true;

            if (EstActif && participants[actif] is Joueur miseur)
                salon.AfficherMise(miseur.Montant < min ? miseur.Montant : min, miseur.Montant);
            else if (participants[actif] is JoueurVirtuel virtuel)
                // Le joueur virtuel mise toujours le minimum ou le reste de son montant si celui-ci est plus petit
                Miser(virtuel.Montant < min ? virtuel.Montant : min);
            else if (participants[actif] is Croupier) {
                // Le croupier ne mise pas, on peut donc distribuer les cartes et procéder aux actions
                DistribuerCartes();
                participants[actif].Actif = true;

                if (participants[actif].Blackjack)
                    FinTour();
                else if (EstActif)
                    salon.DebloquerActions();
                else
                    new Thread(AttendreCoup) { IsBackground = true }.Start();
            } else
                new Thread(AttendreMise) { IsBackground = true }.Start();
        }

        /// <summary>Mets fin au tour actif.</summary>
        private void FinTour() {
            if (EstActif)
                salon.BloquerActions();
            participants[actif++].Actif = false;

            TourSuivant();
        }

        /// <summary>Passe au tour suivant.</summary>
        private void TourSuivant() {
            if (actif == participants.Count)
                Fin();
            else {
                participants[actif].Actif = true;

                if (participants[actif].Blackjack)
                    FinTour();
                else if (EstActif)
                    salon.DebloquerActions();
                else if (participants[actif] is Croupier)
                    CoupCroupier();
                else if (participants[actif] is JoueurVirtuel)
                    CoupJoueurVirtuel();
                else
                    new Thread(AttendreCoup) { IsBackground = true }.Start();
            }
        }

        /// <summary>Effectue le coup d'un joueur virtuel.</summary>
        private void CoupJoueurVirtuel() {
            if ((participants[actif] as JoueurVirtuel).Action)
                Tirer();
            else
                Rester();
        }

        /// <summary>Effectue le coup du croupier.</summary>
        private void CoupCroupier() {
            if ((participants[actif] as Croupier).Action)
                Tirer();
            else
                Rester();
        }

        /// <summary>Effectue la fin d'une partie de Blackjack.</summary>
        private void Fin() {
            // Affiche les résultats de la partie dans un MessageBox
            string msg = "";

            if (participants[Croupier].Saute) {
                foreach (Joueur joueur in participants.GetRange(0, Croupier))
                    if (joueur.Blackjack) {
                        joueur.GagnerBlackjack();
                        msg += joueur.Nom + " a gagné(e) " + (joueur.Mise * 1.5).ToString("C2") + " avec un Blackjack\n";
                    } else if (!joueur.Saute) {
                        joueur.Gagner();
                        msg += joueur.Nom + " a gagné(e) " + joueur.Mise.ToString("C2") + "\n";
                    } else {
                        joueur.Perdre();
                        msg += joueur.Nom + " a perdu(e) " + joueur.Mise.ToString("C2") + "\n";
                    }

            } else {
                foreach (Joueur joueur in participants.GetRange(0, Croupier))
                    if (joueur.Blackjack && !participants[Croupier].Blackjack) {
                        joueur.GagnerBlackjack();
                        msg += joueur.Nom + " a gagné(e) " + (joueur.Mise * 1.5).ToString("C2") + " avec un Blackjack\n";
                    } else if (joueur.Total == participants[Croupier].Total) {
                        joueur.Egaliter();
                        msg += joueur.Nom + " a égalisé(e) le croupier.\n";
                    } else if (joueur.Total > participants[Croupier].Total && !joueur.Saute) {
                        joueur.Gagner();
                        msg += joueur.Nom + " a gagné(e) " + joueur.Mise.ToString("C2") + "\n";
                    } else {
                        joueur.Perdre();
                        msg += joueur.Nom + " a perdu(e) " + joueur.Mise.ToString("C2") + "\n";
                    }
            }

            MessageBox.Show(msg, "Résultat de la partie", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Élimine les joueurs ayant atteint 0$
            List<Joueur> elimine = new List<Joueur>(); // Liste temporaire pour stocker les participants à éliminer
            foreach (Joueur joueur in participants.Where(participant => participant is Joueur joueur && joueur.Montant <= 0))
                elimine.Add(joueur);

            foreach (Joueur joueur in elimine)
                Eliminer(joueur);

            if (elimine.Contains(participants[local]))
                salon.Fermer();
            else
                Jouer();
        }

        /// <summary>Élimine le joueur spécifié de la partie.</summary>
        /// <param name="joueur">Joueur à éliminé</param>
        /// <remarks>Si le joueur éliminé est l'hôte de la partie, celle-ci prend fin automatiquement.</remarks>
        private void Eliminer(Joueur joueur) {
            if (joueur == participants[local])
                MessageBox.Show("Votre montant a atteint 0 $. Votre partie est terminée. Meilleure chance la prochaine fois!", "Partie terminée", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else {
                int index = participants.FindIndex((participant) => participant == joueur);

                if (index == HOTE) {
                    MessageBox.Show("Il n'y a plus d'hôte pour continuer la partie. Retour au menu principal.", "Partie terminée", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    salon.Fermer();
                } else {
                    if (local > index)
                        local--;

                    if (EstHote && reseau != null)
                        reseau.Retirer(index - 1);

                    participants.Remove(joueur);
                    salon.RetirerJoueur(joueur);
                }
            }
        }

        /// <summary>Ajoute le joueur spécifié à la liste de participants et au salon de jeu.</summary>
        /// <param name="joueur">Joueur à ajouter.</param>
        private void AjouterJoueur(Joueur joueur) {
            participants.Add(joueur);
            salon.AjouterJoueur(joueur);
        }

        /// <summary>Ajoute le croupier à la liste de participants et au salon de jeu.</summary>
        private void AjouterCroupier() {
            Croupier croupier = new Croupier();
            participants.Add(croupier);
            salon.AjouterCroupier(croupier);
        }

        /// <summary>Crée un nouveau sabot et gère sa distribution chez les clients réseaux.</summary>
        private void NouveauSabot() {
            if (EstHote) {
                sabot = new Sabot(8);
                sabot.Melanger();
                if (reseau != null)
                    reseau.EnvoyerSabot(sabot);
            } else
                sabot = reseau.ObtenirSabot();
        }

        #endregion

        #region Appels réseaux

        /// <summary>Ajoute un joueur distant à la partie. Cet appel est bloquant.</summary>
        /// <remarks>Puisque cet appel est bloquant il est recommandé de le placer dans un fil.</remarks>
        private void AttendreJoueur() {
            try {
                if (EstHote)
                    reseau.ObtenirConnexion(this);

                AjouterJoueur(EstHote ? reseau.ObtenirJoueur(participants.Count - 1) : reseau.ObtenirJoueur());

                if (Compte < Nombre)
                    new Thread(AttendreJoueur) { IsBackground = true }.Start();
                else {
                    AjouterCroupier();
                    Jouer();
                }
            } catch (SocketException) { // Exception levée lorsqu'aucune connexion est obtenue suite à la fermeture du formulaire par l'hôte
            } catch (IOException ex) { // Puisque qu'une IOException peut provenir de plusieurs causes différentes, le message générique est renvoyer à l'utilisateur 
                MessageBox.Show(ex.Message + " Retour au menu principal.", "Erreur de connexion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                salon.Fermer();
            }
        }

        /// <summary>Reçoit la mise d'un joueur distant. Cet appel est bloquant.</summary>
        /// <remarks>Puisque cet appel est bloquant il est recommandé de le placer dans un fil.</remarks>
        private void AttendreMise() {
            try {
                Miser(EstHote ? reseau.ObtenirMise(actif - 1) : reseau.ObtenirMise());
            } catch (ArgumentNullException) {
                MessageBox.Show("Le joueur actif a quitté la partie. Il sera éliminé.", "Erreur de connexion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Eliminer(participants[actif] as Joueur);
                MiseSuivante();
            } catch (IOException ex) { // Puisque qu'une IOException peut provenir de plusieurs causes différentes, le message générique est renvoyer à l'utilisateur 
                Eliminer(participants[actif] as Joueur);
                if (actif != HOTE) {
                    MessageBox.Show(ex.Message + " Le joueur actif sera éliminé.", "Erreur de connexion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MiseSuivante();
                }
            }
        }

        /// <summary>Reçoit le coup d'un joueur distant. Cet appel est bloquant.</summary>
        /// <remarks>Puisque cet appel est bloquant il est recommandé de le placer dans un fil.</remarks>
        private void AttendreCoup() {
            try {
                if (EstHote ? reseau.ObtenirCoup(actif - 1) : reseau.ObtenirCoup())
                    Tirer();
                else
                    Rester();
            } catch (InvalidDataException) {
                MessageBox.Show("Le joueur actif a quitté la partie. Il sera éliminé.", "Erreur de connexion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Eliminer(participants[actif] as Joueur);
                TourSuivant();
            } catch (IOException ex) { // Puisque qu'une IOException peut provenir de plusieurs causes différentes, le message générique est renvoyer à l'utilisateur 
                Eliminer(participants[actif] as Joueur);
                if (actif != HOTE) {
                    MessageBox.Show(ex.Message + " Le joueur actif sera éliminé.", "Erreur de connexion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TourSuivant();
                }
            }
        }

        #endregion

        /// <summary>Implémentation de l'interface IDisposable pour cette clase.</summary>
        public void Dispose() {
            if (reseau != null)
                reseau.Dispose();

            if (!salon.IsDisposed && !salon.Disposing)
                salon.Dispose();
        }
    }
}
