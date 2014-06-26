using System;
using System.Drawing;
using System.Windows.Forms;
using UnoC;

namespace Uno
{
    public partial class ChooseBox : Form
    {
        private bool bClick = false;
        public Colors Choose = Colors.Black;

        public ChooseBox()
        {
            InitializeComponent();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (Choose != Colors.Black)
                this.Close();
        }

        private void colors_MouseEnter(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            switch (p.Name)
            {
                case "pcb_red":
                    Choose = Colors.Red;
                    break;
                case "pcb_green":
                    Choose = Colors.Green;
                    break;
                case "pcb_blue":
                    Choose = Colors.Blue;
                    break;
                case "pcb_yellow":
                    Choose = Colors.Yellow;
                    break;
            }
            this.Refresh();
        }

        private void colors_MouseLeave(object sender, EventArgs e)
        {
            if (!bClick)
            {
                Choose = Colors.Black;
                this.Refresh();
            }
        }

        private void ChooseBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            switch (Choose)
            {
                case Colors.Red:
                    g.DrawRectangle(new Pen(Color.Black, 5), new Rectangle(pcb_red.Location.X, pcb_red.Location.Y, pcb_red.Size.Width, pcb_red.Size.Height));
                    break;
                case Colors.Green:
                    g.DrawRectangle(new Pen(Color.Black, 5), new Rectangle(pcb_green.Location.X, pcb_green.Location.Y, pcb_green.Size.Width, pcb_green.Size.Height));
                    break;
                case Colors.Blue:
                    g.DrawRectangle(new Pen(Color.Black, 5), new Rectangle(pcb_blue.Location.X, pcb_blue.Location.Y, pcb_blue.Size.Width, pcb_blue.Size.Height));
                    break;
                case Colors.Yellow:
                    g.DrawRectangle(new Pen(Color.Black, 5), new Rectangle(pcb_yellow.Location.X, pcb_yellow.Location.Y, pcb_yellow.Size.Width, pcb_yellow.Size.Height));
                    break;
            }
        }

        private void colors_Click(object sender, EventArgs e)
        {
            if(bClick)
                bClick = false;
            else
                bClick = true;
        }
    }
}
