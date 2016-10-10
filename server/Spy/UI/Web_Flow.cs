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

/*
 * 网速监控 窗体
 */ 
namespace Spy.UI
{
    public partial class Web_Flow : Form
    {
        private static Web_Flow flow;
        private string data;
        private Random random;
        private string up_Text, down_Text;
        private int up, down;
        public int progressbar = 0;
        private Thread thread ;
        public Web_Flow()
        {
            InitializeComponent();
            random = new Random();
        }

        public static Web_Flow getInstance()
        {
            if (flow == null || flow.IsDisposed)
            {
                flow = new Web_Flow();
                flow.FormBorderStyle = FormBorderStyle.None;
                flow.Dock = DockStyle.Fill;
                flow.MdiParent = Cat.ActiveForm;
                flow.WindowState = FormWindowState.Maximized;
                
            }
            return flow;
        }

        public void update()
        {
            string recive = Cat.socket_data.receive();
            if (recive != null || recive != "")
            {
                if (recive.Substring(0, 1) == "%")
                {
                    data = recive.Substring(recive.IndexOf('%') + 1, recive.LastIndexOf('%') - 1);
                    Console.WriteLine(data);
                    up_Text = data.Split('@')[0];
                    down_Text = data.Split('@')[1];

                    up = (int)float.Parse(up_Text.Split(' ')[0]);
                    down = (int)float.Parse(down_Text.Split(' ')[0]);
                    Console.WriteLine("上传速度:" + up);
                    Console.WriteLine("下载速度:" + down);

                 }

             }

            data = "";

        }

        //线程函数
        private void run()
        {
            while (true)
            {
                try
                {
                    update();
                    Thread.Sleep(1000);
                }
                catch (Exception e1)
                {
                    Console.WriteLine("Web_Flow 错误信息 ： " + e1.Message);
                    return;
                }
                
            }
        }

        private void Web_Flow_Load(object sender, EventArgs e)
        {
            thread = new Thread(new ThreadStart(this.run));
            thread.IsBackground = true;
            thread.Start();

            Thread.Sleep(1000);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = up_Text;
            label4.Text = down_Text;
            if (up > statusChart1.Range)
            {

                up = 799;
                statusChart1.Value = up;
            }
            else
            {
                statusChart1.Value = up;
            }
            if (down > statusChart2.Range)
            {
                down = 799;
                statusChart2.Value = down;
            }
            else
            {
                statusChart2.Value = down;
            }
        }

        private void Web_Flow_FormClosed(object sender, FormClosedEventArgs e)
        {
            thread.Abort();
        }


    }
}
