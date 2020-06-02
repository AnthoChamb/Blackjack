using Blackjack.Controls;
using System;

namespace Blackjack {
    /// <summary>Classe d'une carte à jouer.</summary>
    [Serializable]
    public class Carte {
        private readonly int figure;
        private readonly Atout atout;

        /// <summary>Crée une carte à jouer.</summary>
        /// <param name="figure">Figure de la carte de 1 (As) à 13 (Roi).</param>
        /// <param name="atout">Atout de la carte.</param>
        /// <exception cref="ArgumentOutOfRangeException">La figure de la carte doit être entre 1 (As) et 13 (Roi).</exception>
        public Carte(int figure, Atout atout) {
            this.figure = figure > 0 && figure <= 13 ? figure : throw new ArgumentOutOfRangeException("figure", "La figure de la carte doit être entre 1 (As) et 13 (Roi).");
            this.atout = atout;
        }

        /// <summary>Obtient la figure de la carte de 1 (As) à 13 (Roi).</summary>
        public int Figure { get => figure; }

        /// <summary>Obtient la valeur de la carte en utilisant 1 comme valeur par défaut pour l'as.</summary>
        public int Valeur { get => ObtenirValeur(false); }

        /// <summary>Obtient le contrôle utilisateur graphique associé à cette carte.</summary>
        public ControlCarte Control { get => new ControlCarte(ToString(), atout.Couleur, atout.Image); }

        /// <summary>Obtient la valeur de la carte en précisant la valeur de l'as.</summary>
        /// <param name="onze">Détermine si l'as vaut onze ou un.</param>
        /// <returns>Retourne la valeur de la carte.</returns>
        public int ObtenirValeur(bool onze) {
            switch (figure) {
                case 1:
                    return onze ? 11 : 1;
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                    return figure;
                case 11:
                case 12:
                case 13:
                    return 10;
                default:
                    return -1;
            }
        }

        public override string ToString() {
            switch (figure) {
                case 1:
                    return "A";
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                    return figure.ToString();
                case 11:
                    return "J";
                case 12:
                    return "Q";
                case 13:
                    return "K";
                default:
                    return null;
            }
        }
    }
}
