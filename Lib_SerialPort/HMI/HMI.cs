
using System;
using System.Collections.Generic;
using System.Threading;
/// <summary>
/// 打板机触摸屏通讯模块
/// 
/// </summary>
namespace Lib_SerialPort.HMI
{
    public class HMI : SerialPortBase
    {

        /// <summary>
        /// 串口读取时状态返回
        /// -1:初始化
        /// 0：通讯成功
        /// 1：返回异常
        /// 2：通讯超时
        /// </summary>
        public static int nState { get; set; }

        //打板机状态
        public static int nState1 { get; set; }

        public HMI() : base()
        {
        }

        /// <summary>
        /// 染色/后处理读
        /// </summary>
        /// <param name="iStationID">站号</param>
        /// <returns></returns>
        public List<Data> DyeRead(int iStationID, ref List<Data> list)
        {

            lock (this)
            {
                try
                {

                    nState1 = -1;
                    for (int i = 0; i < 2; i++)
                    {
                        if (i == 0)
                        {
                            byte[] b_send = new byte[6];
                            b_send[0] = (byte)(iStationID);
                            b_send[1] = 0x03;
                            b_send[2] = 0x01;//2位开始位
                            b_send[3] = 0x2C;
                            b_send[4] = 0x00;//读取长度固定每次读取80个，相当于5个杯信息
                            b_send[5] = 0x50;
                            WriteAndRead(b_send);
                            if (Lib_SerialPort.HMI.HMI.nState == 0)
                            {
                                if (datapool.Count == 165)
                                {
                                    nState1 = 0;
                                    for (int k = 0; k < 5; k++)
                                    {
                                        Data d = new Data();
                                        d.s_WaitData = (datapool[3 + k * 32] << 8 | datapool[3 + k * 32 + 1]).ToString();
                                        d.s_IsTotalFinish = (datapool[3 + k * 32 + 2] << 8 | datapool[3 + k * 32 + 3]).ToString();
                                        d.s_RealTem = (datapool[3 + k * 32 + 4] << 8 | datapool[3 + k * 32 + 5]).ToString();
                                        d.s_CurrentCraft = (datapool[3 + k * 32 + 6] << 8 | datapool[3 + k * 32 + 7]).ToString();
                                        d.s_CurrentState = (datapool[3 + k * 32 + 8] << 8 | datapool[3 + k * 32 + 9]).ToString();
                                        d.s_CurrentStepNum = (datapool[3 + k * 32 + 10] << 8 | datapool[3 + k * 32 + 11]).ToString();
                                        d.s_OverTemTimes = (datapool[3 + k * 32 + 12] << 8 | datapool[3 + k * 32 + 13]).ToString();
                                        d.s_OverTime = (datapool[3 + k * 32 + 14] << 8 | datapool[3 + k * 32 + 15]).ToString();
                                        d.s_OpenInplace = (datapool[3 + k * 32 + 16] << 8 | datapool[3 + k * 32 + 17]).ToString();
                                        d.s_AddWater = (datapool[3 + k * 32 + 18] << 8 | datapool[3 + k * 32 + 19]).ToString();
                                        d.s_DripFail = (datapool[3 + k * 32 + 20] << 8 | datapool[3 + k * 32 + 21]).ToString();
                                        d.s_History = (datapool[3 + k * 32 + 22] << 8 | datapool[3 + k * 32 + 23]).ToString();
                                        list.Add(d);
                                    }
                                }
                                else
                                {
                                    return list;
                                }
                            }
                            else
                            {
                                return list;
                            }
                        }
                        else
                        {
                            if (Lib_SerialPort.HMI.HMI.nState == 0)
                            {
                                byte[] b_send = new byte[6];
                                b_send[0] = (byte)(iStationID);
                                b_send[1] = 0x03;
                                b_send[2] = 0x01;//2位开始位
                                b_send[3] = 0x7C;
                                b_send[4] = 0x00;//读取长度固定每次读取80个，相当于5个杯信息
                                b_send[5] = 0x50;
                                WriteAndRead(b_send);
                                if (Lib_SerialPort.HMI.HMI.nState == 0)
                                {
                                    if (datapool.Count == 165)
                                    {
                                        nState1 = 0;
                                        for (int k = 0; k < 5; k++)
                                        {
                                            Data d = new Data();
                                            d.s_WaitData = (datapool[3 + k * 32] << 8 | datapool[3 + k * 32 + 1]).ToString();
                                            d.s_IsTotalFinish = (datapool[3 + k * 32 + 2] << 8 | datapool[3 + k * 32 + 3]).ToString();
                                            d.s_RealTem = (datapool[3 + k * 32 + 4] << 8 | datapool[3 + k * 32 + 5]).ToString();
                                            d.s_CurrentCraft = (datapool[3 + k * 32 + 6] << 8 | datapool[3 + k * 32 + 7]).ToString();
                                            d.s_CurrentState = (datapool[3 + k * 32 + 8] << 8 | datapool[3 + k * 32 + 9]).ToString();
                                            d.s_CurrentStepNum = (datapool[3 + k * 32 + 10] << 8 | datapool[3 + k * 32 + 11]).ToString();
                                            d.s_OverTemTimes = (datapool[3 + k * 32 + 12] << 8 | datapool[3 + k * 32 + 13]).ToString();
                                            d.s_OverTime = (datapool[3 + k * 32 + 14] << 8 | datapool[3 + k * 32 + 15]).ToString();
                                            d.s_OpenInplace = (datapool[3 + k * 32 + 16] << 8 | datapool[3 + k * 32 + 17]).ToString();
                                            d.s_AddWater = (datapool[3 + k * 32 + 18] << 8 | datapool[3 + k * 32 + 19]).ToString();
                                            d.s_DripFail = (datapool[3 + k * 32 + 20] << 8 | datapool[3 + k * 32 + 21]).ToString();
                                            d.s_History = (datapool[3 + k * 32 + 22] << 8 | datapool[3 + k * 32 + 23]).ToString();
                                            list.Add(d);
                                        }
                                    }
                                    else
                                    {
                                        nState1 = -1;
                                        return list;
                                    }
                                }
                                else
                                {
                                    return list;
                                }
                            }
                        }
                    }


                    //解析数据
                    return list;
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
        public int DyeReadAlarm(int iStationID)
        {

            lock (this)
            {
                try
                {
                    int nRet = 0;
                    nState1 = -1;

                    byte[] b_send = new byte[6];
                    b_send[0] = (byte)(iStationID);
                    b_send[1] = 0x03;
                    b_send[2] = 0x01;//2位开始位
                    b_send[3] = 0xCC;
                    b_send[4] = 0x00;//
                    b_send[5] = 0x01;
                    WriteAndRead(b_send);
                    if (Lib_SerialPort.HMI.HMI.nState == 0)
                    {
                        //if (datapool.Count == 165)
                        {
                            nState1 = 0;
                            nRet = datapool[3] << 8 | datapool[4];
                            
                        }
                    }
                    return nRet;


                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public void write(byte[] buffer)
        {
            List<Data> list = new List<Data>();
            WriteAndRead(buffer);
        }

        //读数据
        public void WriteAndRead(byte[] buffer)
        {
            lock (this)
            {
                try
                {
                    nState = -1;
                    bSuccess = false;
                    datapool.Clear();

                    byte[] bCRC = MyCRC.ToModbus(buffer);

                    int iLength = buffer.Length + bCRC.Length;
                    byte[] bTotalBuffer = new byte[iLength];

                    for (int i = 0; i < iLength; i++)
                    {
                        if (i < buffer.Length)
                            bTotalBuffer[i] = buffer[i];
                        else
                            bTotalBuffer[i] = bCRC[i - buffer.Length];
                    }

                    Write(bTotalBuffer);
                    //需要读取字节数
                    int nlen = 0;
                    if (buffer[1] == 0x03)
                    {
                        nlen = Convert.ToInt16(buffer[5]) * 2 + 5;
                    }
                    else if (buffer[1] == 0x10)
                    {
                        nlen = 8;
                    }
                    else if (buffer[1] == 0x06)
                    {
                        nlen = 8;
                    }
                   

                    nLen = nlen;
                    //System.Threading.Thread.Sleep(100);
                    ManualResetEvent P_mre = new ManualResetEvent(false);

                    Thread P_thd_time = new Thread(() =>
                    {
                        bool P_bl = true;
                        Thread P_thd_time_1 = new Thread(() =>
                        {
                            while (P_bl)
                            {
                                if (bSuccess)
                                {
                                    //获取数据长度和计算一致证明接收完成，或者长度为5,返回数据过长，属于错误返回
                                    if (nLen == datapool.Count)
                                    {
                                        nState = 0;
                                        P_mre.Set();
                                        break;
                                    }
                                    else if (datapool.Count == 5)
                                    {
                                        nState = 1;
                                        string str = "返回异常：";
                                        for (int i=0;i< datapool.Count;i++)
                                        {
                                            str += datapool[i].ToString("X2") + " ";
                                        }
                                        Lib_Log.Log.writeLogException(str);
                                        P_mre.Set();
                                        break;
                                    }
                                    Thread.Sleep(1);
                                }
                            }
                        });
                        P_thd_time_1.Start();
                        // 设置超时等待时间
                        if (P_mre.WaitOne(2000))
                        {
                            P_mre.Reset();
                            return;
                        }
                        else
                        {
                            nState = 2;
                            string str = "通讯超时";
                            for (int i = 0; i < datapool.Count; i++)
                            {
                                str += datapool[i].ToString("X2") + " ";
                            }
                            Lib_Log.Log.writeLogException(str);
                            // 等待超时
                            P_bl = false;
                            P_thd_time_1.Join();

                        }
                    });

                    // 启动线程
                    P_thd_time.Start();
                    P_thd_time.Join();
                    Thread.Sleep(50);
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }



            //批量写入
        }


    }

    public class Data
    {
        /// <summary>
        /// 等待数据
        /// </summary>
        public string s_WaitData;

        /// <summary>
        /// 总工艺完成
        /// </summary>
        public string s_IsTotalFinish;

        /// <summary>
        /// 实际温度
        /// </summary>
        public string s_RealTem;

        /// <summary>
        /// 当前工艺
        /// </summary>
        public string s_CurrentCraft;

        /// <summary>
        /// 当前状态
        /// </summary>
        public string s_CurrentState;

        /// <summary>
        /// 当前步号
        /// </summary>
        public string s_CurrentStepNum;

        /// <summary>
        /// 超温度次数
        /// </summary>
        public string s_OverTemTimes;

        /// <summary>
        /// 超温度时间
        /// </summary>
        public string s_OverTime;

        /// <summary>
        /// 开盖到位
        /// </summary>
        public string s_OpenInplace;

        /// <summary>
        /// 排压信号
        /// </summary>
        public string s_AddWater;

        /// <summary>
        /// 滴液失败信号
        /// </summary>
        public string s_DripFail;

        /// <summary>
        /// 历史状态
        /// </summary>
        public string s_History;

       
    }
}
