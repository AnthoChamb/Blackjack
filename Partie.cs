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

            if (nombre > 1) {
                reseau = new Reseau(this);
                while (joueurs.Count < nombre) {
                    Joueur client = reseau.ObtenirJoueur(joueurs.Count - 1);
                    joueurs.Add(client);
                    salon.AjouterJoueur(client);
                }
            }
            
            Croupier croupier = new Croupier();
            joueurs.Add(croupier);
            salon.AjouterCroupier(croupier);
        }

        public Partie(string nom, IPAddress ip) {
            reseau = new Reseau(this, ip);
            Partie hote = reseau.ObtenirPartie();
            sabot = hote.sabot;
            joueurs = hote.joueurs;
            initial = hote.initial;
            min = hote.min;

            salon = new Salon(this);
            local = joueurs.Count;

            salon.AjouterJoueur((Joueur)joueurs[HOTE]);

            Joueur client = new Joueur(nom, initial);
            joueurs.Add(client);
            salon.AjouterJoueur(client);

            reseau.EnvoyerJoueur(client);

            Croupier croupier = new Croupier();
            joueurs.Add(croupier);
            salon.AjouterCroupier(croupier);
        }

        #endregion

        #region Propriétés

        public double Initial { get => initial; }

        public double Min { get => min; }

        public int Nombre { get => joueurs.Capacity - 1; }

        #endregion

        #region Méthodes publiques

        /// <summary>Démarre une partie de Blackjack.</summary>
        public void Jouer() {
            if (!salon.Visible)
                salon.Show();

            foreach (Participant participant in joueurs)
                participant.Defausser();

            actif = HOTE;

            if (actif == local && joueurs[actif] is Joueur joueur)
                salon.AfficherMise(joueur.Montant < min ? joueur.Montant : min, joueur.Montant);
            else
                new Thread(() => Miser(reseau.ObtenirMise())).Start();
        }

        /// <summary>Effectue une mise du montant spécifié.</summary>
        /// <param name="mise">Mise du joueur.</param>
        public void Miser(double mise) {
            if (actif == local && Nombre > 1)
                reseau.EnvoyerMise(mise);

            if (joueurs[actif++] is Joueur joueur)
                joueur.Miser(mise);

            if (actif == local && joueurs[actif] is Joueur miseur)
                salon.AfficherMise(miseur.Montant < min ? miseur.Montant : min, miseur.Montant);
            else if (joueurs[actif] is Croupier) {
                DistribuerCartes();
                if (local == actif)
                    salon.DebloquerActions();
                else
                    new Thread(CoupReseau).Start();
            } else
                new Thread(() => Miser(reseau.ObtenirMise())).Start();
        }

        /// <summary>Effectue l'action de tirer une carte (HIT).</summary>
        public void Tirer() {
            if (actif == local)
                reseau.EnvoyerCoup(true);

            joueurs[actif].Tirer(sabot.Piocher());

            if (joueurs[actif].Total >= 21) // Si le joueur saute ou compte déjà 21
                TourSuivant();
            else if (joueurs[actif] is Croupier)
                CoupCroupier();
            else if (actif != local)
                new Thread(CoupReseau).Start();
        }

        /// <summary>Effectue l'action de rester (STAND).</summary>
        public void Rester() {
            if (actif == local)
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
            if (actif == local)
                salon.BloquerActions();

            actif++;

            if (actif == local) // Si c'est maintenant ton tour
                salon.DebloquerActions();
            else if (actif == joueurs.Count)
                Fin();
            else if (joueurs[actif] is Croupier)
                CoupCroupier(); // Faire jouer le croupier
            else
                new Thread(CoupReseau).Start();
        }

        /// <summary>Effectue le coup du croupier.</summary>
        private void CoupCroupier() {
            if (joueurs[actif] is Croupier croupier)
                if (croupier.Action)
                    Tirer();
                else
                    Rester();
        }

        private void CoupReseau() {
            if (reseau.ObtenirCoup())
                Tirer();
            else
                Rester();
        }

        /// <summary>Effectue la fin d'une partie de Blackjack.</summary>
        private void Fin() {
            string msg = "";

            if (joueurs[joueurs.Count - 1].Saute) {
                foreach (Joueur joueur in joueurs.Where(participant => participant is Joueur))
                    if (!joueur.Saute) {
                        joueur.Gagner();
                        msg += joueur.Nom + " a gagné(e) " + joueur.Mise + " $\n";
                    } else
                        msg += joueur.Nom + " a perdu(e) " + joueur.Mise + " $\n";
            } else {
                foreach (Joueur joueur in joueurs.Where(participant => participant is Joueur))
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

        #endregion

        #region Appels réseau

        internal string ObtenirInfo() => Nombre + ";" + initial + ";" + min;

        internal void DefinirInfo(string reseau) {
            string[] info = reseau.Split(';'); 
            joueurs.Capacity = int.Parse(info[0]) + 1;
            initial = double.Parse(info[1]);
            min = double.Parse(info[2]);
        }

        #endregion
    }
}
