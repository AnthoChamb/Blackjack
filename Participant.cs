using Blackjack.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Blackjack {
    /// <summary>Classe d'un participant de Blackjack. Cette classe ne peut pas être instanciée.</summary>
    [Serializable]
    public abstract class Participant {
        protected readonly string nom;
        protected readonly List<Carte> main;
        protected bool reste;

        [NonSerialized] private ControlParticipant control;

        /// <summary>Crée un participant.</summary>
        /// <param name="nom">Nom du participant.</param>
        /// /// <exception cref="ArgumentException">Le nom du participant ne peut pas être une chaine vide.</exception>
        /// <exception cref="ArgumentNullException">Le nom du participant ne peut pas être la valeur null.</exception>
        protected Participant(string nom) {
            this.nom = nom == "" ? throw new ArgumentException("Le nom du participant ne peut pas être une chaine vide.", "nom") : nom ?? throw new ArgumentNullException("nom", "Le nom du participant ne peut pas être la valeur null.");
            main = new List<Carte>(2);
            control = new ControlParticipant(nom);
        }

        /// <summary>Obtient le nom du participant</summary>
        public string Nom { get => nom; }

        /// <summary>Obtient le contrôle utilisateur graphique associé à ce participant.</summary>
        public ControlParticipant Control { get => control ?? (control = GenererControl()); }

        /// <summary>Obtient le total du participant.</summary>
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

        /// <summary>Obtient et définit la propriété Actif du control utilisateur graphique.</summary>
        public bool Actif { get => Control.Actif; set => Control.Actif = value; }

        /// <summary>Évalue si le participant saute.</summary>
        public bool Saute { get => Total > 21; }

        /// <summary>Évalue si le participant reste (STAND).</summary>
        public bool Reste { get => reste; }

        /// <summary>Ajoute la carte spécifié à la main du participant.</summary>
        /// <param name="carte">Carte piochée.</param>
        public void Piocher(Carte carte) {
            main.Add(carte);
            Control.AjouterCarte(carte.Control);
        }

        /// <summary>Effectue le tir de la carte spécifié.</summary>
        /// <param name="carte">Carte tirée.</param>
        public void Tirer(Carte carte) {
            main.Add(carte);
            Control.AjouterCarte(carte.Control);
            Control.Action = "Tire";
        }

        /// <summary>Effectue l'action de rester.</summary>
        public void Rester() {
            reste = true;
            Control.Action = "Reste";
        }

        /// <summary>Vide la main du participant et réinitialise son statut de jeu.</summary>
        public void Defausser() {
            main.Clear();
            Control.Defausser();

            reste = false;
        }

        /// <summary>Génère le contrôle utilisateur graphique associé à ce participant.</summary>
        /// <returns>Retourne le contrôle utilisateur graphique associé à ce participant.</returns>
        protected abstract ControlParticipant GenererControl();
    }
}
