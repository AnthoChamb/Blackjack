using System;
using System.Drawing;
using System.Windows.Forms;

namespace Blackjack {
    /// <summary></summary>
    namespace Controls {
        public partial class ControlCarte : UserControl {
            public ControlCarte() {
                InitializeComponent();
            }

            public ControlCarte(string figure, Color couleur, Image image) : this() {
                Figure = figure;
                Couleur = couleur;
                BackgroundImage = image;
            }

            public string Figure { get => labHaut.Text; set => labHaut.Text = labBas.Text = value; }

            public Color Couleur { get => labHaut.ForeColor; set => labHaut.ForeColor = labBas.ForeColor = value; }
        }
    }
}
