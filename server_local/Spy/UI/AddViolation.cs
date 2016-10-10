using Spy.util;
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
 * 增加违规记录 窗体 
 */
namespace Spy.UI
{
    public partial class AddViolation : Form
    {
        DB db;
        UserInfo userInfo;
        public AddViolation()
        {
            InitializeComponent();
            db = new DB();
            userInfo = db.getUserInfo(Cat.userName.ToString(), Cat.Department.ToString());
        }

        //增加违规记录，提交按钮
        private void button1_Click(object sender, EventArgs e)
        {
            if (db.updateViolation(userInfo.id, textBox1.Text.ToString(), true))
            {
                MessageBox.Show("添加成功！");
            }
            else
            {
                MessageBox.Show("添加失败！");
            }
        }
    }
}
