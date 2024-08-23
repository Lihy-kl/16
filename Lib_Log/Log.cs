using System;
using System.IO;

namespace Lib_Log
{
    public class Log
    {
        public static object m_object = 1;
        /// <summary>
        /// 写入log
        /// </summary>
        /// <param name="ex">异常</param>
        public static void writeLogException(Exception ex) //异常信息写入日志
        {
            lock (m_object)
            {
                //加锁 防止多用户同时写入
                //获取异常信息的类、行号、异常 信息
                string exceptionStr =
                 ex.StackTrace.ToString().Substring(ex.StackTrace.ToString().LastIndexOf('\\') + 1)
                 + "  " + ex.Message;
                exceptionStr = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "  " + exceptionStr;
                //自己定义一个存储日志文件的位置
                string sFilePath = Environment.CurrentDirectory + "\\Logs";
                string sFileName = DateTime.Now.ToString("yyyy-MM-dd") + ".log";
                sFileName = sFilePath + @"\\" + sFileName; //文件
                if (!Directory.Exists(sFilePath))
                {
                    Directory.CreateDirectory(sFilePath);
                }
                FileStream fs;
                StreamWriter sw;
                if (System.IO.File.Exists(sFileName))
                {
                    fs = new FileStream(sFileName, FileMode.Append, FileAccess.Write);
                }
                else
                {
                    fs = new FileStream(sFileName, FileMode.Create, FileAccess.Write);
                }
                sw = new StreamWriter(fs);
                sw.WriteLine(exceptionStr);
                sw.Close();
                fs.Close();

            };
        }

        public static void writeLogException(string  s) //异常信息写入日志
        {
            lock (m_object)
            {
                //加锁 防止多用户同时写入
                //获取异常信息的类、行号、异常 信息
                string exceptionStr = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:fff") + "  " + s;
                //自己定义一个存储日志文件的位置
                string sFilePath = Environment.CurrentDirectory + "\\Logs";
                string sFileName = DateTime.Now.ToString("yyyy-MM-dd") + ".log";
                sFileName = sFilePath + @"\\" + sFileName; //文件
                if (!Directory.Exists(sFilePath))
                {
                    Directory.CreateDirectory(sFilePath);
                }
                FileStream fs;
                StreamWriter sw;
                if (System.IO.File.Exists(sFileName))
                {
                    fs = new FileStream(sFileName, FileMode.Append, FileAccess.Write);
                }
                else
                {
                    fs = new FileStream(sFileName, FileMode.Create, FileAccess.Write);
                }
                sw = new StreamWriter(fs);
                sw.WriteLine(exceptionStr);
                sw.Close();
                fs.Close();

            };
        }
    }
}