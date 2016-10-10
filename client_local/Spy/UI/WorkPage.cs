using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;

namespace Spy.UI
{
    public partial class WorkPage : Form
    {
        private static WorkPage workpage = null;
        private WorkPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = textBox1.Text.ToString();
        }

        public static WorkPage getInstance()
        {
            if(workpage == null || workpage.IsDisposed)
            {
                workpage = new WorkPage();
                workpage.FormBorderStyle = FormBorderStyle.None;
                workpage.Dock = DockStyle.Fill;
                workpage.MdiParent = Main.ActiveForm;
            }
            return workpage;
        }

        private void WorkPage_Load(object sender, EventArgs e)
        {
            
        }
    }
}
