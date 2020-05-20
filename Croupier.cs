using Blackjack.Controls;

namespace Blackjack {
    /// <summary>Classe d'un croupier.</summary>
    public class Croupier : Participant {
        /// <summary>Crée un croupier.</summary>
        public Croupier() : base("Croupier") => Control.Croupier = true;

        /// <summary>Évalue si le croupier doit tirer ou rester.</summary>
        public bool Action { get => JoueurVirtuel.Action(Total); }

        protected override ControlParticipant GenererControl() => new ControlParticipant(nom) { Croupier = true };
    }
}
