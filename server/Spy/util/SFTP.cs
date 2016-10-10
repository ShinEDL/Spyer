using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;
using System.IO;
using System.Collections;

/*
 * 连接服务器工具
 */ 
namespace Spy.util
{
    class SFTP
    {
        private SftpClient sftp;
        public bool Connected
        {
            get
            {
                return sftp.IsConnected;
            }
        }

        #region 构造函数
        /// <summary>  
        /// 构造  
        /// </summary>  
        /// <param name="ip">IP</param>  
        /// <param name="port">端口</param>  
        /// <param name="user">用户名</param>  
        /// <param name="pwd">密码</param>  
        public SFTP()  
        {  
            sftp = new SftpClient("120.27.47.166", Int32.Parse("22"), "root", "Zhang7890078");  
        }  
        #endregion

        #region SFTP上传文件
        /// <summary>  
        /// SFTP上传文件  
        /// </summary>  
        /// <param name="localPath">本地路径</param>  
        /// <param name="remotePath">远程路径</param>  
        public void Put(string localPath, string remotePath)
        {
            try
            {
                using (var file = File.OpenRead(localPath))
                {
                    Connect();
                    sftp.UploadFile(file, remotePath);
                    Disconnect();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("SFTP文件上传失败，原因：{0}", ex.Message));
            }
        }
        #endregion

        #region 连接SFTP
        /// <summary>  
        /// 连接SFTP  
        /// </summary>  
        /// <returns>true成功</returns>  
        public bool Connect()
        {
            try
            {
                if (!Connected)
                {
                    sftp.Connect();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("连接SFTP失败，原因：{0}", ex.Message));
            }
        }
        #endregion  

        #region 断开SFTP
        /// <summary>  
        /// 断开SFTP  
        /// </summary>   
        public void Disconnect()
        {
            try
            {
                if (sftp != null && Connected)
                {
                    sftp.Disconnect();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("断开SFTP失败，原因：{0}", ex.Message));
            }
        }
        #endregion  

        #region SFTP获取文件
        /// <summary>  
        /// SFTP获取文件  
        /// </summary>  
        /// <param name="remotePath">远程路径</param>  
        /// <param name="localPath">本地路径</param> , string localPath 
        public Byte[] Get(string remotePath)
        {
            try
            {
                //Connect();
                var byt = sftp.ReadAllBytes(remotePath);
                //Disconnect();
                //File.WriteAllBytes(localPath, byt);
                return byt;
                
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("SFTP文件获取失败，原因：{0}", ex.Message));
            }

        }
        #endregion 

        #region 获取SFTP文件列表
        /// <summary>  
        /// 获取SFTP文件列表  
        /// </summary>  
        /// <param name="remotePath">远程目录</param>  
        /// <param name="fileSuffix">文件前缀</param>  
        /// <returns></returns>  
        public ArrayList GetFileList(string remotePath, string fileSuffix)
        {
            try
            {
                //Connect();
                var files = sftp.ListDirectory(remotePath);
                ///Disconnect();
                var objList = new ArrayList();
                foreach (var file in files)
                {
                    string name = file.Name;
                    if (name.Length > (fileSuffix.Length + 1) && fileSuffix == name.Split('@')[0])//获取文件前缀的 用户名日期 字符串
                    {
                        objList.Add(name);
                    }
                    else if (name.Length > (fileSuffix.Length + 1) && fileSuffix == name.Split('.')[0])//获取文件前缀的 用户名日期 字符串
                    {
                        objList.Add(name);
                    }
                }
                return objList;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("SFTP文件列表获取失败，原因：{0}", ex.Message));
            }
        }
        #endregion

        #region 获取feedbackimg文件列表
        /// <summary>  
        /// 获取SFTP文件列表  
        /// </summary>  
        /// <param name="remotePath">远程目录</param>  
        /// <param name="fileSuffix">文件前缀</param>  
        /// <returns></returns>  
        public ArrayList GetFeedbackList(string remotePath, string fileSuffix)
        {
            try
            {
                //Connect();
                var files = sftp.ListDirectory(remotePath);
                ///Disconnect();
                var objList = new ArrayList();
                foreach (var file in files)
                {
                    string name = file.Name;
                    if (name.Length > (fileSuffix.Length + 1) && fileSuffix == name.Split('@')[0] + "@" + name.Split('@')[1])//获取文件前缀的 用户名日期 字符串
                    {
                        objList.Add(name);
                    }
                }
                return objList;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("SFTP文件列表获取失败，原因：{0}", ex.Message));
            }
        }
        #endregion


    }
}
