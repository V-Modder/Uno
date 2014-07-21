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
            this.lbl_warnplustwo = new System.Windows.Forms.Label();
            this.pcb_green = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pcb_green)).BeginInit();
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
            // lbl_warnplustwo
            // 
            this.lbl_warnplustwo.ForeColor = System.Drawing.Color.Red;
            this.lbl_warnplustwo.Location = new System.Drawing.Point(586, 145);
            this.lbl_warnplustwo.Name = "lbl_warnplustwo";
            this.lbl_warnplustwo.Size = new System.Drawing.Size(146, 36);
            this.lbl_warnplustwo.TabIndex = 4;
            this.lbl_warnplustwo.Text = "!!!Warning place a +2 card, to avoid getting 2 cards!!!";
            this.lbl_warnplustwo.Visible = false;
            // 
            // pcb_green
            // 
            this.pcb_green.Image = global::Uno.Properties.Resources.green;
            this.pcb_green.Location = new System.Drawing.Point(706, 42);
            this.pcb_green.Name = "pcb_green";
            this.pcb_green.Size = new System.Drawing.Size(26, 23);
            this.pcb_green.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcb_green.TabIndex = 5;
            this.pcb_green.TabStop = false;
            this.pcb_green.Visible = false;
            // 
            // UnoClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 310);
            this.Controls.Add(this.pcb_green);
            this.Controls.Add(this.lbl_warnplustwo);
            this.Controls.Add(this.btn_recieve);
            this.Controls.Add(this.lbl_players);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.txt_players);
            this.DoubleBuffered = true;
            this.Name = "UnoClient";
            this.Text = "UnoClient";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UnoClient_FormClosing);
            this.Load += new System.EventHandler(this.UnoClient_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pcb_green)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox txt_players;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Label lbl_players;
        private System.Windows.Forms.Button btn_recieve;
        private System.Windows.Forms.Label lbl_warnplustwo;
        private System.Windows.Forms.PictureBox pcb_green;

    }
}

