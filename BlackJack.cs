using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blackjack {
    /// <summary>Classe mère du programme. Gère l'initialisation des différentes instances de Partie.</summary>
    public class Blackjack {
        #region Données membres 

        MenuPrincipal menuPrincipal;

        #endregion

        #region Constructeurs
        /// <summary>Créer l'instance du menu principal.</summary>
        public Blackjack() {
            this.menuPrincipal = new MenuPrincipal(this);
        }
        #endregion

        #region Méthodes publiques

        /// <summary>Créer l'instance d'une partie en s'assurant de la validité de ses paramètres via différentes closes catch respectives</summary>
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
        /// <summary>Permet de joindre une partie existante tout en vérifiant son existence et sa validité .</summary>
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
        /// <summary>Affiche l'instance du menu principal associée à l'utilisateur.</summary>
        public void Demarrer() {
            Application.Run(menuPrincipal);
        }
        #endregion
    }
}
