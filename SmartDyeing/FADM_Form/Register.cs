using com.google.zxing.common;
using com.google.zxing;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartDyeing.FADM_Form
{
    public partial class Register : Form
    {
        public static string _s_mNum;

        public Register()
        {
            InitializeComponent();
        }
        
        private void btnReg_Click(object sender, EventArgs e)
        {
            try
            {
                Console.WriteLine(FADM_Object.Communal._softReg.GetRNum());
                if (txtLicence.Text == FADM_Object.Communal._softReg.GetRNum())
                {
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        FADM_Form.CustomMessageBox.Show("注册成功！重启软件后生效！", "信息", MessageBoxButtons.OK, false);
                    else
                        FADM_Form.CustomMessageBox.Show("Registration successful! Take effect after restarting the software！", "Info", MessageBoxButtons.OK, false);
                    RegistryKey retkey = Registry.CurrentUser.OpenSubKey("Software", true).CreateSubKey("chec").CreateSubKey("Register.INI").CreateSubKey(FADM_Object.Communal._softReg.GetRNum());
                    retkey.SetValue("UserName", "Rsoft");
                    this.Close();
                }
                else
                {
                    if (txtLicence.Text.Length == 32)
                    {
                        string s_head = txtLicence.Text.Substring(0, 4);
                        if (s_head != "GZKL")
                        {
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                FADM_Form.CustomMessageBox.Show("注册码错误！", "警告", MessageBoxButtons.OK, false);
                            else
                                FADM_Form.CustomMessageBox.Show("Registration code error！", "warn", MessageBoxButtons.OK, false);
                            txtLicence.SelectAll();
                        }
                        else
                        {
                            try
                            {
                                string s_year = Convert.ToInt32("0x" + txtLicence.Text.Substring(7, 4), 16).ToString();
                                string s_month = Convert.ToInt32("0x" + txtLicence.Text.Substring(14, 2), 16).ToString();
                                string s_dayNow = Convert.ToInt32("0x" + txtLicence.Text.Substring(22, 2), 16).ToString();
                                string s_date = s_year + "/" + s_month + "/" + s_dayNow;
                                string s_dateNow = string.Format("{0:d}", DateTime.Now.Date);
                                string s_temp = txtLicence.Text.Substring(24, 8);
                                if (s_dateNow == s_date && s_temp == FADM_Object.Communal._softReg.GetRNumDS())
                                {
                                    string s_day_1 = txtLicence.Text.Substring(5, 1);
                                    string s_day_2 = txtLicence.Text.Substring(11, 1);
                                    string s_day_3 = txtLicence.Text.Substring(18, 1);
                                    string s_day = s_day_1 + s_day_2 + s_day_3;

                                    int i_day = Convert.ToInt16(s_day);

                                    string s_enNow = FADM_Object.myAES.AesEncrypt(DateTime.Now.ToString());
                                    Console.WriteLine(s_enNow);
                                    //修改创建时间
                                    Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\chec", "CreateDateTime", s_enNow, RegistryValueKind.String);

                                    string s_enP_int_day = FADM_Object.myAES.AesEncrypt(i_day.ToString());
                                    //创建允许天数
                                    Console.WriteLine(s_enP_int_day);
                                    Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\chec", "UseTimes", s_enP_int_day, RegistryValueKind.String);

                                     FADM_Form.CustomMessageBox.Show("试用期延长" + i_day + "天成功！", "信息", MessageBoxButtons.OK, false);
                                    (this.Owner as SmartDyeing.FADM_Form.Main).countDown();
                                    this.Close();

                                }
                                else
                                {
                                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                        FADM_Form.CustomMessageBox.Show("注册码错误！", "警告", MessageBoxButtons.OK, false);
                                    else
                                        FADM_Form.CustomMessageBox.Show("Registration code error！", "warn", MessageBoxButtons.OK, false);
                                    txtLicence.SelectAll();
                                }
                            }
                            catch
                            {
                                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                    FADM_Form.CustomMessageBox.Show("注册码错误！", "警告", MessageBoxButtons.OK, false);
                                else
                                    FADM_Form.CustomMessageBox.Show("Registration code error！", "warn", MessageBoxButtons.OK, false);
                                txtLicence.SelectAll();
                            }
                        }
                    }
                    else
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            FADM_Form.CustomMessageBox.Show("注册码错误！", "警告", MessageBoxButtons.OK, false);
                        else
                            FADM_Form.CustomMessageBox.Show("Registration code error！", "warn", MessageBoxButtons.OK, false);
                        txtLicence.SelectAll();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void Register_Load(object sender, EventArgs e)
        {
            if (FADM_Object.Communal._b_registerOld)
            {
                this.pictureBox2.Visible = false;
                this.button1.Visible = false;
                this.label2.Visible = true;
                this.txtLicence.Visible = true;
                this.btnReg.Visible = true;
            }


            _s_mNum = FADM_Object.Communal._softReg.GetMNum();
            //判断软件是否注册
            RegistryKey retkey = Registry.CurrentUser.OpenSubKey("SOFTWARE", true).CreateSubKey("mySoftWare").CreateSubKey("Register.INI");
            foreach (string strRNum in retkey.GetSubKeyNames())
            {
                if (strRNum == FADM_Object.Communal._softReg.GetRNum())
                {
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    {
                        this.labRegInfo.Text = "已注册";
                    }
                    else
                    {
                        this.labRegInfo.Text = "Registered";
                    }
                    this.btnReg.Enabled = false;
                    this.txtLicence.Enabled = false;
                    return;
                }
            }
            if (Lib_Card.Configure.Parameter.Other_Language == 0)
            {
                this.labRegInfo.Text = "未注册";
            }
            else
            {
                this.labRegInfo.Text = "NotRegistered";

            }
            this.btnReg.Enabled = true;
            this.txtLicence.Enabled = true;

            showZxing();

        }
        public void writeToFile(ByteMatrix matrix, System.Drawing.Imaging.ImageFormat format, string file)
        {
            Bitmap bmap = toBitmap(matrix);
            pictureBox1.Image = bmap;
        }

        public Bitmap toBitmap(ByteMatrix matrix)
        {
            int i_width = matrix.Width;
            int i_height = matrix.Height;
            Bitmap bmap = new Bitmap(i_width, i_height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            for (int x = 0; x < i_width; x++)
            {
                for (int y = 0; y < i_height; y++)
                {
                    bmap.SetPixel(x, y, matrix.get_Renamed(x, y) != -1 ? ColorTranslator.FromHtml("0xFF000000") : ColorTranslator.FromHtml("0xf6f1f1"));
                }
            }
            return bmap;
        }

        private void showZxing()
        {
            ByteMatrix byteMatrix = new MultiFormatWriter().encode(_s_mNum+"##"+ FADM_Object.Communal._s_version, BarcodeFormat.QR_CODE, 180, 180);
            writeToFile(byteMatrix, System.Drawing.Imaging.ImageFormat.Png, "");

        }
        private void Register_FormClosing(object sender, FormClosingEventArgs e)
        {
            (this.Owner as SmartDyeing.FADM_Form.Main).MiRegister.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox2.Visible = true;
                string s_msg = FADM_Object.MyRegister.SyncDy(_s_mNum);
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
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                FADM_Form.CustomMessageBox.Show("注册成功！重启软件后生效！", "信息", MessageBoxButtons.OK, false);
                            else
                                FADM_Form.CustomMessageBox.Show("Registration successful! Take effect after restarting the software！", "Info", MessageBoxButtons.OK, false);
                            RegistryKey retkey = Registry.CurrentUser.OpenSubKey("Software", true).CreateSubKey("chec").CreateSubKey("Register.INI").CreateSubKey(FADM_Object.Communal._softReg.GetRNum());
                            retkey.SetValue("UserName", "Rsoft");
                            this.Close();
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
                                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                    FADM_Form.CustomMessageBox.Show("同步失败!" + "请先校准系统时间!", "警告", MessageBoxButtons.OK, false);
                                else
                                    FADM_Form.CustomMessageBox.Show("Sync failed!" + "Please calibrate the system time first!", "warn", MessageBoxButtons.OK, false);
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
                                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                    FADM_Form.CustomMessageBox.Show("试用期延长" + i_day + "天成功！", "信息", MessageBoxButtons.OK, false);
                                else
                                    FADM_Form.CustomMessageBox.Show("Trial period extended by "+ i_day + " days successfully" , "Info", MessageBoxButtons.OK, false);
                                (this.Owner as SmartDyeing.FADM_Form.Main).countDown();
                                this.Close();
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
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            FADM_Form.CustomMessageBox.Show("已被锁定,请联系管理员!", "信息", MessageBoxButtons.OK, false);
                        else
                            FADM_Form.CustomMessageBox.Show("Locked, please contact the administrator!", "Info", MessageBoxButtons.OK, false);
                        (this.Owner as SmartDyeing.FADM_Form.Main).countDown();
                        this.Close();
                    }
                    else
                    {
                        FADM_Form.CustomMessageBox.Show("同步失败!" + sa_array[4], "警告", MessageBoxButtons.OK, false);
                    }
                }
            }
            catch (Exception ex)
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    FADM_Form.CustomMessageBox.Show("网络连接中断!", "信息", MessageBoxButtons.OK, false);
                else
                    FADM_Form.CustomMessageBox.Show("Network connection interrupted!", "Info", MessageBoxButtons.OK, false);
            }
        }
    }
}
