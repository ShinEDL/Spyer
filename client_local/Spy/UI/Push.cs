using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spy.UI
{
    public partial class Push : Form
    {
        private int screenWidth;//屏幕宽度  
        private int screenHeight;//屏幕高度  
        private bool finished = true;//是否完全显示提示窗口  
        private int tim; //停留时间  
        private string txt; //显示文本  
        private bool flag; //自动关闭  
        private string frmTxt; //标题栏文本 
        public Push()
        {
            InitializeComponent();
        }

        /// <summary>  
        /// 构造方法重构  
        /// </summary>  
        /// <param name="time">提示窗口显示时间 时间过后自动关闭 </param>  
        /// <param name="lableText">提示窗口显示的文本</param>  
        /// <param name="formText">提示窗口标题栏</param>  
        /// <param name="formExit">提示窗口是否自动隐藏 true：自动关闭  false：不关闭</param>  
        public void frmTips(int time, string lableText, string formText, bool formExit)  
        {  
            InitializeComponent();  
  
            tim = time;  
            txt = lableText;  
  
            flag = formExit;  
            frmTxt = formText;  
        }  
  
        /// <summary>  
        private void Push_Load(object sender, EventArgs e)
        {
            this.Text = " " + frmTxt;

            label1.Text = "    " + txt;
            //Console.WriteLine("'" + txt + "'");
            //lableText();
            //fromHeight();

            screenHeight = Screen.PrimaryScreen.Bounds.Height;
            screenWidth = Screen.PrimaryScreen.Bounds.Width;
            Location = new Point(screenWidth - this.Size.Width , screenHeight - 350); //设置初始窗口位置  
            timer1.Start();  
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(tim>0)
            {
                tim = tim - 1;
            }
            else
            {
                this.Close();
            }

        }


        /*private void timer1_Tick(object sender, EventArgs e)
        {
            if (finished)//提示窗口没有完全显示  
            {
                if (Location.Y + Height + 29 >= screenHeight)//如果提示窗口的纵坐标与提示窗口的高度之和大于屏幕高度  
                {
                    Location = new Point(Location.X, Location.Y - 1);
                    //finished = false;
                }
                else
                {
                    if (flag == false) //提示窗口不自动关闭  
                    {
                        timer1.Stop();
                    }
                    else //提示窗口自动关闭  
                    {
                        tim = tim - 1;
                        if (tim <= 0)
                        {
                            finished = false;
                        }
                    }
                }
            }
            else//提示窗口完全显示  
            {
                if (Location.Y < screenHeight)
                {
                    Location = new Point(Location.X, Location.Y + 1);
                }
                else
                {
                    this.Close();
                }
            }
        }*/
        /// <summary>  
        /// 显示文本换行  
        /// </summary>  
        private void lableText()  
        {  
            string txt2 = "";  

            for (int i = 0; i <= txt.Length / 10; i++)  
           {  
                if (i == txt.Length / 10)  
                {  
                    txt2 += txt.Substring(i * 10, txt.Length - (i * 10));  
                }  
                else  
                {  
                    txt2 += txt.Substring(i * 10, 10) + Environment.NewLine;  
                }  
            }

            label1.Text = txt2;  
        }  

        /// <summary>  
        /// 窗体高度控制  
        /// </summary>  
        private void fromHeight()
        {
            if (label1.Height > 60)
            {
                Height = 109 + (label1.Height - 60);
            }
        }  

    }
}
