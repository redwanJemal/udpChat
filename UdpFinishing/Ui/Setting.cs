using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UdpFinishing.ChatClasses;

namespace UdpFinishing
{
    public partial class Setting : Form
    {

        string[] en = { "Select language", "Amharic", "English" };
        string[] am = { "ቋንቋ ይምረጡ", "አማረኛ", "እንግሊዝኛ" };
        Boolean change = true;
        public Setting()
        {
            InitializeComponent();
            //  Trying to use newly built keyboard
            /*this.KeyPreview = true;
            txtUsername.KeyPress +=
                new KeyPressEventHandler(txtUsername_KeyPress);*/
            initData();
        }

        void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {

           /* switch (e.KeyChar)
            {
                case (char)65:
                    txtUsername.AppendText("የ");
                    break;
            }*/
        }
        /*void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57)
            {
                MessageBox.Show("Form.KeyPress: '" +
                    e.KeyChar.ToString() + "' pressed.");

                switch (e.KeyChar)
                {
                    case (char)49:
                    case (char)52:
                    case (char)55:
                        MessageBox.Show("Form.KeyPress: '" +
                            e.KeyChar.ToString() + "' consumed.");
                        e.Handled = true;
                        break;
                }
            }
            switch (e.KeyChar)
            {
                case (char)49:
                    txtUsername.AppendText("የ");
                    break;
            }
        }*/
        #region Initial Data
        private void placeHolderUserName()
        {
            txtUsername.Text = Properties.Settings.Default["UserName"].ToString();
            txtUsername.GotFocus += RemoveText;
            txtUsername.LostFocus += AddText;
        }

        public void initData()
        {
            Common settings = new Common();
            txtreciverip.Text = Properties.Settings.Default.ReciverIp.ToString();
            txtUsername.Text = Properties.Settings.Default["UserName"].ToString();
            txtportSend.Text = Properties.Settings.Default["Ports"].ToString();
            txtrecivePort.Text = Properties.Settings.Default["Portr"].ToString();
            cbmstatus.SelectedText = Properties.Settings.Default.Status.ToString();
            string[] userstatus = { "Select Status", "Online", "Offline", "Busy" };
            cbmstatus.Items.AddRange(userstatus);
            Status userStatus = new Status();
            int status = userStatus.StatusCheck(Properties.Settings.Default.Status);
            cbmstatus.SelectedIndex = status;
        }
        private void placeHolderPortSend()
        {
            txtUsername.Text = "የተጠቃሚ ስም";
            txtportSend.GotFocus += RemoveTextSendPort;
            txtportSend.LostFocus += AddTextSendPort;
        }
        private void placeHolderPortRecieve()
        {
            txtrecivePort.GotFocus += RemoveTextRecievePort;
            txtrecivePort.LostFocus += AddTextRecievePort;
        }

        private void AddText(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
                txtUsername.Text = "የተጠቃሚ ስም";
        }

        private void RemoveText(object sender, EventArgs e)
        {
            txtUsername.Text = "";
        }

        private void AddTextSendPort(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtportSend.Text))
                txtportSend.Text = "መላኪያ ፖርት";
        }

        private void RemoveTextSendPort(object sender, EventArgs e)
        {
            txtportSend.Text = "";
        }

        private void AddTextRecievePort(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtrecivePort.Text))
                txtrecivePort.Text = "መቀበያ ፖርት";
        }

        private void RemoveTextRecievePort(object sender, EventArgs e)
        {
            txtrecivePort.Text = "";
        }
        #endregion

        #region Validate Setting

        public Boolean ValidateSetting()
        {
            Boolean placeholdervalue = txtUsername.Text == "User Name" || txtportSend.Text == "Port to Send" || txtrecivePort.Text == "Port to Recive";
            Boolean valid = !string.IsNullOrEmpty(txtUsername.Text) && !string.IsNullOrEmpty(txtportSend.Text) && !string.IsNullOrEmpty(txtrecivePort.Text);

            return valid && !placeholdervalue;
        }
        #endregion
        private void btnmenu_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void butsave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Ports = Int32.Parse(txtportSend.Text);
            Properties.Settings.Default.Portr = Int32.Parse(txtrecivePort.Text);
            Properties.Settings.Default.UserName = txtUsername.Text;
            Properties.Settings.Default.Status = cbmstatus.Text;
            Properties.Settings.Default.ReciverIp = txtreciverip.Text;

            Properties.Settings.Default.Save();
            this.Close();
        }
    }
}
