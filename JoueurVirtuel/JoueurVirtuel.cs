namespace Blackjack {
    /// <summary>Classe responsable des actions de joueurs virtuel.</summary>
    public static class JoueurVirtuel {
        /// <summary>Évalue si le joueur virtuel doit tirer ou rester.</summary>
        /// <param name="total">Total du joueur virtuel.</param>
        /// <returns>Retourne si le joueur virtuel doit tirer (True) ou rester (False)</returns>
        public static bool Action(int total) => total < 17;
    }
}
