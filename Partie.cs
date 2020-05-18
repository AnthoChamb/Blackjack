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
    [Serializable]
    public class Partie {
        #region Données membres

        [NonSerialized] private readonly Salon salon;
        private readonly Sabot sabot;
        private readonly List<Participant> joueurs;

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
        /// <param name="min">Mise minimal des joueurs.</param>
        public Partie(string nom, int nombre, double initial, double min) {
            salon = new Salon(this);

            sabot = new Sabot(6);
            sabot.Melanger();

            Joueur hote = new Joueur(nom, initial);
            joueurs = new List<Participant>(nombre + 1) { hote };
            salon.AjouterJoueur(hote);

            this.initial = initial;
            this.min = min;
            local = HOTE;

            salon.Show();

            if (nombre > 1) {
                reseau = new Reseau();
                new Thread(AttendreJoueur).Start();
            } else {
                AjouterCroupier();
                Jouer();
            }
        }

        public Partie(string nom, IPAddress ip) {
            reseau = new Reseau(ip);
            Partie hote = reseau.ObtenirPartie();

            sabot = hote.sabot;
            joueurs = hote.joueurs;
            initial = hote.initial;
            min = hote.min;

            salon = new Salon(this);
            local = joueurs.Count;

            foreach (Joueur joueur in joueurs)
                salon.AjouterJoueur(joueur);

            Joueur client = new Joueur(nom, initial);
            AjouterJoueur(client);
            reseau.EnvoyerJoueur(client);

            salon.Show();

            if (joueurs.Count < Nombre)
                new Thread(AttendreJoueur).Start();
            else {
                AjouterCroupier();
                Jouer();
            }
        }

        #endregion

        #region Propriétés

        private int Nombre { get => joueurs.Capacity - 1; }

        private bool EstHote { get => local == HOTE; }

        private bool EstActif { get => local == actif; }

        #endregion

        #region Méthodes publiques

        /// <summary>Démarre une partie de Blackjack.</summary>
        public void Jouer() {
            foreach (Participant participant in joueurs)
                participant.Defausser();

            actif = HOTE;
            joueurs[actif].Actif = true;

            if (EstActif && joueurs[actif] is Joueur joueur)
                salon.AfficherMise(joueur.Montant < min ? joueur.Montant : min, joueur.Montant);
            else
                new Thread(AttendreMise).Start();
        }

        /// <summary>Effectue une mise du montant spécifié.</summary>
        /// <param name="mise">Mise du joueur.</param>
        public void Miser(double mise) {
            if (EstActif && Nombre > 1)
                reseau.EnvoyerMise(mise);

            joueurs[actif].Actif = false;

            if (joueurs[actif++] is Joueur joueur)
                joueur.Miser(mise);

            if (joueurs[actif] is Joueur suivant)
                suivant.Actif = true;

            if (EstActif && joueurs[actif] is Joueur miseur)
                salon.AfficherMise(miseur.Montant < min ? miseur.Montant : min, miseur.Montant);
            else if (joueurs[actif] is Croupier) {
                DistribuerCartes();
                joueurs[actif].Actif = true;
                if (EstActif)
                    salon.DebloquerActions();
                else
                    new Thread(AttendreCoup).Start();
            } else
                new Thread(AttendreMise).Start();
        }

        /// <summary>Effectue l'action de tirer une carte (HIT).</summary>
        public void Tirer() {
            if (EstActif)
                reseau.EnvoyerCoup(true);

            joueurs[actif].Tirer(sabot.Piocher());

            if (joueurs[actif].Total >= 21) // Si le joueur saute ou compte déjà 21
                TourSuivant();
            else if (joueurs[actif] is Croupier)
                CoupCroupier();
            else if (!EstActif)
                new Thread(AttendreCoup).Start();
        }

        /// <summary>Effectue l'action de rester (STAND).</summary>
        public void Rester() {
            if (EstActif)
                reseau.EnvoyerCoup(false);

            joueurs[actif].Rester();
            TourSuivant();
        }

        #endregion

        #region Méthodes privées

        /// <summary>Distribue les cartes aux joueurs et au croupier.</summary>
        private void DistribuerCartes() {
            for (actif = 0; actif < joueurs.Count - 1; actif++)
                for (int i = 0; i < 2; i++)
                    joueurs[actif].Piocher(sabot.Piocher());

            joueurs[actif].Piocher(sabot.Piocher());

            actif = HOTE;
        }

        /// <summary>Passe au tour suivant.</summary>
        private void TourSuivant() {
            if (EstActif)
                salon.BloquerActions();

            joueurs[actif++].Actif = false;

            if (actif != joueurs.Count)
                joueurs[actif].Actif = true;

            if (EstActif) // Si c'est maintenant ton tour
                salon.DebloquerActions();
            else if (actif == joueurs.Count) // Si tout le monde à jouer
                Fin();
            else if (joueurs[actif] is Croupier)
                CoupCroupier(); // Faire jouer le croupier
            else
                new Thread(AttendreCoup).Start();
        }

        /// <summary>Effectue le coup du croupier.</summary>
        private void CoupCroupier() {
            if (joueurs[actif] is Croupier croupier)
                if (croupier.Action)
                    Tirer();
                else
                    Rester();
        }

        /// <summary>Effectue la fin d'une partie de Blackjack.</summary>
        private void Fin() {
            string msg = "";

            if (joueurs[joueurs.Count - 1].Saute) {
                foreach (Joueur joueur in joueurs.GetRange(0, joueurs.Count - 1))
                    if (!joueur.Saute) {
                        joueur.Gagner();
                        msg += joueur.Nom + " a gagné(e) " + joueur.Mise + " $\n";
                    } else
                        msg += joueur.Nom + " a perdu(e) " + joueur.Mise + " $\n";
            } else {
                foreach (Joueur joueur in joueurs.GetRange(0, joueurs.Count - 1))
                    if (joueur.Total == joueurs[joueurs.Count - 1].Total) {
                        joueur.Egaliter();
                        msg += joueur.Nom + " a égalisé(e) le croupier.\n";
                    } else if (joueur.Total > joueurs[joueurs.Count - 1].Total && !joueur.Saute) {
                        joueur.Gagner();
                        msg += joueur.Nom + " a gagné(e) " + joueur.Mise + " $\n";
                    } else
                        msg += joueur.Nom + " a perdu(e) " + joueur.Mise + " $\n";
            }

            MessageBox.Show(msg, "Résultat de la partie", MessageBoxButtons.OK, MessageBoxIcon.None);

            List<Joueur> elimine = new List<Joueur>(); // Liste temporaire pour stocker les joueurs à éliminer
            foreach (Joueur joueur in joueurs.Where(participant => participant is Joueur joueur && joueur.Montant <= 0))
                elimine.Add(joueur);

            foreach (Joueur joueur in elimine)
                Eliminer(joueur);

            if (joueurs.Count > 1)
                Jouer();
            else {
                MessageBox.Show("Nombre de joueurs insufissant pour poursuivre la partie. Retour au menu principal.", "Retour au menu principal", MessageBoxButtons.OK, MessageBoxIcon.None);
                salon.Close();
            }
        }

        /// <summary></summary>
        /// <param name="joueur">Joueur à éliminé</param>
        private void Eliminer(Joueur joueur) {
            if (joueur == joueurs[local])
                MessageBox.Show("Votre montant a atteint 0 $. Votre partie est terminée. Meilleure chance la prochaine fois!", "Partie terminée", MessageBoxButtons.OK, MessageBoxIcon.None);

            joueurs.Remove(joueur);
            salon.RetirerJoueur(joueur);
        }

        private void AjouterJoueur(Joueur joueur) {
            joueurs.Add(joueur);
            salon.AjouterJoueur(joueur);
        }

        private void AjouterCroupier() {
            Croupier croupier = new Croupier();
            joueurs.Add(croupier);
            salon.AjouterCroupier(croupier);
        }

        #endregion

        #region Appels réseaux

        private void AttendreJoueur() {
            if (EstHote)
                reseau.ObtenirConnexion(this);

            AjouterJoueur(EstHote ? reseau.ObtenirJoueur(joueurs.Count - 1) : reseau.ObtenirJoueur());

            if (joueurs.Count < Nombre)
                new Thread(AttendreJoueur).Start();
            else {
                AjouterCroupier();
                Jouer();
            }
        }

        private void AttendreMise() => Miser(EstHote ? reseau.ObtenirMise(actif - 1) : reseau.ObtenirMise());

        private void AttendreCoup() {
            if (EstHote ? reseau.ObtenirCoup(actif - 1) : reseau.ObtenirCoup())
                Tirer();
            else
                Rester();
        }

        #endregion
    }
}
