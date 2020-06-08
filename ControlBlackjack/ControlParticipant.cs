using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Blackjack {
    namespace Controls {
        /// <summary>Classe du contrôle utilisateur graphique d'un participant de Blackjack.</summary>
        public partial class ControlParticipant : UserControl {
            /// <summary>Crée un contrôle utilisateur graphique d'un participant de Blackjack.</summary>
            public ControlParticipant() => InitializeComponent();

            /// <summary>Crée un contrôle utilisateur graphique d'un participant de Blackjack avec le nom spécifié.</summary>
            /// <param name="nom">Nom du participant.</param>
            public ControlParticipant(string nom) : this() => Nom = nom;

            /// <summary>Obtient et définit le nom affiché par le contrôle utilisateur graphique.</summary>
            public string Nom { get => labNom.Text; set => labNom.Text = value; }

            /// <summary>Obtient et définit le montant affiché par le contrôle utilisateur graphique.</summary>
            public double Montant {
                get => double.Parse(labMontant.Text, NumberStyles.Currency);
                set {
                    if (labMontant.InvokeRequired)
                        labMontant.Invoke(new MethodInvoker(delegate { labMontant.Text = value + " $"; }));
                    else
                        labMontant.Text = value.ToString("C2");
                }
            }

            /// <summary>Obtient et définit la dernière action affiché par le contrôle utilisateur graphique.</summary>
            public string Action {
                get => labAction.Text;
                set {
                    if (labAction.InvokeRequired)
                        labAction.Invoke(new MethodInvoker(delegate { labAction.Text = value; }));
                    else
                        labAction.Text = value;
                }
            }

            /// <summary>Obtient et définit le total affiché par le contrôle utilisateur graphique.</summary>
            public int Total {
                get => int.Parse(labTotal.Text);
                set {
                    if (labTotal.InvokeRequired)
                        labTotal.Invoke(new MethodInvoker(delegate { labTotal.Text = value.ToString(); }));
                    else
                        labTotal.Text = value.ToString();
                }
            }

            /// <summary>Obtient et définit si le contrôle utilisateur graphique est affiché comme étant actif.</summary>
            public bool Actif { get => BackColor == Color.DarkOliveGreen; set => BackColor = value ? Color.DarkOliveGreen : Color.Transparent; }

            /// <summary>Obtient et définit si le contrôle utilisateur graphique est affiché comme étant un croupier.</summary>
            public bool Croupier {
                get => labMontant.Visible;
                set {
                    labMontant.Visible = value;
                    labAction.Location = value ? new Point(3, 115) : new Point(3, 128);
                }
            }

            /// <summary>Ajoute la carte spécifié à la main du contrôle utilisateur graphique.</summary>
            /// <param name="carte">Carte à ajouter.</param>
            public void AjouterCarte(ControlCarte carte) {
                if (flowMain.InvokeRequired)
                    flowMain.Invoke(new MethodInvoker(delegate { flowMain.Controls.Add(carte); }));
                else
                    flowMain.Controls.Add(carte);
            }

            /// <summary>Vide la main du contrôle utilisateur graphique.</summary>
            public void Defausser() {
                if (flowMain.InvokeRequired)
                    flowMain.Invoke(new MethodInvoker(delegate { flowMain.Controls.Clear(); }));
                else
                    flowMain.Controls.Clear();
            }
        }
    }
}
