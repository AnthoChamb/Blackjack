using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack {
    /// <summary>Classe d'un paquet de cartes à jouer.</summary>
    public class Paquet {
        protected Stack<Carte> paquet;

        /// <summary>Crée un paquet de cartes à jouer.</summary>
        public Paquet() {
            paquet = new Stack<Carte>(52);
            foreach (Atout atout in Atout.Atouts)
                for (byte figure = 1; figure <= 13; figure++)
                    paquet.Push(new Carte(figure, atout));
        }

        /// <summary>Obtient le nombre de cartes contenues dans le paquet.</summary>
        public int Compte { get => paquet.Count; }

        /// <summary>Mélange le paquet avec l'algorithme Fisher-Yates.</summary>
        public void Brasser() {
            Random alea = new Random();
            Carte[] melange = paquet.ToArray<Carte>();
            int j;
            Carte temp;

            for (int i = melange.Length - 1; i > 0; i--) {
                j = alea.Next(i);
                temp = melange[i]; 
                melange[i] = melange[j];
                melange[j] = temp;
            }

            paquet = new Stack<Carte>(melange);
        }

        /// <summary>Obtient et retire la carte au dessus du paquet.</summary>
        /// <returns>Retourne la carte au dessus du paquet.</returns>
        public Carte Piocher() => paquet.Pop();
    }
}
