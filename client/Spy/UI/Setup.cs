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
using System.Text.RegularExpressions;

namespace Spy.UI
{
    public partial class Setup : Form
    {
        public Setup()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string exepath = Environment.CurrentDirectory;
            if (System.IO.File.Exists(exepath + @"\IpAdress.txt") == true)
            {
                StreamReader sr = File.OpenText(exepath + @"\IpAdress.txt");
                textBox1.Text = sr.ReadLine();
                sr.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str1;
            str1 = textBox1.Text.ToString();
            if (Regex.IsMatch(str1, @"^([1-9]?\d|1\d\d|2[0-4]\d|25[0-5])\.([1-9]?\d|1\d\d|2[0-4]\d|25[0-5])\.([1-9]?\d|1\d\d|2[0-4]\d|25[0-5])\.([1-9]?\d|1\d\d|2[0-4]\d|25[0-5])$"))
            {
                string exepath = Environment.CurrentDirectory;
                File.WriteAllText(exepath + @"\IpAdress.txt", str1);
                MessageBox.Show("服务器IP修改成功，重启程序后才能生效。");
            }
            else
            {
                MessageBox.Show("这并不是有效的IP地址，请更正。");
            }

        }
    }
}
