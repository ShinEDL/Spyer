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
 * 查看违规记录列表 窗体 
 */
namespace Spy.UI
{
    public partial class CatViolation : Form
    {
        private DB db;
        private UserInfo userInfo;
        public CatViolation()
        {
            InitializeComponent();
            db = new DB();
            userInfo = db.getUserInfo(Cat.userName.ToString(), Cat.Department.ToString());
        }

        //确定 按钮
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //载入violation表相应内容
        private void CatViolation_Load(object sender, EventArgs e)
        {
            List<string> list = db.getViolationById(userInfo.id);
            foreach (string item in list)
            {
                listBox1.Items.Add(item);
            }
        }
    }
}
