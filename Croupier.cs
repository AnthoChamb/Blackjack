namespace Blackjack {
    /// <summary>Classe d'un croupier.</summary>
    public class Croupier : Participant {
        /// <summary>Crée un croupier.</summary>
        public Croupier() : base("Croupier") => control.Croupier = true;

        /// <summary>Évalue si le croupier doit tirer ou rester.</summary>
        public bool Action { get => IA.Action(Total); }
    }
}
