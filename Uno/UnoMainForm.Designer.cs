namespace Uno
{
    partial class UnoMainForm
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
            this.btn_start = new System.Windows.Forms.Button();
            this.rdb_createSrv = new System.Windows.Forms.RadioButton();
            this.rdb_connect = new System.Windows.Forms.RadioButton();
            this.grb_connect = new System.Windows.Forms.GroupBox();
            this.lbl_address = new System.Windows.Forms.Label();
            this.txt_address = new System.Windows.Forms.TextBox();
            this.txt_playerName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grb_createSrv = new System.Windows.Forms.GroupBox();
            this.lbl_maxPlayer = new System.Windows.Forms.Label();
            this.txt_maxPlayer = new System.Windows.Forms.TextBox();
            this.grb_connect.SuspendLayout();
            this.grb_createSrv.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(95, 227);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(75, 23);
            this.btn_start.TabIndex = 0;
            this.btn_start.Text = "Start";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // rdb_createSrv
            // 
            this.rdb_createSrv.AutoSize = true;
            this.rdb_createSrv.Location = new System.Drawing.Point(12, 119);
            this.rdb_createSrv.Name = "rdb_createSrv";
            this.rdb_createSrv.Size = new System.Drawing.Size(14, 13);
            this.rdb_createSrv.TabIndex = 1;
            this.rdb_createSrv.UseVisualStyleBackColor = true;
            this.rdb_createSrv.CheckedChanged += new System.EventHandler(this.rdb_createSrv_CheckedChanged);
            // 
            // rdb_connect
            // 
            this.rdb_connect.AutoSize = true;
            this.rdb_connect.Checked = true;
            this.rdb_connect.Location = new System.Drawing.Point(12, 58);
            this.rdb_connect.Name = "rdb_connect";
            this.rdb_connect.Size = new System.Drawing.Size(14, 13);
            this.rdb_connect.TabIndex = 2;
            this.rdb_connect.TabStop = true;
            this.rdb_connect.UseVisualStyleBackColor = true;
            this.rdb_connect.CheckedChanged += new System.EventHandler(this.rdb_connect_CheckedChanged);
            // 
            // grb_connect
            // 
            this.grb_connect.Controls.Add(this.lbl_address);
            this.grb_connect.Controls.Add(this.txt_address);
            this.grb_connect.Location = new System.Drawing.Point(32, 36);
            this.grb_connect.Name = "grb_connect";
            this.grb_connect.Size = new System.Drawing.Size(200, 59);
            this.grb_connect.TabIndex = 3;
            this.grb_connect.TabStop = false;
            this.grb_connect.Text = "Verbinde zu Spiel";
            // 
            // lbl_address
            // 
            this.lbl_address.AutoSize = true;
            this.lbl_address.Location = new System.Drawing.Point(17, 22);
            this.lbl_address.Name = "lbl_address";
            this.lbl_address.Size = new System.Drawing.Size(48, 13);
            this.lbl_address.TabIndex = 1;
            this.lbl_address.Text = "Adresse:";
            // 
            // txt_address
            // 
            this.txt_address.Location = new System.Drawing.Point(94, 19);
            this.txt_address.Name = "txt_address";
            this.txt_address.Size = new System.Drawing.Size(100, 20);
            this.txt_address.TabIndex = 0;
            // 
            // txt_playerName
            // 
            this.txt_playerName.Location = new System.Drawing.Point(104, 177);
            this.txt_playerName.Name = "txt_playerName";
            this.txt_playerName.Size = new System.Drawing.Size(100, 20);
            this.txt_playerName.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 180);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Spielername:";
            // 
            // grb_createSrv
            // 
            this.grb_createSrv.Controls.Add(this.lbl_maxPlayer);
            this.grb_createSrv.Controls.Add(this.txt_maxPlayer);
            this.grb_createSrv.Enabled = false;
            this.grb_createSrv.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.grb_createSrv.Location = new System.Drawing.Point(32, 101);
            this.grb_createSrv.Name = "grb_createSrv";
            this.grb_createSrv.Size = new System.Drawing.Size(200, 70);
            this.grb_createSrv.TabIndex = 6;
            this.grb_createSrv.TabStop = false;
            this.grb_createSrv.Text = "Server erstellen";
            // 
            // lbl_maxPlayer
            // 
            this.lbl_maxPlayer.AutoSize = true;
            this.lbl_maxPlayer.Location = new System.Drawing.Point(17, 29);
            this.lbl_maxPlayer.Name = "lbl_maxPlayer";
            this.lbl_maxPlayer.Size = new System.Drawing.Size(73, 13);
            this.lbl_maxPlayer.TabIndex = 1;
            this.lbl_maxPlayer.Text = "Spieleranzahl:";
            // 
            // txt_maxPlayer
            // 
            this.txt_maxPlayer.Location = new System.Drawing.Point(94, 26);
            this.txt_maxPlayer.Name = "txt_maxPlayer";
            this.txt_maxPlayer.Size = new System.Drawing.Size(100, 20);
            this.txt_maxPlayer.TabIndex = 0;
            // 
            // UnoMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.grb_createSrv);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_playerName);
            this.Controls.Add(this.rdb_connect);
            this.Controls.Add(this.grb_connect);
            this.Controls.Add(this.rdb_createSrv);
            this.Controls.Add(this.btn_start);
            this.Name = "UnoMainForm";
            this.Text = "UnoMainForm";
            this.grb_connect.ResumeLayout(false);
            this.grb_connect.PerformLayout();
            this.grb_createSrv.ResumeLayout(false);
            this.grb_createSrv.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.RadioButton rdb_createSrv;
        private System.Windows.Forms.RadioButton rdb_connect;
        private System.Windows.Forms.GroupBox grb_connect;
        private System.Windows.Forms.Label lbl_address;
        private System.Windows.Forms.TextBox txt_address;
        private System.Windows.Forms.TextBox txt_playerName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grb_createSrv;
        private System.Windows.Forms.Label lbl_maxPlayer;
        private System.Windows.Forms.TextBox txt_maxPlayer;
    }
}

