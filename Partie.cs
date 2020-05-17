using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blackjack {
    public class Partie {
        private readonly Salon salon;
        private readonly Sabot sabot;
        private readonly List<Joueur> joueurs;

        private readonly double initial;
        private readonly double min;

        private int actif;
        private readonly int local;

        /// <summary>Constante utilisée pour représenter l'hôte.</summary>
        private const int HOTE = 0;

        /// <summary>Héberge une partie.</summary>
        /// <param name="nom">Nom de l'hôte.</param>
        /// <param name="joueurs">Nombre de joueurs dans la partie.</param>
        /// <param name="initial">Montant initial des joueurs.</param>
        /// <param name="min">Mise minimal des joueurs.</param>
        public Partie(string nom, int joueurs, double initial, double min) {
            salon = new Salon(this);

            sabot = new Sabot(6);
            sabot.Melanger();

            this.joueurs = new List<Joueur>(joueurs) {
                new Joueur(nom, initial)
            };
            salon.AjouterJoueur(this.joueurs.Last());

            this.joueurs.Add(new Joueur("Croupier", initial));
            salon.AjouterCroupier(this.joueurs.Last());

            this.initial = initial;
            this.min = min;
            local = HOTE;
        }

        /// <summary>Démarre une partie de Blackjack.</summary>
        public void Jouer() {
            if (!salon.Visible)
                salon.Show();

            foreach (Joueur joueur in joueurs)
                joueur.Defausser();

            actif = HOTE;
            salon.AfficherMise(min);
        }

        /// <summary>Distribue les cartes aux joueurs et au croupier.</summary>
        private void DistribuerCartes() {
            for (actif = 0; actif < joueurs.Count - 1; actif++)
                for (int j = 0; j < 2; j++)
                    joueurs[actif].Piocher(sabot.Piocher());

            joueurs[actif].Piocher(sabot.Piocher());

            actif = HOTE;
        }

        /// <summary>Effectue une mise du montant spécifié.</summary>
        /// <param name="mise">Mise du joueur.</param>
        public void Miser(double mise) {
            joueurs[actif++].Miser(mise);

            if (actif == joueurs.Count - 1) {
                joueurs[actif].Miser(min);
                DistribuerCartes();
                if (local == 0) {
                    salon.DebloquerActions();
                }
            }
        }

        /// <summary>Effectue l'action de tirer une carte (HIT).</summary>
        public void Tirer() {
            joueurs[actif].Tirer(sabot.Piocher());

            if (joueurs[actif].Total >= 21) // Si le joueur saute ou compte déjà 21
                TourSuivant();
        }

        /// <summary>Effectue l'action de rester (STAND).</summary>
        public void Rester() {
            joueurs[actif].Rester();
            TourSuivant();
        }

        /// <summary>Passe au tour suivant.</summary>
        private void TourSuivant() {
            if (actif == local)
                salon.BloquerActions();

            actif++;

            if (actif == local) // Si c'est maintenant ton tour
                salon.DebloquerActions();
            else if (sabot != null && actif == joueurs.Count - 1) // Seulement si hote
                TourJoueurVirtuel(actif); // Faire jouer le croupier
            else if (actif == joueurs.Count)
                Fin();
        }

        /// <summary>Effectue le tour d'un joueur virtuel.</summary>
        /// <param name="joueur">Indice du joueur virtuel.</param>
        private void TourJoueurVirtuel(int joueur) {
            while (actif == joueur) {
                if (JoueurVirtuel.Action(joueurs[actif].Total))
                    Tirer();
                else
                    Rester();
            }
        }

        /// <summary>Effectue la fin d'une partie de Blackjack.</summary>
        private void Fin() {
            string msg = "";

            if (joueurs[joueurs.Count - 1].Saute) {
                for (actif = 0; actif < joueurs.Count - 1; actif++)
                    if (!joueurs[actif].Saute) {
                        joueurs[actif].Gagner();
                        msg += joueurs[actif].Nom + " a gagné(e) " + joueurs[actif].Mise + " $\n";
                    } else
                        msg += joueurs[actif].Nom + " a perdu(e) " + joueurs[actif].Mise + " $\n";
            } else {
                for (actif = 0; actif < joueurs.Count - 1; actif++)
                    if (joueurs[actif].Total == joueurs[joueurs.Count - 1].Total) {
                        joueurs[actif].Egaliter();
                        msg += joueurs[actif].Nom + " a égalisé(e) le croupier.\n";
                    } else if (joueurs[actif].Total > joueurs[joueurs.Count - 1].Total && !joueurs[actif].Saute) {
                        joueurs[actif].Gagner();
                        msg += joueurs[actif].Nom + " a gagné(e) " + joueurs[actif].Mise + " $\n";
                    } else
                        msg += joueurs[actif].Nom + " a perdu(e) " + joueurs[actif].Mise + " $\n";
            }

            MessageBox.Show(msg, "Résultat de la partie", MessageBoxButtons.OK, MessageBoxIcon.None);

            Jouer();
        }
    }
}
