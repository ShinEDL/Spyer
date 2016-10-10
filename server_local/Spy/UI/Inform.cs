using Spy.Properties;
using Spy.util;
using System;
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
 * 通知 窗体
 */ 
namespace Spy.UI
{
    public partial class Inform : Form
    {
        private static Inform inf;
        private List<string> users;//在线用户列表
        private List<string> IP;//需要推送的IP列表
        private SocketService socket_Inform;
        private Thread socket_thread;
        private string richTextBox;
        public int progressbar = 0;
        public string sender;
        DB db = null;
        public Inform()
        {
            InitializeComponent();
            db = new DB();
            IP = new List<string>();
        }

        public static Inform getInstance()
        {
            if (inf == null || inf.IsDisposed)
            {
                inf = new Inform();
                inf.FormBorderStyle = FormBorderStyle.None;
                inf.Dock = DockStyle.Fill;
                inf.MdiParent = Cat.ActiveForm;
                inf.WindowState = FormWindowState.Maximized;
            }
            return inf;
        }

        //user.Split('@')[0] 计算机编号
        //user.Split('@')[1] IP
        private void Inform_Load(object sender, EventArgs e)
        {
            string u;
            users = db.getOnlineUsers();
            foreach(string user in users)
            {
                u = user.Split('@')[0];
                listBox1.Items.Add(u);
            }
        }

        //发送按钮
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                progressbar = 0;
                richTextBox = richTextBox1.Text;
                socket_thread = new Thread(new ThreadStart(this.run));
                socket_thread.IsBackground = true;
                socket_thread.Start();
            }
            catch(Exception e1)
            {
                Console.WriteLine("确定按钮-错误信息：" + e1.Message);
            }
            

            
        }

        //保存按钮
        private void button7_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "文本文件|*.txt";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SaveFile(sfd.FileName, RichTextBoxStreamType.PlainText);
                MessageBox.Show("文件已成功保存");
            }
            
        }

        //取消按钮
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                socket_thread.Abort();
                MessageBox.Show("停止推送！");
            }
            catch(Exception e1)
            {
                Console.WriteLine("取消按钮-错误信息：" + e1.Message);
            }
            
        }

        //+ 按钮
        private void button1_Click(object sender, EventArgs e)
        {
            foreach (string user in users)
            {
                if (listBox1.SelectedItem.ToString() == user.Split('@')[0])
                {
                    IP.Add(user.Split('@')[1]);
                }
            }
            listBox2.Items.Add(listBox1.SelectedItem);
        }

        //- 按钮
        private void button2_Click(object sender, EventArgs e)
        {
            foreach (string user in users)
            {
                if (listBox2.SelectedItem.ToString() == user.Split('@')[0])
                {
                    IP.Remove(user.Split('@')[1]);
                }
            }
            listBox2.Items.Remove(listBox2.SelectedItem);
        }

        //选择全部 按钮
        private void button3_Click(object sender, EventArgs e)
        {
            string u,ip;
            listBox2.Items.Clear();
            foreach (string user in users)
            {
                ip = user.Split('@')[1];
                IP.Add(ip);
                u = user.Split('@')[0];
                listBox2.Items.Add(u);
            }
        }

        //取消全部 按钮
        private void button4_Click(object sender, EventArgs e)
        {
            IP.Clear();
            listBox2.Items.Clear();
        }

        //线程函数
        private void run()
        {
            socketReuse(richTextBox);
        }
        //socket重用封装
        private void socketReuse(string richTextBox)
        {
            foreach (string ip in IP)
            {
                socket_Inform = new SocketService(ip, 2002);
                socket_Inform.socketConnect();
                //发送信息 格式：[%]推送[%][&]推送内容[&]
                socket_Inform.send("[%]推送[%][&]" + richTextBox + "[&]");
                socket_Inform.socketClose();
                socket_Inform = null;
                //更新数据库inform_table
                db.updateInform(sender, richTextBox);

                progressbar = progressbar + 2;
                if (progressbar > 90)
                {
                    progressbar = 90;
                }
                Thread.Sleep(200);
            }
            progressbar = 100;
        }

    }
}
