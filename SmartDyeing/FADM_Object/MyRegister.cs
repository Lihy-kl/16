using System;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Net;
using System.IO;

namespace SmartDyeing.FADM_Object
{
   public class MyRegister
    {

        public static MyRegister _myreg;
        ///<summary>
        /// 获取硬盘卷标号
        ///</summary>
        ///<returns></returns>
        public string GetDiskVolumeSerialNumber()
        {
            ManagementClass mc = new ManagementClass("win32_NetworkAdapterConfiguration");
            ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"c:\"");
            disk.Get();
            return disk.GetPropertyValue("VolumeSerialNumber").ToString();
        }

        ///<summary>
        /// 获取CPU序列号
        ///</summary>
        ///<returns></returns>
        public string GetCpu()
        {
            string s_cpu = null;
            ManagementClass myCpu = new ManagementClass("win32_Processor");
            ManagementObjectCollection myCpuCollection = myCpu.GetInstances();
            foreach (ManagementObject myObject in myCpuCollection)
            {
                s_cpu = myObject.Properties["Processorid"].Value.ToString();
            }
            return s_cpu;
        }

        ///<summary>
        /// 生成机器码
        ///</summary>
        ///<returns></returns>
        public string GetMNum()
        {
            string s_num = GetCpu() + GetDiskVolumeSerialNumber();
            string s_mNum = s_num.Substring(0, 24);    //截取前24位作为机器码
            return s_mNum;
        }

        public int[] ia_code = new int[127];    //存储密钥
        public char[] ca_code = new char[25];  //存储ASCII码
        public int[] ia_number = new int[25];   //存储ASCII码值

        //记录8位硬盘序列号
        public char[] ca_codeDS = new char[9];  //存储ASCII码
        public int[] ia_numberDS = new int[9];   //存储ASCII码值



        //初始化密钥
        public void SetIntCode()
        {
            for (int i = 1; i < ia_code.Length; i++)
            {
                ia_code[i] = i % 9;
            }
        }

        ///<summary>
        /// 生成注册码
        ///</summary>
        ///<returns></returns>
        public string GetRNum()
        {
            SetIntCode();
            string s_mNum = GetMNum();
            for (int i = 1; i < ca_code.Length; i++)   //存储机器码
            {
                ca_code[i] = Convert.ToChar(s_mNum.Substring(i - 1, 1));
            }
            for (int j = 1; j < ia_number.Length; j++)  //改变ASCII码值
            {
                ia_number[j] = Convert.ToInt32(ca_code[j]) + ia_code[Convert.ToInt32(ca_code[j])];
            }
            string s_asciiName = "";   //注册码
            for (int k = 1; k < ia_number.Length; k++)  //生成注册码
            {

                if ((ia_number[k] >= 48 && ia_number[k] <= 57) || (ia_number[k] >= 65 && ia_number[k]
                    <= 90) || (ia_number[k] >= 97 && ia_number[k] <= 122))  //判断如果在0-9、A-Z、a-z之间
                {
                    s_asciiName += Convert.ToChar(ia_number[k]).ToString();
                }
                else if (ia_number[k] > 122)  //判断如果大于z
                {
                    s_asciiName += Convert.ToChar(ia_number[k] - 10).ToString();
                }
                else
                {
                    s_asciiName += Convert.ToChar(ia_number[k] - 9).ToString();
                }
            }
            return s_asciiName;
        }

        ///<summary>
        /// 生成硬盘序列号注册码
        ///</summary>
        ///<returns></returns>
        public string GetRNumDS()
        {
            SetIntCode();
            string s_mNum = GetDiskVolumeSerialNumber().Substring(0,8);
            for (int i = 1; i < ca_codeDS.Length; i++)   //存储机器码
            {
                ca_codeDS[i] = Convert.ToChar(s_mNum.Substring(i - 1, 1));
            }
            for (int j = 1; j < ia_numberDS.Length; j++)  //改变ASCII码值
            {
                ia_numberDS[j] = Convert.ToInt32(ca_codeDS[j]) + ia_code[Convert.ToInt32(ca_codeDS[j])];
            }
            string s_asciiName = "";   //注册码
            for (int k = 1; k < ia_numberDS.Length; k++)  //生成注册码
            {

                if ((ia_numberDS[k] >= 48 && ia_numberDS[k] <= 57) || (ia_numberDS[k] >= 65 && ia_numberDS[k]
                    <= 90) || (ia_numberDS[k] >= 97 && ia_numberDS[k] <= 122))  //判断如果在0-9、A-Z、a-z之间
                {
                    s_asciiName += Convert.ToChar(ia_numberDS[k]).ToString();
                }
                else if (ia_numberDS[k] > 122)  //判断如果大于z
                {
                    s_asciiName += Convert.ToChar(ia_numberDS[k] - 10).ToString();
                }
                else
                {
                    s_asciiName += Convert.ToChar(ia_numberDS[k] - 9).ToString();
                }
            }
            return s_asciiName;
        }


        public static int overdue()
        {
            if (_myreg==null) {
                _myreg =  new MyRegister();
            }
            string s_rnum = _myreg.GetRNum();
            string s_mnum = _myreg.GetMNum();
            RegistryKey retkey = Registry.CurrentUser.OpenSubKey("SOFTWARE", true).CreateSubKey("chec").CreateSubKey("Register.INI");
            foreach (string strRNum in retkey.GetSubKeyNames())
            {
                if (strRNum == s_rnum)
                {
                    return 0;
                }
            }

            int i_usetime = 0;

            DateTime P_dt_create = new DateTime();

            DateTime P_dt_last = new DateTime();

            DateTime P_dt_now = DateTime.Now;
        again:

            try
            {
                //获取允许使用次数
                string i_usetimeStr = (string)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\chec", "UseTimes", "");
                i_usetimeStr = FADM_Object.myAES.AesDecrypt(i_usetimeStr);
                i_usetime = Convert.ToInt32(i_usetimeStr);

                //获取上次时间 
                P_dt_last = Convert.ToDateTime(Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\chec", "LastDateTime", null));


                //获取创建时间
                string s_createStr = (string)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\chec", "CreateDateTime", "");
                s_createStr = FADM_Object.myAES.AesDecrypt(s_createStr);
                P_dt_create = Convert.ToDateTime(s_createStr);


            }
            catch (Exception ex)
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    FADM_Form.CustomMessageBox.Show("倒计时异常！", "警告", MessageBoxButtons.OK, false);
                else
                    FADM_Form.CustomMessageBox.Show("Countdown exception！", "warn", MessageBoxButtons.OK, false);


                ////创建起始时间
                //Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\chec", "CreateDateTime", P_dt_now, RegistryValueKind.String);

                ////创建上次时间
                //Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\chec", "LastDateTime", P_dt_now, RegistryValueKind.String);

                ////创建允许天数
                //Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\chec", "UseTimes", 120, RegistryValueKind.DWord);

                //goto again;
            }


            if (P_dt_last > P_dt_now)
            {
                return 1;
            }
            else
            {
                if ((P_dt_now - P_dt_last).Days >= 1)
                {
                    //修改上次时间
                    Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\chec", "LastDateTime", P_dt_now, RegistryValueKind.String);
                }
            }

            //if (false)
            if ((P_dt_now - P_dt_create).Days < i_usetime)
            {
                //提前十天报警
                //if ((P_dt_now - P_dt_create).Days >= i_usetime - 10)
                //{
                //     FADM_Form.CustomMessageBox.Show("还有" + (i_usetime - (P_dt_now - P_dt_create).Days) + "天软件过期,请联系供应商注册!",
                //                    "信息", MessageBoxButtons.OK, false);
                //}
                return 0;

            }
            else
            {
                // FADM_Form.CustomMessageBox.Show("试用次数已到！请联系供应商注册!", "信息", MessageBoxButtons.OK, false);
                //return false;

                //发起同步请求
                try
                {
                    string s_msg = SyncDy(s_mnum);
                    if (!s_msg.Equals("") && s_msg.Length > 0)
                    {
                        string s_demsg = FADM_Object.myAES.AesDecrypt(s_msg);
                        Console.WriteLine("解密后的文本" + s_demsg);
                        char[] ca_separator_1 = { '#' };
                        string[] sa_array = s_demsg.Split(ca_separator_1);
                        string s_errcode = sa_array[0];
                        if (s_errcode.Equals("errcode=0"))
                        {
                            //先判断下是否完全注册
                            string s_isreg = sa_array[3];
                            if (s_isreg.Equals("isreg=true"))
                            {
                                retkey = Registry.CurrentUser.OpenSubKey("Software", true).CreateSubKey("chec").CreateSubKey("Register.INI").CreateSubKey(s_rnum);
                                retkey.SetValue("UserName", "Rsoft");
                                return 1001;

                            }
                            else
                            {
                                string s_time = sa_array[2];//校验下时间
                                char[] ca_separator_2 = { '=' };
                                string[] sa_array2 = s_time.Split(ca_separator_2);
                                DateTime distanceTime = Convert.ToDateTime(sa_array2[1]);
                                DateTime now = DateTime.Now;
                                TimeSpan daysSpan = new TimeSpan(now.Ticks - distanceTime.Ticks);
                                int i_differ = daysSpan.Days;
                                if (Math.Abs(i_differ) > 1)
                                {
                                    return 1;
                                }
                                else
                                {
                                    //延长天数
                                    string s_dayStr = sa_array[1];
                                    char[] ca_separator_3 = { '=' };
                                    string[] sa_array3 = s_dayStr.Split(ca_separator_3);
                                    int i_day = Convert.ToInt32(sa_array3[1]);

                                    string s_enNow = FADM_Object.myAES.AesEncrypt(DateTime.Now.ToString());
                                    //修改创建时间
                                    Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\chec", "CreateDateTime", s_enNow, RegistryValueKind.String);
                                    //创建允许天数
                                    string s_enP_int_day = FADM_Object.myAES.AesEncrypt(i_day.ToString());
                                    Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\chec", "UseTimes", s_enP_int_day, RegistryValueKind.String);
                                    return 1001;
                                }
                            }
                        } else if (s_errcode.Equals("errcode=10010")) {
                            //创建时间改为当前时间 使用天数也就是使用次数= 0
                            String s_s = DateTime.Now.ToString();
                            string s_enNow = FADM_Object.myAES.AesEncrypt(s_s);
                            //修改创建时间
                            Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\chec", "CreateDateTime", s_enNow, RegistryValueKind.String);
                            //创建允许天数
                            string s_enP_int_day = FADM_Object.myAES.AesEncrypt("0");
                            Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\chec", "UseTimes", s_enP_int_day, RegistryValueKind.String);
                            return 10010;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                }
                catch (Exception e)
                {

                }
                return -1;
            }

        }

        public static string SyncDy(string s_rnum)
        {
            try
            {
                //同步
                Console.WriteLine("准备同步发起请求");
                DateTime dateTime = DateTime.Now;
                string s_formattedDateTime = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
                string s_enstr = s_rnum + "##" + s_formattedDateTime;
                Console.WriteLine("机器码和时间戳" + s_enstr);
                string s_enstrCode = myAES.AesEncrypt(s_enstr);
                Console.WriteLine("加密后" + s_enstrCode);
                string s_enstrCodeBase64 = HttpUtil.Base64Encrypt(s_enstrCode);
                Console.WriteLine("base64后" + s_enstrCodeBase64);
                IDictionary<string, string> dic_parameters = new Dictionary<string, string>();
                dic_parameters.Add("msg", s_enstrCodeBase64);
                dic_parameters.Add("sendtime", s_formattedDateTime);
                dic_parameters.Add("Version", FADM_Object.Communal._s_version);
                HttpWebResponse response = HttpUtil.CreatePostHttpResponse(FADM_Object.Communal.URL + "/outer/product/getDySyn", dic_parameters, 15000, null, null);
                Stream st = response.GetResponseStream();
                StreamReader reader = new StreamReader(st);
                string s_msg = reader.ReadToEnd();
                Console.WriteLine("返回结果是");  //estring str = "s_errcode=1#day=天数#s_time=时间戳#s_isreg=true#s_msg=";
                Console.WriteLine(s_msg);
                return s_msg;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }

        }




    }
}
