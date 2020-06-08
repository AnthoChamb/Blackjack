namespace Blackjack {
    partial class MenuPrincipal {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuPrincipal));
            this.btnCreer = new System.Windows.Forms.Button();
            this.btnRejoindre = new System.Windows.Forms.Button();
            this.grpHote = new System.Windows.Forms.GroupBox();
            this.numMise = new System.Windows.Forms.NumericUpDown();
            this.numMontant = new System.Windows.Forms.NumericUpDown();
            this.chbBot = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txbNomHote = new System.Windows.Forms.TextBox();
            this.numNbJoueur = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.grpClient = new System.Windows.Forms.GroupBox();
            this.txbAddresseIP = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txbNomClient = new System.Windows.Forms.TextBox();
            this.grpHote.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMise)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMontant)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNbJoueur)).BeginInit();
            this.grpClient.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCreer
            // 
            this.btnCreer.Location = new System.Drawing.Point(484, 112);
            this.btnCreer.Name = "btnCreer";
            this.btnCreer.Size = new System.Drawing.Size(123, 37);
            this.btnCreer.TabIndex = 0;
            this.btnCreer.Text = "Créer";
            this.btnCreer.UseVisualStyleBackColor = true;
            this.btnCreer.Click += new System.EventHandler(this.btnCreer_Click);
            // 
            // btnRejoindre
            // 
            this.btnRejoindre.Location = new System.Drawing.Point(484, 86);
            this.btnRejoindre.Name = "btnRejoindre";
            this.btnRejoindre.Size = new System.Drawing.Size(123, 37);
            this.btnRejoindre.TabIndex = 1;
            this.btnRejoindre.Text = "Rejoindre";
            this.btnRejoindre.UseVisualStyleBackColor = true;
            this.btnRejoindre.Click += new System.EventHandler(this.btnRejoindre_Click);
            // 
            // grpHote
            // 
            this.grpHote.Controls.Add(this.numMise);
            this.grpHote.Controls.Add(this.numMontant);
            this.grpHote.Controls.Add(this.chbBot);
            this.grpHote.Controls.Add(this.label6);
            this.grpHote.Controls.Add(this.label5);
            this.grpHote.Controls.Add(this.label4);
            this.grpHote.Controls.Add(this.label3);
            this.grpHote.Controls.Add(this.label2);
            this.grpHote.Controls.Add(this.txbNomHote);
            this.grpHote.Controls.Add(this.numNbJoueur);
            this.grpHote.Controls.Add(this.btnCreer);
            this.grpHote.Location = new System.Drawing.Point(91, 103);
            this.grpHote.Name = "grpHote";
            this.grpHote.Size = new System.Drawing.Size(613, 155);
            this.grpHote.TabIndex = 2;
            this.grpHote.TabStop = false;
            this.grpHote.Text = "Créer une partie hôte";
            // 
            // numMise
            // 
            this.numMise.DecimalPlaces = 2;
            this.numMise.Location = new System.Drawing.Point(394, 55);
            this.numMise.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numMise.Name = "numMise";
            this.numMise.Size = new System.Drawing.Size(86, 20);
            this.numMise.TabIndex = 14;
            // 
            // numMontant
            // 
            this.numMontant.DecimalPlaces = 2;
            this.numMontant.Location = new System.Drawing.Point(244, 54);
            this.numMontant.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numMontant.Name = "numMontant";
            this.numMontant.Size = new System.Drawing.Size(112, 20);
            this.numMontant.TabIndex = 13;
            // 
            // chbBot
            // 
            this.chbBot.AutoSize = true;
            this.chbBot.Location = new System.Drawing.Point(541, 56);
            this.chbBot.Name = "chbBot";
            this.chbBot.Size = new System.Drawing.Size(15, 14);
            this.chbBot.TabIndex = 11;
            this.chbBot.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(501, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Partie avec bot(s) :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(391, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Mise minimale :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(262, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Montant initial :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(126, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Nombre de joueurs :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(49, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Nom :";
            // 
            // txbNomHote
            // 
            this.txbNomHote.Location = new System.Drawing.Point(16, 55);
            this.txbNomHote.Name = "txbNomHote";
            this.txbNomHote.Size = new System.Drawing.Size(100, 20);
            this.txbNomHote.TabIndex = 2;
            // 
            // numNbJoueur
            // 
            this.numNbJoueur.Location = new System.Drawing.Point(157, 54);
            this.numNbJoueur.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numNbJoueur.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numNbJoueur.Name = "numNbJoueur";
            this.numNbJoueur.Size = new System.Drawing.Size(41, 20);
            this.numNbJoueur.TabIndex = 1;
            this.numNbJoueur.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("Tw Cen MT Condensed", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(231, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(340, 58);
            this.label1.TabIndex = 3;
            this.label1.Text = "Black Jack Connect";
            // 
            // grpClient
            // 
            this.grpClient.Controls.Add(this.txbAddresseIP);
            this.grpClient.Controls.Add(this.label8);
            this.grpClient.Controls.Add(this.label7);
            this.grpClient.Controls.Add(this.txbNomClient);
            this.grpClient.Controls.Add(this.btnRejoindre);
            this.grpClient.Location = new System.Drawing.Point(91, 284);
            this.grpClient.Name = "grpClient";
            this.grpClient.Size = new System.Drawing.Size(613, 129);
            this.grpClient.TabIndex = 4;
            this.grpClient.TabStop = false;
            this.grpClient.Text = "Rejoindre une partie existante";
            // 
            // txbAddresseIP
            // 
            this.txbAddresseIP.Location = new System.Drawing.Point(318, 56);
            this.txbAddresseIP.Name = "txbAddresseIP";
            this.txbAddresseIP.Size = new System.Drawing.Size(100, 20);
            this.txbAddresseIP.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(334, 31);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Adresse IP :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(161, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Nom :";
            // 
            // txbNomClient
            // 
            this.txbNomClient.Location = new System.Drawing.Point(128, 56);
            this.txbNomClient.Name = "txbNomClient";
            this.txbNomClient.Size = new System.Drawing.Size(100, 20);
            this.txbNomClient.TabIndex = 2;
            // 
            // MenuPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.grpClient);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grpHote);
            this.Icon = new System.Drawing.Icon("Icon.ico");
            this.MaximumSize = new System.Drawing.Size(816, 489);
            this.MinimumSize = new System.Drawing.Size(816, 489);
            this.Name = "MenuPrincipal";
            this.Text = "Form1";
            this.grpHote.ResumeLayout(false);
            this.grpHote.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMise)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMontant)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNbJoueur)).EndInit();
            this.grpClient.ResumeLayout(false);
            this.grpClient.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCreer;
        private System.Windows.Forms.Button btnRejoindre;
        private System.Windows.Forms.GroupBox grpHote;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpClient;
        private System.Windows.Forms.NumericUpDown numNbJoueur;
        private System.Windows.Forms.TextBox txbNomHote;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txbNomClient;
        private System.Windows.Forms.CheckBox chbBot;
        private System.Windows.Forms.NumericUpDown numMise;
        private System.Windows.Forms.NumericUpDown numMontant;
        private System.Windows.Forms.TextBox txbAddresseIP;
    }
}