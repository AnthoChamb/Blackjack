using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blackjack {
    internal class Reseau {
        Partie partie;
        List<NetworkStream> reseaux;
        List<StreamReader> lectures;
        List<StreamWriter> ecritures;

        private static ManualResetEvent attente = new ManualResetEvent(false);
        private static BinaryFormatter formatteur = new BinaryFormatter();

        internal Reseau(Partie partie) {
            this.partie = partie;
            reseaux = new List<NetworkStream>(partie.Nombre - 1);
            lectures = new List<StreamReader>(partie.Nombre - 1);
            ecritures = new List<StreamWriter>(partie.Nombre - 1);

            TcpListener tcpListener = new TcpListener(IPAddress.Any, 999);
            tcpListener.Start();

            tcpListener.BeginAcceptSocket(new AsyncCallback(NouvelleConnexion), tcpListener);

            attente.WaitOne();
        }

        internal Reseau(Partie partie, IPAddress ip) {
            this.partie = partie;
            reseaux = new List<NetworkStream>(1);
            lectures = new List<StreamReader>(1);
            ecritures = new List<StreamWriter>(1);

            TcpClient hote;
            hote = new TcpClient(ip.ToString(), 999);

            NetworkStream fluxReseau = hote.GetStream();
            reseaux.Add(fluxReseau);
            lectures.Add(new StreamReader(fluxReseau));
            ecritures.Add(new StreamWriter(fluxReseau));
        }

        internal Partie ObtenirPartie() => (Partie)formatteur.Deserialize(reseaux[0]);

        internal void EnvoyerJoueur(Joueur joueur) => formatteur.Serialize(reseaux[0], joueur);

        internal Joueur ObtenirJoueur(int index) => (Joueur)formatteur.Deserialize(reseaux[index]);

        internal void EnvoyerMise(double mise) {
            foreach (StreamWriter ecriture in ecritures) {
                ecriture.WriteLine(mise);
                ecriture.Flush();
            }
        }

        internal double ObtenirMise() => double.Parse(lectures[0].ReadLine());

        internal void EnvoyerCoup(bool tirer) {
            foreach (StreamWriter ecriture in ecritures) {
                ecriture.WriteLine(tirer);
                ecriture.Flush();
            }
        }

        internal bool ObtenirCoup() => lectures[0].ReadLine() == true.ToString();

        private void NouvelleConnexion(IAsyncResult async) {
            TcpListener tcpListener = (TcpListener) async.AsyncState;

            Socket client = tcpListener.EndAcceptSocket(async);

            if (client.Connected) {
                NetworkStream fluxReseau = new NetworkStream(client);
                reseaux.Add(fluxReseau);
                lectures.Add(new StreamReader(fluxReseau));
                ecritures.Add(new StreamWriter(fluxReseau));

                formatteur.Serialize(fluxReseau, partie);

                attente.Set();
            }
        }
    }
}
