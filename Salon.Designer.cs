﻿namespace Blackjack {
    partial class Salon {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent() {
            this.flowJoueurs = new System.Windows.Forms.FlowLayoutPanel();
            this.pannelActions = new System.Windows.Forms.Panel();
            this.btnRester = new System.Windows.Forms.Button();
            this.btnTirer = new System.Windows.Forms.Button();
            this.numMise = new System.Windows.Forms.NumericUpDown();
            this.btnMiser = new System.Windows.Forms.Button();
            this.pannelMise = new System.Windows.Forms.Panel();
            this.labMise = new System.Windows.Forms.Label();
            this.pannelActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMise)).BeginInit();
            this.pannelMise.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowJoueurs
            // 
            this.flowJoueurs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowJoueurs.BackColor = System.Drawing.Color.Transparent;
            this.flowJoueurs.Location = new System.Drawing.Point(12, 241);
            this.flowJoueurs.Name = "flowJoueurs";
            this.flowJoueurs.Size = new System.Drawing.Size(1235, 156);
            this.flowJoueurs.TabIndex = 0;
            // 
            // pannelActions
            // 
            this.pannelActions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pannelActions.Controls.Add(this.btnRester);
            this.pannelActions.Controls.Add(this.btnTirer);
            this.pannelActions.Enabled = false;
            this.pannelActions.Location = new System.Drawing.Point(12, 403);
            this.pannelActions.Name = "pannelActions";
            this.pannelActions.Size = new System.Drawing.Size(170, 35);
            this.pannelActions.TabIndex = 2;
            // 
            // btnRester
            // 
            this.btnRester.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRester.Location = new System.Drawing.Point(84, 6);
            this.btnRester.Name = "btnRester";
            this.btnRester.Size = new System.Drawing.Size(75, 23);
            this.btnRester.TabIndex = 1;
            this.btnRester.Text = "Rester";
            this.btnRester.UseVisualStyleBackColor = true;
            this.btnRester.Click += new System.EventHandler(this.btnRester_Click);
            // 
            // btnTirer
            // 
            this.btnTirer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTirer.Location = new System.Drawing.Point(3, 6);
            this.btnTirer.Name = "btnTirer";
            this.btnTirer.Size = new System.Drawing.Size(75, 23);
            this.btnTirer.TabIndex = 0;
            this.btnTirer.Text = "Tirer";
            this.btnTirer.UseVisualStyleBackColor = true;
            this.btnTirer.Click += new System.EventHandler(this.btnTirer_Click);
            // 
            // numMise
            // 
            this.numMise.DecimalPlaces = 2;
            this.numMise.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numMise.Location = new System.Drawing.Point(16, 30);
            this.numMise.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numMise.Name = "numMise";
            this.numMise.Size = new System.Drawing.Size(120, 20);
            this.numMise.TabIndex = 3;
            // 
            // btnMiser
            // 
            this.btnMiser.Location = new System.Drawing.Point(16, 56);
            this.btnMiser.Name = "btnMiser";
            this.btnMiser.Size = new System.Drawing.Size(120, 23);
            this.btnMiser.TabIndex = 4;
            this.btnMiser.Text = "Confirmer";
            this.btnMiser.UseVisualStyleBackColor = true;
            this.btnMiser.Click += new System.EventHandler(this.btnMiser_Click);
            // 
            // pannelMise
            // 
            this.pannelMise.Controls.Add(this.labMise);
            this.pannelMise.Controls.Add(this.numMise);
            this.pannelMise.Controls.Add(this.btnMiser);
            this.pannelMise.Location = new System.Drawing.Point(422, 121);
            this.pannelMise.Name = "pannelMise";
            this.pannelMise.Size = new System.Drawing.Size(147, 100);
            this.pannelMise.TabIndex = 5;
            this.pannelMise.Visible = false;
            // 
            // labMise
            // 
            this.labMise.AutoSize = true;
            this.labMise.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labMise.Location = new System.Drawing.Point(13, 11);
            this.labMise.Name = "labMise";
            this.labMise.Size = new System.Drawing.Size(82, 13);
            this.labMise.TabIndex = 5;
            this.labMise.Text = "Placer une mise";
            // 
            // Salon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGreen;
            this.ClientSize = new System.Drawing.Size(1264, 450);
            this.Controls.Add(this.pannelMise);
            this.Controls.Add(this.pannelActions);
            this.Controls.Add(this.flowJoueurs);
            this.Name = "Salon";
            this.Text = "Salon de jeu";
            this.pannelActions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numMise)).EndInit();
            this.pannelMise.ResumeLayout(false);
            this.pannelMise.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flowJoueurs;
        private System.Windows.Forms.Panel pannelActions;
        private System.Windows.Forms.Button btnRester;
        private System.Windows.Forms.Button btnTirer;
        private System.Windows.Forms.NumericUpDown numMise;
        private System.Windows.Forms.Button btnMiser;
        private System.Windows.Forms.Panel pannelMise;
        private System.Windows.Forms.Label labMise;
    }
}
