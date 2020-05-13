namespace Blackjack {
    partial class MenuPrincipal {
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
            this.button1 = new System.Windows.Forms.Button();
            this.btnTirer = new System.Windows.Forms.Button();
            this.pannelActions.SuspendLayout();
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
            this.pannelActions.Controls.Add(this.button1);
            this.pannelActions.Controls.Add(this.btnTirer);
            this.pannelActions.Location = new System.Drawing.Point(12, 403);
            this.pannelActions.Name = "pannelActions";
            this.pannelActions.Size = new System.Drawing.Size(170, 35);
            this.pannelActions.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(84, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Rester";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            // MenuPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGreen;
            this.ClientSize = new System.Drawing.Size(1264, 450);
            this.Controls.Add(this.pannelActions);
            this.Controls.Add(this.flowJoueurs);
            this.Name = "MenuPrincipal";
            this.Text = "Form1";
            this.pannelActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flowJoueurs;
        private System.Windows.Forms.Panel pannelActions;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnTirer;
    }
}

