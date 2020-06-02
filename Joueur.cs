using Blackjack.Controls;
using System;
using System.Runtime.Serialization;

namespace Blackjack {
    /// <summary>Classe d'un joueur de Blackjack.</summary>
    [Serializable]
    public class Joueur : Participant {
        private double montant;
        private double mise;

        /// <summary>Crée un joueur.</summary>
        /// <param name="nom">Nom du joueur.</param>
        /// <param name="montant">Montant initial du joueur.</param>
        /// <exception cref="ArgumentOutOfRangeException">Le montant initial du joueur doit être plus grand que 0.</exception>
        public Joueur(string nom, double montant) : base(nom) {
            this.montant = montant > 0 ? montant : throw new ArgumentOutOfRangeException("montant", "Le montant initial du joueur doit être plus grand que 0.");
            control.Montant = montant;
        }

        /// <summary>Obtient le montant du joueur.</summary>
        public double Montant { get => montant; }

        /// <summary>Obtient la mise actuelle du joueur.</summary>
        public double Mise { get => mise; }

        /// <summary>Effectue une mise du montant spécifié.</summary>
        /// <param name="mise">Montant de la mise.</param>
        public void Miser(double mise) {
            this.mise = mise;
            montant -= mise;

            control.Montant = montant;
            control.Action = "Mise " + mise + " $";
        }

        /// <summary>Effectue l'action lors d'une victoire avec un Blackjack contre le croupier.</summary>
        public void GagnerBlackjack() {
            montant += mise * 2.5;
            control.Montant = montant;

            control.Action = "Gagne avec Blackjack";
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

        /// <summary>Effectue l'action de perdre contre le coupier.</summary>
        public void Perdre() => control.Action = "Perdu";

        /// <summary>Génère le contrôle utilisateur graphique du joueur après la sérialisation.</summary>
        /// <param name="contexte">Contexte du flux sérialisé</param>
        [OnDeserialized()]
        private void GenererControl(StreamingContext contexte) => control = new ControlParticipant(nom) { Montant = montant };
    }
}
