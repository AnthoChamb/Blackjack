﻿using Blackjack.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack {
    /// <summary>Classe d'un joueur de Blackjack.</summary>
    [Serializable]
    public class Joueur : Participant {
        private double montant;
        private double mise;

        /// <summary>Crée un joueur.</summary>
        /// <param name="nom">Nom du joueur.</param>
        /// <param name="montant">Montant initial du joueur.</param>
        public Joueur(string nom, double montant) : base(nom) {
            this.montant = montant;
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

        protected override ControlJoueur GenererControl() => new ControlJoueur(nom) { Montant = montant };
    }
}