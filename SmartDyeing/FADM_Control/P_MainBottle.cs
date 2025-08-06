using SmartDyeing.FADM_Auto;
using SmartDyeing.FADM_Form;
using SmartDyeing.FADM_Object;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartDyeing.FADM_Control
{
    public partial class P_MainBottle : UserControl
    {
        [DllImport("user32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Auto)]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        public const int WM_CLOSE = 0x10;

        private int _i_bottleAlarmWeight,
                    _i_bottleMinWeight,
                    _i_machineType;
        private List<Bottle> _lis_bottle = new List<Bottle>();
        private List<Cup> _lis_cup = new List<Cup>();
        private List<Label> _lis_lab = new List<Label>();

        private Balance _balance;

        int _i_nBottleNum = 0;

        public P_MainBottle()
        {
            InitializeComponent();

            //定义母液瓶资料表
            DataTable dt_bottledetails = new DataTable();

            //定义染助剂资料表
            DataTable dt_assistantdetails = new DataTable();

            //获取机台型号
            _i_machineType = Lib_Card.Configure.Parameter.Machine_Bottle_Total;

            //获取瓶子列数
            int i_bottleLine = Lib_Card.Configure.Parameter.Machine_Bottle_Column;

            _i_bottleAlarmWeight = Convert.ToInt32(Lib_Card.Configure.Parameter.Other_Bottle_AlarmWeight);
            _i_bottleMinWeight = Convert.ToInt32(Lib_Card.Configure.Parameter.Other_Bottle_MinWeight);


            //获取母液库存警告值

            //获取母液库存最低值
            _balance = new Balance();
            int i_balanceX = 0;

            //显示瓶子
            for (int i = 1; i <= _i_machineType; i++)
            {
                //初始化母液瓶
                Bottle bottle = new Bottle();

                //计算瓶子X轴间隔
                int i_bottleInterval_X = (this.PnlBottle.Width - bottle.Width * ((_i_machineType / i_bottleLine) + 1)) / (_i_machineType / i_bottleLine);

                //计算瓶子Y轴间隔
                int i_bottleInterval_Y = (this.PnlBottle.Height - 10 - (bottle.Height + 10) * i_bottleLine) / (i_bottleLine - 1);

                //定义母液瓶名称
                bottle.Name = "Bottle" + i;

                //显示母液瓶瓶号
                bottle.NO = i.ToString();

                //计算母液瓶坐标
                //if (i <= machineType - 14)
                    bottle.Location = new Point(((i - 1) / i_bottleLine * (i_bottleInterval_X + bottle.Width)),
                         ((i - 1) % i_bottleLine) * (i_bottleInterval_Y + bottle.Height + 10));
                //else if (i <= machineType - 7)
                //    bottle.Location = new Point((((machineType - 14) / bottleLine + ((i + 14 - machineType) / 8)) * (bottleInterval_X + bottle.Width)),
                //       ((i + 13 - machineType) % 8 + 3) * (bottleInterval_Y + bottle.Height + 10));
                //else
                //    bottle.Location = new Point((((machineType - 14) / bottleLine + ((i + 14 - machineType) / 8)) * (bottleInterval_X + bottle.Width)),
                //      ((i + 14 - machineType) % 8 + 3) * (bottleInterval_Y + bottle.Height + 10));

                //iBalanceX = (machineType - 14) / bottleLine * (bottleInterval_X + bottle.Width) + ((bottleInterval_X + bottle.Width * 2 - balance.Width) / 2) - 10;

                i_balanceX = 1200;
                //关联母液瓶的点击事件
                bottle.Click += Bottle_Click;
                bottle.MouseUp += Bottle_MouseUp;
                //关联母液瓶的鼠标离开事件
                bottle.MouseLeave += Bottle_MouseLeave;

                //显示母液瓶
                this.PnlBottle.Controls.Add(bottle);
                _lis_bottle.Add(bottle);
            }
            //初始化母液瓶信息
            bottle_update();

           

            //区域1
            if (Lib_Card.Configure.Parameter.Machine_Area1_Type == 1 || Lib_Card.Configure.Parameter.Machine_Area1_Type == 2)
            {
                FADM_Control.P_DripBeater s = new P_DripBeater(Lib_Card.Configure.Parameter.Machine_Area1_Row, Lib_Card.Configure.Parameter.Machine_Area1_CupMin, Lib_Card.Configure.Parameter.Machine_Area1_CupMax);
                this.panel1.Controls.Add(s);
                string s_str1 = null;
                if (Lib_Card.Configure.Parameter.Machine_Area1_Type == 1)
                {
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    {
                        s_str1 = "一号前处理区";
                    }
                    else
                    {
                        s_str1 = "No.1 pre-treatment area";
                    }
                }
                else
                {
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    {
                        s_str1 = "一号滴液区";
                    }
                    else
                    {
                        s_str1 = "No.1 drip area";
                    }
                }

                foreach (Control c1 in s.Controls)
                {

                    if (c1 is GroupBox)
                    {
                        c1.Text = s_str1;
                        foreach (Control c2 in c1.Controls)
                        {
                            if (c2 is Cup)
                            {
                                ((Cup)c2).Click += Cup_Click2;
                                ((Cup)c2).MouseLeave += Cup_MouseLeave2;
                                _lis_cup.Add((Cup)c2);
                            }
                        }
                    }
                }

            }
            else if (Lib_Card.Configure.Parameter.Machine_Area1_Type == 3)
            {
                FADM_Control.Beater s = new Beater();
                this.panel1.Controls.Add(s);

                foreach (Control c1 in s.Controls)
                {
                    if (c1 is GroupBox)
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        {
                            c1.Text = "一号染固色区";
                        }
                        else
                        {

                            c1.Text = "No.1 dyeing and fixation area";
                        }
                        int n = 0;
                        foreach (Control c2 in c1.Controls)
                        {
                            if (c2 is Cup)
                            {
                                ((Cup)c2).Name = (Lib_Card.Configure.Parameter.Machine_Area1_CupMin + n).ToString();
                                ((Cup)c2).NO = (Lib_Card.Configure.Parameter.Machine_Area1_CupMin + n).ToString();
                                ((Cup)c2).Click += Cup_Click;
                                ((Cup)c2).MouseLeave += Cup_MouseLeave;
                                _lis_cup.Add((Cup)c2);
                                n++;
                            }
                            else if (c2 is Label)
                            {
                                _lis_lab.Add((Label)c2);
                            }
                        }
                    }


                }
            }

            



            _balance.MaxValue = (decimal)Lib_Card.Configure.Parameter.Other_BalanceMaxWeight;
            if (Lib_Card.Configure.Parameter.Other_Language == 0)
            {
                _balance.m_NO = "天平";
            }
           else
            {
                _balance.m_NO = "Balance";
            }

            _balance.Location = new Point(i_balanceX, 50);
            this.Controls.Add(_balance);
        }

        //母液瓶点击事件
        void Bottle_Click(object sender, EventArgs e)
        {
            //Bottle Bottle = (Bottle)sender;

            //IntPtr ptr = FindWindow(null, "母液瓶详细资料");
            //if (ptr == IntPtr.Zero)
            //{
            //    new BottleDetails(Convert.ToInt16(Bottle.NO)).Show();
            //}


        }

        void Bottle_MouseDown(object sender, MouseEventArgs e)
        {
            Bottle Bottle = (Bottle)sender;
            _i_nBottleNum = Convert.ToInt16(Bottle.NO);
            //点击鼠标左键，显示母液瓶信息
            if (e.Button != MouseButtons.Right)
            {
                IntPtr ptr;
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    ptr = FindWindow(null, "母液瓶详细资料");
                else
                    ptr = FindWindow(null, "MotherLiquorInformation");
                if (ptr == IntPtr.Zero)
                {
                    new BottleDetails(Convert.ToInt16(Bottle.NO)).Show();
                }
            }
            else
            {
                //contextMenuStrip2.Show(MousePosition.X, MousePosition.Y);
            }
        }

        void Bottle_MouseUp(object sender, MouseEventArgs e)
        {
            Bottle Bottle = (Bottle)sender;
            _i_nBottleNum = Convert.ToInt16(Bottle.NO);
            //点击鼠标左键，显示母液瓶信息
            if (e.Button != MouseButtons.Right)
            {
                string s_temp = null;
                if(Lib_Card.Configure.Parameter.Other_Language == 0)
                {
                    s_temp = "母液瓶详细资料";
                }
                else
                {
                    s_temp = "MotherLiquorInformation";
                }

                IntPtr ptr = FindWindow(null, s_temp);
                if (ptr == IntPtr.Zero)
                {
                    new BottleDetails(Convert.ToInt16(Bottle.NO)).Show();
                }
            }
            else
            {
                contextMenuStrip2.Show(MousePosition.X, MousePosition.Y);
            }
        }

        //鼠标离开母液瓶事件
        void Bottle_MouseLeave(object sender, EventArgs e)
        {
            string s_temp = null;
            if (Lib_Card.Configure.Parameter.Other_Language == 0)
            {
                s_temp = "母液瓶详细资料";
            }
            else
            {
                s_temp = "MotherLiquorInformation";
            }

            IntPtr ptr = FindWindow(null, s_temp);
            if (ptr != IntPtr.Zero)
            {
                PostMessage(ptr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            }
        }

        //配液杯点击事件
        void Cup_Click(object sender, EventArgs e)
        {
            Cup Cup = (Cup)sender;
            IntPtr ptr;
            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                ptr = FindWindow(null, "配液杯详细资料");
            else
                ptr = FindWindow(null, "CupDetails");
            if (ptr == IntPtr.Zero)
            {
                string s_num = Cup.NO.ToString();

                int i_count = 0;
                if (Convert.ToInt16(s_num) >= Lib_Card.Configure.Parameter.Machine_Area3_CupMin)
                {
                    if (Lib_Card.Configure.Parameter.Machine_Area2_Type == 1 || Lib_Card.Configure.Parameter.Machine_Area2_Type == 2)
                    {
                        i_count += Lib_Card.Configure.Parameter.Machine_Area2_CupMax - Lib_Card.Configure.Parameter.Machine_Area2_CupMin + 1;
                    }
                    if (Lib_Card.Configure.Parameter.Machine_Area1_Type == 1 || Lib_Card.Configure.Parameter.Machine_Area1_Type == 2)
                    {
                        i_count += Lib_Card.Configure.Parameter.Machine_Area1_CupMax - Lib_Card.Configure.Parameter.Machine_Area1_CupMin + 1;
                    }

                }
                else if (Convert.ToInt16(s_num) >= Lib_Card.Configure.Parameter.Machine_Area2_CupMin)
                {
                    if (Lib_Card.Configure.Parameter.Machine_Area1_Type == 1 || Lib_Card.Configure.Parameter.Machine_Area1_Type == 2)
                    {
                        i_count += Lib_Card.Configure.Parameter.Machine_Area1_CupMax - Lib_Card.Configure.Parameter.Machine_Area1_CupMin + 1;
                    }
                }
                string s_state = _lis_lab[Convert.ToInt32(s_num) - i_count - 1].Text;
                if (!string.IsNullOrEmpty(s_state))
                {
                    if (s_state != "下线" && s_state != "待机")
                        new FADM_Form.CupDetails(Convert.ToInt16(s_num)).Show();
                }
            }
            else
            {
                //先删除页面，再重新打开
                PostMessage(ptr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);

                string s_num = Cup.NO.ToString();

                int i_count = 0;
                if (Convert.ToInt16(s_num) >= Lib_Card.Configure.Parameter.Machine_Area3_CupMin)
                {
                    if (Lib_Card.Configure.Parameter.Machine_Area2_Type == 1 || Lib_Card.Configure.Parameter.Machine_Area2_Type == 2)
                    {
                        i_count += Lib_Card.Configure.Parameter.Machine_Area2_CupMax - Lib_Card.Configure.Parameter.Machine_Area2_CupMin + 1;
                    }
                    if (Lib_Card.Configure.Parameter.Machine_Area1_Type == 1 || Lib_Card.Configure.Parameter.Machine_Area1_Type == 2)
                    {
                        i_count += Lib_Card.Configure.Parameter.Machine_Area1_CupMax - Lib_Card.Configure.Parameter.Machine_Area1_CupMin + 1;
                    }

                }
                else if (Convert.ToInt16(s_num) >= Lib_Card.Configure.Parameter.Machine_Area2_CupMin)
                {
                    if (Lib_Card.Configure.Parameter.Machine_Area1_Type == 1 || Lib_Card.Configure.Parameter.Machine_Area1_Type == 2)
                    {
                        i_count += Lib_Card.Configure.Parameter.Machine_Area1_CupMax - Lib_Card.Configure.Parameter.Machine_Area1_CupMin + 1;
                    }
                }
                string s_state = _lis_lab[Convert.ToInt32(s_num) - i_count - 1].Text;
                if (!string.IsNullOrEmpty(s_state))
                {
                    if (s_state != "下线" && s_state != "待机")
                        new FADM_Form.CupDetails(Convert.ToInt16(s_num)).Show();
                }
            }
        }

        void Cup_Click2(object sender, EventArgs e)
        {
            Cup Cup = (Cup)sender;
            IntPtr ptr;
            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                ptr = FindWindow(null, "配液杯滴液资料");
            else
                ptr = FindWindow(null, "CupDetails");
            if (ptr == IntPtr.Zero)
            {
                string s_num = Cup.NO.ToString();
                new FADM_Form.DripCupDetails(Convert.ToInt16(s_num)).Show();
            }
            else
            {
                //先删除页面，再重新打开
                PostMessage(ptr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                string s_num = Cup.NO.ToString();
                new FADM_Form.DripCupDetails(Convert.ToInt16(s_num)).Show();
            }
        }

        //鼠标离开配液杯事件
        void Cup_MouseLeave(object sender, EventArgs e)
        {
            //IntPtr ptr = FindWindow(null, "配液杯详细资料");
            //if (ptr != IntPtr.Zero)
            //{
            //    PostMessage(ptr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            //}

        }

        //鼠标离开配液杯事件
        void Cup_MouseLeave2(object sender, EventArgs e)
        {
            //IntPtr ptr = FindWindow(null, "配液杯详细资料");
            //if (ptr != IntPtr.Zero)
            //{
            //    PostMessage(ptr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            //}

        }


        private void Tmr_Tick(object sender, EventArgs e)
        {
            try
            {

                //更新所以瓶
                bottle_update();
                cup_update();
                if (Lib_Card.Configure.Parameter.Machine_Type == 0)
                {
                    if (Lib_Card.Configure.Parameter.Machine_BalanceType == 0)
                    {
                        if (6666 != Lib_SerialPort.Balance.METTLER.BalanceValue &&
                            7777 != Lib_SerialPort.Balance.METTLER.BalanceValue &&
                            8888 != Lib_SerialPort.Balance.METTLER.BalanceValue &&
                            9999 != Lib_SerialPort.Balance.METTLER.BalanceValue)
                            _balance.Title = string.Format("{0:F}", Lib_SerialPort.Balance.METTLER.BalanceValue);
                    }
                    else
                    {
                        if (6666 != Lib_SerialPort.Balance.SHINKO.BalanceValue &&
                            //7777 != Lib_SerialPort.Balance.SHINKO.BalanceValue &&
                            8888 != Lib_SerialPort.Balance.SHINKO.BalanceValue &&
                            9999 != Lib_SerialPort.Balance.SHINKO.BalanceValue)
                            _balance.Title = string.Format("{0:F3}", Lib_SerialPort.Balance.SHINKO.BalanceValue);
                    }

                    if (FADM_Object.Communal._b_balanceAlarm)
                        this._balance.LiquidColor = Color.Red;
                    else
                        this._balance.LiquidColor = Color.DeepSkyBlue;
                }
                else
                {
                    if ("6666" != FADM_Object.Communal._s_balanceValue &&
                        "7777" != FADM_Object.Communal._s_balanceValue &&
                        "8888" != FADM_Object.Communal._s_balanceValue &&
                        "9999" != FADM_Object.Communal._s_balanceValue)
                    {
                        _balance.Title = Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? string.Format("{0:F}", FADM_Object.Communal._s_balanceValue) : string.Format("{0:F3}", FADM_Object.Communal._s_balanceValue);
                        this._balance.LiquidColor = Color.DeepSkyBlue;
                    }
                    else
                    {
                        this._balance.LiquidColor = Color.Red;
                    }
                }
                    

                //更新当前批次母液瓶不足信息
                Bottle_low();

            }
            catch
            {
                // new Class_Alarm.MyAlarm(ex.Message, "定时器", false);
            }
        }

        //当前批次母液量不足瓶号
        private void Bottle_low()
        {
            //
            string s_sql = "select drop_details.BottleNum  from drop_details left join bottle_details on drop_details.BottleNum = bottle_details.BottleNum " +
                "where bottle_details.CurrentWeight <='" + this._i_bottleAlarmWeight + "' and drop_details.BatchName !='0' group by drop_details.BottleNum order by drop_details.BottleNum;";
            DataTable dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
            string s_bootlelow = null;
            List<int> lis_bootlelow = new List<int>();

            string s_bootleExpire = null;
            List<int> lis_bootleExpire = new List<int>();

            foreach (DataRow dr in dt_data.Rows)
            {
                //P_str_bootlelow += (dr[0] + ",");
                lis_bootlelow.Add(Convert.ToInt16(dr[0]));
            }

            s_sql = "select dye_details.BottleNum  from dye_details left join bottle_details on dye_details.BottleNum = bottle_details.BottleNum " +
                "where bottle_details.CurrentWeight <='" + this._i_bottleAlarmWeight + "' and dye_details.BatchName !='0' and dye_details.BottleNum is not null group by dye_details.BottleNum order by dye_details.BottleNum;";
            dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

            foreach (DataRow dr in dt_data.Rows)
            {
                if (!lis_bootlelow.Contains(Convert.ToInt16(dr[0])))
                    //P_str_bootlelow += (dr[0] + ",");
                    lis_bootlelow.Add(Convert.ToInt16(dr[0]));
            }

            lis_bootlelow.Sort();

            for (int i = 0; i < lis_bootlelow.Count(); i++)
            {
                s_bootlelow += lis_bootlelow[i].ToString() + ",";
            }


            if (s_bootlelow != null)
            {
                //lab_Low.Visible = true;
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                {
                    lab_Low.Text = "液量低瓶号:" + s_bootlelow.Substring(0, s_bootlelow.Length - 1);
                }
                else
                {
                    lab_Low.Text = "LowLiquidVolumeBottleNumber:" + s_bootlelow.Substring(0, s_bootlelow.Length - 1);
                }
            }
            else
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                {
                    //lab_Low.Visible = false;
                    lab_Low.Text = "液量低瓶号:";
                }
                else
                {
                    lab_Low.Text = "LowLiquidVolumeBottleNumber:";
                }
            }

            //获取过期瓶号
            s_sql = "select drop_details.BottleNum from drop_details left join assistant_details on drop_details.AssistantCode = assistant_details.AssistantCode " +
                "left join bottle_details on drop_details.BottleNum = bottle_details.BottleNum where datediff(HOUR,  bottle_details.BrewingData,GETDATE())>= TermOfValidity and drop_details.BatchName !='0' group by drop_details.BottleNum order by drop_details.BottleNum";
            dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

            foreach (DataRow dr in dt_data.Rows)
            {
                lis_bootleExpire.Add(Convert.ToInt16(dr[0]));
            }

            s_sql = "select dye_details.BottleNum from dye_details left join assistant_details on dye_details.AssistantCode = assistant_details.AssistantCode " +
               "left join bottle_details on dye_details.BottleNum = bottle_details.BottleNum where datediff(HOUR,  bottle_details.BrewingData,GETDATE())>= TermOfValidity  and dye_details.BottleNum is not null and dye_details.BatchName !='0' group by dye_details.BottleNum order by dye_details.BottleNum";
            dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

            foreach (DataRow dr in dt_data.Rows)
            {
                if (!lis_bootleExpire.Contains(Convert.ToInt16(dr[0])))
                    lis_bootleExpire.Add(Convert.ToInt16(dr[0]));
            }

            lis_bootleExpire.Sort();

            for (int i = 0; i < lis_bootleExpire.Count(); i++)
            {
                s_bootleExpire += lis_bootleExpire[i].ToString() + ",";
            }

            if (s_bootleExpire != null)
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                {
                    //lab_Expire.Visible = true;
                    lab_Expire.Text = "已过期瓶号:" + s_bootleExpire.Substring(0, s_bootleExpire.Length - 1);
                }
                else
                {
                    lab_Expire.Text = "ExpiredBottleNumber:" + s_bootleExpire.Substring(0, s_bootleExpire.Length - 1);
                }
                }
            else
            {
                //lab_Expire.Visible = false;
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                {
                    lab_Expire.Text = "已过期瓶号:";
                }
                else
                {
                    lab_Expire.Text = "ExpiredBottleNumber:";
                }
            }
        }


        private static string[] sa_bottleInfo = new string[Lib_Card.Configure.Parameter.Machine_Bottle_Total];
        /// <summary>
        /// 更新瓶子信息
        /// </summary>
        /// <param name="bottle">瓶子</param>
        private void bottle_update()
        {
            try
            {
                string s_sql = "SELECT bottle_details.*,AssistantName,UnitOfAccount,TermOfValidity,AllowMinColoringConcentration, AllowMaxColoringConcentration " +
                    "FROM bottle_details left join assistant_details on bottle_details.AssistantCode = assistant_details.AssistantCode ORDER BY BottleNum ;";

                DataTable dt_bottledetails = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);


                int i_i = 0;
                foreach (DataRow dr in dt_bottledetails.Rows)
                {
                    i_i++;
                    if (Convert.ToInt16(dr["BottleNum"]) > Lib_Card.Configure.Parameter.Machine_Bottle_Total)
                        continue;

                    //设置母液瓶染助剂名称
                    _lis_bottle[Convert.ToInt16(dr["BottleNum"]) - 1].Title = dr["AssistantName"].ToString();

                    //设置母液瓶当前库存量
                    _lis_bottle[Convert.ToInt16(dr["BottleNum"]) - 1].Value = (decimal)Convert.ToDouble(dr[9]);

                    //获取泡制时间
                    DateTime brewTime = Convert.ToDateTime(dr[10]);

                    double d_blCompCoefficient = Convert.ToDouble(dr["AllowMinColoringConcentration"]);
                    double d_blCompConstant = Convert.ToDouble(dr["AllowMaxColoringConcentration"]);

                    //获取有效期
                    string s_termOfValidity = dr["TermOfValidity"].ToString();

                    //获取当前时间
                    DateTime timeNow = DateTime.Now;

                    //计算时间差
                    UInt32 timeDifference = Convert.ToUInt32(timeNow.Subtract(brewTime).Duration().TotalSeconds);

                    //设置母液瓶液体颜色
                    if (_lis_bottle[Convert.ToInt16(dr["BottleNum"]) - 1].Value > this._i_bottleAlarmWeight)
                    {
                        //液量足够
                        if (timeDifference > Convert.ToUInt32(s_termOfValidity) * 60 * 60)
                        {
                            //过期
                            _lis_bottle[Convert.ToInt16(dr["BottleNum"]) - 1].LiquidColor = Color.Red;
                            if (d_blCompCoefficient != 0 || d_blCompConstant != 0)
                            {
                                //  FADM_Object.Communal._fadmSqlserver.InsertSpeechInfo(Convert.ToInt16(dr["BottleNum"]) + "号母液瓶过期，请重新泡制");
                                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                    sa_bottleInfo[Convert.ToInt16(dr["BottleNum"]) - 1] = Lib_Card.CardObject.InsertD(Convert.ToInt16(dr["BottleNum"]) + "号母液瓶过期，请重新泡制", "bottle_update");
                                else
                                    sa_bottleInfo[Convert.ToInt16(dr["BottleNum"]) - 1] = Lib_Card.CardObject.InsertD("The " + Convert.ToInt16(dr["BottleNum"]) + " mother liquor bottle has expired, please re brew it", "bottle_update");
                            }
                        }
                        else if (timeDifference > (Convert.ToUInt32(s_termOfValidity) - 4) * 60 * 60)
                        {
                            //提前4小时预警
                            _lis_bottle[Convert.ToInt16(dr["BottleNum"]) - 1].LiquidColor = Color.Green;
                        }
                        else
                        {
                            //未过期
                            _lis_bottle[Convert.ToInt16(dr["BottleNum"]) - 1].LiquidColor = Color.DeepSkyBlue;

                            if (d_blCompCoefficient != 0 || d_blCompConstant != 0)
                            {
                                //FADM_Object.Communal._fadmSqlserver.DeleteSpeechInfo(Convert.ToInt16(dr["BottleNum"]) + "号母液瓶过期，请重新泡制");
                                Lib_Card.CardObject.DeleteD(sa_bottleInfo[Convert.ToInt16(dr["BottleNum"]) - 1]);
                            }
                        }
                    }
                    else
                    {

                        if (_lis_bottle[Convert.ToInt16(dr["BottleNum"]) - 1].Value > this._i_bottleMinWeight)
                        {
                            //液量不够但大于最低值
                            if (timeDifference > Convert.ToUInt32(s_termOfValidity) * 60 * 60)
                            {
                                //过期
                                _lis_bottle[Convert.ToInt16(dr["BottleNum"]) - 1].LiquidColor = Color.Red;
                                if (d_blCompCoefficient != 0 || d_blCompConstant != 0)
                                {
                                    //FADM_Object.Communal._fadmSqlserver.InsertSpeechInfo(Convert.ToInt16(dr["BottleNum"]) + "号母液瓶过期，请重新泡制");
                                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                        sa_bottleInfo[Convert.ToInt16(dr["BottleNum"]) - 1] = Lib_Card.CardObject.InsertD(Convert.ToInt16(dr["BottleNum"]) + "号母液瓶过期，请重新泡制", "bottle_update");
                                    else
                                        sa_bottleInfo[Convert.ToInt16(dr["BottleNum"]) - 1] = Lib_Card.CardObject.InsertD("The " + Convert.ToInt16(dr["BottleNum"]) + " mother liquor bottle has expired, please re brew it", "bottle_update");
                                }
                            }
                            else if (timeDifference > (Convert.ToUInt32(s_termOfValidity) - 4) * 60 * 60)
                            {
                                //提前4小时预警
                                _lis_bottle[Convert.ToInt16(dr["BottleNum"]) - 1].LiquidColor = Color.Green;
                            }
                            else
                            {
                                //未过期
                                _lis_bottle[Convert.ToInt16(dr["BottleNum"]) - 1].LiquidColor = Color.Yellow;
                                if (d_blCompCoefficient != 0 || d_blCompConstant != 0)
                                {
                                    // FADM_Object.Communal._fadmSqlserver.DeleteSpeechInfo(Convert.ToInt16(dr["BottleNum"]) + "号母液瓶过期，请重新泡制");
                                    Lib_Card.CardObject.DeleteD(sa_bottleInfo[Convert.ToInt16(dr["BottleNum"]) - 1]);
                                }
                            }
                        }
                        else
                        {
                            //液量不够
                            _lis_bottle[Convert.ToInt16(dr["BottleNum"]) - 1].LiquidColor = Color.Transparent;
                        }
                    }

                    string s_checksuccess = Convert.ToString(dr[14]);
                    if (_lis_bottle[Convert.ToInt16(dr["BottleNum"]) - 1].NO == SmartDyeing.FADM_Object.Communal._i_optBottleNum.ToString())
                    {
                        if (this._b_bottle_twinkle_run == false)
                        {
                            Thread P_thd_twinkle = new Thread(bottle_twinkle);
                            P_thd_twinkle.IsBackground = true;
                            P_thd_twinkle.Start(_lis_bottle[Convert.ToInt16(dr["BottleNum"]) - 1]);
                        }
                    }
                    else
                    {
                        if (s_checksuccess == "False" || s_checksuccess == "0")
                        {

                            _lis_bottle[Convert.ToInt16(dr["BottleNum"]) - 1].bottleColor = Color.Red;
                        }
                        else
                        {
                            _lis_bottle[Convert.ToInt16(dr["BottleNum"]) - 1].bottleColor = Color.Black;
                        }
                    }
                }
            }
            catch (Exception ex)
            { }

        }
        /// <summary>
        /// 更新瓶子信息
        /// </summary>
        /// <param name="bottle">瓶子</param>
        private void bottle_update(Bottle bottle)
        {
            string s_sql = "SELECT bottle_details.*,AssistantName,UnitOfAccount,TermOfValidity FROM bottle_details left join assistant_details on bottle_details.AssistantCode = assistant_details.AssistantCode where bottle_details.BottleNum = '" + bottle.NO + "' ORDER BY BottleNum ;";

            DataTable dt_bottledetails = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

            if (dt_bottledetails.Rows.Count > 0)
            {
                //设置母液瓶染助剂名称
                bottle.Title = dt_bottledetails.Rows[0]["AssistantName"].ToString();

                //设置母液瓶当前库存量
                bottle.Value = (decimal)Convert.ToDouble(dt_bottledetails.Rows[0][9]);

                //获取泡制时间
                DateTime brewTime = Convert.ToDateTime(dt_bottledetails.Rows[0][10]);

                //获取有效期
                string s_termOfValidity = dt_bottledetails.Rows[0]["TermOfValidity"].ToString();

                //获取当前时间
                DateTime timeNow = DateTime.Now;

                //计算时间差
                UInt32 timeDifference = Convert.ToUInt32(timeNow.Subtract(brewTime).Duration().TotalSeconds);

                //设置母液瓶液体颜色
                if (bottle.Value > this._i_bottleAlarmWeight)
                {
                    //液量足够
                    if (timeDifference > Convert.ToUInt32(s_termOfValidity) * 60 * 60)
                    {
                        //过期
                        bottle.LiquidColor = Color.Red;
                    }
                    else
                    {
                        //未过期
                        bottle.LiquidColor = Color.DeepSkyBlue;
                    }
                }
                else
                {

                    if (bottle.Value > this._i_bottleMinWeight)
                    {
                        //液量不够但大于最低值
                        if (timeDifference > Convert.ToUInt32(s_termOfValidity) * 60 * 60)
                        {
                            //过期
                            bottle.LiquidColor = Color.Red;
                        }
                        else
                        {
                            //未过期
                            bottle.LiquidColor = Color.Yellow;
                        }
                    }
                    else
                    {
                        //液量不够
                        bottle.LiquidColor = Color.Transparent;
                    }
                }

                //设置瓶子颜色
                string s_checksuccess = Convert.ToString(dt_bottledetails.Rows[0][14]);
                if (bottle.NO == SmartDyeing.FADM_Object.Communal._i_optBottleNum.ToString())
                {
                    if (this._b_bottle_twinkle_run == false)
                    {
                        Thread P_thd_twinkle = new Thread(bottle_twinkle);
                        P_thd_twinkle.IsBackground = true;
                        P_thd_twinkle.Start(bottle);
                    }
                }
                else
                {
                    if (s_checksuccess == "False" || s_checksuccess == "0")
                    {

                        bottle.bottleColor = Color.Red;
                    }
                    else
                    {
                        bottle.bottleColor = Color.Black;
                    }
                }

            }
            else
            {
                bottle.Title = null;
                bottle.Value = 0;
                bottle.LiquidColor = Color.Transparent;
                bottle.bottleColor = Color.Black;
            }
        }

        //运行中瓶子闪烁线程
        private bool _b_bottle_twinkle_run = false;
        private void bottle_twinkle(object obj)
        {
            this._b_bottle_twinkle_run = true;
            while (((Bottle)obj).NO == SmartDyeing.FADM_Object.Communal._i_optBottleNum.ToString())
            {
                ((Bottle)obj).bottleColor = Color.Yellow;
                Thread.Sleep(1000);
                ((Bottle)obj).bottleColor = Color.Black;
                Thread.Sleep(1000);
            }
            this._b_bottle_twinkle_run = false;
        }

        /// <summary>
        /// 更新配液杯信息
        /// </summary>
        /// <param name="cup"></param>
        private void cup_update(Cup cup)
        {
            lock (this)
            {
                try
                {
                    //获取当前批次号
                    string s_sql = "SELECT BatchName FROM enabled_set" +
                                       " WHERE MyID = 1 ";

                    DataTable dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                    string s_batch = Convert.ToString(dt_data.Rows[0][dt_data.Columns[0]]);

                    //获取配液杯资料
                    s_sql = "SELECT * FROM drop_head" +
                                " WHERE CupNum = " + cup.NO + "; ";
                    dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);



                    if (dt_data.Rows.Count > 0)
                    {
                        //更新当前杯的文本
                        cup.Title = Convert.ToString(dt_data.Rows[0][dt_data.Columns["FormulaCode"]]);

                        //更新当前杯的最大值

                        s_sql = "SELECT SUM(CAST(ISNULL(ObjectDropWeight,0.00) as numeric(18,2))) FROM drop_details WHERE" +
                                    " CupNum = '" + cup.NO + "' " +
                                    " AND BottleNum > 0 AND ( BottleNum <= " + _i_machineType + ");";

                        DataTable dt_data1 = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                        //获取配液杯资料
                        s_sql = "SELECT * FROM drop_head" +
                                    " WHERE CupNum = " + cup.NO + "; ";
                        dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                        if (dt_data1.Rows[0][0] is DBNull)
                        {
                        }
                        else
                        {
                            if (Convert.ToString(dt_data.Rows[0][dt_data.Columns["AddWaterChoose"]]) == "False" || Convert.ToString(dt_data.Rows[0][dt_data.Columns["AddWaterChoose"]]) == "0")
                            {
                                //不加水
                                cup.maxValue = (decimal)Convert.ToInt16(Convert.ToDouble(dt_data1.Rows[0][dt_data1.Columns[0]]) > 1 ?
                                                  Convert.ToDouble(dt_data1.Rows[0][dt_data1.Columns[0]]) : 1);
                            }
                            else
                            {

                                cup.maxValue = (decimal)Convert.ToInt16((Convert.ToDouble(dt_data1.Rows[0][dt_data1.Columns[0]]) +
                                                         (Convert.ToDouble(dt_data.Rows[0][dt_data.Columns["ObjectAddWaterWeight"]]))) > 1 ?
                                                         (Convert.ToDouble(dt_data1.Rows[0][dt_data1.Columns[0]]) +
                                                         (Convert.ToDouble(dt_data.Rows[0][dt_data.Columns["ObjectAddWaterWeight"]]))) : 1);

                            }
                        }
                        //更新当前杯的液体颜色
                        if (Convert.ToString(dt_data.Rows[0][dt_data.Columns["CupFinish"]]) == "False" || Convert.ToString(dt_data.Rows[0][dt_data.Columns["CupFinish"]]) == "0")
                        {
                            cup.LiquidColor = Color.DeepSkyBlue;

                            //更新当前杯的杯子颜色
                            if (cup.NO == SmartDyeing.FADM_Object.Communal._i_OptCupNum.ToString())
                            {
                                if (this._b_cup_twinkle_run == false)
                                {
                                    Thread P_thd_twinkle = new Thread(cup_twinkle);
                                    P_thd_twinkle.IsBackground = true;
                                    P_thd_twinkle.Start(cup);
                                }
                            }
                            else
                            {
                                cup.cupColor = Color.Black;
                            }
                        }
                        else
                        {

                            //完成

                            if (cup.LiquidColor != Color.Red && cup.LiquidColor != Color.Lime)
                            {

                                double d_bl_drop_allow_err = Lib_Card.Configure.Parameter.Other_AErr_Drip;
                                double d_water_allow_err = Lib_Card.Configure.Parameter.Other_AErr_Water;

                                s_sql = "SELECT CupNum FROM drop_system.drop_details WHERE" +
                                           " (ROUND(ABS(ObjectDropWeight * 100 - RealDropWeight * 100),2) > " + d_bl_drop_allow_err * 100 + " AND" +
                                           " BottleNum > 0 AND BottleNum <= " + _i_machineType + ")   GROUP BY CupNum;";

                                DataTable dt_data2 = new DataTable();
                                dt_data2 = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                                bool b_err = false;
                                foreach (DataRow dr in dt_data1.Rows)
                                {
                                    if (Convert.ToInt16(dr[0]) == Convert.ToInt16(cup.NO))
                                    {
                                        b_err = true;
                                        break;
                                    }
                                }
                                if (b_err)
                                {
                                    cup.LiquidColor = Color.Red;
                                    b_err = false;
                                }
                                else
                                {

                                    s_sql = "SELECT * FROM drop_system.drop_head WHERE" +
                                                 " CupNum =" + cup.NO + ";";
                                    DataTable dt_allcup = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                                    double d_bl_objWater = Convert.ToDouble(dt_allcup.Rows[0][dt_allcup.Columns["ObjectAddWaterWeight"]]);
                                    double d_bl_realWater = Convert.ToDouble(dt_allcup.Rows[0][dt_allcup.Columns["RealAddWaterWeight"]]);

                                    double d_bl_TestTubeObjectAddWaterWeight = Convert.ToDouble(dt_allcup.Rows[0][dt_allcup.Columns["TestTubeObjectAddWaterWeight"]]);
                                    double d_testTubeRealAddWaterWeight = Convert.ToDouble(dt_allcup.Rows[0][dt_allcup.Columns["TestTubeRealAddWaterWeight"]]);


                                    double d_bl_realDif = Convert.ToDouble(string.Format("{0:F3}", d_bl_realWater - d_bl_objWater));
                                    d_bl_realDif = d_bl_realDif < 0 ? -d_bl_realDif : d_bl_realDif;
                                    double d_bl_allDif = Convert.ToDouble(string.Format("{0:F3}", d_bl_objWater * Convert.ToDouble(d_water_allow_err / 100.00)));

                                    double d_bl_allow_water_err = Convert.ToDouble(string.Format("{0:F3}", d_water_allow_err));

                                    double d_bl_real_water_err = Convert.ToDouble(string.Format("{0:F3}", d_bl_realDif / d_bl_objWater * 100));

                                    double d_bl_TestTube_err = Convert.ToDouble(string.Format("{0:F3}", d_bl_TestTubeObjectAddWaterWeight - d_testTubeRealAddWaterWeight));

                                    d_bl_TestTube_err = d_bl_TestTube_err < 0 ? -d_bl_TestTube_err : d_bl_TestTube_err;

                                    if (d_bl_allow_water_err < d_bl_real_water_err || d_bl_TestTube_err > d_bl_drop_allow_err)
                                    {
                                        cup.LiquidColor = Color.Red;
                                    }
                                    else
                                    {
                                        cup.LiquidColor = Color.Lime;
                                    }
                                }
                            }


                        }


                        //更新杯子当前液量
                        s_sql = "SELECT SUM(CAST(ISNULL(RealDropWeight,0.00) as numeric(18,2))) FROM drop_details" +
                                    " WHERE CupNum = '" + cup.NO + "';";


                        DataTable dt_weight = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                        double d_bl_weight_1 = 0;
                        if (dt_weight.Rows[0][dt_weight.Columns[0]].ToString() != "")
                        {
                            d_bl_weight_1 = Convert.ToDouble(dt_weight.Rows[0][dt_weight.Columns[0]]);
                        }
                        double d_bl_weight_2 = 0;

                        if (Convert.ToString(dt_data.Rows[0][dt_data.Columns["AddWaterFinish"]]) == "True" || Convert.ToString(dt_data.Rows[0][dt_data.Columns["AddWaterFinish"]]) == "1")
                        {
                            d_bl_weight_2 = Convert.ToDouble(dt_data.Rows[0][dt_data.Columns["ObjectAddWaterWeight"]]);
                        }


                        double d_bl_value = (d_bl_weight_1 + d_bl_weight_2) > Convert.ToDouble(cup.maxValue) ?
                            Convert.ToDouble(cup.maxValue) : (d_bl_weight_1 + d_bl_weight_2);
                        cup.Value = (decimal)(d_bl_value);

                    }
                    else
                    {

                        //this.pal_cup.Controls.Remove(cup);
                    }
                }
                catch (Exception ex)
                {
                    //new Class_Alarm.MyAlarm(ex.Message);
                }
            }

        }

        /// <summary>
        /// 更新配液杯信息
        /// </summary>
        /// <param name="cup"></param>
        private void cup_update()
        {
            lock (this)
            {
                try
                {
                    //获取配液杯资料
                    DataTable dt_data = FADM_Object.Communal._fadmSqlserver.GetData(
                        "SELECT * FROM cup_details order by CupNum;");

                    foreach (DataRow dr1 in dt_data.Rows)
                    {
                        Cup cup = _lis_cup[Convert.ToInt16(dr1["CupNum"].ToString()) - 1];
                        if (Convert.ToInt16(dr1["Type"].ToString()) == 3)
                        {
                            int i_count = 0;
                            string s_num = dr1["CupNum"].ToString();
                            if (Convert.ToInt16(s_num) >= Lib_Card.Configure.Parameter.Machine_Area6_CupMin)
                            {
                                if (Lib_Card.Configure.Parameter.Machine_Area5_Type == 1 || Lib_Card.Configure.Parameter.Machine_Area5_Type == 2)
                                {
                                    i_count += Lib_Card.Configure.Parameter.Machine_Area5_CupMax - Lib_Card.Configure.Parameter.Machine_Area5_CupMin + 1;
                                }
                                if (Lib_Card.Configure.Parameter.Machine_Area4_Type == 1 || Lib_Card.Configure.Parameter.Machine_Area4_Type == 2)
                                {
                                    i_count += Lib_Card.Configure.Parameter.Machine_Area4_CupMax - Lib_Card.Configure.Parameter.Machine_Area4_CupMin + 1;
                                }
                                if (Lib_Card.Configure.Parameter.Machine_Area3_Type == 1 || Lib_Card.Configure.Parameter.Machine_Area3_Type == 2)
                                {
                                    i_count += Lib_Card.Configure.Parameter.Machine_Area3_CupMax - Lib_Card.Configure.Parameter.Machine_Area3_CupMin + 1;
                                }
                                if (Lib_Card.Configure.Parameter.Machine_Area2_Type == 1 || Lib_Card.Configure.Parameter.Machine_Area2_Type == 2)
                                {
                                    i_count += Lib_Card.Configure.Parameter.Machine_Area2_CupMax - Lib_Card.Configure.Parameter.Machine_Area2_CupMin + 1;
                                }
                                if (Lib_Card.Configure.Parameter.Machine_Area1_Type == 1 || Lib_Card.Configure.Parameter.Machine_Area1_Type == 2)
                                {
                                    i_count += Lib_Card.Configure.Parameter.Machine_Area1_CupMax - Lib_Card.Configure.Parameter.Machine_Area1_CupMin + 1;
                                }

                            }
                            else if (Convert.ToInt16(s_num) >= Lib_Card.Configure.Parameter.Machine_Area5_CupMin)
                            {
                                if (Lib_Card.Configure.Parameter.Machine_Area4_Type == 1 || Lib_Card.Configure.Parameter.Machine_Area4_Type == 2)
                                {
                                    i_count += Lib_Card.Configure.Parameter.Machine_Area4_CupMax - Lib_Card.Configure.Parameter.Machine_Area4_CupMin + 1;
                                }
                                if (Lib_Card.Configure.Parameter.Machine_Area3_Type == 1 || Lib_Card.Configure.Parameter.Machine_Area3_Type == 2)
                                {
                                    i_count += Lib_Card.Configure.Parameter.Machine_Area3_CupMax - Lib_Card.Configure.Parameter.Machine_Area3_CupMin + 1;
                                }
                                if (Lib_Card.Configure.Parameter.Machine_Area2_Type == 1 || Lib_Card.Configure.Parameter.Machine_Area2_Type == 2)
                                {
                                    i_count += Lib_Card.Configure.Parameter.Machine_Area2_CupMax - Lib_Card.Configure.Parameter.Machine_Area2_CupMin + 1;
                                }
                                if (Lib_Card.Configure.Parameter.Machine_Area1_Type == 1 || Lib_Card.Configure.Parameter.Machine_Area1_Type == 2)
                                {
                                    i_count += Lib_Card.Configure.Parameter.Machine_Area1_CupMax - Lib_Card.Configure.Parameter.Machine_Area1_CupMin + 1;
                                }

                            }
                            else if (Convert.ToInt16(s_num) >= Lib_Card.Configure.Parameter.Machine_Area4_CupMin)
                            {
                                if (Lib_Card.Configure.Parameter.Machine_Area3_Type == 1 || Lib_Card.Configure.Parameter.Machine_Area3_Type == 2)
                                {
                                    i_count += Lib_Card.Configure.Parameter.Machine_Area3_CupMax - Lib_Card.Configure.Parameter.Machine_Area3_CupMin + 1;
                                }
                                if (Lib_Card.Configure.Parameter.Machine_Area2_Type == 1 || Lib_Card.Configure.Parameter.Machine_Area2_Type == 2)
                                {
                                    i_count += Lib_Card.Configure.Parameter.Machine_Area2_CupMax - Lib_Card.Configure.Parameter.Machine_Area2_CupMin + 1;
                                }
                                if (Lib_Card.Configure.Parameter.Machine_Area1_Type == 1 || Lib_Card.Configure.Parameter.Machine_Area1_Type == 2)
                                {
                                    i_count += Lib_Card.Configure.Parameter.Machine_Area1_CupMax - Lib_Card.Configure.Parameter.Machine_Area1_CupMin + 1;
                                }

                            }
                            else if (Convert.ToInt16(s_num) >= Lib_Card.Configure.Parameter.Machine_Area3_CupMin)
                            {
                                if (Lib_Card.Configure.Parameter.Machine_Area2_Type == 1 || Lib_Card.Configure.Parameter.Machine_Area2_Type == 2)
                                {
                                    i_count += Lib_Card.Configure.Parameter.Machine_Area2_CupMax - Lib_Card.Configure.Parameter.Machine_Area2_CupMin + 1;
                                }
                                if (Lib_Card.Configure.Parameter.Machine_Area1_Type == 1 || Lib_Card.Configure.Parameter.Machine_Area1_Type == 2)
                                {
                                    i_count += Lib_Card.Configure.Parameter.Machine_Area1_CupMax - Lib_Card.Configure.Parameter.Machine_Area1_CupMin + 1;
                                }

                            }
                            else if (Convert.ToInt16(s_num) >= Lib_Card.Configure.Parameter.Machine_Area2_CupMin)
                            {
                                if (Lib_Card.Configure.Parameter.Machine_Area1_Type == 1 || Lib_Card.Configure.Parameter.Machine_Area1_Type == 2)
                                {
                                    i_count += Lib_Card.Configure.Parameter.Machine_Area1_CupMax - Lib_Card.Configure.Parameter.Machine_Area1_CupMin + 1;
                                }
                            }

                            Label label = _lis_lab[Convert.ToInt16(dr1["CupNum"].ToString()) - i_count - 1];

                            label.Text = dr1["Statues"].ToString();
                        }

                        //获取配液杯资料
                        DataTable dt_head = FADM_Object.Communal._fadmSqlserver.GetData(
                            "SELECT * FROM drop_head WHERE CupNum = " + cup.NO + " AND BatchName != '0'; ");

                        if (dt_head.Rows.Count > 0)
                        {

                            //更新当前杯的最大值
                            DataTable dt_data1 = FADM_Object.Communal._fadmSqlserver.GetData(
                                "SELECT SUM(CAST(ISNULL(ObjectDropWeight,0.00) as numeric(18,2))) FROM drop_details WHERE" +
                                " CupNum = '" + cup.NO + "' " +
                                " AND BottleNum > 0 AND ( BottleNum <= " + _i_machineType + ");");
                            if (0 < dt_data1.Rows.Count)
                            {
                                if (Convert.ToString(dt_head.Rows[0]["AddWaterChoose"]) == "0")
                                {
                                    //不加水
                                    cup.maxValue = (decimal)Convert.ToInt16(Convert.ToDouble(dt_data1.Rows[0][0]) > 1 ?
                                                      Convert.ToDouble(dt_data1.Rows[0][0]) : 1);
                                }
                                else
                                {

                                    cup.maxValue = (decimal)Convert.ToInt16((Convert.ToDouble(dt_data1.Rows[0][0]) +
                                                             (Convert.ToDouble(dt_head.Rows[0]["ObjectAddWaterWeight"]))) > 1 ?
                                                             (Convert.ToDouble(dt_data1.Rows[0][0]) +
                                                             (Convert.ToDouble(dt_head.Rows[0]["ObjectAddWaterWeight"]))) : 1);

                                }
                            }

                            //更新当前杯的文本
                            cup.Title = Convert.ToString(dt_head.Rows[0]["FormulaCode"]);

                            //更新当前杯的液体颜色
                            if (Convert.ToString(dt_head.Rows[0]["CupFinish"]) == "0")
                            {
                                cup.LiquidColor = Color.DeepSkyBlue;

                                //更新当前杯的杯子颜色
                                if (cup.NO == FADM_Object.Communal._i_OptCupNum.ToString())
                                {
                                    if (this._b_cup_twinkle_run == false)
                                    {
                                        Thread P_thd_twinkle = new Thread(cup_twinkle);
                                        P_thd_twinkle.IsBackground = true;
                                        P_thd_twinkle.Start(cup);
                                    }
                                }
                                else
                                {
                                    cup.cupColor = Color.Black;
                                }
                            }
                            else
                            {

                                //完成
                                if (cup.LiquidColor != Color.Red && cup.LiquidColor != Color.Lime)
                                {

                                    //double d_bl_drop_allow_err = Lib_Card.Configure.Parameter.Other_AErr_Drip;
                                    //double d_water_allow_err = Lib_Card.Configure.Parameter.Other_AErr_DripWater;




                                    //DataTable dt_data2 = FADM_Object.Communal._fadmSqlserver.GetData(
                                    //    "SELECT CupNum FROM drop_details WHERE (ROUND(ABS(ObjectDropWeight * 100 - RealDropWeight * 100),2) > " +
                                    //    d_bl_drop_allow_err * 100 + " AND BottleNum > 0 AND BottleNum <= " + _i_machineType + ")   GROUP BY CupNum;");

                                    //bool b_err = false;
                                    //foreach (DataRow dr in dt_data2.Rows)
                                    //{
                                    //    if (Convert.ToInt16(dr[0]) == Convert.ToInt16(cup.NO))
                                    //    {
                                    //        b_err = true;
                                    //        break;
                                    //    }
                                    //}
                                    //if (b_err)
                                    //{
                                    //    cup.LiquidColor = Color.Red;
                                    //    b_err = false;
                                    //}
                                    //else
                                    //{

                                    //    DataTable dt_allcup = FADM_Object.Communal._fadmSqlserver.GetData(
                                    //        "SELECT * FROM drop_head WHERE CupNum =" + cup.NO + ";");

                                    //    double d_bl_objWater = Convert.ToDouble(dt_allcup.Rows[0][dt_allcup.Columns["ObjectAddWaterWeight"]]);
                                    //    double d_bl_realWater = Convert.ToDouble(dt_allcup.Rows[0][dt_allcup.Columns["RealAddWaterWeight"]]);
                                    //    double d_bl_TotalW = Convert.ToDouble(dt_allcup.Rows[0][dt_allcup.Columns["TotalWeight"]]);

                                    //    double d_bl_TestTubeObjectAddWaterWeight = Convert.ToDouble(dt_allcup.Rows[0][dt_allcup.Columns["TestTubeObjectAddWaterWeight"]]);
                                    //    double d_testTubeRealAddWaterWeight = Convert.ToDouble(dt_allcup.Rows[0][dt_allcup.Columns["TestTubeRealAddWaterWeight"]]);


                                    //    double d_bl_realDif = Convert.ToDouble(string.Format("{0:F3}", d_bl_realWater - d_bl_objWater));
                                    //    d_bl_realDif = d_bl_realDif < 0 ? -d_bl_realDif : d_bl_realDif;
                                    //    double d_bl_allDif = Convert.ToDouble(string.Format("{0:F3}", d_bl_objWater * Convert.ToDouble(d_water_allow_err / 100.00)));

                                    //    double d_bl_allow_water_err = Convert.ToDouble(string.Format("{0:F3}", d_water_allow_err));

                                    //    double d_bl_real_water_err = Convert.ToDouble(string.Format("{0:F3}", d_bl_realDif / d_bl_TotalW * 100));

                                    //    double d_bl_TestTube_err = Convert.ToDouble(string.Format("{0:F3}", d_bl_TestTubeObjectAddWaterWeight - d_testTubeRealAddWaterWeight));

                                    //    d_bl_TestTube_err = d_bl_TestTube_err < 0 ? -d_bl_TestTube_err : d_bl_TestTube_err;

                                    //    if (d_bl_allow_water_err < d_bl_real_water_err || d_bl_TestTube_err > d_bl_drop_allow_err || (d_bl_realWater == 0.00 && d_bl_objWater !=0.00))
                                    //    {
                                    //        cup.LiquidColor = Color.Red;
                                    //    }
                                    //    else
                                    //    {
                                    //        cup.LiquidColor = Color.Lime;
                                    //    }
                                    //}

                                    string s_describeChar = dt_head.Rows[0]["DescribeChar"] is DBNull ? "" : dt_head.Rows[0]["DescribeChar"].ToString();
                                    if (s_describeChar.Contains("失败"))
                                    {
                                        cup.LiquidColor = Color.Red;
                                    }
                                    else
                                    {
                                        cup.LiquidColor = Color.Lime;

                                        //再次判断后处理是否有不合格项
                                        string s_sql = "SELECT * from dye_details where AssistantCode is not null And Finish = 1 And  CupNum = " + cup.NO;
                                        DataTable P_dt_data = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                                        double d_bl_drop_allow_err = Lib_Card.Configure.Parameter.Other_AErr_Drip;

                                        bool b_err = false;
                                        //判断是否合格
                                        foreach (DataRow dr2 in P_dt_data.Rows)
                                        {
                                            double d_bl_RealErr = Convert.ToDouble(Lib_Card.Configure.Parameter.Machine_IsThousandsBalance == 0 ? string.Format("{0:F}", Convert.ToDouble(dr2["ObjectDropWeight"]) - Convert.ToDouble(dr2["RealDropWeight"])) : string.Format("{0:F3}", Convert.ToDouble(dr2["ObjectDropWeight"]) - Convert.ToDouble(dr2["RealDropWeight"])));
                                            d_bl_RealErr = d_bl_RealErr > 0 ? d_bl_RealErr : -d_bl_RealErr;
                                            if (dr2["StandError"] is DBNull)
                                            {
                                                d_bl_drop_allow_err = (dr2["UnitOfAccount"].ToString() == "%" ? Lib_Card.Configure.Parameter.Other_AErr_Drip : Lib_Card.Configure.Parameter.Other_AErr_Drip);
                                            }
                                            else
                                            {
                                                d_bl_drop_allow_err = Convert.ToDouble(dr2["StandError"]);
                                            }
                                            if (d_bl_RealErr > d_bl_drop_allow_err)
                                            {
                                                b_err = true;
                                                break;
                                            }

                                        }
                                        if (b_err)
                                        {
                                            cup.LiquidColor = Color.Red;
                                        }
                                    }
                                }


                            }


                            //更新杯子当前液量

                            DataTable dt_weight = FADM_Object.Communal._fadmSqlserver.GetData(
                                "SELECT SUM(CAST(ISNULL(RealDropWeight,0.00) as numeric(18,2))) FROM drop_details WHERE CupNum = '" + cup.NO + "';");

                            double d_bl_weight_1 = 0;
                            if (dt_weight.Rows[0][0].ToString() != "")
                            {
                                d_bl_weight_1 = Convert.ToDouble(dt_weight.Rows[0][0]);
                            }
                            double d_bl_weight_2 = 0;

                            if (Convert.ToString(dt_head.Rows[0]["AddWaterFinish"]) == "1")
                            {
                                d_bl_weight_2 = Convert.ToDouble(dt_head.Rows[0][dt_head.Columns["ObjectAddWaterWeight"]]);
                            }


                            double d_bl_value = (d_bl_weight_1 + d_bl_weight_2) > Convert.ToDouble(cup.maxValue) ?
                                Convert.ToDouble(cup.maxValue) : (d_bl_weight_1 + d_bl_weight_2);
                            cup.Value = (decimal)(d_bl_value);

                        }
                        else
                        {
                            cup.Title = null;
                            cup.Value = 0;
                        }
                    }
                }
                catch
                {

                }
            }

        }

        //运行中杯子闪烁线程
        private bool _b_cup_twinkle_run = false;
        private void cup_twinkle(object obj)
        {
            this._b_cup_twinkle_run = true;
            while (((Cup)obj).NO == SmartDyeing.FADM_Object.Communal._i_OptCupNum.ToString())
            {
                ((Cup)obj).cupColor = Color.Yellow;
                Thread.Sleep(500);
                ((Cup)obj).cupColor = Color.Black;
                Thread.Sleep(500);
            }
            this._b_cup_twinkle_run = false;
        }

        private void tsm_CheckAndSelf_Click(object sender, EventArgs e)
        {
            IntPtr ptr;
            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                ptr = FindWindow(null, "母液瓶针检");
            else
                ptr = FindWindow(null, "MotherLiquorCorrection");
            if (ptr == IntPtr.Zero)
            {
                new FADM_Form.Check().Show();
            }
            else
            {
                //先删除页面，再重新打开
                PostMessage(ptr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                new FADM_Form.Check().Show();
            }
        }

        private void tsm_Self_Click(object sender, EventArgs e)
        {
            IntPtr ptr;
            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                ptr = FindWindow(null, "母液瓶自检");
            else
                ptr = FindWindow(null, "MotherLiquorSelf-checking");
            if (ptr == IntPtr.Zero)
            {
                new FADM_Form.Self().Show();
            }
            else
            {
                //先删除页面，再重新打开
                PostMessage(ptr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                new FADM_Form.Self().Show();
            }
        }

        private void tsm_Water_Click(object sender, EventArgs e)
        {
            IntPtr ptr;
            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                ptr = FindWindow(null, "水校正");
            else
                ptr = FindWindow(null, "Water-Correction/Checking");
            if (ptr == IntPtr.Zero)
            {
                new FADM_Form.WaterCheckAndSelf().Show();
            }
            else
            {
                //先删除页面，再重新打开
                PostMessage(ptr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                new FADM_Form.WaterCheckAndSelf().Show();
            }
        }

        private void tsm_SignCheck_Click(object sender, EventArgs e)
        {

        }

        private void BottleCheck()
        {
            try
            {
                FADM_Object.Communal._fadmSqlserver.ReviseData("TRUNCATE TABLE bottle_check");

                char[] ca_chars = { ',', '，' };
                string[] sa_strings = TxtCheckBottleNo.Text.Split(ca_chars);

                char[] ca_chars1 = { '-', '-' };
                List<int> lis_ints = new List<int>();
                foreach (string s in sa_strings)
                {
                    string[] sa_strings1 = s.Split(ca_chars1);
                    if (1 == sa_strings1.Length)
                        lis_ints.Add(Convert.ToInt16(s));
                    else
                        for (int i = Convert.ToInt16(sa_strings1[0]); i <= Convert.ToInt16(sa_strings1[1]); i++)
                            lis_ints.Add(i);

                }

                lis_ints.Sort();
                lis_ints = lis_ints.Distinct().ToList();

                foreach (int i in lis_ints)
                {
                    DataTable dt_data = FADM_Object.Communal._fadmSqlserver.GetData(
                        "SELECT * FROM bottle_details WHERE BottleNum = " + i + ";");
                    if (0 == dt_data.Rows.Count || i > Lib_Card.Configure.Parameter.Machine_Bottle_Total)
                        continue;
                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                        "INSERT INTO bottle_check(BottleNum, Finish, Successed) VALUES('" + i + "',0,0);");


                }

                Thread thread = new Thread(() =>
                {
                    new FADM_Auto.BottleCheck().Check();
                });
                thread.Start();



            }
            catch (Exception ex)
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    FADM_Form.CustomMessageBox.Show(ex.Message, "BottleCheck",
                    MessageBoxButtons.OK, false);
                else
                {
                    string s_message = ex.Message;
                    if (SmartDyeing.FADM_Object.Communal._dic_warning.ContainsKey(ex.Message))
                    {
                        //如果存在就替换英文
                        s_message = SmartDyeing.FADM_Object.Communal._dic_warning[ex.Message];
                    }
                    FADM_Form.CustomMessageBox.Show(s_message, "BottleCheck",
                    MessageBoxButtons.OK, false);
                }

                FADM_Object.Communal.WriteDripWait(false);
                FADM_Object.Communal.WriteMachineStatus(8);
            }
        }

        private void tsm_SignSelf_Click(object sender, EventArgs e)
        {

        }

        private void BottleSelf()
        {
            try
            {
                FADM_Object.Communal._fadmSqlserver.ReviseData("TRUNCATE TABLE bottle_check");

                char[] ca_chars = { ',', '，' };
                string[] sa_strings = TxtSelfBottleNo.Text.Split(ca_chars);

                char[] ca_chars1 = { '-', '-' };
                List<int> lis_ints = new List<int>();
                foreach (string s in sa_strings)
                {
                    string[] sa_strings1 = s.Split(ca_chars1);
                    if (1 == sa_strings1.Length)
                        lis_ints.Add(Convert.ToInt16(s));
                    else
                        for (int i = Convert.ToInt16(sa_strings1[0]); i <= Convert.ToInt16(sa_strings1[1]); i++)
                            lis_ints.Add(i);

                }

                lis_ints.Sort();
                lis_ints = lis_ints.Distinct().ToList();

                foreach (int i in lis_ints)
                {
                    DataTable dt_data = FADM_Object.Communal._fadmSqlserver.GetData(
                        "SELECT * FROM bottle_details WHERE BottleNum = " + i + ";");
                    if (0 == dt_data.Rows.Count || i > Lib_Card.Configure.Parameter.Machine_Bottle_Total)
                        continue;
                    FADM_Object.Communal._fadmSqlserver.ReviseData(
                        "INSERT INTO bottle_check(BottleNum, Finish, Successed) VALUES('" + i + "',0,0);");


                }


                Thread thread = new Thread(() =>
                {

                    new FADM_Auto.BottleSelf().Self();
                });
                thread.Start();


            }
            catch (Exception ex)
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    FADM_Form.CustomMessageBox.Show(ex.Message, "BottleSelf",
                    MessageBoxButtons.OK, false);
                else
                {
                    string s_message = ex.Message;
                    if (SmartDyeing.FADM_Object.Communal._dic_warning.ContainsKey(ex.Message))
                    {
                        //如果存在就替换英文
                        s_message = SmartDyeing.FADM_Object.Communal._dic_warning[ex.Message];
                    }
                    FADM_Form.CustomMessageBox.Show(s_message, "BottleSelf",
                    MessageBoxButtons.OK, false);
                }

                FADM_Object.Communal.WriteDripWait(false);
                FADM_Object.Communal.WriteMachineStatus(8);
            }
        }

        private void tsm_SignPause_Click(object sender, EventArgs e)
        {
            //if (11 == FADM_Object.Communal.ReadMachineStatus() || 6 == FADM_Object.Communal.ReadMachineStatus())
            //{
            //    if (Lib_Card.Configure.Parameter.Other_Language == 0)
            //    {
            //        if ("暂停" == tsm_SignPause.Text)
            //        {
            //            FADM_Object.Communal._fadmSqlserver.InsertRun("Machine", "点击暂停");
            //            FADM_Object.Communal._b_pause = true;
            //        }
            //        else
            //        {
            //            FADM_Object.Communal._fadmSqlserver.InsertRun("Machine", "点击恢复");
            //            FADM_Object.Communal._b_pause = false;
            //        }
            //    }
            //    else
            //    {
            //        if ("_b_pause" == tsm_SignPause.Text)
            //        {
            //            FADM_Object.Communal._fadmSqlserver.InsertRun("Machine", "点击暂停");
            //            FADM_Object.Communal._b_pause = true;
            //        }
            //        else
            //        {
            //            FADM_Object.Communal._fadmSqlserver.InsertRun("Machine", "点击恢复");
            //            FADM_Object.Communal._b_pause = false;
            //        }
            //    }
            //}
        }

        private void tsm_SignStop_Click(object sender, EventArgs e)
        {

        }

        private void BtnBottleCheckStart_Click(object sender, EventArgs e)
        {
            FADM_Object.Communal._fadmSqlserver.InsertRun("Machine", "点击针检启动");
            if ((0 == FADM_Object.Communal.ReadMachineStatus() || 8 == FADM_Object.Communal.ReadMachineStatus()) && null == FADM_Object.Communal.ReadDyeThread())
            {
                FADM_Object.Communal.WriteDripWait(true);
                FADM_Object.Communal.WriteMachineStatus(6);

                Thread thread = new Thread(BottleCheck);
                thread.Start();

                Thread threadReset = new Thread(Reset);
                threadReset.Start();

            }
        }

        private void BtnBottleCheckPause_Click(object sender, EventArgs e)
        {
            if (6 == FADM_Object.Communal.ReadMachineStatus())
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                {
                    if ("暂 停" == BtnBottleCheckPause.Text)
                    {
                        FADM_Object.Communal._b_pause = true;
                        BtnBottleCheckPause.Text = "恢 复";
                        FADM_Object.Communal._fadmSqlserver.InsertRun("Machine", "点击针检暂停");
                    }
                    else
                    {
                        FADM_Object.Communal._b_pause = false;
                        BtnBottleCheckPause.Text = "暂 停";
                        FADM_Object.Communal._fadmSqlserver.InsertRun("Machine", "点击针检恢复");
                    }
                }
                else
                {
                    if ("Pause" == BtnBottleCheckPause.Text)
                    {
                        FADM_Object.Communal._b_pause = true;
                        BtnBottleCheckPause.Text = "Restore";
                        FADM_Object.Communal._fadmSqlserver.InsertRun("Machine", "点击针检暂停");
                    }
                    else
                    {
                        FADM_Object.Communal._b_pause = false;
                        BtnBottleCheckPause.Text = "Pause";
                        FADM_Object.Communal._fadmSqlserver.InsertRun("Machine", "点击针检恢复");
                    }
                }
            }
        }

        private void BtnBottleCheckStop_Click(object sender, EventArgs e)
        {
            FADM_Object.Communal._fadmSqlserver.InsertRun("Machine", "点击针检停止");
            if (6 == FADM_Object.Communal.ReadMachineStatus())
            {
                FADM_Object.Communal._b_stop = true;

                Thread threadReset = new Thread(Reset);
                threadReset.Start();
            }
        }

        private void BtnBottleSelfStart_Click(object sender, EventArgs e)
        {
            FADM_Object.Communal._fadmSqlserver.InsertRun("Machine", "点击自检启动");
            if ((0 == FADM_Object.Communal.ReadMachineStatus() || 8 == FADM_Object.Communal.ReadMachineStatus()) && null == FADM_Object.Communal.ReadDyeThread())
            {

                FADM_Object.Communal.WriteDripWait(true);
                FADM_Object.Communal.WriteMachineStatus(11);


                Thread thread = new Thread(BottleSelf);
                thread.Start();

                Thread threadReset = new Thread(Reset);
                threadReset.Start();
            }
        }

        private void BtnBottleSelfPause_Click(object sender, EventArgs e)
        {
            if (11 == FADM_Object.Communal.ReadMachineStatus())
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                {
                    if ("暂 停" == BtnBottleSelfPause.Text)
                    {
                        FADM_Object.Communal._fadmSqlserver.InsertRun("Machine", "点击自检暂停");
                        FADM_Object.Communal._b_pause = true;
                        BtnBottleSelfPause.Text = "恢 复";
                    }
                    else
                    {
                        FADM_Object.Communal._fadmSqlserver.InsertRun("Machine", "点击自检恢复");
                        FADM_Object.Communal._b_pause = false;
                        BtnBottleSelfPause.Text = "暂 停";
                    }
                }
                else
                {
                    if ("Pause" == BtnBottleSelfPause.Text)
                    {
                        FADM_Object.Communal._fadmSqlserver.InsertRun("Machine", "点击自检暂停");
                        FADM_Object.Communal._b_pause = true;
                        BtnBottleSelfPause.Text = "Restore";
                    }
                    else
                    {
                        FADM_Object.Communal._fadmSqlserver.InsertRun("Machine", "点击自检恢复");
                        FADM_Object.Communal._b_pause = false;
                        BtnBottleSelfPause.Text = "Pause";
                    }
                }
            }
        }

        private void BtnBottleSelfStop_Click(object sender, EventArgs e)
        {
            if (11 == FADM_Object.Communal.ReadMachineStatus())
            {
                FADM_Object.Communal._b_stop = true;

                Thread threadReset = new Thread(Reset);
                threadReset.Start();
            }
        }

        private void BtnWaterCheckStart_Click(object sender, EventArgs e)
        {
            FADM_Object.Communal._fadmSqlserver.InsertRun("Machine", "点击水校正启动");
            if ((0 == FADM_Object.Communal.ReadMachineStatus() || 8 == FADM_Object.Communal.ReadMachineStatus()) && null == FADM_Object.Communal.ReadDyeThread())
            {

                FADM_Object.Communal.WriteDripWait(true);
                FADM_Object.Communal.WriteMachineStatus(5);


                Thread thread = new Thread(CheckWater);
                thread.Start();

                Thread threadReset = new Thread(Reset);
                threadReset.Start();
            }
        }

        private void BtnRecheckStart_Click(object sender, EventArgs e)
        {
            FADM_Object.Communal._fadmSqlserver.InsertRun("Machine", "点击水验证启动");
            if ((0 == FADM_Object.Communal.ReadMachineStatus() || 8 == FADM_Object.Communal.ReadMachineStatus()) && null == FADM_Object.Communal.ReadDyeThread())
            {
                FADM_Object.Communal.WriteDripWait(true);
                FADM_Object.Communal.WriteMachineStatus(12);

                Thread thread = new Thread(RecheckWater);
                thread.Start();

                Thread threadReset = new Thread(Reset);
                threadReset.Start();
            }
        }

        private void CheckWater()
        {
            new FADM_Auto.Water().Check();
        }

        private void RecheckWater()
        {
            try
            {
                if (!string.IsNullOrEmpty(TxtWaterAddWeight.Text))
                {
                    double d_blObjectWeight = Convert.ToDouble(string.Format("{0:F3}", TxtWaterAddWeight.Text));
                    new FADM_Auto.Water().Recheck(d_blObjectWeight);
                }
                else
                {
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        throw new Exception("请输入验证水量");
                    else
                        throw new Exception("Please enter the verification water quantity");
                }

            }
            catch (Exception ex)
            {
                FADM_Object.Communal.WriteMachineStatus(8);
                FADM_Object.Communal.WriteDripWait(false);

                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    FADM_Form.CustomMessageBox.Show(ex.Message, "RecheckWater",
                    MessageBoxButtons.OK, false);
                else
                {
                    string s_message = ex.Message;
                    if (SmartDyeing.FADM_Object.Communal._dic_warning.ContainsKey(ex.Message))
                    {
                        //如果存在就替换英文
                        s_message = SmartDyeing.FADM_Object.Communal._dic_warning[ex.Message];
                    }
                    FADM_Form.CustomMessageBox.Show(s_message, "RecheckWater",
                    MessageBoxButtons.OK, false);
                }
            }
        }


        private void tsm_SignUpdate_Click(object sender, EventArgs e)
        {
            string s_sqlpre = "SELECT * FROM pre_brew WHERE  BottleNum = " + _i_nBottleNum + ";";
            DataTable dt_pre = FADM_Object.Communal._fadmSqlserver.GetData(s_sqlpre);
            if (dt_pre.Rows.Count > 0)
            {
                string s_sql = "UPDATE bottle_details SET RealConcentration = '" + dt_pre.Rows[0]["RealConcentration"].ToString() + "',CurrentWeight = '" + dt_pre.Rows[0]["CurrentWeight"].ToString() + "',BrewingData='"
                                   + dt_pre.Rows[0]["BrewingData"].ToString() + "'" +
                               " WHERE BottleNum = " + _i_nBottleNum + ";";
                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
                FADM_Object.Communal._fadmSqlserver.ReviseData("Delete from pre_brew where BottleNum = " + _i_nBottleNum);
            }
        }

        private void tsm_Stop_Click(object sender, EventArgs e)
        {
            if (Lib_Card.Configure.Parameter.Other_Language == 0)
            {
                DialogResult dialogResult = FADM_Form.CustomMessageBox.Show("确定置为暂停状态吗?", "提示", MessageBoxButtons.YesNo, true);

                if (dialogResult == DialogResult.Yes)
                {


                }
                else
                {
                    return;
                }
            }
            else
            {
                DialogResult dialogResult = FADM_Form.CustomMessageBox.Show("Are you sure to put it in a paused state?", "Tips", MessageBoxButtons.YesNo, true);

                if (dialogResult == DialogResult.Yes)
                {


                }
                else
                {
                    return;
                }
            }
            string s_sql = "UPDATE bottle_details SET Status = 0,CurrentWeight=0" +
                               " WHERE BottleNum = " + _i_nBottleNum + ";";
            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
        }

        private void tsm_Normal_Click(object sender, EventArgs e)
        {
            string s_sql = "UPDATE bottle_details SET Status = 1" +
                               " WHERE BottleNum = " + _i_nBottleNum + ";";
            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
        }

        private void tsm_Need_Click(object sender, EventArgs e)
        {
            string s_sql = "UPDATE bottle_details SET Status = 2" +
                               " WHERE BottleNum = " + _i_nBottleNum + ";";
            FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
        }

        private void tsm_PreDrip_Click(object sender, EventArgs e)
        {
            if (tsm_PreDrip.Checked)
            {
                string s_sql = "UPDATE bottle_details SET DripReserveFirst = 0" +
                               " WHERE BottleNum = " + _i_nBottleNum + ";";
                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
            }
            else
            {
                string s_sql = "UPDATE bottle_details SET DripReserveFirst = 1" +
                               " WHERE BottleNum = " + _i_nBottleNum + ";";
                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);
            }
        }

        private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
        {
            //if(FADM_Object.Communal._b_pause)
            //{
            //    if (Lib_Card.Configure.Parameter.Other_Language == 0)
            //        tsm_SignPause.Text = "恢复";
            //    else
            //        tsm_SignPause.Text = "Restore";
            //}

            if(!FADM_Object.Communal._b_isShowBottleStatus)
            {
                tsm_Need.Visible = false;
                tsm_Normal.Visible = false;
                tsm_Stop.Visible = false;
            }
            else
            {
                string s_sql = "SELECT *  FROM bottle_details" +
                                  " WHERE BottleNum = " + _i_nBottleNum + " ; ";
                DataTable dt_bottledetails = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                if(dt_bottledetails.Rows.Count>0)
                {
                    if (dt_bottledetails.Rows[0]["Status"].ToString()=="0")
                    {
                        tsm_Stop.Checked = true;
                        tsm_Need.Checked = false;
                        tsm_Normal.Checked = false;
                    }
                    else if (dt_bottledetails.Rows[0]["Status"].ToString() == "2")
                    {
                        tsm_Need.Checked = true;
                        tsm_Stop.Checked = false;
                        tsm_Normal.Checked = false;
                    }
                    else
                    {
                        tsm_Normal.Checked = true;
                        tsm_Stop.Checked = false;
                        tsm_Need.Checked = false;
                    }
                }
            }

            if(Communal._b_isAloneDripReserve)
            {
                string s_sql = "SELECT *  FROM bottle_details" +
                                  " WHERE BottleNum = " + _i_nBottleNum + " ; ";
                DataTable dt_bottledetails = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

                if (dt_bottledetails.Rows.Count > 0)
                {
                    if (dt_bottledetails.Rows[0]["DripReserveFirst"].ToString() == "1")
                    {
                        tsm_PreDrip.Checked = true;
                    }
                    else
                    {
                        tsm_PreDrip.Checked = false;
                    }
                }
            }
            else
            {
                tsm_PreDrip.Visible = false;
            }
        }

        private void Reset()
        {
            while (true)
            {
                if (0 == FADM_Object.Communal.ReadMachineStatus() || 8 == FADM_Object.Communal.ReadMachineStatus())
                {
                    FADM_Object.Communal.WriteDripWait(false);
                    break;
                }
                Thread.Sleep(1);
            }
        }

       




    }
}
