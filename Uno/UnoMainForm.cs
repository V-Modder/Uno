using System;
using System.Collections.Concurrent;
using System.Net;
using System.Windows.Forms;
using UnoServer;

namespace Uno
{
    public partial class UnoMainForm : Form
    {
        private ConcurrentBag<string> Logger;
        private UnoSrv server;
        private UnoClient.UnoClient uc;

        public UnoMainForm()
        {
            InitializeComponent();
            this.Icon = Uno.Properties.Resources.uno;
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
            Start();
        }

        private void tmr_closed_Tick(object sender, EventArgs e)
        {
            if (!uc.IsRunning)
            {
                if (server != null && server.IsRunning)
                    server.Stop();
                this.Close();
            }
        }

        private void txt_address_Leave(object sender, EventArgs e)
        {
            if (txt_address.Text == "")
                return;
            IPAddress ip;
            if (!IPAddress.TryParse(txt_address.Text, out ip))
            {
                TextBox tb = (TextBox)sender;
                ToolTip tt = new ToolTip();
                tt.ToolTipIcon = ToolTipIcon.Error;
                tt.IsBalloon = true;
                tt.ToolTipTitle = "Error";
                tt.Show("Please type in a valid address", tb, 20, -70, 2500);
                tb.Focus();
                tb.SelectAll();
            }
        }

        private void txt_maxPlayer_Leave(object sender, EventArgs e)
        {
            if (txt_maxPlayer.Text == "")
                return;
            int x;
            if(!int.TryParse(txt_maxPlayer.Text, out x) || x < 2 || x > 10)
            {
                TextBox tb = (TextBox)sender;
                ToolTip tt = new ToolTip();
                tt.ToolTipIcon = ToolTipIcon.Error;
                tt.IsBalloon = true;
                tt.ToolTipTitle = "Error";
                tt.Show("Playercount must be between 2-10", tb, 20, -70, 2500);
                tb.Focus();
                tb.SelectAll();
            }
        }

        private void txt_playerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Start();
            }
        }

        private void Start()
        {
            if (rdb_connect.Checked == true)
            {
                uc = new UnoClient.UnoClient(txt_address.Text, txt_playerName.Text);
                this.Hide();
                uc.Show();
                tmr_closed.Enabled = true;
            }
            else
            {
                Logger = new ConcurrentBag<string>();
                server = new UnoSrv(Convert.ToInt32(txt_maxPlayer.Text), ref Logger);
                server.Start();

                //start client
                uc = new UnoClient.UnoClient("127.0.0.1", txt_playerName.Text, true);
                this.Hide();
                uc.Show();
                tmr_closed.Enabled = true;
            }
        }
    }
}
