namespace Blackjack {
    /// <summary>Classe responsable des actions de l'intelligence artificielle.</summary>
    public static class IA {
        /// <summary>Évalue si le participant doit tirer ou rester.</summary>
        /// <param name="total">Total du participant.</param>
        /// <returns>Retourne si le participant doit tirer (True) ou rester (False)</returns>
        public static bool Action(int total) => total < 17;
    }
}