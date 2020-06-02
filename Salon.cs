using Blackjack.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Blackjack {
    /// <summary>Form du salon de jeu d'une partie.</summary>
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
        public void AjouterJoueur(Joueur joueur) {
            if (flowJoueurs.InvokeRequired)
                flowJoueurs.Invoke(new MethodInvoker(delegate { flowJoueurs.Controls.Add(joueur.Control); }));
            else
                flowJoueurs.Controls.Add(joueur.Control);
        }

        /// <summary>Retire le joueur spécifié du salon de jeu.</summary>
        /// <param name="joueur">Joueur à retirer.</param>
        public void RetirerJoueur(Joueur joueur) {
            if (flowJoueurs.InvokeRequired)
                flowJoueurs.Invoke(new MethodInvoker(delegate { flowJoueurs.Controls.Remove(joueur.Control); }));
            else
                flowJoueurs.Controls.Remove(joueur.Control);
        }

        /// <summary>Ajoute le croupier au salon de jeu.</summary>
        /// <param name="croupier">Croupier à ajouter.</param>
        public void AjouterCroupier(Croupier croupier) {
            ControlParticipant ctrl = croupier.Control;
            ctrl.Location = new Point(12, 12);

            if (InvokeRequired)
                Invoke(new MethodInvoker(delegate { Controls.Add(ctrl); }));
            else
                Controls.Add(ctrl);
        }

        /// <summary>Affiche la fenêtre de mise à l'écran.</summary>
        /// <param name="min">Mise minimale.</param>
        /// <param name="max">Mise maximale.</param>
        public void AfficherMise(double min, double max) {
            numMise.Minimum = (decimal)min;
            numMise.Maximum = (decimal)max;

            if (pannelMise.InvokeRequired)
                pannelMise.Invoke(new MethodInvoker(delegate { pannelMise.Visible = true; }));
            else
                pannelMise.Visible = true;
        }

        /// <summary>Gestionnaire d'évènement d'un clic sur le boutton «Miser».</summary>
        /// <param name="sender">Objet à l'origine de l'évènement.</param>
        /// <param name="e">Paramètres de l'évènement.</param>
        private void btnMiser_Click(object sender, EventArgs e) {
            partie.Miser((double)numMise.Value);
            pannelMise.Visible = false;
        }

        /// <summary>Bloque la section des actions de l'écran.</summary>
        public void BloquerActions() => pannelActions.Enabled = false;

        /// <summary>Débloque la section des actions à l'écran.</summary>
        public void DebloquerActions() {
            if (pannelActions.InvokeRequired)
                pannelActions.Invoke(new MethodInvoker(delegate { pannelActions.Enabled = true; }));
            else
                pannelActions.Enabled = true;
        }
    }
}
