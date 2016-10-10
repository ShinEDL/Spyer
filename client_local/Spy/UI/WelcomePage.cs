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
    public partial class WelcomePage : Form
    {
        //private static WelcomePage welcomepage = null;
        public WelcomePage()
        {
            InitializeComponent();
        }

        /*public static WelcomePage getInstance()
        {
            if (welcomepage == null || welcomepage.IsDisposed)
            {
                welcomepage = new WelcomePage();
                welcomepage.FormBorderStyle = FormBorderStyle.None;
                welcomepage.Dock = DockStyle.Fill;
                welcomepage.MdiParent = Main.ActiveForm;
            }
            return welcomepage;
        }*/
    }
}
