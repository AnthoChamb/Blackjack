namespace Blackjack {
    /// <summary>Classe d'un joueur virtuel.</summary>
    public class JoueurVirtuel : Joueur {
        /// <summary>Crée un joueur virtuel.</summary>
        /// <param name="nom">Nom du joueur virtuel.</param>
        /// <param name="montant">Montant initial du joueur virtuel.</param>
        public JoueurVirtuel(string nom, double montant) : base(nom, montant) { }

        /// <summary>Évalue si le joueur virtuel doit tirer ou rester.</summary>
        public bool Action { get => IA.Action(Total); }
    }
}
