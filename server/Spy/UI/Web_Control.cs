using Spy.util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * 网页监控 窗体
 */ 
namespace Spy.UI
{
    public partial class Web_Control : Form
    {
        private static Web_Control control;
        private SFTP sftp;
        private Thread thread;
        private String filename;//用户名+日期
        private String cacheUrl;//缓存txt路径
        private StreamReader sr;//读取字符流
        private Boolean error = false;//错误标志
        public int progressbar = 0;
        public Web_Control()
        {
            InitializeComponent();
            sftp = new SFTP();
            cacheUrl = "./webControl_cache.txt";
        }

        public static Web_Control getInstance()
        {
            if (control == null || control.IsDisposed)
            {
                control = new Web_Control();
                control.FormBorderStyle = FormBorderStyle.None;
                control.Dock = DockStyle.Fill;
                control.MdiParent = Cat.ActiveForm;
                control.WindowState = FormWindowState.Maximized;
            }
            return control;
        }

        //确定 按钮
        private void button1_Click(object sender, EventArgs e)
        {
            //不让再次点击确定按钮
            button1.Enabled = false;

            error = false;
            progressbar = 0;
            this.richTextBox1.Text = "";

            //建立线程
            thread = new Thread(new ThreadStart(getDataThread));
            thread.IsBackground = true;

            //获得日期，并转换其格式以满足需求
            String date = dateTimePicker1.Value.ToString();
            date = date.Split(' ')[0].Replace('/', '-');
            Console.WriteLine(date);
            filename = Cat.userName.Text + date;

            sftp.Connect();//建立服务器连接

            //开启线程
            thread.Start();
        }

        //定义一个委托  
        public delegate void MyInvoke(string a);
        //委托函数，进行更新richTextBox1的操作
        public void updateText(string filename)
        {
            //先将文件保存下来，再用sr读取数据
            File.WriteAllBytes(cacheUrl, sftp.Get("/root/netrecord/" + filename + ".txt"));
            sr = new StreamReader(cacheUrl, System.Text.Encoding.UTF8);
            //使用StreamReader类来读取文件
            sr.BaseStream.Seek(0, SeekOrigin.Begin);
            // 从数据流中读取每一行，直到文件的最后一行，并在richTextBox1中显示出内容
            this.richTextBox1.Text = "";
            string strLine = sr.ReadLine();
            while (strLine != null)
            {
                this.richTextBox1.Text += strLine + "\n";//读取换行
                strLine = sr.ReadLine();
                if (progressbar < 90)
                {
                    progressbar = progressbar + 6;
                }
                else
                {
                    progressbar = 90;
                }
                
            }
            //关闭此StreamReader对象
            sr.Close();
        }

        //委托函数，进行更新button的操作
        public void updateButton(string a)
        {
            button2.Enabled = true;
            button3.Enabled = true;
            progressbar = 100;
        }

        //获得txt的线程操作
        private void getDataThread()
        {
            ArrayList a = new ArrayList();
            try
            {
                a = sftp.GetFileList("/root/netrecord", filename);
                if (a.Count > 0)
                {
                    //获得日期的网页监控（txt文件）
                    MyInvoke mi2 = new MyInvoke(updateText);//窗体控件的线程间安全调用
                    this.BeginInvoke(mi2, new Object[] { filename });
                    mi2 = new MyInvoke(updateButton);
                    this.BeginInvoke(mi2, new Object[] { filename });
                }
                else
                {
                    error = true;
                    progressbar = 0;
                    MessageBox.Show("没有该日期的数据！");
                    return;
                }
                
            }
            catch(Exception e)
            {
                error = true;
                Console.WriteLine("错误信息："+e.Message);
            }
            
        }

        //监控线程thread是否完成
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressbar == 100 || error == true)
            {
                sftp.Disconnect();
                if (thread.ThreadState == ThreadState.Running)
                {
                    thread.Abort();
                }
                if (error == true)
                {
                    //使按钮不可用
                    button2.Enabled = false;
                    button3.Enabled = false;
                }
                //启用确定按钮
                button1.Enabled = true;
            }
        }

        //增加违规记录
        private void button2_Click(object sender, EventArgs e)
        {
            AddViolation add = new AddViolation();
            add.ShowDialog();
        }

        //减少违规记录
        private void button3_Click(object sender, EventArgs e)
        {
            ReduceViolation reduce = new ReduceViolation();
            reduce.ShowDialog();
        }
    }
}
