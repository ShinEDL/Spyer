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
    public partial class Connecting : Form
    {
        private int time;
        public Connecting()
        {
            InitializeComponent();
            this.ControlBox = false;
            this.skinEngine1.SkinFile = Application.StartupPath + "//DiamondBlue.ssk";
            Sunisoft.IrisSkin.SkinEngine se = null;
            se = new Sunisoft.IrisSkin.SkinEngine();
            se.SkinAllForm = true;
            time = 0;
        }

       
        private void Connecting_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer2.Enabled = true;
            //start = DateTime.Now;
        }
        //窗体加载时利用timer1启动后台程序
        private void timer1_Tick(object sender, EventArgs e)
        {
            progress.Text = time.ToString() + "%";
            
            progressBar.Value = time;
            if(time<100)
                time = time + 20;

        }
        //加载后台程序完成时，关闭timer1，启动主页面
        private void timer2_Tick(object sender, EventArgs e)
        {
            if(progressBar.Value == progressBar.Maximum)
            {
                timer1.Enabled = false;
                Main main = new Main();
                main.Show();
                this.Close();
            }
        }

        

        
        

        
    }
}
