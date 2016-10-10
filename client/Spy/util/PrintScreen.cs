using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Spy.util
{
    class PrintScreen
    {
        static public void PrintS() 
        {       
            Bitmap image = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics imgGraphics = Graphics.FromImage(image);
            imgGraphics.CopyFromScreen(0, 0, 0, 0, new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height)); //设置截屏区域 
            image.Save(@"E:\PrintScreen\" + DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss") + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg); ;//保存
   
        }

    }
    

}
