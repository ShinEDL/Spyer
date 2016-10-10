using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spy.Test
{
    class ThreadTest
    {
        #region 多线程测试
        public void ThreadTest1(string a1, int b1)
        {
            Console.WriteLine("这是：{0}" + " 与 {1}", a1, b1);
        }

        private string a;
        public string A
        {
            get { return a; }
            set { this.a = value; }
        }
        private int b;

        public int B
        {
            get { return b; }
            set { this.b = value; }
        }
        public void run()
        {
            ThreadTest1(a, b);
        }
        #endregion
    }
}
