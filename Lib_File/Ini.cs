using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Lib_File
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

    public class Txt
    {
        public static void WriteTXTC(int cupNo, string str)
        {
            try
            {
                string FilePath = Environment.CurrentDirectory + "\\App_Data\\" + cupNo.ToString() + "C.txt";
                str += "\n"+DateTime.Now.ToString();
                byte[] s = System.Text.Encoding.Default.GetBytes(str.ToString());
                FileStream fs = new FileStream(FilePath, FileMode.Append, FileAccess.Write);
                fs.Write(s, 0, s.Length);
                fs.Close();
            }
            catch (Exception ex)
            {
                // FADM_Form.CustomMessageBox.Show(ex.Message);
            }
        }

        public static void WriteTXT(int cupNo, string str)
        {
            try
            {
                string FilePath = Environment.CurrentDirectory + "\\App_Data\\" + cupNo.ToString() + ".txt";
                str += "@";
                byte[] s = System.Text.Encoding.Default.GetBytes(str.ToString());
                FileStream fs = new FileStream(FilePath, FileMode.Append, FileAccess.Write);
                fs.Write(s, 0, s.Length);
                fs.Close();
            }
            catch (Exception ex)
            {
                // FADM_Form.CustomMessageBox.Show(ex.Message);
            }
        }

        public static string ReadTXT(int cupNo)
        {
            try
            {
                string FilePath = Environment.CurrentDirectory + "\\App_Data\\" + cupNo.ToString() + ".txt";
                StreamReader readstring = new StreamReader(FilePath, System.Text.Encoding.Default);
                string myfile;
                myfile = readstring.ReadToEnd();//此句读取到尾时，已把光标指针移动到文件结尾
                readstring.Close();
                return myfile;
            }
            catch (Exception ex)
            {
                // FADM_Form.CustomMessageBox.Show(ex.Message);
                return null;
            }
        }

        public static void DeleteTXT(int cupNo)
        {
            string FilePath = Environment.CurrentDirectory + "\\App_Data\\" + cupNo.ToString() + ".txt";
            if(File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
        }


        public static void WriteMarkTXT(int cupNo, string TechnologyName, string StepNum)
        {
            try
            {
                string FilePath = Environment.CurrentDirectory + "\\App_Data\\" + cupNo.ToString() + "Step.txt";
                TechnologyName += ",";
                TechnologyName += StepNum;
                TechnologyName += "@";
                byte[] s = System.Text.Encoding.Default.GetBytes(TechnologyName.ToString());
                FileStream fs = new FileStream(FilePath, FileMode.Append, FileAccess.Write);
                fs.Write(s, 0, s.Length);
                fs.Close();
            }
            catch (Exception ex)
            {
                // FADM_Form.CustomMessageBox.Show(ex.Message);
            }
        }

        public static string ReadMarkTXT(int cupNo)
        {
            try
            {
                string FilePath = Environment.CurrentDirectory + "\\App_Data\\" + cupNo.ToString() + "Step.txt";
                StreamReader readstring = new StreamReader(FilePath, System.Text.Encoding.Default);
                string myfile;
                myfile = readstring.ReadToEnd();//此句读取到尾时，已把光标指针移动到文件结尾
                readstring.Close();
                return myfile;
            }
            catch (Exception ex)
            {
                // FADM_Form.CustomMessageBox.Show(ex.Message);
                return null;
            }
        }

        public static void DeleteMarkTXT(int cupNo)
        {
            string FilePath = Environment.CurrentDirectory + "\\App_Data\\" + cupNo.ToString() + "Step.txt";
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
        }
    }
}