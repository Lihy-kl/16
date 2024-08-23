using Lib_File;
using SmartDyeing.FADM_Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Speech.Synthesis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SmartDyeing.FADM_Object;
using System.IO;
using Microsoft.Win32;
using static Lib_Card.CardObject;
using EasyModbus;
using static System.Windows.Forms.AxHost;
using System.Collections;
using SmartDyeing.FADM_Auto;

namespace SmartDyeing.FADM_Form
{
    public partial class Main : Form
    {
        [DllImport("user32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Auto)]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        public const int WM_CLOSE = 0x10;

        Thread _thread_check = null;
        public Main()
        {
            InitializeComponent();
        }
        private void Main_Load(object sender, EventArgs e)
        {
            string s_path = Environment.CurrentDirectory + "\\Config\\Config.ini";
            string s_balanceInDrip = Lib_File.Ini.GetIni("Setting", "IsBalanceInDrip", "1", s_path);
            if (s_balanceInDrip == "0")
            {
                FADM_Object.Communal._b_isBalanceInDrip = false;
            }

            BtnMain_Click(sender, e);

            Thread thread = new Thread(ReadBalance); //读取天平数据和光幕数据
            thread.IsBackground = true;
            thread.Start();

            //Thread thread3 = new Thread(ReadBalance1); //读取天平数据和光幕数据
            //thread3.IsBackground = true;
            //thread3.Start();

            //Thread thread2 = new Thread(WriteBalance1); //读取天平数据和光幕数据
            //thread2.IsBackground = true;
            //thread2.Start();



            for (int i = 0; i < FADM_Auto.Dye._ia_stop.Count(); i++)
            {
                FADM_Auto.Dye._ia_stop[i] = 0;
                FADM_Auto.Dye._ia_stopSend[i] = 0;
            }

            Thread thread1 = new Thread(new FADM_Auto.Dye().ClothDyeing); //打板机相关线程
            thread1.IsBackground = true;
            thread1.Start();

            System.Threading.Thread P_thd_brew = new System.Threading.Thread(SmartDyeing.FADM_Auto.MyBrew.Brew);
            P_thd_brew.IsBackground = true;
            P_thd_brew.Start();

            //播报线程
            Thread P_thd = new Thread(Speech);
            P_thd.IsBackground = true;
            P_thd.Start();

            //自动加入批次
            Thread P_thd1 = new Thread(WaitList);
            P_thd1.IsBackground = true;
            P_thd1.Start();

            //开启线程，自动备份90天之前的数据
            Thread P_thd2 = new Thread(BackUp);
            P_thd2.IsBackground = true;
            P_thd2.Start();

            //设置分光仪信息
            string s_isUse = Lib_File.Ini.GetIni("Spectrometer", "IsUse", s_path);
            this._s_m_route = Lib_File.Ini.GetIni("Spectrometer", "Route", s_path);

            double d_mytime = Convert.ToDouble(Lib_File.Ini.GetIni("Spectrometer", "Time", s_path) == "" ? "0" : Lib_File.Ini.GetIni("Spectrometer", "Time", s_path));

            if (_s_m_route != "" && s_isUse == "1")
            {
                TmrFGY.Interval = Convert.ToInt16(d_mytime * 1000);
                TmrFGY.Enabled = true;
            }

            //string sPath = Environment.CurrentDirectory + "\\Config.ini";
            string s_out = Lib_File.Ini.GetIni("Setting", "IsOutDrip", "0", s_path);
            if (s_out == "0")
            {
                MiOut1.Checked = false;
                FADM_Object.Communal._b_isOutDrip = false;
            }
            else
            {
                MiOut1.Checked = true;
                FADM_Object.Communal._b_isOutDrip = true;
            }
            string s_low = Lib_File.Ini.GetIni("Setting", "IsLowDrip", "0", s_path);
            if (s_low == "0")
            {
                MiLow1.Checked = false;
                FADM_Object.Communal._b_isLowDrip = false;
            }
            else
            {
                MiLow1.Checked = true;
                FADM_Object.Communal._b_isLowDrip = true;
            }
            string s_full = Lib_File.Ini.GetIni("Setting", "IsFullDrip", "1", s_path);
            if (s_full == "0")
            {
                MiFullDrip1.Checked = false;
                FADM_Object.Communal._b_isFullDrip = false;
            }
            else
            {
                MiFullDrip1.Checked = true;
                FADM_Object.Communal._b_isFullDrip = true;
            }

            string s_zero = Lib_File.Ini.GetIni("Setting", "IsZero", "0", s_path);
            if (s_zero == "1")
            {
                FADM_Object.Communal._b_isZero = true;
            }

            string s_registerOld = Lib_File.Ini.GetIni("Setting", "registerOld", "0", s_path);
            if (s_registerOld == "0")
            {
                FADM_Object.Communal._b_registerOld = false;
            }
            else
            {
                FADM_Object.Communal._b_registerOld = true;
            }

            string s_isNetWork = Lib_File.Ini.GetIni("Setting", "IsNetWork", "0", s_path);
            if (s_isNetWork == "0")
            {
                FADM_Object.Communal._b_isNetWork = false;
            }
            else
            {
                FADM_Object.Communal._b_isNetWork = true;
            }



            string s_dripAll = Lib_File.Ini.GetIni("Setting", "IsDripAll", "0", s_path);
            if (s_dripAll == "1")
            {
                FADM_Object.Communal._b_isDripAll = true;
            }
            string s_assitantFirst = Lib_File.Ini.GetIni("Setting", "IsAssitantFirst", "0", s_path);
            if (s_assitantFirst == "1")
            {
                FADM_Object.Communal._b_isAssitantFirst = true;
            }
            string s_needCheck = Lib_File.Ini.GetIni("Setting", "IsNeedCheck", "1", s_path);
            if (s_needCheck == "0")
            {
                FADM_Object.Communal._b_isNeedCheck = false;
            }
            string s_finishSend = Lib_File.Ini.GetIni("Setting", "IsFinishSend", "1", s_path);
            if (s_finishSend == "0")
            {
                FADM_Object.Communal._b_isFinishSend = false;
            }
            string s_dripReserveFirst = Lib_File.Ini.GetIni("Setting", "IsDripReserveFirst", "1", s_path);
            if (s_dripReserveFirst == "0")
            {
                FADM_Object.Communal._b_isDripReserveFirst = false;
            }
            string s_debug = Lib_File.Ini.GetIni("Setting", "IsDebug", "0", s_path);
            if (s_debug == "1")
            {
                FADM_Object.Communal._b_isDebug = true;
            }
            string s_saveRealConcentration = Lib_File.Ini.GetIni("Setting", "IsSaveRealConcentration", "0", s_path);
            if (s_saveRealConcentration == "1")
            {
                FADM_Object.Communal._b_isSaveRealConcentration = true;
            }

            string s_useClamp = Lib_File.Ini.GetIni("Setting", "IsUseClamp", "0", s_path);
            if (s_useClamp == "1")
            {
                FADM_Object.Communal._b_isUseClamp = true;
            }

            string isUseAuto = Lib_File.Ini.GetIni("Setting", "IsUseAuto", "1", s_path);
            if (isUseAuto == "0")
            {
                FADM_Object.Communal._b_isUseAuto = false;
            }

            countDown();
        }

        public void countDown()
        {
            SmartDyeing.FADM_Object.MyRegister softReg = new SmartDyeing.FADM_Object.MyRegister();
            RegistryKey retkey = Registry.CurrentUser.OpenSubKey("SOFTWARE", true).CreateSubKey("chec").CreateSubKey("Register.INI");
            foreach (string strRNum in retkey.GetSubKeyNames())
            {
                if (strRNum == softReg.GetRNum())
                {
                    this.toolStripLabel1.Text = "";
                    return;
                }
            }

            int i_usetime = 0;

            DateTime P_dt_create = new DateTime();

            DateTime P_dt_last = new DateTime();

            DateTime P_dt_now = DateTime.Now;
        again:

            try
            {
                if(Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\chec", "UseTimes", null) == null)
                {
                    //创建允许天数
                    string s_enP_int_day = FADM_Object.myAES.AesEncrypt("7");
                    Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\chec", "UseTimes", s_enP_int_day, RegistryValueKind.String);

                }
                if(Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\chec", "CreateDateTime", null)==null)
                {
                    string s_enNow = FADM_Object.myAES.AesEncrypt(DateTime.Now.ToString());
                    //修改创建时间
                    Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\chec", "CreateDateTime", s_enNow, RegistryValueKind.String);
                }
                //获取允许使用次数 天数
                string s_usetimeStr = (string)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\chec", "UseTimes", "");
                s_usetimeStr = FADM_Object.myAES.AesDecrypt(s_usetimeStr);
                i_usetime = Convert.ToInt32(s_usetimeStr);

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

            if ((P_dt_now - P_dt_create).Days < i_usetime)
            {
                P_dt_create = P_dt_create.AddDays(i_usetime);
                string s_end = Convert.ToString(P_dt_create);
                s_end = s_end.Replace("/", "-");
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                {
                    this.toolStripLabel1.Text = "有效日期至:" + s_end;
                }
                else
                {
                    this.toolStripLabel1.Text = "ValidUntil:" + s_end;
                }
            }
            else
            {
                Communal._b_isexpired = true;
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                {
                    this.toolStripLabel1.Text = "软件过期,请联系供应商!";
                }
                else
                {
                    this.toolStripLabel1.Text = "The software has expired, please contact the supplier!";
                }
            }

        }
        private void Speech()
        {
            while (true)
            {
                try
                {
                    Thread.Sleep(1);
                    //查询所有播报信息

                    var sortedKeys = Lib_Card.CardObject.keyValuePairs.Keys.OrderByDescending(k => k);

                    //有报警
                    if(sortedKeys.Any())
                    {
                        if(!Communal._b_isBSendOpen)
                        {
                            Communal._i_bType = 1;
                        }
                    }
                    //无播报
                    else
                    {
                        if (!Communal._b_isBSendClose)
                        {
                            Communal._i_bType = 2;
                        }
                    }

                    foreach (var key in sortedKeys)
                    {
                        if (!Lib_Card.CardObject.keyValuePairs[key].Speech)
                        {
                            Lib_Card.CardObject.prompt prompt = new Lib_Card.CardObject.prompt();
                            prompt = Lib_Card.CardObject.keyValuePairs[key];
                            prompt.Speech = true;
                            Lib_Card.CardObject.keyValuePairs[key] = prompt;
                        }

                        SpeechSynthesizer speech = new SpeechSynthesizer();
                        speech.Rate = -1; //语速
                        speech.Volume = 100; //声音
                        speech.SelectVoice("Microsoft Huihui Desktop");//设置中文
                        string s_info = Lib_Card.CardObject.keyValuePairs[key].Info;
                        speech.Speak(s_info);

                    }


                }
                catch
                {

                }
            }
        }

        private void WaitList()
        {
            while (true)
            {
                try
                {
                    if (!FADM_Object.Communal._b_isOpenWaitList && (LabStatus.Text == "待机"|| LabStatus.Text == "Standby") && FADM_Object.Communal._b_finshRun && !FADM_Object.Communal._b_isDripping)
                    {
                        bool b_newInsert = false;
                        FADM_Object.Communal._b_isDripping = true;
                        //先查询空闲固定色杯号
                        string s_sqltemp = "SELECT  CupNum FROM cup_details WHERE   IsUsing = 0 and IsFixed = 1 and Statues = '待机' and enable = 1 and Type = 3 order by CupNum ;";
                        DataTable dt_temp = FADM_Object.Communal._fadmSqlserver.GetData(s_sqltemp);

                        foreach (DataRow Row in dt_temp.Rows)
                        {
                            string P_str_sqltemp1 = "SELECT  * FROM wait_list WHERE  CupNum = " + Row[0].ToString() + " and Type = 3  order by IndexNum;";
                            DataTable dataTabletemp1 = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sqltemp1);

                            //查询是否在drop_head里存在该杯
                            string str_head = "SELECT  * FROM drop_head WHERE  CupNum = " + Row[0].ToString() + " ;";
                            DataTable dt_head = FADM_Object.Communal._fadmSqlserver.GetData(str_head);

                            if (dataTabletemp1.Rows.Count > 0 && dt_head.Rows.Count == 0)
                            {
                                b_newInsert = true;
                                //加入批次
                                AddDropList a = new AddDropList(dataTabletemp1.Rows[0]["FormulaCode"].ToString(), dataTabletemp1.Rows[0]["VersionNum"].ToString(), Row[0].ToString(), 3);
                                //删除等待列表记录
                                FADM_Object.Communal._fadmSqlserver.GetData("Delete from wait_list where Type = 3 and IndexNum = " + dataTabletemp1.Rows[0]["IndexNum"].ToString());
                            }
                        }

                        //查询空闲自动分配杯号（后处理类型）
                        s_sqltemp = "SELECT  CupNum FROM cup_details WHERE   IsUsing = 0 and IsFixed = 0 and Statues = '待机' and enable = 1 and Type = 3 order by CupNum ;";
                        dt_temp = FADM_Object.Communal._fadmSqlserver.GetData(s_sqltemp);

                        string s_sqltemp2 = "SELECT  * FROM wait_list WHERE  CupNum = 0 and Type = 3  order by IndexNum;";
                        DataTable dt_temp2 = FADM_Object.Communal._fadmSqlserver.GetData(s_sqltemp2);

                        int i_num = 0;
                        foreach (DataRow Row in dt_temp.Rows)
                        {
                            //查询是否在drop_head里存在该杯
                            string s_head = "SELECT  * FROM drop_head WHERE  CupNum = " + Row[0].ToString() + " ;";
                            DataTable dt_head = FADM_Object.Communal._fadmSqlserver.GetData(s_head);

                            if (dt_temp2.Rows.Count > i_num && dt_head.Rows.Count == 0)
                            {
                                b_newInsert = true;
                                //加入批次
                                AddDropList a = new AddDropList(dt_temp2.Rows[i_num]["FormulaCode"].ToString(), dt_temp2.Rows[i_num]["VersionNum"].ToString(), Row[0].ToString(), 3);
                                //删除等待列表记录
                                FADM_Object.Communal._fadmSqlserver.GetData("Delete from wait_list where Type = 3 and IndexNum = " + dt_temp2.Rows[i_num]["IndexNum"].ToString());

                                i_num++;
                            }

                        }

                        //查询空闲自动分配杯号（滴液类型）
                        s_sqltemp = "SELECT  CupNum FROM cup_details WHERE   IsUsing = 0 and  enable = 1 and Type = 2 order by CupNum ;";
                        dt_temp = FADM_Object.Communal._fadmSqlserver.GetData(s_sqltemp);

                        string s_sqltemp3 = "SELECT  * FROM wait_list WHERE  Type = 2  order by IndexNum;";
                        DataTable dt_temp3 = FADM_Object.Communal._fadmSqlserver.GetData(s_sqltemp3);

                        int i_num2 = 0;
                        bool b_type2 = false;
                        foreach (DataRow Row in dt_temp.Rows)
                        {
                            //查询是否在drop_head里存在该杯
                            string s_head = "SELECT  * FROM drop_head WHERE  CupNum = " + Row[0].ToString() + " ;";
                            DataTable dt_head = FADM_Object.Communal._fadmSqlserver.GetData(s_head);

                            if (dt_temp3.Rows.Count > i_num2 && dt_head.Rows.Count == 0)
                            {
                                b_newInsert = true;
                                //加入批次
                                AddDropList a = new AddDropList(dt_temp3.Rows[i_num2]["FormulaCode"].ToString(), dt_temp3.Rows[i_num2]["VersionNum"].ToString(), Row[0].ToString(), 2);
                                //删除等待列表记录
                                FADM_Object.Communal._fadmSqlserver.GetData("Delete from wait_list where Type = 2 and IndexNum = " + dt_temp3.Rows[i_num2]["IndexNum"].ToString());

                                i_num2++;
                                b_type2 = true;
                            }

                        }
                        bool b_p=false;
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        {
                            if (!toolStripLabel1.Text.Contains("过期") && !toolStripLabel1.Text.Contains("修改"))
                            {
                                b_p = true;
                            }
                        }
                        else
                        {
                            if (!toolStripLabel1.Text.Contains("expired") && !toolStripLabel1.Text.Contains("modify"))
                            {
                                b_p = true;
                            }
                        }

                        if (b_p && FADM_Object.Communal._b_isUseAuto)
                        {
                            try
                            {
                                string s_sql2 = "SELECT * FROM drop_head Where BatchName = '0' and Stage = '后处理';";
                                DataTable dt_data2 = FADM_Object.Communal._fadmSqlserver.GetData(s_sql2);
                                if (/*newInsert || */dt_data2.Rows.Count > 0)
                                {
                                    while (true)
                                    {
                                        if (LabStatus.Text == "待机" || LabStatus.Text == "Standby")
                                        {
                                            break;
                                        }
                                        Thread.Sleep(1);
                                    }
                                    if (LabStatus.Text == "待机" || LabStatus.Text == "Standby")
                                    {

                                        //获取之前批次号
                                        string s_sql = "SELECT BatchName FROM enabled_set Where MyID = 1;";
                                        DataTable dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                                        string s_batchNum_last = Convert.ToString(dt_data.Rows[0][dt_data.Columns[0]]);

                                        //计算当前批次号
                                        string s_batchNum = null;
                                        if (s_batchNum_last == "0")
                                        {
                                            //初始状态
                                            int i_no = 1;
                                            s_batchNum = DateTime.Now.ToString("yyyyMMdd") + i_no.ToString("d4");
                                        }
                                        else
                                        {
                                            string s_date = s_batchNum_last.Substring(0, 8);
                                            string s_no = s_batchNum_last.Substring(8, 4);

                                            if (s_date == DateTime.Now.ToString("yyyyMMdd"))
                                            {
                                                s_batchNum = s_date + (Convert.ToInt32(s_no) + 1).ToString("d4");
                                            }
                                            else
                                            {
                                                int i_no = 1;
                                                s_batchNum = DateTime.Now.ToString("yyyyMMdd") + i_no.ToString("d4");
                                            }
                                        }

                                        //修改当前批次号
                                        s_sql = "UPDATE enabled_set SET BatchName = '" + s_batchNum + "'" +
                                                    " WHERE MyID = 1;";
                                        FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                                        //查询修改该配方为已滴定配方
                                        s_sql = "SELECT * FROM drop_head Where BatchName = '0'  and Stage = '后处理';";
                                        dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                                        foreach (DataRow dr in dt_data.Rows)
                                        {
                                            //写入配方浏览表
                                            s_sql = "UPDATE formula_head SET State = '已滴定配方'" +
                                                       " WHERE FormulaCode = '" + dr["FormulaCode"] + "' AND" +
                                                       " VersionNum = " + dr["VersionNum"] + " ;";
                                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                                            //修改批次表头批次号
                                            s_sql = "UPDATE drop_head SET BatchName = '" + s_batchNum + "', State = '已滴定配方',Step = 1 where  BatchName = '0' and CupNum = '" + dr["CupNum"] + "';";
                                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                                            //修改批次详细资料表批次号
                                            s_sql = "UPDATE drop_details SET BatchName = '" + s_batchNum + "' where  BatchName = '0' and CupNum = '" + dr["CupNum"] + "';";
                                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                                            s_sql = "UPDATE dye_details SET BatchName = '" + s_batchNum + "' where  BatchName = '0' and CupNum = '" + dr["CupNum"] + "';";
                                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                                        }




                                        Thread thread = new Thread(() =>
                                        {
                                            new FADM_Auto.Drip().DripLiquid(s_batchNum);
                                            FADM_Control.Formula.P_bl_update = true;
                                        });
                                        thread.Start();
                                        b_newInsert = false;
                                        FADM_Control.Formula._b_updateWait = true;
                                        FADM_Object.Communal._b_isDripping = false;
                                    }
                                }

                                if (b_type2)
                                    FADM_Control.Formula._b_updateWait = true;
                                FADM_Object.Communal._b_isDripping = false;

                            }
                            catch
                            {
                                FADM_Object.Communal._b_isDripping = false;
                            }
                        }
                        else
                        {
                            FADM_Control.Formula._b_updateWait = true;
                            FADM_Object.Communal._b_isDripping = false;
                        }
                    }



                    //1分钟一次
                    Thread.Sleep(60000);
                }
                catch
                {
                    FADM_Object.Communal._b_isDripping = false;
                }
            }
        }

        private void ReadBalance()
        {
            
            while (true)
            {
                Thread.Sleep(60);  //这里一直读 如果同时地方也同时读 就有可能造成读不到 
                //
                if (FADM_Object.Communal.ReadTcpStatus() && FADM_Object.Communal._b_auto)
                {
                    if (!Communal._b_isBSendOpen)
                    {
                        if (Communal._i_bType == 1)
                        {
                            int[] ia_array12 = { 17 };
                            int i_state12 = FADM_Object.Communal._tcpModBus.Write(811, ia_array12);

                            Communal._b_isBSendClose = false;
                            Communal._b_isBSendOpen = true;
                        }
                    }

                    if (!Communal._b_isBSendClose)
                    {
                        if (Communal._i_bType == 2)
                        {
                            int[] ia_array12 = { 18 };
                            int i_state12 = FADM_Object.Communal._tcpModBus.Write(811, ia_array12);

                            Communal._b_isBSendOpen = false;
                            Communal._b_isBSendClose = true;
                        }
                    }

                    //Console.WriteLine("读天平");
                    //只读天平寄存器 
                    int[] ia_array = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                    int i_state = FADM_Object.Communal._tcpModBus.Read(901, 28, ref ia_array);
                    if (i_state != -1)
                    {
                        MyAlarm myAlarm = null;

                        Communal._s_plcVersion = "V"+ ia_array[26].ToString("d4")+ ia_array[27].ToString("d4");

                        double d_b = 0.0;
                        if (ia_array[0] < 0)
                        {
                            d_b = ((ia_array[1] + 1) * 65536 + ia_array[0]) / 1000.0;
                        }
                        else
                        {
                            d_b = (ia_array[1] * 65536 + ia_array[0]) / 1000.0;
                        }
                        //if(Lib_Card.Configure.Parameter.Machine_BalanceType == 1)
                        //{
                        //    b /= 10;
                        //}




                        if (Convert.ToString(d_b).Equals("9999"))
                        {
                            FADM_Object.Communal._s_balanceValue = "9999";
                            //myAlarm = new FADM_Object.MyAlarm("天平超上限", "天平", true, 1);
                        }
                        else if (Convert.ToString(d_b).Equals("8888"))
                        {
                            FADM_Object.Communal._s_balanceValue = "8888";
                            //myAlarm = new FADM_Object.MyAlarm("天平超下限", "天平", true, 1);
                        }
                        else if (Convert.ToString(d_b).Equals("7777"))
                        {
                            FADM_Object.Communal._s_balanceValue = "7777";
                            //myAlarm = new FADM_Object.MyAlarm("未拿走废液桶", "天平", true, 1);
                        }
                        else if (Convert.ToString(d_b).Equals("6666"))
                        {
                            FADM_Object.Communal._s_balanceValue = "6666";
                            //myAlarm = new FADM_Object.MyAlarm("通讯失败 ", "天平", true, 1);
                        }
                        else
                        {
                            FADM_Object.Communal._s_balanceValue = Convert.ToString(d_b);
                        }

                        //失能信号
                        int i_a5 = ia_array[5];
                        bool b_xpower = false;
                        bool b_ypower = false;
                        char[] ca_cc1 = Convert.ToString(i_a5, 2).PadLeft(12, '0').ToArray();
                        if (ca_cc1[ca_cc1.Length - 6].Equals('1'))
                        {
                            b_xpower = true;
                        }
                        if (ca_cc1[ca_cc1.Length - 12].Equals('1'))
                        {
                            b_ypower = true;
                        }
                        //if (xpower || ypower)
                        {
                            //光幕信号 这上面的MyAlarm不要等待
                            int i_a2 = ia_array[2];
                            char[] ca_cc = Convert.ToString(i_a2, 2).PadLeft(3, '0').ToArray();
                            if (ca_cc[ca_cc.Length - 1].Equals('0'))
                            {

                                if (Lib_Card.Configure.Parameter.Machine_IsStopOrFront == 0)
                                {
                                    if (!Lib_Card.CardObject.bStopScr)
                                    {
                                        Lib_Card.CardObject.bStopScr = true;
                                        if (b_xpower || b_ypower)
                                        {
                                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                                new FADM_Object.MyAlarm("急停已按下", 1);
                                            else
                                                new FADM_Object.MyAlarm("Emergency stop pressed", 1);
                                        }
                                    }
                                }
                                else
                                {
                                    if (!Lib_Card.CardObject.bFront)
                                    {
                                        Lib_Card.CardObject.bFront = true;
                                        if (b_xpower || b_ypower)
                                        {
                                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                                new FADM_Object.MyAlarm("前光幕遮挡", 1);
                                            else
                                                new FADM_Object.MyAlarm("Front light curtain obstruction", 1);
                                        }
                                    }
                                }

                            }
                            else
                            {
                                if (Lib_Card.Configure.Parameter.Machine_IsStopOrFront == 0)
                                {
                                    Lib_Card.CardObject.bStopScr = false;
                                }
                                else
                                {
                                    Lib_Card.CardObject.bFront = false;
                                }
                            }



                            if (ca_cc[ca_cc.Length - 3].Equals('0'))
                            {
                                if (!Lib_Card.CardObject.bRight)
                                {
                                    Lib_Card.CardObject.bRight = true;
                                    if (b_xpower || b_ypower)
                                    {
                                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                        {
                                            if (Lib_Card.Configure.Parameter.Machine_IsRightDoor == 1)
                                                new FADM_Object.MyAlarm("右光幕遮挡", 1);
                                            else
                                                new FADM_Object.MyAlarm("右门已打开", 1);
                                        }
                                        else
                                        {
                                            if (Lib_Card.Configure.Parameter.Machine_IsRightDoor == 1)
                                                new FADM_Object.MyAlarm("Right light curtain obstruction", 1);
                                            else
                                                new FADM_Object.MyAlarm("The right door is open", 1);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Lib_Card.CardObject.bRight = false;
                            }
                            //左光幕信号
                            if (ca_cc[ca_cc.Length - 2].Equals('0'))
                            {
                                if (!Lib_Card.CardObject.bLeft)
                                {
                                    Lib_Card.CardObject.bLeft = true;
                                    if (b_xpower || b_ypower)
                                    {
                                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                        {
                                            if (Lib_Card.Configure.Parameter.Machine_IsRightDoor == 1)
                                                new FADM_Object.MyAlarm("右光幕遮挡", 1);
                                            else
                                                new FADM_Object.MyAlarm("右门已打开", 1);
                                        }
                                        else
                                        {
                                            if (Lib_Card.Configure.Parameter.Machine_IsRightDoor == 1)
                                                new FADM_Object.MyAlarm("Right light curtain obstruction", 1);
                                            else
                                                new FADM_Object.MyAlarm("The right door is open", 1);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Lib_Card.CardObject.bLeft = false;
                            }
                        }
                    }
                    else
                    {
                        //重新连接试试
                        FADM_Object.Communal._tcpModBus.ReConnect();
                    }
                }
                else if (!FADM_Object.Communal.ReadTcpStatus() || !FADM_Object.Communal._b_auto) //说明在执行动作编号后 一直循环读取完成标志位，
                {
                    if (Lib_Card.Configure.Parameter.Machine_IsStopOrFront == 0)
                    {
                        if (Lib_Card.CardObject.bStopScr)
                        {
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                new FADM_Object.MyAlarm("急停已按下", 1);
                            else
                                new FADM_Object.MyAlarm("Emergency stop pressed", 1);
                        }
                        else
                        {
                            Lib_Card.CardObject.bStopScr = false;
                        }
                    }
                    else
                    {

                        if (Lib_Card.CardObject.bFront)
                        {
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                new FADM_Object.MyAlarm("前光幕遮挡", 1);
                            else
                                new FADM_Object.MyAlarm("Front light curtain obstruction", 1);
                        }
                        else
                        {
                            Lib_Card.CardObject.bFront = false;
                        }
                    }

                    if (Lib_Card.CardObject.bRight)
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        {
                            if (Lib_Card.Configure.Parameter.Machine_IsRightDoor == 1)
                                new FADM_Object.MyAlarm("右光幕遮挡", 1);
                            else
                                new FADM_Object.MyAlarm("右门已打开", 1);
                        }
                        else
                        {
                            if (Lib_Card.Configure.Parameter.Machine_IsRightDoor == 1)
                                new FADM_Object.MyAlarm("Right light curtain obstruction", 1);
                            else
                                new FADM_Object.MyAlarm("The right door is open", 1);
                        }
                    }
                    else {
                        Lib_Card.CardObject.bRight = false;
                    }

                    if (Lib_Card.CardObject.bLeft)
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        {
                            if (Lib_Card.Configure.Parameter.Machine_IsLeftDoor == 1)
                                new FADM_Object.MyAlarm("左光幕遮挡", 1);
                            else
                                new FADM_Object.MyAlarm("左门已打开", 1);
                        }
                        else
                        {
                            if (Lib_Card.Configure.Parameter.Machine_IsLeftDoor == 1)
                                new FADM_Object.MyAlarm("Left light curtain obstruction", 1);
                            else
                                new FADM_Object.MyAlarm("The left door is open", 1);
                        }
                    }
                    else
                    {
                        Lib_Card.CardObject.bLeft = false;
                    }
                } else if (!FADM_Object.Communal._tcpModBus.getConnect()) {
                    if (Lib_Card.Configure.Parameter.Machine_IsLeftDoor == 1)
                        new FADM_Object.MyAlarm("PLC连接中断", 1);
                    else
                        new FADM_Object.MyAlarm("PLC connection interruption", 1);
                }
                
            }
        }

        //private void ReadBalance1()
        //{
        //    while (true)
        //    {

        //        int[] _ia_array = { 0, 0, 0, 0, 0, 0 };
        //        int state = FADM_Object.Communal._tcpModBus.Read(901, 6, ref _ia_array);
        //        if (state == -1)
        //        {
        //        }
        //        Thread.Sleep(50);
        //    }
        //}

        //private void WriteBalance1()
        //{
        //    while (true)
        //    {
        //        int[] _ia_array = { 0 };
        //        int state = FADM_Object.Communal._tcpModBus.Write(814,  _ia_array);
        //        if (state == -1)
        //        {
        //        }
        //        Thread.Sleep(50);
        //    }
        //}

        /// <summary>
        /// 光幕遮挡
        /// </summary>
        private void lightCur()
        {
            while (true)
            {
                Thread.Sleep(100);


                if (!Lib_Card.CardObject.bRight)
                {
                    Lib_Card.CardObject.bRight = true;
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        new FADM_Object.MyAlarm("右光幕遮挡", 1);
                    else
                        new FADM_Object.MyAlarm("Right light curtain occlusion", 1);
                }

                if (!Lib_Card.CardObject.bLeft)
                {
                    Lib_Card.CardObject.bLeft = true;
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        new FADM_Object.MyAlarm("左光幕遮挡", 1);
                    else
                        new FADM_Object.MyAlarm("Left light curtain occlusion", 1);
                }

            }
        }


        private void BtnUserSwitching_Click(object sender, EventArgs e)
        {
            if (Lib_Card.Configure.Parameter.Other_Language == 0)
            {
                if ("普通用户" == BtnUserSwitching.Text)
                {
                    BtnUserSwitching.Enabled = false;
                    Admin admin = new Admin(this)
                    {
                        Owner = this
                    };
                    admin.Show();
                    admin.Focus();

                }
                else
                {
                    BtnUserSwitching.Text = "普通用户";
                    toolStripSplitButton1.Enabled = false;
                    toolStripSplitButton2.Enabled = false;
                    toolStripSplitButton6.Enabled = false;
                    FADM_Object.Communal._s_operator = "123";
                }
            }
            else
            {
                if ("OrdinaryUsers" == BtnUserSwitching.Text)
                {
                    BtnUserSwitching.Enabled = false;
                    Admin admin = new Admin(this)
                    {
                        Owner = this
                    };
                    admin.Show();
                    admin.Focus();

                }
                else
                {
                    BtnUserSwitching.Text = "OrdinaryUsers";
                    toolStripSplitButton1.Enabled = false;
                    toolStripSplitButton2.Enabled = false;
                    toolStripSplitButton6.Enabled = false;
                    FADM_Object.Communal._s_operator = "123";
                }

            }
            
        }

        private void MiBrewingProcess_Click(object sender, EventArgs e)
        {
            foreach (Control control in this.PnlMain.Controls)
            {
                this.PnlMain.Controls.Remove(control);
                control.Dispose();
            }

            FADM_Control.BrewingProcess brewingProcess = new BrewingProcess();
            this.PnlMain.Controls.Add(brewingProcess);
            brewingProcess.Focus();
        }

        private void MiDyeingProcess_Click(object sender, EventArgs e)
        {
           
        }

        private void MiAssistant_Click(object sender, EventArgs e)
        {
            foreach (Control control in this.PnlMain.Controls)
            {
                this.PnlMain.Controls.Remove(control);
                control.Dispose();
            }

            FADM_Control.AssistantDefin assistantDefin = new AssistantDefin();
            this.PnlMain.Controls.Add(assistantDefin);
            assistantDefin.Focus();
        }

        private void MiBottle_Click(object sender, EventArgs e)
        {
            foreach (Control control in this.PnlMain.Controls)
            {
                this.PnlMain.Controls.Remove(control);
                control.Dispose();
            }

            FADM_Control.BottleDefin bottleDefin = new BottleDefin();
            this.PnlMain.Controls.Add(bottleDefin);
            bottleDefin.Focus();
        }

        private void MiDebug_Click(object sender, EventArgs e)
        {
            foreach (Control control in this.PnlMain.Controls)
            {
                this.PnlMain.Controls.Remove(control);
                control.Dispose();
            }

            FADM_Control.Debug debug = new Debug();
            this.PnlMain.Controls.Add(debug);
            debug.Focus();
        }

        private void MiRun_Click(object sender, EventArgs e)
        {
            foreach (Control control in this.PnlMain.Controls)
            {
                this.PnlMain.Controls.Remove(control);
                control.Dispose();
            }

            FADM_Control.Run run = new Run();
            this.PnlMain.Controls.Add(run);
            run.Focus();

        }

        private void MiAlarm_Click(object sender, EventArgs e)
        {
            foreach (Control control in this.PnlMain.Controls)
            {
                this.PnlMain.Controls.Remove(control);
                control.Dispose();
            }

            FADM_Control.Alarm alarm = new Alarm();
            this.PnlMain.Controls.Add(alarm);
            alarm.Focus();
        }

        public void BtnMain_Click(object sender, EventArgs e)
        {
            foreach (Control control in this.PnlMain.Controls)
            {
                this.PnlMain.Controls.Remove(control);
                control.Dispose();
            }

            if (Lib_Card.Configure.Parameter.Other_IsOnlyDrip == 0)
            {
                if (Communal._b_isBalanceInDrip)
                {
                    FADM_Control.MainBottle main = new MainBottle();
                    this.PnlMain.Controls.Add(main);
                    main.Focus();
                }
                else
                {

                    FADM_Control.S_MainBottle main = new S_MainBottle();
                    this.PnlMain.Controls.Add(main);
                    main.Focus();
                }
            }
            else
            {
                FADM_Control.P_MainBottle main = new P_MainBottle();
                this.PnlMain.Controls.Add(main);
                main.Focus();
            }

            if (!Cup._b_open && Screen.AllScreens.Count() > 1)
            {
                Cup c = new Cup();
                c.Bounds = Screen.AllScreens[1].Bounds;
                c.StartPosition = FormStartPosition.Manual;
                c.Location = Screen.AllScreens[1].Bounds.Location;
                Point point = new Point(Screen.AllScreens[1].Bounds.Location.X, Screen.AllScreens[1].Bounds.Location.Y);
                c.Location = point;
                c.WindowState = FormWindowState.Maximized;

                c.Show();
                Cup._b_open = true;
            }
        }

        private void BtnDrops_Click(object sender, EventArgs e)
        {
            foreach (Control control in this.PnlMain.Controls)
            {
                this.PnlMain.Controls.Remove(control);
                control.Dispose();
            }

            if (Lib_Card.Configure.Parameter.Other_IsOnlyDrip == 1)
            {
                FADM_Control.P_Formula formula = new P_Formula(this, FADM_Object.Communal._s_operator);
                this.PnlMain.Controls.Add(formula);
                formula.Focus();
            }
            else
            {
                FADM_Control.Formula formula = new Formula(this);
                this.PnlMain.Controls.Add(formula);
                formula.Focus();
            }
        }

        private void MiAbort_Click(object sender, EventArgs e)
        {
            Abort abort = new Abort();
            abort.Owner = this;
            abort.Show();
            abort.Focus();
        }

        private void MiRegister_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.Owner = this;
            register.Show();
            register.Focus();
        }

        private void BtnResetBrew_Click(object sender, EventArgs e)
        {
            FADM_Object.Communal._b_brewErr = false;
            //FADM_Object.Communal.MyBrew.Open();
            Thread P_thd_brew = new Thread(SmartDyeing.FADM_Auto.MyBrew.Brew);
            P_thd_brew.IsBackground = true;
            P_thd_brew.Start();
        }


        private void Pause()
        {

            //int iXPower = Lib_Card.CardObject.OA1.ReadOutPut(Lib_Card.ADT8940A1.ADT8940A1_IO.OutPut_X_Power);
            //if (-1 == iXPower)
            //{
            //    if (Lib_Card.Configure.Parameter.Other_Language == 0)
            //        FADM_Form.CustomMessageBox.Show("驱动异常", "ReadOutPut", MessageBoxButtons.OK, false);
            //    else
            //        FADM_Form.CustomMessageBox.Show("Drive abnormality", "ReadOutPut", MessageBoxButtons.OK, false);
            //}

            //int iYPower = Lib_Card.CardObject.OA1.ReadOutPut(Lib_Card.ADT8940A1.ADT8940A1_IO.OutPut_Y_Power);
            //if (-1 == iYPower)
            //{
            //    if (Lib_Card.Configure.Parameter.Other_Language == 0)
            //        FADM_Form.CustomMessageBox.Show("驱动异常", "ReadOutPut", MessageBoxButtons.OK, false);
            //    else
            //        FADM_Form.CustomMessageBox.Show("Drive abnormality", "ReadOutPut", MessageBoxButtons.OK, false);
            //}

            //if (Lib_Card.Configure.Parameter.Other_ActualPosition == 1)
            //{
            //    int iXReady = Lib_Card.CardObject.OA1Input.InPutStatus(Lib_Card.ADT8940A1.ADT8940A1_IO.InPut_X_Ready);
            //    int iYReady = Lib_Card.CardObject.OA1Input.InPutStatus(Lib_Card.ADT8940A1.ADT8940A1_IO.InPut_Y_Ready);
            //    if (0 == iXReady || 0 == iYReady)
            //    {
            //        Lib_Card.ADT8940A1.Module.Home.Home.Home_XYZFinish = false;
            //    }
            //}

            //if (0 == iXPower || 0 == iYPower)
            //{
            //    //Lib_Card.ADT8940A1.Module.Home.Home.Home_XYZFinish = false;
            //    //FADM_Object.Communal._fadmSqlserver.DeleteSpeechInfo("右光幕遮挡");
            //    //FADM_Object.Communal._fadmSqlserver.DeleteSpeechInfo("左光幕遮挡");
            //    //FADM_Object.Communal._fadmSqlserver.DeleteSpeechInfo("急停已按下");
            //}
            //else
            //{


            //    if (1 == Lib_Card.CardObject.OA1Input.InPutStatus(Lib_Card.ADT8940A1.ADT8940A1_IO.InPut_Sunx_A))
            //    {
            //        if (!Lib_Card.CardObject.bRight)
            //        {
            //            Lib_Card.CardObject.bRight = true;
            //            if (Lib_Card.Configure.Parameter.Other_Language == 0)
            //                new FADM_Object.MyAlarm("右光幕遮挡", 1);
            //            else
            //                new FADM_Object.MyAlarm("Right light curtain occlusion", 1);
            //        }
            //    }

            //    if (1 == Lib_Card.CardObject.OA1Input.InPutStatus(Lib_Card.ADT8940A1.ADT8940A1_IO.InPut_Sunx_B))
            //    {
            //        if (!Lib_Card.CardObject.bLeft)
            //        {
            //            Lib_Card.CardObject.bLeft = true;
            //            if (Lib_Card.Configure.Parameter.Other_Language == 0)
            //                new FADM_Object.MyAlarm("左光幕遮挡", 1);
            //            else
            //                new FADM_Object.MyAlarm("Left light curtain occlusion", 1);
            //        }
            //    }

            //    if (1 == Lib_Card.CardObject.OA1Input.InPutStatus(Lib_Card.ADT8940A1.ADT8940A1_IO.InPut_Stop))
            //    {
            //        if (!Lib_Card.CardObject.bFront)
            //        {
            //            Lib_Card.CardObject.bFront = true;
            //            if (Lib_Card.Configure.Parameter.Other_Language == 0)
            //                new FADM_Object.MyAlarm("前光幕遮挡", 1);
            //            else
            //                new FADM_Object.MyAlarm("Front light curtain occlusion", 1);
            //        }
            //    }
            //}

        }

        private void TmrMain_Tick(object sender, EventArgs e)
        {
            //Thread thread = new Thread(_b_pause); //这里弹出光幕遮挡
            //thread.Start();  //改造屏蔽
          
            
            if (Convert.ToDouble(FADM_Object.Communal._s_balanceValue) > Lib_Card.Configure.Parameter.Other_BalanceMaxWeight - 100)
                FADM_Object.Communal._b_balanceAlarm = true;
            else
                FADM_Object.Communal._b_balanceAlarm = false;


            //Lib_Card.ADT8940A1.Axis.Axis.Axis_Paused = FADM_Object.Communal._b_pause;

            //Lib_Card.ADT8940A1.Axis.Axis.Axis_Exit = FADM_Object.Communal._b_stop;


            if (Lib_Card.Configure.Parameter.Other_Language == 0)
            {
                switch (FADM_Object.Communal.ReadMachineStatus())
                {

                    case 0:

                        LabStatus.Text = "待机";
                        break;
                    case 1:
                        LabStatus.Text = "回零";
                        break;

                    case 2:
                        LabStatus.Text = "定坐标";
                        break;

                    case 3:
                        LabStatus.Text = "联动";
                        break;

                    case 4:
                        LabStatus.Text = "定点移动";
                        break;

                    case 5:
                        LabStatus.Text = "校正";
                        break;

                    case 6:
                        LabStatus.Text = "针检";
                        break;

                    case 7:
                        LabStatus.Text = "滴液";
                        break;

                    case 8:
                        LabStatus.Text = "异常";
                        break;
                    case 9:
                        LabStatus.Text = "滴液完成";
                        break;
                    case 10:
                        LabStatus.Text = "复位";
                        break;

                    case 11:
                        LabStatus.Text = "自检";
                        break;

                    case 12:
                        LabStatus.Text = "验证";
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (FADM_Object.Communal.ReadMachineStatus())
                {

                    case 0:

                        LabStatus.Text = "Standby";
                        break;
                    case 1:
                        LabStatus.Text = "Homing";
                        break;

                    case 2:
                        LabStatus.Text = "FixedCoordinates";
                        break;

                    case 3:
                        LabStatus.Text = "Linkage";
                        break;

                    case 4:
                        LabStatus.Text = "Fixed-pointMovement";
                        break;

                    case 5:
                        LabStatus.Text = "Water-Checking";
                        break;

                    case 6:
                        LabStatus.Text = "Bottle-Checking";
                        break;

                    case 7:
                        LabStatus.Text = "Drip";
                        break;

                    case 8:
                        LabStatus.Text = "Exceptional";
                        break;
                    case 9:
                        LabStatus.Text = "DroppingCompleted";
                        break;
                    case 10:
                        LabStatus.Text = "Reset";
                        break;

                    case 11:
                        LabStatus.Text = "Self-checking";
                        break;

                    case 12:
                        LabStatus.Text = "Water-Correction";
                        break;
                    default:
                        break;
                }
            }

            //重启com2通讯
            if (FADM_Object.Communal._b_brewErr)
            {
                if (BtnResetBrew.Visible == false)
                {
                    BtnResetBrew.Visible = true;
                }
            }
            else
            {
                if (BtnResetBrew.Visible)
                {
                    BtnResetBrew.Visible = false;
                }
            }
        }

        private void MiCup_Click(object sender, EventArgs e)
        {
            foreach (Control control in this.PnlMain.Controls)
            {
                this.PnlMain.Controls.Remove(control);
                control.Dispose();
            }

            FADM_Control.CupDefin cupdefin = new CupDefin();
            this.PnlMain.Controls.Add(cupdefin);
        }

        public void DetailInfo_Click(object sender, EventArgs e)
        {
            if (Lib_Card.Configure.Parameter.Other_IsOnlyDrip == 1)
            {
                return;
            }
            //foreach (Control control in this.PnlMain.Controls)
            //{
            //    this.PnlMain.Controls.Remove(control);
            //    control.Dispose();
            //}

            //FADM_Control.CupListDetail cupListDetail = new();
            //this.PnlMain.Controls.Add(cupListDetail);
            if (!Cup._b_open && Screen.AllScreens.Count() > 1)
            {
                Cup c = new Cup();
                c.Bounds = Screen.AllScreens[1].Bounds;
                c.StartPosition = FormStartPosition.Manual;
                c.Location = Screen.AllScreens[1].Bounds.Location;
                Point point = new Point(Screen.AllScreens[1].Bounds.Location.X, Screen.AllScreens[1].Bounds.Location.Y);
                c.Location = point;
                c.WindowState = FormWindowState.Maximized;
                c.Show();
                Cup._b_open = true;
            }
            else if (!Cup._b_open && Screen.AllScreens.Count() == 1)
            {
                Cup cup = new Cup();
                cup.Show();
                Cup._b_open = true;
            }
            else if (Cup._b_open && Screen.AllScreens.Count() == 1)
            {
                IntPtr ptr;
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    ptr = FindWindow(null, "配液杯详情");
                else
                    ptr = FindWindow(null, "CupStatus");
                if (ptr != IntPtr.Zero)
                {
                    //先删除页面，再重新打开
                    PostMessage(ptr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);

                    Cup cup = new Cup();
                    cup.Show();
                    Cup._b_open = true;
                }
            }
        }

        private void TmrTemp_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < FADM_Auto.Dye._cup_Temps.Length; i++)
            {
                if (FADM_Auto.Dye._cup_Temps[i]._b_start)
                {
                    if (Convert.ToDouble(FADM_Auto.Dye._cup_Temps[i]._s_temp) > 0)
                    {
                        Txt.WriteTXT(i + 1, FADM_Auto.Dye._cup_Temps[i]._s_temp);
                        FADM_Object.Communal._fadmSqlserver.ReviseData(
                            "UPDATE cup_details SET RecordIndex = RecordIndex + 1 WHERE CupNum = " + (i + 1) + ";");

                        if (FADM_Auto.Dye._cup_Temps[i]._b_tagging)
                        {
                            DataTable dt_data = FADM_Object.Communal._fadmSqlserver.GetData(
                                "SELECT * FROM cup_details WHERE CupNum = " + (i + 1) + ";");

                            Txt.WriteMarkTXT(i + 1, FADM_Auto.Dye._cup_Temps[i]._s_technologyName, dt_data.Rows[0]["RecordIndex"].ToString());
                            FADM_Auto.Dye._cup_Temps[i]._b_tagging = false;
                        }
                    }
                }

            }
        }
        private string _s_m_route = null;
        static ReaderWriterLockSlim _logWriteLock = new ReaderWriterLockSlim();
        private void TmrFGY_Tick(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(this._s_m_route))
                {
                    try
                    {
                        if (!_logWriteLock.IsWriteLockHeld)
                        {

                            _logWriteLock.EnterWriteLock(); //进入写入锁
                            string[] sa_temp = File.ReadAllLines(this._s_m_route);
                            File.Delete(this._s_m_route);
                            Thread th = new Thread(insert);
                            th.IsBackground = true;
                            th.Start(sa_temp);
                        }

                    }
                    catch (Exception ex)
                    {
                        if (_logWriteLock.IsWriteLockHeld)
                            _logWriteLock.ExitWriteLock(); //退出写入锁
                    }
                    finally
                    {
                        if (_logWriteLock.IsWriteLockHeld)
                            _logWriteLock.ExitWriteLock(); //退出写入锁

                    }

                }
            }
            catch (Exception ex)
            {
                FADM_Form.CustomMessageBox.Show(ex.Message, "TmrFGY_Tick", MessageBoxButtons.OK, true);
                TmrFGY.Enabled = false;
            }
        }

        struct recipe
        {
            public int _i_indexNum;
            public string _s_assistantCode;
            public string _s_assistantName;
            public double _s_formulaDosage;
            public string _s_unitOfAccount;
            public int _i_bottleNum;
            public double _d_settingConcentration;
            public double _d_realConcentration;
            public double _d_objectDropWeight;
            public string _s_code;
            public string _s_technologyName;


        }

        private void insert(object obj_arg)
        {
            try
            {
                string[] sa_rcp = (string[])obj_arg;
                int i_currentIndex = 0;

            label:
                string s_formulaCode = "";
                int i_versionNum = 0;
                int i_num = 0;
                string s_formulaName = "";
                double d_clothWeight = 0;
                double d_totalWeight = 0;
                double d_bathRatio = 0;
                double d_allW = 0;
                string s_deyCode = "";
                double d_readBathRatio = 0;
                double d_handleBathRatio = 0;
                double d_non_AnhydrationWR = 0;
                double d_anhydrationWR = 0;

                for (; i_currentIndex < sa_rcp.Length; i_currentIndex++)
                {
                    if (sa_rcp[i_currentIndex].Substring(0, 4) == "500M" && sa_rcp[i_currentIndex].Length == 148)
                    {
                        //表头资料
                        s_formulaCode = sa_rcp[i_currentIndex].Substring(4, 12).Trim();
                        i_num = Convert.ToInt16(sa_rcp[i_currentIndex].Substring(18, 2));
                        s_formulaName = sa_rcp[i_currentIndex].Substring(24, 24).Trim();
                        d_clothWeight = Convert.ToDouble(sa_rcp[i_currentIndex].Substring(48, 8));
                        d_totalWeight = Convert.ToDouble(sa_rcp[i_currentIndex].Substring(56, 8));
                        d_readBathRatio = Convert.ToDouble(sa_rcp[i_currentIndex].Substring(86, 8));
                        d_handleBathRatio = Convert.ToDouble(sa_rcp[i_currentIndex].Substring(94, 8));
                        d_anhydrationWR = Convert.ToDouble(sa_rcp[i_currentIndex].Substring(102, 8));
                        d_non_AnhydrationWR = Convert.ToDouble(sa_rcp[i_currentIndex].Substring(110, 8));
                        s_deyCode = sa_rcp[i_currentIndex].Substring(118, 30).Trim();
                        d_bathRatio = Convert.ToDouble(string.Format("{0:F}", d_totalWeight / d_clothWeight));
                        DataTable dt_data = FADM_Object.Communal._fadmSqlserver.GetData(
                            "SELECT * FROM formula_head WHERE FormulaCode ='" +
                            s_formulaCode + "' ORDER BY VersionNum DESC");
                        if (dt_data.Rows.Count > 0)
                        {
                            i_versionNum = (Convert.ToInt16(dt_data.Rows[0]["VersionNum"])) + 1;
                        }
                        break;
                    }
                }

                //先把染固色工艺全部子工艺全部记录

                string s_sql_Code = "select * from dyeing_code where DyeingCode ='" + s_deyCode.Trim() + "' order by IndexNum;";
                DataTable dt_data_Code = FADM_Object.Communal._fadmSqlserver.GetData(s_sql_Code);
                Dictionary<int, List<recipe>> dic_listCode = new Dictionary<int, List<recipe>>();
                foreach (DataRow dr in dt_data_Code.Rows)
                {
                    List<recipe> lis_temp = new List<recipe>();
                    dic_listCode.Add(Convert.ToInt32(dr["IndexNum"].ToString()), lis_temp);
                }
                List<recipe> lis_data = new List<recipe>();

                //判断第一个是不是染色工艺
                bool b_dyeing = false;

                //List<recipe> Dyelist = new List<recipe>();
                //List<recipe> Handle1list = new List<recipe>();
                //List<recipe> Handle2list = new List<recipe>();
                //List<recipe> Handle3list = new List<recipe>();
                //List<recipe> Handle4list = new List<recipe>();
                //List<recipe> Handle5list = new List<recipe>();
                for (; i_currentIndex < sa_rcp.Length; i_currentIndex++)
                {
                    if (sa_rcp[i_currentIndex].Substring(0, 4) == "500C" && sa_rcp[i_currentIndex].Length == 48 &&
                        sa_rcp[i_currentIndex].Substring(4, 12).Trim() == s_formulaCode)
                    {
                        if (sa_rcp[i_currentIndex].Substring(24, 8) != "WATER   ")
                        {
                            recipe re = new recipe();
                            re._i_indexNum = Convert.ToInt16(sa_rcp[i_currentIndex].Substring(18, 2));
                            re._s_assistantCode = sa_rcp[i_currentIndex].Substring(24, 8).Trim();
                            re._s_formulaDosage = Convert.ToDouble(sa_rcp[i_currentIndex].Substring(32, 9));
                            re._s_code = sa_rcp[i_currentIndex].Substring(41, 2).Trim();
                            re._s_technologyName = sa_rcp[i_currentIndex].Substring(43, 5).Trim();

                            DataTable dt_data = FADM_Object.Communal._fadmSqlserver.GetData(
                                "SELECT *  FROM assistant_details WHERE " +
                                 "AssistantCode = '" + re._s_assistantCode + "';");

                            if (dt_data.Rows.Count == 0)
                            {
                                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                    throw new Exception("未找到" + re._s_assistantCode + "染助剂代码");
                                else
                                    throw new Exception("not found " + re._s_assistantCode + " Dyeing agent code");
                            }

                            re._s_assistantName = Convert.ToString(dt_data.Rows[0]["AssistantName"]);
                            re._s_unitOfAccount = Convert.ToString(dt_data.Rows[0]["UnitOfAccount"]);

                            dt_data = FADM_Object.Communal._fadmSqlserver.GetData(
                                "SELECT *  FROM bottle_details WHERE " +
                                 "AssistantCode = '" + re._s_assistantCode + "' AND " +
                                 "RealConcentration != 0 Order BY SettingConcentration DESC;");
                            if (dt_data.Rows.Count == 0)
                            {
                                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                    throw new Exception("未找到" + re._s_assistantCode + "染助剂代码的瓶号");
                                else

                                    throw new Exception("not found " + " The bottle number of the  " + re._s_assistantCode);
                            }
                            for (int i = 0; i < dt_data.Rows.Count; i++)
                            {
                                double d_objectW = 0;
                                if (re._s_unitOfAccount == "%")
                                {
                                    d_objectW = Convert.ToDouble(string.Format("{0:F}",
                                        d_clothWeight * re._s_formulaDosage / Convert.ToDouble(dt_data.Rows[i]["RealConcentration"])));
                                }
                                else if (re._s_unitOfAccount == "g/l")
                                {
                                    d_objectW = Convert.ToDouble(string.Format("{0:F}",
                                        d_totalWeight * re._s_formulaDosage / Convert.ToDouble(dt_data.Rows[i]["RealConcentration"])));
                                }
                                else
                                {
                                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                        throw new Exception(re._s_assistantCode + "染助剂的计算单位设置异常");
                                    else
                                        throw new Exception(re._s_assistantCode + " Abnormal setting of calculation unit for dyeing auxiliaries");
                                }

                                if (d_objectW >= Convert.ToDouble(string.Format("{0:F}", dt_data.Rows[i]["DropMinWeight"])))
                                {
                                    re._i_bottleNum = Convert.ToInt16(dt_data.Rows[i]["BottleNum"]);
                                    re._d_settingConcentration = Convert.ToDouble(string.Format("{0:F}", dt_data.Rows[i]["SettingConcentration"]));
                                    re._d_realConcentration = Convert.ToDouble(string.Format("{0:F}", dt_data.Rows[i]["RealConcentration"]));
                                    re._d_objectDropWeight = d_objectW;
                                    break;
                                }
                                else
                                {
                                    if (i == dt_data.Rows.Count - 1)
                                    {
                                        if (d_objectW > 0.1)
                                        {
                                            re._i_bottleNum = Convert.ToInt16(dt_data.Rows[i]["BottleNum"]);
                                            re._d_settingConcentration = Convert.ToDouble(string.Format("{0:F}", dt_data.Rows[i]["SettingConcentration"]));
                                            re._d_realConcentration = Convert.ToDouble(string.Format("{0:F}", dt_data.Rows[i]["RealConcentration"]));
                                            re._d_objectDropWeight = d_objectW;
                                            break;
                                        }
                                        else
                                        {
                                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                                throw new Exception(re._s_assistantCode + "染助剂滴液量小于0.1克");
                                            else
                                                throw new Exception(re._s_assistantCode + " Dyeing aids with a droplet volume of less than 0.1 grams");
                                        }
                                    }
                                }
                            }
                            if (re._s_code.Trim() == "")
                            {
                                d_allW += re._d_objectDropWeight;
                                lis_data.Add(re);
                            }
                            else
                            {
                                d_allW += re._d_objectDropWeight;
                                int i_index = Convert.ToInt32(re._s_code.Trim());
                                //判断第一个是不是染色工艺，如果是，计算加水就要扣减

                                if (i_index == 1)
                                {
                                    if (i_index > dt_data_Code.Rows.Count)
                                    {
                                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                            throw new Exception("配方序号输入错误");
                                        else
                                            throw new Exception("Formula number input error");
                                    }
                                    else
                                    {
                                        if (Convert.ToInt32(dt_data_Code.Rows[i_index - 1]["Type"].ToString()) == 1)
                                        {
                                            d_allW += re._d_objectDropWeight;
                                            b_dyeing = true;
                                        }
                                    }
                                }
                                dic_listCode[i_index].Add(re);
                            }

                        }
                        else
                        {
                            int i_nCount = 0;
                            for (int i = 1; i <= dic_listCode.Count; i++)
                            {
                                i_nCount += dic_listCode[i].Count;
                            }
                            if (lis_data.Count + i_nCount >= i_num - 1)
                            {
                                break;
                            }
                        }
                    }
                }
                string s_sql = null;
                string s_hBRList = "";
                //先判断染色后处理加药条数是否足够
                if (s_deyCode.Trim() != "")
                {
                    string s_sql1 = "select * from dyeing_code where DyeingCode ='" + s_deyCode.Trim() + "' order by IndexNum;";
                    DataTable dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql1);

                    //先把助剂代码写入对应列表
                    foreach (DataRow dr in dt_data.Rows)
                    {
                        string s_sql2;
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)

                            s_sql2 = "SELECT TechnologyName FROM dyeing_process where Code = '" + dr[3].ToString() + "' and TechnologyName in ('加A','加B','加C','加D','加E','加F','加G','加H','加I','加J','加K','加L','加M','加N')  group  by TechnologyName;";
                        else
                            s_sql2 = "SELECT TechnologyName FROM dyeing_process where Code = '" + dr[3].ToString() + "' and TechnologyName in ('Add A','Add B','Add C','Add D','Add E','Add F','Add G','Add H','Add I','Add J','Add K','Add L','Add M','Add N')  group  by TechnologyName;";

                        DataTable dt_data2 = FADM_Object.Communal._fadmSqlserver.GetData(s_sql2);
                        if (dic_listCode[Convert.ToInt32(dr["IndexNum"].ToString())].Count != dt_data2.Rows.Count)
                        {
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                throw new Exception(s_formulaCode + "染色或后处理配方数据不匹配");
                            else
                                throw new Exception(s_formulaCode + " Mismatch in dyeing or post-processing formula data");
                        }

                    }
                    int i_nIndex = 1;
                    foreach (DataRow dr in dt_data.Rows)
                    {
                        foreach (recipe rc in dic_listCode[i_nIndex])
                        {
                            s_sql = "INSERT INTO formula_handle_details (" +
                              " FormulaCode, VersionNum,  AssistantCode," +
                              " FormulaDosage, UnitOfAccount, BottleNum, SettingConcentration," +
                              " RealConcentration, AssistantName, ObjectDropWeight,DyeingCode,Code,TechnologyName,BottleSelection,RealDropWeight) VALUES( '" +
                              s_formulaCode + "', '" + i_versionNum + "', '" + rc._s_assistantCode + "', '" +
                              rc._s_formulaDosage + "', '" + rc._s_unitOfAccount + "'," +
                              " '" + rc._i_bottleNum + "', '" + rc._d_settingConcentration + "', '" +
                              rc._d_realConcentration + "', '" + rc._s_assistantName + "'," +
                              " '" + rc._d_objectDropWeight + "', '" + s_deyCode.Trim() + "', '" + dr["Code"].ToString() + "', '" + rc._s_technologyName + "',0,0.0);";

                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                        }
                        i_nIndex++;
                    }
                    //全部使用后处理浴比
                    foreach (DataRow dr in dt_data.Rows)
                    {
                        s_hBRList += d_handleBathRatio + "|";
                    }
                    //去掉最后一个分割符
                    s_hBRList = s_hBRList.Substring(0, s_hBRList.Length - 1);
                }


                //需要滴液
                if (lis_data.Count + (b_dyeing ? dic_listCode[1].Count : 0) > 0)
                {
                    string s_stage = "滴液";

                    int i_nCount = 0;
                    for (int i = 1; i <= dic_listCode.Count; i++)
                    {
                        i_nCount += dic_listCode[i].Count;
                    }
                    if (i_nCount > 0)
                    {
                        s_stage = "后处理";
                    }
                    string s_temAddWaterWeight = Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? String.Format("{0:F}", d_totalWeight - d_clothWeight * d_anhydrationWR - d_allW) : String.Format("{0:F3}", d_totalWeight - d_clothWeight * d_anhydrationWR - d_allW);
                    //添加配方表
                    s_sql = "INSERT INTO formula_head (" +
                        " FormulaCode, VersionNum, FormulaName," +
                        " AddWaterChoose,ClothWeight," +
                        " TotalWeight,CreateTime," +
                        " ObjectAddWaterWeight,BathRatio,HandleBathRatio,Non_AnhydrationWR,AnhydrationWR,DyeingCode,Stage,HandleBRList) VALUES('" + s_formulaCode + "'," +
                        " '" + i_versionNum + "', '" + s_formulaName + "', 1, " +
                        " '" + d_clothWeight + "', '" + d_totalWeight + "', " +
                        " '" + DateTime.Now + "', '" + s_temAddWaterWeight + "', '" + d_readBathRatio + "', '" + d_handleBathRatio + "', '" + d_non_AnhydrationWR + "', '" + d_anhydrationWR + "', '" + s_deyCode.Trim() + "', '" + s_stage + "', '" + s_hBRList + "');";
                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                }
                //只做后处理
                else
                {
                    //加水不勾选，加水量为0
                    //添加配方表
                    s_sql = "INSERT INTO formula_head (" +
                        " FormulaCode, VersionNum, FormulaName," +
                        " AddWaterChoose,ClothWeight," +
                        " TotalWeight,CreateTime," +
                        " ObjectAddWaterWeight,BathRatio,HandleBathRatio,Non_AnhydrationWR,AnhydrationWR,DyeingCode,Stage,HandleBRList) VALUES('" + s_formulaCode + "'," +
                        " '" + i_versionNum + "', '" + s_formulaName + "', 0, " +
                        " '" + d_clothWeight + "', '" + d_totalWeight + "', " +
                        " '" + DateTime.Now + "', '" + "0" + "', '" + d_readBathRatio + "', '" + d_handleBathRatio + "', '" + d_non_AnhydrationWR + "', '" + d_anhydrationWR + "', '" + s_deyCode.Trim() + "', '" + "后处理" + "', '" + s_hBRList + "');";
                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                }

                foreach (recipe rc in lis_data)
                {
                    s_sql = "INSERT INTO formula_details (" +
                          " FormulaCode, VersionNum, IndexNum, AssistantCode," +
                          " FormulaDosage, UnitOfAccount, BottleNum, SettingConcentration," +
                          " RealConcentration, AssistantName, ObjectDropWeight) VALUES( '" +
                          s_formulaCode + "', '" + i_versionNum + "', " +
                          "'" + rc._i_indexNum + "', '" + rc._s_assistantCode + "', '" +
                          rc._s_formulaDosage + "', '" + rc._s_unitOfAccount + "'," +
                          " '" + rc._i_bottleNum + "', '" + rc._d_settingConcentration + "', '" +
                          rc._d_realConcentration + "', '" + rc._s_assistantName + "'," +
                          " '" + rc._d_objectDropWeight + "');";

                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                }




                if (i_currentIndex < sa_rcp.Length - 1)
                {
                    goto label;
                }

            }
            catch (Exception ex)
            {
                FADM_Form.CustomMessageBox.Show(ex.Message, "insert", MessageBoxButtons.OK, true);
            }
        }

        private void Reset()
        {
            MyModbusFun.Reset();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FADM_Object.Communal._b_isBackUp)
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    FADM_Form.CustomMessageBox.Show("正在备份数据，请勿关闭系统", "温馨提示", MessageBoxButtons.OK, false);
                else
                    FADM_Form.CustomMessageBox.Show("Backing up data, please do not shut down the system", "Tips", MessageBoxButtons.OK, false);
                e.Cancel = true;
                return;
            }
            if (Lib_Card.Configure.Parameter.Other_Language == 0)
            {
                DialogResult dialogResult = FADM_Form.CustomMessageBox.Show("确定退出吗(确定退出，请把机械手移动到安全区域)?", "温馨提示", MessageBoxButtons.YesNo, true);
                if (dialogResult == DialogResult.Yes)
                {
                    //把过期标记位置位，等待2秒响应发到开料机
                    Communal._b_closebrew = true;
                    Thread.Sleep(2000);


                    try
                    {
                        MyModbusFun.ResetClosing();
                    }
                    catch(Exception ex)
                    {
                        if(ex.Message.Equals("发现针筒或杯盖"))
                        {
                            FADM_Form.CustomMessageBox.Show("发现针筒或杯盖,点击确定10s后打开抓手", "温馨提示", MessageBoxButtons.OK, false);
                            Thread.Sleep(10000);
                        lab811:
                            int[] ia_array4 = { 7 };
                            int i_state = FADM_Object.Communal._tcpModBus.Write(811, ia_array4);
                            if (i_state == -1)
                                goto lab811;
                        }
                    }

                    //FADM_Auto.Reset.IOReset();

                    e.Cancel = false;
                    System.Diagnostics.Process.GetProcessById(System.Diagnostics.Process.GetCurrentProcess().Id).Kill();

                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                DialogResult dialogResult = FADM_Form.CustomMessageBox.Show("Are you sure to exit? (Please move the robotic arm to a safe area if you are sure to exit)?", "Tips", MessageBoxButtons.YesNo, true);
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        MyModbusFun.ResetClosing();
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Equals("发现针筒或杯盖"))
                        {
                            FADM_Form.CustomMessageBox.Show("Upon discovering the syringe or cup cap, click OK and open the gripper after 10 seconds", "Tips", MessageBoxButtons.OK, false);
                            Thread.Sleep(10000);
                        lab811:
                            int[] ia_array4 = { 7 };
                            int i_state = FADM_Object.Communal._tcpModBus.Write(811, ia_array4);
                            if (i_state == -1)
                                goto lab811;
                        }
                    }

                    e.Cancel = false;
                    System.Diagnostics.Process.GetProcessById(System.Diagnostics.Process.GetCurrentProcess().Id).Kill();

                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void BtnAllFormual_Click(object sender, EventArgs e)
        {

        }

        private void BackUp()
        {
            while (true)
            {
                try
                {

                    //执行备份操作
                    if (!FADM_Object.Communal._b_isBackUp)
                    {
                        FADM_Object.Communal._b_isBackUp = true;
                        string s_sql = "SELECT FormulaCode,VersionNum,CreateTime  FROM formula_head WHERE CreateTime < (SELECT DATEADD(DAY,-90,GETDATE())) ;";

                        DataTable dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                        foreach (DataRow dr in dt_data.Rows)
                        {
                            string s_str;
                            s_str = "insert into formula_details_temp select * from formula_details where FormulaCode='" + dr["FormulaCode"].ToString() + "' and VersionNum='" + dr["VersionNum"].ToString() + "';";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_str);
                            s_str = "delete from  formula_details where FormulaCode='" + dr["FormulaCode"].ToString() + "' and VersionNum='" + dr["VersionNum"].ToString() + "';";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_str);
                            s_str = "insert into formula_handle_details_temp select * from formula_handle_details where FormulaCode='" + dr["FormulaCode"].ToString() + "' and VersionNum='" + dr["VersionNum"].ToString() + "';";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_str);
                            s_str = "delete from  formula_handle_details where FormulaCode='" + dr["FormulaCode"].ToString() + "' and VersionNum='" + dr["VersionNum"].ToString() + "';";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_str);
                            s_str = "insert into formula_head_temp select * from formula_head where FormulaCode='" + dr["FormulaCode"].ToString() + "' and VersionNum='" + dr["VersionNum"].ToString() + "';";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_str);
                            s_str = "delete from formula_head where FormulaCode='" + dr["FormulaCode"].ToString() + "' and VersionNum='" + dr["VersionNum"].ToString() + "';";
                            FADM_Object.Communal._fadmSqlserver.ReviseData(s_str);
                        }
                        FADM_Object.Communal._b_isBackUp = false;
                    }
                    Thread.Sleep(60000 * 60);

                }
                catch
                {
                    FADM_Object.Communal._b_isBackUp = false;
                }
            }
        }

        private void BtnLimitSet_Click(object sender, EventArgs e)
        {
            FADM_Form.LimitSet limitSet = new FADM_Form.LimitSet();

            limitSet.Show();
            limitSet.Focus();
        }

        private void MiLimitSet_Click(object sender, EventArgs e)
        {
            IntPtr ptr;
            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                ptr = FindWindow(null, "限值设置");
            else
                ptr = FindWindow(null, "LimitSettings");
            if (ptr == IntPtr.Zero)
            {
                FADM_Form.LimitSet limitSet = new FADM_Form.LimitSet();

                limitSet.Show();
                limitSet.Focus();
            }

        }

        private void MiHistoryPage_Click(object sender, EventArgs e)
        {
            foreach (Control control in this.PnlMain.Controls)
            {
                this.PnlMain.Controls.Remove(control);
                control.Dispose();
            }
            FADM_Control.HistoryData historyData = new FADM_Control.HistoryData(this);
            this.PnlMain.Controls.Add(historyData);
            historyData.Focus();
        }

        private void MiFormulaPage_Click(object sender, EventArgs e)
        {
            foreach (Control control in this.PnlMain.Controls)
            {
                this.PnlMain.Controls.Remove(control);
                control.Dispose();
            }
            FADM_Control.AllFormulas allFormulas = new FADM_Control.AllFormulas();
            this.PnlMain.Controls.Add(allFormulas);
            allFormulas.Focus();
        }

        private void MiOperator_Click(object sender, EventArgs e)
        {
            IntPtr ptr;
            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                ptr = FindWindow(null, "操作员总览");
            else
                ptr = FindWindow(null, "OperatorOverview");
            if (ptr == IntPtr.Zero)
            {
                FADM_Form.Operator op = new FADM_Form.Operator();

                op.Show();
                op.Focus();
            }
        }

        private void toolStripSplitButton5_DropDownOpening(object sender, EventArgs e)
        {
            toolStripSplitButton5.DropDownItems.Clear();
            if (Lib_Card.Configure.Parameter.Other_Language == 0)
            {
                if (BtnUserSwitching.Text == "普通用户")
                {
                    string P_str_sql = "SELECT * FROM operator_table ;";
                    DataTable P_dt = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);
                    foreach (DataRow dr in P_dt.Rows)
                    {
                        toolStripSplitButton5.DropDownItems.Add(dr[0].ToString());
                    }
                }
            }
          else
            {
                if (BtnUserSwitching.Text == "OrdinaryUsers")
                {
                    string P_str_sql = "SELECT * FROM operator_table ;";
                    DataTable P_dt = FADM_Object.Communal._fadmSqlserver.GetData(P_str_sql);
                    foreach (DataRow dr in P_dt.Rows)
                    {
                        toolStripSplitButton5.DropDownItems.Add(dr[0].ToString());
                    }
                }

            }

        }

        private void toolStripSplitButton5_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            FADM_Object.Communal._s_operator = e.ClickedItem.ToString();

            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                BtnUserSwitching.Text = "普通用户";
            else
                BtnUserSwitching.Text = "OrdinaryUsers";
            toolStripSplitButton1.Enabled = false;
            toolStripSplitButton2.Enabled = false;
            toolStripSplitButton6.Enabled = false;

            foreach (Control control in this.PnlMain.Controls)
            {
                this.PnlMain.Controls.Remove(control);
                control.Dispose();
            }

            if (Lib_Card.Configure.Parameter.Other_IsOnlyDrip == 1)
            {
                FADM_Control.P_Formula formula = new P_Formula(this, FADM_Object.Communal._s_operator);
                this.PnlMain.Controls.Add(formula);
                formula.Focus();
            }
            else
            {
                FADM_Control.Formula formula = new Formula(this);
                this.PnlMain.Controls.Add(formula);
                formula.Focus();
            }
        }

        private void MiOut_Click(object sender, EventArgs e)
        {
            //if ((LabStatus.Text == "待机"|| LabStatus.Text == "Standby") && null == FADM_Object.Communal.ReadDyeThread())
            //{
            //    if (MiOut.Checked)
            //    {
            //        MiOut.Checked = false;
            //        string sPath = Environment.CurrentDirectory + "\\Config\\Config.ini";
            //        Lib_File.Ini.WriteIni("Setting", "_b_isOutDrip", "0", sPath);
            //        FADM_Object.Communal._b_isOutDrip = false;
            //    }
            //    else
            //    {
            //        MiOut.Checked = true;
            //        string sPath = Environment.CurrentDirectory + "\\Config\\Config.ini";
            //        Lib_File.Ini.WriteIni("Setting", "_b_isOutDrip", "1", sPath);
            //        FADM_Object.Communal._b_isOutDrip = true;
            //    }
            //}
            //else
            //{
            //    if (Lib_Card.Configure.Parameter.Other_Language == 0)

            //        FADM_Form.CustomMessageBox.Show("待机状态下才能修改", "温馨提示", MessageBoxButtons.OK, false);
            //    else
            //        FADM_Form.CustomMessageBox.Show("Can only be modified in standby mode", "Tips", MessageBoxButtons.OK, false);

            //}
        }

        private void MiLow_Click(object sender, EventArgs e)
        {
            //if ((LabStatus.Text == "待机" || LabStatus.Text == "Standby") && null == FADM_Object.Communal.ReadDyeThread())
            //{
            //    if (MiLow.Checked)
            //    {
            //        MiLow.Checked = false;
            //        string sPath = Environment.CurrentDirectory + "\\Config\\Config.ini";
            //        Lib_File.Ini.WriteIni("Setting", "_b_isLowDrip", "0", sPath);
            //        FADM_Object.Communal._b_isLowDrip = false;
            //    }
            //    else
            //    {
            //        MiLow.Checked = true;
            //        string sPath = Environment.CurrentDirectory + "\\Config\\Config.ini";
            //        Lib_File.Ini.WriteIni("Setting", "_b_isLowDrip", "1", sPath);
            //        FADM_Object.Communal._b_isLowDrip = true;
            //    }
            //}
            //else
            //{
            //    if (Lib_Card.Configure.Parameter.Other_Language == 0)

            //        FADM_Form.CustomMessageBox.Show("待机状态下才能修改", "温馨提示", MessageBoxButtons.OK, false);
            //    else
            //        FADM_Form.CustomMessageBox.Show("Can only be modified in standby mode", "Tips", MessageBoxButtons.OK, false);
            //}
        }

        private void toolStripSplitButton5_Click(object sender, EventArgs e)
        {
            if (FADM_Object.Communal._s_operator == "管理用户" || FADM_Object.Communal._s_operator == "工程师")
            {
                foreach (Control control in this.PnlMain.Controls)
                {
                    this.PnlMain.Controls.Remove(control);
                    control.Dispose();
                }
                if (Lib_Card.Configure.Parameter.Other_IsOnlyDrip == 1)
                {
                    FADM_Control.P_Formula formula = new P_Formula(this, FADM_Object.Communal._s_operator);
                    this.PnlMain.Controls.Add(formula);
                    formula.Focus();
                }
                else
                {
                    FADM_Control.Formula formula = new Formula(this);
                    this.PnlMain.Controls.Add(formula);
                    formula.Focus();
                }
            }
        }

        private void MiFullDrip_Click(object sender, EventArgs e)
        {
            //if ((LabStatus.Text == "待机" || LabStatus.Text == "Standby") && null == FADM_Object.Communal.ReadDyeThread())
            //{
            //    if (MiFullDrip.Checked)
            //    {
            //        MiFullDrip.Checked = false;
            //        string sPath = Environment.CurrentDirectory + "\\Config\\Config.ini";
            //        Lib_File.Ini.WriteIni("Setting", "_b_isFullDrip", "0", sPath);
            //        FADM_Object.Communal._b_isFullDrip = false;
            //    }
            //    else
            //    {
            //        MiFullDrip.Checked = true;
            //        string sPath = Environment.CurrentDirectory + "\\Config\\Config.ini";
            //        Lib_File.Ini.WriteIni("Setting", "_b_isFullDrip", "1", sPath);
            //        FADM_Object.Communal._b_isFullDrip = true;
            //    }
            //}
            //else
            //{
            //    if (Lib_Card.Configure.Parameter.Other_Language == 0)

            //        FADM_Form.CustomMessageBox.Show("待机状态下才能修改", "温馨提示", MessageBoxButtons.OK, false);
            //    else
            //        FADM_Form.CustomMessageBox.Show("Can only be modified in standby mode", "Tips", MessageBoxButtons.OK, false);
            //}
        }

        private void timerReg_Tick(object sender, EventArgs e)
        {
            //删除30天前开料记录
            DateTime dt = DateTime.Now;
            DateTime dt1= DateTime.Now;
            dt1= dt.AddDays(-30);
            FADM_Object.Communal._fadmSqlserver.ReviseData("delete from brew_run_table where MyDateTime < "+dt1.ToString());
            countDown();


        }


        private void BtnNTD_Click(object sender, EventArgs e)
        {

            if (NeedToDo._b_showRun == false)
            {

                NeedToDo need = new NeedToDo();
                need.Owner = this;
                need.Show();
                need.Focus();
            }

        }

        private void MiFormulaGroup_Click(object sender, EventArgs e)
        {
            foreach (Control control in this.PnlMain.Controls)
            {
                this.PnlMain.Controls.Remove(control);
                control.Dispose();
            }

            FADM_Control.FormulaGroup formulaGroup = new FormulaGroup();
            this.PnlMain.Controls.Add(formulaGroup);
            formulaGroup.Focus();
        }

        private void DyeingAndFixationProcessConfiguration_Click(object sender, EventArgs e)
        {
            foreach (Control control in this.PnlMain.Controls)
            {
                this.PnlMain.Controls.Remove(control);
                control.Dispose();
            }

            FADM_Control.DyeingAndFixationProcessConfiguration dyeingAndFixationProcessConfiguration = new DyeingAndFixationProcessConfiguration();
            this.PnlMain.Controls.Add(dyeingAndFixationProcessConfiguration);
            dyeingAndFixationProcessConfiguration.Focus();
        }

        private void DyeingProcessConfiguration_Click(object sender, EventArgs e)
        {
            foreach (Control control in this.PnlMain.Controls)
            {
                this.PnlMain.Controls.Remove(control);
                control.Dispose();
            }

            FADM_Control.DyeingConfiguration dyeingConfiguration = new DyeingConfiguration();
            this.PnlMain.Controls.Add(dyeingConfiguration);
            dyeingConfiguration.Focus();
        }

        private void PostTreatmentProcessConfiguration_Click(object sender, EventArgs e)
        {
            foreach (Control control in this.PnlMain.Controls)
            {
                this.PnlMain.Controls.Remove(control);
                control.Dispose();
            }

            FADM_Control.PostTreatmentConfiguration postTreatment = new PostTreatmentConfiguration();
            this.PnlMain.Controls.Add(postTreatment);
            postTreatment.Focus();
        }

        private void MiBrewPage_Click(object sender, EventArgs e)
        {
            foreach (Control control in this.PnlMain.Controls)
            {
                this.PnlMain.Controls.Remove(control);
                control.Dispose();
            }
            FADM_Control.BrewRun brewRun = new FADM_Control.BrewRun();
            this.PnlMain.Controls.Add(brewRun);
            brewRun.Focus();
        }

        private void MiOut1_Click(object sender, EventArgs e)
        {
            if ((LabStatus.Text == "待机" || LabStatus.Text == "Standby") && null == FADM_Object.Communal.ReadDyeThread())
            {
                if (MiOut1.Checked)
                {
                    MiOut1.Checked = false;
                    string s_path = Environment.CurrentDirectory + "\\Config\\Config.ini";
                    Lib_File.Ini.WriteIni("Setting", "IsOutDrip", "0", s_path);
                    FADM_Object.Communal._b_isOutDrip = false;
                }
                else
                {
                    MiOut1.Checked = true;
                    string s_path = Environment.CurrentDirectory + "\\Config\\Config.ini";
                    Lib_File.Ini.WriteIni("Setting", "IsOutDrip", "1", s_path);
                    FADM_Object.Communal._b_isOutDrip = true;
                }
            }
            else
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)

                    FADM_Form.CustomMessageBox.Show("待机状态下才能修改", "温馨提示", MessageBoxButtons.OK, false);
                else
                    FADM_Form.CustomMessageBox.Show("Can only be modified in standby mode", "Tips", MessageBoxButtons.OK, false);

            }
        }

        private void MiLow1_Click(object sender, EventArgs e)
        {
            if ((LabStatus.Text == "待机" || LabStatus.Text == "Standby") && null == FADM_Object.Communal.ReadDyeThread())
            {
                if (MiLow1.Checked)
                {
                    MiLow1.Checked = false;
                    string s_path = Environment.CurrentDirectory + "\\Config\\Config.ini";
                    Lib_File.Ini.WriteIni("Setting", "IsLowDrip", "0", s_path);
                    FADM_Object.Communal._b_isLowDrip = false;
                }
                else
                {
                    MiLow1.Checked = true;
                    string s_path = Environment.CurrentDirectory + "\\Config\\Config.ini";
                    Lib_File.Ini.WriteIni("Setting", "IsLowDrip", "1", s_path);
                    FADM_Object.Communal._b_isLowDrip = true;
                }
            }
            else
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)

                    FADM_Form.CustomMessageBox.Show("待机状态下才能修改", "温馨提示", MessageBoxButtons.OK, false);
                else
                    FADM_Form.CustomMessageBox.Show("Can only be modified in standby mode", "Tips", MessageBoxButtons.OK, false);
            }
        }

        private void MiFullDrip1_Click(object sender, EventArgs e)
        {
            if ((LabStatus.Text == "待机" || LabStatus.Text == "Standby") && null == FADM_Object.Communal.ReadDyeThread())
            {
                if (MiFullDrip1.Checked)
                {
                    MiFullDrip1.Checked = false;
                    string s_path = Environment.CurrentDirectory + "\\Config\\Config.ini";
                    Lib_File.Ini.WriteIni("Setting", "IsFullDrip", "0", s_path);
                    FADM_Object.Communal._b_isFullDrip = false;
                }
                else
                {
                    MiFullDrip1.Checked = true;
                    string s_path = Environment.CurrentDirectory + "\\Config\\Config.ini";
                    Lib_File.Ini.WriteIni("Setting", "IsFullDrip", "1", s_path);
                    FADM_Object.Communal._b_isFullDrip = true;
                }
            }
            else
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)

                    FADM_Form.CustomMessageBox.Show("待机状态下才能修改", "温馨提示", MessageBoxButtons.OK, false);
                else
                    FADM_Form.CustomMessageBox.Show("Can only be modified in standby mode", "Tips", MessageBoxButtons.OK, false);
            }
        }
    }
}
