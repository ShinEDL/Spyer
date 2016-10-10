using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

/*
 * socket连接
 * 建立连接、接收、发送等操作
 */ 
namespace Spy.util
{
    public class SocketService
    {
        private String ip;//客户端ip
        private int port;//客户端的监听端口
        public Socket s;
        public bool isConnectSuccess;//判断是否连接成功
        public SocketService(String IP,int Port)
        {
            ip = IP;
            port = Port;
        }

        //建立socket连接
        public void socketConnect()
        {
            try
            {
                s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(ip), port);//把ip和端口转化为IPEndPoint的实例
                s.Connect(ipe);//建立连接
                Console.WriteLine("Connect Success ! port : "+ port);
                isConnectSuccess = true;
            }
            catch(Exception e)
            {
                Console.WriteLine("Connect Failed ! port : " + port + " Exception : "+ e);
                isConnectSuccess = false;
            }
            
        }

        //关闭Socket
        public void socketClose()
        {
            try
            {
                s.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine("Connection Close Failed ! Exception : " + e);
                isConnectSuccess = false;
            }
        }

        //发送数据
        public void send(String code)
        {
            try
            {
                if (s != null && s.Connected == true)
                {
                    byte[] bs = Encoding.BigEndianUnicode.GetBytes(code);//把字符串编码为字节
                    s.Send(bs, bs.Length, 0);//发送信息
                    this.isConnectSuccess = true;
                }
            }
            catch
            {
                this.isConnectSuccess = false;
            }
        }

        //接受数据
        public String receive()
        {
            try
            {
                if (s != null && s.Connected == true)
                {
                    String recv = "";
                    byte[] recvBytes = new byte[1024];
                    int bytes;
                    bytes = s.Receive(recvBytes, recvBytes.Length, 0);//接收
                    recv += Encoding.BigEndianUnicode.GetString(recvBytes, 0, bytes);
                    this.isConnectSuccess = true;
                    return recv;
                }
            }
            catch
            {
                this.isConnectSuccess = false;
            }
            return null;
        }
        
    }
}
