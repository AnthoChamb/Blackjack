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
    /// <summary>Classe de la gestion de l'affichage graphique du menu principal.</summary>
    public partial class MenuPrincipal : Form {
        #region Données membres 
        private readonly Blackjack blackjack;

        #endregion

        #region Constructeurs

        /// <summary>Lie l'instance du menu principal au Black Jack en cours.</summary>
        /// <param name="blackjack">Instance mère du menu principal.</param>
        public MenuPrincipal(Blackjack blackjack) {
            InitializeComponent();
            this.blackjack = blackjack;
        }

        #endregion

        #region Méthodes privées

        /// <summary>Action du bouton Créer qui créer la partie selon les entrées utilisateur.</summary>
        /// <param name="sender">Objet à l'origine de l'évènement.</param>
        /// <param name="e">Paramètres de l'évènement.</param>
        private void btnCreer_Click(object sender, EventArgs e) {
            blackjack.CreerPartie();
        }

        /// <summary>Action du bouton Créer qui permet de rejoindre une partie existante. </summary>
        /// <param name="sender">Objet à l'origine de l'évènement.</param>
        /// <param name="e">Paramètres de l'évènement.</param>
        private void btnRejoindre_Click(object sender, EventArgs e) {
            blackjack.JoindrePartie();
        }

        #endregion

        #region Méthodes publiques 

        /// <summary>Affiche à l'utlisateur un message d'erreur mentionnant que le format de l'adress IP est invalide.</summary>
        public static void AdresseIPFormatInvalide() => MessageBox.Show("Le format de l'adresse IP est invalide. " +
            "Veuillez revoir la syntaxe appropriée pour rejoindre une partie.", "Format invalide", MessageBoxButtons.OK,
            MessageBoxIcon.Error);

        /// <summary>Affiche à l'utlisateur un message d'erreur mentionnant que l'hôte est manquant pour rejoindre la partie.</summary>
        public static void AdresseIPHoteIntrouvable() => MessageBox.Show("La partie que vous tenter de rejoindre ne semble pas avoir " +
           "d'hôte. Veuiller revoir l'adresse IP pour rejoindre une partie existante.", "Hôte introuvable", MessageBoxButtons.OK,
           MessageBoxIcon.Error);

        /// <summary>Affiche à l'utlisateur un message d'erreur rappelant à l'utilisateur qu'il ne peut héberger deux parties simultanément.</summary>
        public static void HebergementInvalide() => MessageBox.Show("Vous ne pouvez héberger plus d'une partie de Black Jack Connect. " +
            "Veuillez amorcer votre partie en cours pour en débuter une nouvelle.", "Hébergement invalide", MessageBoxButtons.OK,
            MessageBoxIcon.Error);

        /// <summary>Affiche à l'utlisateur un message d'erreur indiqué en paramètre.</summary>
        public void AfficherErreur(string err) => MessageBox.Show(err, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

        #endregion

        #region Propriétés 

        /// <summary>Retourne la valeur de la boîte associée au nom de l'hôte.</summary>
        public string EntreeNomHote { get => txbNomHote.Text; }

        /// <summary>Retourne la valeur de la boîte associée au nom du client.</summary>
        public string EntreeNomClient { get => txbNomClient.Text; }

        /// <summary>Retourne la valeur du numérique haut bas associé à la valeur de la mise minimale.</summary>
        public double EntreeMin { get => (Double)numMise.Value; }

        /// <summary>Retourne la valeur du numérique haut bas associé à la valeur du montant de départ par joueur.</summary>
        public double EntreeInitial { get => (Double)numMontant.Value; }

        /// <summary>Retourne la valeur de la boîte associée à l'adresse IP de l'hôte à rejoindre.</summary>
        public IPAddress EntreeIPAdresse { get => IPAddress.Parse(txbAddresseIP.Text); }

        /// <summary>Retourne la valeur du numérique haut bas associé à la valeur du nombre de joueurs participants.</summary>
        public int EntreeNBjoueurs { get => (int)numNbJoueur.Value ; }

        /// <summary>Retourne la valeur booléenne de la boîte à cocher associée au choix de l'utilisateur concernant la participation de joueurs virtuels.</summary>
        public bool EntreeBot { get => chbBot.Checked; }

        #endregion 
    }
}
