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
 * 监控应用程序开启记录 窗体 
 */
namespace Spy.UI
{
    public partial class Applications : Form
    {
        private static Applications apps;
        private SFTP sftp;
        public int progressbar = 0;
        public Applications()
        {
            InitializeComponent();
            sftp = new SFTP();
        }

        public static Applications getInstance()
        {
            if (apps == null || apps.IsDisposed)
            {
                apps = new Applications();
                apps.FormBorderStyle = FormBorderStyle.None;
                apps.Dock = DockStyle.Fill;
                apps.MdiParent = Cat.ActiveForm;
                apps.WindowState = FormWindowState.Maximized;
            }
            return apps;
        }

        //保存并上传记录
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                richTextBox1.SaveFile("./apps.txt", RichTextBoxStreamType.PlainText);
                Thread.Sleep(1000);
                sftp.Put("./apps.txt", "/root/applications/" + Cat.userName + DateTime.Now.ToString("yyyy-M-d") + ".txt");
                progressbar = 100;
            }
            catch
            {
                MessageBox.Show("上传失败！");
            }
        }

        //获取客户端当前应用程序记录
        private void button2_Click(object sender, EventArgs e)
        {
            StreamReader sr = null ;
            try
            {
                Cat.socket_command.socketConnect();
                Cat.socket_command.send("[%]进程列表[%]");

                byte[] length = new byte[100];
                Cat.socket_command.s.Receive(length);
                int leng = System.BitConverter.ToInt32(length, 0);
                byte[] buffer = new byte[leng];
                Cat.socket_command.s.Receive(buffer);
                string str = System.Text.Encoding.Default.GetString(buffer);
                StreamWriter protxt = new StreamWriter("process.txt", false);//存在改写 不存在创建
                protxt.WriteLine(str);//写入
                protxt.Close();

                Console.WriteLine("接受成功");

                sr = new StreamReader("./process.txt", System.Text.Encoding.UTF8);
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
                Cat.socket_command.socketClose();
                progressbar = 100;
            }
            catch
            {
                MessageBox.Show("获取失败！");
            }
            finally
            {
                sr.Close();
            }
            
        }

        //添加违规记录
        private void button3_Click(object sender, EventArgs e)
        {
            AddViolation add = new AddViolation();
            add.ShowDialog();
        }

        //删除违规记录
        private void button4_Click(object sender, EventArgs e)
        {
            ReduceViolation reduce = new ReduceViolation();
            reduce.ShowDialog();
        }



    }
}
