using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Spy.util;
using Spy.Properties;
using MySql.Data.MySqlClient;

/*
 * 选择被监控客户端 窗体
 */ 
namespace Spy.UI
{
    public partial class Selection : Form
    {
        public string username;
        private List<MyListBoxItem> items = null;
        DB db = null;
        public Selection()
        {
            InitializeComponent();
            db = new DB(this);
        }

        private void Selection_Load(object sender, EventArgs e)
        {
            items = db.getComputer();//获取数据库中的在线客机列表
            if (items != null)
            {
                items.Sort(sortMy);//根据部门排序
                for (int i = 0; i < items.Count; i++)
                {
                    mylistBox1.Items.Add(items[i]);
                }
            }
        }

        private void Selection_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            Application.Exit();
        }

        //查看按钮
        private void button1_Click(object sender, EventArgs e)
        {
            if (mylistBox1.SelectedIndex >= 0)
            {
                MyListBoxItem item = (MyListBoxItem)mylistBox1.SelectedItem;
                Cat cat = new Cat(this);
                cat.username = this.username;
                cat.user = item.userName;
                cat.ip = item.ip;
                cat.department = item.department;
                cat.cid = item.id;
                cat.Show();
                this.timer1.Enabled = false;
                this.Hide();
            }
            else
            {
                MessageBox.Show("请选择被监控对象！");
            }
            
        }

        //退出按钮
        private void button2_Click(object sender, EventArgs e)
        {
            
           DialogResult r = MessageBox.Show("确定要退出程序？","提示",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
           if(r == DialogResult.OK)
           {
               this.Dispose();
               Application.Exit();
           }
           else 
           {
               
           }
            
        }


        //
        //List<MyListBoxItem>排序规则
        //使用委托的方式实现
        //
        private int sortMy(MyListBoxItem a, MyListBoxItem b)
        {
            //待定
            return a.department.CompareTo(b.department);//根据部门排序
        }

        //重新载入在线客机
        public void reloadMyListBox()
        {
            items = db.getComputer();//获取数据库中的在线客机列表
            if (items != null)
            {
                items.Sort(sortMy);//根据部门排序
                mylistBox1.Items.Clear();
                for (int i = 0; i < items.Count; i++)
                {
                    mylistBox1.Items.Add(items[i]);
                }
            }
        }

        //刷新按钮
        private void button3_Click(object sender, EventArgs e)
        {
            mylistBox1.Items.Clear();
            items = db.getComputer();//获取数据库中的在线客机列表
            if (items != null)
            {
                items.Sort(sortMy);//根据部门排序
                for (int i = 0; i < items.Count; i++)
                {
                    mylistBox1.Items.Add(items[i]);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            mylistBox1.Items.Clear();
            items = db.getComputer();//获取数据库中的在线客机列表
            if (items != null)
            {
                items.Sort(sortMy);//根据部门排序
                for (int i = 0; i < items.Count; i++)
                {
                    mylistBox1.Items.Add(items[i]);
                }
            }
        }



    }
}
