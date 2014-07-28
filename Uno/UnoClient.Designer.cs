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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btn_start = new System.Windows.Forms.Button();
            this.btn_recieve = new System.Windows.Forms.Button();
            this.lbl_warnplustwo = new System.Windows.Forms.Label();
            this.pcb_green = new System.Windows.Forms.PictureBox();
            this.btn_endround = new System.Windows.Forms.Button();
            this.dgv_player = new System.Windows.Forms.DataGridView();
            this.player = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cardcount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pcb_green)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_player)).BeginInit();
            this.SuspendLayout();
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
            // btn_recieve
            // 
            this.btn_recieve.Enabled = false;
            this.btn_recieve.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_recieve.Location = new System.Drawing.Point(675, 12);
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
            this.pcb_green.Image = global::Uno.Properties.Resources.greenlight;
            this.pcb_green.Location = new System.Drawing.Point(707, 41);
            this.pcb_green.Name = "pcb_green";
            this.pcb_green.Size = new System.Drawing.Size(26, 23);
            this.pcb_green.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcb_green.TabIndex = 5;
            this.pcb_green.TabStop = false;
            this.pcb_green.Visible = false;
            // 
            // btn_endround
            // 
            this.btn_endround.Enabled = false;
            this.btn_endround.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_endround.ForeColor = System.Drawing.Color.Red;
            this.btn_endround.Location = new System.Drawing.Point(707, 12);
            this.btn_endround.Name = "btn_endround";
            this.btn_endround.Size = new System.Drawing.Size(26, 23);
            this.btn_endround.TabIndex = 6;
            this.btn_endround.Text = "Ø";
            this.btn_endround.UseVisualStyleBackColor = true;
            this.btn_endround.Click += new System.EventHandler(this.btn_endround_Click);
            // 
            // dgv_player
            // 
            this.dgv_player.AllowUserToAddRows = false;
            this.dgv_player.AllowUserToDeleteRows = false;
            this.dgv_player.AllowUserToResizeColumns = false;
            this.dgv_player.AllowUserToResizeRows = false;
            this.dgv_player.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgv_player.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.dgv_player.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_player.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.player,
            this.cardcount});
            this.dgv_player.Location = new System.Drawing.Point(569, 200);
            this.dgv_player.Name = "dgv_player";
            this.dgv_player.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.dgv_player.RowHeadersVisible = false;
            this.dgv_player.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv_player.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_player.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgv_player.Size = new System.Drawing.Size(164, 98);
            this.dgv_player.TabIndex = 7;
            this.dgv_player.SelectionChanged += new System.EventHandler(this.dgv_player_SelectionChanged);
            // 
            // player
            // 
            this.player.HeaderText = "Player";
            this.player.Name = "player";
            this.player.ReadOnly = true;
            this.player.Width = 120;
            // 
            // cardcount
            // 
            this.cardcount.HeaderText = "Cards";
            this.cardcount.Name = "cardcount";
            this.cardcount.ReadOnly = true;
            this.cardcount.Width = 44;
            // 
            // UnoClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 310);
            this.Controls.Add(this.dgv_player);
            this.Controls.Add(this.btn_endround);
            this.Controls.Add(this.pcb_green);
            this.Controls.Add(this.lbl_warnplustwo);
            this.Controls.Add(this.btn_recieve);
            this.Controls.Add(this.btn_start);
            this.DoubleBuffered = true;
            this.Name = "UnoClient";
            this.Text = "UnoClient";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UnoClient_FormClosing);
            this.Load += new System.EventHandler(this.UnoClient_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pcb_green)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_player)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Button btn_recieve;
        private System.Windows.Forms.Label lbl_warnplustwo;
        private System.Windows.Forms.PictureBox pcb_green;
        private System.Windows.Forms.Button btn_endround;
        private System.Windows.Forms.DataGridView dgv_player;
        private System.Windows.Forms.DataGridViewTextBoxColumn player;
        private System.Windows.Forms.DataGridViewTextBoxColumn cardcount;

    }
}

