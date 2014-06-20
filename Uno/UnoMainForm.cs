using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using UnoClient;

namespace Uno
{
    public partial class UnoMainForm : Form
    {
        Process p;

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
                UnoClient.UnoClient uc = new UnoClient.UnoClient(txt_address.Text, txt_playerName.Text);
                uc.OnExit += new MyEventHandler(uc_OnExit);
                this.Hide();
                uc.Show();
            }
            else
            {
                ///ToDo: Start server somehow
                p = new Process();
                p.StartInfo.FileName = "UnoSrv.exe";
                p.StartInfo.Arguments = "-player " + txt_maxPlayer.Text;
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.Start();
                UnoClient.UnoClient uc = new UnoClient.UnoClient("127.0.0.1", txt_playerName.Text, true);
                uc.OnExit += new MyEventHandler(uc_OnExit);
                this.Hide();
                uc.Show();
            }
        }

        private void uc_OnExit(object sender, MyEventArgs e)
        {
            if (p != null)
                p.Close();
            this.Close();
        }
    }
}
