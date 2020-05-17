
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Blackjack {
    /// <summary>Structure d'un atout de carte à jouer.</summary>
    public readonly struct Atout {
        private readonly Color couleur;
        private readonly Image image;

        /// <summary>Crée un atout.</summary>
        /// <param name="couleur">Couleur de l'atout.</param>
        /// <param name="image">Image de l'atout.</param>
        private Atout(Color couleur, Image image) {
            this.couleur = couleur;
            this.image = image;
        }

        /// <summary>Obtient un tableau d'atouts contenant les atouts définies par le système.</summary>
        public static Atout[] Atouts { get => new Atout[] { Carreaux, Coeurs, Piques, Trefles }; }

        /// <summary>Obtient l'atout définie par le système pour les carreaux.</summary>
        public static Atout Carreaux { get => new Atout(Color.Red, Bitmap.FromFile("Properties/carreaux.png")); }

        /// <summary>Obtient l'atout définie par le système pour les coeurs.</summary>
        public static Atout Coeurs { get => new Atout(Color.Red, Bitmap.FromFile("Properties/coeurs.png")); }

        /// <summary>Obtient l'atout définie par le système pour les piques.</summary>
        public static Atout Piques { get => new Atout(Color.Black, Bitmap.FromFile("Properties/piques.png")); }

        /// <summary>Obtient l'atout définie par le système pour les trèfles.</summary>
        public static Atout Trefles { get => new Atout(Color.Black, Bitmap.FromFile("Properties/trefles.png")); }

        /// <summary>Obtient la couleur associée à cet atout.</summary>
        public Color Couleur { get => couleur; }

        /// <summary>Obtient l'image associée à cet atout.</summary>
        public Image Image { get => image; }
    }
}
