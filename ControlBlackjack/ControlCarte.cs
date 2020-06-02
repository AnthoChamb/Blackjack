using System.Drawing;
using System.Windows.Forms;

namespace Blackjack {
    namespace Controls {
        /// <summary>Classe du contrôle utilisateur graphique d'une carte à jouer.</summary>
        public partial class ControlCarte : UserControl {
            /// <summary>Crée un contrôle utilisateur graphique d'une carte à joueur.</summary>
            public ControlCarte() {
                InitializeComponent();
            }

            /// <summary>Crée un contrôle utilisateur graphique d'une carte à jouer avec les informations spécifiés.</summary>
            /// <param name="figure">Figure de la carte.</param>
            /// <param name="couleur">Couleure de la carte.</param>
            /// <param name="image">Image de l'atout de la carte.</param>
            public ControlCarte(string figure, Color couleur, Image image) : this() {
                Figure = figure;
                Couleur = couleur;
                BackgroundImage = image;
            }

            /// <summary>Obtient et définit la figure de la carte.</summary>
            public string Figure { get => labHaut.Text; set => labHaut.Text = labBas.Text = value; }

            /// <summary>Obtient et définit la couleure de la carte.</summary>
            public Color Couleur { get => labHaut.ForeColor; set => labHaut.ForeColor = labBas.ForeColor = value; }

            /// <summary>Obtient et définit l'image de l'atout de la carte.</summary>
            public Image Image { get => BackgroundImage; set => BackgroundImage = value; }
        }
    }
}
