using Spy.util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Collections;


/*
 * 查看截图 窗体
 */ 
namespace Spy.UI
{
    public partial class Screenshot : Form
    {
        private static Screenshot ss;
        private SFTP sftp;
        private Thread thread;
        private Image[] images;
        private ArrayList arr;//图片的数量数组
        private int flag;//images下标
        private Boolean error = false;//错误标志
        private String filename;//文件名：用户名+日期
        public int progressbar;//进度条


        public Screenshot()
        {
            InitializeComponent();
            progressbar = 0;
            sftp = new SFTP();
            arr = new ArrayList();
        }

        public static Screenshot getInstance()
        {
            if (ss == null || ss.IsDisposed)
            {
                ss = new Screenshot();
                ss.FormBorderStyle = FormBorderStyle.None;
                ss.Dock = DockStyle.Fill;
                ss.MdiParent = Cat.ActiveForm;
                ss.WindowState = FormWindowState.Maximized;
            }
            return ss;
        }

        //确定按钮
        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;//不让再次点击确定按钮
            button1.Enabled = false;
            button2.Enabled = false;
            error = false;
            progressbar = 0;
            images = null;
            arr.Clear();

            //转换dateTimePicker1控件的值以符合要求
            String date = dateTimePicker1.Value.ToString();
            date = date.Split(' ')[0].Replace('/', '-');
            Console.WriteLine(date);
            filename = Cat.userName.Text + date;

            thread = new Thread(new ThreadStart(getImageThread));
            thread.IsBackground = true;
            thread.Start();

        }

        //定义一个委托  
        public delegate void MyInvoke();
        //符合委托的函数，进行button的操作  
        public void UpdateForm()
        {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
        }  
        //获取图片线程操作
        private void getImageThread()
        {
            //初始化
            sftp.Connect();//与服务器建立连接
            MemoryStream ms;//内存区域的流，用来读取截图
            MyInvoke mi = new MyInvoke(UpdateForm);//窗体控件的线程间安全调用

            arr = sftp.GetFileList("/root/img",filename);//获取文件列表

            progressbar = 10;
            //判断如果没有相应的截图时，则报错
            if (arr.Count == 0)
            {
                MessageBox.Show("没有该日期的数据！");
                progressbar = 0;
                error = true;
                return;
            }
            
            //先读取出第一个截图
            ms = new MemoryStream(sftp.Get("/root/img/"+arr[0]));
            images = new Image[arr.Count];
            images[0] = Image.FromStream(ms);
            pictureBox1.Image = images[0];
            flag = 0;
            ms.Close();
            progressbar = 12;

            
            //如果有更多图片，则一一读取
            if(arr.Count > 1)
            {
                for (int i = 1; i < arr.Count; i++)
                {
                    Console.WriteLine(arr[i]);
                    ms = new MemoryStream(sftp.Get("/root/img/" + arr[i]));
                    images[i] = Image.FromStream(ms);
                    ms.Close();
                    if (progressbar >= 92)
                        progressbar = 92;
                    else
                        progressbar = progressbar + 4;
                        
                }  
            }
            sftp.Disconnect();//断开服务器连接
            this.BeginInvoke(mi);//调用控件的安全线程
            progressbar = 100;
        }

        //上一页
        private void button1_Click(object sender, EventArgs e)
        {
            flag = flag - 1;
            if (flag < 0)
            {
                flag = 0;
                button1.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
                button2.Enabled = true;
            }
            pictureBox1.Image = images[flag];
                
        }

        //下一页
        private void button2_Click(object sender, EventArgs e)
        {
            flag = flag + 1;
            if( flag >= images.Length)
            { 
                flag = images.Length - 1;
                button2.Enabled = false;
            }
            else
            {
                button2.Enabled = true;
                button1.Enabled = true;
            }
            pictureBox1.Image = images[flag];
                
        }

        //检测thread是否完成工作
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            if (progressbar == 100 || error == true)
            {
                thread.Abort();
                button3.Enabled = true;
            }
        }

        

    }
}
