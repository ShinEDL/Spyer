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
 * 删除违规记录 窗体
 */ 
namespace Spy.UI
{
    public partial class ReduceViolation : Form
    {
        private DB db;
        private UserInfo userInfo;
        public ReduceViolation()
        {
            InitializeComponent();
            db = new DB();
            userInfo = db.getUserInfo(Cat.userName.ToString(), Cat.Department.ToString());
        }

        //载入violation表相应内容
        private void ReduceViolation_Load(object sender, EventArgs e)
        {
            List<string> list = db.getViolationById(userInfo.id);
            foreach (string item in list)
            {
                listBox1.Items.Add(item);
            }
        }

        //删除按钮
        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                Console.WriteLine(listBox1.SelectedItem.ToString());

                if (db.updateViolation(userInfo.id, listBox1.SelectedItem.ToString(), false))
                {
                    listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                    MessageBox.Show("删除成功！");
                }
                else
                {
                    MessageBox.Show("删除失败！");
                }
            }  
            
        }
    }
}
