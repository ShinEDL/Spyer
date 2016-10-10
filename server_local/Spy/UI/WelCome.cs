using Spy.util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * 欢迎界面 窗体
 */ 
namespace Spy.UI
{
    public partial class WelCome : Form
    {
        private static WelCome welcome;
        private DB db;
        private SFTP sftp;

        public int progressbar = 0;
        public WelCome()
        {
            InitializeComponent();
            db = new DB();
            sftp = new SFTP();
            sftp.Connect();
        }

        private void WelCome_Load(object sender, EventArgs e)
        {
            MemoryStream ms = null;//内存区域的流，用来读取截图
            try
            {
                UserInfo userInfo = db.getUserInfo(Cat.userName.ToString(), Cat.Department.ToString());
                this.label7.Text = userInfo.user;
                this.label8.Text = userInfo.id.ToString();
                this.label9.Text = userInfo.positions;
                this.label10.Text = userInfo.department;
                this.label11.Text = userInfo.employeement_date;
                this.label17.Text = Cat.Ip;
                this.label18.Text = Cat.computer_id.ToString();
                this.label19.Text = userInfo.violation.ToString();

                progressbar = 50;

                ms = new MemoryStream(sftp.Get("/root/employeePic/" + userInfo.user + ".jpg"));
                this.pictureBox1.Image = Image.FromStream(ms);
                ms.Close();

                progressbar = 100;
            }
            catch
            {
                MessageBox.Show("没有相应员工信息！");
            }
            finally
            {
                ms.Close();
            }
            
        }

        public static WelCome getInstance()
        {
            if (welcome == null || welcome.IsDisposed)
            {
                welcome = new WelCome();
                welcome.FormBorderStyle = FormBorderStyle.None;
                welcome.Dock = DockStyle.Fill;
                welcome.MdiParent = Cat.ActiveForm;
                welcome.WindowState = FormWindowState.Maximized;
            }
            return welcome;
        }

        //查看违规记录
        private void button1_Click(object sender, EventArgs e)
        {
            CatViolation cat = new CatViolation();
            cat.ShowDialog();
        }


    }
}
