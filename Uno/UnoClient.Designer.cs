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
            this.lbl_players = new System.Windows.Forms.Label();
            this.btn_recieve = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txt_players
            // 
            this.txt_players.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_players.Location = new System.Drawing.Point(569, 200);
            this.txt_players.Name = "txt_players";
            this.txt_players.ReadOnly = true;
            this.txt_players.Size = new System.Drawing.Size(164, 98);
            this.txt_players.TabIndex = 0;
            this.txt_players.Text = "";
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(120, 200);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(75, 23);
            this.btn_start.TabIndex = 1;
            this.btn_start.Text = "Start";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // lbl_players
            // 
            this.lbl_players.AutoSize = true;
            this.lbl_players.Location = new System.Drawing.Point(569, 181);
            this.lbl_players.Name = "lbl_players";
            this.lbl_players.Size = new System.Drawing.Size(44, 13);
            this.lbl_players.TabIndex = 2;
            this.lbl_players.Text = "Players:";
            // 
            // btn_recieve
            // 
            this.btn_recieve.Enabled = false;
            this.btn_recieve.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_recieve.Location = new System.Drawing.Point(706, 13);
            this.btn_recieve.Name = "btn_recieve";
            this.btn_recieve.Size = new System.Drawing.Size(26, 23);
            this.btn_recieve.TabIndex = 3;
            this.btn_recieve.Text = "+";
            this.btn_recieve.UseVisualStyleBackColor = true;
            this.btn_recieve.Click += new System.EventHandler(this.btn_recieve_Click);
            // 
            // UnoClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 310);
            this.Controls.Add(this.btn_recieve);
            this.Controls.Add(this.lbl_players);
            this.Controls.Add(this.btn_start);
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

        private System.Windows.Forms.RichTextBox txt_players;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Label lbl_players;
        private System.Windows.Forms.Button btn_recieve;

    }
}

