using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack {
    /// <summary>Classe d'un sabot. Un rasemblement de paquets de cartes à jouer.</summary>
    public class Sabot : Paquet {
        /// <summary>Crée un sabot contenant un seul paquet.</summary>
        public Sabot() {

        }

        /// <summary>Crée un sabot contenant le nombre de paquets spécifié.</summary>
        /// <param name="paquets">Nombre de paquets contenus dans le sabot.</param>
        /// <exception cref="ArgumentOutOfRangeException">Le sabot doit contenir au moins un paquet de cartes à jouer.</exception>
        public Sabot(int paquets) {
            if (paquets < 1)
                throw new ArgumentOutOfRangeException("paquets", "Le sabot doit contenir au moins un paquet de cartes à jouer.");

            Paquet temp;
            for (int i = 1; i < paquets; i++) {
                temp = new Paquet();
                while (temp.Compte > 0)
                    paquet.Push(temp.Piocher());
            }
        }
    }
}
