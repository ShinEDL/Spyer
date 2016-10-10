using PacketDotNet;
using SharpPcap;
using SharpPcap.AirPcap;
using SharpPcap.LibPcap;
using SharpPcap.WinPcap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Spy.background
{
    class WebFilter
    {
        private static Regex httpgetRegex = new Regex(@"GET\s+([^\s\r\n]+)\s+HTTP");
        private static Regex hostRegex = new Regex(@"Host:\s*([^\s\r\n]+)");
        ICaptureDevice[] device;
        CaptureDeviceList devices;

        //开始抓包
        public void start_capture()
        {
            
            int i = 0;

            /* Scan the list printing every entry 扫描列表*/
            foreach (var dev in devices)
            {
                /* Description */
                Console.WriteLine("{0}) {1} {2}", i, dev.Name, dev.Description);
                i++;
            }

            Console.WriteLine();
            Console.Write("-- Please choose a device to capture: ");
            
            //分别启动各个网卡
            for (i = 0; i < devices.Count; i++)
            {
                device[i] = devices[i];

                //注册截获包的处理事件
                // Register our handler function to the 'packet arrival' event
                device[i].OnPacketArrival +=
                    new PacketArrivalEventHandler(device_OnPacketArrival);
                //开启设备以监听
                // Open the device for capturing
                int readTimeoutMilliseconds = 1000;
                if (device[i] is AirPcapDevice)
                {
                    // NOTE: AirPcap devices cannot disable local capture
                    var airPcap = device[i] as AirPcapDevice;
                    airPcap.Open(SharpPcap.WinPcap.OpenFlags.DataTransferUdp, readTimeoutMilliseconds);
                }
                else if (device[i] is WinPcapDevice)
                {
                    var winPcap = device[i] as WinPcapDevice;
                    winPcap.Open(SharpPcap.WinPcap.OpenFlags.DataTransferUdp | SharpPcap.WinPcap.OpenFlags.NoCaptureLocal, readTimeoutMilliseconds);
                }
                else if (device[i] is LibPcapLiveDevice)
                {
                    var livePcapDevice = device[i] as LibPcapLiveDevice;
                    livePcapDevice.Open(DeviceMode.Promiscuous, readTimeoutMilliseconds);
                }
                else
                {
                    throw new System.InvalidOperationException("unknown device type of " + device.GetType().ToString());
                }
                //设置协议过滤
                // tcpdump filter to capture only TCP/IP packets
                string filter = "ip and tcp";
                device[i].Filter = filter;
                //开始监听
                // Start the capturing process
                device[i].StartCapture();
            }
        }

        //停止抓包
        public void stop_capture()
        {
            for(int i=0;i<devices.Count;i++)
            {
                if (device[i] != null)
                {
                     device[i].StopCapture();
                
                     device[i].Close();
                }
            }
        }

        /// <summary>
        /// 截获包的处理事件
        /// Prints the time and length of each received packet
        /// </summary>
        private void device_OnPacketArrival(object sender, CaptureEventArgs e)
        {
            //var time = e.Packet.Timeval.Date;
            //var len = e.Packet.Data.Length;
            //Console.WriteLine("{0}:{1}:{2},{3} Len={4}",
            //    time.Hour, time.Minute, time.Second, time.Millisecond, len);
            //Console.WriteLine(e.Packet.ToString());
            //_logger.Info(e.Packet.ToString());
            //转换为TCP包
            var packet = Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);
            var tcpPacket = TcpPacket.GetEncapsulated(packet);
            //用UTF8编码解析包的内容
            var datastr = Encoding.UTF8.GetString(tcpPacket.PayloadData);

            //输出包的ID信息
            //Console.WriteLine($"{nameof(tcpPacket.AcknowledgmentNumber)}:{tcpPacket.AcknowledgmentNumber}  {nameof(tcpPacket.SequenceNumber)}:{tcpPacket.SequenceNumber}");
            //如果能分析到HTTP报头中的Url，则输出之
            var url = httpgetRegex.Match(datastr);
            if (url.Success)
            {
                var host = hostRegex.Match(datastr);
                if (host.Success)
                {
                    Console.WriteLine(host.Groups[1].Value + url.Groups[1].Value);
                    
                }
            }
        }

    }
}
