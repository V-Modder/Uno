using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UnoClient;

namespace Uno
{
    public partial class UnoMainForm : Form
    {
        public UnoMainForm()
        {
            InitializeComponent();
        }

        private void rdb_connect_CheckedChanged(object sender, EventArgs e)
        {
            grb_connect.Enabled = true;
            grb_createSrv.Enabled = false;
        }

        private void rdb_createSrv_CheckedChanged(object sender, EventArgs e)
        {
            grb_createSrv.Enabled = true;
            grb_connect.Enabled = false;
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            if (rdb_connect.Checked == true)
            {
                UnoClient.UnoClient uc = new UnoClient.UnoClient(txt_address.Text);
                uc.OnExit += new MyEventHandler(uc_OnExit);
                this.Hide();
                uc.Show();
            }
            else
            {
                ///ToDo: Start server somehow
            }
        }

        private void uc_OnExit(object sender, MyEventArgs e)
        {
            this.Close();
        }
    }
}
