using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blackjack {
    /// <summary>Classe d'une partie de Blackjack.</summary>
    [Serializable]
    public class Partie {
        #region Données membres

        [NonSerialized] private readonly Salon salon;
        private readonly Sabot sabot;
        private readonly List<Participant> participants;

        private double initial;
        private double min;

        [NonSerialized] private int actif;
        [NonSerialized] private readonly int local;
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
        /// <exception cref="ArgumentOutOfRangeException">Le nombre de joueur dans la partie doit être entre 1 et 4.</exception>
        /// <exception cref="ArgumentOutOfRangeException">La mise minimale des joueurs doit être plus grande que 0.</exception>
        public Partie(string nom, int nombre, double initial, double min) {
            salon = new Salon(this);

            sabot = new Sabot(8);
            sabot.Melanger();

            this.initial = initial;
            this.min = min > 0 ? min : throw new ArgumentOutOfRangeException("min", "La mise minimale des joueurs doit être plus grande que 0.");
            local = HOTE;

            Joueur hote = new Joueur(nom, initial);
            participants = new List<Participant>(nombre > 1 && nombre <= 4 ? nombre + 1 : throw new ArgumentOutOfRangeException("nombre", "Le nombre de joueur dans la partie doit être entre 1 et 4."));
            AjouterJoueur(hote);

            salon.Show();

            if (nombre > 1) {
                reseau = new Reseau();
                new Thread(AttendreJoueur).Start();
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

            sabot = hote.sabot;
            participants = hote.participants;
            initial = hote.initial;
            min = hote.min;

            salon = new Salon(this);
            local = participants.Count;

            foreach (Joueur joueur in participants)
                salon.AjouterJoueur(joueur);

            Joueur client = new Joueur(nom, initial);
            AjouterJoueur(client);
            reseau.EnvoyerJoueur(client);

            salon.Show();

            if (participants.Count < Nombre)
                new Thread(AttendreJoueur).Start();
            else {
                AjouterCroupier();
                Jouer();
            }
        }

        #endregion

        #region Propriétés

        /// <summary>Obtient le nombre de joueurs de la partie.</summary>
        private int Nombre { get => participants.Capacity - 1; }

        /// <summary>Obtient l'indice du croupier dans la liste de participants.</summary>
        private int Croupier { get => participants.Count - 1; }

        /// <summary>Évalue si le le joueur local est l'hôte de la partie.</summary>
        private bool EstHote { get => local == HOTE; }

        /// <summary>Évalue si le joueur local est le joueur actif de la partie.</summary>
        private bool EstActif { get => local == actif; }

        #endregion

        #region Méthodes publiques

        /// <summary>Démarre une partie de Blackjack.</summary>
        public void Jouer() {
            foreach (Participant participant in participants)
                participant.Defausser();

            actif = HOTE;
            participants[actif].Actif = true;

            if (EstActif && participants[actif] is Joueur joueur)
                salon.AfficherMise(joueur.Montant < min ? joueur.Montant : min, joueur.Montant);
            else
                new Thread(AttendreMise).Start();
        }

        /// <summary>Effectue une mise du montant spécifié.</summary>
        /// <param name="mise">Mise du joueur.</param>
        public void Miser(double mise) {
            if (EstActif && reseau != null)
                reseau.EnvoyerMise(mise);

            participants[actif].Actif = false;

            if (participants[actif++] is Joueur joueur)
                joueur.Miser(mise);

            if (participants[actif] is Joueur suivant)
                suivant.Actif = true;

            if (EstActif && participants[actif] is Joueur miseur)
                salon.AfficherMise(miseur.Montant < min ? miseur.Montant : min, miseur.Montant);
            else if (participants[actif] is Croupier) {
                DistribuerCartes();
                participants[actif].Actif = true;
                if (EstActif)
                    salon.DebloquerActions();
                else
                    new Thread(AttendreCoup).Start();
            } else
                new Thread(AttendreMise).Start();
        }

        /// <summary>Effectue l'action de tirer une carte (HIT).</summary>
        public void Tirer() {
            if (EstActif && reseau != null)
                reseau.EnvoyerCoup(true);

            participants[actif].Tirer(sabot.Piocher());

            if (participants[actif].Total >= 21) // Si le joueur saute ou compte déjà 21
                TourSuivant();
            else if (participants[actif] is Croupier)
                CoupCroupier();
            else if (!EstActif)
                new Thread(AttendreCoup).Start();
        }

        /// <summary>Effectue l'action de rester (STAND).</summary>
        public void Rester() {
            if (EstActif && reseau != null)
                reseau.EnvoyerCoup(false);

            participants[actif].Rester();
            TourSuivant();
        }

        #endregion

        #region Méthodes privées

        /// <summary>Distribue les cartes aux joueurs et au croupier.</summary>
        private void DistribuerCartes() {
            for (actif = 0; actif < Croupier; actif++)
                for (int i = 0; i < 2; i++)
                    participants[actif].Piocher(sabot.Piocher());

            participants[Croupier].Piocher(sabot.Piocher());

            actif = HOTE;
        }

        /// <summary>Passe au tour suivant.</summary>
        private void TourSuivant() {
            if (EstActif)
                salon.BloquerActions();

            participants[actif++].Actif = false;

            if (actif == participants.Count)
                Fin();
            else {
                participants[actif].Actif = true;

                if (EstActif) // Si c'est maintenant ton tour
                    salon.DebloquerActions();
                else if (participants[actif] is Croupier)
                    CoupCroupier(); // Faire jouer le croupier
                else
                    new Thread(AttendreCoup).Start();
            }
        }

        /// <summary>Effectue le coup du croupier.</summary>
        private void CoupCroupier() {
            if (participants[actif] is Croupier croupier)
                if (croupier.Action)
                    Tirer();
                else
                    Rester();
        }

        /// <summary>Effectue la fin d'une partie de Blackjack.</summary>
        private void Fin() {
            string msg = "";

            if (participants[Croupier].Saute) {
                foreach (Joueur joueur in participants.GetRange(0, Croupier))
                    if (!joueur.Saute) {
                        joueur.Gagner();
                        msg += joueur.Nom + " a gagné(e) " + joueur.Mise + " $\n";
                    } else
                        msg += joueur.Nom + " a perdu(e) " + joueur.Mise + " $\n";
            } else {
                foreach (Joueur joueur in participants.GetRange(0, Croupier))
                    if (joueur.Total == participants[Croupier].Total) {
                        joueur.Egaliter();
                        msg += joueur.Nom + " a égalisé(e) le croupier.\n";
                    } else if (joueur.Total > participants[Croupier].Total && !joueur.Saute) {
                        joueur.Gagner();
                        msg += joueur.Nom + " a gagné(e) " + joueur.Mise + " $\n";
                    } else
                        msg += joueur.Nom + " a perdu(e) " + joueur.Mise + " $\n";
            }

            MessageBox.Show(msg, "Résultat de la partie", MessageBoxButtons.OK, MessageBoxIcon.None);

            List<Joueur> elimine = new List<Joueur>(); // Liste temporaire pour stocker les participants à éliminer
            foreach (Joueur joueur in participants.Where(participant => participant is Joueur joueur && joueur.Montant <= 0))
                elimine.Add(joueur);

            foreach (Joueur joueur in elimine)
                Eliminer(joueur);

            if (participants.Count > 1)
                Jouer();
            else {
                MessageBox.Show("Nombre de participants insufissant pour poursuivre la partie. Retour au menu principal.", "Retour au menu principal", MessageBoxButtons.OK, MessageBoxIcon.None);
                salon.Close();
            }
        }

        /// <summary>Élimine le joueur spécifié de la partie.</summary>
        /// <param name="joueur">Joueur à éliminé</param>
        private void Eliminer(Joueur joueur) {
            if (joueur == participants[local])
                MessageBox.Show("Votre montant a atteint 0 $. Votre partie est terminée. Meilleure chance la prochaine fois!", "Partie terminée", MessageBoxButtons.OK, MessageBoxIcon.None);

            participants.Remove(joueur);
            salon.RetirerJoueur(joueur);
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

        #endregion

        #region Appels réseaux

        /// <summary>Ajoute un joueur distant à la partie. Cet appel est bloquant.</summary>
        /// <remarks>Puisque cet appel est bloquant il est recommandé de le placer dans un fil.</remarks>
        private void AttendreJoueur() {
            if (EstHote)
                reseau.ObtenirConnexion(this);

            AjouterJoueur(EstHote ? reseau.ObtenirJoueur(participants.Count - 1) : reseau.ObtenirJoueur());

            if (participants.Count < Nombre)
                new Thread(AttendreJoueur).Start();
            else {
                AjouterCroupier();
                Jouer();
            }
        }

        /// <summary>Reçoit la mise d'un joueur distant. Cet appel est bloquant.</summary>
        /// <remarks>Puisque cet appel est bloquant il est recommandé de le placer dans un fil.</remarks>
        private void AttendreMise() => Miser(EstHote ? reseau.ObtenirMise(actif - 1) : reseau.ObtenirMise());

        /// <summary>Reçoit le coup d'un joueur distant. Cet appel est bloquant.</summary>
        /// <remarks>Puisque cet appel est bloquant il est recommandé de le placer dans un fil.</remarks>
        private void AttendreCoup() {
            if (EstHote ? reseau.ObtenirCoup(actif - 1) : reseau.ObtenirCoup())
                Tirer();
            else
                Rester();
        }

        #endregion
    }
}
