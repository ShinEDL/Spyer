using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Spy;
using System.Management;

namespace Spy.util
{
    class DB
    {
        private Login login1 = null;
        private Main main;
        public DB(Login login)
        {
            login1 = login;
        }

        public DB(Main main)
        {
            // TODO: Complete member initialization
            this.main = main;
        }
        //建立MySql数据库连接
        public  MySqlConnection getMySqlCon()
        {
            MySqlConnection con ;
            //String mysqlstr = "Database=test;Data Source=172.18.13.42;User Id=shine;Password=abc123456;pooling=true;CharSet=utf8;port=3306";
            //String mysqlstr = "Database=test;Data Source=120.27.47.166;User Id=root;Password=123456;pooling=true;CharSet=utf8;port=3306";
            String mysqlstr = "Database=spy;Data Source=localhost;User Id=root;Password=123456;pooling=true;CharSet=utf8;port=3306";
            con = new MySqlConnection(mysqlstr);
            return con;
            //172.18.13.97
        }
        // 建立执行命令语句对象
        public  MySqlCommand getSqlCommand(String sql, MySqlConnection mysql)
        {
            MySqlCommand mySqlCommand = new MySqlCommand(sql, mysql);
            //  MySqlCommand mySqlCommand = new MySqlCommand(sql);
            // mySqlCommand.Connection = mysql;
            return mySqlCommand;
        }
        /// <summary>
        /// 查询用户信息并获得结果集并遍历；
        /// 登录验证
        /// </summary>
        /// <param name="mySqlCommand"></param>
        public int loginVilify(MySqlConnection mycon, string username)
        {
            MySqlDataReader reader1 = null;
            MySqlDataReader reader2 = null;
            try
            {
                int cid;
                string mac = this.GetMacAddress();
                String sql1 = "select cid from mac_cid where mac=" + '"' + mac + '"'; //创建查询语句

                MySqlCommand mySqlCommand = new MySqlCommand(sql1, mycon);
                MySqlDataReader reader = mySqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    cid = reader.GetInt32(reader.GetOrdinal("cid"));
                    reader.Close();

                    string search1 = "select * from user";
                    string search2 = "select isOnline from user_LoginInfo where userName='" + username + "' and cid=" + cid;
                    MySqlCommand mysqlcommand1 = getSqlCommand(search1, mycon);
                    MySqlCommand mysqlcommand2 = getSqlCommand(search2, mycon);
                    reader1 = mysqlcommand1.ExecuteReader();
                    while (reader1.Read())
                    {
                        if (reader1.HasRows)
                        {
                            if (reader1.GetString("user") == login1.num.Text && reader1.GetString("pwd") == login1.pwd1.Text)
                            {
                                if (reader1.GetString("department") != "管理员" && reader1.GetInt32("isRegist") == 1)
                                {
                                    reader1.Close();
                                    reader2 = mysqlcommand2.ExecuteReader();
                                    if (reader2.Read())
                                    {
                                        if (reader2.HasRows)
                                        {
                                            if (reader2.GetInt32("isOnline") == 0)
                                            {
                                                reader2.Close();
                                                this.updateIsOnline(mycon, 1, username);
                                                return 1;//登录成功
                                            }
                                            else
                                            {
                                                reader2.Close();
                                                return -2;//已经在线
                                            }
                                        }

                                    }
                                    reader2.Close();
                                    this.updateIsOnline(mycon, 1, username);
                                    return 1;//登录成功
                                }
                                else if (reader1.GetString("department") == "管理员")
                                {
                                    return -1;//是管理员
                                }
                                else if (reader1.GetInt32("isRegist") == 0)
                                {
                                    return 1;//注册
                                }

                            }
                        }
                    }
                    return 0;//用户名密码错误
                }
                return -3;

                
            }
            catch (Exception e)
            {
                Console.WriteLine("查询失败了！错误：" + e.Message);
                return -3;
            }
            finally
            {
                if (reader1 != null)
                {
                    reader1.Close();
                }
            }
        }

        //
        //登录或退出系统时，更改数据库online状态
        //
        public void updateIsOnline(MySqlConnection mycon, int status, string username)
        {
            try
            {
                int cid ;
                string mac = this.GetMacAddress();
                String sql1 = "select cid from mac_cid where mac=" + '"' + mac + '"'; //创建查询语句

                MySqlCommand mySqlCommand1 = new MySqlCommand(sql1, mycon);
                MySqlDataReader reader = mySqlCommand1.ExecuteReader();
                Console.WriteLine(sql1);
                if (reader.Read())
                {
                    cid = reader.GetInt32(reader.GetOrdinal("cid"));
                    reader.Close();
                    String sql2 = "update user_LoginInfo set isOnline=" + status + " where userName='" + username + "' and cid=" + cid;
                    MySqlCommand mySqlCommand2 = new MySqlCommand(sql2, mycon);
                    mySqlCommand2.ExecuteNonQuery();
                    Console.WriteLine(sql2);
                } 
                
            }
            catch (Exception e)
            {
                Console.WriteLine("updateIsOnline error："+e.Message);
            }

        }

        public string GetMacAddress()//网卡mac 查询
        {
            try
            {
                //获取网卡硬件地址 
                string mac = "";
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        mac = mo["MacAddress"].ToString();
                        break;
                    }
                }
                moc = null;
                mc = null;
                return mac;
            }
            catch
            {
                //MessageBox.Show("获取电脑CPU信息失败 请联系管理员");
                return "error";
            }
            finally
            {
            }

        }

        
    }

    
}
