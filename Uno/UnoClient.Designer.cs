namespace UnoClient
{
    partial class UnoClient
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.txt_players = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txt_players
            // 
            this.txt_players.Enabled = false;
            this.txt_players.Location = new System.Drawing.Point(560, 13);
            this.txt_players.Multiline = true;
            this.txt_players.Name = "txt_players";
            this.txt_players.Size = new System.Drawing.Size(173, 285);
            this.txt_players.TabIndex = 0;
            // 
            // UnoClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 310);
            this.Controls.Add(this.txt_players);
            this.DoubleBuffered = true;
            this.Name = "UnoClient";
            this.Text = "UnoClient";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UnoClient_FormClosing);
            this.Load += new System.EventHandler(this.UnoClient_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_players;
    }
}

