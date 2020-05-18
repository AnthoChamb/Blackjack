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

            public ControlJoueur(string nom) : this() {
                Nom = nom;
            }

            public string Nom { get => labNom.Text; set => labNom.Text = value; }

            public double Montant {
                get => double.Parse(labMontant.Text.Split(' ')[0]);
                set {
                    if (labMontant.InvokeRequired)
                        labMontant.Invoke(new MethodInvoker(delegate { labMontant.Text = value + " $"; }));
                    else
                        labMontant.Text = value + " $";
                }
            }

            public string Action { 
                get => labAction.Text;
                set {
                    if (labAction.InvokeRequired)
                        labAction.Invoke(new MethodInvoker(delegate { labAction.Text = value; }));
                    else
                        labAction.Text = value;
                }
            }

            public bool Actif { get => BackColor == Color.DarkOliveGreen; set => BackColor = value ? Color.DarkOliveGreen : Color.Transparent; }

            public bool Croupier { 
                get => labMontant.Visible;
                set {
                    labMontant.Visible = value;
                    labAction.Location = value ? new Point(3, 115) : new Point(3, 128);
                }
            }

            public void AjouterCarte(ControlCarte carte) {
                if (flowMain.InvokeRequired)
                    flowMain.Invoke(new MethodInvoker(delegate { flowMain.Controls.Add(carte); }));
                else
                    flowMain.Controls.Add(carte);
            }

            public void Defausser() {
                if (flowMain.InvokeRequired)
                    flowMain.Invoke(new MethodInvoker(delegate { flowMain.Controls.Clear(); }));
                else
                    flowMain.Controls.Clear();
            }
        }
    }
}
