using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blackjack {
    public class Blackjack {
        MenuPrincipal menuPrincipal;
        public Blackjack() {
            this.menuPrincipal = new MenuPrincipal(this);
        }

        public void CreerPartie() {
            try {
                Partie partie = new Partie(menuPrincipal.EntreeNomHote, menuPrincipal.EntreeNBjoueurs, menuPrincipal.EntreeInitial,
                    menuPrincipal.EntreeMin, menuPrincipal.EntreeBot);
                partie.Afficher();

            } catch (FormatException) {
                MenuPrincipal.AdresseIPFormatInvalide();

            } catch (SocketException) {
                MenuPrincipal.HebergementInvalide();

            } catch (Exception ex) {
                menuPrincipal.AfficherErreur(ex.Message);

            }
        }

        public void JoindrePartie() {
            try {
                Partie partie = new Partie(menuPrincipal.EntreeNomClient, menuPrincipal.EntreeIPAdresse);
                partie.Afficher();

            } catch (FormatException) {
                MenuPrincipal.AdresseIPFormatInvalide();

            } catch (SocketException) {
                MenuPrincipal.AdresseIPHoteIntrouvable();

            } catch (Exception ex) {
                menuPrincipal.AfficherErreur(ex.Message);

            }
        }

        public void Demarrer() {
            Application.Run(menuPrincipal);
        }
    }
}
