using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommunicationLibrary;
using System.Web.Helpers;
using System.Diagnostics;

namespace KuhlClient
{

    public partial class Form1 : Form
    {
        public string ip;
        public string name;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmMain frmMain = new frmMain(this);
            name = txtName.Text;
            ip = txtIp.Text;
            frmMain.Show();
            this.Visible = false;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (txtName.Text != "Name" && txtIp.Text != "")
            {
                btnconnect.Enabled = true; 
            }
            else btnconnect.Enabled = false;
        }

    }
}
