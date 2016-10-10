using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Spy.util;
using Spy.UI;

/*
 * 登录 窗体
 */ 
namespace Spy
{

    public partial class Login : Form
    {
        public TextBox num = null;
        public TextBox pwd1 = null;
        
        DB db = null;
        string path = Application.ExecutablePath;
        RegistryKey rk = Registry.LocalMachine;//本机配置数据，读取注册表基项
        RegistryKey rk2 = null;
        bool autoRun;
        static MySqlConnection mycon = null;
        public Login()
        {
            InitializeComponent();
            db = new DB(this);
             num = this.Num;
             pwd1 = this.pwd;

            //开机启动注册表引用对象rk2
            //rk2 = rk.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
            //取消自动启动
            //rk2.DeleteValue("AutoRun");
            //rk2.Close();
            //rk.Close();
            //判断是否设置了自动启动
            //autoRun = isSetAutoRun(rk2);
            //if (autoRun == false)
                //设置开机自动启动
                //setAutoRun(rk2);
            //else MessageBox.Show("autoRun");

        }

        //“取消”按钮
        private void Close_Click(object sender, EventArgs e)
        {
            mycon.Close();
            Application.Exit();
        }

        //“登录”按钮，完成用户登录验证行为
        private void OK_Click(object sender, EventArgs e)
        {
            if (mycon != null && mycon.State == ConnectionState.Open)
            {
                switch (db.loginVilify(mycon,this.num.Text.ToString()))//登录验证
                {
                    case -3:
                        MessageBox.Show("登录失败！密码不正确！");
                        break;
                    case -2:
                        MessageBox.Show("该用户非管理员！");
                        break;
                    case -1:
                        MessageBox.Show("登录异常！");
                        break;
                    case 0:
                        MessageBox.Show("登录失败！用户不存在！");
                        break;
                    case 1:
                        Selection select = new Selection();
                        select.username = this.num.Text.ToString();
                        select.Show();
                        mycon.Close();
                        this.Close();
                        break;
                }
            }
            
            
        }
        //设置开机启动的函数
        private Boolean setAutoRun(RegistryKey rk2)
        {
            rk2.SetValue("AutoRun", path);
            rk2.Close();
            rk.Close();
            return true;
        }

        //判断是否已经设置开机启动
        private Boolean isSetAutoRun(RegistryKey rk2)
        {
            if (rk2.GetValue("AutoRun").ToString().ToLower() != "false")
                return true;
            else 
                return false;
        }

        //当窗体第一次出现时，连接数据库
        private void Login_Shown(object sender, EventArgs e)
        {
            mycon = db.getMySqlCon();
            if (mycon != null)
            {
                try
                {
                    mycon.Open();
                    if (mycon.State == ConnectionState.Open)
                    {
                        dbmsg.Text = "连接数据库成功！";
                    }
                }
                catch 
                {
                    Num.Enabled = false;
                    pwd.Enabled = false;
                    dbmsg.Text = "连接数据库失败！";
                    MessageBox.Show("无法连接数据库！");
                }

            }
        }


    }

}
