using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blackjack {
    [Obsolete("Classe temporaire. Utilisé un menu principal avec un controlleur à la place.")]
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            Partie partie = new Partie("Anthony", 1, 100, 5);
            partie.Jouer();
        }
    }
}
