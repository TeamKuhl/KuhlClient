using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using CommunicationLibrary;

namespace KuhlClient
{
    public partial class frmMain : Form
    {
        private Client Client = new Client();
        Form1 mParentForm;
        public frmMain(Form1 parent)
        {
            mParentForm = parent;
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Client.onReceive += new ClientReceiveHandler(ReceiveEvent);
            Client.connect(mParentForm.ip, 45454);
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Client.disconnect();
            Client.onReceive -= new ClientReceiveHandler(ReceiveEvent);
            mParentForm.Close();
        }

        public void ReceiveEvent(String type, String message)
        {
            switch (type)
            {
                case "welcome":
                    Client.send("name", mParentForm.name);
                    addChat("You joined the chatroom");
                    break;
                case "message":
                    addChat(message);
                    break;
            }
        }


        private delegate void SetlstChatMessageCallback(string message);
        private void addChat(string message)
        {
            if (lstChat.InvokeRequired)
            {
                lstChat.Invoke(new SetlstChatMessageCallback(addChat), message);
            }
            else
            {
                lstChat.Items.Add(message);
                lstChat.SelectedIndex = lstChat.Items.Count - 1;
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (txtInput.Text != "")
            {
                Client.send("message", txtInput.Text);
                txtInput.Text = "";
                txtInput.Focus();
            }
        }

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                btnSend.PerformClick();
            }
        }
    }
}
