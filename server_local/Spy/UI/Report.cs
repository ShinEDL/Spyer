using Spy.Properties;
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
 * 查看反馈报告 窗体
 */ 

namespace Spy.UI
{
    
    public partial class Report : Form
    {
        public int progressbar = 0;
        public Image[] images;
        private static Report report;
        private Picture pic;
        private SFTP sftp;
        private Thread thread1;
        private Thread thread2;
        private Boolean error = false;//错误标志
        private String num;//编号
        private String filename;//用户名+日期
        private StreamReader sr;//读取字符流
        private String cacheUrl;//缓存txt路径
        private ArrayList arr;//图片的数量数组
        private List<string> str_list;//编号列表
        private DB db;
        public Report()
        {
            InitializeComponent();
            images = new Image[4];
            sftp = new SFTP();
            cacheUrl = "./feedback_cache.txt";
            arr = new ArrayList();
            str_list = new List<string>();
            db = new DB(this);
        }

        private void Report_Load(object sender, EventArgs e)
        {
            //打开反馈窗口时，初始化编号选择项
            //获得日期，并转换其格式以满足需求
            String date = dateTimePicker1.Value.ToString();
            date = date.Split(' ')[0].Replace('/', '-');
            Console.WriteLine(date);
            filename = Cat.userName.Text + date;

            str_list = db.getNum(filename);
            if (str_list != null)
            {
                foreach (string s in str_list)
                {
                    comboBox1.Items.Add(s);
                }
            }
        }

        public static Report getInstance()
        {
            if (report == null || report.IsDisposed)
            {
                report = new Report();
                report.FormBorderStyle = FormBorderStyle.None;
                report.Dock = DockStyle.Fill;
                report.MdiParent = Cat.ActiveForm;
                report.WindowState = FormWindowState.Maximized;
            }
            return report;
        }

        //确定按钮
        private void button1_Click(object sender, EventArgs e)
        {
            //不让再次点击确定按钮
            button1.Enabled = false;
            //使按钮不可用
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;

            error = false;
            progressbar = 0;
            images = new Image[4];
            arr.Clear();
            this.richTextBox1.Text = "";
            pictureBox1.Image = null;
            pictureBox2.Image = null;
            pictureBox3.Image = null;
            pictureBox4.Image = null;

            
            //建立线程
            thread1 = new Thread(new ThreadStart(getDataThread));
            thread2 = new Thread(new ThreadStart(getImageThread));
            thread1.IsBackground = true;
            thread2.IsBackground = true;

            //获得日期，并转换其格式以满足需求
            String date = dateTimePicker1.Value.ToString();
            date = date.Split(' ')[0].Replace('/', '-');
            Console.WriteLine(date);
            filename = Cat.userName.Text + date;

            //获得反馈编号
            num = comboBox1.Text.Trim();
            if (num == "请选择" || num =="" || num == null)
            {
                MessageBox.Show("没有选择编号！");
                error = true;
                return;
            }

            //状态显示
            label8.Text = db.getResult(filename,num);

            sftp.Connect();//建立服务器连接

            //开启线程
            thread1.Start();
            Thread.Sleep(1000);
            if (error == false)
            {
                thread2.Start();
            }
                

        }

        //定义一个委托  
        public delegate void MyInvoke(string a);  
        //委托函数，进行更新richTextBox1的操作
        public void updateText(string filename)
        {
            //先将文件保存下来，再用sr读取数据
            File.WriteAllBytes(cacheUrl, sftp.Get("/root/txt/" + filename + "@" + num + ".txt"));
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
                
            }
            //关闭此StreamReader对象
            sr.Close();
            progressbar = 20;
        }

        //委托函数，进行更新button的操作
        public void updateButton(string a)
        {
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
        }

        //获得txt的线程操作
        private void getDataThread()
        {
            ArrayList a = new ArrayList(); 
            //获得日期+编号的反馈报告（txt文件）
            try
            {
                a = sftp.GetFileList("/root/txt",filename);    
                if (a.Count > 0)
                {
                    MyInvoke mi2 = new MyInvoke(updateText);//窗体控件的线程间安全调用
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
            catch
            {
                error = true;
                progressbar = 0 ;
                MessageBox.Show("没有相应数据！请重新选择日期或编号");
                return;
            }
            
        }

        //获取到图片的线程操作
        private void getImageThread()
        {
            MyInvoke mi = new MyInvoke(updateButton);//窗体控件的线程间安全调用
            MemoryStream ms;//内存区域的流，用来读取截图
            string[] feedbackimgs = new string[4];
            arr = sftp.GetFeedbackList("/root/feedbackimg", filename+"@"+num);

            for (int i = 0; i < arr.Count; i++)
            {
                ms = new MemoryStream(sftp.Get("/root/feedbackimg/" + arr[i]));
                images[i] = Image.FromStream(ms);
                progressbar = progressbar + 18;
            }
            pictureBox1.Image = images[0];
            pictureBox2.Image = images[1];
            pictureBox3.Image = images[2];
            pictureBox4.Image = images[3];
            this.BeginInvoke(mi, new Object[] { filename });
            progressbar = 100;
        }

        //点击截图1，显示大图
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pic = new Picture(this);
            pic.i = 0;
            pic.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pic = new Picture(this);
            pic.i = 1;
            pic.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pic = new Picture(this);
            pic.i = 2;
            pic.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            pic = new Picture(this);
            pic.i = 3;
            pic.Show();
        }

        //已查阅按钮
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                db.updateFeedback("已查阅", filename + "@" + num + ".txt");
                MessageBox.Show("更新成功！！！");
                
            }
            catch
            {
                MessageBox.Show("更新失败！！！");
            }
            finally
            {
                label8.Text = db.getResult(filename, num);
            }
            
        }

        //处理中按钮
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                db.updateFeedback("处理中", filename + "@" + num + ".txt");
                MessageBox.Show("更新成功！！！");
            }
            catch
            {
                MessageBox.Show("更新失败！！！");
            }
            finally
            {
                label8.Text = db.getResult(filename,num);
            }
        }

        //已完成按钮
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                db.updateFeedback("已完成", filename + "@" + num + ".txt");
                MessageBox.Show("更新成功！！！");
            }
            catch
            {
                MessageBox.Show("更新失败！！！");
            }
            finally
            {
                label8.Text = db.getResult(filename,num);
            }
        }

        //监控线程thread是否完成
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressbar == 100 || error == true)
            {
                sftp.Disconnect();
                if (thread1.ThreadState == ThreadState.Running)
                {
                    thread1.Abort();
                    thread2.Abort();
                }
                if (error == true)
                {
                    //使按钮不可用
                    button2.Enabled = false;
                    button3.Enabled = false;
                    button4.Enabled = false;
                }
                //启用确定按钮
                button1.Enabled = true;
            }
        }

        //当日期更改了，也随则改变编号选择项
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            //获得日期，并转换其格式以满足需求
            String date = dateTimePicker1.Value.ToString();
            date = date.Split(' ')[0].Replace('/', '-');
            Console.WriteLine(date);
            filename = Cat.userName.Text + date;

            str_list = db.getNum(filename);
            if (str_list != null)
            {
                foreach (string s in str_list)
                {
                    comboBox1.Items.Add(s);
                }
            }
            
        }




    }
}
