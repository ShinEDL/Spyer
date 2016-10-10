using Spy.util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spy.UI
{
    public partial class Remotecontrol : Form
    {
        private static Remotecontrol rtc;
        private SocketService socket_remote;
        private byte[] MsgBuffer;//存放消息数据
        private Thread ServerThread;//服务端运行的线程
        private Image img;
        public int progressbar = 0;
        public Remotecontrol()
        {
            InitializeComponent();

        }

        public static Remotecontrol getInstance()
        {
            if (rtc == null || rtc.IsDisposed)
            {
                rtc = new Remotecontrol();
                rtc.FormBorderStyle = FormBorderStyle.None;
                rtc.Dock = DockStyle.Fill;
                rtc.MdiParent = Cat.ActiveForm;
                rtc.WindowState = FormWindowState.Maximized;
            }
            return rtc;
        }

        private void Remotecontrol_Load(object sender, EventArgs e)
        {
            socket_remote = new SocketService(Cat.Ip, 2003);
            
            MsgBuffer = new byte[1048576];
        }

        //线程函数
        private void run()
        {
            progressbar = 40;
            Thread.Sleep(350);
            socket_remote.socketConnect();
            socket_remote.send("Send");
            progressbar = 100;
            socket_remote.s.BeginReceive(MsgBuffer, 0, MsgBuffer.Length, SocketFlags.None, new AsyncCallback(RecieveCallBack), socket_remote.s);
        }

        //定义一个委托  
        public delegate void MyInvoke();

        //更新picturebox
        private void updatePicbox()
        {
            if (img != null)
                pictureBox1.Image = img;
        }

        private void RecieveCallBack(IAsyncResult AR)
        {
            try
            {
                MsgBuffer = new byte[1048576];
                int number = socket_remote.s.Receive(MsgBuffer);
                img = RemoteScreen.ToImage(MsgBuffer);
                img = RemoteScreen.BySizeGetScreen(img, pictureBox1.Width, pictureBox1.Height);
                //if (img != null)
                //    pictureBox1.Image = img;
                MyInvoke mi = new MyInvoke(updatePicbox);
                this.BeginInvoke(mi);
                socket_remote.s.BeginReceive(MsgBuffer, 0, MsgBuffer.Length, SocketFlags.None, new AsyncCallback(RecieveCallBack), socket_remote.s);
            }
            catch
            {
                //停止监控
            }
            

        }

        //启动监控 按钮
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                progressbar = 10;
                //MsgBuffer = new byte[65535];
                //MsgBuffer = Encoding.Unicode.GetBytes("Send");
                //socket_remote.s.Send(MsgBuffer);
                //socket_remote.s.BeginReceive(MsgBuffer, 0, MsgBuffer.Length, SocketFlags.None, new AsyncCallback(RecieveCallBack), socket_remote.s);
                ServerThread = new Thread(new ThreadStart(this.run));
                ServerThread.IsBackground = true;
                ServerThread.Start();
                button1.Enabled = false;
                button2.Enabled = true;
            }
            catch
            {

            }

        }

        //停止监控 按钮
        private void button2_Click(object sender, EventArgs e)
        {
            progressbar = 0;
            button1.Enabled = true;
            button2.Enabled = false;

            ServerThread.Abort();
            Thread.Sleep(1000);
            
            socket_remote.socketClose();
            
            ServerThread = null;
            pictureBox1.Image = null;

            MessageBox.Show("已经停止监控！");
        }





    }
}
