using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
    }
}
