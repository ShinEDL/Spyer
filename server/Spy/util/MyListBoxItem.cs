using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 自定义ListBox控件类的item结构
 */ 
namespace Spy.util
{
    class MyListBoxItem
    {
        private int _id;
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        private String _ip;
        public String ip
        {
            get { return _ip; }
            set { _ip = value; }
        }

        private String _department;
        public String department
        {
            get { return _department; }
            set { _department = value; }
        }

        private String _userName;
        public String userName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        public Bitmap img;


        public MyListBoxItem(int Id,string Ip,string Department,string UserName,Bitmap Img)
        {
            id = Id;
            ip = Ip;
            department = Department;
            userName = UserName;
            img = Img;
        }
        public MyListBoxItem()
        {

        }


    }
}
