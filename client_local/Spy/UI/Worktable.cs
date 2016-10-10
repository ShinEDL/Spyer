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

namespace Spy.UI
{
    public partial class Worktable : Form
    {
        int Pagesize = 0;     //每页显示行数
        int Maxsize = 0;         //总记录数
        int Totalpage = 0;    //页数
        int Pagenum = 0;   //当前页号
        int nCurrent = 0;
        string Sender;
        string Date;

        DataSet ds = new DataSet();
        DataTable worktb = new DataTable();

        public Worktable()
        {
            InitializeComponent();
        }

        private void Worktable_Load(object sender, EventArgs e)
        {

            String Strconn = "Server=120.27.47.166;uid=root;pwd=123456;database=test"; //链接数据库
            MySqlConnection conn = new MySqlConnection(Strconn);
            String CommandText = "select Sender,Date,Title from test.inform_table order by Date desc";  //获取worktable
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(CommandText, conn);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds, "worktable");     //赋值
            worktb = ds.Tables["worktable"];
            InitDataSet();
            conn.Close();
            // dataGridView1.DataSource = ds;
            // dataGridView1.DataMember = "worktable";

        }
        private void InitDataSet()  //分页设置
        {
            Pagesize = 15; //每页行数
            Maxsize = worktb.Rows.Count;
            Totalpage = (Maxsize / Pagesize);    //总页数计算
            if ((Maxsize % Pagesize) > 0) Totalpage++;
            Pagenum = 1;       //当前页数从1开始
            nCurrent = 0;       //当前记录数从0开始

            LoadData();

        }

        private void LoadData()
        {
            int nStartPos = 0;   //当前页面开始记录行
            int nEndPos = 0;     //当前页面结束记录行

            DataTable dtTemp = worktb.Clone();   //克隆DataTable结构框架

            if (Pagenum == Totalpage)
            {
                nEndPos = Maxsize;
            }
            else
            {
                nEndPos = Pagesize * Pagenum;
            }

            nStartPos = nCurrent;

            toolStripLabel1.Text = "/"+Totalpage.ToString();
            toolStripTextBox1.Text = Convert.ToString(Pagenum);

            //从元数据源复制记录行
            for (int i = nStartPos; i < nEndPos; i++)
            {
                dtTemp.ImportRow(worktb.Rows[i]);
                nCurrent++;
            }
            bindingSource1.DataSource = dtTemp;
            bindingNavigator1.BindingSource = bindingSource1;
            dataGridView1.DataSource = bindingSource1;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Pagenum--;
            if (Pagenum <= 0)
            {
                MessageBox.Show("已经是第一页，请点击“下一页”查看！");
                return;
            }
            else
            {
                nCurrent = Pagesize * (Pagenum - 1);
            }

            LoadData();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Pagenum++;
            if (Pagenum > Totalpage)
            {
                MessageBox.Show("已经是最后一页，请点击“上一页”查看！");
                return;
            }
            else
            {
                nCurrent = Pagesize * (Pagenum - 1);
            }
            LoadData();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            //int index = dataGridView1.CurrentRow.Index; //获取选中行的行号
            Sender = dataGridView1.CurrentRow.Cells[0].Value.ToString();//Cells[0]为要选的第几列
            Date = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            String Strconn = "Server=120.27.47.166;uid=root;pwd=123456;database=test"; //链接数据库
            MySqlConnection conn = new MySqlConnection(Strconn);
            conn.Open();//打开数据库
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select Message from test.inform_table where Sender=" + '"' + Sender + '"' + "and Date=" + '"' + Date + '"';
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                textBox1.Text = reader.GetString(reader.GetOrdinal("Message"));
            }
            reader.Close();
            reader.Dispose();
            conn.Close();

        } 
       
        
    }
}
