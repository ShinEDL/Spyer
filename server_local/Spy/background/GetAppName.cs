/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Spy.util;
using System.Runtime.InteropServices;
using System.IO;


namespace Spy.background
{
    /*
     * 开启一个线程循环检测是否有新程序打开，方法是与之前的应用程序列表对比，查看是否有新增
     *的应用程序
     *
     */
/*
    class GetAppName
    {
        private SystemInfo sysInfo;
        private List<string> str1;
        private List<string> str2;
        private DateTime dt;
        private bool flag1,flag2;
        private FileStream file;
        private StreamWriter writer;

        public GetAppName()
        {
            str1 = new List<string>();
            str2 = new List<string>();
            sysInfo = new SystemInfo();
            
            file = new FileStream("D:\\test1.txt",FileMode.OpenOrCreate);
            writer = new StreamWriter(file);
            writer.AutoFlush = true;
            flag1 = true;
            flag2 = false;
        }
        public void run()
        {
            str1 = sysInfo.getName();
            
            try
            {
                while (flag1)
                {
                    dt = DateTime.Now;
                    str2 = sysInfo.getName();
                    compare(str1, str2, dt);
                    Thread.Sleep(1000);
                }
            }
            catch
            {
                writer.Close();
                file.Close(); //发生异常，将文件流关闭
            }
            
        }

        //对比是否有打开新的应用程序
        public void compare(List<string> a ,List<string> b,DateTime dt)
        {
            string time;
            string text;
            
            foreach(string str in b)
            {
                if(!a.Contains(str))
                {
                    //如果str不存在与之前的应用列表中，则更新a并且将新打开的应用程序写入日志
                    time = dt.ToString("f");
                    text = str + "---" + time;
                    writer.WriteLine(text);
                    //writer.Flush();

                }             
            }
            
            str1 = str2;
        }

    }
}
*/