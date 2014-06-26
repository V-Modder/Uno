using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using UnoServer;

namespace UnoSrvForm
{
    public partial class UnoSvrForm : Form
    {
        private List<string> ausgabe = new List<string>();
        private ConcurrentBag<string> aus = new ConcurrentBag<string>();
        private UnoSrv server;

        public UnoSvrForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int playercount = 0;
            string[] args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-player")
                    playercount = Convert.ToInt32(args[i + 1]);
            }
            if (playercount != 0)
            {
                server = new UnoSrv(playercount,ref aus);
                timer1.Enabled = true;
                server.Start();
            }
            else
                this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            while (aus.Count > 0)
            {
                string s;
                while(!aus.TryTake(out s));
                if(s.Contains("error"))
                    rtb_logger.AppendText(s + Environment.NewLine, Color.Red);
                else
                    rtb_logger.AppendText(s + Environment.NewLine, Color.Black);
            }
        }
    }
}
