using Blackjack.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack {
    /// <summary>Classe d'une carte à jouer.</summary>
    public class Carte {
        private readonly byte figure;
        private readonly Atout atout;

        /// <summary>Crée une carte à jouer</summary>
        /// <param name="figure">Figure de la carte de 1 (As) à 13 (Roi).</param>
        /// <param name="atout">Atout de la carte.</param>
        /// <exception cref="ArgumentOutOfRangeException">La figure de la carte doit être entre 1 et 13.</exception>
        public Carte(byte figure, Atout atout) {
            if (figure > 0 && figure <= 13)
            this.figure = figure > 0 && figure <= 13 ? figure : throw new ArgumentOutOfRangeException("figure", "La figure de la carte doit être entre 1 et 13.");
            this.atout = atout;
        }

        /// <summary>Obtient la figure de la carte de 1 (As) à 13 (Roi).</summary>
        public byte Figure { get => figure; }

        /// <summary>Obtient le controle utilisateur graphique associé à cette carte.</summary>
        public ControlCarte Control { get => new ControlCarte(ToString(), atout.Couleur, atout.Image); }

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
