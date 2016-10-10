using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spy
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            this.skinEngine1.SkinFile = Application.StartupPath + "//DiamondBlue.ssk";
            Sunisoft.IrisSkin.SkinEngine se = null;
            se = new Sunisoft.IrisSkin.SkinEngine();
            se.SkinAllForm = true;
            
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult r = MessageBox.Show("确定要退出程序？","操作提示",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
                if(r == DialogResult.OK)
                {
                    e.Cancel = false;
                    Application.Exit();
                }
                else 
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
