using EasyModbus;
using Lib_Card;
using SmartDyeing.FADM_Object;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace SmartDyeing.FADM_Control
{
    public partial class Debug : UserControl
    {
        public Debug()
        {
            InitializeComponent();
            //FADM_Object.Communal.WriteTcpStatus(false);
            FADM_Object.Communal._b_auto = false;

        }

        ~Debug()
        {
            //FADM_Object.Communal.WriteTcpStatus(true);
            FADM_Object.Communal._b_auto = true;
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);
            // 在此添加需要手动释放资源的代码
            //FADM_Object.Communal.WriteTcpStatus(true);
            FADM_Object.Communal._b_auto = true;
        }
   

        private int[] _ia_array = new int[1];

        private char[] _ca_cc;

        private bool _b_istrue = false;
        //异常读取次数
        int _i_count = 0;

        private void button2_Click_1(object sender, EventArgs e)
        {
            int[] ia_array = new int[11];
            int[] ia_array2 = new int[11];
            int[] ia_array3 = new int[11];
            int[] ia_array4 = new int[11];
            //int i_state = FADM_Object.Communal._tcpModBusBrew.Read(400139 , 11, ref _ia_array);
            //FADM_Object.Communal._tcpModBusBrew.Read(100139, 11, ref ia_array2);
            //FADM_Object.Communal._tcpModBusBrew.Read(40139, 11, ref ia_array3);
            int i_state = FADM_Object.Communal._tcpModBusBrew.Read(10139, 11, ref ia_array4);
            if (i_state != -1)
            {
                FADM_Object.Communal._tcpModBusBrew._b_Connect = true;
            }
            else
            {
                FADM_Object.Communal._tcpModBusBrew._b_Connect = false;

                throw new Exception("开料机通讯异常");
            }

            //try {
            //    //调用tcp
            //    int[] _ia_array = { 0, 0};
            //    int i_state = FADM_Object.Communal._tcpModBus.Read(0, 2, ref _ia_array);
            //    //int i_state = FADM_Object.Communal._tcpModBus.Write(0, _ia_array);
            //    if (i_state!=-1) {
            //        string str1 = Convert.ToString(_ia_array[0], 2);
            //        string s_str2 = Convert.ToString(_ia_array[1], 2);
            //        string str3 = str1  + s_str2;
            //        string str4 = str3.Substring(0, 2);
            //        Console.WriteLine(str1+"@@"+s_str2);
            //    }
            //} catch(Exception ex){
            //    Console.WriteLine(ex.Message);
            //}
        }

        bool _b_reconnect=false;

        private void Tmr_Tick(object sender, EventArgs e)
        {
            if (_b_reconnect)
            { return; }
            try {
                //if (!_b_istrue)
                {
                    //while (true)
                    //{
                    //    //当没进行写入时可以读取数据
                    //    if (!Communal._b_isWriting)
                    //        break;
                    //}
                    //tcp读取900到928
                    int[] ia_array = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0 ,0 ,0 ,0 ,0 ,0, 0,0,0,0};
                    int i_state = 0;
                    if (!FADM_Object.Communal.ReadTcpStatus())
                    {
                        while(true)
                        {
                            if (Communal._b_isUpdateNewData)
                                break;
                            Thread.Sleep(50);
                        }
                        ia_array = Communal._ia_d900;
                    }
                    else
                    {
                        i_state = FADM_Object.Communal._tcpModBus.Read(900, 29, ref ia_array);
                    }
                    if (i_state != -1)
                    {
                        Communal._s_plcVersion = "V" + ia_array[27].ToString("d4") + ia_array[28].ToString("d4");

                        _i_count = 0;

                        //调试页面下数据
                        //Communal._ia_d900[0]= _ia_array[0];//动作错误编号
                        //Communal._ia_d900[1] = _ia_array[1];
                        //Communal._ia_d900[2] = _ia_array[2];
                        //Communal._ia_d900[3] = _ia_array[3];
                        //Communal._ia_d900[4] = _ia_array[4];

                        Communal._b_isUpdateNewData = true;

                        int a1 = ia_array[0];//动作错误编号
                        int a2 = ia_array[1]; //电子秤数据
                        int a3 = ia_array[2]; //电子秤数据
                        int aa3 = ia_array[3]; //光幕信号
                        int a4 = ia_array[4]; //执行完成
                        int a5 = ia_array[5]; //输入点(双字)
                        int a6 = ia_array[6]; //输入点(双字)
                        int a7 = ia_array[7];//输出点(双字)
                        int a8 = ia_array[8]; //输出点(双字) 

                        int a9 = ia_array[9]; //X轴当前坐标
                        int a10 = ia_array[10];//X轴当前坐标  

                        int a11 = ia_array[11]; //Y轴当前坐标 
                        int a12 = ia_array[12]; //Y轴当前坐标 

                        int a13 = ia_array[13];//Z轴当前坐标（保留）
                        int a14 = ia_array[14];//Z轴当前坐标（保留）

                        int a15 = ia_array[15]; //X轴当前速度
                        int a16 = ia_array[16]; //X轴当前速度

                        int a17 = ia_array[17]; //Y轴当前速度
                        int a18 = ia_array[18]; //Y轴当前速度

                        int a19 = ia_array[19]; //Z轴当前速度
                        int a20 = ia_array[20]; //Z轴当前速度

                        int a23 = ia_array[23]; //
                        int a25 = ia_array[25]; //

                        double d_b = 0.0;
                        if (a2 < 0)
                        {
                            d_b = ((a3 + 1) * 65536 + a2) / 1000.0;
                        }
                        else
                        {
                            d_b = (a3 * 65536 + a2) / 1000.0;
                        }
                        //if(Lib_Card.Configure.Parameter.Machine_BalanceType == 0)
                        //{
                        //    d_b /= 10;
                        //}


                        LabBalanceValue.Text = Convert.ToString(d_b); //显示天平的数据
                        string s_str = Convert.ToString(a4, 2); //执行完成


                        char[] ca_cc = Convert.ToString(aa3, 2).PadLeft(3, '0').ToArray();
                        if (ca_cc[ca_cc.Length - 1].Equals('0'))
                        {
                            if (Lib_Card.Configure.Parameter.Machine_IsStopOrFront == 0)
                            {
                                if (!Lib_Card.CardObject.bStopScr)
                                {
                                    Lib_Card.CardObject.bStopScr = true;
                                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                        new FADM_Object.MyAlarm("急停已按下", 1);
                                    else
                                        new FADM_Object.MyAlarm("Emergency stop pressed", 1);
                                }
                            }
                            else
                            {
                                if (!Lib_Card.CardObject.bFront)
                                {
                                    Lib_Card.CardObject.bFront = true;
                                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                        new FADM_Object.MyAlarm("前光幕遮挡", 1);
                                    else
                                        new FADM_Object.MyAlarm("Front light curtain obstruction", 1);
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
                        else
                        {
                            Lib_Card.CardObject.bRight = false;
                        }
                        //急停信号
                        if (ca_cc[ca_cc.Length - 2].Equals('0'))
                        {
                            if (!Lib_Card.CardObject.bLeft)
                            {
                                Lib_Card.CardObject.bLeft = true;
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
                        }
                        else
                        {
                            Lib_Card.CardObject.bLeft = false;
                        }



                        string s_str2 = Convert.ToString(a5, 2).PadLeft(15, '0');
                        if (!s_str2.Equals("0"))
                        {
                            ca_cc = s_str2.ToArray();//急停
                            ChkInPut_Stop.Checked = ca_cc[ca_cc.Length - 1].Equals('0') ? true : false; ;//急停 _ca_cc[_ca_cc.Length-1]
                            ChkInPut_Z_Corotation.Checked = ca_cc[ca_cc.Length - 3].Equals('1') ? true : false; //Z轴反限位
                                                                                                          //_ca_cc[2]; 空 _ca_cc[_ca_cc.Length-3]
                            ChkInPut_Tray_Out.Checked = ca_cc[ca_cc.Length - 4].Equals('1') ? true : false; //接液盘出
                            ChkInPut_Tray_In.Checked = ca_cc[ca_cc.Length - 5].Equals('1') ? true : false; //接液盘回
                            ChkInPut_Syringe.Checked = ca_cc[ca_cc.Length - 6].Equals('1') ? true : false; //针筒传感器
                            ChkInPut_Tongs_A.Checked = ca_cc[ca_cc.Length - 7].Equals('1') ? true : false; //抓手A
                            ChkInPut_Tongs_B.Checked = ca_cc[ca_cc.Length - 8].Equals('1') ? true : false; //抓手B
                            ChkInPut_Cylinder_Up.Checked = ca_cc[ca_cc.Length - 9].Equals('1') ? true : false; //上限位
                            ChkInPut_Cylinder_Down.Checked = ca_cc[ca_cc.Length - 10].Equals('1') ? true : false; //下限位
                            ChkInPut_Sunx_B.Checked = ca_cc[ca_cc.Length - 11].Equals('0') ? true : false; //光幕
                            ChkInPut_Sunx_A.Checked = ca_cc[ca_cc.Length - 12].Equals('0') ? true : false; //光幕
                            ChkInPut_Cylinder_Mid.Checked = ca_cc[ca_cc.Length - 13].Equals('1') ? true : false; //气缸中限位
                            ChkInPut_Decompression_Up.Checked = ca_cc[ca_cc.Length - 14].Equals('1') ? true : false; //泄压上限位
                            ChkInPut_Decompression_Down.Checked = ca_cc[ca_cc.Length - 15].Equals('1') ? true : false; //泄压下限位
                        }
                        //其他输入点信息
                        s_str2 = Convert.ToString(a23, 2).PadLeft(4, '0');
                        ca_cc = s_str2.ToArray();//
                        ChkInPut_Block_Out.Checked = ca_cc[ca_cc.Length - 1].Equals('1') ? true : false; //阻挡出限位
                        ChkInPut_Block_In.Checked = ca_cc[ca_cc.Length - 2].Equals('1') ? true : false; //阻挡回限位
                        ChkInPut_Slow_Mid.Checked = ca_cc[ca_cc.Length - 3].Equals('1') ? true : false; //气缸慢速中限位
                        ChkInPut_Block.Checked = ca_cc[ca_cc.Length - 4].Equals('1') ? true : false; //气缸阻挡限位


                        s_str = Convert.ToString(a6, 2).PadLeft(12, '0');
                        if (!s_str.Equals("0"))
                        {
                            ca_cc = s_str.ToArray();
                            ChkInPut_X_Corotation.Checked = ca_cc[ca_cc.Length - 2].Equals('1') ? true : false;  //X轴正限位
                            ChkInPut_X_Reverse.Checked = ca_cc[ca_cc.Length - 1].Equals('1') ? true : false;  //X轴反限位
                            ChkInPut_X_Origin.Checked = ca_cc[ca_cc.Length - 3].Equals('1') ? true : false;  //X转原点位
                            ChkInPut_X_Ready.Checked = ca_cc[ca_cc.Length - 4].Equals('1') ? true : false;  //X轴准备位
                            ChkInPut_X_Alarm.Checked = ca_cc[ca_cc.Length - 5].Equals('1') ? true : false;  //X轴报警
                            this.BtnOutPut_X_Power.ForeColor = ca_cc[ca_cc.Length - 6].Equals('1') ? Color.Red : Color.Black;//X轴失能
                            ChkInPut_Y_Corotation.Checked = ca_cc[ca_cc.Length - 8].Equals('1') ? true : false;  //Y轴正限位
                            ChkInPut_Y_Reverse.Checked = ca_cc[ca_cc.Length - 7].Equals('1') ? true : false;  //Y轴反限位
                            ChkInPut_Y_Origin.Checked = ca_cc[ca_cc.Length - 9].Equals('1') ? true : false;  //Y转原点位
                            ChkInPut_Y_Ready.Checked = ca_cc[ca_cc.Length - 10].Equals('1') ? true : false;  //Y轴准备位
                            ChkInPut_Y_Alarm.Checked = ca_cc[ca_cc.Length - 11].Equals('1') ? true : false;  //Y轴报警
                            this.BtnOutPut_Y_Power.ForeColor = ca_cc[ca_cc.Length - 12].Equals('1') ? Color.Red : Color.Black;//Y轴失能
                        }

                        s_str = Convert.ToString(a7, 2).PadLeft(15, '0');
                        if (!s_str.Equals("0"))
                        {
                            ca_cc = s_str.ToArray();
                            //_ca_cc[0]; Z轴脉冲 _ca_cc.Length-1
                            //_ca_cc[1];Z轴方向 _ca_cc.Length-2
                            //_ca_cc[2];空  _ca_cc.Length-3
                            //_ca_cc[3];空 _ca_cc.Length-4
                            this.BtnOutPut_Blender.ForeColor = ca_cc[ca_cc.Length - 5].Equals('0') ? Color.Red : Color.Black; //搅拌停
                            this.BtnOutPut_Waste.ForeColor = ca_cc[ca_cc.Length - 6].Equals('1') ? Color.Red : Color.Black; //抽废液
                            this.BtnOutPut_Buzzer.ForeColor = ca_cc[ca_cc.Length - 7].Equals('1') ? Color.Red : Color.Black; //抽废液
                            this.BtnOutPut_Water.ForeColor = ca_cc[ca_cc.Length - 8].Equals('1') ? Color.Red : Color.Black; //加水
                            this.BtnOutPut_Tray.ForeColor = ca_cc[ca_cc.Length - 9].Equals('1') ? Color.Red : Color.Black; //接液盘
                            this.BtnOutPut_Tongs.ForeColor = ca_cc[ca_cc.Length - 10].Equals('1') ? Color.Red : Color.Black; //抓手
                            this.BtnOutPut_Cylinder_Down.ForeColor = ca_cc[ca_cc.Length - 11].Equals('1') ? Color.Red : Color.Black; //气缸下
                            this.BtnOutPut_Decompression.ForeColor = ca_cc[ca_cc.Length - 14].Equals('1') ? Color.Red : Color.Black; //泄压下
                            this.BtnOutPut_Block.ForeColor = ca_cc[ca_cc.Length - 15].Equals('1') ? Color.Red : Color.Black; //阻挡出

                        }

                        //s_str = Convert.ToString(a8, 2);//输出
                        //_ca_cc = s_str.ToArray();

                        if (a9 < 0)
                        {
                            d_b = ((a10 + 1) * 65536 + a9) / 10;
                        }
                        else
                        {
                            d_b = (a10 * 65536 + a9) / 10;
                        }
                        this.TxtRPosX.Text = Convert.ToString(d_b); //TxtRPosX X轴当前坐标


                        if (a11 < 0)
                        {
                            d_b = ((a12 + 1) * 65536 + a11) / 10;
                        }
                        else
                        {
                            d_b = (a12 * 65536 + a11) / 10;
                        }
                        this.TxtRPosY.Text = Convert.ToString(d_b); //Y轴当前坐标



                        if (a13 < 0)
                        {
                            d_b = (((a14 + 1) * 65536 + a13)) ;
                        }
                        else
                        {
                            d_b = ((a14 * 65536 + a13));
                        }

                        this.TxtCPosZ.Text = Convert.ToString(d_b); //Z轴当前坐标

                        if (a15 < 0)
                        {
                            d_b = ((a16 + 1) * 65536 + a15) / 1000.0;
                        }
                        else
                        {
                            d_b = (a16 * 65536 + a15) / 1000.0;
                        }
                        //d_b = Convert.ToInt16(a16<<16|a15);
                        this.TxtCSpeedX.Text = Convert.ToString(d_b); //X轴当前速度

                        if (a17 < 0)
                        {
                            d_b = ((a18 + 1) * 65536 + a17) / 1000.0;
                        }
                        else
                        {
                            d_b = (a18 * 65536 + a17) / 1000.0;
                        }
                        this.TxtCSpeedY.Text = Convert.ToString(d_b); //Y轴当前速度

                        if (a19 < 0)
                        {
                            d_b = ((a20 + 1) * 65536 + a19) / 1000.0;
                        }
                        else
                        {
                            d_b = (a20 * 65536 + a19) / 1000.0;
                        }
                        this.TxtCSpeedZ.Text = Convert.ToString(d_b); //Z轴当前速度

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


                    }
                    else {
                        //重新连接试试
                        FADM_Object.Communal._tcpModBus.ReConnect();
                        _i_count++;
                        if(_i_count>5)
                        {
                            _b_reconnect = true;

                            Thread thread = new Thread(ReConnect); //线程读取数据，确定已经重连
                            thread.IsBackground = true;
                            thread.Start();
                        }
                    }
                }
                
            }
            catch(Exception ex) { 
            
            }
        }

        private void ReConnect()
        {

            while (true)
            {
                Thread.Sleep(60);  //这里一直读 如果同时地方也同时读 就有可能造成读不到 
                FADM_Object.Communal._tcpModBus.ReConnect();
                int[] ia_array = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                int i_state = FADM_Object.Communal._tcpModBus.Read(900, 21, ref ia_array);
                if (i_state != -1)
                {
                    _i_count = 0;
                    _b_reconnect = false;
                    break;
                }

            }
        }



        private void BtnOutPut_X_Power_Click(object sender, EventArgs e)
        {
            try
            {
                _b_istrue = true;
                //X失能
                if (this.BtnOutPut_X_Power.ForeColor == Color.Red)
                { //红色 上了失能 关
                    _ia_array[0] = 2;
                }
                else
                {
                    _ia_array[0] = 1;
                }
                int i_state = FADM_Object.Communal._tcpModBus.Write(811, _ia_array);
            }
            catch (Exception ex) { }
            finally {
                _b_istrue = false;
            }


            
         }


        private void BtnOutPut_Y_Power_Click(object sender, EventArgs e)
        {
            try {
                _b_istrue = true;
                //Y失能
                if (this.BtnOutPut_Y_Power.ForeColor == Color.Red)
                { //红色 上了失能 关
                    _ia_array[0] = 4;
                }
                else
                {
                    _ia_array[0] = 3;
                }
                int i_state = FADM_Object.Communal._tcpModBus.Write(811, _ia_array);
            } catch { } finally {
                _b_istrue = false;
            }
            
        }

        private void BtnOutPut_Cylinder_Down_Click(object sender, EventArgs e)
        {
            try {
                _b_istrue = true;
                if (this.BtnOutPut_Cylinder_Down.ForeColor == Color.Red)
                {
                    _ia_array[0] = 5;
                }
                else
                {
                    _ia_array[0] = 6;
                }
                int i_state = FADM_Object.Communal._tcpModBus.Write(811, _ia_array);
                
            } catch { 
            
            } finally {
                _b_istrue = false;
            }

        }

        private void BtnOutPut_Blender_Click(object sender, EventArgs e)
        {
            try {
                _b_istrue = true;
                if (this.BtnOutPut_Blender.ForeColor == Color.Red)
                {
                    _ia_array[0] = 10;
                }
                else
                {
                    _ia_array[0] = 9;
                }
                int i_state = FADM_Object.Communal._tcpModBus.Write(811, _ia_array);
            } catch { 
            } finally {
                _b_istrue = false;
            }
            
        }

        private void BtnOutPut_Tray_Click(object sender, EventArgs e)
        {
            try {
                _b_istrue = true;
                if (this.BtnOutPut_Tray.ForeColor == Color.Red)
                {
                    _ia_array[0] = 12;
                }
                else
                {
                    _ia_array[0] = 11;
                }
                int i_state = FADM_Object.Communal._tcpModBus.Write(811, _ia_array);
                
            } catch { } finally {
                _b_istrue = false;
            }

            
        }

        private void BtnOutPut_Tongs_Click(object sender, EventArgs e)
        {
            try {
                _b_istrue = true;
                if (this.BtnOutPut_Tongs.ForeColor == Color.Red)
                {
                    _ia_array[0] = 7;
                }
                else
                {
                    _ia_array[0] = 8;
                }
                int i_state = FADM_Object.Communal._tcpModBus.Write(811, _ia_array);
                
            } catch { 
            
            } finally {
                _b_istrue = false;
            }
            
        }

        private void BtnOutPut_Block_Click(object sender, EventArgs e)
        {
            try {
                _b_istrue = true;
                if (this.BtnOutPut_Block.ForeColor == Color.Red)
                {
                    _ia_array[0] = 35;
                }
                else
                {
                    _ia_array[0] = 34;
                }
                int i_state = FADM_Object.Communal._tcpModBus.Write(811, _ia_array);
                
            } catch { } finally {
                _b_istrue = false;
            }
            
        }

        private void BtnOutPut_Decompression_Click(object sender, EventArgs e)
        {
            try {
                _b_istrue = true;
                if (this.BtnOutPut_Decompression.ForeColor == Color.Red)
                {
                    _ia_array[0] = 23;
                }
                else
                {
                    _ia_array[0] = 24;
                }
                int i_state = FADM_Object.Communal._tcpModBus.Write(811, _ia_array);
                
            } catch { } finally {
                _b_istrue = false;
            }
            
        }

        private void BtnOutPut_Apocenosis_Click(object sender, EventArgs e)
        {
            try {
                _b_istrue = true;
                if (this.BtnOutPut_Apocenosis.ForeColor == Color.Red)
                {
                    _ia_array[0] = 20;
                }
                else
                {
                    _ia_array[0] = 19;
                }
                int i_state = FADM_Object.Communal._tcpModBus.Write(811, _ia_array);
            } catch { } finally {
                _b_istrue = false;
            }
            
        }

        private void BtnOutPut_Buzzer_Click(object sender, EventArgs e)
        {
            try {
                _b_istrue = true;
                if (this.BtnOutPut_Buzzer.ForeColor == Color.Red)
                {
                    _ia_array[0] = 18;
                }
                else
                {
                    _ia_array[0] = 17;
                }
                int i_state = FADM_Object.Communal._tcpModBus.Write(811, _ia_array);
            } catch { } finally {
                _b_istrue = false;
            }
            
        }

        private void BtnOutPut_Water_Click(object sender, EventArgs e)
        {
            try {
                _b_istrue = true;
                if (this.BtnOutPut_Water.ForeColor == Color.Red)
                {
                    _ia_array[0] = 16;
                }
                else
                {
                    _ia_array[0] = 15;
                }
                int i_state = FADM_Object.Communal._tcpModBus.Write(811, _ia_array);
                
            } catch { } finally {
                _b_istrue = false;
            }
            

        }

        private void BtnOutPut_Waste_Click(object sender, EventArgs e)
        {
            try {
                _b_istrue = true;
                if (this.BtnOutPut_Waste.ForeColor == Color.Red)
                {
                    _ia_array[0] = 14;
                }
                else
                {
                    _ia_array[0] = 13;
                }
                int i_state = FADM_Object.Communal._tcpModBus.Write(811, _ia_array);
            } catch { } finally {
                _b_istrue = false;
            }
            
        }

        private void BtnStartMove_Click(object sender, EventArgs e)
        {
            FADM_Object.Communal._fadmSqlserver.InsertRun("Machine", "定点移动启动");
            if ((0 == FADM_Object.Communal.ReadMachineStatus() || 8 == FADM_Object.Communal.ReadMachineStatus()) && null == FADM_Object.Communal.ReadDyeThread() && FADM_Object.Communal.ReadTcpStatus())
            {
                FADM_Object.Communal.WriteDripWait(true);
                if (RdoBottle.Checked)
                {
                    if (string.IsNullOrEmpty(TxtNum.Text))
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            FADM_Form.CustomMessageBox.Show("请输入瓶号！", "温馨提示", MessageBoxButtons.OK, false);
                        else
                            FADM_Form.CustomMessageBox.Show("Please enter the bottle number！", "Tips", MessageBoxButtons.OK, false);
                        FADM_Object.Communal.WriteDripWait(false);
                        return;
                    }
                    FADM_Object.Communal.WriteMachineStatus(4);
                    Thread thread = new Thread(BottleMove);
                    thread.Start();

                }
                else if (RdoCup.Checked)
                {
                    if (string.IsNullOrEmpty(TxtNum.Text))
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            FADM_Form.CustomMessageBox.Show("请输入杯号！", "温馨提示", MessageBoxButtons.OK, false);
                        else
                            FADM_Form.CustomMessageBox.Show("Please enter the cup number！", "Tips", MessageBoxButtons.OK, false);
                        FADM_Object.Communal.WriteDripWait(false);
                        return;
                    }
                    FADM_Object.Communal.WriteMachineStatus(4);
                    Thread thread = new Thread(CupMove);
                    thread.Start();
                }
                else if (RdoStress.Checked)
                {
                    if (string.IsNullOrEmpty(TxtNum.Text))
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            FADM_Form.CustomMessageBox.Show("请输入杯号！", "温馨提示", MessageBoxButtons.OK, false);
                        else
                            FADM_Form.CustomMessageBox.Show("Please enter the cup number！", "Tips", MessageBoxButtons.OK, false);
                        FADM_Object.Communal.WriteDripWait(false);
                        return;
                    }
                    FADM_Object.Communal.WriteMachineStatus(4);
                    Thread thread = new Thread(CupStress);
                    thread.Start();
                }
                else if (RdoBalance.Checked)
                {
                    FADM_Object.Communal.WriteMachineStatus(4);
                    Thread thread = new Thread(BalanceMove);
                    thread.Start();
                }
                else if (RdoDecompression.Checked)
                {
                    if (string.IsNullOrEmpty(TxtNum.Text))
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            FADM_Form.CustomMessageBox.Show("请输入杯号！", "温馨提示", MessageBoxButtons.OK, false);
                        else
                            FADM_Form.CustomMessageBox.Show("Please enter the cup number！", "Tips", MessageBoxButtons.OK, false);
                        FADM_Object.Communal.WriteDripWait(false);
                        return;
                    }
                    FADM_Object.Communal.WriteMachineStatus(4);
                    Thread thread = new Thread(DecompressionMove);
                    thread.Start();
                }
                else if (RdoDryCloth.Checked)
                {
                    if (string.IsNullOrEmpty(TxtNum.Text))
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            FADM_Form.CustomMessageBox.Show("请输入杯号！", "温馨提示", MessageBoxButtons.OK, false);
                        else
                            FADM_Form.CustomMessageBox.Show("Please enter the cup number！", "Tips", MessageBoxButtons.OK, false);
                        FADM_Object.Communal.WriteDripWait(false);
                        return;
                    }
                    FADM_Object.Communal.WriteMachineStatus(4);
                    Thread thread = new Thread(DryClothMove);
                    thread.Start();
                }
                else if (RdoWetCloth.Checked)
                {
                    if (string.IsNullOrEmpty(TxtNum.Text))
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            FADM_Form.CustomMessageBox.Show("请输入杯号！", "温馨提示", MessageBoxButtons.OK, false);
                        else
                            FADM_Form.CustomMessageBox.Show("Please enter the cup number！", "Tips", MessageBoxButtons.OK, false);
                        FADM_Object.Communal.WriteDripWait(false);
                        return;
                    }
                    FADM_Object.Communal.WriteMachineStatus(4);
                    Thread thread = new Thread(WetClothMove);
                    thread.Start();
                }
                else if (RdoDryClamp.Checked)
                {
                    FADM_Object.Communal.WriteMachineStatus(4);
                    Thread thread = new Thread(DryClampMove);
                    thread.Start();
                }
                else if (RdoWetClamp.Checked)
                {
                    FADM_Object.Communal.WriteMachineStatus(4);
                    Thread thread = new Thread(WetClampMove);
                    thread.Start();
                }
                else
                {
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        FADM_Form.CustomMessageBox.Show("请选择目标区域！", "温馨提示", MessageBoxButtons.OK, false);
                    else
                        FADM_Form.CustomMessageBox.Show("Please select the target area！", "Tips", MessageBoxButtons.OK, false);
                    FADM_Object.Communal.WriteDripWait(false);
                    return;

                }
                //while (true)
                //{
                //    if (0 == FADM_Object.Communal.ReadMachineStatus() || 8 == FADM_Object.Communal.ReadMachineStatus())
                //    {
                //        FADM_Object.Communal.WriteDripWait(false);
                //        break;
                //    }
                //    Thread.Sleep(1);
                //}

                Thread threadReset = new Thread(Reset);
                threadReset.Start();
            }
            else
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    FADM_Form.CustomMessageBox.Show("机台正在运动，不能开始", "定点移动", MessageBoxButtons.OK, true);
                else
                    FADM_Form.CustomMessageBox.Show("The machine is in motion and cannot start", "Tips", MessageBoxButtons.OK, true);
            }
        }

        private void GetError()
        {
            try
            {
                //FADM_Auto.Reset.IOReset();//这里会检查是否有针筒

                if (0 != MyModbusFun.Do())
                    throw new Exception("驱动异常");
                else
                    FADM_Object.Communal.WriteMachineStatus(0);
            }
            catch (Exception ex)
            {
                _b_istrue = false;
                FADM_Object.Communal.WriteMachineStatus(8);

                if (ex.Message.Equals("-2"))
                {
                    //string[] strArray = { "", "" };
                    ////根据编号读取异常信息
                    //MyModbusFun.GetErrMsg(ref strArray);
                    //if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    //    FADM_Form.CustomMessageBox.Show(strArray[1], "原点", MessageBoxButtons.OK, true);
                    //else
                    //    FADM_Form.CustomMessageBox.Show(strArray[1], "origin", MessageBoxButtons.OK, true);

                    int[] ia_errArray = new int[100];
                    MyModbusFun.GetErrMsgNew(ref ia_errArray);

                    List<string> lis_err = new List<string>();
                    for (int i = 0; i < ia_errArray.Length; i++)
                    {
                        if (ia_errArray[i] != 0)
                        {
                            if (SmartDyeing.FADM_Object.Communal._dic_errModbusNoNew.ContainsKey(ia_errArray[i]))
                            {
                                string s_err = SmartDyeing.FADM_Object.Communal._dic_errModbusNoNew[ia_errArray[i]];
                                string s_sql = "INSERT INTO alarm_table" +
                                 "(MyDate,MyTime,AlarmHead,AlarmDetails)" +
                                 " VALUES( '" +
                                 String.Format("{0:d}", DateTime.Now) + "','" +
                                 String.Format("{0:T}", DateTime.Now) + "','" +
                                 "Debug" + "','" +
                                 s_err + "(Test)');";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                                string s = CardObject.InsertD(s_err, " Home");
                                if (!lis_err.Contains(s))
                                    lis_err.Add(s);
                                //while (true)
                                //{
                                //    Thread.Sleep(1);
                                //    if (Lib_Card.CardObject.keyValuePairs[s_insert].Choose != 0)
                                //        break;

                                //}

                                //int _i_alarm_Choose = Lib_Card.CardObject.keyValuePairs[s_insert].Choose;
                                //CardObject.DeleteD(s_insert);

                            }

                        }
                    }

                    while (true)
                    {
                        for (int p = lis_err.Count - 1; p >= 0; p--)
                        {
                            if (Lib_Card.CardObject.keyValuePairs[lis_err[p]].Choose != 0)
                            {
                                CardObject.DeleteD(lis_err[p]);
                                lis_err.Remove(lis_err[p]);
                            }
                        }
                        if (lis_err.Count == 0)
                        {
                            break;
                        }
                        Thread.Sleep(1);
                    }

                }
                else
                {
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        FADM_Form.CustomMessageBox.Show(ex.Message, "回零", MessageBoxButtons.OK, true);
                    else
                        FADM_Form.CustomMessageBox.Show(ex.Message, "zeroing", MessageBoxButtons.OK, true);
                }


            }
            finally
            {
                _b_istrue = false;
            }

            //Thread threadReset = new Thread(GetError);
            //threadReset.Start();
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

        private void DecompressionMove()
        {
            try
            {
                if (0 >= Convert.ToInt32(TxtNum.Text) || Convert.ToInt32(TxtNum.Text) > Lib_Card.Configure.Parameter.Machine_Cup_Total)
                {
                    MessageBox.Show("杯号输入错误");
                    FADM_Object.Communal.WriteDripWait(false);
                    return;
                }
                if (0 != MyModbusFun.TargetMove(4, Convert.ToInt32(TxtNum.Text), 0))
                    throw new Exception("驱动异常");
                FADM_Object.Communal.WriteMachineStatus(0);
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("-2"))
                {
                    FADM_Object.Communal.WriteMachineStatus(8);
                    int[] ia_errArray = new int[100];
                    MyModbusFun.GetErrMsgNew(ref ia_errArray);
                    List<string> lis_err = new List<string>();
                    for (int i = 0; i < ia_errArray.Length; i++)
                    {
                        if (ia_errArray[i] != 0)
                        {
                            if (SmartDyeing.FADM_Object.Communal._dic_errModbusNoNew.ContainsKey(ia_errArray[i]))
                            {
                                string s_err = SmartDyeing.FADM_Object.Communal._dic_errModbusNoNew[ia_errArray[i]];
                                string s_sql = "INSERT INTO alarm_table" +
                                 "(MyDate,MyTime,AlarmHead,AlarmDetails)" +
                                 " VALUES( '" +
                                 String.Format("{0:d}", DateTime.Now) + "','" +
                                 String.Format("{0:T}", DateTime.Now) + "','" +
                                 "Debug" + "','" +
                                 s_err + "(Test)');";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                                string s_insert = CardObject.InsertD(s_err, " myMachineReset");
                                if (!lis_err.Contains(s_insert))
                                    lis_err.Add(s_insert);
                                //while (true)
                                //{
                                //    Thread.Sleep(1);
                                //    if (Lib_Card.CardObject.keyValuePairs[s_insert].Choose != 0)
                                //        break;

                                //}

                                //int _i_alarm_Choose = Lib_Card.CardObject.keyValuePairs[s_insert].Choose;
                                //CardObject.DeleteD(s_insert);

                            }

                        }
                    }

                    while (true)
                    {
                        for (int p = lis_err.Count - 1; p >= 0; p--)
                        {
                            if (Lib_Card.CardObject.keyValuePairs[lis_err[p]].Choose != 0)
                            {
                                CardObject.DeleteD(lis_err[p]);
                                lis_err.Remove(lis_err[p]);
                            }
                        }
                        if (lis_err.Count == 0)
                        {
                            break;
                        }
                        Thread.Sleep(1);
                    }

                }
                else
                {
                    FADM_Object.Communal.WriteMachineStatus(8);
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        FADM_Form.CustomMessageBox.Show(ex.Message, "定点移动", MessageBoxButtons.OK, true);
                    else
                        FADM_Form.CustomMessageBox.Show(ex.Message, "Fixed-point movement", MessageBoxButtons.OK, true);
                }
            }
        }

        private void BottleMove()
        {
            try
            {

                if (0 >= Convert.ToInt32(TxtNum.Text) || Convert.ToInt32(TxtNum.Text) > Lib_Card.Configure.Parameter.Machine_Bottle_Total)
                {
                    MessageBox.Show("瓶号输入错误");
                    FADM_Object.Communal.WriteDripWait(false);
                    return;
                }

                if (0 != MyModbusFun.TargetMove(0, Convert.ToInt32(TxtNum.Text), 0))
                    throw new Exception("驱动异常");
                FADM_Object.Communal.WriteMachineStatus(0);
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("-2"))
                {
                    FADM_Object.Communal.WriteMachineStatus(8);
                    int[] ia_errArray = new int[100];
                    MyModbusFun.GetErrMsgNew(ref ia_errArray);
                    List<string> lis_err = new List<string>();
                    for (int i = 0; i < ia_errArray.Length; i++)
                    {
                        if (ia_errArray[i] != 0)
                        {
                            if (SmartDyeing.FADM_Object.Communal._dic_errModbusNoNew.ContainsKey(ia_errArray[i]))
                            {
                                string s_err = SmartDyeing.FADM_Object.Communal._dic_errModbusNoNew[ia_errArray[i]];
                                string s_sql = "INSERT INTO alarm_table" +
                                 "(MyDate,MyTime,AlarmHead,AlarmDetails)" +
                                 " VALUES( '" +
                                 String.Format("{0:d}", DateTime.Now) + "','" +
                                 String.Format("{0:T}", DateTime.Now) + "','" +
                                 "Debug" + "','" +
                                 s_err + "(Test)');";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                                string s_insert = CardObject.InsertD(s_err, " myMachineReset");
                                if (!lis_err.Contains(s_insert))
                                    lis_err.Add(s_insert);
                                //while (true)
                                //{
                                //    Thread.Sleep(1);
                                //    if (Lib_Card.CardObject.keyValuePairs[s_insert].Choose != 0)
                                //        break;

                                //}

                                //int _i_alarm_Choose = Lib_Card.CardObject.keyValuePairs[s_insert].Choose;
                                //CardObject.DeleteD(s_insert);

                            }

                        }
                    }

                    while (true)
                    {
                        for (int p = lis_err.Count - 1; p >= 0; p--)
                        {
                            if (Lib_Card.CardObject.keyValuePairs[lis_err[p]].Choose != 0)
                            {
                                CardObject.DeleteD(lis_err[p]);
                                lis_err.Remove(lis_err[p]);
                            }
                        }
                        if (lis_err.Count == 0)
                        {
                            break;
                        }
                        Thread.Sleep(1);
                    }

                }
                else
                {
                    FADM_Object.Communal.WriteMachineStatus(8);
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        FADM_Form.CustomMessageBox.Show(ex.Message, "定点移动", MessageBoxButtons.OK, true);
                    else
                        FADM_Form.CustomMessageBox.Show(ex.Message, "Fixed-point movement", MessageBoxButtons.OK, true);
                }
            }
        }

        private void CupMove()
        {
            try
            {
                if (0 >= Convert.ToInt32(TxtNum.Text) || Convert.ToInt32(TxtNum.Text) > Lib_Card.Configure.Parameter.Machine_Cup_Total)
                {
                    MessageBox.Show("杯号输入错误");
                    FADM_Object.Communal.WriteDripWait(false);
                    return;
                }

                if (0 != MyModbusFun.TargetMove(1, Convert.ToInt32(TxtNum.Text), 0))
                    throw new Exception("驱动异常");
                FADM_Object.Communal.WriteMachineStatus(0);
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("-2"))
                {
                    FADM_Object.Communal.WriteMachineStatus(8);
                    int[] ia_errArray = new int[100];
                    MyModbusFun.GetErrMsgNew(ref ia_errArray);
                    List<string> lis_err = new List<string>();
                    for (int i = 0; i < ia_errArray.Length; i++)
                    {
                        if (ia_errArray[i] != 0)
                        {
                            if (SmartDyeing.FADM_Object.Communal._dic_errModbusNoNew.ContainsKey(ia_errArray[i]))
                            {
                                string s_err = SmartDyeing.FADM_Object.Communal._dic_errModbusNoNew[ia_errArray[i]];
                                string s_sql = "INSERT INTO alarm_table" +
                                 "(MyDate,MyTime,AlarmHead,AlarmDetails)" +
                                 " VALUES( '" +
                                 String.Format("{0:d}", DateTime.Now) + "','" +
                                 String.Format("{0:T}", DateTime.Now) + "','" +
                                 "Debug" + "','" +
                                 s_err + "(Test)');";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                                string s_insert = CardObject.InsertD(s_err, " myMachineReset");
                                if (!lis_err.Contains(s_insert))
                                    lis_err.Add(s_insert);
                                //while (true)
                                //{
                                //    Thread.Sleep(1);
                                //    if (Lib_Card.CardObject.keyValuePairs[s_insert].Choose != 0)
                                //        break;

                                //}

                                //int _i_alarm_Choose = Lib_Card.CardObject.keyValuePairs[s_insert].Choose;
                                //CardObject.DeleteD(s_insert);

                            }

                        }
                    }

                    while (true)
                    {
                        for (int p = lis_err.Count - 1; p >= 0; p--)
                        {
                            if (Lib_Card.CardObject.keyValuePairs[lis_err[p]].Choose != 0)
                            {
                                CardObject.DeleteD(lis_err[p]);
                                lis_err.Remove(lis_err[p]);
                            }
                        }
                        if (lis_err.Count == 0)
                        {
                            break;
                        }
                        Thread.Sleep(1);
                    }

                }
                else
                {
                    FADM_Object.Communal.WriteMachineStatus(8);
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        FADM_Form.CustomMessageBox.Show(ex.Message, "定点移动", MessageBoxButtons.OK, true);
                    else
                        FADM_Form.CustomMessageBox.Show(ex.Message, "Fixed-point movement", MessageBoxButtons.OK, true);
                }
            }
        }

        private void DryClothMove()
        {
            try
            {
                if (0 >= Convert.ToInt32(TxtNum.Text) || Convert.ToInt32(TxtNum.Text) > Lib_Card.Configure.Parameter.Machine_Cup_Total)
                {
                    MessageBox.Show("杯号输入错误");
                    FADM_Object.Communal.WriteDripWait(false);
                    return;
                }

                if (0 != MyModbusFun.TargetMove(6, Convert.ToInt32(TxtNum.Text), 0))
                    throw new Exception("驱动异常");
                FADM_Object.Communal.WriteMachineStatus(0);
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("-2"))
                {
                    FADM_Object.Communal.WriteMachineStatus(8);
                    int[] ia_errArray = new int[100];
                    MyModbusFun.GetErrMsgNew(ref ia_errArray);
                    List<string> lis_err = new List<string>();
                    for (int i = 0; i < ia_errArray.Length; i++)
                    {
                        if (ia_errArray[i] != 0)
                        {
                            if (SmartDyeing.FADM_Object.Communal._dic_errModbusNoNew.ContainsKey(ia_errArray[i]))
                            {
                                string s_err = SmartDyeing.FADM_Object.Communal._dic_errModbusNoNew[ia_errArray[i]];
                                string s_sql = "INSERT INTO alarm_table" +
                                 "(MyDate,MyTime,AlarmHead,AlarmDetails)" +
                                 " VALUES( '" +
                                 String.Format("{0:d}", DateTime.Now) + "','" +
                                 String.Format("{0:T}", DateTime.Now) + "','" +
                                 "Debug" + "','" +
                                 s_err + "(Test)');";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                                string s_insert = CardObject.InsertD(s_err, " myMachineReset");
                                if (!lis_err.Contains(s_insert))
                                    lis_err.Add(s_insert);
                                //while (true)
                                //{
                                //    Thread.Sleep(1);
                                //    if (Lib_Card.CardObject.keyValuePairs[s_insert].Choose != 0)
                                //        break;

                                //}

                                //int _i_alarm_Choose = Lib_Card.CardObject.keyValuePairs[s_insert].Choose;
                                //CardObject.DeleteD(s_insert);

                            }

                        }
                    }

                    while (true)
                    {
                        for (int p = lis_err.Count - 1; p >= 0; p--)
                        {
                            if (Lib_Card.CardObject.keyValuePairs[lis_err[p]].Choose != 0)
                            {
                                CardObject.DeleteD(lis_err[p]);
                                lis_err.Remove(lis_err[p]);
                            }
                        }
                        if (lis_err.Count == 0)
                        {
                            break;
                        }
                        Thread.Sleep(1);
                    }

                }
                else
                {
                    FADM_Object.Communal.WriteMachineStatus(8);
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        FADM_Form.CustomMessageBox.Show(ex.Message, "定点移动", MessageBoxButtons.OK, true);
                    else
                        FADM_Form.CustomMessageBox.Show(ex.Message, "Fixed-point movement", MessageBoxButtons.OK, true);
                }
            }
        }

        private void WetClothMove()
        {
            try
            {
                if (0 >= Convert.ToInt32(TxtNum.Text) || Convert.ToInt32(TxtNum.Text) > Lib_Card.Configure.Parameter.Machine_Cup_Total)
                {
                    MessageBox.Show("杯号输入错误");
                    FADM_Object.Communal.WriteDripWait(false);
                    return;
                }

                if (0 != MyModbusFun.TargetMove(7, Convert.ToInt32(TxtNum.Text), 0))
                    throw new Exception("驱动异常");
                FADM_Object.Communal.WriteMachineStatus(0);
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("-2"))
                {
                    FADM_Object.Communal.WriteMachineStatus(8);
                    int[] ia_errArray = new int[100];
                    MyModbusFun.GetErrMsgNew(ref ia_errArray);
                    List<string> lis_err = new List<string>();
                    for (int i = 0; i < ia_errArray.Length; i++)
                    {
                        if (ia_errArray[i] != 0)
                        {
                            if (SmartDyeing.FADM_Object.Communal._dic_errModbusNoNew.ContainsKey(ia_errArray[i]))
                            {
                                string s_err = SmartDyeing.FADM_Object.Communal._dic_errModbusNoNew[ia_errArray[i]];
                                string s_sql = "INSERT INTO alarm_table" +
                                 "(MyDate,MyTime,AlarmHead,AlarmDetails)" +
                                 " VALUES( '" +
                                 String.Format("{0:d}", DateTime.Now) + "','" +
                                 String.Format("{0:T}", DateTime.Now) + "','" +
                                 "Debug" + "','" +
                                 s_err + "(Test)');";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                                string s_insert = CardObject.InsertD(s_err, " myMachineReset");
                                if (!lis_err.Contains(s_insert))
                                    lis_err.Add(s_insert);
                                //while (true)
                                //{
                                //    Thread.Sleep(1);
                                //    if (Lib_Card.CardObject.keyValuePairs[s_insert].Choose != 0)
                                //        break;

                                //}

                                //int _i_alarm_Choose = Lib_Card.CardObject.keyValuePairs[s_insert].Choose;
                                //CardObject.DeleteD(s_insert);

                            }

                        }
                    }

                    while (true)
                    {
                        for (int p = lis_err.Count - 1; p >= 0; p--)
                        {
                            if (Lib_Card.CardObject.keyValuePairs[lis_err[p]].Choose != 0)
                            {
                                CardObject.DeleteD(lis_err[p]);
                                lis_err.Remove(lis_err[p]);
                            }
                        }
                        if (lis_err.Count == 0)
                        {
                            break;
                        }
                        Thread.Sleep(1);
                    }

                }
                else
                {
                    FADM_Object.Communal.WriteMachineStatus(8);
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        FADM_Form.CustomMessageBox.Show(ex.Message, "定点移动", MessageBoxButtons.OK, true);
                    else
                        FADM_Form.CustomMessageBox.Show(ex.Message, "Fixed-point movement", MessageBoxButtons.OK, true);
                }
            }
        }

        private void CupStress()
        {
            try
            {
                if (0 >= Convert.ToInt32(TxtNum.Text) || Convert.ToInt32(TxtNum.Text) > Lib_Card.Configure.Parameter.Machine_Cup_Total)
                {
                    MessageBox.Show("杯号输入错误");
                    FADM_Object.Communal.WriteDripWait(false);
                    return;
                }
                if (0 != MyModbusFun.TargetMove(5, Convert.ToInt32(TxtNum.Text), 0))
                    throw new Exception("驱动异常");
                FADM_Object.Communal.WriteMachineStatus(0);
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("-2"))
                {
                    FADM_Object.Communal.WriteMachineStatus(8);
                    int[] ia_errArray = new int[100];
                    MyModbusFun.GetErrMsgNew(ref ia_errArray);
                    List<string> lis_err = new List<string>();
                    for (int i = 0; i < ia_errArray.Length; i++)
                    {
                        if (ia_errArray[i] != 0)
                        {
                            if (SmartDyeing.FADM_Object.Communal._dic_errModbusNoNew.ContainsKey(ia_errArray[i]))
                            {
                                string s_err = SmartDyeing.FADM_Object.Communal._dic_errModbusNoNew[ia_errArray[i]];
                                string s_sql = "INSERT INTO alarm_table" +
                                 "(MyDate,MyTime,AlarmHead,AlarmDetails)" +
                                 " VALUES( '" +
                                 String.Format("{0:d}", DateTime.Now) + "','" +
                                 String.Format("{0:T}", DateTime.Now) + "','" +
                                 "Debug" + "','" +
                                 s_err + "(Test)');";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                                string s_insert = CardObject.InsertD(s_err, " myMachineReset");
                                if (!lis_err.Contains(s_insert))
                                    lis_err.Add(s_insert);
                                //while (true)
                                //{
                                //    Thread.Sleep(1);
                                //    if (Lib_Card.CardObject.keyValuePairs[s_insert].Choose != 0)
                                //        break;

                                //}

                                //int _i_alarm_Choose = Lib_Card.CardObject.keyValuePairs[s_insert].Choose;
                                //CardObject.DeleteD(s_insert);

                            }

                        }
                    }

                    while (true)
                    {
                        for (int p = lis_err.Count - 1; p >= 0; p--)
                        {
                            if (Lib_Card.CardObject.keyValuePairs[lis_err[p]].Choose != 0)
                            {
                                CardObject.DeleteD(lis_err[p]);
                                lis_err.Remove(lis_err[p]);
                            }
                        }
                        if (lis_err.Count == 0)
                        {
                            break;
                        }
                        Thread.Sleep(1);
                    }

                }
                else
                {
                    FADM_Object.Communal.WriteMachineStatus(8);
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        FADM_Form.CustomMessageBox.Show(ex.Message, "定点移动", MessageBoxButtons.OK, true);
                    else
                        FADM_Form.CustomMessageBox.Show(ex.Message, "Fixed-point movement", MessageBoxButtons.OK, true);
                }
            }
        }

        private void BalanceMove()
        {
            try
            {
                if (0 != MyModbusFun.TargetMove(2, 0, 0))
                    throw new Exception("驱动异常");
                FADM_Object.Communal.WriteMachineStatus(0);
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("-2"))
                {
                    FADM_Object.Communal.WriteMachineStatus(8);
                    int[] ia_errArray = new int[100];
                    MyModbusFun.GetErrMsgNew(ref ia_errArray);
                    List<string> lis_err = new List<string>();
                    for (int i = 0; i < ia_errArray.Length; i++)
                    {
                        if (ia_errArray[i] != 0)
                        {
                            if (SmartDyeing.FADM_Object.Communal._dic_errModbusNoNew.ContainsKey(ia_errArray[i]))
                            {
                                string s_err = SmartDyeing.FADM_Object.Communal._dic_errModbusNoNew[ia_errArray[i]];
                                string s_sql = "INSERT INTO alarm_table" +
                                 "(MyDate,MyTime,AlarmHead,AlarmDetails)" +
                                 " VALUES( '" +
                                 String.Format("{0:d}", DateTime.Now) + "','" +
                                 String.Format("{0:T}", DateTime.Now) + "','" +
                                 "Debug" + "','" +
                                 s_err + "(Test)');";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                                string s_insert = CardObject.InsertD(s_err, " myMachineReset");
                                if (!lis_err.Contains(s_insert))
                                    lis_err.Add(s_insert);
                                //while (true)
                                //{
                                //    Thread.Sleep(1);
                                //    if (Lib_Card.CardObject.keyValuePairs[s_insert].Choose != 0)
                                //        break;

                                //}

                                //int _i_alarm_Choose = Lib_Card.CardObject.keyValuePairs[s_insert].Choose;
                                //CardObject.DeleteD(s_insert);

                            }

                        }
                    }

                    while (true)
                    {
                        for (int p = lis_err.Count - 1; p >= 0; p--)
                        {
                            if (Lib_Card.CardObject.keyValuePairs[lis_err[p]].Choose != 0)
                            {
                                CardObject.DeleteD(lis_err[p]);
                                lis_err.Remove(lis_err[p]);
                            }
                        }
                        if (lis_err.Count == 0)
                        {
                            break;
                        }
                        Thread.Sleep(1);
                    }

                }
                else
                {
                    FADM_Object.Communal.WriteMachineStatus(8);
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        FADM_Form.CustomMessageBox.Show(ex.Message, "定点移动", MessageBoxButtons.OK, true);
                    else
                        FADM_Form.CustomMessageBox.Show(ex.Message, "Fixed-point movement", MessageBoxButtons.OK, true);
                }
            }
        }

        private void DryClampMove()
        {
            try
            {
                if (0 != MyModbusFun.TargetMove(8, 0, 0))
                    throw new Exception("驱动异常");
                FADM_Object.Communal.WriteMachineStatus(0);
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("-2"))
                {
                    FADM_Object.Communal.WriteMachineStatus(8);
                    int[] ia_errArray = new int[100];
                    MyModbusFun.GetErrMsgNew(ref ia_errArray);
                    List<string> lis_err = new List<string>();
                    for (int i = 0; i < ia_errArray.Length; i++)
                    {
                        if (ia_errArray[i] != 0)
                        {
                            if (SmartDyeing.FADM_Object.Communal._dic_errModbusNoNew.ContainsKey(ia_errArray[i]))
                            {
                                string s_err = SmartDyeing.FADM_Object.Communal._dic_errModbusNoNew[ia_errArray[i]];
                                string s_sql = "INSERT INTO alarm_table" +
                                 "(MyDate,MyTime,AlarmHead,AlarmDetails)" +
                                 " VALUES( '" +
                                 String.Format("{0:d}", DateTime.Now) + "','" +
                                 String.Format("{0:T}", DateTime.Now) + "','" +
                                 "Debug" + "','" +
                                 s_err + "(Test)');";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                                string s_insert = CardObject.InsertD(s_err, " myMachineReset");
                                if (!lis_err.Contains(s_insert))
                                    lis_err.Add(s_insert);
                                //while (true)
                                //{
                                //    Thread.Sleep(1);
                                //    if (Lib_Card.CardObject.keyValuePairs[s_insert].Choose != 0)
                                //        break;

                                //}

                                //int _i_alarm_Choose = Lib_Card.CardObject.keyValuePairs[s_insert].Choose;
                                //CardObject.DeleteD(s_insert);

                            }

                        }
                    }

                    while (true)
                    {
                        for (int p = lis_err.Count - 1; p >= 0; p--)
                        {
                            if (Lib_Card.CardObject.keyValuePairs[lis_err[p]].Choose != 0)
                            {
                                CardObject.DeleteD(lis_err[p]);
                                lis_err.Remove(lis_err[p]);
                            }
                        }
                        if (lis_err.Count == 0)
                        {
                            break;
                        }
                        Thread.Sleep(1);
                    }

                }
                else
                {
                    FADM_Object.Communal.WriteMachineStatus(8);
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        FADM_Form.CustomMessageBox.Show(ex.Message, "定点移动", MessageBoxButtons.OK, true);
                    else
                        FADM_Form.CustomMessageBox.Show(ex.Message, "Fixed-point movement", MessageBoxButtons.OK, true);
                }
            }
        }

        private void WetClampMove()
        {
            try
            {
                if (0 != MyModbusFun.TargetMove(9, 0, 0))
                    throw new Exception("驱动异常");
                FADM_Object.Communal.WriteMachineStatus(0);
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("-2"))
                {
                    FADM_Object.Communal.WriteMachineStatus(8);
                    int[] ia_errArray = new int[100];
                    MyModbusFun.GetErrMsgNew(ref ia_errArray);
                    List<string> lis_err = new List<string>();
                    for (int i = 0; i < ia_errArray.Length; i++)
                    {
                        if (ia_errArray[i] != 0)
                        {
                            if (SmartDyeing.FADM_Object.Communal._dic_errModbusNoNew.ContainsKey(ia_errArray[i]))
                            {
                                string s_err = SmartDyeing.FADM_Object.Communal._dic_errModbusNoNew[ia_errArray[i]];
                                string s_sql = "INSERT INTO alarm_table" +
                                 "(MyDate,MyTime,AlarmHead,AlarmDetails)" +
                                 " VALUES( '" +
                                 String.Format("{0:d}", DateTime.Now) + "','" +
                                 String.Format("{0:T}", DateTime.Now) + "','" +
                                 "Debug" + "','" +
                                 s_err + "(Test)');";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                                string s_insert = CardObject.InsertD(s_err, " myMachineReset");
                                if (!lis_err.Contains(s_insert))
                                    lis_err.Add(s_insert);
                                //while (true)
                                //{
                                //    Thread.Sleep(1);
                                //    if (Lib_Card.CardObject.keyValuePairs[s_insert].Choose != 0)
                                //        break;

                                //}

                                //int _i_alarm_Choose = Lib_Card.CardObject.keyValuePairs[s_insert].Choose;
                                //CardObject.DeleteD(s_insert);

                            }

                        }
                    }

                    while (true)
                    {
                        for (int p = lis_err.Count - 1; p >= 0; p--)
                        {
                            if (Lib_Card.CardObject.keyValuePairs[lis_err[p]].Choose != 0)
                            {
                                CardObject.DeleteD(lis_err[p]);
                                lis_err.Remove(lis_err[p]);
                            }
                        }
                        if (lis_err.Count == 0)
                        {
                            break;
                        }
                        Thread.Sleep(1);
                    }

                }
                else
                {
                    FADM_Object.Communal.WriteMachineStatus(8);
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        FADM_Form.CustomMessageBox.Show(ex.Message, "定点移动", MessageBoxButtons.OK, true);
                    else
                        FADM_Form.CustomMessageBox.Show(ex.Message, "Fixed-point movement", MessageBoxButtons.OK, true);
                }
            }
        }
        private void BtnHome_Click(object sender, EventArgs e)
        {
            FADM_Object.Communal._fadmSqlserver.InsertRun("Machine", "点击回零");
            _b_istrue = true;
            if ((0 == FADM_Object.Communal.ReadMachineStatus() || 8 == FADM_Object.Communal.ReadMachineStatus()) && null == FADM_Object.Communal.ReadDyeThread()&& FADM_Object.Communal.ReadTcpStatus())
            {
                FADM_Object.Communal.WriteDripWait(true);
                FADM_Object.Communal.WriteMachineStatus(1);
                Thread thread = new Thread(Home);
                thread.Start();

                Thread threadReset = new Thread(Reset);
                threadReset.Start();

            }
            else
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    FADM_Form.CustomMessageBox.Show("机台正在运动，不能开始", "回零", MessageBoxButtons.OK, true);
                else
                    FADM_Form.CustomMessageBox.Show("The machine is in motion and cannot start", "Tips", MessageBoxButtons.OK, true);
            }

        }

        private void Home()
        {
            try
            {
                //FADM_Auto.Reset.IOReset();//这里会检查是否有针筒
                int i_state = MyModbusFun.goHome();
                    if (0 != i_state && -2 != i_state)

                    throw new Exception("驱动异常");
                else
                    FADM_Object.Communal.WriteMachineStatus(0);
            }
            catch (Exception ex)
            {
                _b_istrue = false;
                FADM_Object.Communal.WriteMachineStatus(8);

                if (ex.Message.Equals("-2"))
                {
                    FADM_Object.Communal.WriteMachineStatus(8);
                    int[] ia_errArray = new int[100];
                    MyModbusFun.GetErrMsgNew(ref ia_errArray);
                    List<string> lis_err = new List<string>();
                    for (int i = 0; i < ia_errArray.Length; i++)
                    {
                        if (ia_errArray[i] != 0)
                        {
                            if (SmartDyeing.FADM_Object.Communal._dic_errModbusNoNew.ContainsKey(ia_errArray[i]))
                            {
                                string s_err = SmartDyeing.FADM_Object.Communal._dic_errModbusNoNew[ia_errArray[i]];
                                string s_sql = "INSERT INTO alarm_table" +
                                 "(MyDate,MyTime,AlarmHead,AlarmDetails)" +
                                 " VALUES( '" +
                                 String.Format("{0:d}", DateTime.Now) + "','" +
                                 String.Format("{0:T}", DateTime.Now) + "','" +
                                 "Home" + "','" +
                                 s_err + "(Test)');";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                                string s_insert = CardObject.InsertD(s_err, " myMachineReset");
                                if (!lis_err.Contains(s_insert))
                                    lis_err.Add(s_insert);
                                //while (true)
                                //{
                                //    Thread.Sleep(1);
                                //    if (Lib_Card.CardObject.keyValuePairs[s_insert].Choose != 0)
                                //        break;

                                //}

                                //int _i_alarm_Choose = Lib_Card.CardObject.keyValuePairs[s_insert].Choose;
                                //CardObject.DeleteD(s_insert);

                            }

                        }
                    }

                    while (true)
                    {
                        for (int p = lis_err.Count - 1; p >= 0; p--)
                        {
                            if (Lib_Card.CardObject.keyValuePairs[lis_err[p]].Choose != 0)
                            {
                                CardObject.DeleteD(lis_err[p]);
                                lis_err.Remove(lis_err[p]);
                            }
                        }
                        if (lis_err.Count == 0)
                        {
                            break;
                        }
                        Thread.Sleep(1);
                    }

                }
                else {
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        FADM_Form.CustomMessageBox.Show(ex.Message, "回零", MessageBoxButtons.OK, true);
                    else
                        FADM_Form.CustomMessageBox.Show(ex.Message, "zeroing", MessageBoxButtons.OK, true);
                }

                
            }
            finally {
                _b_istrue = false;
            }
        }


        private void BtnCoordinate_Click(object sender, EventArgs e)
        {
           
        }

        private void Coordinate()
        {
         
        }

        private void BtnMove_Click(object sender, EventArgs e)
        {
            FADM_Object.Communal._fadmSqlserver.InsertRun("Machine", "点击联动");
            if ((0 == FADM_Object.Communal.ReadMachineStatus() || 8 == FADM_Object.Communal.ReadMachineStatus()) && null == FADM_Object.Communal.ReadDyeThread() && FADM_Object.Communal.ReadTcpStatus())
            {
                FADM_Object.Communal.WriteDripWait(true);
                if (RdoX.Checked)
                {
                    FADM_Object.Communal.WriteMachineStatus(3);
                    Thread thread = new Thread(X);
                    thread.Start();
                }
                else if (RdoY.Checked)
                {
                    FADM_Object.Communal.WriteMachineStatus(3);
                    Thread thread = new Thread(Y);
                    thread.Start();
                }
                else if (RdoZ.Checked)
                {
                    FADM_Object.Communal.WriteMachineStatus(3);
                    Thread thread = new Thread(Z);
                    thread.Start();
                }
                else
                {

                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        FADM_Form.CustomMessageBox.Show("请选择轴号！", "温馨提示", MessageBoxButtons.OK, false);
                    else
                        FADM_Form.CustomMessageBox.Show("Please select the axis number！", "Tips", MessageBoxButtons.OK, true);
                }
                Thread threadReset = new Thread(Reset);
                threadReset.Start();
            }
            else
            {
                FADM_Form.CustomMessageBox.Show("机台正在运动，不能开始", "联动", MessageBoxButtons.OK, true);
            }
        }

        private void X()
        {
            try
            {
                _b_istrue = false;
                int i_state = MyModbusFun.TargetMoveRelative(1, Convert.ToInt32(this.TxtPulseX.Text), Convert.ToInt32(this.TxtHSpeedX.Text), Convert.ToInt32(this.TxtUpTimeX.Text));
                if (i_state != 0&& i_state != -2)
                    throw new Exception("驱动异常");
                else
                    FADM_Object.Communal.WriteMachineStatus(0);
            }
            catch (Exception ex)
            {
                FADM_Object.Communal.WriteMachineStatus(8);
                if (ex.Message.Equals("-2"))
                {
                    FADM_Object.Communal.WriteMachineStatus(8);
                    int[] ia_errArray = new int[100];
                    MyModbusFun.GetErrMsgNew(ref ia_errArray);
                    List<string> lis_err = new List<string>();
                    for (int i = 0; i < ia_errArray.Length; i++)
                    {
                        if (ia_errArray[i] != 0)
                        {
                            if (SmartDyeing.FADM_Object.Communal._dic_errModbusNoNew.ContainsKey(ia_errArray[i]))
                            {
                                string s_err = SmartDyeing.FADM_Object.Communal._dic_errModbusNoNew[ia_errArray[i]];
                                string s_sql = "INSERT INTO alarm_table" +
                                 "(MyDate,MyTime,AlarmHead,AlarmDetails)" +
                                 " VALUES( '" +
                                 String.Format("{0:d}", DateTime.Now) + "','" +
                                 String.Format("{0:T}", DateTime.Now) + "','" +
                                 "X" + "','" +
                                 s_err + "(Test)');";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                                string s_insert = CardObject.InsertD(s_err, " myMachineReset");
                                if (!lis_err.Contains(s_insert))
                                    lis_err.Add(s_insert);
                                //while (true)
                                //{
                                //    Thread.Sleep(1);
                                //    if (Lib_Card.CardObject.keyValuePairs[s_insert].Choose != 0)
                                //        break;

                                //}

                                //int _i_alarm_Choose = Lib_Card.CardObject.keyValuePairs[s_insert].Choose;
                                //CardObject.DeleteD(s_insert);

                            }

                        }
                    }

                    while (true)
                    {
                        for (int p = lis_err.Count - 1; p >= 0; p--)
                        {
                            if (Lib_Card.CardObject.keyValuePairs[lis_err[p]].Choose != 0)
                            {
                                CardObject.DeleteD(lis_err[p]);
                                lis_err.Remove(lis_err[p]);
                            }
                        }
                        if (lis_err.Count == 0)
                        {
                            break;
                        }
                        Thread.Sleep(1);
                    }

                }
                else
                {
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        FADM_Form.CustomMessageBox.Show(ex.Message, "联动", MessageBoxButtons.OK, true);
                    else
                        FADM_Form.CustomMessageBox.Show(ex.Message, "linkage", MessageBoxButtons.OK, true);
                }
            }
            finally {
                _b_istrue = false;
            }

        }

        private void Y()
        {
            try
            {
                _b_istrue = true;
                int state = MyModbusFun.TargetMoveRelative(2, Convert.ToInt32(this.TxtPulseY.Text), Convert.ToInt32(this.TxtHSpeedY.Text), Convert.ToInt32(this.TxtUpTimeY.Text));
                if (state != 0 && state != -2)
                    throw new Exception("驱动异常");
                else
                    FADM_Object.Communal.WriteMachineStatus(0);
            }
            catch (Exception ex)
            {
                FADM_Object.Communal.WriteMachineStatus(8);
                if (ex.Message.Equals("-2"))
                {
                    //string[] strArray = { "", "" };
                    ////根据编号读取异常信息
                    //MyModbusFun.GetErrMsg(ref strArray);
                    //if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    //    FADM_Form.CustomMessageBox.Show(strArray[1], "联动", MessageBoxButtons.OK, true);
                    //else
                    //    FADM_Form.CustomMessageBox.Show(strArray[1], "linkage", MessageBoxButtons.OK, true);

                    int[] ErrArray = new int[100];
                    MyModbusFun.GetErrMsgNew(ref ErrArray);

                    List<string> stringErr = new List<string>();
                    for (int i = 0; i < ErrArray.Length; i++)
                    {
                        if (ErrArray[i] != 0)
                        {
                            if (SmartDyeing.FADM_Object.Communal._dic_errModbusNoNew.ContainsKey(ErrArray[i]))
                            {
                                string strErr = SmartDyeing.FADM_Object.Communal._dic_errModbusNoNew[ErrArray[i]];
                                string Str = "INSERT INTO alarm_table" +
                                 "(MyDate,MyTime,AlarmHead,AlarmDetails)" +
                                 " VALUES( '" +
                                 String.Format("{0:d}", DateTime.Now) + "','" +
                                 String.Format("{0:T}", DateTime.Now) + "','" +
                                 "Y" + "','" +
                                 strErr + "(Test)');";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(Str);

                                string s = CardObject.InsertD(strErr, " Y");
                                if (!stringErr.Contains(s))
                                    stringErr.Add(s);
                                //while (true)
                                //{
                                //    Thread.Sleep(1);
                                //    if (Lib_Card.CardObject.keyValuePairs[s_insert].Choose != 0)
                                //        break;

                                //}

                                //int _i_alarm_Choose = Lib_Card.CardObject.keyValuePairs[s_insert].Choose;
                                //CardObject.DeleteD(s_insert);

                            }

                        }
                    }

                    while (true)
                    {
                        for (int p = stringErr.Count - 1; p >= 0; p--)
                        {
                            if (Lib_Card.CardObject.keyValuePairs[stringErr[p]].Choose != 0)
                            {
                                CardObject.DeleteD(stringErr[p]);
                                stringErr.Remove(stringErr[p]);
                            }
                        }
                        if (stringErr.Count == 0)
                        {
                            break;
                        }
                        Thread.Sleep(1);
                    }

                }
                else
                {
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        FADM_Form.CustomMessageBox.Show(ex.Message, "联动", MessageBoxButtons.OK, true);
                    else
                        FADM_Form.CustomMessageBox.Show(ex.Message, "linkage", MessageBoxButtons.OK, true);
                }
            }
            finally {
                _b_istrue = false;
            }
        }

        private void Z()
        {
            try
            {
                _b_istrue = true;
                int state = MyModbusFun.TargetMoveRelative(3, Convert.ToInt32(this.TxtPulseZ.Text), Convert.ToInt32(this.TxtHSpeedZ.Text), Convert.ToInt32(this.TxtUpTimeZ.Text));
                if (state != 0 && state != -2)
                    throw new Exception("驱动异常");
                else
                    FADM_Object.Communal.WriteMachineStatus(0);
            }
            catch (Exception ex)
            {
                FADM_Object.Communal.WriteMachineStatus(8);
                if (ex.Message.Equals("-2"))
                {
                    FADM_Object.Communal.WriteMachineStatus(8);
                    int[] ia_errArray = new int[100];
                    MyModbusFun.GetErrMsgNew(ref ia_errArray);
                    List<string> lis_err = new List<string>();
                    for (int i = 0; i < ia_errArray.Length; i++)
                    {
                        if (ia_errArray[i] != 0)
                        {
                            if (SmartDyeing.FADM_Object.Communal._dic_errModbusNoNew.ContainsKey(ia_errArray[i]))
                            {
                                string s_err = SmartDyeing.FADM_Object.Communal._dic_errModbusNoNew[ia_errArray[i]];
                                string s_sql = "INSERT INTO alarm_table" +
                                 "(MyDate,MyTime,AlarmHead,AlarmDetails)" +
                                 " VALUES( '" +
                                 String.Format("{0:d}", DateTime.Now) + "','" +
                                 String.Format("{0:T}", DateTime.Now) + "','" +
                                 "Z" + "','" +
                                 s_err + "(Test)');";
                                FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                                string s_insert = CardObject.InsertD(s_err, " myMachineReset");
                                if (!lis_err.Contains(s_insert))
                                    lis_err.Add(s_insert);
                                //while (true)
                                //{
                                //    Thread.Sleep(1);
                                //    if (Lib_Card.CardObject.keyValuePairs[s_insert].Choose != 0)
                                //        break;

                                //}

                                //int _i_alarm_Choose = Lib_Card.CardObject.keyValuePairs[s_insert].Choose;
                                //CardObject.DeleteD(s_insert);

                            }

                        }
                    }

                    while (true)
                    {
                        for (int p = lis_err.Count - 1; p >= 0; p--)
                        {
                            if (Lib_Card.CardObject.keyValuePairs[lis_err[p]].Choose != 0)
                            {
                                CardObject.DeleteD(lis_err[p]);
                                lis_err.Remove(lis_err[p]);
                            }
                        }
                        if (lis_err.Count == 0)
                        {
                            break;
                        }
                        Thread.Sleep(1);
                    }

                }
                else
                {
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        FADM_Form.CustomMessageBox.Show(ex.Message, "联动", MessageBoxButtons.OK, true);
                    else
                        FADM_Form.CustomMessageBox.Show(ex.Message, "linkage", MessageBoxButtons.OK, true);
                }
            }
            finally {
                _b_istrue = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void Debug_Load(object sender, EventArgs e)
        {
            //FADM_Object.Communal._b_pause = true;
            if (Lib_Card.Configure.Parameter.Machine_Decompression == 1 || Lib_Card.Configure.Parameter.Machine_Decompression == 0)
            {
                //BtnOutPut_Decompression_Right.Visible = false;
                //ChkInPut_Decompression_Up_Right.Visible = false;
                //ChkInPut_Decompression_Down_Right.Visible = false;
            }
            if (Lib_Card.Configure.Parameter.Other_IsOnlyDrip == 1)
            {
                RdoDecompression.Enabled = false;
                BtnOutPut_Decompression.Enabled = false;
            }
        }

        private void Debug_Leave(object sender, EventArgs e)
        {
            //FADM_Object.Communal._b_pause = false;
            if (Lib_Card.Configure.Parameter.Machine_IsStopOrFront == 0)
            {
                Lib_Card.CardObject.bStopScr = false;
            }
            else
            {
                Lib_Card.CardObject.bFront = false;
            }
            Lib_Card.CardObject.bRight = false;
            Lib_Card.CardObject.bLeft = false;
        }

        private void BtnOutPut_Decompression_Right_Click(object sender, EventArgs e)
        {
            //try {
            //    _b_istrue = true;
            //    if (this.BtnOutPut_Decompression_Right.ForeColor == Color.Red)
            //    {
            //        _ia_array[0] = 25;
            //    }
            //    else
            //    {
            //        _ia_array[0] = 26;
            //    }
            //    int i_state = FADM_Object.Communal._tcpModBus.Write(811, _ia_array);

            //} catch { } finally {
            //    _b_istrue = false ;
            //}
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                _b_istrue = true;
                _ia_array[0] = 27;
                int i_state = FADM_Object.Communal._tcpModBus.Write(811, _ia_array);

            }
            catch { }
            finally
            {
                _b_istrue = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                _b_istrue = true;
                _ia_array[0] = 28;
                int i_state = FADM_Object.Communal._tcpModBus.Write(811, _ia_array);

            }
            catch { }
            finally
            {
                _b_istrue = false;
            }
        }

        private void btn_reset_a_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    btn_reset_a.Enabled = false;
            //    _b_istrue = true;
            //    _ia_array[0] = 29;
            //    int i_state = FADM_Object.Communal._tcpModBus.Write(811, _ia_array);
            //    if (i_state != -1)
            //    {
            //        FADM_Form.CustomMessageBox.Show("清零成功!", "温馨提示", MessageBoxButtons.OK, false);
            //    }
            //    else {
            //        FADM_Form.CustomMessageBox.Show("清零失败!", "温馨提示", MessageBoxButtons.OK, false);
            //    }
            //    btn_reset_a.Enabled = true;
            //}
            //catch
            //{

            //}
            //finally
            //{
            //    _b_istrue = false;
            //}
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            FADM_Object.Communal._b_stop = true;
        }

        private void BtnOutPut_Slow_Click(object sender, EventArgs e)
        {
            try
            {
                _b_istrue = true;
                _ia_array[0] = 36;
                int i_state = FADM_Object.Communal._tcpModBus.Write(811, _ia_array);

            }
            catch { }
            finally
            {
                _b_istrue = false;
            }
        }

        private void BtnOutPut_Block_Cylinder_Click(object sender, EventArgs e)
        {
            try
            {
                _b_istrue = true;
                _ia_array[0] = 37;
                int i_state = FADM_Object.Communal._tcpModBus.Write(811, _ia_array);

            }
            catch { }
            finally
            {
                _b_istrue = false;
            }
        }
    }
}
