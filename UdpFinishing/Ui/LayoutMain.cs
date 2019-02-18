using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UdpFinishing.ChatClasses;
using UdpFinishing.Interface;
using UdpFinishing.Ui;
using UdpFinishing.UIControls;

namespace UdpFinishing
{
    public partial class LayoutMain : Form
    {
        int panelWidth;
        
        bool Hidden;
        IChat chatapp;
        delegate void SetMessage(string message, string username, int status);
        string filepath = null;
        FileManager fileManager;
        int counter = 0;

        public LayoutMain()
        {
            InitializeComponent();
            lblHidenChat.Visible = false;
            flowLayoutPanel2.AutoScroll = false;
            flowLayoutPanel2.VerticalScroll.Maximum = 0;
            flowLayoutPanel2.AutoScroll = true;
        }

        private void openchat()
        {
            chatapp = new UdpPortM(Properties.Settings.Default.Ports, Properties.Settings.Default.Portr);
            //string portsend = Properties.Settings.Default["Ports"].ToString();
            //string portrecieve = Properties.Settings.Default["Portr"].ToString();
            chatapp.OpenPort();
            chatapp.Datarecived += Chatapp_Datarecived;
            chatapp.Readmessageboth();
            //InitializeReceiver();

        }

        public void AddnewMessage(string username, string message)
        {
            UICMessage newmessage = new UICMessage();
            newmessage.txtUsername = username;
            newmessage.txtMessage = message;
            flowLayoutPanel2.Controls.Add(newmessage);

        }

        private void btnmenu_Click(object sender, EventArgs e)
        {
            tm.Start();
        }

        private void tm_Tick(object sender, EventArgs e)
        {
            if (Hidden)
            {
                pnlSidemenuContent.Width = pnlSidemenuContent.Width + 150;
                if (pnlSidemenuContent.Width >= panelWidth)
                {
                    lblHidenChat.Visible = false;
                    tm.Stop();
                    Hidden = false;
                    this.Refresh();
                }
            }
            else
            {
                pnlSidemenuContent.Width = pnlSidemenuContent.Width - 150;
                if (pnlSidemenuContent.Width <= panelWidth)
                {
                    lblHidenChat.Visible = true;
                    tm.Stop();
                    Hidden = true;
                    this.Refresh();
                }
            }
        }

        private void btnfile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            //openFileDialog1.Multiselect = true;
            openFileDialog1.Title = "Select File to send";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new
                    System.IO.StreamReader(openFileDialog1.FileName);
                filepath = openFileDialog1.FileName;
                if(filepath != null)
                {
                    txtsend.Text = filepath;
                    txtsend.ReadOnly = true;
                }
                
                
                //Console.WriteLine(" Selected file is => " + openFileDialog1.FileName);
                sr.Close();
            }
        }

        private void butnsetting_Click(object sender, EventArgs e)
        {
            Setting setting = new Setting();
            setting.ShowDialog();
        }
        private void btnclose_Click(object sender, EventArgs e)
        {
            if(chatapp != null)
                chatapp.ClosePort();
            this.Close();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            /*string portsend = Properties.Settings.Default["Ports"].ToString();
            string portrecieve = Properties.Settings.Default["Portr"].ToString();
            Connect newchat = new Connect();
            newchat.ShowDialog();*/

             if (chatapp == null || !chatapp.IsOpen)
                openchat();
            //SocketMessage sk = new SocketMessage();
            //sk.StartClient();
        }
        private void btnsend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtsend.Text) && filepath == null)
            {
                MessageBox.Show("Enter Message to send");
            }
            else if(filepath != null && !string.IsNullOrEmpty(txtsend.Text)) // File is selected and message is typed
            {
                // Send file with message text
                chatapp.SendFile(filepath,counter);
            }
            else if(filepath != null && string.IsNullOrEmpty(txtsend.Text)) // File is selected but no message typed
            {
                // Send file without Message text

                chatapp.SendFile(filepath,counter);
            }
            else // Send Only Text Message
            {
                if (chatapp == null || !chatapp.IsOpen)
                    MessageBox.Show("Please Connect first");
                else
                {
                    chatapp.SendMessage(txtsend.Text);
                    AddnewMessage("You", txtsend.Text);
                    txtsend.Text = "";
                }
                
            }
            if (txtsend.ReadOnly)
            {
                txtsend.ReadOnly = false;
                txtsend.Text = "";
            }
        }

        private void Chatapp_Datarecived(object sender, EventArgs e)
        {
            MessageEventargs msg = e as MessageEventargs;
            SetMessage messageDelegate = MessageReceived;
            string message = msg.Message.ToString();
            int status = Int32.Parse(msg.Status.ToString());
            string username = msg.Username.ToString();
            //Console.WriteLine("Datarecived event " + message);
            Invoke(messageDelegate, username,message,status);
        }
        private void MessageReceived(string username, string message, int status)
        {
            //txtsend.AppendText(message + "\n");
            AddnewMessage(username, message + status);
        }

        private void btninfo_Click(object sender, EventArgs e)
        {
            //UdpPortM um = new UdpPortM(200, 200);
            //um.TestBroadcasting();

            byte[] result = fileManager.ReadChunk(counter, 5);
            fileManager.AppendData(counter,result);
            counter++;
        }

        private void txtsend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && Control.ModifierKeys == Keys.Control)
            {
                e.Handled = false;
                MessageBox.Show("Ok KeyDown");
            }

            else if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtsend.Text))
                {
                    MessageBox.Show("Enter Message to send");
                }
                else
                {
                    chatapp.SendMessage(txtsend.Text);
                    AddnewMessage("You", txtsend.Text);
                    txtsend.Text = "";
                }
            }
        }
    }
}
