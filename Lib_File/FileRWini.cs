using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_File
{
    public class FileRWini
    {
        [System.Runtime.InteropServices.DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [System.Runtime.InteropServices.DllImport("kernel32")]
        private static extern long GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        #region   创建文件
        public static void CreateFile(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                try
                {
                    string dr = Path.GetDirectoryName(path);
                    if (!Directory.Exists(dr))
                    {
                        Directory.CreateDirectory(dr);
                    }
                    if (!File.Exists(path))
                    {
                        FileStream fs = File.Create(path);
                        fs.Close();
                    }
                }
                catch (Exception e)
                {
                }
            }
        }
        #endregion
        #region 写ini文件
        ///<param name="Selection">ini文件中的节名</param>
        ///<param name="key">ini 文件中的健</param>
        ///<param name="value">要写入该健所对应的值</param>
        ///<param name="iniFilePath">ini文件路径</param>
        public static bool WriteIniData(string Section, string key, string val, string inifilePath)
        {
            if (File.Exists(inifilePath))
            {
                long opSt = WritePrivateProfileString(Section, key, val, inifilePath);
                if (opSt == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                CreateFile(inifilePath);
                long opSt = WritePrivateProfileString(Section, key, val, inifilePath);
                if (opSt == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        #endregion
        #region  取ini文件
        /// <param name="section">节点名称</param>
        /// <param name="key">对应的key</param>
        /// <param name="noText">读不到值时返回的默认值</param>
        /// <param name="iniFilePath">文件路径</param>
        public static string ReadIniData(string section, string key, string noText, string iniFilePath)
        {
            if (File.Exists(iniFilePath))
            {
                StringBuilder temp = new StringBuilder(1024);
                long k = GetPrivateProfileString(section, key, noText, temp, 1024, iniFilePath);
                if (k != 0)
                {
                    return temp.ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }
        #endregion


        public static int getklZh(char key)
        {
            string str = new string(key, 1);
            string path6 = Environment.CurrentDirectory + "\\Config"; 
            path6 = path6 + "\\kl_zh.txt";
            string[] array = File.ReadAllLines(path6, Encoding.Default);

            for (int i = 0; i < array.Length; i++)
            {
                if (str.Equals(array[i].Trim()))
                {
                    //Console.WriteLine("找到中文了" + str);
                    //Console.WriteLine(126 + i + 1);
                    return 126 + i + 1;
                }

            }
            return 0;

        }
    }
}
