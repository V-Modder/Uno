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
            this.txt_players = new System.Windows.Forms.RichTextBox();
            this.btn_start = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txt_players
            // 
            this.txt_players.Location = new System.Drawing.Point(569, 12);
            this.txt_players.Name = "txt_players";
            this.txt_players.ReadOnly = true;
            this.txt_players.Size = new System.Drawing.Size(164, 286);
            this.txt_players.TabIndex = 0;
            this.txt_players.Text = "";
            // 
            // btn_start
            // 
            this.btn_start.Enabled = false;
            this.btn_start.Location = new System.Drawing.Point(120, 200);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(75, 23);
            this.btn_start.TabIndex = 1;
            this.btn_start.Text = "Start";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // UnoClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 310);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.txt_players);
            this.DoubleBuffered = true;
            this.Name = "UnoClient";
            this.Text = "UnoClient";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UnoClient_FormClosing);
            this.Load += new System.EventHandler(this.UnoClient_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox txt_players;
        private System.Windows.Forms.Button btn_start;

    }
}

