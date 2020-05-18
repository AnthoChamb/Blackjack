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
        private readonly List<NetworkStream> reseaux;
        private readonly List<StreamReader> lectures;
        private readonly List<StreamWriter> ecritures;

        private TcpListener tcpListener;
        private static BinaryFormatter formatteur = new BinaryFormatter();

        internal Reseau() {
            reseaux = new List<NetworkStream>(1);
            lectures = new List<StreamReader>(1);
            ecritures = new List<StreamWriter>(1);

            tcpListener = new TcpListener(IPAddress.Any, 999);
            tcpListener.Start();
        }

        internal Reseau(IPAddress ip) {
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

        internal void ObtenirConnexion(Partie partie) {
            Socket client = tcpListener.AcceptSocket();

            if (client.Connected) {
                NetworkStream fluxReseau = new NetworkStream(client);
                reseaux.Add(fluxReseau);
                lectures.Add(new StreamReader(fluxReseau));
                ecritures.Add(new StreamWriter(fluxReseau));

                formatteur.Serialize(fluxReseau, partie);
            }
        } 

        internal Partie ObtenirPartie() => (Partie)formatteur.Deserialize(reseaux[0]);

        internal void EnvoyerJoueur(Joueur joueur) => formatteur.Serialize(reseaux[0], joueur);

        private void EnvoyerJoueur(Joueur joueur, int index) => formatteur.Serialize(reseaux[index], joueur);

        internal Joueur ObtenirJoueur() => (Joueur)formatteur.Deserialize(reseaux[0]);

        internal Joueur ObtenirJoueur(int index) {
            Joueur joueur = (Joueur)formatteur.Deserialize(reseaux[index]);

            for (int i = 0; i < reseaux.Count; i++)
                if (i != index)
                    EnvoyerJoueur(joueur, i);

            return joueur;
        }

        internal void EnvoyerMise(double mise) {
            foreach (StreamWriter ecriture in ecritures) {
                ecriture.WriteLine(mise);
                ecriture.Flush();
            }
        }

        private void EnvoyerMise(double mise, int index) {
            ecritures[index].WriteLine(mise);
            ecritures[index].Flush();
        }

        internal double ObtenirMise() => double.Parse(lectures[0].ReadLine());

        internal double ObtenirMise(int index) {
            double mise = double.Parse(lectures[index].ReadLine());

            for (int i = 0; i < reseaux.Count; i++)
                if (i != index)
                    EnvoyerMise(mise, i);

            return mise;
        }

        internal void EnvoyerCoup(bool tirer) {
            foreach (StreamWriter ecriture in ecritures) {
                ecriture.WriteLine(tirer);
                ecriture.Flush();
            }
        }

        private void EnvoyerCoup(bool tirer, int index) {
            ecritures[index].WriteLine(tirer);
            ecritures[index].Flush();
        }

        internal bool ObtenirCoup() => lectures[0].ReadLine() == true.ToString();

        internal bool ObtenirCoup(int index) {
            bool tirer = lectures[index].ReadLine() == true.ToString();

            for (int i = 0; i < reseaux.Count; i++)
                if (i != index)
                    EnvoyerCoup(tirer, i);

            return tirer;
        }
    }
}
