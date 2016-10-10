using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * 远程关机 窗体
 */ 
namespace Spy.UI
{
    public partial class ShutDown : Form
    {
        Cat cat;
        public ShutDown(Cat c)
        {
            InitializeComponent();
            cat = c;
        }

        //确定按钮
        private void button1_Click(object sender, EventArgs e)
        {
            Cat.socket_command.socketConnect();
            Cat.socket_command.send("[%]主机关闭[%]");
            Cat.socket_command.socketClose();
            this.Close();
            Thread.Sleep(2000);
            cat.Close();
            
        }

        //关闭
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
