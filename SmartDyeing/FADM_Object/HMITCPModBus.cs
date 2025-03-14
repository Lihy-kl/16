﻿using EasyModbus;
using Newtonsoft.Json.Linq;
using SmartDyeing.FADM_Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace SmartDyeing.FADM_Object
{
    public class HMITCPModBus
    {
        private static readonly object lockObject = new object();

        public HMITCPModBus() { }

        //
        public ModbusClient _modbusClient;

        //是否已连接
        public bool _b_Connect = false;

        //连接IP
        public string _s_ip = "192.168.1.2";
        //连接端口
        public int _i_port = 502;

        //是否已经发送过开关盖状态
        public bool _b_isSendCoverStatus1 = false;
        public bool _b_isSendCoverStatus2 = false;
        public bool _b_isSendCoverStatus3 = false;
        public bool _b_isSendCoverStatus4 = false;
        public bool _b_isSendCoverStatus5 = false;
        public bool _b_isSendCoverStatus6 = false;
        public bool _b_isSendCoverStatus7 = false;
        public bool _b_isSendCoverStatus8 = false;
        public bool _b_isSendCoverStatus9 = false;
        public bool _b_isSendCoverStatus10 = false;
        public bool _b_isSendCoverStatus11 = false;
        public bool _b_isSendCoverStatus12 = false;
        public bool _b_isSendCoverStatus13 = false;
        public bool _b_isSendCoverStatus14 = false;
        public bool _b_isSendCoverStatus15 = false;
        public bool _b_isSendCoverStatus16 = false;
        //是否已经发送过开关盖状态（用于双杯）
        public bool[] _b_isSendCoverStatus = { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        //是否已经读取版本号
        public bool _b_isGetVer = false;


        //连接
        public int Connect()
        {
            try
            {
                _modbusClient = new ModbusClient(_s_ip, _i_port);  // IP地址和端口
                _modbusClient.Connect(); // 连接到服务器
                _b_Connect = true;
                return 0;
            }
            catch
            {
                return -1;
            }
        }
        //关闭连接
        public int Disconnect()
        {
            _modbusClient.Disconnect(); // 断开连接
            return 0;
        }

        //重连
        public int ReConnect()
        {
            Lib_Log.Log.writeLogException("注意,重连!!!!!");
            Console.WriteLine("注意,重连!!!!!");
            try
            {
                _modbusClient.Disconnect(); // 断开连接
                _modbusClient.Connect(); // 连接到服务器
                return 0;
            }
            catch { return -1; }
        }
        /// <summary>
        /// 读取打板机版本号
        /// </summary>
        /// <param name="i_type">打板机类型 0 转子机 1 摇摆机 </param>
        /// <returns></returns>
        public int ReadVer(ref int[] ia_values)
        {
            lock (lockObject)
            {
                try
                {
                    int i_ret = -1;
                    i_ret = Read(4, 6, ref ia_values);
                    if (i_ret == 0)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// 读取打板机版本号
        /// </summary>
        /// <param name="i_type">打板机类型 0 转子机 1 摇摆机 </param>
        /// <returns></returns>
        public int ReadVer(ref int[] ia_values,int i_type)
        {
            lock (lockObject)
            {
                try
                {
                    int i_ret = -1;
                    if (i_type == 4)
                    {
                        i_ret = Read(0x400, 1, ref ia_values);
                    }
                    else
                    {
                        i_ret = Read(4, 6, ref ia_values);
                    }
                    if (i_ret == 0)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }


        //读寄存器数据
        //startingAddress读取寄存器开始地址；num读取寄存器数量；values返回寄存器的值

        public int Read(int i_startingAddress, int i_num, ref int[] ia_values)
        {
            lock (lockObject)
            {
                try
                {
                    //_modbusClient.Connect(); // 连接到服务器
                    ia_values = _modbusClient.ReadHoldingRegisters(i_startingAddress, i_num);
                    //_modbusClient.Disconnect(); // 断开连接
                    return 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("读寄存器数据异常" + ex.Message);
                    return -1;
                }
            }
        }

        //写寄存器数据
        //startingAddress读取寄存器开始地址；values返回寄存器的值
        public int Write(int i_startingAddress, int[] ia_values)
        {
            lock (lockObject)
            {
            labRewrite:
                try
                {
                    //_modbusClient.Connect(); // 连接到服务器
                    _modbusClient.WriteMultipleRegisters(i_startingAddress, ia_values);
                    string s = null;
                    for (int i = 0; i < ia_values.Length; i++)
                    {
                        s += ia_values[i].ToString() + " ";
                    }
                    Lib_Log.Log.writeLogExceptionHMI("开始地址:" + i_startingAddress + " 数据:" + s);
                    // _modbusClient.Disconnect(); // 断开连接
                    return 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("写寄存器数据异常" + ex.Message);
                    ReConnect();
                    goto labRewrite;
                    return -1;
                }
            }
        }

        public bool getConnect()
        {
            try
            {
                //bool _b_istrue =_modbusClient.Available(2000);
                return _modbusClient.Connected;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 染色/后处理读
        /// </summary>
        /// <param name="i_type">打板机类型 0 转子机 1 摇摆机 </param>
        /// <returns></returns>
        public List<Data> DyeRead(ref List<Data> lis_l,int i_type)
        {

            lock (lockObject)
            {
                try
                {
                    //6杯/12杯/4杯/10杯
                    if (i_type == 1 || i_type == 2|| i_type == 3 || i_type == 5)
                    {
                        int[] ia_values_ask = new int[6];
                        for(int i = 0;i< ia_values_ask.Length;i++)
                        {
                            ia_values_ask[i] = 0;
                        }
                        //读取放布申请
                        if (FADM_Object.Communal._b_isNeedConfirm)
                        {
                            if (i_type == 2||i_type == 5)
                            {
                                
                                int i_ret = -1;
                                i_ret = Read(910, 6, ref ia_values_ask);
                                if (i_ret == 0)
                                {
                                }
                            }
                        }


                        for (int i = 0; i < 6; i++)
                        {
                            int[] ia_values = new int[64];
                            int i_ret = -1;
                            //if (i_type == 0)
                            {
                                i_ret = Read(500 + 64 * i, 64, ref ia_values);
                                if (i_ret == 0)
                                {
                                    //解析数据
                                    Data d = new Data();
                                    d._s_waitData = (ia_values[0]).ToString();
                                    d._s_isTotalFinish = (ia_values[1]).ToString();
                                    d._s_realTem = (ia_values[2]).ToString();
                                    d._s_currentCraft = (ia_values[3]).ToString();
                                    d._s_currentState = (ia_values[4]).ToString();
                                    d._s_currentStepNum = (ia_values[5]).ToString();
                                    d._s_holdTimes = (ia_values[6]).ToString();
                                    d._s_overTime = (ia_values[7]).ToString();
                                    d._s_openInplace = (ia_values[8]).ToString();
                                    d._s_addWater = (ia_values[9]).ToString();
                                    d._s_dripFail = (ia_values[10]).ToString();
                                    d._s_history = (ia_values[11]).ToString();
                                    d._s_lockUp = (ia_values[17]).ToString();
                                    d._s_Warm = (ia_values[19]).ToString();
                                    d._s_secondhistory = (ia_values[21]).ToString();
                                    d._s_secondopenInplace = (ia_values[22]).ToString();
                                    d._s_secondrealTem = (ia_values[44]).ToString();
                                    d._s_putcloth = (ia_values_ask[i]).ToString();
                                    //int[] ia_values1 = new int[1];
                                    lis_l.Add(d);
                                    _b_Connect = true;
                                    //int ret1 = Read(508 + 64 * i, 1, ref ia_values1);
                                    //if (ret1 == 0)
                                    //{
                                    //    d._s_coverSign = (ia_values1[0]).ToString();
                                    //    lis_l.Add(d);
                                    //    _b_Connect = true;
                                    //}
                                    //else
                                    //{
                                    //    _b_Connect = false;
                                    //}
                                }
                                else
                                {
                                    _b_Connect = false;
                                }
                            }

                        }

                        
                    }
                    //16杯翻转
                    else if(i_type == 4)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            int[] ia_values = new int[64];
                            int i_ret = -1;
                            //if (i_type == 0)
                            {
                                i_ret = Read(0x0100 + 64 * i, 64, ref ia_values);
                                if (i_ret == 0)
                                {
                                    //解析数据
                                    Data d = new Data();
                                    d._s_waitData = (ia_values[0]).ToString();
                                    d._s_isTotalFinish = (ia_values[1]).ToString();
                                    d._s_realTem = (ia_values[2]).ToString();
                                    d._s_currentCraft = (ia_values[3]).ToString();
                                    d._s_currentState = (ia_values[4]).ToString();
                                    d._s_currentStepNum = (ia_values[5]).ToString();
                                    d._s_holdTimes = (ia_values[6]).ToString();
                                    d._s_overTime = (ia_values[7]).ToString();
                                    d._s_openInplace = (ia_values[8]).ToString();
                                    d._s_addWater = (ia_values[9]).ToString();
                                    d._s_dripFail = (ia_values[10]).ToString();
                                    d._s_history = (ia_values[11]).ToString();
                                    d._s_lockUp = (ia_values[17]).ToString();
                                    d._s_Warm = (ia_values[19]).ToString();
                                    d._s_secondhistory = (ia_values[21]).ToString();
                                    d._s_secondopenInplace = (ia_values[22]).ToString();
                                    d._s_secondrealTem = (ia_values[46]).ToString();
                                    d._s_putcloth = (ia_values[23]).ToString();
                                    //int[] ia_values1 = new int[1];
                                    lis_l.Add(d);
                                    _b_Connect = true;
                                    //int ret1 = Read(508 + 64 * i, 1, ref ia_values1);
                                    //if (ret1 == 0)
                                    //{
                                    //    d._s_coverSign = (ia_values1[0]).ToString();
                                    //    lis_l.Add(d);
                                    //    _b_Connect = true;
                                    //}
                                    //else
                                    //{
                                    //    _b_Connect = false;
                                    //}
                                }
                                else
                                {
                                    _b_Connect = false;
                                }
                            }

                        }
                    }
                    else
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            int[] ia_values = new int[16];
                            int i_ret = -1;
                            i_ret = Read(300 + 16 * i, 16, ref ia_values);
                            if (i_ret == 0)
                            {
                                //解析数据
                                Data d = new Data();
                                d._s_waitData = (ia_values[0]).ToString();
                                d._s_isTotalFinish = (ia_values[1]).ToString();
                                d._s_realTem = (ia_values[2]).ToString();
                                d._s_currentCraft = (ia_values[3]).ToString();
                                d._s_currentState = (ia_values[4]).ToString();
                                d._s_currentStepNum = (ia_values[5]).ToString();
                                d._s_overTemTimes = (ia_values[6]).ToString();
                                d._s_overTime = (ia_values[7]).ToString();
                                d._s_openInplace = (ia_values[8]).ToString();
                                d._s_addWater = (ia_values[9]).ToString();
                                d._s_dripFail = (ia_values[10]).ToString();
                                d._s_history = (ia_values[11]).ToString();
                                int[] ia_values1 = new int[1];
                                lis_l.Add(d);
                                _b_Connect = true;
                            }
                            else
                            {
                                _b_Connect = false;
                            }
                        }
                    }


                    //解析数据
                    return lis_l;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// 16杯翻转缸参数读取
        /// </summary>
        /// <returns></returns>
        public List<ParameterData> DyeReadParameter(ref List<ParameterData> lis_l)
        {

            lock (lockObject)
            {
                try
                {
                    for (int i = 0; i < 4; i++)
                    {
                        int[] ia_values = new int[64];
                        int i_ret = -1;
                        //if (i_type == 0)
                        {
                            i_ret = Read(0x0200 + 64 * i, 64, ref ia_values);
                            if (i_ret == 0)
                            {
                                //解析数据
                                ParameterData d = new ParameterData();
                                d._s_rev = (ia_values[2] / 10.0).ToString("f1");
                                d._s_zeroVelocity = (ia_values[3] / 10.0).ToString("f1");
                                d._s_decelerationTime = (ia_values[4]).ToString();
                                d._s_forwardTime = (ia_values[5]).ToString();
                                d._s_pauseTime = (ia_values[6]).ToString();
                                d._s_reversalTime = (ia_values[7]).ToString();
                                d._s_openTem = (ia_values[13] / 10.0).ToString("f1");
                                d._s_limitTem = (ia_values[14] / 10.0).ToString("f1");
                                d._s_warmUp = (ia_values[19] / 10.0).ToString("f1");
                                d._s_warmDown = (ia_values[20] / 10.0).ToString("f1");
                                d._s_openCoverDrainage = (ia_values[21]).ToString();
                                d._s_closeCoverDrainage = (ia_values[32]).ToString();
                                d._s_currenAlarmValue = (ia_values[27]).ToString();
                                d._s_alarmTime = (ia_values[28]).ToString();
                                d._s_fastTem = (ia_values[29] / 10.0).ToString("f1");
                                d._s_fastRate = (ia_values[30] / 10.0).ToString("f1");
                                d._s_washTem = (ia_values[31] / 10.0).ToString("f1");
                                d._s_mainTem = (ia_values[0]/10.0).ToString("f1");
                                d._s_mainTemCorrection = (ia_values[8]/10.0).ToString("f1");
                                d._s_mainControlCycle = (ia_values[9]/10.0).ToString("f1");
                                d._s_mainP = (ia_values[10]).ToString();
                                d._s_mainI = (ia_values[11]).ToString();
                                d._s_mainD = (ia_values[12]).ToString();
                                d._s_mainCurrenAD = (ia_values[46]).ToString();
                                d._s_mainLowTem = (ia_values[47] / 10.0).ToString("f1");
                                d._s_mainLowTemAD = (ia_values[48]).ToString();
                                d._s_mainHighTem = (ia_values[49] / 10.0).ToString("f1");
                                d._s_mainHighTemAD = (ia_values[50]).ToString();

                                int[] ia_values1 = new int[64];
                                int ret1 = Read(0x0300 + 64 * i, 64, ref ia_values1);
                                if (ret1 == 0)
                                {
                                    d._s_assistantTem = (ia_values1[0]/10.0).ToString("f1");
                                    d._s_assistantTemCorrection = (ia_values1[8]/10.0).ToString("f1");
                                    d._s_assistantControlCycle = (ia_values1[9] / 10.0).ToString("f1");
                                    d._s_assistantP = (ia_values1[10] ).ToString();
                                    d._s_assistantI = (ia_values1[11]).ToString();
                                    d._s_assistantD = (ia_values1[12]).ToString();
                                    d._s_assistantCurrenAD = (ia_values1[46]).ToString();
                                    d._s_assistantLowTem = (ia_values1[47] / 10.0).ToString("f1");
                                    d._s_assistantLowTemAD = (ia_values1[48]).ToString();
                                    d._s_assistantHighTem = (ia_values1[49] / 10.0).ToString("f1");
                                    d._s_assistantHighTemAD = (ia_values1[50]).ToString();
                                    lis_l.Add(d);
                                    _b_Connect = true;
                                }
                                else
                                {
                                    _b_Connect = false;
                                }
                            }
                            else
                            {
                                _b_Connect = false;
                            }
                        }

                    }
                    //解析数据
                    return lis_l;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// 读取报警信息
        /// </summary>
        /// <param name="iStationID">站号</param>
        /// <returns></returns>
        public int  DyeReadAlarm()
        {

            lock (this)
            {
                try
                {

                    int[] values = new int[1];
                    int ret = -1;
                    ret = Read(460, 1, ref values);
                    if (ret == 0)
                    {
                        int num = values[0];
                        _b_Connect = true;
                        return num;
                    }
                    else
                    {
                        _b_Connect = false;
                        return -1;
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }

    /// <summary>
    /// 染色/后处理数据交互结构
    /// </summary>
    public class Data
    {
        /// <summary>
        /// 等待数据
        /// </summary>
        public string _s_waitData;

        /// <summary>
        /// 总工艺完成
        /// </summary>
        public string _s_isTotalFinish;

        /// <summary>
        /// 实际温度
        /// </summary>
        public string _s_realTem;

        /// <summary>
        /// 副杯实际温度
        /// </summary>
        public string _s_secondrealTem;

        /// <summary>
        /// 当前工艺
        /// </summary>
        public string _s_currentCraft;

        /// <summary>
        /// 当前状态
        /// </summary>
        public string _s_currentState;

        /// <summary>
        /// 当前步号
        /// </summary>
        public string _s_currentStepNum;

        /// <summary>
        /// 当前保温时间
        /// </summary>
        public string _s_holdTimes;

        /// <summary>
        /// 超温度次数
        /// </summary>
        public string _s_overTemTimes;

        /// <summary>
        /// 超温度时间
        /// </summary>
        public string _s_overTime;

        /// <summary>
        /// 杯盖动作信号(或泄压信号)
        /// </summary>
        public string _s_openInplace;

        /// <summary>
        /// 副杯杯盖动作信号(或泄压信号)
        /// </summary>
        public string _s_secondopenInplace;

        /// <summary>
        /// 洗杯加水
        /// </summary>
        public string _s_addWater;

        /// <summary>
        /// 滴液失败信号
        /// </summary>
        public string _s_dripFail;

        /// <summary>
        /// 历史状态
        /// </summary>
        public string _s_history;

        /// <summary>
        /// 副杯历史状态
        /// </summary>
        public string _s_secondhistory;

        /// <summary>
        ///  请求开关盖信号
        /// </summary>
        public string _s_coverSign;

        /// <summary>
        ///  副杯请求开关盖信号
        /// </summary>
        public string _s_secondcoverSign;

        /// <summary>
        ///  锁止上状态0=无信号 1=锁止上信号
        /// </summary>
        public string _s_lockUp;

        /// <summary>
        ///  报警提示 0=超极限温度 1=电机电流过大 2=高于安全温度进入冷行 3=回原点超时
        /// </summary>
        public string _s_Warm;

        /// <summary>
        /// 放布确认申请(1放布申请)
        /// </summary>
        public string _s_putcloth;


    }

    /// <summary>
    /// 16杯转子缸参数结构
    /// </summary>
    public class ParameterData
    {
        /// <summary>
        /// 转速设定
        /// </summary>
        public string _s_rev;

        /// <summary>
        /// 回零速度
        /// </summary>
        public string _s_zeroVelocity;

        /// <summary>
        /// 加减速时间
        /// </summary>
        public string _s_decelerationTime;

        /// <summary>
        /// 正转时间
        /// </summary>
        public string _s_forwardTime;

        /// <summary>
        /// 停顿时间
        /// </summary>
        public string _s_pauseTime;

        /// <summary>
        /// 反转时间
        /// </summary>
        public string _s_reversalTime;

        /// <summary>
        /// 开盖温度
        /// </summary>
        public string _s_openTem;

        /// <summary>
        /// 极限温度
        /// </summary>
        public string _s_limitTem;

        /// <summary>
        /// 保温上限
        /// </summary>
        public string _s_warmUp;

        /// <summary>
        /// 保温下限
        /// </summary>
        public string _s_warmDown;

        /// <summary>
        /// 开盖排水时间
        /// </summary>
        public string _s_openCoverDrainage;

        /// <summary>
        /// 关盖排水时间
        /// </summary>
        public string _s_closeCoverDrainage;

        /// <summary>
        /// 电流报警值
        /// </summary>
        public string _s_currenAlarmValue;

        /// <summary>
        /// 报警时间
        /// </summary>
        public string _s_alarmTime;

        /// <summary>
        /// 快速升温温度
        /// </summary>
        public string _s_fastTem;

        /// <summary>
        /// 快速升温速率
        /// </summary>
        public string _s_fastRate;

        /// <summary>
        ///  洗杯温度
        /// </summary>
        public string _s_washTem;

        /// <summary>
        ///  主杯温度
        /// </summary>
        public string _s_mainTem;

        /// <summary>
        ///  主杯温度修正
        /// </summary>
        public string _s_mainTemCorrection;

        /// <summary>
        ///  主杯控制周期
        /// </summary>
        public string _s_mainControlCycle;

        /// <summary>
        /// 主杯控温P
        /// </summary>
        public string _s_mainP;

        /// <summary>
        /// 主杯控温I
        /// </summary>
        public string _s_mainI;

        /// <summary>
        /// 主杯控温D
        /// </summary>
        public string _s_mainD;

        /// <summary>
        /// 主杯当前温度AD
        /// </summary>
        public string _s_mainCurrenAD;

        /// <summary>
        /// 主杯低端温度设置
        /// </summary>
        public string _s_mainLowTem;

        /// <summary>
        /// 主杯低端温度的AD值
        /// </summary>
        public string _s_mainLowTemAD;

        /// <summary>
        /// 主杯高端温度设置
        /// </summary>
        public string _s_mainHighTem;

        /// <summary>
        /// 主杯高端温度的AD值
        /// </summary>
        public string _s_mainHighTemAD;

        /// <summary>
        ///  副杯温度
        /// </summary>
        public string _s_assistantTem;

        /// <summary>
        ///  副杯温度修正
        /// </summary>
        public string _s_assistantTemCorrection;

        /// <summary>
        ///  副杯控制周期
        /// </summary>
        public string _s_assistantControlCycle;

        /// <summary>
        /// 副杯控温P
        /// </summary>
        public string _s_assistantP;

        /// <summary>
        /// 副杯控温I
        /// </summary>
        public string _s_assistantI;

        /// <summary>
        /// 副杯控温D
        /// </summary>
        public string _s_assistantD;

        /// <summary>
        /// 副杯当前温度AD
        /// </summary>
        public string _s_assistantCurrenAD;

        /// <summary>
        /// 副杯低端温度设置
        /// </summary>
        public string _s_assistantLowTem;

        /// <summary>
        /// 副杯低端温度的AD值
        /// </summary>
        public string _s_assistantLowTemAD;

        /// <summary>
        /// 副杯高端温度设置
        /// </summary>
        public string _s_assistantHighTem;

        /// <summary>
        /// 副杯高端温度的AD值
        /// </summary>
        public string _s_assistantHighTemAD;

        /// <summary>
        /// 是否已刷新
        /// </summary>
        public bool  _b_refresh;

        public ParameterData() 
        {
            _b_refresh = false;
        }
    }
}
