namespace Uno
{
    partial class ChooseBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_ok = new System.Windows.Forms.Button();
            this.pcb_red = new System.Windows.Forms.PictureBox();
            this.pcb_yellow = new System.Windows.Forms.PictureBox();
            this.pcb_blue = new System.Windows.Forms.PictureBox();
            this.pcb_green = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pcb_red)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcb_yellow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcb_blue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcb_green)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(72, 84);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(75, 23);
            this.btn_ok.TabIndex = 0;
            this.btn_ok.Text = "OK";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // pcb_red
            // 
            this.pcb_red.BackColor = System.Drawing.Color.Red;
            this.pcb_red.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pcb_red.Location = new System.Drawing.Point(12, 24);
            this.pcb_red.Name = "pcb_red";
            this.pcb_red.Size = new System.Drawing.Size(44, 44);
            this.pcb_red.TabIndex = 1;
            this.pcb_red.TabStop = false;
            this.pcb_red.Click += new System.EventHandler(this.colors_Click);
            this.pcb_red.MouseEnter += new System.EventHandler(this.colors_MouseEnter);
            this.pcb_red.MouseLeave += new System.EventHandler(this.colors_MouseLeave);
            // 
            // pcb_yellow
            // 
            this.pcb_yellow.BackColor = System.Drawing.Color.Yellow;
            this.pcb_yellow.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pcb_yellow.Location = new System.Drawing.Point(62, 24);
            this.pcb_yellow.Name = "pcb_yellow";
            this.pcb_yellow.Size = new System.Drawing.Size(44, 44);
            this.pcb_yellow.TabIndex = 2;
            this.pcb_yellow.TabStop = false;
            this.pcb_yellow.Click += new System.EventHandler(this.colors_Click);
            this.pcb_yellow.MouseEnter += new System.EventHandler(this.colors_MouseEnter);
            this.pcb_yellow.MouseLeave += new System.EventHandler(this.colors_MouseLeave);
            // 
            // pcb_blue
            // 
            this.pcb_blue.BackColor = System.Drawing.Color.Blue;
            this.pcb_blue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pcb_blue.Location = new System.Drawing.Point(112, 24);
            this.pcb_blue.Name = "pcb_blue";
            this.pcb_blue.Size = new System.Drawing.Size(44, 44);
            this.pcb_blue.TabIndex = 3;
            this.pcb_blue.TabStop = false;
            this.pcb_blue.Click += new System.EventHandler(this.colors_Click);
            this.pcb_blue.MouseEnter += new System.EventHandler(this.colors_MouseEnter);
            this.pcb_blue.MouseLeave += new System.EventHandler(this.colors_MouseLeave);
            // 
            // pcb_green
            // 
            this.pcb_green.BackColor = System.Drawing.Color.Lime;
            this.pcb_green.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pcb_green.Location = new System.Drawing.Point(162, 24);
            this.pcb_green.Name = "pcb_green";
            this.pcb_green.Size = new System.Drawing.Size(44, 44);
            this.pcb_green.TabIndex = 4;
            this.pcb_green.TabStop = false;
            this.pcb_green.Click += new System.EventHandler(this.colors_Click);
            this.pcb_green.MouseEnter += new System.EventHandler(this.colors_MouseEnter);
            this.pcb_green.MouseLeave += new System.EventHandler(this.colors_MouseLeave);
            // 
            // ChooseBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(218, 118);
            this.ControlBox = false;
            this.Controls.Add(this.pcb_green);
            this.Controls.Add(this.pcb_blue);
            this.Controls.Add(this.pcb_yellow);
            this.Controls.Add(this.pcb_red);
            this.Controls.Add(this.btn_ok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChooseBox";
            this.Text = "ChooseBox";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ChooseBox_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.pcb_red)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcb_yellow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcb_blue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcb_green)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.PictureBox pcb_red;
        private System.Windows.Forms.PictureBox pcb_yellow;
        private System.Windows.Forms.PictureBox pcb_blue;
        private System.Windows.Forms.PictureBox pcb_green;
    }
}