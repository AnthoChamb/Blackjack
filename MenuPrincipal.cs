using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blackjack {

    public partial class MenuPrincipal : Form {
        private readonly Blackjack blackjack;
        public MenuPrincipal(Blackjack blackjack) {
            InitializeComponent();
            this.blackjack = blackjack;
        }

        private void btnCreer_Click(object sender, EventArgs e) {
            blackjack.CreerPartie();
        }

        private void btnRejoindre_Click(object sender, EventArgs e) {
            blackjack.JoindrePartie();
        }

        public static void AdresseIPFormatInvalide() => MessageBox.Show("Le format de l'adresse IP est invalide. " +
            "Veuillez revoir la syntaxe appropriée pour rejoindre une partie.", "Format invalide", MessageBoxButtons.OK,
            MessageBoxIcon.Error);
        public static void AdresseIPHoteIntrouvable() => MessageBox.Show("La partie que vous tenter de rejoindre ne semble pas avoir " +
           "d'hôte. Veuiller revoir l'adresse IP pour rejoindre une partie existante.", "Hôte introuvable", MessageBoxButtons.OK,
           MessageBoxIcon.Error);
        public static void HebergementInvalide() => MessageBox.Show("Vous ne pouvez héberger plus d'une partie de Black Jack Connect. " +
            "Veuillez amorcer votre partie en cours pour en débuter une nouvelle.", "Hébergement invalide", MessageBoxButtons.OK,
            MessageBoxIcon.Error);

        public static void MiseInvalide() => MessageBox.Show(" Vous ne pouvez suggérer une mise inférieure au montant initial."
           , "Mise invalide", MessageBoxButtons.OK,
            MessageBoxIcon.Error);

        public void AfficherErreur(string err) => MessageBox.Show(err, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public string EntreeNomHote { get => txbNomHote.Text; }

        public string EntreeNomClient { get => txbNomClient.Text; }

        public double EntreeMin { get => (Double)numMise.Value; }

        public double EntreeInitial { get => (Double)numMontant.Value; }

        public IPAddress EntreeIPAdresse { get => IPAddress.Parse(txbAddresseIP.Text); }

        public int EntreeNBjoueurs { get => (int)numNbJoueur.Value ; }

        public bool EntreeBot { get => chbBot.Checked; }

    }
}
