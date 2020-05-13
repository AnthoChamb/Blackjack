using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blackjack {
    namespace Controls {
        public partial class ControlJoueur : UserControl {
            public ControlJoueur() {
                InitializeComponent();
            }

            public ControlJoueur(string nom, double montant, string action) {
                Nom = nom;
                Montant = montant;
                Action = action;
            }

            public string Nom { get => labNom.Text; set => labNom.Text = value; }

            public double Montant { get => double.Parse(labMontant.Text.Split(' ')[0]); set => labMontant.Text = value + " $"; }

            public string Action { get => labAction.Text; set => labAction.Text = value; }

            public bool Actif { get => BackColor == Color.DarkOliveGreen; set => BackColor = value ? Color.DarkOliveGreen : Color.Transparent; }

            public void AjouterCarte(ControlCarte carte) => flowMain.Controls.Add(carte);

            public void ViderMain() => flowMain.Controls.Clear();

        }
    }
}
