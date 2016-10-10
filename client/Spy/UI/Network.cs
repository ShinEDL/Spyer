using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Echevil;
using System.Net;
using System.Net.Sockets;
using System.Threading; 

namespace Spy.UI
{
    
    public partial class Network : Form
    {
        private static Network network;
        public string up;
        public string down;
        public Network()
        {
            InitializeComponent();
        }
        
        private void label1_Click(object sender, EventArgs e)
        {

        }

        public double totalup=0;
       // public double totalupA = 0;
        public double totaldown=0;
       // public double totaldownA = 0;

        private NetworkAdapter[] adapters;
        private NetworkMonitor monitor;
        private void Network_Load_1(object sender, EventArgs e)
        {
            monitor = new NetworkMonitor();
            this.adapters = monitor.Adapters;

            if (adapters.Length == 0)
            {
                this.ListAdapters.Enabled = false;
                Message message = new Message();
                message.flag = 2;
                message.Show();
                return;
            }

           this.ListAdapters.Items.AddRange(this.adapters);
           //this.ListAdapters.SelectedIndex = 1;
           int flag = 1;
           for (int i = 0; i < this.ListAdapters.Items.Count; i++)
           {
               this.ListAdapters.SelectedIndex = i;
               NetworkAdapter adapter = this.adapters[this.ListAdapters.SelectedIndex];
               System.Threading.Thread.Sleep(2000);
               if (adapter.DownloadSpeedKbps!=0)
               {
                   flag = 0;
                   break;
               }
           }
           if (flag == 1)
           {
               Message message = new Message();
               message.flag = 1;
               message.Show();

           }
          

        }

        private void ListAdapters_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            monitor.StopMonitoring();
            monitor.StartMonitoring(adapters[this.ListAdapters.SelectedIndex]);
            this.timer2.Start();
        }

        private void TimerCounter_Tick(object sender, System.EventArgs e)
        {
            NetworkAdapter adapter = this.adapters[this.ListAdapters.SelectedIndex];
            if (adapter.DownloadSpeedKbps > 1024)
            {
                this.label_download.Text = String.Format("{0:n} Mbps", adapter.DownloadSpeedKbps/1024);
                total_down();
                down = String.Format("{0:n} kbps", adapter.DownloadSpeedKbps);

            }
            else
            { 
                this.label_download.Text = String.Format("{0:n} kbps", adapter.DownloadSpeedKbps);
                total_down();
                down = String.Format("{0:n} kbps", adapter.DownloadSpeedKbps);

            }

            if (adapter.UploadSpeedKbps > 1024)
            {
                this.label_upload.Text = String.Format("{0:n} Mbps", adapter.UploadSpeedKbps/1024);
                total_up();
                up = String.Format("{0:n} kbps", adapter.UploadSpeedKbps);
            }
            else
            {
                this.label_upload.Text = String.Format("{0:n} kbps", adapter.UploadSpeedKbps);
                total_up();
                up = String.Format("{0:n} kbps", adapter.UploadSpeedKbps);
            }
        }
        private void total_down()
        {   
            
            NetworkAdapter adapter = this.adapters[this.ListAdapters.SelectedIndex];
            totaldown = totaldown + adapter.DownloadSpeedKbps;
            this.label_totaldown.Text = String.Format("{0:n} Mbps", totaldown / 1024);
        }
        private void total_up()
        {   
           
            NetworkAdapter adapter = this.adapters[this.ListAdapters.SelectedIndex];
            totalup = totalup + adapter.UploadSpeedKbps;
            this.label_totalup.Text = String.Format("{0:n} Mbps", totalup / 1024);
        }

        /*
        public void socket2001()
        {

            int port = 2001;
            //string host = "127.0.0.1";
            string host = Main.MYIP;
            //创建终结点

            IPAddress ip = IPAddress.Parse(host);

            IPEndPoint ipe = new IPEndPoint(ip, port);

            

                //创建Socket并开始监听

            while (true)
            {
                    try
                    {
                    Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); //创建一个Socket对象，如果用UDP协议，则要用SocketTyype.Dgram类型的套接字
                    s.Bind(ipe);    //绑定EndPoint对象(2000端口和ip地址)
                    s.Listen(0);    //开始监听



                    Console.WriteLine("等待客户端连接2001");



                    //接受到Client连接，为此连接建立新的Socket，并接受消息

                    Socket temp = s.Accept();   //为新建立的连接创建新的Socket


                    Console.WriteLine("建立连接2001");

                    //string recvStr = "";

                    byte[] recvBytes = new byte[1024];

                    //int bytes;

                    string sendStr = "四哥好！";
                    byte[] bs = Encoding.BigEndianUnicode.GetBytes(sendStr);
                    try
                    {
                        while (true)
                        {
                            sendStr =  up + "@" + down ;
                            bs = Encoding.BigEndianUnicode.GetBytes(sendStr);
                            temp.Send(bs, bs.Length, 0);
                            Console.WriteLine("'" + sendStr + "'");
                            Thread.Sleep(1000);
                        }
                    }
                    catch
                    {

                        temp.Close();

                        Console.ReadLine();
                        s.Close();
                    }
                    }
             catch { }
            }

        }*/
        
    }
}
