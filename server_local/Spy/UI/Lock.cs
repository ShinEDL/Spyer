using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * 锁屏 窗体
 */ 
namespace Spy.UI
{
    public partial class Lock : Form
    {
        public Lock()
        {
            InitializeComponent();
        }

        private void Lock_Load(object sender, EventArgs e)
        {
            this.label2.Text = Cat.socket_command.receive();
        }
    }
}
