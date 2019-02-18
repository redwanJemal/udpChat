using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UdpFinishing.UIControls
{
    public partial class UICMessage : UserControl
    {
        public UICMessage()
        {
            InitializeComponent();
        }

        public string txtUsername {
            get { return lblusername.Text; }
            set { lblusername.Text = value; }
        }

        public string txtMessage
        {
            get { return lblMessage.Text; }
            set { lblMessage.Text = value; }
        }
    }
}
