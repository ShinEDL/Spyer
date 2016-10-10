using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spy.util
{
    class RemoteScreen
    {
        /// <summary>
        /// 根据指定大小获取截图
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Bitmap BySizeGetScreen(int width, int height)
        {
            try
            {
                int thumbWidth = width;
                System.Drawing.Image image = RemoteScreen.GetFullScreen(); //利用Image对象装载源图像
                //接着创建一个System.Drawing.Bitmap对象，并设置你希望的缩略图的宽度和高度。
                Screen s = Screen.PrimaryScreen;
                Rectangle r = s.Bounds;
                int srcWidth = r.Width;
                int srcHeight = r.Height;
                int thumbHeight = (srcHeight / srcWidth) * thumbWidth;
                if (thumbHeight == 0)
                    thumbHeight = 300;
                Bitmap bmp = new Bitmap(width, height);
                //从Bitmap创建一个System.Drawing.Graphics对象，用来绘制高质量的缩小图。
                System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(bmp);
                //设置 System.Drawing.Graphics对象的SmoothingMode属性为HighQuality
                gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //下面这个也设成高质量
                gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                //下面这个设成High
                gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                //把原始图像绘制成上面所设置宽高的缩小图
                System.Drawing.Rectangle rectDestination = new System.Drawing.Rectangle(0, 0, thumbWidth, thumbHeight);
                gr.DrawImage(image, rectDestination, 0, 0, srcWidth, srcHeight, GraphicsUnit.Pixel);

                //保存图像，大功告成！
                //bmp.Save("C:\\1.jpg");
                //最后别忘了释放资源（译者PS：C#可以自动回收吧)
                //bmp.Dispose();
                //image.Dispose();
                return bmp;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Bitmap BySizeGetScreen(Image _image, int width, int height)
        {
            try
            {
                int thumbWidth = width;
                System.Drawing.Image image = _image; //利用Image对象装载源图像
                //接着创建一个System.Drawing.Bitmap对象，并设置你希望的缩略图的宽度和高度。
                Screen s = Screen.PrimaryScreen;
                Rectangle r = s.Bounds;
                int srcWidth = _image.Width;
                int srcHeight = _image.Height;
                //int thumbHeight = (srcHeight / srcWidth) * thumbWidth;
                //if (thumbHeight == 0)
                //    thumbHeight = 300;
                int thumbHeight = height;
                Bitmap bmp = new Bitmap(width, height);
                //从Bitmap创建一个System.Drawing.Graphics对象，用来绘制高质量的缩小图。
                System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(bmp);
                //设置 System.Drawing.Graphics对象的SmoothingMode属性为HighQuality
                gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //下面这个也设成高质量
                gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                //下面这个设成High
                gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                //把原始图像绘制成上面所设置宽高的缩小图
                System.Drawing.Rectangle rectDestination = new System.Drawing.Rectangle(0, 0, thumbWidth, thumbHeight);
                gr.DrawImage(image, rectDestination, 0, 0, srcWidth, srcHeight, GraphicsUnit.Pixel);

                //保存图像，大功告成！
                //bmp.Save("C:\\1.jpg");
                //最后别忘了释放资源（译者PS：C#可以自动回收吧)
                //bmp.Dispose();
                //image.Dispose();
                return bmp;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 全屏截图
        /// </summary>
        /// <returns></returns>
        public static Image GetFullScreen()
        {
            //开始时窗体最小化，截屏后再还原
            Screen s = Screen.PrimaryScreen;
            Rectangle r = s.Bounds;
            int iWidth = r.Width;
            int iHeight = r.Height;
            //创建一个和屏幕一样大的bitmap
            Image img = new Bitmap(iWidth, iHeight);
            try
            {
                //从一个继承自image类的对象中创建Graphics对象
                Graphics g = Graphics.FromImage(img);
                //抓取全屏幕
                g.CopyFromScreen(new Point(0, 0), new Point(0, 0), new Size(iWidth, iHeight));
                //img.Save("C:\\1.jpeg");
            }
            catch (Exception)
            {
            }
            return img;
        }

        /// <summary>
        /// 根据指针截图
        /// </summary>
        /// <returns></returns>
        public static Image GetAppointScreen()
        {
            Image myImage = new Bitmap(300, 200);
            try
            {
                Graphics g1 = Graphics.FromImage(myImage);
                g1.CopyFromScreen(new Point(Cursor.Position.X - 150, Cursor.Position.Y - 25), new Point(0, 0), new Size(300, 200));
                IntPtr dc1 = g1.GetHdc();
                g1.ReleaseHdc(dc1);
            }
            catch (Exception)
            {

                throw;
            }
            return myImage;
        }

        /// <summary>
        /// 将指定的 Image 内容写入到数组
        /// </summary>
        /// <param name="img">指定的 Image</param>
        /// <param name="imgFormat">指定的 ImageFormat</param>
        public static byte[] ToByteArray(Image img, ImageFormat imgFormat)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                img.Save(ms, imgFormat);
                return ms.ToArray();
            }
            catch (Exception)
            {
                return new byte[1];
            }
        }

        /// <summary>
        /// 将指定的数组内容写入到 Image
        /// </summary>
        /// <param name="byteArray">指定的数组</param>
        /// <returns></returns>
        public static Image ToImage(byte[] byteArray)
        {
            try
            {
                MemoryStream ms = new MemoryStream(byteArray);
                return Image.FromStream(ms);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
