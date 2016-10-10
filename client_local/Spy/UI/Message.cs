using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spy.UI
{
    public partial class Message : Form
    {
        public int flag = 0;
        public Message()
        {
            InitializeComponent();
        }

        private void Message_Load(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                label1.Text = "Auto select adapter failed,please manually select!";
            }
            if (flag == 2)
            {
                label1.Text = "No network adapters found on this computer.";
            }
            
        }
    }
}
