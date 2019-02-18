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
    public partial class UIExit : UserControl
    {
        public UIExit()
        {
            InitializeComponent();
        }

        private void Exit_Load(object sender, EventArgs e)
        {
            btnNo.Focus();
        }
    }
}
