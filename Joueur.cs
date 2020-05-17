using Blackjack.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack {
    public class Joueur {
        private readonly string nom;
        private double montant;
        private readonly List<Carte> main;

        private double mise;
        private bool reste;

        private readonly ControlJoueur control;

        /// <summary>Crée un joueur.</summary>
        /// <param name="nom">Nom du joueur.</param>
        /// <param name="montant">Montant initial du joueur.</param>
        public Joueur(string nom, double montant) {
            this.nom = nom;
            this.montant = montant;
            main = new List<Carte>(2);
            control = new ControlJoueur(nom, montant, "A rejoint");
        }

        /// <summary>Obtient le nom du joueur</summary>
        public string Nom { get => nom; }

        /// <summary>Obtient la mise actuelle du joueur.</summary>
        public double Mise { get => mise; }

        /// <summary>Obtient le controle utilisateur graphique associé à ce joueur.</summary>
        public ControlJoueur Control { get => control; }

        /// <summary>Obtient le total du joueur.</summary>
        /// <remarks>Le total obtenu calcule lui-même les as dans le but d'obtenir le total le plus près de 21 sans le dépasser lorsque cela est possible.</remarks>
        public int Total { 
            get {
                List<int> possibles = new List<int>(1) { 0 }; // Liste des totaux possibles
                int i; // Variable de bouclage

                foreach (Carte carte in main) {
                    for (i = 0; i < possibles.Count; i++) 
                        possibles[i] += carte.Valeur;

                    if (carte.Figure == 1)
                        possibles.Add(possibles.Last() + 10);
                }

                int total = possibles[0]; // Obtient le plus petit total possible
                i = 1;

                while (i < possibles.Count && possibles[i] > total && possibles[i] <= 21)
                    total = possibles[i++];

                return total;
            }
        }

        /// <summary>Évalue si le joueur saute.</summary>
        public bool Saute { get => Total > 21; }

        /// <summary>Évalue si le joueur reste (STAND).</summary>
        public bool Reste { get => reste; }

        /// <summary>Effectue une mise du montant spécifié.</summary>
        /// <param name="mise">Montant de la mise.</param>
        public void Miser(double mise) {
            this.mise = mise;
            montant -= mise;

            control.Montant = montant;
            control.Action = "Mise " + mise + " $";
        }

        /// <summary>Ajoute la carte spécifié à la main du joueur.</summary>
        /// <param name="carte">Carte piochée.</param>
        public void Piocher(Carte carte) {
            main.Add(carte);
            control.AjouterCarte(carte.Control);
        }

        /// <summary>Effectue le tire de la carte spécifié.</summary>
        /// <param name="carte">Carte tirée.</param>
        public void Tirer(Carte carte) {
            main.Add(carte);
            control.AjouterCarte(carte.Control);
            control.Action = "Tire";
        }

        /// <summary>Effectue l'action de rester.</summary>
        public void Rester() {
            reste = true;
            control.Action = "Reste";
        }

        /// <summary>Vide la main du joueur et réinitialise sa mise et son statut de jeu.</summary>
        public void Defausser() {
            main.Clear();
            control.Defausser();

            reste = false;
        }

        /// <summary>Effectue l'action lors d'une victoire contre le croupier.</summary>
        public void Gagner() {
            montant += mise * 2;
            control.Montant = montant;

            control.Action = "Gagne";
        }

        /// <summary>Effectue l'action lors d'égalité avec le croupier.</summary>
        public void Egaliter() {
            montant += mise;
            control.Montant = montant;

            control.Action = "Égalité";
        }
    }
}
