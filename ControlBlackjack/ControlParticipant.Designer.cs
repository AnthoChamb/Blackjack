namespace Blackjack {
    namespace Controls {
        partial class ControlParticipant {
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
            this.labNom = new System.Windows.Forms.Label();
            this.labMontant = new System.Windows.Forms.Label();
            this.labAction = new System.Windows.Forms.Label();
            this.flowMain = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // labNom
            // 
            this.labNom.AutoSize = true;
            this.labNom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.labNom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labNom.Location = new System.Drawing.Point(3, 95);
            this.labNom.MinimumSize = new System.Drawing.Size(150, 0);
            this.labNom.Name = "labNom";
            this.labNom.Size = new System.Drawing.Size(150, 20);
            this.labNom.TabIndex = 0;
            this.labNom.Text = "Nom du joueur";
            // 
            // labMontant
            // 
            this.labMontant.AutoSize = true;
            this.labMontant.BackColor = System.Drawing.Color.Silver;
            this.labMontant.Location = new System.Drawing.Point(3, 115);
            this.labMontant.MinimumSize = new System.Drawing.Size(100, 0);
            this.labMontant.Name = "labMontant";
            this.labMontant.Size = new System.Drawing.Size(100, 13);
            this.labMontant.TabIndex = 1;
            this.labMontant.Text = "100 $";
            // 
            // labAction
            // 
            this.labAction.AutoSize = true;
            this.labAction.BackColor = System.Drawing.Color.Silver;
            this.labAction.ForeColor = System.Drawing.Color.Khaki;
            this.labAction.Location = new System.Drawing.Point(3, 128);
            this.labAction.MinimumSize = new System.Drawing.Size(100, 0);
            this.labAction.Name = "labAction";
            this.labAction.Size = new System.Drawing.Size(100, 13);
            this.labAction.TabIndex = 2;
            this.labAction.Text = "Rejoint";
            // 
            // flowMain
            // 
            this.flowMain.Location = new System.Drawing.Point(4, 4);
            this.flowMain.Name = "flowMain";
            this.flowMain.Size = new System.Drawing.Size(397, 88);
            this.flowMain.TabIndex = 3;
            // 
            // ControlParticipant
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.flowMain);
            this.Controls.Add(this.labAction);
            this.Controls.Add(this.labMontant);
            this.Controls.Add(this.labNom);
            this.Name = "ControlParticipant";
            this.Size = new System.Drawing.Size(404, 145);
            this.ResumeLayout(false);
            this.PerformLayout();

            }

            #endregion

            private System.Windows.Forms.Label labNom;
            private System.Windows.Forms.Label labMontant;
            private System.Windows.Forms.Label labAction;
            private System.Windows.Forms.FlowLayoutPanel flowMain;
        }
    }
}
