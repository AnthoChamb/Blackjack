using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Blackjack.Controls;

namespace Blackjack {
    public partial class MenuPrincipal : Form {
        Sabot paquet;

        public MenuPrincipal() {
            InitializeComponent();
            paquet = new Sabot(6);
            paquet.Brasser();
            flowJoueurs.Controls.Add(new ControlJoueur() { Actif = true });
        }

        private void btnTirer_Click(object sender, EventArgs e) {
            if (flowJoueurs.Controls[0] is ControlJoueur ctrl) {
                ctrl.AjouterCarte(paquet.Piocher().Control);
                ctrl.Action = "Tirer une carte";
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            if (flowJoueurs.Controls[0] is ControlJoueur ctrl) {
                ctrl.Action = "Reste";
                ctrl.Actif = false;
            }
        }
    }
}
