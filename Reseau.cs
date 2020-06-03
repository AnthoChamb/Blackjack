using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace Blackjack {
    [Serializable]
    internal class Reseau : IDisposable {
        private readonly List<NetworkStream> reseaux;
        private readonly List<StreamReader> lectures;
        private readonly List<StreamWriter> ecritures;

        private readonly TcpListener tcpListener;
        private static readonly BinaryFormatter formatteur = new BinaryFormatter();

        /// <summary>Crée une classe réseau pour un hôte.</summary>
        internal Reseau() {
            reseaux = new List<NetworkStream>(1);
            lectures = new List<StreamReader>(1);
            ecritures = new List<StreamWriter>(1);

            tcpListener = new TcpListener(IPAddress.Any, 999);
            tcpListener.Start();
        }

        /// <summary>Crée une classe réseau pour un client.</summary>
        /// <param name="ip">Adresse IP de l'hôte.</param>
        /// <exception cref="ArgumentNullException">L'adresse IP de l'hôte ne peut pas être la valeur null.</exception>
        internal Reseau(IPAddress ip) {
            reseaux = new List<NetworkStream>(1);
            lectures = new List<StreamReader>(1);
            ecritures = new List<StreamWriter>(1);

            TcpClient hote;
            hote = new TcpClient(ip.ToString() ?? throw new ArgumentNullException("ip", "L'adresse IP de l'hôte ne peut pas être la valeur null."), 999);

            NetworkStream fluxReseau = hote.GetStream();
            fluxReseau.ReadTimeout = fluxReseau.WriteTimeout = 20000; // Fixe la limite du délai de réponses

            reseaux.Add(fluxReseau);
            lectures.Add(new StreamReader(fluxReseau));
            ecritures.Add(new StreamWriter(fluxReseau));
        }

        /// <summary>Obtient une connexion distante et envoie les informations de la partie à celle-ci.</summary>
        /// <param name="partie">Partie à envoyer.</param>
        /// <exception cref="SocketException">Exception levée lorsqu'aucune connexion est obtenue.</exception>
        internal void ObtenirConnexion(Partie partie) {
            Socket client = tcpListener.AcceptSocket();

            if (client.Connected) {
                NetworkStream fluxReseau = new NetworkStream(client);
                fluxReseau.ReadTimeout = fluxReseau.WriteTimeout = 20000; // Fixe la limite du délai de réponses

                reseaux.Add(fluxReseau);
                lectures.Add(new StreamReader(fluxReseau));
                ecritures.Add(new StreamWriter(fluxReseau));

                formatteur.Serialize(fluxReseau, partie);
            }
        }

        /// <summary>Obtient les informations de la partie de l'hôte.</summary>
        /// <returns>Retourne la partie obtenue.</returns>
        internal Partie ObtenirPartie() => (Partie)formatteur.Deserialize(reseaux[0]);

        /// <summary>Envoie le joueur spécifié à l'hôte.</summary>
        /// <param name="joueur">Joueur à envoyer.</param>
        internal void EnvoyerJoueur(Joueur joueur) => formatteur.Serialize(reseaux[0], joueur);

        /// <summary>Envoie le joueur spécifié à un client.</summary>
        /// <param name="joueur">Joueur à envoyer.</param>
        /// <param name="index">Indice réseau du client.</param>
        private void EnvoyerJoueur(Joueur joueur, int index) => formatteur.Serialize(reseaux[index], joueur);

        /// <summary>Obtient un joueur distant de l'hôte.</summary>
        /// <returns>Retourne le joueur obtenu.</returns>
        internal Joueur ObtenirJoueur() => (Joueur)formatteur.Deserialize(reseaux[0]);

        /// <summary>Obtient le joueur d'un client et distribue celui-ci aux autres.</summary>
        /// <param name="index">Indice réseau du client.</param>
        /// <returns>Retourne le joueur obtenu.</returns>
        internal Joueur ObtenirJoueur(int index) {
            Joueur joueur = (Joueur)formatteur.Deserialize(reseaux[index]);

            for (int i = 0; i < reseaux.Count; i++)
                if (i != index)
                    EnvoyerJoueur(joueur, i);

            return joueur;
        }

        /// <summary>Envoie la mise spécifié.</summary>
        /// <param name="mise">Mise à envoyer.</param>
        internal void EnvoyerMise(double mise) {
            foreach (StreamWriter ecriture in ecritures) {
                ecriture.WriteLine(mise);
                ecriture.Flush();
            }
        }

        /// <summary>Envoie la mise spécifié à un client.</summary>
        /// <param name="mise">Mise à envoyer.</param>
        /// <param name="index">Indice réseau du client.</param>
        private void EnvoyerMise(double mise, int index) {
            ecritures[index].WriteLine(mise);
            ecritures[index].Flush();
        }

        /// <summary>Obtient la mise de l'hôte.</summary>
        /// <returns>Retourne la mise obtenue.</returns>
        internal double ObtenirMise() => double.Parse(lectures[0].ReadLine());

        /// <summary>Obtient la mise d'un client et distribue celle-ci aux autres.</summary>
        /// <param name="index">Indice réseau du client.</param>
        /// <returns>Retourne la mise obtenue.</returns>
        internal double ObtenirMise(int index) {
            double mise = double.Parse(lectures[index].ReadLine());

            for (int i = 0; i < reseaux.Count; i++)
                if (i != index)
                    EnvoyerMise(mise, i);

            return mise;
        }

        /// <summary>Envoie si le jouer doit tirer (HIT) ou rester (STAND).</summary>
        /// <param name="tirer">Évalue si le joueur doit tirer (HIT) ou rester (STAND).</param>
        internal void EnvoyerCoup(bool tirer) {
            foreach (StreamWriter ecriture in ecritures) {
                ecriture.WriteLine(tirer);
                ecriture.Flush();
            }
        }

        /// <summary>Envoie si le jouer doit tirer (HIT) ou rester (STAND) à un client.</summary>
        /// <param name="tirer">Évalue si le joueur doit tirer (HIT) ou rester (STAND).</param>
        /// <param name="index">Indice réseau du client.</param>
        private void EnvoyerCoup(bool tirer, int index) {
            ecritures[index].WriteLine(tirer);
            ecritures[index].Flush();
        }

        /// <summary>Obtient si le jouer doit tirer (HIT) ou rester (STAND) de l'hôte.</summary>
        /// <returns>Retourne si le jouer doit tirer (HIT) ou rester (STAND).</returns>
        internal bool ObtenirCoup() => lectures[0].ReadLine() == true.ToString();

        /// <summary>Obtient si le jouer doit tirer (HIT) ou rester (STAND) d'un client et distribue cette réponse aux autres.</summary>
        /// <param name="index">Indice réseau du client.</param>
        /// <returns>Retourne si le jouer doit tirer (HIT) ou rester (STAND).</returns>
        /// <exception cref="InvalidDataException">Le flux de données réseau doit obtenir un coup valide.</exception>
        internal bool ObtenirCoup(int index) {
            string rep = lectures[index].ReadLine();
            if (rep == null)
                throw new InvalidDataException();

            bool tirer = rep == true.ToString();

            for (int i = 0; i < reseaux.Count; i++)
                if (i != index)
                    EnvoyerCoup(tirer, i);

            return tirer;
        }

        /// <summary>Envoie le sabot aux clients.</summary>
        /// <param name="sabot">Sabot à envoyer.</param>
        internal void EnvoyerSabot(Sabot sabot) {
            foreach (NetworkStream flux in reseaux)
                formatteur.Serialize(flux, sabot);
        }

        /// <summary>Obtient le sabot de l'hôte.</summary>
        /// <returns>Retourne le sabot obtenu.</returns>
        internal Sabot ObtenirSabot() => (Sabot)formatteur.Deserialize(reseaux[0]);

        /// <summary>Retire un client de la communication réseau.</summary>
        /// <param name="index">Indice réseau du client.</param>
        internal void Retirer(int index) {
            reseaux.RemoveAt(index);
            lectures.RemoveAt(index);
            ecritures.RemoveAt(index);
        }

        public void Dispose() {
            foreach (NetworkStream reseau in reseaux)
                reseau.Dispose();
            foreach (StreamReader lecture in lectures)
                lecture.Dispose();
            foreach (StreamWriter ecriture in ecritures)
                ecriture.Dispose();

            if (tcpListener != null)
                tcpListener.Stop();
        }
    }
}
