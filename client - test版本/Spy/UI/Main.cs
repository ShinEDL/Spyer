using Spy.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Spy.util;
using System.Management;
using MySql.Data.MySqlClient;
using System.IO;
using System.Threading;
using System.Net;
using Sniffer;


namespace Spy
{
    public partial class Main : Form
    {
        public string username;
        public static string user;
        public string IpAddress;
        public static string MYIP;
        public string MacAddress;
        public string department;
        public int cid;
        public static int computernum;
        DB db = null;
        public static Network network;
        PrintScreen1 printscreen;
        public static int protxtnum = 1;
        public static string Severip;
        public int nettxtnum = 1;
        public int flag = 0;
        private static SnifferSocket m_Sniffer;
        public string GetMacAddress()//网卡mac 查询
        {
            try
            {
                //获取网卡硬件地址 
                string mac = "";
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        mac = mo["MacAddress"].ToString();
                        break;
                    }
                }
                moc = null;
                mc = null;
                return mac;
            }
            catch
            {
                MessageBox.Show("获取电脑CPU信息失败 请联系管理员");
                return "error";
            }
            finally
            {
            }

        }


        public string GetIPAddress()//IP地址查询
        {
            try
            {
                //获取IP地址 
                string st = "";
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        //st=mo["IpAddress"].ToString(); 
                        System.Array ar;
                        ar = (System.Array)(mo.Properties["IpAddress"].Value);
                        st = ar.GetValue(0).ToString();
                        break;
                    }
                }
                moc = null;
                mc = null;
                return st;
            }
            catch
            {
                MessageBox.Show("获取电脑IP信息失败 请联系管理员");
                return "error";
            }
            finally
            {
            }

        }

        public Main()
        {
            InitializeComponent();
            //this.skinEngine1.SkinFile = Application.StartupPath + "//DiamondBlue.ssk";
            //Sunisoft.IrisSkin.SkinEngine se = null;
            //se = new Sunisoft.IrisSkin.SkinEngine();
            //se.SkinAllForm = true;
            this.IsMdiContainer = true;
            db = new DB(this);
            
        }

        private void Main_Load(object sender, EventArgs e)
        {
            network = new Network();
            network.MdiParent = this;
            network.FormBorderStyle = FormBorderStyle.None;
            network.Dock = DockStyle.Fill;
            network.Show();
            userName.Text = this.username;//显示当前用户名
            user = this.username;
            MacAddress = GetMacAddress();
            //Mac.Text = MacAddress;
            IpAddress = GetIPAddress();
            MYIP = IpAddress;
            IPadd.Text = IpAddress;
            //清空旧缓存文件
            string exepath = Environment.CurrentDirectory;
            try
            {
                DirectoryInfo di = new DirectoryInfo(exepath + @"\net\");
                di.Delete(true);
                di = new DirectoryInfo(exepath + @"\feedbacktxt\");
                di.Delete(true);
                di = new DirectoryInfo(exepath + @"\pro\");
                di.Delete(true);

            }
            catch
            {

            }
            if (Directory.Exists(exepath + @"\net\") == false)//如果不存在就创建net文件夹
            {
                Directory.CreateDirectory(exepath + @"\net\");
            }
            if (Directory.Exists(exepath + @"\feedbacktxt\") == false)//如果不存在就创建feedbacktxt文件夹
            {
                Directory.CreateDirectory(exepath + @"\feedbacktxt\");
            }
            if (Directory.Exists(exepath + @"\pro\") == false)//如果不存在就创建pro文件夹
            {
                Directory.CreateDirectory(exepath + @"\pro\");
            }

            //获取服务器IP
            if (System.IO.File.Exists(exepath + @"\IpAdress.txt") == true)
            {
                StreamReader sr = File.OpenText(exepath + @"\IpAdress.txt");
                Severip = sr.ReadLine().ToString();
                sr.Close();
            }
            else
            {
                MessageBox.Show("读取服务器IP发生错误，请联系管理员。");
            }

            String Strconn = "Server=" + Severip +";uid=root;pwd=123456;database=test;CharSet=utf8"; //链接数据库
            MySqlConnection conn = new MySqlConnection(Strconn);
            conn.Open();//打开数据库
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from test.user where user=" + '"' + username + '"';
            MySqlDataReader reader = cmd.ExecuteReader();//从数据库中读取数据流存入reader中
            if (reader.Read())
            {
                department = reader.GetString(reader.GetOrdinal("department"));
                reader.Close();
                reader.Dispose();
            }
            else
            {
                MessageBox.Show("请联系管理员");
            }

            cmd.CommandText = "select * from test.mac_cid where mac=" + '"' + MacAddress + '"'; //创建查询语句
            //从数据库中读取数据流存入reader中  
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                cid = reader.GetInt32(reader.GetOrdinal("cid"));
                computernum = cid;
                int ccid = cid;
                Mac.Text = ccid.ToString();
                reader.Dispose();
                cmd.CommandText = "select * from test.user_LoginInfo where cid=" + '"' + cid + '"';
                reader = cmd.ExecuteReader();
             if (reader.Read())
                {
                    reader.Dispose();
                    cmd.CommandText = "update test.user_LoginInfo set isOnline=" + '"' + "1" + '"' + ",ip=" + '"' + IpAddress + '"' + ",department=" + '"' + department + '"' + ",userName=" + '"' + userName + '"' + "where cid=" + '"' + cid + '"';
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "select * from test.user where user=" + '"' + username + '"';
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    int isRegist = reader.GetInt32(reader.GetOrdinal("isRegist"));
                    if(isRegist ==0)
                    {
                        reader.Dispose();
                        cmd.CommandText = "update test.user set isRegist=" + '"' + "1" + '"' + "where user=" + '"' + username + '"';
                        cmd.ExecuteNonQuery();
                    }
                    
                 }
             else

                {
                    reader.Dispose();
                    cmd.CommandText = "insert into test.user_LoginInfo values(" + cid + "," + '"' + IpAddress + '"' + "," + '"' + department + '"' + "," + '"' + username + '"' + "," + "1" + ")";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "update test.user set isRegist=" + '"' + "1" + '"' + "where user=" + '"' + username + '"';
                    cmd.ExecuteNonQuery();
                }
                
            }
            else
            {
                reader.Dispose();
                cmd.CommandText = "insert into test.mac_cid values(" + '"' + MacAddress + '"' + ",null)";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "select * from test.mac_cid where mac=" + '"' + MacAddress + '"'; //创建查询语句
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    cid = reader.GetInt32(reader.GetOrdinal("cid"));
                    Mac.Text = cid.ToString();
                    reader.Close();
                    reader.Dispose();

                }
                else
                {
                    reader.Close();
                    reader.Dispose();
                    MessageBox.Show("请联系管理员");
                }
                cmd.CommandText = "insert into test.user_LoginInfo values(" + cid + "," + '"' + IpAddress + '"' + "," + '"' + department + '"' + "," + '"' + username + '"' + "," + "1" + ")";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "update test.user set isRegist=" + '"' + "1" + '"' + "where user=" + '"' + username + '"';
                cmd.ExecuteNonQuery();
            }
            conn.Close();
            Thread thread1;
            thread1 = new Thread(new ThreadStart(Httpstart));
            thread1.IsBackground = true;
            thread1.Start();//网页访问记录开启
            Spy.background.Socketmain socketmain = new Spy.background.Socketmain();
            socketmain.socket();//sockrt功能开启
            
             
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult r = MessageBox.Show("确定要退出程序？","提示",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
                if(r == DialogResult.OK)
                {
                    e.Cancel = false;
                    Application.Exit();
                }
                else 
                {
                    e.Cancel = true;
                }
            }
        }


        public void Httpstart()
        {
            timer1.Start();
            string exepath = Environment.CurrentDirectory;
            if (Directory.Exists(exepath + @"\net\") == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(exepath + @"\net\");
            }
            if (Directory.Exists(exepath + @"\net\upload\") == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(exepath + @"\net\upload\");
            }
            m_Sniffer = new SnifferSocket();
            m_Sniffer.TcpPacketReceived += new TcpPacketCallback(m_Sniffer_TcpPacketReceived);
            IPAddress[] addressList = Dns.GetHostAddresses(Dns.GetHostName());
            if (addressList.Length != 0)
            {
                foreach (IPAddress ip in addressList)
                {
                    if (ip.ToString().Split('.').Length == 4) m_Sniffer.Sniff(ip.ToString());
                }
            }
        }
        void m_Sniffer_TcpPacketReceived(TcpPacket packet)
        {
            string data = Encoding.ASCII.GetString(packet.Data);
            if (data.StartsWith("GET "))
            {
                HttpSniffer.HttpPacket sn = new HttpSniffer.HttpPacket();
                sn.ParseRequest(data);
                string exepath = Environment.CurrentDirectory;
                Main main = new Main();
                StreamWriter nettxt = new StreamWriter(exepath + @"\net\httphistory" + nettxtnum + ".txt", true);
                nettxt.WriteLine(DateTime.Now.ToString("yyyy-M-d@HH.mm.ss") + " % " + sn.Host);
                nettxt.Close();
            }
            else if (data.StartsWith("POST "))
            {
                HttpSniffer.HttpPacket sn = new HttpSniffer.HttpPacket();
                sn.ParseRequest(data);
                string exepath = Environment.CurrentDirectory;
                Main main = new Main();
                StreamWriter nettxt = new StreamWriter(exepath + @"\net\httphistory" + nettxtnum + ".txt", true);
                nettxt.WriteLine(DateTime.Now.ToString("yyyy-M-d@HH.mm.ss") + " % " + sn.Host);
                nettxt.Close();
            }
            
        }
        void netupload()
        {
            List<string> list = new List<string>();
            string exepath = Environment.CurrentDirectory;
            Main main = new Main();
            if (System.IO.File.Exists(exepath + @"\net\httphistory" + (nettxtnum - 1) + ".txt") == true)
            {
                using (StreamReader sr = File.OpenText(exepath + @"\net\httphistory" + (nettxtnum - 1) + ".txt"))
                {
                    while (!sr.EndOfStream) //读到结尾退出
                    {
                        string temp = sr.ReadLine();

                        if (!list.Contains(temp))  //去除重复的行
                        {
                            list.Add(temp);
                        }
                    }

                }
                using (StreamWriter sw = new StreamWriter(exepath + @"\net\upload\upload1.txt", true))
                {
                    foreach (string line in list)
                    {
                        sw.WriteLine(line);
                    }
                }
            }
        }







        private void 主页ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (network == null || network.IsDisposed)
            {
                network = new Network();
                network.MdiParent = this;
                network.FormBorderStyle = FormBorderStyle.None;
                network.Dock = DockStyle.Fill;
                network.Show();
            }
            else 
            {
                network.Activate();
            }
            
        }

        private void 反馈ToolStripMenuItem_Click(object sender, EventArgs e)
        {
          /* FeedbackPage.getInstance().Show();
           FeedbackPage.getInstance().BringToFront(); */
            new FeedbackPage().Show();
        }

        private void 任务表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Worktable().Show();
        }

        private void 截图保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (printscreen == null || printscreen.IsDisposed)
            {
                printscreen = new PrintScreen1();
                printscreen.MdiParent = this;
                printscreen.FormBorderStyle = FormBorderStyle.None;
                printscreen.Dock = DockStyle.Fill;
                printscreen.Show();
            }
            else
            {
                printscreen.Activate();
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Thread thread1;
            thread1 = new Thread(new ThreadStart(PrintScreen.PrintS));
            thread1.IsBackground = true;
            thread1.Start();
            //PrintScreen.PrintS();//定时自动截图功能
        }

       

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            //更新状态
            MySqlConnection mycon = db.getMySqlCon();
            mycon.Open();
            db.updateIsOnline(mycon, 0, this.username);
            mycon.Close();
        }

        private void timerSocket_Tick(object sender, EventArgs e)
        {
            if(Spy.background.Socketmain.flag==1)
            {
                //new FeedbackPage().Show();
                Spy.background.Socketmain.flag = 0;
            }

            if (Spy.background.Socketmain.flag == 2)
            {
                Push push = new Push();
                push.frmTips(200,Spy.background.Socketmain.result2,"通知",true);
                push.Show();
                Spy.background.Socketmain.flag = 0;
            }
            if(Spy.background.Socketmain.flag == 3)
            {
                MySqlConnection mycon = db.getMySqlCon();
                mycon.Open();
                db.updateIsOnline(mycon, 0, this.username);
                mycon.Close();
                Spy.background.Control.shutdown();
                Spy.background.Socketmain.flag = 0;
            }
            if(Spy.background.Socketmain.flag == 4)
            {
                MySqlConnection mycon = db.getMySqlCon();
                mycon.Open();
                db.updateIsOnline(mycon, 0, this.username);
                mycon.Close();
                Spy.background.Control.lockin();
                Spy.background.Socketmain.flag = 0;
            }
        }

        private void timerprocss_Tick(object sender, EventArgs e)
        {
            Thread thread1;
            thread1 = new Thread(new ThreadStart(Spy.background.process.getprocess));
            thread1.IsBackground = true;
            thread1.Start();
            //Spy.background.process.getprocess();
        }

        private void timerhttp_Tick(object sender, EventArgs e)
        {
            Thread thread1;
            thread1 = new Thread(new ThreadStart(Httpup));
            thread1.IsBackground = true;
            thread1.Start();
            
        }

        void Httpup()
        {
            string exepath = Environment.CurrentDirectory;
            string format = Spy.Main.user + DateTime.Now.ToString("yyyy-M-d");
            var upload = new Spy.background.SFTPOperation(Severip, "22", "root", "Zhang7890078");
            if (System.IO.File.Exists(exepath + @"\net\httphistory" + nettxtnum + ".txt") == true)
            {
                nettxtnum++;
            }
            try
            {
                upload.Get("/root/netrecord/" + format + ".txt", exepath + @"\net\upload\upload1.txt");
            }
            catch
            {

            }
            try
            {
                netupload();
            }
            catch
            {
                nettxtnum--;
            }
            try
            {
                upload.Put(exepath + @"\net\upload\upload1.txt", "/root/netrecord/" + format + ".txt");
                if (flag == 0)
                {
                    String Strconn = "Server=" + Severip + ";uid=root;pwd=123456;database=test;CharSet=utf8"; //链接数据库
                    MySqlConnection conn = new MySqlConnection(Strconn);
                    conn.Open();//打开数据库  
                    MySqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "select * from test.net_record where userName=" + '"' + Spy.Main.user + '"' + "and Date=" + '"' + DateTime.Now.ToString("yyyy-MM-dd") + '"'; //创建查询语句
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        conn.Close();
                        return;
                    }
                    else
                    {
                        string url = format + ".txt";
                        reader.Dispose();
                        cmd.CommandText = "insert into test.net_record values(null," + '"' + Spy.Main.user + '"' + "," + '"' + DateTime.Now.ToString("yyyy-MM-dd") + '"' + "," + '"' + url + '"' + ")";
                        cmd.ExecuteNonQuery();

                    }
                    flag = 1;
                    conn.Close();
                }
            }
            catch
            {
                nettxtnum--;
                MessageBox.Show("网络不稳定,上传失败，请联系管理员处理。");
            }
            
        }

        private void statusStrip1_SizeChanged(object sender, EventArgs e)
        {

        }


        private void Main_SizeChanged(object sender, EventArgs e)
        {
            {
                //判断是否选择的是最小化按钮 
                if (WindowState == FormWindowState.Minimized)
                {
                    
                    //隐藏任务栏区图标 
                    this.ShowInTaskbar = false;
                    //图标显示在托盘区 
                    notifyIcon1.Visible = true;
                }
            }

        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            //判断是否已经最小化于托盘 
            if (WindowState == FormWindowState.Minimized)
            {
                //还原窗体显示 
                WindowState = FormWindowState.Normal;
                //激活窗体并给予它焦点 
                this.Activate();
                //任务栏区显示图标 
                this.ShowInTaskbar = true;
                //托盘区图标隐藏 
                notifyIcon1.Visible = false;
            }

        }


        }

   
    }
