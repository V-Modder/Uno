using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Windows.Forms;

namespace UnoClient
{
    public partial class UnoClient : Form
    {
        public event MyEventHandler OnExit;
        private TcpClient client;
        private NetworkStream stream;

        public UnoClient(string Address)
        {
            InitializeComponent();
            client = new TcpClient(Address, 3456);
            stream = client.GetStream();
            //stream.
        }

        private void UnoClient_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (OnExit != null)
            {
                OnExit(this, new MyEventArgs("UnoClient Exited"));
            }
        }
    }

    public delegate void MyEventHandler(object source, MyEventArgs e);

    public class MyEventArgs : EventArgs
    {
        private string EventInfo;
        public MyEventArgs(string Text)
        {
            EventInfo = Text;
        }
        public string GetInfo()
        {
            return EventInfo;
        }
    }

}
