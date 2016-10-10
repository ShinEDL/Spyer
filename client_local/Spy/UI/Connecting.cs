using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Spy.util;
using Spy.background;
using Spy.Test;
namespace Spy
{
    public partial class Connecting : Form
    {
        private int time;
        public string username;
        public Connecting()
        {
            InitializeComponent();
            this.ControlBox = false;
            //this.skinEngine1.SkinFile = Application.StartupPath + "//DiamondBlue.ssk";
            //Sunisoft.IrisSkin.SkinEngine se = null;
            //se = new Sunisoft.IrisSkin.SkinEngine();
            //se.SkinAllForm = true;
            time = 0;
            
        }

       
        private void Connecting_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer2.Enabled = true;

            
        }
        //窗体加载时利用timer1启动后台程序
        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar.Value = time;
            progress.Text = time.ToString() + "%";
            if(time<100)
                time = time + 50;

        }
        //加载后台程序完成时，关闭timer1，启动主页面
        private void timer2_Tick(object sender, EventArgs e)
        {
            if(progressBar.Value == progressBar.Maximum)
            {
                timer1.Enabled = false;
                Main main = new Main();
                main.username = this.username;//传递当前用户名
                main.Show();
                this.Hide();
                timer2.Enabled = false;
                //
            }
        }


    }
}
