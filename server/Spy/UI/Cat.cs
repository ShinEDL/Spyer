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

/*
 * 监控主窗体 
 */
namespace Spy.UI
{
    public partial class Cat : Form
    {
        //Thread thread;
        public String username;
        public String ip,user,department;
        public int cid;
        public static string Ip;
        public static int computer_id;
        public static SocketService socket_command,socket_data;
        public static ToolStripProgressBar progressBar;
        public static ToolStripStatusLabel userName,Department;
        public Thread socketCommandThread, socketDataThread,testSocketConnectThread;
        private Selection select;   
        private Web_Flow wf;
        private Applications apps;
        private Web_Control webControl;
        private Report report;
        private Inform inform;
        private ShutDown shutDown;
        private Lock lk;
        private WelCome welcome;
        private Screenshot ss;
        private ScreenshotNow sn;
        private Remotecontrol rtc;
        private DB db;
        public Cat(Selection selection)
        {
            InitializeComponent();
            select = selection;
            this.IsMdiContainer = true;//设置此窗体为mdi父窗体

        }

        private void Cat_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = username;
            toolStripStatusLabel5.Text = ip;
            toolStripStatusLabel8.Text = user;
            toolStripStatusLabel11.Text = department;

            Ip = ip;
            computer_id = cid;
            progressBar = this.toolStripProgressBar1;
            userName = this.toolStripStatusLabel8;
            Department = this.toolStripStatusLabel11;

            db = new DB();

            socket_command = new SocketService(ip, 2000);//初始化socket_command,默认端口2000
            socket_data = new SocketService(ip, 2001);//初始化socket_data,默认端口2001
            socketCommandThread = new Thread(new ThreadStart(socket_command.socketConnect));//建立socket_command连接线程
            socketDataThread = new Thread(new ThreadStart(socket_data.socketConnect));//建立socket_data连接线程
            socketCommandThread.IsBackground = true;
            socketDataThread.IsBackground = true;
            //socketCommandThread.Start();
            socketDataThread.Start();
            
            //检测是否断开连接的线程
            testSocketConnectThread = new Thread(new ThreadStart(this.testSocketConnect));
            testSocketConnectThread.IsBackground = true;
            testSocketConnectThread.Start();
            
        }

        //窗体第一次出现时
        private void Cat_Shown(object sender, EventArgs e)
        {
            if (welcome == null)
            {
                welcome = WelCome.getInstance();
                welcome.Show();
            }

        }

        private void Cat_FormClosed(object sender, FormClosedEventArgs e)
        {
            testSocketConnectThread.Abort();
            //如果还有socket连接,则关闭
            if (socket_data.s.Connected == true)
            {
                socket_data.socketClose();
            }
            this.Dispose();
            //重新载入客机列表    
            select.reloadMyListBox();
            select.timer1.Enabled = true;
            select.Show();
        }

        private void 网络流量ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form active = this.ActiveMdiChild;
            if (active != null)
            {
                active.Close();
                active.Dispose();
            }
            if (wf == null || wf.IsDisposed)
            {
                wf = Web_Flow.getInstance();
                wf.Show();
            }
            //if (active != null) 
            //{
            //    active.SendToBack();
                //wf.StartPosition = FormStartPosition.Manual;
                //wf.BringToFront();
            //    wf.Activate();
            //}
        }

        private void 进程管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form active = this.ActiveMdiChild;
            if (active != null)
            {
                active.Dispose();
            }
            if (apps == null || apps.IsDisposed)
            {
                apps = Applications.getInstance();
                apps.Show();
            }
        }

        private void 网页监控ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form active = this.ActiveMdiChild;
            if (active != null)
            {
                active.Dispose();
            }
            if (webControl == null || webControl.IsDisposed)
            {
                webControl = Web_Control.getInstance();
                webControl.Show();
            }
            //if (webControl == null)
            //{
            //    webControl = Web_Control.getInstance();
            //    webControl.Show();
            //}
            //webControl.StartPosition = FormStartPosition.Manual;
            //webControl.BringToFront();
        }

        private void 反馈报告ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form active = this.ActiveMdiChild;
            if (active != null)
            {
                active.Dispose();
            }
            if (report == null || report.IsDisposed)
            {
                report = Report.getInstance();
                report.Show();
            }
            //if (report == null)
            //{
            //    report = Report.getInstance();
            //    report.Show();
            //}
            //report.StartPosition = FormStartPosition.Manual;
            //report.BringToFront();
        }

        private void 遥控关机ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            shutDown = new ShutDown(this);
            shutDown.Show();
        }

        private void 锁屏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            socket_command.socketConnect();
            // 此处执行警告锁屏函数
            socket_command.send("[%]主机锁定[%]");
            lk = new Lock(); 
            lk.Show();
            socket_command.socketClose();
        }

        private void 发布通知ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form active = this.ActiveMdiChild;
            if (active != null)
            {
                active.Dispose();
            }
            if (inform == null || inform.IsDisposed)
            {
                inform = Inform.getInstance();
                inform.sender = username;
                inform.Show();
            }
            //if (inform == null)
            //{
            //    inform = Inform.getInstance();
            //    inform.Show();
            //}
            //inform.StartPosition = FormStartPosition.Manual;
            //inform.BringToFront();
        }

        private void 联系管理员ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Contact con = new Contact();
            con.Show();
        }

        private void 关于SpyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.Show();
        }

        private void 截图管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form active = this.ActiveMdiChild;
            if (active != null)
            {
                active.Dispose();
            }
            if (ss == null || ss.IsDisposed)
            {
                ss = Screenshot.getInstance();
                ss.Show();
            }
        }

        private void 即时截图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            socket_command.socketConnect();
            //执行发送截图指令函数
            socket_command.send("[%]即时截图[%]");
            sn = new ScreenshotNow();
            sn.Screenshot();
            sn.Show();
            socket_command.socketClose();
        }

        //首页
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form active = this.ActiveMdiChild;
            if (active != null)
            {
                active.Dispose();
            }
            if (welcome == null || welcome.IsDisposed)
            {
                welcome = WelCome.getInstance();
                welcome.Show();
            }
        }

        //远程控制
        private void 远程监控ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form active = this.ActiveMdiChild;
            if (active != null)
            {
                active.Dispose();
            }
            if (rtc == null || rtc.IsDisposed)
            {
                rtc = Remotecontrol.getInstance();
                rtc.Show();
            }
        }

        //控制progressbar进度显示
        private void timer1_Tick(object sender, EventArgs e)
        {
            //控制progressbar进度显示
            Form active = this.ActiveMdiChild;
            if(active != null)
            {
                progressBar.Value = getFormType(active);
                if (progressBar.Value < 100 && progressBar.Value > 0)
                {
                    status.Text = "读取数据中...";
                }
                else if(progressBar.Value == 100)
                {
                    status.Text = "完成操作";
                }
                else if(progressBar.Value == 0)
                {
                    status.Text = "无操作";
                }
            }
            else
            {
                progressBar.Value = 0;
                status.Text = "无操作";
            }
        }

        /*
         * private void timer2_Tick(object sender, EventArgs e)
        {
            //如果失去连接，则退出监控页面，回到选择页面
            if (socket_data.s.Connected != true || socket_data.isConnectSuccess != true || socket_data.receive() == null || socket_data.receive() == "")
            {
                this.timer1.Enabled = false;
                MessageBox.Show("主机与该客户端失去连接！");
                this.Dispose();
                select.reloadMyListBox();
                select.Show();
                this.timer2.Enabled = false;
                this.Close();
                return;
            }
        }*/

        //定义一个委托  
        public delegate void MyInvoke();

        //委托函数
        public void quit()
        {
            this.timer1.Enabled = false;
            MessageBox.Show("主机与该客户端失去连接！");
            this.Dispose();
            select.reloadMyListBox();
            select.Show();
            this.Close();
        }

        private void testSocketConnect()
        {
            Thread.Sleep(2000);
            while (true)
            {
                //如果失去连接，则退出监控页面，回到选择页面
                if (socket_data.receive() == null || socket_data.receive() == "")
                {
                    MyInvoke mi = new MyInvoke(quit);//窗体控件的线程间安全调用
                    this.BeginInvoke(mi);
                    db.updateIsOnline(0, computer_id);
                    return;
                }
            }
            
        }

        //得到当前mdi子窗体的进度条progressbar
        private int getFormType(Form active)
        {
            string type = active.GetType().FullName;
            switch (type)
            {
                case "Spy.UI.Applications":
                    return apps.progressbar;
                case "Spy.UI.Inform":
                    return inform.progressbar;
                case "Spy.UI.Report":
                    return report.progressbar;
                case "Spy.UI.Screenshot":
                    return ss.progressbar;
                case "Spy.UI.Web_Control":
                    return webControl.progressbar;
                case "Spy.UI.Web_Flow":
                    return wf.progressbar;
                case "Spy.UI.WelCome":
                    return welcome.progressbar;
            }
            return 0;
        }



    }
}
