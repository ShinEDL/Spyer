using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;

namespace Spy.UI
{   
    
    
    public partial class FeedbackPage : Form
    {   
        public int fid;
        string pic1, pic2, pic3, pic4;
        public static FeedbackPage feedbackpage;
        public FeedbackPage()
        {
            InitializeComponent();
        }

        public static FeedbackPage getInstance()
        {
            if (feedbackpage == null || feedbackpage.IsDisposed)
            {
                feedbackpage = new FeedbackPage();
                feedbackpage.FormBorderStyle = FormBorderStyle.None;
                feedbackpage.Dock = DockStyle.Fill;
                feedbackpage.MdiParent = Main.ActiveForm;
            }
            return feedbackpage;
        }
        private void check()
        {
            if (pictureBox1.Image != null)
            {
                button_de1.Visible = true;
            }
            else
            {
                button_de1.Visible = false;
            }
            if (pictureBox2.Image != null)
            {
                button_de2.Visible = true;
            }
            else
            {
                button_de2.Visible = false;
            }
            if (pictureBox3.Image != null)
            {
                button_de3.Visible = true;
            }
            else
            {
                button_de3.Visible = false;
            }
            if (pictureBox4.Image != null)
            {
                button_de4.Visible = true;
            }
            else
            {
                button_de4.Visible = false;
            }

        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                if (DialogResult.OK == openFileDialog1.ShowDialog())
                {
                    pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                    pic1 = openFileDialog1.FileName;
                 }
                check();
                return;
            }
            if (pictureBox2.Image == null)
            {
                if (DialogResult.OK == openFileDialog1.ShowDialog())
                {
                    pictureBox2.Image = Image.FromFile(openFileDialog1.FileName);
                    pic2 = openFileDialog1.FileName;
                }
                check();
                return;
            }
            if (pictureBox3.Image == null)
            {
                if (DialogResult.OK == openFileDialog1.ShowDialog())
                {
                    pictureBox3.Image = Image.FromFile(openFileDialog1.FileName);
                    pic3 = openFileDialog1.FileName;
                }
                check();
                return;
            }
            if (pictureBox4.Image == null)
            {
                if (DialogResult.OK == openFileDialog1.ShowDialog())
                {
                    pictureBox4.Image = Image.FromFile(openFileDialog1.FileName);
                    pic4 = openFileDialog1.FileName;
                }
                check();
                return;
            }
            MessageBox.Show("无法继续增加图片");
        }

        private void button_de1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image.Dispose();
            pictureBox1.Image = null;
            pic1 = null;
            if (pictureBox2.Image != null)
            {
                pictureBox1.Image = Image.FromFile(pic2);
                pic1 = pic2;
                button_de2_Click(null, null);
                return;
            }
            check();
        }

        private void button_de2_Click(object sender, EventArgs e)
        {
            pictureBox2.Image.Dispose();
            pictureBox2.Image = null;
            pic2 = null;
            if (pictureBox3.Image != null)
            {
                pictureBox2.Image = Image.FromFile(pic3);
                pic2 = pic3;
                button_de3_Click(null, null);
                return;
            }
            check();
        }

        private void button_de3_Click(object sender, EventArgs e)
        {
            pictureBox3.Image.Dispose();
            pictureBox3.Image = null;
            pic3 = null;
            if (pictureBox4.Image != null)
            {
                pictureBox3.Image = Image.FromFile(pic4);
                pic3 = pic4;
                button_de4_Click(null, null);
                return;
            }
            check();
        }

        private void button_de4_Click(object sender, EventArgs e)
        {
            pictureBox4.Image.Dispose();
            pictureBox4.Image = null;
            pic4 = null;
            check();
        }

        private void ban()
        {
            buttonfast.Enabled = false;
            timer1.Start();
        }
        private void enban(object sender, EventArgs e)
        {
            buttonfast.Enabled = true;
            timer1.Dispose();
        }


        private void buttonfast_Click(object sender, EventArgs e)
        {
            ban();
            if (pictureBox4.Image != null)
            {
                MessageBox.Show("无法继续增加图片");
            } 

            string fastpath;
            Bitmap image = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics imgGraphics = Graphics.FromImage(image);
            imgGraphics.CopyFromScreen(0, 0, 0, 0, new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height));
            string exepath = Environment.CurrentDirectory;
            if (System.IO.File.Exists(exepath + @"\Text1.txt") == true)
            {
                StreamReader sr = File.OpenText(exepath + @"\Text1.txt");
                string path = sr.ReadLine();
                if (Directory.Exists(path) == false)//如果不存在就创建file文件夹
                {
                    Directory.CreateDirectory(path);
                }
                fastpath = path + DateTime.Now.ToString("yyyy-M-d@HH.mm.ss") + ".jpg";
                image.Save(fastpath, System.Drawing.Imaging.ImageFormat.Jpeg); //保存
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
                fastpath = exepath + @"\PrintScreen\" + DateTime.Now.ToString("yyyy-M-d@HH.mm.ss") + ".jpg";
                image.Save(fastpath, System.Drawing.Imaging.ImageFormat.Jpeg); //保存

            }
            //System.Threading.Thread.Sleep(1000);//防止连按冲突
            if (pictureBox1.Image == null)
            {
                pictureBox1.Image = Image.FromFile(fastpath);
                pic1 = fastpath;
                check();
                return;
            }
            if (pictureBox2.Image == null)
            {
                pictureBox2.Image = Image.FromFile(fastpath);
                pic2 = fastpath;
                check();
                return;
            }
            if (pictureBox3.Image == null)
            {
                pictureBox3.Image = Image.FromFile(fastpath);
                pic3 = fastpath;
                check();
                return;
            }
            if (pictureBox4.Image == null)
            {
                pictureBox4.Image = Image.FromFile(fastpath);
                pic4 = fastpath;
                check();
                return;
            }
        }

        private void buttonsubmit_Click(object sender, EventArgs e)
        {
            
            Warn warn = new Warn();
            warn.Warnmessage = "上传中 请稍等";
            warn.Show();
            System.Threading.Thread.Sleep(200);
            try
            {
                if (textBox1.Text.Trim() != String.Empty)
                {
                    String Strconn = "Server=" + Spy.Main.Severip + ";uid=root;pwd=123456;database=test;CharSet=utf8"; //链接数据库
                    MySqlConnection conn = new MySqlConnection(Strconn);
                    conn.Open();//打开数据库  
                    MySqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "select * from test.feedback order by fid desc limit 1";
                    MySqlDataReader reader = cmd.ExecuteReader();//从数据库中读取数据流存入reader中  
                    if (reader.Read())
                    {
                        fid = reader.GetInt32(reader.GetOrdinal("fid")) + 1;
                        reader.Close();
                        //MessageBox.Show(fid.ToString());
                    }
                    else
                    {
                        fid = 1;
                        // MessageBox.Show(fid.ToString());
                    }
                    conn.Close();//获取fid
                    string format;
                    format = Spy.Main.user + DateTime.Now.ToString("yyyy-M-d") + "@" + fid;
                    //MessageBox.Show(format);
                    string exepath = Environment.CurrentDirectory;
                    string str;
                    string upload_p1 = null, upload_p2 = null, upload_p3 = null, upload_p4 = null, upload_txt = null;
                    str = textBox1.Text;
                    if (Directory.Exists(exepath + @"\feedbacktxt\") == false)//如果不存在就创建feedbacktxt文件夹
                    {
                        Directory.CreateDirectory(exepath + @"\feedbacktxt\");
                    }
                    StreamWriter feedbacktxt = new StreamWriter(exepath + @"\feedbacktxt\" + format + ".txt", false);//存在改写 不存在创建
                    feedbacktxt.WriteLine(str);//写入
                    feedbacktxt.Close();
                    var upload = new Spy.background.SFTPOperation(Spy.Main.Severip, "22", "root", "Zhang7890078");
                    upload.Put(exepath + @"\feedbacktxt\" + format + ".txt", "/root/txt/" + format + ".txt");
                    upload_txt = format + ".txt";//txt上传

                    if (pic1 != null)
                    {
                        upload.Put(pic1, "/root/feedbackimg/" + format + "@p1.jpg");
                        upload_p1 = format + "@p1.jpg;";
                    }
                    if (pic2 != null)
                    {
                        upload.Put(pic2, "/root/feedbackimg/" + format + "@p2.jpg");
                        upload_p2 = format + "@p2.jpg;";
                    }
                    if (pic3 != null)
                    {
                        upload.Put(pic3, "/root/feedbackimg/" + format + "@p3.jpg");
                        upload_p3 = format + "@p3.jpg;";
                    }
                    if (pic4 != null)
                    {
                        upload.Put(pic4, "/root/feedbackimg/" + format + "@p4.jpg");
                        upload_p4 = format + "@p4.jpg;";
                    }//4张图片上传

                    conn.Open();
                    cmd.CommandText = "insert into test.feedback values(null," + '"' + upload_p1 + upload_p2 + upload_p3 + upload_p4 + '"' + "," + '"' + upload_txt + '"' + "," + Spy.Main.computernum + "," + '"' + Spy.Main.user + '"' + "," + '"' + "未阅读" + '"' + ")";
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    warn.Close();
                    MessageBox.Show("上传成功 可关闭页面");
                }
                else
                {
                    warn.Close();
                    MessageBox.Show("请填写文字内容后,再次上传。");
                }
                //var upload = new Spy.background.SFTPOperation("120.27.47.166", "22", "root", "Zhang7890078");
                //upload.Put(pic1,"/root/img/1.jpg");


                //上传格式 获取fid   
                //txt格式 user2016-4-1.fid  图片格式 user2016-4-1.fid.p1 
                //定时截图格式 user2016-4-1@10.00.00
            }
            catch
            {
                warn.Close();
                MessageBox.Show("网络出现问题，提交失败！！请联系管理员。");
            }

        }

       

        
    }
}
