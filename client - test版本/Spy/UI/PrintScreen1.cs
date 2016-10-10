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

namespace Spy.UI
{
   
    public partial class PrintScreen1 : Form
    {      
        public PrintScreen1()
        {
            InitializeComponent();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog sprint = new FolderBrowserDialog();
            if (sprint.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = sprint.SelectedPath;
                string exepath = Environment.CurrentDirectory;
                File.WriteAllText(exepath + @"\Text1.txt",sprint.SelectedPath + @"\");
            }
        }

        private void saveFileDialog1_FileOk_1(object sender, CancelEventArgs e)
        {
            
        }

        private void PrintScreen1_Load(object sender, EventArgs e)
        {
            string exepath = Environment.CurrentDirectory;
            if (System.IO.File.Exists(exepath + @"\Text1.txt") == true)
            {
                StreamReader sr = File.OpenText(exepath + @"\Text1.txt");
                textBox1.Text = sr.ReadLine();
                sr.Close();
            }
            else
            {
                MessageBox.Show("保存路径不存在自动保存在" + exepath + @"\PrintScreen");
                File.WriteAllText(exepath + @"\Text1.txt", exepath + @"\PrintScreen\");
                StreamReader sr = File.OpenText(exepath + @"\Text1.txt");
                textBox1.Text = sr.ReadLine();
                sr.Close();
            }
        }
    }
}
