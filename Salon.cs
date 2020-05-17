using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Blackjack.Controls;

namespace Blackjack {
    /// <summary>Classe du salon de jeu d'une partie.</summary>
    public partial class Salon : Form {
        private readonly Partie partie;

        /// <summary>Crée un salon de jeu.</summary>
        /// <param name="partie">Partie associée à ce salon de jeu.</param>
        public Salon(Partie partie) {
            InitializeComponent();
            this.partie = partie;
        }

        /// <summary>Gestionnaire d'évènement d'un clic sur le boutton «Tirer».</summary>
        /// <param name="sender">Objet à l'origine de l'évènement.</param>
        /// <param name="e">Paramètres de l'évènement.</param>
        private void btnTirer_Click(object sender, EventArgs e) {
            partie.Tirer();
        }

        /// <summary>Gestionnaire d'évènement d'un clic sur le boutton «Rester».</summary>
        /// <param name="sender">Objet à l'origine de l'évènement.</param>
        /// <param name="e">Paramètres de l'évènement.</param>
        private void btnRester_Click(object sender, EventArgs e) {
            partie.Rester();
        }

        /// <summary>Ajoute un joueur au salon de jeu.</summary>
        /// <param name="joueur">Joueur à ajouter.</param>
        public void AjouterJoueur(Joueur joueur) => flowJoueurs.Controls.Add(joueur.Control);

        /// <summary>Ajoute le croupier au salon de jeu.</summary>
        /// <param name="joueur">Joueur représentant le croupier.</param>
        public void AjouterCroupier(Joueur joueur) {
            ControlJoueur ctrl = joueur.Control;
            ctrl.Location = new Point(12, 12);
            Controls.Add(ctrl);
        }

        /// <summary>Affiche la fenêtre de mise à l'écran.</summary>
        /// <param name="min">Mise minimale.</param>
        public void AfficherMise(double min) {
            numMise.Minimum = (decimal) min;
            pannelMise.Visible = true;
        }

        /// <summary>Gestionnaire d'évènement d'un clic sur le boutton «Miser».</summary>
        /// <param name="sender">Objet à l'origine de l'évènement.</param>
        /// <param name="e">Paramètres de l'évènement.</param>
        private void btnMiser_Click(object sender, EventArgs e) {
            partie.Miser((double) numMise.Value);
            pannelMise.Visible = false;
        }

        /// <summary>Bloque la section des actions de l'écran.</summary>
        public void BloquerActions() => pannelActions.Enabled = false;

        /// <summary>Débloque la section des actions à l'écran.</summary>
        public void DebloquerActions() => pannelActions.Enabled = true;
    }
}
