namespace Blackjack {
    namespace Controls {
        partial class ControlCarte {
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

            #region Code généré par le Concepteur de composants

            /// <summary>
            /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
            /// le contenu de cette méthode avec l'éditeur de code.
            /// </summary>
            private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlCarte));
            this.labHaut = new System.Windows.Forms.Label();
            this.labBas = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labHaut
            // 
            this.labHaut.AutoSize = true;
            this.labHaut.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labHaut.Location = new System.Drawing.Point(2, 2);
            this.labHaut.Margin = new System.Windows.Forms.Padding(0);
            this.labHaut.Name = "labHaut";
            this.labHaut.Size = new System.Drawing.Size(18, 16);
            this.labHaut.TabIndex = 0;
            this.labHaut.Text = "A";
            // 
            // labBas
            // 
            this.labBas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labBas.AutoSize = true;
            this.labBas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labBas.Location = new System.Drawing.Point(28, 62);
            this.labBas.Margin = new System.Windows.Forms.Padding(0);
            this.labBas.Name = "labBas";
            this.labBas.Size = new System.Drawing.Size(18, 16);
            this.labBas.TabIndex = 1;
            this.labBas.Text = "A";
            this.labBas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ControlCarte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Controls.Add(this.labBas);
            this.Controls.Add(this.labHaut);
            this.DoubleBuffered = true;
            this.Name = "ControlCarte";
            this.Size = new System.Drawing.Size(50, 80);
            this.ResumeLayout(false);
            this.PerformLayout();

            }

            #endregion

            private System.Windows.Forms.Label labHaut;
            private System.Windows.Forms.Label labBas;
        }
    }
}
