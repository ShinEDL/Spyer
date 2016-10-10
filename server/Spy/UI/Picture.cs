using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * 查看截图大图 窗体
 */ 
namespace Spy.UI
{
    public partial class Picture : Form
    {
        public int i;
        private Image[] images;
        private Image image;
        public Picture(Report report)
        {
            InitializeComponent();
            images = report.images;
            image = null;
        }

        public Picture(Image img)
        {
            InitializeComponent();
            image = img;
            images = null;
        }

        public Picture()
        {
            // TODO: Complete member initialization
        }

        private void Picture_Load(object sender, EventArgs e)
        {
            if (image == null && images != null && images.Length != 0)
            {
                pictureBox1.Image = images[i];
                this.Text = "截图" + (i + 1);
            }
            if (image != null && images == null)
            {
                pictureBox1.Image = image;
                this.Text = "截图";
                button1.Enabled = false;
                button2.Enabled = false;
            }

            
        }

        //上一张 按钮
        private void button1_Click(object sender, EventArgs e)
        {
            i = i - 1;
            if (i < 0)
            {
                i = 0;
                button1.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
                button2.Enabled = true;
            }
            pictureBox1.Image = images[i];
            this.Text = "截图" + (i + 1);
        }

        //下一张 按钮
        private void button2_Click(object sender, EventArgs e)
        {
            i = i + 1;
            if (i >= images.Length)
            {
                i = images.Length - 1;
                button2.Enabled = false;
            }
            else
            {
                button2.Enabled = true;
                button1.Enabled = true;
            }
            pictureBox1.Image = images[i];
            this.Text = "截图" + (i + 1);
        }



    }
}
