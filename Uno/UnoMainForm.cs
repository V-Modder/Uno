using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using UnoClient;
using UnoServer;

namespace Uno
{
    public partial class UnoMainForm : Form
    {
        private ConcurrentBag<string> Logger;
        private UnoSrv server;

        public UnoMainForm()
        {
            InitializeComponent();
        }

        private void rdb_connect_CheckedChanged(object sender, EventArgs e)
        {
            grb_connect.Enabled = true;
            grb_createSrv.Enabled = false;
            txt_address.Focus();
        }

        private void rdb_createSrv_CheckedChanged(object sender, EventArgs e)
        {
            grb_createSrv.Enabled = true;
            grb_connect.Enabled = false;
            txt_maxPlayer.Focus();
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
                Logger = new ConcurrentBag<string>();
                server = new UnoSrv(Convert.ToInt32(txt_maxPlayer.Text), ref Logger); 
                server.Start();

                //start client
                UnoClient.UnoClient uc = new UnoClient.UnoClient("127.0.0.1", txt_playerName.Text, true);
                uc.OnExit += new MyEventHandler(uc_OnExit);
                this.Hide();
                uc.Show();
            }
        }

        private void uc_OnExit(object sender, MyEventArgs e)
        {
            if (server.IsRunning)
                server.Stop();
            this.Close();
        }
    }
}
