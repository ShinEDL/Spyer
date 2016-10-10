using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Diagnostics;
using System.IO;

namespace Spy.background
{
    class process
    {



        static public void getprocess()
        {
            string name;
            string str1, str2;
            int flag=0;
            DateTime startTime;
            string exepath = Environment.CurrentDirectory;
            if (Directory.Exists(exepath + @"\pro\") == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(exepath + @"\pro\");
            }
            if (!File.Exists(exepath + @"\pro\" + "prototal.txt"))
            {
                FileStream fs = new FileStream(exepath + @"\pro\" + "prototal.txt", FileMode.Create);
                fs.Close();
            }
            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {

                try
                {
                    flag = 0;
                    name = process.ProcessName;
                    startTime = process.StartTime;
                    if (name != "notepad" && name != "dllhost")
                    {
                        str1 = startTime.ToString() + " % " + name.ToString();
                        StreamReader totaltxt = new StreamReader(exepath + @"\pro\" + "prototal.txt", true);
                        while (!totaltxt.EndOfStream)
                        {
                            str2 = totaltxt.ReadLine();
                            if (str1 == str2)
                            {
                                flag = 1;
                                break;
                            }
                        }
                        totaltxt.Close();
                        if (flag == 0)
                        {
                            StreamWriter writertxt = new StreamWriter(exepath + @"\pro\" + "prototal.txt", true);
                            writertxt.WriteLine(str1);
                            writertxt.Close();
                        }

                    }
                }
                catch
                {
                    
                }
            }
        }

       
    }

    

}
