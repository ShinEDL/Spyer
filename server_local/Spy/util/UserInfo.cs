using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 客户端用户的实体类
 */ 
namespace Spy.util
{
    class UserInfo
    {
        private int _id;
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        private int _violation;
        public int violation
        {
            get { return _violation; }
            set { _violation = value; }
        }

        private string _user;
        public string user
        {
            get { return _user; }
            set { _user = value; }
        }

        private string _department;
        public string department
        {
            get { return _department; }
            set { _department = value; }
        }

        private string _positions;
        public string positions
        {
            get { return _positions; }
            set { _positions = value; }
        }

        private string _employeement_date;
        public string employeement_date
        {
            get { return _employeement_date; }
            set { _employeement_date = value; }
        }


        public UserInfo(int Id,string User,string Department,string Positions,string Employeement_date,int Violation)
        {
            id = Id;
            user = User;
            department = Department;
            positions = Positions;
            employeement_date = Employeement_date;
            violation = Violation;
        }

        public UserInfo()
        {

        }
    }
}
