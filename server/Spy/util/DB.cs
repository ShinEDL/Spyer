using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Spy;
using Spy.UI;
using Spy.Properties;

/*
 * 数据库操作类
 */ 
namespace Spy.util
{
    class DB
    {
        private Login login1 = null;
        private Selection selection = null;
        private Report report = null;
        public DB(Login login)
        {
            login1 = login;
        }
        public DB(Selection select)
        {
            selection = select;
        }
        public DB(Report rep)
        {
            report = rep;
        }
        public DB()
        {

        }
        //建立MySql数据库连接
        public  MySqlConnection getMySqlCon()
        {
            MySqlConnection con ;
            String mysqlstr = "Database=test;Data Source=120.27.47.166;User Id=root;Password=123456;pooling=true;CharSet=utf8;port=3306";
            con = new MySqlConnection(mysqlstr);
            return con;
        }
        // 建立执行命令语句对象
        public  MySqlCommand getSqlCommand(String sql, MySqlConnection mysql)
        {
            MySqlCommand mySqlCommand = new MySqlCommand(sql, mysql);
            return mySqlCommand;
        }

        /// <summary>
        /// 查询用户信息并获得结果集并遍历；
        /// 登录验证
        /// </summary>
        /// <param name="mySqlCommand"></param>
        public int loginVilify(MySqlConnection mycon,string username)
        {
            MySqlDataReader reader1 = null ;
            try
            {
                string search1 = "select * from user";
                MySqlCommand mysqlcommand1 = getSqlCommand(search1, mycon);
                reader1 = mysqlcommand1.ExecuteReader();
                while (reader1.Read())
                {
                    if (reader1.HasRows)
                    {
                        if (reader1.GetString("user") == login1.num.Text)
                        {
                            if(reader1.GetString("department")=="管理员")
                            {
                                if (reader1.GetString("pwd") == login1.pwd1.Text)
                                {
                                    return 1;
                                }
                                else
                                {
                                    return -3;//密码错误
                                }
                                
                            }
                            else
                            {
                                return -2;//非管理员
                            }
                            
                        }
                    }
                }
                return 0;//用户名错误
            }
            catch (Exception e )
            {
                Console.WriteLine("查询失败了！错误：" + e.Message);
                return -1;
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
        //查询员工信息
        //
        public UserInfo getUserInfo(string username,string department)
        {
            UserInfo userInfo = null;
            MySqlConnection mycon = getMySqlCon();
            MySqlDataReader reader1 = null ;
            try
            {
                if (mycon != null)
                {
                    mycon.Open();
                    string search1 = "select * from user where user='"+ username +"' and department='"+ department +"'";
                    MySqlCommand mysqlcommand1 = getSqlCommand(search1, mycon);
                    reader1 = mysqlcommand1.ExecuteReader();
                    while (reader1.Read())
                    {
                        if (reader1.HasRows)
                        {
                            userInfo = new UserInfo(reader1.GetInt32("id"), username, department, reader1.GetString("positions"), reader1.GetString("employment_date"), reader1.GetInt32("violation"));
                        }
                    }
                }
                return userInfo;
            }
            catch(Exception e)
            {
                Console.WriteLine("读取User表发生错误 ：" +e.Message);
                return null;
            }
            finally
            {
                mycon.Close();
            }
        }

        //
        //查询violation表
        //
        public List<string> getViolationById(int id)
        {
            List<string> violations = new List<string>();
            string violation = null;
            MySqlConnection mycon = getMySqlCon();
            MySqlDataReader reader = null;
            try
            {
                if (mycon != null)
                {
                    mycon.Open();
                    string search = "select * from violation where pid=" + id;
                    MySqlCommand mysqlcommand = getSqlCommand(search, mycon);
                    reader = mysqlcommand.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            violation = reader.GetString("violation");
                            violations.Add(violation);
                        }
                    }
                }
                return violations;
                
            }
            catch
            {
                return null;
            }
            finally
            {
                mycon.Close();
            }
        }

        //
        //更新violation值
        //
        public bool updateViolation(int id,string content,bool flag)
        {
            int violation = 0;
            string user = "";
            MySqlConnection mycon = getMySqlCon();
            MySqlDataReader reader = null;
            try
            {
                mycon.Open();
                string search = "select * from user where id=" + id;
                MySqlCommand mysqlcommand = getSqlCommand(search, mycon);
                reader = mysqlcommand.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        violation = reader.GetInt32("violation");
                        user = reader.GetString("user");
                    }
                }
                reader.Close();
                //true为增加、false为减少
                if (flag)
                {
                    String sql1 = "update user set violation=violation+1 where id=" + id;
                    Console.WriteLine(sql1);
                    MySqlCommand cmd1 = new MySqlCommand(sql1, mycon);
                    cmd1.ExecuteNonQuery();
                    String sql2 = "insert into violation values (" + id + ",'" + user + "','" + DateTime.Now.ToString() + "---" + content + "')" ;
                    Console.WriteLine(sql2);
                    MySqlCommand cmd2 = new MySqlCommand(sql2, mycon);
                    cmd2.ExecuteNonQuery();
                    return true;
                }
                else
                {
                    if (violation > 0)
                    {
                         String sql = "update user set violation=violation-1 where id=" + id;
                         Console.WriteLine(sql);
                         MySqlCommand cmd = new MySqlCommand(sql, mycon);
                         cmd.ExecuteNonQuery();
                         String sql2 = "delete from violation where violation='"+ content +"'";
                         Console.WriteLine(sql2);
                         MySqlCommand cmd2 = new MySqlCommand(sql2, mycon);
                         cmd2.ExecuteNonQuery();
                         return true;
                    }
                    else
                    {
                        return false;            
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("错误 ： " + e.Message);
                return false;
            }
            finally
            {
                mycon.Close();
            }
        }

        //
        //获取在线客机  计算机编号-用户名-部门信息
        //
        public List<string> getOnlineUsers()
        {
            List<string> OnlineUsers = new List<string>();
            string onlineUser = null;
            MySqlConnection mycon = getMySqlCon();
            MySqlDataReader reader = null;
            try
            {
                if (mycon != null)
                {
                    mycon.Open();
                    string search = "select * from user_LoginInfo where isOnline=1";
                    MySqlCommand mysqlcommand = getSqlCommand(search, mycon);
                    reader = mysqlcommand.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            onlineUser = reader.GetInt32("cid").ToString() + "-" + reader.GetString("userName") + "-" + reader.GetString("department") + "@" + reader.GetString("ip");
                            OnlineUsers.Add(onlineUser);
                        }
                    }
                }
                return OnlineUsers;

            }
            catch
            {
                return null;
            }
            finally
            {
                mycon.Close();
            }
        }

        //
        //查询在线客机
        //返回：在线客机列表
        //
        public List<MyListBoxItem> getComputer()
        {
            MySqlConnection mycon = getMySqlCon();
            MySqlDataReader reader = null;
            List<MyListBoxItem> items = new List<MyListBoxItem>();
            MyListBoxItem item = null;
            try
            {
                if (mycon != null)
                {
                    mycon.Open();
                    Console.WriteLine("数据库user_LoginInfo打开成功！！！");
                    String sql = "select * from user_LoginInfo where department not like '%管理员%'";
                    MySqlCommand mySqlCommand = new MySqlCommand(sql, mycon);
                    reader = mySqlCommand.ExecuteReader();
                    while(reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            if (reader.GetInt32("isOnline") != 0)
                            {
                                item = new MyListBoxItem(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), Resources.test);
                                items.Add(item);
                            }
                        }
                    }
                    Console.WriteLine("数据库user_LoginInfo读取成功！！！");
                    return items;
                    
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("查询失败了！错误：" + e.Message);
                return null;
            }
            finally
            {
                reader.Close();
                mycon.Close();
            }
            return items;
        }

        //
        //更新feedback表result字段
        //args0 是result的值  args1是txtUrl的值
        //
        public void updateFeedback(string args0,string args1)
        {
            MySqlConnection mycon = getMySqlCon();
            try
            {
                if (mycon != null)
                {
                    mycon.Open();
                    String sql = "update feedback set result='"+ args0 +"' where txtUrl='"+ args1 + "'";
                    Console.WriteLine(sql);
                    MySqlCommand cmd = new MySqlCommand(sql, mycon);
                    cmd.ExecuteNonQuery();
                }
                
            }
            catch(Exception e)
            {
                Console.WriteLine("更新失败了！错误：" + e.Message);
            }
            finally
            {
                mycon.Close();
            }
        }

        //
        //查询feedback表txtUrl字段
        //
        public List<String> getNum(string filename)
        {
            MySqlConnection mycon = getMySqlCon();
            MySqlDataReader reader = null;
            List<String> str_list = new List<string>(); 
            try
            {
                if (mycon != null)
                {
                    mycon.Open();
                    String sql = "select txtUrl from feedback where txtUrl like '" + filename + "%'";
                    MySqlCommand cmd = new MySqlCommand(sql, mycon);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            str_list.Add(reader.GetString(0).Split('@')[1].Split('.')[0]);//直接获取编号
                        }
                    }
                    return str_list;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("更新失败了！错误：" + e.Message);
                return null;
            }
            finally
            {
                mycon.Close();
            }
            return null;
        }

        //
        //查询feedback表的result值
        //
        public string getResult(string filename,string num)
        {
            MySqlConnection mycon = getMySqlCon();
            MySqlDataReader reader = null;
            string result = null;
            try
            {
                if (mycon != null)
                {
                    mycon.Open();
                    String sql = "select result from feedback where txtUrl like '" + filename + "@" + num + "%'";
                    MySqlCommand cmd = new MySqlCommand(sql, mycon);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            result = reader.GetString(0);
                        }
                    }
                }

            }
            catch(Exception e)
            {
                Console.WriteLine("查询失败了！错误：" + e.Message);
                return null;
            }
            finally
            {
                mycon.Close();
            }
            return result;
        }

        //
        //更新user_LoginInfo表的isOnline值
        //
        public void updateIsOnline(int online,int id)
        {
            MySqlConnection mycon = getMySqlCon();
            try
            {
                if (mycon != null)
                {
                    mycon.Open();
                    String sql = "update user_LoginInfo set isOnline=" + online + " where cid=" + id ;
                    MySqlCommand cmd = new MySqlCommand(sql, mycon);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine(sql);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("isOnline值更新失败了！错误：" + e.Message);
            }
            finally
            {
                mycon.Close();
            }
        }

        //
        //插入inform_table表
        //
        public void updateInform(string sender,string message)
        {
            MySqlConnection mycon = getMySqlCon();
            try
            {
                if (mycon != null)
                {
                    mycon.Open();
                    String sql = "insert into inform_table values ('" + sender + "','" + DateTime.Now.ToString() + "','通知','" + message + "')";
                    MySqlCommand cmd = new MySqlCommand(sql, mycon);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine(sql);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("updateInform 错误信息：" + e.Message);
            }
            finally
            {
                mycon.Close();
            }
        }


    }

    
}
