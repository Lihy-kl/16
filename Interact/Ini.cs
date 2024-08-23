using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Interact
{
    public class Ini
    {
        /// <summary>
        /// 写入ini文件,调用WinApi函数操作ini
        /// </summary>
        /// <param name="node">节点名称</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="filepath">ini路径</param>
        /// <returns>0失败/其他成功</returns>
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern long WritePrivateProfileString(string node, string key, string value, string filepath);

        /// <summary>
        /// 读取ini
        /// </summary>
        /// <param name="node">节点名称</param>
        /// <param name="key">键</param>
        /// <param name="value">值(未读取到数据时设置的默认返回值)</param>
        /// <param name="result">读取的结果值</param>
        /// <param name="size">读取缓冲区大小</param>
        /// <param name="filePath">ini路径</param>
        /// <returns>读取到的字节数量</returns>
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string node, string key, string value, StringBuilder result, int size, string filePath);


        static readonly string iniPath = Environment.CurrentDirectory + "\\config.ini";
        /// <summary>
        /// 读取ini
        /// </summary>
        public static string GetIni(string node, string key)
        {
            //声明接收的数据
            StringBuilder builder = new StringBuilder(1024);
            //调用Winapi函数读取config节点下Name的值
            int len = GetPrivateProfileString(node, key, "", builder, 1024, iniPath);
            return builder.ToString();
        }

        public static string GetIni(string node, string key, string sPath)
        {
            //声明接收的数据
            StringBuilder builder = new StringBuilder(1024);
            //调用Winapi函数读取config节点下Name的值
            int len = GetPrivateProfileString(node, key, "", builder, 1024, sPath);
            return builder.ToString();
        }

        /// <summary>
        /// 读取ini,sValue为默认值
        /// </summary>
        public static string GetIni(string node, string key, string sValue, string sPath)
        {
            //声明接收的数据
            StringBuilder builder = new StringBuilder(1024);
            //调用Winapi函数读取config节点下Name的值
            int len = GetPrivateProfileString(node, key, sValue, builder, 1024, sPath);
            return builder.ToString();
        }

        /// <summary>
        /// 写入ini
        /// </summary>
        public static void WriteIni(string node, string key, string value)
        {
            long len = WritePrivateProfileString(node, key, value, iniPath);
        }

        public static void WriteIni(string node, string key, string value, string sPath)
        {
            long len = WritePrivateProfileString(node, key, value, sPath);
        }
    }
}
