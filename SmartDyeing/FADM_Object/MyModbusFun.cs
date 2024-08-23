﻿using Lib_Card;
using SmartDyeing.FADM_Auto;
using SmartDyeing.FADM_Control;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.AxHost;

namespace SmartDyeing.FADM_Object
{
    class MyModbusFun
    {

        //nNeedCheck是否需要处理未发现针筒
        //i_type 0为不发停止(复位时不响应停止)，1为发停止
        public static int GetReturn(int i_type)
        {

            int[] ia_array2 = { 0, 0, 0, 0, 0 };
            List<int> lis_data = new List<int>();
            List<string> lis_array = new List<string>();
            bool b_send = false;

            Communal._b_isUpdateNewData = false;

            while (true) //这里一直读取。然后天平那边分开读取 也会造成数据混乱问题 所以这里直接读4个字节。天平、光幕和执行完成
            {
                if (i_type == 1)
                {
                    if (FADM_Object.Communal._b_stop)
                    {
                        if (!b_send)
                        {
                            //输出停止信号
                            int[] ia_array = { 1 };
                            int i_state = FADM_Object.Communal._tcpModBus.Write(839, ia_array);
                            if (i_state != -1)
                            {
                                //FADM_Object.Communal._b_stop = false;
                                //return -2;
                                b_send = true;
                            }
                        }
                    }
                }

                if (!Communal._b_isBSendOpen)
                {
                    if (Communal._i_bType == 1)
                    {
                        int[] ia_array = { 17 };
                        int i_state = FADM_Object.Communal._tcpModBus.Write(811, ia_array);

                        Communal._b_isBSendClose = false;
                        Communal._b_isBSendOpen = true;
                    }
                }

                if (!Communal._b_isBSendClose)
                {
                    if (Communal._i_bType == 2)
                    {
                        int[] ia_array = { 18 };
                        int i_state = FADM_Object.Communal._tcpModBus.Write(811, ia_array);

                        Communal._b_isBSendOpen = false;
                        Communal._b_isBSendClose = true;
                    }
                }
                //一直读d904 执行完成标志位
                ia_array2 = AwaitSuccess();
                if (ia_array2[4] == 2 || ia_array2[4] == 3)
                {
                    Lib_Log.Log.writeLogException("读取标志位退出" + ia_array2[4].ToString());
                    Console.WriteLine("读取标志位退出");
                    //ClearSuccessState();//清除标志位
                    break;
                }
                else
                {
                    //判断是否有错误码
                    if (ia_array2[0] == 1)
                    {
                        int[] ia_errArray = new int[100];
                        int i_state = MyModbusFun.GetErrMsgNew(ref ia_errArray);
                        if (i_state != -1)
                        {
                            //插入报警信息
                            //先判断是否有结束错误，有就不显示询问项
                            for (int i = 0; i < ia_errArray.Length; i++)
                            {
                                //只有询问才显示在这里，异常统一在904为3时一起显示
                                if (ia_errArray[i] > 10000)
                                {
                                    if (SmartDyeing.FADM_Object.Communal._dic_errModbusNoNew.Keys.Contains(ia_errArray[i]))
                                    {

                                        //如果播报没存在，就加入
                                        if (!lis_data.Contains(i))
                                        {
                                            lis_data.Add(i);
                                            //循环判断是否已加入报警
                                            string s_message = SmartDyeing.FADM_Object.Communal._dic_errModbusNoNew[ia_errArray[i]];
                                            string s_insert = CardObject.InsertD(s_message, " GetReturn");
                                            //将对应报警记录下来，方便选择交互后重新写入对应位置
                                            Lib_Card.CardObject.prompt prompt = new Lib_Card.CardObject.prompt();
                                            prompt = Lib_Card.CardObject.keyValuePairs[s_insert];
                                            prompt.Count = i;
                                            Lib_Card.CardObject.keyValuePairs[s_insert] = prompt;

                                            lis_array.Add(s_insert);
                                        }
                                    }
                                    else
                                    {
                                        Lib_Log.Log.writeLogException("异常返回");
                                    }
                                }

                            }
                            for (int i = lis_array.Count - 1; i >= 0; i--)
                            {
                                int i_alarm_Choose = Lib_Card.CardObject.keyValuePairs[lis_array[i]].Choose;
                                int i_count = Lib_Card.CardObject.keyValuePairs[lis_array[i]].Count;
                                string s_info = Lib_Card.CardObject.keyValuePairs[lis_array[i]].Info;
                                if (i_alarm_Choose != 0)
                                {
                                    string s_sql = "INSERT INTO alarm_table" +
                                         "(MyDate,MyTime,AlarmHead,AlarmDetails)" +
                                         " VALUES( '" +
                                         String.Format("{0:d}", DateTime.Now) + "','" +
                                         String.Format("{0:T}", DateTime.Now) + "','GetReturn','" +
                                         s_info + "(" + (i_alarm_Choose == 1 ? "Yes" : "No") + ")');";

                                    FADM_Object.Communal._fadmSqlserver.ReviseData(s_sql);

                                    CardObject.DeleteD(lis_array[i]);
                                    lis_array.Remove(lis_array[i]);
                                    lis_data.Remove(i_count);
                                    if (i_alarm_Choose == 1)
                                    {
                                        Communal._b_isWriting = true;
                                    //写814地址
                                    lab814:
                                        int[] ia_array4 = { 1 };
                                        i_state = FADM_Object.Communal._tcpModBus.Write(3100 + i_count, ia_array4);
                                        if (i_state == -1)
                                            goto lab814;
                                        //清空903地址
                                        ia_array4[0] = 0;

                                        Thread.Sleep(200);
                                        Communal._b_isWriting = false;
                                    }
                                    else
                                    {
                                        Communal._b_isWriting = true;
                                    lab814:
                                        int[] ia_array4 = { 2 };
                                        i_state = FADM_Object.Communal._tcpModBus.Write(3100 + i_count, ia_array4);
                                        if (i_state == -1)
                                            goto lab814;
                                        //throw new Exception("-2");

                                        Thread.Sleep(200);
                                        Communal._b_isWriting = false;

                                    }
                                }
                            }

                        }
                    }
                }
                Console.WriteLine("tcp读取执行完成标志位" + ia_array2[4].ToString());
                if (ia_array2[4].ToString().Equals("0"))
                {
                    // Console.WriteLine(123);
                }
                Thread.Sleep(1);
            }
            if (ia_array2[4] == 2)
            {
                Lib_Log.Log.writeLogException("执行完成");
                if (FADM_Object.Communal._b_stop)
                {
                    FADM_Object.Communal._b_stop = false;
                    return -2;
                }
                Console.WriteLine("执行完成");
                //FADM_Object.Communal._fadmSqlserver.InsertRun("RobotHand", "回零完成");
            }
            else if (ia_array2[4] == 3)
            {
                Thread.Sleep(200);
                Lib_Log.Log.writeLogException("执行异常了,准备读取错误信息地址");
                Console.WriteLine("执行异常了,准备读取错误信息地址");

                throw new Exception("-2");//跳到外面异常 自己读取异常地址
            }
            return 0;
        }

        /// <summary>
        /// 操作
        /// </summary>
        /// <returns></returns>
        public static int Do()
        {
            try
            {
                bool b_istrue = false;
                FADM_Object.Communal.WriteTcpStatus(false); //天平先不要轮询 
                if (!FADM_Object.Communal._b_auto)
                {
                    for (int i = 0; i < Communal._ia_d900.Length; i++) { Communal._ia_d900[i] = 0; }
                    Communal._b_isUpdateNewData = false;
                    //等待调试页面停止刷新
                    Thread.Sleep(200);
                }

                //判断错误返回值
                if (GetReturn(1) == -2)
                {
                    return -2;
                }
                return 0;
            }
            catch (Exception e)
            {
                FADM_Object.Communal.WriteTcpStatus(true); //恢复
                if (e.Message.Equals("-2"))
                {
                    throw e;
                }
                else
                {
                    MessageBox.Show(e.ToString());
                    throw new Exception("-1");
                }
            }
            finally
            {
                FADM_Object.Communal.WriteTcpStatus(true); //恢复
            }
        }


        /// <summary>
        /// 回零
        /// </summary>
        /// <returns></returns>
        public static int goHome()
        {
            try
            {
                Lib_Log.Log.writeLogException("执行回零方法");

                bool b_istrue = false;
                FADM_Object.Communal.WriteTcpStatus(false); //天平先不要轮询 
                if (!FADM_Object.Communal._b_auto)
                {
                    for (int i = 0; i < Communal._ia_d900.Length; i++) { Communal._ia_d900[i] = 0; }
                    Communal._b_isUpdateNewData = false;
                    //等待调试页面停止刷新
                    Thread.Sleep(200);
                }
                ClearSuccessState();//先清除标志位
                Lib_Log.Log.writeLogException("清除标志位结束");

            lableTop:
                int[] ia_array = { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 };
                int i_state = FADM_Object.Communal._tcpModBus.Write(800, ia_array);
                if (i_state != -1)
                {
                    //判断错误返回值
                    if (GetReturn(1) == -2)
                    {
                        return -2;
                    }
                    else
                    {

                    }
                }
                else
                {
                    Lib_Log.Log.writeLogException("写入回零动作编号返回失败,继续写入");
                    Console.WriteLine("写入回零动作编号返回失败,继续写入");
                    if (b_istrue)
                    {
                        b_istrue = false;
                        FADM_Object.Communal._tcpModBus.ReConnect();
                        goto lableTop;
                    }
                    b_istrue = true;
                    goto lableTop;
                }
                return 0;
            }
            catch (Exception e)
            {
                FADM_Object.Communal.WriteTcpStatus(true); //恢复
                if (e.Message.Equals("-2"))
                {
                    throw e;
                }
                else
                {
                    MessageBox.Show(e.ToString());
                    throw new Exception("-1");
                }
            }
            finally
            {
                FADM_Object.Communal.WriteTcpStatus(true); //恢复
            }
        }

        /// <summary>
        /// 计算目标坐标
        /// </summary>
        /// <param name="i_move_Type">移动类型 0母液瓶 1缸杯位 2 天平位 3 待机位置 4 放盖区 5 泄压区</param>
        /// <param name="Type">0:气缸上到位 1：气缸中限位</param>
        /// <returns></returns>
        public static int CalTarget(int i_move_Type, int i_no, ref int i_x, ref int i_y)
        {
            try
            {
                Lib_Log.Log.writeLogException("计算坐标");
                Console.WriteLine("计算坐标");
                int i_xPules = 0;
                int i_yPules = 0;
                switch (i_move_Type)
                {
                    case 0:
                        if (0 >= i_no || i_no > Lib_Card.Configure.Parameter.Machine_Bottle_Total)
                        {
                            throw new Exception("4");
                        }
                        if (Lib_Card.Configure.Parameter.Other_IsOnlyDrip == 1 || (!Communal._b_isBalanceInDrip))
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_Bottle_X - (i_no - 1) %
                            Lib_Card.Configure.Parameter.Machine_Bottle_Column * Lib_Card.Configure.Parameter.Coordinate_Bottle_Interval;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_Bottle_Y + (i_no - 1) /
                                Lib_Card.Configure.Parameter.Machine_Bottle_Column * Lib_Card.Configure.Parameter.Coordinate_Bottle_Interval;
                        }
                        else
                        {
                            int iNo = i_no;
                            if (Lib_Card.Configure.Parameter.Machine_Bottle_Total - 14 >= iNo)
                            {
                                i_xPules = Lib_Card.Configure.Parameter.Coordinate_Bottle_X - (iNo - 1) %
                                    Lib_Card.Configure.Parameter.Machine_Bottle_Column * Lib_Card.Configure.Parameter.Coordinate_Bottle_Interval;
                                i_yPules = Lib_Card.Configure.Parameter.Coordinate_Bottle_Y + (iNo - 1) /
                                    Lib_Card.Configure.Parameter.Machine_Bottle_Column * Lib_Card.Configure.Parameter.Coordinate_Bottle_Interval;
                            }
                            else if (Lib_Card.Configure.Parameter.Machine_Bottle_Total - 7 >= iNo)
                            {
                                i_xPules = Lib_Card.Configure.Parameter.Coordinate_Bottle_X -
                                    ((iNo + 14 - Lib_Card.Configure.Parameter.Machine_Bottle_Total) % 8 + 2)
                                    * Lib_Card.Configure.Parameter.Coordinate_Bottle_Interval;
                                i_yPules = Lib_Card.Configure.Parameter.Coordinate_Bottle_Y +
                                    ((Lib_Card.Configure.Parameter.Machine_Bottle_Total - 14) /
                                    Lib_Card.Configure.Parameter.Machine_Bottle_Column +
                                    (iNo + 14 - Lib_Card.Configure.Parameter.Machine_Bottle_Total) / 8)
                                    * Lib_Card.Configure.Parameter.Coordinate_Bottle_Interval;


                            }
                            else
                            {
                                i_xPules = Lib_Card.Configure.Parameter.Coordinate_Bottle_X -
                                   ((iNo + 14 - Lib_Card.Configure.Parameter.Machine_Bottle_Total) % 8 + 3)
                                   * Lib_Card.Configure.Parameter.Coordinate_Bottle_Interval;
                                i_yPules = Lib_Card.Configure.Parameter.Coordinate_Bottle_Y +
                                    ((Lib_Card.Configure.Parameter.Machine_Bottle_Total - 14) /
                                    Lib_Card.Configure.Parameter.Machine_Bottle_Column +
                                    (iNo + 14 - Lib_Card.Configure.Parameter.Machine_Bottle_Total) / 8)
                                    * Lib_Card.Configure.Parameter.Coordinate_Bottle_Interval;
                            }
                        }
                        break;
                    case 1:
                        if (0 >= i_no || i_no > Lib_Card.Configure.Parameter.Machine_Cup_Total)
                        {
                            throw new Exception("4");
                        }
                        if (Lib_Card.Configure.Parameter.Other_IsOnlyDrip == 1)
                        {
                            //在区域1
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area1_X -
                                ((i_no - Lib_Card.Configure.Parameter.Machine_Area1_CupMin) % Lib_Card.Configure.Parameter.Machine_Area1_Row) * Lib_Card.Configure.Parameter.Coordinate_Area1_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area1_Y -
                                ((i_no - Lib_Card.Configure.Parameter.Machine_Area1_CupMin) / Lib_Card.Configure.Parameter.Machine_Area1_Row) * Lib_Card.Configure.Parameter.Coordinate_Area1_IntervalY;

                        }
                        else
                        {
                            if (SmartDyeing.FADM_Object.Communal._lis_dyeCupNum.Contains(i_no) && SmartDyeing.FADM_Object.Communal._dic_dyeType[i_no] == 1)
                            {
                                if (i_no == 1)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup1_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup1_IntervalY;
                                }
                                else if (i_no == 2)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup2_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup2_IntervalY;
                                }
                                else if (i_no == 3)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup3_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup3_IntervalY;
                                }
                                else if (i_no == 4)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup4_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup4_IntervalY;
                                }
                                else if (i_no == 5)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup5_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup5_IntervalY;
                                }
                                else if (i_no == 6)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup6_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup6_IntervalY;
                                }
                                else if (i_no == 7)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup7_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup7_IntervalY;
                                }
                                else if (i_no == 8)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup8_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup8_IntervalY;
                                }
                                else if (i_no == 9)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup9_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup9_IntervalY;
                                }
                                else if (i_no == 10)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup10_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup10_IntervalY;
                                }
                                else if (i_no == 11)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup11_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup11_IntervalY;
                                }
                                else if (i_no == 12)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup12_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup12_IntervalY;
                                }
                                else if (i_no == 13)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup13_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup13_IntervalY;
                                }
                                else if (i_no == 14)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup14_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup14_IntervalY;
                                }
                                else if (i_no == 15)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup15_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup15_IntervalY;
                                }
                                else if (i_no == 16)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup16_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup16_IntervalY;
                                }
                                else if (i_no == 17)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup17_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup17_IntervalY;
                                }
                                else if (i_no == 18)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup18_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup18_IntervalY;
                                }
                                else if (i_no == 19)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup19_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup19_IntervalY;
                                }
                                else if (i_no == 20)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup20_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup20_IntervalY;
                                }
                                else if (i_no == 21)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup21_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup21_IntervalY;
                                }
                                else if (i_no == 22)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup22_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup22_IntervalY;
                                }
                                else if (i_no == 23)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup23_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup23_IntervalY;
                                }
                                else if (i_no == 24)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup24_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup24_IntervalY;
                                }
                                else if (i_no == 25)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup25_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup25_IntervalY;
                                }
                                else if (i_no == 26)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup26_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup26_IntervalY;
                                }
                                else if (i_no == 27)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup27_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup27_IntervalY;
                                }
                                else if (i_no == 28)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup28_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup28_IntervalY;
                                }
                                else if (i_no == 29)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup29_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup29_IntervalY;
                                }
                                else if (i_no == 30)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup30_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup30_IntervalY;
                                }
                                else if (i_no == 31)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup31_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup31_IntervalY;
                                }
                                else if (i_no == 32)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup32_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup32_IntervalY;
                                }
                                else if (i_no == 33)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup33_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup33_IntervalY;
                                }
                                else if (i_no == 34)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup34_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup34_IntervalY;
                                }
                                else if (i_no == 35)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup35_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup35_IntervalY;
                                }
                                else if (i_no == 36)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup36_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup36_IntervalY;
                                }
                            }
                            else
                            {
                                if (i_no >= Lib_Card.Configure.Parameter.Machine_Area1_CupMin && i_no <= Lib_Card.Configure.Parameter.Machine_Area1_CupMax)
                                {
                                    if (Lib_Card.Configure.Parameter.Machine_Area1_Layout == 1)
                                    {
                                        //在区域1
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area1_X -
                                            ((i_no - Lib_Card.Configure.Parameter.Machine_Area1_CupMin) / Lib_Card.Configure.Parameter.Machine_Area1_Row) * Lib_Card.Configure.Parameter.Coordinate_Area1_IntervalX;

                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area1_Y +
                                            ((i_no - Lib_Card.Configure.Parameter.Machine_Area1_CupMin) % Lib_Card.Configure.Parameter.Machine_Area1_Row) * Lib_Card.Configure.Parameter.Coordinate_Area1_IntervalY;

                                    }
                                    else
                                    {
                                        //在区域1
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area1_X -
                                            ((i_no - Lib_Card.Configure.Parameter.Machine_Area1_CupMin) % Lib_Card.Configure.Parameter.Machine_Area1_Col) * Lib_Card.Configure.Parameter.Coordinate_Area1_IntervalX;
                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area1_Y -
                                            ((i_no - Lib_Card.Configure.Parameter.Machine_Area1_CupMin) / Lib_Card.Configure.Parameter.Machine_Area1_Col) * Lib_Card.Configure.Parameter.Coordinate_Area1_IntervalY;
                                    }
                                }
                                else if (i_no >= Lib_Card.Configure.Parameter.Machine_Area2_CupMin && i_no <= Lib_Card.Configure.Parameter.Machine_Area2_CupMax)
                                {
                                    if (Lib_Card.Configure.Parameter.Machine_Area2_Layout == 1)
                                    {
                                        //在区域2
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area2_X -
                                      ((i_no - Lib_Card.Configure.Parameter.Machine_Area2_CupMin) / Lib_Card.Configure.Parameter.Machine_Area2_Row) * Lib_Card.Configure.Parameter.Coordinate_Area2_IntervalX;

                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area2_Y +
                                        ((i_no - Lib_Card.Configure.Parameter.Machine_Area2_CupMin) % Lib_Card.Configure.Parameter.Machine_Area2_Row) * Lib_Card.Configure.Parameter.Coordinate_Area2_IntervalY;

                                    }
                                    else
                                    {
                                        //在区域2
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area2_X -
                                          ((i_no - Lib_Card.Configure.Parameter.Machine_Area2_CupMin) % Lib_Card.Configure.Parameter.Machine_Area2_Col) * Lib_Card.Configure.Parameter.Coordinate_Area2_IntervalX;

                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area2_Y -
                                        ((i_no - Lib_Card.Configure.Parameter.Machine_Area2_CupMin) / Lib_Card.Configure.Parameter.Machine_Area2_Col) * Lib_Card.Configure.Parameter.Coordinate_Area2_IntervalY;
                                    }
                                }
                                else if (i_no >= Lib_Card.Configure.Parameter.Machine_Area3_CupMin && i_no <= Lib_Card.Configure.Parameter.Machine_Area3_CupMax)
                                {
                                    if (Lib_Card.Configure.Parameter.Machine_Area3_Layout == 1)
                                    {
                                        //在区域3
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area3_X -
                                    ((i_no - Lib_Card.Configure.Parameter.Machine_Area3_CupMin) / Lib_Card.Configure.Parameter.Machine_Area3_Row) * Lib_Card.Configure.Parameter.Coordinate_Area3_IntervalX;

                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area3_Y +
                                        ((i_no - Lib_Card.Configure.Parameter.Machine_Area3_CupMin) % Lib_Card.Configure.Parameter.Machine_Area3_Row) * Lib_Card.Configure.Parameter.Coordinate_Area3_IntervalY;

                                    }
                                    else
                                    {
                                        //在区域3
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area3_X -
                                          ((i_no - Lib_Card.Configure.Parameter.Machine_Area3_CupMin) % Lib_Card.Configure.Parameter.Machine_Area3_Col) * Lib_Card.Configure.Parameter.Coordinate_Area3_IntervalX;

                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area3_Y -
                                        ((i_no - Lib_Card.Configure.Parameter.Machine_Area3_CupMin) / Lib_Card.Configure.Parameter.Machine_Area3_Col) * Lib_Card.Configure.Parameter.Coordinate_Area3_IntervalY;
                                    }
                                }
                                else if (i_no >= Lib_Card.Configure.Parameter.Machine_Area4_CupMin && i_no <= Lib_Card.Configure.Parameter.Machine_Area4_CupMax)
                                {
                                    if (Lib_Card.Configure.Parameter.Machine_Area4_Layout == 1)
                                    {
                                        //在区域4
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area4_X -
                                    ((i_no - Lib_Card.Configure.Parameter.Machine_Area4_CupMin) / Lib_Card.Configure.Parameter.Machine_Area4_Row) * Lib_Card.Configure.Parameter.Coordinate_Area4_IntervalX;

                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area4_Y +
                                        ((i_no - Lib_Card.Configure.Parameter.Machine_Area4_CupMin) % Lib_Card.Configure.Parameter.Machine_Area4_Row) * Lib_Card.Configure.Parameter.Coordinate_Area4_IntervalY;

                                    }
                                    else
                                    {
                                        //在区域4
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area4_X +
                                          ((i_no - Lib_Card.Configure.Parameter.Machine_Area4_CupMin) % Lib_Card.Configure.Parameter.Machine_Area4_Col) * Lib_Card.Configure.Parameter.Coordinate_Area4_IntervalX;

                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area4_Y +
                                        ((i_no - Lib_Card.Configure.Parameter.Machine_Area4_CupMin) / Lib_Card.Configure.Parameter.Machine_Area4_Col) * Lib_Card.Configure.Parameter.Coordinate_Area4_IntervalY;
                                    }
                                }
                                else if (i_no >= Lib_Card.Configure.Parameter.Machine_Area5_CupMin && i_no <= Lib_Card.Configure.Parameter.Machine_Area5_CupMax)
                                {
                                    if (Lib_Card.Configure.Parameter.Machine_Area5_Layout == 1)
                                    {
                                        //在区域5
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area5_X -
                                    ((i_no - Lib_Card.Configure.Parameter.Machine_Area5_CupMin) / Lib_Card.Configure.Parameter.Machine_Area5_Row) * Lib_Card.Configure.Parameter.Coordinate_Area5_IntervalX;

                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area5_Y +
                                        ((i_no - Lib_Card.Configure.Parameter.Machine_Area5_CupMin) % Lib_Card.Configure.Parameter.Machine_Area5_Row) * Lib_Card.Configure.Parameter.Coordinate_Area5_IntervalY;
                                    }
                                    else
                                    {
                                        //在区域5
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area5_X +
                                      ((i_no - Lib_Card.Configure.Parameter.Machine_Area5_CupMin) % Lib_Card.Configure.Parameter.Machine_Area5_Col) * Lib_Card.Configure.Parameter.Coordinate_Area5_IntervalX;

                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area5_Y +
                                        ((i_no - Lib_Card.Configure.Parameter.Machine_Area5_CupMin) / Lib_Card.Configure.Parameter.Machine_Area5_Col) * Lib_Card.Configure.Parameter.Coordinate_Area5_IntervalY;
                                    }
                                }
                                else
                                {
                                    if (Lib_Card.Configure.Parameter.Machine_Area6_Layout == 1)
                                    {
                                        //在区域6
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area6_X -
                                    ((i_no - Lib_Card.Configure.Parameter.Machine_Area6_CupMin) / Lib_Card.Configure.Parameter.Machine_Area6_Row) * Lib_Card.Configure.Parameter.Coordinate_Area6_IntervalX;

                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area6_Y +
                                        ((i_no - Lib_Card.Configure.Parameter.Machine_Area6_CupMin) % Lib_Card.Configure.Parameter.Machine_Area6_Row) * Lib_Card.Configure.Parameter.Coordinate_Area6_IntervalY;
                                    }
                                    else
                                    {
                                        //在区域6
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area6_X +
                                     ((i_no - Lib_Card.Configure.Parameter.Machine_Area6_CupMin) % Lib_Card.Configure.Parameter.Machine_Area6_Col) * Lib_Card.Configure.Parameter.Coordinate_Area6_IntervalX;
                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area6_Y +
                                        ((i_no - Lib_Card.Configure.Parameter.Machine_Area6_CupMin) / Lib_Card.Configure.Parameter.Machine_Area6_Col) * Lib_Card.Configure.Parameter.Coordinate_Area6_IntervalY;
                                    }

                                }
                            }
                        }
                        break;

                    case 2:
                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Balance_X;
                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Balance_Y;
                        break;
                    case 3:
                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Standby_X;
                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Standby_Y;
                        break;
                    case 4:
                        if (0 >= i_no || i_no > Lib_Card.Configure.Parameter.Machine_Cup_Total)
                        {
                            throw new Exception("4");
                        }
                        if (i_no == 1)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover1_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover1_IntervalY;
                        }
                        else if (i_no == 2)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover2_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover2_IntervalY;
                        }
                        else if (i_no == 3)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover3_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover3_IntervalY;
                        }
                        else if (i_no == 4)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover4_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover4_IntervalY;
                        }
                        else if (i_no == 5)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover5_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover5_IntervalY;
                        }
                        else if (i_no == 6)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover6_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover6_IntervalY;
                        }
                        else if (i_no == 7)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover7_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover7_IntervalY;
                        }
                        else if (i_no == 8)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover8_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover8_IntervalY;
                        }
                        else if (i_no == 9)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover9_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover9_IntervalY;
                        }
                        else if (i_no == 10)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover10_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover10_IntervalY;
                        }
                        else if (i_no == 11)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover11_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover11_IntervalY;
                        }
                        else if (i_no == 12)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover12_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover12_IntervalY;
                        }
                        else if (i_no == 13)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover13_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover13_IntervalY;
                        }
                        else if (i_no == 14)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover14_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover14_IntervalY;
                        }
                        else if (i_no == 15)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover15_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover15_IntervalY;
                        }
                        else if (i_no == 16)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover16_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover16_IntervalY;
                        }
                        else if (i_no == 17)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover17_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover17_IntervalY;
                        }
                        else if (i_no == 18)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover18_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover18_IntervalY;
                        }
                        else if (i_no == 19)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover19_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover19_IntervalY;
                        }
                        else if (i_no == 20)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover20_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover20_IntervalY;
                        }
                        else if (i_no == 21)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover21_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover21_IntervalY;
                        }
                        else if (i_no == 22)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover22_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover22_IntervalY;
                        }
                        else if (i_no == 23)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover23_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover23_IntervalY;
                        }
                        else if (i_no == 24)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover24_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover24_IntervalY;
                        }
                        else if (i_no == 25)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover25_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover25_IntervalY;
                        }
                        else if (i_no == 26)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover26_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover26_IntervalY;
                        }
                        else if (i_no == 27)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover27_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover27_IntervalY;
                        }
                        else if (i_no == 28)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover28_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover28_IntervalY;
                        }
                        else if (i_no == 29)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover29_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover29_IntervalY;
                        }
                        else if (i_no == 30)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover30_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover30_IntervalY;
                        }
                        else if (i_no == 31)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover31_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover31_IntervalY;
                        }
                        else if (i_no == 32)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover32_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover32_IntervalY;
                        }
                        else if (i_no == 33)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover33_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover33_IntervalY;
                        }
                        else if (i_no == 34)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover34_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover34_IntervalY;
                        }
                        else if (i_no == 35)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover35_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover35_IntervalY;
                        }
                        else if (i_no == 36)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover36_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover36_IntervalY;
                        }
                        break;
                    case 5:
                        if (0 >= i_no || i_no > Lib_Card.Configure.Parameter.Machine_Cup_Total)
                        {
                            throw new Exception("4");
                        }
                        if (Lib_Card.Configure.Parameter.Other_IsOnlyDrip == 1)
                        {
                            //在区域1
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area1_X -
                                ((i_no - Lib_Card.Configure.Parameter.Machine_Area1_CupMin) % Lib_Card.Configure.Parameter.Machine_Area1_Row) * Lib_Card.Configure.Parameter.Coordinate_Area1_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area1_Y -
                                ((i_no - Lib_Card.Configure.Parameter.Machine_Area1_CupMin) / Lib_Card.Configure.Parameter.Machine_Area1_Row) * Lib_Card.Configure.Parameter.Coordinate_Area1_IntervalY;

                        }
                        else
                        {
                            if (SmartDyeing.FADM_Object.Communal._lis_dyeCupNum.Contains(i_no) && SmartDyeing.FADM_Object.Communal._dic_dyeType[i_no] == 1)
                            {
                                if (i_no == 1)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup1_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup1_IntervalY;
                                }
                                else if (i_no == 2)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup2_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup2_IntervalY;
                                }
                                else if (i_no == 3)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup3_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup3_IntervalY;
                                }
                                else if (i_no == 4)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup4_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup4_IntervalY;
                                }
                                else if (i_no == 5)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup5_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup5_IntervalY;
                                }
                                else if (i_no == 6)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup6_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup6_IntervalY;
                                }
                                else if (i_no == 7)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup7_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup7_IntervalY;
                                }
                                else if (i_no == 8)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup8_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup8_IntervalY;
                                }
                                else if (i_no == 9)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup9_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup9_IntervalY;
                                }
                                else if (i_no == 10)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup10_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup10_IntervalY;
                                }
                                else if (i_no == 11)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup11_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup11_IntervalY;
                                }
                                else if (i_no == 12)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup12_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup12_IntervalY;
                                }
                                else if (i_no == 13)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup13_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup13_IntervalY;
                                }
                                else if (i_no == 14)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup14_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup14_IntervalY;
                                }
                                else if (i_no == 15)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup15_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup15_IntervalY;
                                }
                                else if (i_no == 16)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup16_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup16_IntervalY;
                                }
                                else if (i_no == 17)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup17_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup17_IntervalY;
                                }
                                else if (i_no == 18)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup18_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup18_IntervalY;
                                }
                                else if (i_no == 19)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup19_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup19_IntervalY;
                                }
                                else if (i_no == 20)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup20_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup20_IntervalY;
                                }
                                else if (i_no == 21)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup21_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup21_IntervalY;
                                }
                                else if (i_no == 22)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup22_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup22_IntervalY;
                                }
                                else if (i_no == 23)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup23_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup23_IntervalY;
                                }
                                else if (i_no == 24)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup24_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup24_IntervalY;
                                }
                                else if (i_no == 25)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup25_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup25_IntervalY;
                                }
                                else if (i_no == 26)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup26_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup26_IntervalY;
                                }
                                else if (i_no == 27)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup27_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup27_IntervalY;
                                }
                                else if (i_no == 28)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup28_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup28_IntervalY;
                                }
                                else if (i_no == 29)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup29_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup29_IntervalY;
                                }
                                else if (i_no == 30)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup30_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup30_IntervalY;
                                }
                                else if (i_no == 31)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup31_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup31_IntervalY;
                                }
                                else if (i_no == 32)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup32_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup32_IntervalY;
                                }
                                else if (i_no == 33)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup33_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup33_IntervalY;
                                }
                                else if (i_no == 34)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup34_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup34_IntervalY;
                                }
                                else if (i_no == 35)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup35_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup35_IntervalY;
                                }
                                else if (i_no == 36)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup36_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup36_IntervalY;
                                }
                            }
                            else
                            {
                                if (i_no >= Lib_Card.Configure.Parameter.Machine_Area1_CupMin && i_no <= Lib_Card.Configure.Parameter.Machine_Area1_CupMax)
                                {
                                    if (Lib_Card.Configure.Parameter.Machine_Area1_Layout == 1)
                                    {
                                        //在区域1
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area1_X -
                                            ((i_no - Lib_Card.Configure.Parameter.Machine_Area1_CupMin) / Lib_Card.Configure.Parameter.Machine_Area1_Row) * Lib_Card.Configure.Parameter.Coordinate_Area1_IntervalX;

                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area1_Y +
                                            ((i_no - Lib_Card.Configure.Parameter.Machine_Area1_CupMin) % Lib_Card.Configure.Parameter.Machine_Area1_Row) * Lib_Card.Configure.Parameter.Coordinate_Area1_IntervalY;

                                    }
                                    else
                                    {
                                        //在区域1
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area1_X -
                                            ((i_no - Lib_Card.Configure.Parameter.Machine_Area1_CupMin) % Lib_Card.Configure.Parameter.Machine_Area1_Col) * Lib_Card.Configure.Parameter.Coordinate_Area1_IntervalX;
                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area1_Y -
                                            ((i_no - Lib_Card.Configure.Parameter.Machine_Area1_CupMin) / Lib_Card.Configure.Parameter.Machine_Area1_Col) * Lib_Card.Configure.Parameter.Coordinate_Area1_IntervalY;
                                    }
                                }
                                else if (i_no >= Lib_Card.Configure.Parameter.Machine_Area2_CupMin && i_no <= Lib_Card.Configure.Parameter.Machine_Area2_CupMax)
                                {
                                    if (Lib_Card.Configure.Parameter.Machine_Area2_Layout == 1)
                                    {
                                        //在区域2
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area2_X -
                                      ((i_no - Lib_Card.Configure.Parameter.Machine_Area2_CupMin) / Lib_Card.Configure.Parameter.Machine_Area2_Row) * Lib_Card.Configure.Parameter.Coordinate_Area2_IntervalX;

                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area2_Y +
                                        ((i_no - Lib_Card.Configure.Parameter.Machine_Area2_CupMin) % Lib_Card.Configure.Parameter.Machine_Area2_Row) * Lib_Card.Configure.Parameter.Coordinate_Area2_IntervalY;

                                    }
                                    else
                                    {
                                        //在区域2
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area2_X -
                                          ((i_no - Lib_Card.Configure.Parameter.Machine_Area2_CupMin) % Lib_Card.Configure.Parameter.Machine_Area2_Col) * Lib_Card.Configure.Parameter.Coordinate_Area2_IntervalX;

                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area2_Y -
                                        ((i_no - Lib_Card.Configure.Parameter.Machine_Area2_CupMin) / Lib_Card.Configure.Parameter.Machine_Area2_Col) * Lib_Card.Configure.Parameter.Coordinate_Area2_IntervalY;
                                    }
                                }
                                else if (i_no >= Lib_Card.Configure.Parameter.Machine_Area3_CupMin && i_no <= Lib_Card.Configure.Parameter.Machine_Area3_CupMax)
                                {
                                    if (Lib_Card.Configure.Parameter.Machine_Area3_Layout == 1)
                                    {
                                        //在区域3
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area3_X -
                                    ((i_no - Lib_Card.Configure.Parameter.Machine_Area3_CupMin) / Lib_Card.Configure.Parameter.Machine_Area3_Row) * Lib_Card.Configure.Parameter.Coordinate_Area3_IntervalX;

                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area3_Y +
                                        ((i_no - Lib_Card.Configure.Parameter.Machine_Area3_CupMin) % Lib_Card.Configure.Parameter.Machine_Area3_Row) * Lib_Card.Configure.Parameter.Coordinate_Area3_IntervalY;

                                    }
                                    else
                                    {
                                        //在区域3
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area3_X -
                                          ((i_no - Lib_Card.Configure.Parameter.Machine_Area3_CupMin) % Lib_Card.Configure.Parameter.Machine_Area3_Col) * Lib_Card.Configure.Parameter.Coordinate_Area3_IntervalX;

                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area3_Y -
                                        ((i_no - Lib_Card.Configure.Parameter.Machine_Area3_CupMin) / Lib_Card.Configure.Parameter.Machine_Area3_Col) * Lib_Card.Configure.Parameter.Coordinate_Area3_IntervalY;
                                    }
                                }
                                else if (i_no >= Lib_Card.Configure.Parameter.Machine_Area4_CupMin && i_no <= Lib_Card.Configure.Parameter.Machine_Area4_CupMax)
                                {
                                    if (Lib_Card.Configure.Parameter.Machine_Area4_Layout == 1)
                                    {
                                        //在区域4
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area4_X -
                                    ((i_no - Lib_Card.Configure.Parameter.Machine_Area4_CupMin) / Lib_Card.Configure.Parameter.Machine_Area4_Row) * Lib_Card.Configure.Parameter.Coordinate_Area4_IntervalX;

                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area4_Y +
                                        ((i_no - Lib_Card.Configure.Parameter.Machine_Area4_CupMin) % Lib_Card.Configure.Parameter.Machine_Area4_Row) * Lib_Card.Configure.Parameter.Coordinate_Area4_IntervalY;

                                    }
                                    else
                                    {
                                        //在区域4
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area4_X +
                                          ((i_no - Lib_Card.Configure.Parameter.Machine_Area4_CupMin) % Lib_Card.Configure.Parameter.Machine_Area4_Col) * Lib_Card.Configure.Parameter.Coordinate_Area4_IntervalX;

                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area4_Y +
                                        ((i_no - Lib_Card.Configure.Parameter.Machine_Area4_CupMin) / Lib_Card.Configure.Parameter.Machine_Area4_Col) * Lib_Card.Configure.Parameter.Coordinate_Area4_IntervalY;
                                    }
                                }
                                else if (i_no >= Lib_Card.Configure.Parameter.Machine_Area5_CupMin && i_no <= Lib_Card.Configure.Parameter.Machine_Area5_CupMax)
                                {
                                    if (Lib_Card.Configure.Parameter.Machine_Area5_Layout == 1)
                                    {
                                        //在区域5
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area5_X -
                                    ((i_no - Lib_Card.Configure.Parameter.Machine_Area5_CupMin) / Lib_Card.Configure.Parameter.Machine_Area5_Row) * Lib_Card.Configure.Parameter.Coordinate_Area5_IntervalX;

                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area5_Y +
                                        ((i_no - Lib_Card.Configure.Parameter.Machine_Area5_CupMin) % Lib_Card.Configure.Parameter.Machine_Area5_Row) * Lib_Card.Configure.Parameter.Coordinate_Area5_IntervalY;
                                    }
                                    else
                                    {
                                        //在区域5
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area5_X +
                                      ((i_no - Lib_Card.Configure.Parameter.Machine_Area5_CupMin) % Lib_Card.Configure.Parameter.Machine_Area5_Col) * Lib_Card.Configure.Parameter.Coordinate_Area5_IntervalX;

                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area5_Y +
                                        ((i_no - Lib_Card.Configure.Parameter.Machine_Area5_CupMin) / Lib_Card.Configure.Parameter.Machine_Area5_Col) * Lib_Card.Configure.Parameter.Coordinate_Area5_IntervalY;
                                    }
                                }
                                else
                                {
                                    if (Lib_Card.Configure.Parameter.Machine_Area6_Layout == 1)
                                    {
                                        //在区域6
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area6_X -
                                    ((i_no - Lib_Card.Configure.Parameter.Machine_Area6_CupMin) / Lib_Card.Configure.Parameter.Machine_Area6_Row) * Lib_Card.Configure.Parameter.Coordinate_Area6_IntervalX;

                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area6_Y +
                                        ((i_no - Lib_Card.Configure.Parameter.Machine_Area6_CupMin) % Lib_Card.Configure.Parameter.Machine_Area6_Row) * Lib_Card.Configure.Parameter.Coordinate_Area6_IntervalY;
                                    }
                                    else
                                    {
                                        //在区域6
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area6_X +
                                     ((i_no - Lib_Card.Configure.Parameter.Machine_Area6_CupMin) % Lib_Card.Configure.Parameter.Machine_Area6_Col) * Lib_Card.Configure.Parameter.Coordinate_Area6_IntervalX;
                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area6_Y +
                                        ((i_no - Lib_Card.Configure.Parameter.Machine_Area6_CupMin) / Lib_Card.Configure.Parameter.Machine_Area6_Col) * Lib_Card.Configure.Parameter.Coordinate_Area6_IntervalY;
                                    }

                                }
                            }
                        }
                        i_xPules -= Lib_Card.Configure.Parameter.Coordinate_Cup_Decompression;
                        break;
                    case 6:
                        if (0 >= i_no || i_no > Lib_Card.Configure.Parameter.Machine_Cup_Total)
                        {
                            throw new Exception("4");
                        }
                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_AreaDryCloth_X - (i_no - 1) %
                        Lib_Card.Configure.Parameter.Machine_AreaDryCloth_Col * Lib_Card.Configure.Parameter.Coordinate_AreaDryCloth_IntervalX;
                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_AreaDryCloth_Y + (i_no - 1) /
                            Lib_Card.Configure.Parameter.Machine_AreaDryCloth_Col * Lib_Card.Configure.Parameter.Coordinate_AreaDryCloth_IntervalY;

                        break;
                    case 7:
                        if (0 >= i_no || i_no > Lib_Card.Configure.Parameter.Machine_Cup_Total)
                        {
                            throw new Exception("4");
                        }
                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_AreaWetCloth_X - (i_no - 1) %
                        Lib_Card.Configure.Parameter.Machine_AreaWetCloth_Col * Lib_Card.Configure.Parameter.Coordinate_AreaWetCloth_IntervalX;
                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_AreaWetCloth_Y + (i_no - 1) /
                            Lib_Card.Configure.Parameter.Machine_AreaWetCloth_Col * Lib_Card.Configure.Parameter.Coordinate_AreaWetCloth_IntervalY;

                        break;
                    case 8:
                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_DryClamp_X;
                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_DryClamp_Y;
                        break;
                    case 9:
                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_WetClamp_X;
                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_WetClamp_Y;
                        break;
                    default:
                        throw new Exception("5");
                }
                Lib_Log.Log.writeLogException("计算脉冲是iXPules=" + i_xPules.ToString() + "计算iYPules=" + i_yPules.ToString());
                Console.WriteLine("计算脉冲是iXPules=" + i_xPules.ToString() + "计算iYPules=" + i_yPules.ToString());
                //int d_1 = 0;
                //if (i_xPules > 65536)
                //{
                //    d_1 = i_xPules / 65536;
                //    i_xPules = i_xPules % 65536;
                //}
                //int d_2 = 0;
                //if (i_yPules > 65536)
                //{
                //    d_2 = i_yPules / 65536;
                //    i_yPules = i_yPules % 65536;
                //}
                i_x = i_xPules;
                i_y = i_yPules;


                return 0;
            }
            catch (Exception e)
            {
                FADM_Object.Communal.WriteTcpStatus(true); //恢复
                if (e.Message.Equals("4") || e.Message.Equals("5") || e.Message.Equals("-2") || e.Message.Equals("6"))
                {
                    throw e;
                }
                else
                {
                    MessageBox.Show(e.ToString());
                    throw new Exception("-1");
                }
            }
            finally
            {
                FADM_Object.Communal.WriteTcpStatus(true); //恢复
            }
        }

        /// <summary>
        /// 定点移动
        /// </summary>
        /// <param name="i_move_Type">移动类型 0母液瓶 1缸杯位 2 天平位 3 待机位置 4 放盖区 5 泄压区 6:干布区域 7:湿布区域 8:干布夹子 9:湿布夹子</param>
        /// <param name="i_type">0:气缸上到位 1：气缸中限位</param>
        /// <returns></returns>
        public static int TargetMove(int i_move_Type, int i_no, int i_type)
        {
            try
            {
                Lib_Log.Log.writeLogException("开始定点移动");
                Console.WriteLine("开始定点移动");
                bool b_istrue = false;
                FADM_Object.Communal.WriteTcpStatus(false); //天平先不要轮询
                if (!FADM_Object.Communal._b_auto)
                {
                    for (int i = 0; i < Communal._ia_d900.Length; i++) { Communal._ia_d900[i] = 0; }
                    Communal._b_isUpdateNewData = false;
                    //等待调试页面停止刷新
                    Thread.Sleep(200);
                }
                ClearSuccessState();//先清除标志位

                Console.WriteLine("清除完标志位");
                Lib_Log.Log.writeLogException("清除标志位结束");
            lableTop:
            Label1:
                /*if (!FADM_Object.Communal._b_auto) {//在手动页面 等待手动页面退出
                    goto Label1;
                }*/
                int i_xPules = 0;
                int i_yPules = 0;
                switch (i_move_Type)
                {
                    case 0:
                        if (0 >= i_no || i_no > Lib_Card.Configure.Parameter.Machine_Bottle_Total)
                        {
                            throw new Exception("4");
                        }
                        if (Lib_Card.Configure.Parameter.Other_IsOnlyDrip == 1 || (!Communal._b_isBalanceInDrip))
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_Bottle_X - (i_no - 1) %
                            Lib_Card.Configure.Parameter.Machine_Bottle_Column * Lib_Card.Configure.Parameter.Coordinate_Bottle_Interval;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_Bottle_Y + (i_no - 1) /
                                Lib_Card.Configure.Parameter.Machine_Bottle_Column * Lib_Card.Configure.Parameter.Coordinate_Bottle_Interval;
                        }
                        else
                        {
                            int iNo = i_no;
                            if (Lib_Card.Configure.Parameter.Machine_Bottle_Total - 14 >= iNo)
                            {
                                i_xPules = Lib_Card.Configure.Parameter.Coordinate_Bottle_X - (iNo - 1) %
                                    Lib_Card.Configure.Parameter.Machine_Bottle_Column * Lib_Card.Configure.Parameter.Coordinate_Bottle_Interval;
                                i_yPules = Lib_Card.Configure.Parameter.Coordinate_Bottle_Y + (iNo - 1) /
                                    Lib_Card.Configure.Parameter.Machine_Bottle_Column * Lib_Card.Configure.Parameter.Coordinate_Bottle_Interval;
                            }
                            else if (Lib_Card.Configure.Parameter.Machine_Bottle_Total - 7 >= iNo)
                            {
                                i_xPules = Lib_Card.Configure.Parameter.Coordinate_Bottle_X -
                                    ((iNo + 14 - Lib_Card.Configure.Parameter.Machine_Bottle_Total) % 8 + 2)
                                    * Lib_Card.Configure.Parameter.Coordinate_Bottle_Interval;
                                i_yPules = Lib_Card.Configure.Parameter.Coordinate_Bottle_Y +
                                    ((Lib_Card.Configure.Parameter.Machine_Bottle_Total - 14) /
                                    Lib_Card.Configure.Parameter.Machine_Bottle_Column +
                                    (iNo + 14 - Lib_Card.Configure.Parameter.Machine_Bottle_Total) / 8)
                                    * Lib_Card.Configure.Parameter.Coordinate_Bottle_Interval;


                            }
                            else
                            {
                                i_xPules = Lib_Card.Configure.Parameter.Coordinate_Bottle_X -
                                   ((iNo + 14 - Lib_Card.Configure.Parameter.Machine_Bottle_Total) % 8 + 3)
                                   * Lib_Card.Configure.Parameter.Coordinate_Bottle_Interval;
                                i_yPules = Lib_Card.Configure.Parameter.Coordinate_Bottle_Y +
                                    ((Lib_Card.Configure.Parameter.Machine_Bottle_Total - 14) /
                                    Lib_Card.Configure.Parameter.Machine_Bottle_Column +
                                    (iNo + 14 - Lib_Card.Configure.Parameter.Machine_Bottle_Total) / 8)
                                    * Lib_Card.Configure.Parameter.Coordinate_Bottle_Interval;
                            }
                        }
                        break;
                    case 1:
                        if (0 >= i_no || i_no > Lib_Card.Configure.Parameter.Machine_Cup_Total)
                        {
                            throw new Exception("4");
                        }
                        if (Lib_Card.Configure.Parameter.Other_IsOnlyDrip == 1)
                        {
                            //在区域1
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area1_X -
                                ((i_no - Lib_Card.Configure.Parameter.Machine_Area1_CupMin) % Lib_Card.Configure.Parameter.Machine_Area1_Row) * Lib_Card.Configure.Parameter.Coordinate_Area1_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area1_Y -
                                ((i_no - Lib_Card.Configure.Parameter.Machine_Area1_CupMin) / Lib_Card.Configure.Parameter.Machine_Area1_Row) * Lib_Card.Configure.Parameter.Coordinate_Area1_IntervalY;

                        }
                        else
                        {
                            if (SmartDyeing.FADM_Object.Communal._lis_dyeCupNum.Contains(i_no) && SmartDyeing.FADM_Object.Communal._dic_dyeType[i_no] == 1)
                            {
                                if (i_no == 1)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup1_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup1_IntervalY;
                                }
                                else if (i_no == 2)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup2_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup2_IntervalY;
                                }
                                else if (i_no == 3)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup3_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup3_IntervalY;
                                }
                                else if (i_no == 4)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup4_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup4_IntervalY;
                                }
                                else if (i_no == 5)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup5_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup5_IntervalY;
                                }
                                else if (i_no == 6)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup6_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup6_IntervalY;
                                }
                                else if (i_no == 7)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup7_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup7_IntervalY;
                                }
                                else if (i_no == 8)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup8_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup8_IntervalY;
                                }
                                else if (i_no == 9)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup9_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup9_IntervalY;
                                }
                                else if (i_no == 10)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup10_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup10_IntervalY;
                                }
                                else if (i_no == 11)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup11_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup11_IntervalY;
                                }
                                else if (i_no == 12)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup12_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup12_IntervalY;
                                }
                                else if (i_no == 13)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup13_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup13_IntervalY;
                                }
                                else if (i_no == 14)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup14_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup14_IntervalY;
                                }
                                else if (i_no == 15)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup15_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup15_IntervalY;
                                }
                                else if (i_no == 16)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup16_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup16_IntervalY;
                                }
                                else if (i_no == 17)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup17_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup17_IntervalY;
                                }
                                else if (i_no == 18)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup18_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup18_IntervalY;
                                }
                                else if (i_no == 19)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup19_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup19_IntervalY;
                                }
                                else if (i_no == 20)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup20_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup20_IntervalY;
                                }
                                else if (i_no == 21)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup21_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup21_IntervalY;
                                }
                                else if (i_no == 22)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup22_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup22_IntervalY;
                                }
                                else if (i_no == 23)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup23_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup23_IntervalY;
                                }
                                else if (i_no == 24)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup24_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup24_IntervalY;
                                }
                                else if (i_no == 25)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup25_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup25_IntervalY;
                                }
                                else if (i_no == 26)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup26_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup26_IntervalY;
                                }
                                else if (i_no == 27)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup27_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup27_IntervalY;
                                }
                                else if (i_no == 28)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup28_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup28_IntervalY;
                                }
                                else if (i_no == 29)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup29_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup29_IntervalY;
                                }
                                else if (i_no == 30)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup30_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup30_IntervalY;
                                }
                                else if (i_no == 31)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup31_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup31_IntervalY;
                                }
                                else if (i_no == 32)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup32_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup32_IntervalY;
                                }
                                else if (i_no == 33)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup33_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup33_IntervalY;
                                }
                                else if (i_no == 34)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup34_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup34_IntervalY;
                                }
                                else if (i_no == 35)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup35_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup35_IntervalY;
                                }
                                else if (i_no == 36)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup36_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup36_IntervalY;
                                }
                            }
                            else
                            {
                                if (i_no >= Lib_Card.Configure.Parameter.Machine_Area1_CupMin && i_no <= Lib_Card.Configure.Parameter.Machine_Area1_CupMax)
                                {
                                    if (Lib_Card.Configure.Parameter.Machine_Area1_Layout == 1)
                                    {
                                        //在区域1
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area1_X -
                                            ((i_no - Lib_Card.Configure.Parameter.Machine_Area1_CupMin) / Lib_Card.Configure.Parameter.Machine_Area1_Row) * Lib_Card.Configure.Parameter.Coordinate_Area1_IntervalX;

                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area1_Y +
                                            ((i_no - Lib_Card.Configure.Parameter.Machine_Area1_CupMin) % Lib_Card.Configure.Parameter.Machine_Area1_Row) * Lib_Card.Configure.Parameter.Coordinate_Area1_IntervalY;

                                    }
                                    else
                                    {
                                        //在区域1
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area1_X -
                                            ((i_no - Lib_Card.Configure.Parameter.Machine_Area1_CupMin) % Lib_Card.Configure.Parameter.Machine_Area1_Col) * Lib_Card.Configure.Parameter.Coordinate_Area1_IntervalX;
                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area1_Y -
                                            ((i_no - Lib_Card.Configure.Parameter.Machine_Area1_CupMin) / Lib_Card.Configure.Parameter.Machine_Area1_Col) * Lib_Card.Configure.Parameter.Coordinate_Area1_IntervalY;
                                    }
                                }
                                else if (i_no >= Lib_Card.Configure.Parameter.Machine_Area2_CupMin && i_no <= Lib_Card.Configure.Parameter.Machine_Area2_CupMax)
                                {
                                    if (Lib_Card.Configure.Parameter.Machine_Area2_Layout == 1)
                                    {
                                        //在区域2
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area2_X -
                                      ((i_no - Lib_Card.Configure.Parameter.Machine_Area2_CupMin) / Lib_Card.Configure.Parameter.Machine_Area2_Row) * Lib_Card.Configure.Parameter.Coordinate_Area2_IntervalX;

                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area2_Y +
                                        ((i_no - Lib_Card.Configure.Parameter.Machine_Area2_CupMin) % Lib_Card.Configure.Parameter.Machine_Area2_Row) * Lib_Card.Configure.Parameter.Coordinate_Area2_IntervalY;

                                    }
                                    else
                                    {
                                        //在区域2
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area2_X -
                                          ((i_no - Lib_Card.Configure.Parameter.Machine_Area2_CupMin) % Lib_Card.Configure.Parameter.Machine_Area2_Col) * Lib_Card.Configure.Parameter.Coordinate_Area2_IntervalX;

                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area2_Y -
                                        ((i_no - Lib_Card.Configure.Parameter.Machine_Area2_CupMin) / Lib_Card.Configure.Parameter.Machine_Area2_Col) * Lib_Card.Configure.Parameter.Coordinate_Area2_IntervalY;
                                    }
                                }
                                else if (i_no >= Lib_Card.Configure.Parameter.Machine_Area3_CupMin && i_no <= Lib_Card.Configure.Parameter.Machine_Area3_CupMax)
                                {
                                    if (Lib_Card.Configure.Parameter.Machine_Area3_Layout == 1)
                                    {
                                        //在区域3
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area3_X -
                                    ((i_no - Lib_Card.Configure.Parameter.Machine_Area3_CupMin) / Lib_Card.Configure.Parameter.Machine_Area3_Row) * Lib_Card.Configure.Parameter.Coordinate_Area3_IntervalX;

                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area3_Y +
                                        ((i_no - Lib_Card.Configure.Parameter.Machine_Area3_CupMin) % Lib_Card.Configure.Parameter.Machine_Area3_Row) * Lib_Card.Configure.Parameter.Coordinate_Area3_IntervalY;

                                    }
                                    else
                                    {
                                        //在区域3
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area3_X -
                                          ((i_no - Lib_Card.Configure.Parameter.Machine_Area3_CupMin) % Lib_Card.Configure.Parameter.Machine_Area3_Col) * Lib_Card.Configure.Parameter.Coordinate_Area3_IntervalX;

                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area3_Y -
                                        ((i_no - Lib_Card.Configure.Parameter.Machine_Area3_CupMin) / Lib_Card.Configure.Parameter.Machine_Area3_Col) * Lib_Card.Configure.Parameter.Coordinate_Area3_IntervalY;
                                    }
                                }
                                else if (i_no >= Lib_Card.Configure.Parameter.Machine_Area4_CupMin && i_no <= Lib_Card.Configure.Parameter.Machine_Area4_CupMax)
                                {
                                    if (Lib_Card.Configure.Parameter.Machine_Area4_Layout == 1)
                                    {
                                        //在区域4
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area4_X -
                                    ((i_no - Lib_Card.Configure.Parameter.Machine_Area4_CupMin) / Lib_Card.Configure.Parameter.Machine_Area4_Row) * Lib_Card.Configure.Parameter.Coordinate_Area4_IntervalX;

                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area4_Y +
                                        ((i_no - Lib_Card.Configure.Parameter.Machine_Area4_CupMin) % Lib_Card.Configure.Parameter.Machine_Area4_Row) * Lib_Card.Configure.Parameter.Coordinate_Area4_IntervalY;

                                    }
                                    else
                                    {
                                        //在区域4
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area4_X +
                                          ((i_no - Lib_Card.Configure.Parameter.Machine_Area4_CupMin) % Lib_Card.Configure.Parameter.Machine_Area4_Col) * Lib_Card.Configure.Parameter.Coordinate_Area4_IntervalX;

                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area4_Y +
                                        ((i_no - Lib_Card.Configure.Parameter.Machine_Area4_CupMin) / Lib_Card.Configure.Parameter.Machine_Area4_Col) * Lib_Card.Configure.Parameter.Coordinate_Area4_IntervalY;
                                    }
                                }
                                else if (i_no >= Lib_Card.Configure.Parameter.Machine_Area5_CupMin && i_no <= Lib_Card.Configure.Parameter.Machine_Area5_CupMax)
                                {
                                    if (Lib_Card.Configure.Parameter.Machine_Area5_Layout == 1)
                                    {
                                        //在区域5
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area5_X -
                                    ((i_no - Lib_Card.Configure.Parameter.Machine_Area5_CupMin) / Lib_Card.Configure.Parameter.Machine_Area5_Row) * Lib_Card.Configure.Parameter.Coordinate_Area5_IntervalX;

                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area5_Y +
                                        ((i_no - Lib_Card.Configure.Parameter.Machine_Area5_CupMin) % Lib_Card.Configure.Parameter.Machine_Area5_Row) * Lib_Card.Configure.Parameter.Coordinate_Area5_IntervalY;
                                    }
                                    else
                                    {
                                        //在区域5
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area5_X +
                                      ((i_no - Lib_Card.Configure.Parameter.Machine_Area5_CupMin) % Lib_Card.Configure.Parameter.Machine_Area5_Col) * Lib_Card.Configure.Parameter.Coordinate_Area5_IntervalX;

                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area5_Y +
                                        ((i_no - Lib_Card.Configure.Parameter.Machine_Area5_CupMin) / Lib_Card.Configure.Parameter.Machine_Area5_Col) * Lib_Card.Configure.Parameter.Coordinate_Area5_IntervalY;
                                    }
                                }
                                else
                                {
                                    if (Lib_Card.Configure.Parameter.Machine_Area6_Layout == 1)
                                    {
                                        //在区域6
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area6_X -
                                    ((i_no - Lib_Card.Configure.Parameter.Machine_Area6_CupMin) / Lib_Card.Configure.Parameter.Machine_Area6_Row) * Lib_Card.Configure.Parameter.Coordinate_Area6_IntervalX;

                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area6_Y +
                                        ((i_no - Lib_Card.Configure.Parameter.Machine_Area6_CupMin) % Lib_Card.Configure.Parameter.Machine_Area6_Row) * Lib_Card.Configure.Parameter.Coordinate_Area6_IntervalY;
                                    }
                                    else
                                    {
                                        //在区域6
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area6_X +
                                     ((i_no - Lib_Card.Configure.Parameter.Machine_Area6_CupMin) % Lib_Card.Configure.Parameter.Machine_Area6_Col) * Lib_Card.Configure.Parameter.Coordinate_Area6_IntervalX;
                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area6_Y +
                                        ((i_no - Lib_Card.Configure.Parameter.Machine_Area6_CupMin) / Lib_Card.Configure.Parameter.Machine_Area6_Col) * Lib_Card.Configure.Parameter.Coordinate_Area6_IntervalY;
                                    }

                                }
                            }
                        }
                        break;

                    case 2:
                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Balance_X;
                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Balance_Y;
                        break;
                    case 3:
                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Standby_X;
                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Standby_Y;
                        break;
                    case 4:
                        if (0 >= i_no || i_no > Lib_Card.Configure.Parameter.Machine_Cup_Total)
                        {
                            throw new Exception("4");
                        }
                        if (i_no == 1)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover1_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover1_IntervalY;
                        }
                        else if (i_no == 2)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover2_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover2_IntervalY;
                        }
                        else if (i_no == 3)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover3_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover3_IntervalY;
                        }
                        else if (i_no == 4)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover4_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover4_IntervalY;
                        }
                        else if (i_no == 5)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover5_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover5_IntervalY;
                        }
                        else if (i_no == 6)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover6_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover6_IntervalY;
                        }
                        else if (i_no == 7)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover7_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover7_IntervalY;
                        }
                        else if (i_no == 8)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover8_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover8_IntervalY;
                        }
                        else if (i_no == 9)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover9_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover9_IntervalY;
                        }
                        else if (i_no == 10)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover10_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover10_IntervalY;
                        }
                        else if (i_no == 11)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover11_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover11_IntervalY;
                        }
                        else if (i_no == 12)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover12_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover12_IntervalY;
                        }
                        else if (i_no == 13)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover13_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover13_IntervalY;
                        }
                        else if (i_no == 14)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover14_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover14_IntervalY;
                        }
                        else if (i_no == 15)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover15_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover15_IntervalY;
                        }
                        else if (i_no == 16)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover16_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover16_IntervalY;
                        }
                        else if (i_no == 17)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover17_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover17_IntervalY;
                        }
                        else if (i_no == 18)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover18_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover18_IntervalY;
                        }
                        else if (i_no == 19)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover19_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover19_IntervalY;
                        }
                        else if (i_no == 20)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover20_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover20_IntervalY;
                        }
                        else if (i_no == 21)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover21_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover21_IntervalY;
                        }
                        else if (i_no == 22)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover22_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover22_IntervalY;
                        }
                        else if (i_no == 23)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover23_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover23_IntervalY;
                        }
                        else if (i_no == 24)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover24_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover24_IntervalY;
                        }
                        else if (i_no == 25)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover25_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover25_IntervalY;
                        }
                        else if (i_no == 26)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover26_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover26_IntervalY;
                        }
                        else if (i_no == 27)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover27_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover27_IntervalY;
                        }
                        else if (i_no == 28)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover28_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover28_IntervalY;
                        }
                        else if (i_no == 29)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover29_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover29_IntervalY;
                        }
                        else if (i_no == 30)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover30_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover30_IntervalY;
                        }
                        else if (i_no == 31)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover31_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover31_IntervalY;
                        }
                        else if (i_no == 32)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover32_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover32_IntervalY;
                        }
                        else if (i_no == 33)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover33_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover33_IntervalY;
                        }
                        else if (i_no == 34)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover34_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover34_IntervalY;
                        }
                        else if (i_no == 35)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover35_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover35_IntervalY;
                        }
                        else if (i_no == 36)
                        {
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_CupCover36_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_CupCover36_IntervalY;
                        }
                        break;
                    case 5:
                        if (0 >= i_no || i_no > Lib_Card.Configure.Parameter.Machine_Cup_Total)
                        {
                            throw new Exception("4");
                        }
                        if (Lib_Card.Configure.Parameter.Other_IsOnlyDrip == 1)
                        {
                            //在区域1
                            i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area1_X -
                                ((i_no - Lib_Card.Configure.Parameter.Machine_Area1_CupMin) % Lib_Card.Configure.Parameter.Machine_Area1_Row) * Lib_Card.Configure.Parameter.Coordinate_Area1_IntervalX;
                            i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area1_Y -
                                ((i_no - Lib_Card.Configure.Parameter.Machine_Area1_CupMin) / Lib_Card.Configure.Parameter.Machine_Area1_Row) * Lib_Card.Configure.Parameter.Coordinate_Area1_IntervalY;

                        }
                        else
                        {
                            if (SmartDyeing.FADM_Object.Communal._lis_dyeCupNum.Contains(i_no) && SmartDyeing.FADM_Object.Communal._dic_dyeType[i_no] == 1)
                            {
                                if (i_no == 1)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup1_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup1_IntervalY;
                                }
                                else if (i_no == 2)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup2_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup2_IntervalY;
                                }
                                else if (i_no == 3)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup3_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup3_IntervalY;
                                }
                                else if (i_no == 4)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup4_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup4_IntervalY;
                                }
                                else if (i_no == 5)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup5_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup5_IntervalY;
                                }
                                else if (i_no == 6)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup6_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup6_IntervalY;
                                }
                                else if (i_no == 7)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup7_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup7_IntervalY;
                                }
                                else if (i_no == 8)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup8_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup8_IntervalY;
                                }
                                else if (i_no == 9)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup9_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup9_IntervalY;
                                }
                                else if (i_no == 10)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup10_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup10_IntervalY;
                                }
                                else if (i_no == 11)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup11_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup11_IntervalY;
                                }
                                else if (i_no == 12)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup12_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup12_IntervalY;
                                }
                                else if (i_no == 13)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup13_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup13_IntervalY;
                                }
                                else if (i_no == 14)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup14_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup14_IntervalY;
                                }
                                else if (i_no == 15)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup15_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup15_IntervalY;
                                }
                                else if (i_no == 16)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup16_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup16_IntervalY;
                                }
                                else if (i_no == 17)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup17_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup17_IntervalY;
                                }
                                else if (i_no == 18)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup18_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup18_IntervalY;
                                }
                                else if (i_no == 19)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup19_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup19_IntervalY;
                                }
                                else if (i_no == 20)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup20_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup20_IntervalY;
                                }
                                else if (i_no == 21)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup21_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup21_IntervalY;
                                }
                                else if (i_no == 22)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup22_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup22_IntervalY;
                                }
                                else if (i_no == 23)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup23_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup23_IntervalY;
                                }
                                else if (i_no == 24)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup24_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup24_IntervalY;
                                }
                                else if (i_no == 25)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup25_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup25_IntervalY;
                                }
                                else if (i_no == 26)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup26_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup26_IntervalY;
                                }
                                else if (i_no == 27)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup27_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup27_IntervalY;
                                }
                                else if (i_no == 28)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup28_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup28_IntervalY;
                                }
                                else if (i_no == 29)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup29_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup29_IntervalY;
                                }
                                else if (i_no == 30)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup30_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup30_IntervalY;
                                }
                                else if (i_no == 31)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup31_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup31_IntervalY;
                                }
                                else if (i_no == 32)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup32_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup32_IntervalY;
                                }
                                else if (i_no == 33)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup33_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup33_IntervalY;
                                }
                                else if (i_no == 34)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup34_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup34_IntervalY;
                                }
                                else if (i_no == 35)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup35_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup35_IntervalY;
                                }
                                else if (i_no == 36)
                                {
                                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Cup36_IntervalX;
                                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Cup36_IntervalY;
                                }
                            }
                            else
                            {
                                if (i_no >= Lib_Card.Configure.Parameter.Machine_Area1_CupMin && i_no <= Lib_Card.Configure.Parameter.Machine_Area1_CupMax)
                                {
                                    if (Lib_Card.Configure.Parameter.Machine_Area1_Layout == 1)
                                    {
                                        //在区域1
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area1_X -
                                            ((i_no - Lib_Card.Configure.Parameter.Machine_Area1_CupMin) / Lib_Card.Configure.Parameter.Machine_Area1_Row) * Lib_Card.Configure.Parameter.Coordinate_Area1_IntervalX;

                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area1_Y +
                                            ((i_no - Lib_Card.Configure.Parameter.Machine_Area1_CupMin) % Lib_Card.Configure.Parameter.Machine_Area1_Row) * Lib_Card.Configure.Parameter.Coordinate_Area1_IntervalY;

                                    }
                                    else
                                    {
                                        //在区域1
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area1_X -
                                            ((i_no - Lib_Card.Configure.Parameter.Machine_Area1_CupMin) % Lib_Card.Configure.Parameter.Machine_Area1_Col) * Lib_Card.Configure.Parameter.Coordinate_Area1_IntervalX;
                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area1_Y -
                                            ((i_no - Lib_Card.Configure.Parameter.Machine_Area1_CupMin) / Lib_Card.Configure.Parameter.Machine_Area1_Col) * Lib_Card.Configure.Parameter.Coordinate_Area1_IntervalY;
                                    }
                                }
                                else if (i_no >= Lib_Card.Configure.Parameter.Machine_Area2_CupMin && i_no <= Lib_Card.Configure.Parameter.Machine_Area2_CupMax)
                                {
                                    if (Lib_Card.Configure.Parameter.Machine_Area2_Layout == 1)
                                    {
                                        //在区域2
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area2_X -
                                      ((i_no - Lib_Card.Configure.Parameter.Machine_Area2_CupMin) / Lib_Card.Configure.Parameter.Machine_Area2_Row) * Lib_Card.Configure.Parameter.Coordinate_Area2_IntervalX;

                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area2_Y +
                                        ((i_no - Lib_Card.Configure.Parameter.Machine_Area2_CupMin) % Lib_Card.Configure.Parameter.Machine_Area2_Row) * Lib_Card.Configure.Parameter.Coordinate_Area2_IntervalY;

                                    }
                                    else
                                    {
                                        //在区域2
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area2_X -
                                          ((i_no - Lib_Card.Configure.Parameter.Machine_Area2_CupMin) % Lib_Card.Configure.Parameter.Machine_Area2_Col) * Lib_Card.Configure.Parameter.Coordinate_Area2_IntervalX;

                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area2_Y -
                                        ((i_no - Lib_Card.Configure.Parameter.Machine_Area2_CupMin) / Lib_Card.Configure.Parameter.Machine_Area2_Col) * Lib_Card.Configure.Parameter.Coordinate_Area2_IntervalY;
                                    }
                                }
                                else if (i_no >= Lib_Card.Configure.Parameter.Machine_Area3_CupMin && i_no <= Lib_Card.Configure.Parameter.Machine_Area3_CupMax)
                                {
                                    if (Lib_Card.Configure.Parameter.Machine_Area3_Layout == 1)
                                    {
                                        //在区域3
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area3_X -
                                    ((i_no - Lib_Card.Configure.Parameter.Machine_Area3_CupMin) / Lib_Card.Configure.Parameter.Machine_Area3_Row) * Lib_Card.Configure.Parameter.Coordinate_Area3_IntervalX;

                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area3_Y +
                                        ((i_no - Lib_Card.Configure.Parameter.Machine_Area3_CupMin) % Lib_Card.Configure.Parameter.Machine_Area3_Row) * Lib_Card.Configure.Parameter.Coordinate_Area3_IntervalY;

                                    }
                                    else
                                    {
                                        //在区域3
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area3_X -
                                          ((i_no - Lib_Card.Configure.Parameter.Machine_Area3_CupMin) % Lib_Card.Configure.Parameter.Machine_Area3_Col) * Lib_Card.Configure.Parameter.Coordinate_Area3_IntervalX;

                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area3_Y -
                                        ((i_no - Lib_Card.Configure.Parameter.Machine_Area3_CupMin) / Lib_Card.Configure.Parameter.Machine_Area3_Col) * Lib_Card.Configure.Parameter.Coordinate_Area3_IntervalY;
                                    }
                                }
                                else if (i_no >= Lib_Card.Configure.Parameter.Machine_Area4_CupMin && i_no <= Lib_Card.Configure.Parameter.Machine_Area4_CupMax)
                                {
                                    if (Lib_Card.Configure.Parameter.Machine_Area4_Layout == 1)
                                    {
                                        //在区域4
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area4_X -
                                    ((i_no - Lib_Card.Configure.Parameter.Machine_Area4_CupMin) / Lib_Card.Configure.Parameter.Machine_Area4_Row) * Lib_Card.Configure.Parameter.Coordinate_Area4_IntervalX;

                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area4_Y +
                                        ((i_no - Lib_Card.Configure.Parameter.Machine_Area4_CupMin) % Lib_Card.Configure.Parameter.Machine_Area4_Row) * Lib_Card.Configure.Parameter.Coordinate_Area4_IntervalY;

                                    }
                                    else
                                    {
                                        //在区域4
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area4_X +
                                          ((i_no - Lib_Card.Configure.Parameter.Machine_Area4_CupMin) % Lib_Card.Configure.Parameter.Machine_Area4_Col) * Lib_Card.Configure.Parameter.Coordinate_Area4_IntervalX;

                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area4_Y +
                                        ((i_no - Lib_Card.Configure.Parameter.Machine_Area4_CupMin) / Lib_Card.Configure.Parameter.Machine_Area4_Col) * Lib_Card.Configure.Parameter.Coordinate_Area4_IntervalY;
                                    }
                                }
                                else if (i_no >= Lib_Card.Configure.Parameter.Machine_Area5_CupMin && i_no <= Lib_Card.Configure.Parameter.Machine_Area5_CupMax)
                                {
                                    if (Lib_Card.Configure.Parameter.Machine_Area5_Layout == 1)
                                    {
                                        //在区域5
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area5_X -
                                    ((i_no - Lib_Card.Configure.Parameter.Machine_Area5_CupMin) / Lib_Card.Configure.Parameter.Machine_Area5_Row) * Lib_Card.Configure.Parameter.Coordinate_Area5_IntervalX;

                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area5_Y +
                                        ((i_no - Lib_Card.Configure.Parameter.Machine_Area5_CupMin) % Lib_Card.Configure.Parameter.Machine_Area5_Row) * Lib_Card.Configure.Parameter.Coordinate_Area5_IntervalY;
                                    }
                                    else
                                    {
                                        //在区域5
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area5_X +
                                      ((i_no - Lib_Card.Configure.Parameter.Machine_Area5_CupMin) % Lib_Card.Configure.Parameter.Machine_Area5_Col) * Lib_Card.Configure.Parameter.Coordinate_Area5_IntervalX;

                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area5_Y +
                                        ((i_no - Lib_Card.Configure.Parameter.Machine_Area5_CupMin) / Lib_Card.Configure.Parameter.Machine_Area5_Col) * Lib_Card.Configure.Parameter.Coordinate_Area5_IntervalY;
                                    }
                                }
                                else
                                {
                                    if (Lib_Card.Configure.Parameter.Machine_Area6_Layout == 1)
                                    {
                                        //在区域6
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area6_X -
                                    ((i_no - Lib_Card.Configure.Parameter.Machine_Area6_CupMin) / Lib_Card.Configure.Parameter.Machine_Area6_Row) * Lib_Card.Configure.Parameter.Coordinate_Area6_IntervalX;

                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area6_Y +
                                        ((i_no - Lib_Card.Configure.Parameter.Machine_Area6_CupMin) % Lib_Card.Configure.Parameter.Machine_Area6_Row) * Lib_Card.Configure.Parameter.Coordinate_Area6_IntervalY;
                                    }
                                    else
                                    {
                                        //在区域6
                                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Area6_X +
                                     ((i_no - Lib_Card.Configure.Parameter.Machine_Area6_CupMin) % Lib_Card.Configure.Parameter.Machine_Area6_Col) * Lib_Card.Configure.Parameter.Coordinate_Area6_IntervalX;
                                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Area6_Y +
                                        ((i_no - Lib_Card.Configure.Parameter.Machine_Area6_CupMin) / Lib_Card.Configure.Parameter.Machine_Area6_Col) * Lib_Card.Configure.Parameter.Coordinate_Area6_IntervalY;
                                    }

                                }
                            }
                        }
                        i_xPules -= Lib_Card.Configure.Parameter.Coordinate_Cup_Decompression;
                        break;
                    case 6:
                        if (0 >= i_no || i_no > Lib_Card.Configure.Parameter.Machine_Cup_Total)
                        {
                            throw new Exception("4");
                        }
                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_AreaDryCloth_X - (i_no - 1) %
                        Lib_Card.Configure.Parameter.Machine_AreaDryCloth_Col * Lib_Card.Configure.Parameter.Coordinate_AreaDryCloth_IntervalX;
                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_AreaDryCloth_Y + (i_no - 1) /
                            Lib_Card.Configure.Parameter.Machine_AreaDryCloth_Col * Lib_Card.Configure.Parameter.Coordinate_AreaDryCloth_IntervalY;

                        break;
                    case 7:
                        if (0 >= i_no || i_no > Lib_Card.Configure.Parameter.Machine_Cup_Total)
                        {
                            throw new Exception("4");
                        }
                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_AreaWetCloth_X - (i_no - 1) %
                        Lib_Card.Configure.Parameter.Machine_AreaWetCloth_Col * Lib_Card.Configure.Parameter.Coordinate_AreaWetCloth_IntervalX;
                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_AreaWetCloth_Y + (i_no - 1) /
                            Lib_Card.Configure.Parameter.Machine_AreaWetCloth_Col * Lib_Card.Configure.Parameter.Coordinate_AreaWetCloth_IntervalY;

                        break;
                    case 8:
                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_DryClamp_X;
                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_DryClamp_Y;
                        break;
                    case 9:
                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_WetClamp_X;
                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_WetClamp_Y;
                        break;
                    default:
                        throw new Exception("5");
                }
                Lib_Log.Log.writeLogException("脉冲是iXPules=" + i_xPules.ToString() + "iYPules=" + i_yPules.ToString());
                int d1 = 0;
                if (i_xPules > 65536)
                {
                    d1 = i_xPules / 65536;
                    i_xPules = i_xPules % 65536;
                }
                int d2 = 0;
                if (i_yPules > 65536)
                {
                    d2 = i_yPules / 65536;
                    i_yPules = i_yPules % 65536;
                }
                int[] ia_array = { 2, i_xPules, d1, i_yPules, d2, 0, 0, 0, 0, i_type, 1 };
                Lib_Log.Log.writeLogException("发送坐标");
                Console.WriteLine("发送坐标");
                int i_state = FADM_Object.Communal._tcpModBus.Write(800, ia_array);
                if (i_state != -1)
                {
                    //判断错误返回值
                    if (GetReturn(1) == -2)
                    {
                        return -2;
                    }
                }
                else
                {
                    Lib_Log.Log.writeLogException("写入定点移动编号返回失败,继续写入");
                    Console.WriteLine("写入定点移动编号返回失败,继续写入");
                    if (b_istrue)
                    {
                        b_istrue = false;
                        FADM_Object.Communal._tcpModBus.ReConnect();
                        goto lableTop;
                    }
                    b_istrue = true;
                    goto lableTop;
                    //throw new Exception("6");
                }
                if (i_move_Type == 3)
                {
                    //失能关闭
                    Power(2);
                }
                return 0;
            }
            catch (Exception e)
            {
                FADM_Object.Communal.WriteTcpStatus(true); //恢复
                if (e.Message.Equals("4") || e.Message.Equals("5") || e.Message.Equals("-2") || e.Message.Equals("6"))
                {
                    throw e;
                }
                else
                {
                    MessageBox.Show(e.ToString());
                    throw new Exception("-1");
                }
            }
            finally
            {
                FADM_Object.Communal.WriteTcpStatus(true); //恢复
            }
        }



        /// <summary>
        /// 抽液
        /// </summary>
        /// <param name="i_extract_Pulse">脉冲数</param>
        /// <param name="b_isTrue">排空动作</param>
        /// <param name="isBM">大小针筒 0小针筒 大针筒</param>
        /// <returns></returns>
        public static int Extract(int i_extract_Pulse, bool b_isTrue, int i_syringeType)
        {
            try
            {

            Label1:
                if (!FADM_Object.Communal._b_auto)
                {//在手动页面 等待手动页面退出
                    goto Label1;
                }

                Lib_Log.Log.writeLogException("执行抽液方法Extract_Pulse=" + i_extract_Pulse.ToString());

                Console.WriteLine("开始抽液Extract_Pulse=" + i_extract_Pulse.ToString());
                bool b_istrue = false;
                FADM_Object.Communal.WriteTcpStatus(false); //天平先不要轮询
                ClearSuccessState();//先清除标志位
                Console.WriteLine("清除完标志位");
                Lib_Log.Log.writeLogException("清除标志位结束");



            lableTop:
                int d_1 = 0;
                d_1 = i_extract_Pulse / 65536;
                if (i_extract_Pulse < 0) //负数脉冲
                {
                    if (d_1 == 0)
                    {
                        d_1 = -1;
                    }
                    else
                    {
                        if (Math.Abs(i_extract_Pulse) > 65536)
                        {
                            d_1 = d_1 + -1;
                        }
                    }
                }
                else
                {  //正数脉冲
                    d_1 = i_extract_Pulse / 65536;
                }
                i_extract_Pulse = i_extract_Pulse % 65536;
                int[] ia_array = { 3, 0, 0, 0, 0, i_extract_Pulse, d_1, 0, 0, 0, 1, 0, b_isTrue ? 1 : 0, i_syringeType };
                int i_state = FADM_Object.Communal._tcpModBus.Write(800, ia_array);
                if (i_state != -1)
                {
                    //判断错误返回值
                    if (GetReturn(1) == -2)
                    {
                        return -2;
                    }
                }
                else
                {
                    Lib_Log.Log.writeLogException("写入抽液编号返回失败,继续写入");
                    Console.WriteLine("写入抽液编号返回失败,继续写入");
                    if (b_istrue)
                    {
                        b_istrue = false;
                        FADM_Object.Communal._tcpModBus.ReConnect();
                        goto lableTop;
                    }
                    b_istrue = true;
                    goto lableTop;
                    //throw new Exception("7");
                }
                return 0;
            }
            catch (Exception e)
            {
                FADM_Object.Communal.WriteTcpStatus(true); //恢复
                if (e.Message.Equals("3") || e.Message.Equals("7") || e.Message.Equals("-2") || e.Message.Equals("未发现针筒"))
                {
                    string msg = "";
                    int[] ia_errArray = new int[100];
                Label123:
                    int i_state1 = MyModbusFun.GetErrMsgNew(ref ia_errArray);
                    if (i_state1 == -1)
                        goto Label123;
                    for (int i = 0; i < ia_errArray.Length; i++)
                    {
                        if (ia_errArray[i] == 2101)
                        {
                            throw new Exception("未发现针筒");
                        }

                    }
                    throw e;
                }
                else
                {
                    throw new Exception("-1");
                }
            }
            finally
            {
                FADM_Object.Communal.WriteTcpStatus(true); //恢复
            }
        }

        /// <summary>
        /// 注液
        /// </summary>
        /// <param name="i_extract_Pulse">脉冲数</param>
        /// <returns></returns>
        public static int Shove(int i_extract_Pulse)
        {
            try
            {
            Label1:
                if (!FADM_Object.Communal._b_auto)
                {//在手动页面 等待手动页面退出
                    goto Label1;
                }
                Lib_Log.Log.writeLogException("执行注液方法Extract_Pulse=" + i_extract_Pulse.ToString());

                bool b_istrue = false;
                FADM_Object.Communal.WriteTcpStatus(false); //天平先不要轮询
                ClearSuccessState();//先清除标志位
                Lib_Log.Log.writeLogException("清除标志位结束");

            lableTop:
                int d_1 = 0;
                d_1 = i_extract_Pulse / 65536;
                if (i_extract_Pulse < 0) //负数脉冲
                {
                    if (d_1 == 0)
                    {
                        d_1 = -1;
                    }
                    else
                    {
                        if (Math.Abs(i_extract_Pulse) > 65536)
                        {
                            d_1 = d_1 + -1;
                        }
                    }
                }
                else
                {  //正数脉冲
                    d_1 = i_extract_Pulse / 65536;
                }

                int[] ia_array = { 4, 0, 0, 0, 0, i_extract_Pulse, d_1, 0, 0, 0, 1 };
                int i_state = FADM_Object.Communal._tcpModBus.Write(800, ia_array);
                if (i_state != -1)
                {
                    //判断错误返回值
                    if (GetReturn(1) == -2)
                    {
                        return -2;
                    }
                    else
                    {

                    }
                }
                else
                {
                    Lib_Log.Log.writeLogException("写入注液编号返回失败,继续写入");
                    Console.WriteLine("写入注液编号返回失败,继续写入");
                    if (b_istrue)
                    {
                        b_istrue = false;
                        FADM_Object.Communal._tcpModBus.ReConnect();
                        goto lableTop;
                    }
                    b_istrue = true;
                    goto lableTop;
                    //throw new Exception("8");
                }
                return 0;
            }
            catch (Exception e)
            {
                FADM_Object.Communal.WriteTcpStatus(true); //恢复
                if (e.Message.Equals("-2") || e.Message.Equals("8"))
                {
                    throw e;
                }
                else
                {
                    throw new Exception("-1");
                }
            }
            finally
            {
                FADM_Object.Communal.WriteTcpStatus(true); //恢复
            }
        }

        /// <summary>
        /// 天平检查
        /// </summary>
        /// <returns></returns>
        public static int CheckBalance()
        {
            try
            {
                //Label1:
                //    if (!FADM_Object.Communal._b_auto)
                //    {//在手动页面 等待手动页面退出
                //        goto Label1;
                //    }
                Lib_Log.Log.writeLogException("执行天平检查CheckBalance");

                bool b_istrue = false;
                FADM_Object.Communal.WriteTcpStatus(false); //天平先不要轮询
                ClearSuccessState();//先清除标志位
                Lib_Log.Log.writeLogException("清除标志位结束");

            lableTop:


                int[] ia_array = { 13 };
                int i_state = FADM_Object.Communal._tcpModBus.Write(800, ia_array);
                if (i_state != -1)
                {
                    //判断错误返回值
                    if (GetReturn(1) == -2)
                    {
                        return -2;
                    }
                    else
                    {

                    }
                }
                else
                {
                    Lib_Log.Log.writeLogException("写入天平检查编号返回失败,继续写入");
                    Console.WriteLine("写入天平检查编号返回失败,继续写入");
                    if (b_istrue)
                    {
                        b_istrue = false;
                        FADM_Object.Communal._tcpModBus.ReConnect();
                        goto lableTop;
                    }
                    b_istrue = true;
                    goto lableTop;
                    //throw new Exception("8");
                }
                return 0;
            }
            catch (Exception e)
            {
                FADM_Object.Communal.WriteTcpStatus(true); //恢复
                if (e.Message.Equals("-2") || e.Message.Equals("8"))
                {
                    throw e;
                }
                else
                {
                    throw new Exception("-1");
                }
            }
            finally
            {
                FADM_Object.Communal.WriteTcpStatus(true); //恢复
            }
        }

        /// <summary>
        /// 加水
        /// </summary>
        /// <param name="d_addWaterTime">加水时间</param>
        /// <returns></returns>
        public static int AddWater(double d_addWaterTime)
        {
            try
            {
            Label1:
                if (!FADM_Object.Communal._b_auto)
                {//在手动页面 等待手动页面退出
                    goto Label1;
                }
                Lib_Log.Log.writeLogException("执行加水方法addWaterTime = " + d_addWaterTime.ToString());
                bool b_istrue = false;
                FADM_Object.Communal.WriteTcpStatus(false); //天平先不要轮询
                ClearSuccessState();//先清除标志位
                Lib_Log.Log.writeLogException("清除标志位结束");


            lableTop:
                int[] ia_array = { 5, 0, 0, 0, 0, 0, 0, (int)(d_addWaterTime * 1000), 0, 0, 1 };
                int i_state = FADM_Object.Communal._tcpModBus.Write(800, ia_array);
                if (i_state != -1)
                {
                    //判断错误返回值
                    if (GetReturn(1) == -2)
                    {
                        return -2;
                    }
                }
                else
                {
                    Lib_Log.Log.writeLogException("写入加水编号返回失败,继续写入");
                    Console.WriteLine("写入加水编号返回失败,继续写入");
                    if (b_istrue)
                    {
                        b_istrue = false;
                        FADM_Object.Communal._tcpModBus.ReConnect();
                        goto lableTop;
                    }
                    b_istrue = true;
                    goto lableTop;
                    //throw new Exception("9");
                }
                return 0;
            }
            catch (Exception e)
            {
                FADM_Object.Communal.WriteTcpStatus(true); //恢复
                if (e.Message.Equals("-2") || e.Message.Equals("9"))
                {
                    throw e;
                }
                else
                {
                    throw new Exception("-1");
                }
            }
            finally
            {
                FADM_Object.Communal.WriteTcpStatus(true); //恢复
            }
        }
        

        /// <summary>
        /// 泄压
        /// </summary>
        /// <param name="i_xStartPules">开始X坐标</param>
        /// <param name="i_yStartPules">开始Y坐标</param>
        /// <returns></returns>
        public static int Stressrelief(int i_xStartPules, int i_yStartPules)
        {
            try
            {
            Label1:
                if (!FADM_Object.Communal._b_auto)
                {//在手动页面 等待手动页面退出
                    goto Label1;
                }

                Lib_Log.Log.writeLogException("执行泄压方法");

                bool b_istrue = false;
                FADM_Object.Communal.WriteTcpStatus(false); //天平先不要轮询
                ClearSuccessState();//先清除标志位
                Lib_Log.Log.writeLogException("清除标志位结束");

            lableTop:
                int d_1 = 0;
                if (i_xStartPules > 65536)
                {
                    d_1 = i_xStartPules / 65536;
                    i_xStartPules = i_xStartPules % 65536;
                }
                int d_2 = 0;
                if (i_yStartPules > 65536)
                {
                    d_2 = i_yStartPules / 65536;
                    i_yStartPules = i_yStartPules % 65536;
                }
                int[] ia_array = { 7, i_xStartPules, d_1, i_yStartPules, d_2, 0, 0, 0, 0, 0, 1 };
                int i_state = FADM_Object.Communal._tcpModBus.Write(800, ia_array);
                if (i_state != -1)
                {
                    //判断错误返回值
                    if (GetReturn(1) == -2)
                    {
                        return -2;
                    }
                    else
                    {

                    }
                }
                else
                {
                    Lib_Log.Log.writeLogException("写入泄压动作编号返回失败,继续写入");
                    Console.WriteLine("泄压编号返回失败,继续写入");
                    if (b_istrue)
                    {
                        b_istrue = false;
                        FADM_Object.Communal._tcpModBus.ReConnect();
                        goto lableTop;
                    }
                    b_istrue = true;
                    goto lableTop;
                    //throw new Exception("10");
                }
                return 0;
            }
            catch (Exception e)
            {
                FADM_Object.Communal.WriteTcpStatus(true); //恢复
                if (e.Message.Equals("-2") || e.Message.Equals("10"))
                {
                    throw e;
                }
                else
                {
                    throw new Exception("-1");
                }
            }
            finally
            {
                FADM_Object.Communal.WriteTcpStatus(true); //恢复
            }
        }

        /// <summary>
        /// 放针
        /// </summary>
        /// <returns></returns>
        public static int Put()
        {
            try
            {
            Label1:
                if (!FADM_Object.Communal._b_auto)
                {//在手动页面 等待手动页面退出
                    goto Label1;
                }
                Lib_Log.Log.writeLogException("执行放针方法");

                bool b_istrue = false;
                FADM_Object.Communal.WriteTcpStatus(false); //天平先不要轮询
                ClearSuccessState();//先清除标志位
                Lib_Log.Log.writeLogException("清除标志位结束");

            lableTop:
                int[] ia_array = { 6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 };
                int i_state = FADM_Object.Communal._tcpModBus.Write(800, ia_array);
                if (i_state != -1)
                {
                    //判断错误返回值
                    if (GetReturn(1) == -2)
                    {
                        return -2;
                    }
                    else
                    {

                    }
                }
                else
                {
                    Lib_Log.Log.writeLogException("写入放针动作编号返回失败,继续写入");
                    Console.WriteLine("放针编号返回失败,继续写入");
                    if (b_istrue)
                    {
                        b_istrue = false;
                        FADM_Object.Communal._tcpModBus.ReConnect();
                        goto lableTop;
                    }
                    b_istrue = true;
                    goto lableTop;
                    //throw new Exception("10");
                }
                return 0;
            }
            catch (Exception e)
            {
                FADM_Object.Communal.WriteTcpStatus(true); //恢复
                if (e.Message.Equals("-2") || e.Message.Equals("10"))
                {
                    throw e;
                }
                else
                {
                    throw new Exception("-1");
                }
            }
            finally
            {
                FADM_Object.Communal.WriteTcpStatus(true); //恢复
            }
        }


        /// <summary>
        /// 放盖(关盖)
        /// </summary>
        /// <returns></returns>
        public static int PutCover()
        {
            try
            {
            Label1:
                if (!FADM_Object.Communal._b_auto)
                {//在手动页面 等待手动页面退出
                    goto Label1;
                }
                Lib_Log.Log.writeLogException("执行放盖方法");

                bool b_istrue = false;
                FADM_Object.Communal.WriteTcpStatus(false); //天平先不要轮询
                ClearSuccessState();//先清除标志位
                Lib_Log.Log.writeLogException("清除标志位结束");

            lableTop:
                int[] ia_array = { 12, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 };
                int i_state = FADM_Object.Communal._tcpModBus.Write(800, ia_array);
                if (i_state != -1)
                {
                    //判断错误返回值
                    if (GetReturn(1) == -2)
                    {
                        return -2;
                    }
                    else
                    {

                    }
                }
                else
                {
                    Lib_Log.Log.writeLogException("写入放盖动作编号返回失败,继续写入");
                    Console.WriteLine("放盖编号返回失败,继续写入");
                    if (b_istrue)
                    {
                        b_istrue = false;
                        FADM_Object.Communal._tcpModBus.ReConnect();
                        goto lableTop;
                    }
                    b_istrue = true;
                    goto lableTop;
                    //throw new Exception("10");
                }
                return 0;
            }
            catch (Exception e)
            {
                FADM_Object.Communal.WriteTcpStatus(true); //恢复
                if (e.Message.Equals("-2") || e.Message.Equals("10") || e.Message.Equals("未发现杯盖") || e.Message.Equals("发现杯盖或针筒") || e.Message.Equals("抓手A夹紧异常") || e.Message.Equals("抓手B夹紧异常"))
                {
                    throw e;
                }
                else
                {
                    throw new Exception("-1");
                }
            }
            finally
            {
                FADM_Object.Communal.WriteTcpStatus(true); //恢复
            }
        }

        /// <summary>
        /// 开关盖
        /// </summary>
        /// <param name="i_xStartPules">开始X坐标</param>
        /// <param name="i_yStartPules">开始Y坐标</param>
        /// <param name="i_xEndPules">结束X坐标</param>
        /// <param name="i_yEndPules">结束Y坐标</param>
        /// <param name="i_type">0:开盖 1：关盖</param>
        /// <returns></returns>
        public static int OpenOrPutCover(int i_xStartPules, int i_yStartPules, int i_xEndPules, int i_yEndPules, int i_type)
        {
            try
            {
            Label1:
                if (!FADM_Object.Communal._b_auto)
                {//在手动页面 等待手动页面退出
                    goto Label1;
                }
                Lib_Log.Log.writeLogException("执行放关盖方法");

                bool b_istrue = false;
                FADM_Object.Communal.WriteTcpStatus(false); //天平先不要轮询
                ClearSuccessState();//先清除标志位
                Lib_Log.Log.writeLogException("清除标志位结束");

            lableTop:
                int d_1 = 0;
                if (i_xStartPules > 65536)
                {
                    d_1 = i_xStartPules / 65536;
                    i_xStartPules = i_xStartPules % 65536;
                }
                int d_2 = 0;
                if (i_yStartPules > 65536)
                {
                    d_2 = i_yStartPules / 65536;
                    i_yStartPules = i_yStartPules % 65536;
                }
                int d_3 = 0;
                if (i_xEndPules > 65536)
                {
                    d_3 = i_xEndPules / 65536;
                    i_xEndPules = i_xEndPules % 65536;
                }
                int d_4 = 0;
                if (i_yEndPules > 65536)
                {
                    d_4 = i_yEndPules / 65536;
                    i_yEndPules = i_yEndPules % 65536;
                }
                int[] ia_array = { 11, i_xStartPules, d_1, i_yStartPules, d_2, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, i_xEndPules, d_3, i_yEndPules, d_4, 0, 0, i_type };
                int i_state = FADM_Object.Communal._tcpModBus.Write(800, ia_array);
                if (i_state != -1)
                {
                    //判断错误返回值
                    if (GetReturn(1) == -2)
                    {
                        return -2;
                    }
                    else
                    {

                    }
                }
                else
                {
                    Lib_Log.Log.writeLogException("写入开关盖动作编号返回失败,继续写入");
                    Console.WriteLine("开关盖编号返回失败,继续写入");
                    if (b_istrue)
                    {
                        b_istrue = false;
                        FADM_Object.Communal._tcpModBus.ReConnect();
                        goto lableTop;
                    }
                    b_istrue = true;
                    goto lableTop;
                    //throw new Exception("10");
                }
                return 0;
            }
            catch (Exception e)
            {
                FADM_Object.Communal.WriteTcpStatus(true); //恢复
                if (e.Message.Equals("-2") || e.Message.Equals("10") || e.Message.Equals("未发现杯盖") || e.Message.Equals("发现杯盖或针筒") || e.Message.Equals("抓手A夹紧异常") || e.Message.Equals("抓手B夹紧异常"))
                {
                    int[] ia_errArray = new int[100];
                Label123:
                    int i_state1 = MyModbusFun.GetErrMsgNew(ref ia_errArray);
                    if (i_state1 == -1)
                        goto Label123;
                    for (int i = 0; i < ia_errArray.Length; i++)
                    {
                        if (ia_errArray[i] == 2702)
                        {
                            throw new Exception("未发现杯盖");
                        }
                        else if (ia_errArray[i] == 2701)
                        {
                            throw new Exception("发现杯盖或针筒");
                        }
                        else if (ia_errArray[i] == 2703)
                        {
                            throw new Exception("配液杯取盖失败");
                        }
                        else if (ia_errArray[i] == 2704)
                        {
                            throw new Exception("放盖区取盖失败");
                        }
                        else if (ia_errArray[i] == 2705)
                        {
                            throw new Exception("关盖失败");
                        }

                    }
                    throw e;
                }
                else
                {
                    throw new Exception("-1");
                }
            }
            finally
            {
                FADM_Object.Communal.WriteTcpStatus(true); //恢复
            }
        }

        /// <summary>
        /// 出布/放布
        /// </summary>
        /// <param name="i_xStartPules">开始X坐标</param>
        /// <param name="i_yStartPules">开始Y坐标</param>
        /// <param name="i_xEndPules">结束X坐标</param>
        /// <param name="i_yEndPules">结束Y坐标</param>
        /// <param name="i_getCloth">0:备布区 1：缸体</param>
        /// <param name="i_putCloth">0:完成区 1：缸体</param>
        /// <returns></returns>
        public static int PutOrGetCloth(int i_xStartPules, int i_yStartPules, int i_xEndPules, int i_yEndPules, int i_getCloth, int i_putCloth)
        {
            try
            {
            Label1:
                if (!FADM_Object.Communal._b_auto)
                {//在手动页面 等待手动页面退出
                    goto Label1;
                }
                Lib_Log.Log.writeLogException("执行出布/放布方法");

                bool b_istrue = false;
                FADM_Object.Communal.WriteTcpStatus(false); //天平先不要轮询
                ClearSuccessState();//先清除标志位
                Lib_Log.Log.writeLogException("清除标志位结束");

            lableTop:
                int d_1 = 0;
                if (i_xStartPules > 65536)
                {
                    d_1 = i_xStartPules / 65536;
                    i_xStartPules = i_xStartPules % 65536;
                }
                int d_2 = 0;
                if (i_yStartPules > 65536)
                {
                    d_2 = i_yStartPules / 65536;
                    i_yStartPules = i_yStartPules % 65536;
                }
                int d_3 = 0;
                if (i_xEndPules > 65536)
                {
                    d_3 = i_xEndPules / 65536;
                    i_xEndPules = i_xEndPules % 65536;
                }
                int d_4 = 0;
                if (i_yEndPules > 65536)
                {
                    d_4 = i_yEndPules / 65536;
                    i_yEndPules = i_yEndPules % 65536;
                }
                int[] ia_array = { 16, i_xStartPules, d_1, i_yStartPules, d_2, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, i_xEndPules, d_3, i_yEndPules, d_4, 0, 0, 0,0,0, i_getCloth, i_putCloth };
                int i_state = FADM_Object.Communal._tcpModBus.Write(800, ia_array);
                if (i_state != -1)
                {
                    //判断错误返回值
                    if (GetReturn(1) == -2)
                    {
                        return -2;
                    }
                    else
                    {

                    }
                }
                else
                {
                    Lib_Log.Log.writeLogException("写入出布/放布动作编号返回失败,继续写入");
                    Console.WriteLine("出布/放布编号返回失败,继续写入");
                    if (b_istrue)
                    {
                        b_istrue = false;
                        FADM_Object.Communal._tcpModBus.ReConnect();
                        goto lableTop;
                    }
                    b_istrue = true;
                    goto lableTop;
                    //throw new Exception("10");
                }
                return 0;
            }
            catch (Exception e)
            {
                FADM_Object.Communal.WriteTcpStatus(true); //恢复
                if (e.Message.Equals("-2") || e.Message.Equals("10") || e.Message.Equals("未发现杯盖") || e.Message.Equals("发现杯盖或针筒") || e.Message.Equals("抓手A夹紧异常") || e.Message.Equals("抓手B夹紧异常"))
                {
                //    string msg = "";
                //    int[] ia_errArray = new int[100];
                //Label123:
                //    int i_state1 = MyModbusFun.GetErrMsgNew(ref ia_errArray);
                //    if (i_state1 == -1)
                //        goto Label123;
                //    for (int i = 0; i < ia_errArray.Length; i++)
                //    {
                //        if (ia_errArray[i] == 2702)
                //        {
                //            throw new Exception("未发现杯盖");
                //        }
                //        else if (ia_errArray[i] == 2701)
                //        {
                //            throw new Exception("发现杯盖或针筒");
                //        }

                //    }
                    throw e;
                }
                else
                {
                    throw new Exception("-1");
                }
            }
            finally
            {
                FADM_Object.Communal.WriteTcpStatus(true); //恢复
            }
        }

        /// <summary>
        /// 失能开关
        /// </summary>
        /// <returns></returns>
        public static int Power(int i_type)
        {
            try
            {
            Label1:
                if (!FADM_Object.Communal._b_auto)
                {//在手动页面 等待手动页面退出
                    goto Label1;
                }
                Lib_Log.Log.writeLogException("失能" + i_type);

                bool b_istrue = false;
                FADM_Object.Communal.WriteTcpStatus(false); //天平先不要轮询

            lableTop:
                int[] ia_array = new int[1];
                ia_array[0] = i_type;
                int i_state = FADM_Object.Communal._tcpModBus.Write(808, ia_array);
                if (i_state != -1)
                {
                    //写入成功  
                }
                else
                {
                    Lib_Log.Log.writeLogException("失能" + i_type + "写入失败");
                    Console.WriteLine("失能" + i_type + "写入失败继续写入");
                    if (b_istrue)
                    {
                        b_istrue = false;
                        FADM_Object.Communal._tcpModBus.ReConnect();
                        goto lableTop;
                    }
                    b_istrue = true;
                    goto lableTop;
                    //throw new Exception("10");
                }
                return 0;
            }
            catch (Exception e)
            {
                FADM_Object.Communal.WriteTcpStatus(true); //恢复
                if (e.Message.Equals("-2") || e.Message.Equals("10") || e.Message.Equals("未发现杯盖") || e.Message.Equals("发现杯盖或针筒") || e.Message.Equals("抓手A夹紧异常") || e.Message.Equals("抓手B夹紧异常"))
                {
                    throw e;
                }
                else
                {
                    throw new Exception("-1");
                }
            }
            finally
            {
                FADM_Object.Communal.WriteTcpStatus(true); //恢复
            }
        }

        /// <summary>
        /// 开盖
        /// </summary>
        /// <returns></returns>
        public static int OpenCover()
        {
            try
            {
            Label1:
                if (!FADM_Object.Communal._b_auto)
                {//在手动页面 等待手动页面退出
                    goto Label1;
                }
                Lib_Log.Log.writeLogException("执行开盖方法");

                bool b_istrue = false;
                FADM_Object.Communal.WriteTcpStatus(false); //天平先不要轮询
                ClearSuccessState();//先清除标志位
                Lib_Log.Log.writeLogException("清除标志位结束");

            lableTop:
                int[] ia_array = { 11, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 };
                int i_state = FADM_Object.Communal._tcpModBus.Write(800, ia_array);
                if (i_state != -1)
                {
                    //判断错误返回值
                    if (GetReturn(1) == -2)
                    {
                        return -2;
                    }
                    else
                    {

                    }
                }
                else
                {
                    Lib_Log.Log.writeLogException("写入开盖动作编号返回失败,继续写入");
                    Console.WriteLine("开盖编号返回失败,继续写入");
                    if (b_istrue)
                    {
                        b_istrue = false;
                        FADM_Object.Communal._tcpModBus.ReConnect();
                        goto lableTop;
                    }
                    b_istrue = true;
                    goto lableTop;
                    //throw new Exception("10");
                }
                return 0;
            }
            catch (Exception e)
            {
                FADM_Object.Communal.WriteTcpStatus(true); //恢复
                if (e.Message.Equals("-2") || e.Message.Equals("10") || e.Message.Equals("未发现杯盖") || e.Message.Equals("发现杯盖或针筒") || e.Message.Equals("抓手A夹紧异常") || e.Message.Equals("抓手B夹紧异常"))
                {
                    int[] ia_errArray = new int[100];
                Label123:
                    int i_state1 = MyModbusFun.GetErrMsgNew(ref ia_errArray);
                    if (i_state1 == -1)
                        goto Label123;
                    for (int i = 0; i < ia_errArray.Length; i++)
                    {
                        if (ia_errArray[i] == 2702)
                        {
                            throw new Exception("未发现杯盖");
                        }
                        else if (ia_errArray[i] == 2701)
                        {
                            throw new Exception("发现杯盖或针筒");
                        }

                    }
                    throw e;
                }
                else
                {
                    throw new Exception("-1");
                }
            }
            finally
            {
                FADM_Object.Communal.WriteTcpStatus(true); //恢复
            }
        }

        /// <summary>
        /// 拿夹子 i_type 1:干布 2：湿布
        /// </summary>
        /// <returns></returns>
        public static int GetClamp( int i_xStartPules, int i_yStartPules,int i_type)
        {
            try
            {
            Label1:
                if (!FADM_Object.Communal._b_auto)
                {//在手动页面 等待手动页面退出
                    goto Label1;
                }
                Lib_Log.Log.writeLogException("执行拿夹子方法");

                bool b_istrue = false;
                FADM_Object.Communal.WriteTcpStatus(false); //天平先不要轮询
                ClearSuccessState();//先清除标志位
                Lib_Log.Log.writeLogException("清除标志位结束");

            lableTop:
                int d_1 = 0;
                if (i_xStartPules > 65536)
                {
                    d_1 = i_xStartPules / 65536;
                    i_xStartPules = i_xStartPules % 65536;
                }
                int d_2 = 0;
                if (i_yStartPules > 65536)
                {
                    d_2 = i_yStartPules / 65536;
                    i_yStartPules = i_yStartPules % 65536;
                }

                int[] ia_array = { 14, i_xStartPules, d_1, i_yStartPules, d_2, 0, 0, 0, 0, 0, 1 };
                int i_state = FADM_Object.Communal._tcpModBus.Write(800, ia_array);
                if (i_state != -1)
                {
                    //判断错误返回值
                    if (GetReturn(1) == -2)
                    {
                        return -2;
                    }
                    else
                    {

                    }
                }
                else
                {
                    Lib_Log.Log.writeLogException("写入拿夹子动作编号返回失败,继续写入");
                    Console.WriteLine("拿夹子编号返回失败,继续写入");
                    if (b_istrue)
                    {
                        b_istrue = false;
                        FADM_Object.Communal._tcpModBus.ReConnect();
                        goto lableTop;
                    }
                    b_istrue = true;
                    goto lableTop;
                    //throw new Exception("10");
                }

                if(i_type ==1)
                {
                    FADM_Object.Communal._b_isGetDryClamp = true;
                    FADM_Object.Communal._b_isGetWetClamp = false;
                }
                else
                {
                    FADM_Object.Communal._b_isGetWetClamp = true;
                    FADM_Object.Communal._b_isGetDryClamp = false;
                }
                return 0;
            }
            catch (Exception e)
            {
                FADM_Object.Communal.WriteTcpStatus(true); //恢复
                if (e.Message.Equals("-2") || e.Message.Equals("10") || e.Message.Equals("未发现杯盖") || e.Message.Equals("发现杯盖或针筒") || e.Message.Equals("抓手A夹紧异常") || e.Message.Equals("抓手B夹紧异常"))
                {
                    int[] ia_errArray = new int[100];
                Label123:
                    int i_state1 = MyModbusFun.GetErrMsgNew(ref ia_errArray);
                    if (i_state1 == -1)
                        goto Label123;
                    for (int i = 0; i < ia_errArray.Length; i++)
                    {
                        if (ia_errArray[i] == 4501)
                        {
                            throw new Exception("未发现抓手");
                        }
                        else if (ia_errArray[i] == 2701)
                        {
                            throw new Exception("发现杯盖或针筒");
                        }

                    }
                    throw e;
                }
                else
                {
                    throw new Exception("-1");
                }
            }
            finally
            {
                FADM_Object.Communal.WriteTcpStatus(true); //恢复
            }
        }

        /// <summary>
        /// 放夹子 1:干布夹子 2:湿布夹子
        /// </summary>
        /// <returns></returns>
        public static int PutClamp(int i_xStartPules, int i_yStartPules)
        {
            try
            {
            Label1:
                if (!FADM_Object.Communal._b_auto)
                {//在手动页面 等待手动页面退出
                    goto Label1;
                }
                Lib_Log.Log.writeLogException("执行放盖方法");

                bool b_istrue = false;
                FADM_Object.Communal.WriteTcpStatus(false); //天平先不要轮询
                ClearSuccessState();//先清除标志位
                Lib_Log.Log.writeLogException("清除标志位结束");

            lableTop:
                int d_1 = 0;
                if (i_xStartPules > 65536)
                {
                    d_1 = i_xStartPules / 65536;
                    i_xStartPules = i_xStartPules % 65536;
                }
                int d_2 = 0;
                if (i_yStartPules > 65536)
                {
                    d_2 = i_yStartPules / 65536;
                    i_yStartPules = i_yStartPules % 65536;
                }
                int[] ia_array = { 15, i_xStartPules, d_1, i_yStartPules, d_2, 0, 0, 0, 0, 0, 1 };
                int i_state = FADM_Object.Communal._tcpModBus.Write(800, ia_array);
                if (i_state != -1)
                {
                    //判断错误返回值
                    if (GetReturn(1) == -2)
                    {
                        return -2;
                    }
                    else
                    {

                    }
                }
                else
                {
                    Lib_Log.Log.writeLogException("写入放盖动作编号返回失败,继续写入");
                    Console.WriteLine("放盖编号返回失败,继续写入");
                    if (b_istrue)
                    {
                        b_istrue = false;
                        FADM_Object.Communal._tcpModBus.ReConnect();
                        goto lableTop;
                    }
                    b_istrue = true;
                    goto lableTop;
                    //throw new Exception("10");
                }
                //把抓夹子状态置为false
                FADM_Object.Communal._b_isGetDryClamp = false;
                FADM_Object.Communal._b_isGetWetClamp = false;
                return 0;
            }
            catch (Exception e)
            {
                FADM_Object.Communal.WriteTcpStatus(true); //恢复
                if (e.Message.Equals("-2") || e.Message.Equals("10") || e.Message.Equals("未发现杯盖") || e.Message.Equals("发现杯盖或针筒") || e.Message.Equals("抓手A夹紧异常") || e.Message.Equals("抓手B夹紧异常"))
                {
                    throw e;
                }
                else
                {
                    throw new Exception("-1");
                }
            }
            finally
            {
                FADM_Object.Communal.WriteTcpStatus(true); //恢复
            }
        }


        /// <summary>
        /// 读取异常信息地址
        /// </summary>
        /// <returns>返回异常编号和信息</returns>
        public static int GetErrMsg(ref string[] sa_rarray)
        {
            try
            {
                Lib_Log.Log.writeLogException("读取异常信息地址");
                Console.WriteLine("读取异常信息地址");
                bool b_istrue = false;
                FADM_Object.Communal.WriteTcpStatus(false); //天平先不要轮询
            lableTop:
                int[] ia_array = { 0 };
                int i_state = FADM_Object.Communal._tcpModBus.Read(900, 1, ref ia_array);
                if (i_state != -1)
                {
                    Console.WriteLine("异常是" + ia_array[0].ToString());
                    Lib_Log.Log.writeLogException("异常是" + ia_array[0].ToString());
                    if (ia_array[0] != 0 && SmartDyeing.FADM_Object.Communal._dic_errModbusNo.ContainsKey(ia_array[0]))
                    {
                        string s_message = SmartDyeing.FADM_Object.Communal._dic_errModbusNo[ia_array[0]];
                        sa_rarray[0] = ia_array[0].ToString();
                        sa_rarray[1] = s_message;
                    }
                    else
                    {
                        Console.WriteLine("未知异常");
                        Lib_Log.Log.writeLogException("未知异常");

                        sa_rarray[0] = "0";
                        sa_rarray[1] = "未知异常";
                    }

                }
                else
                {
                    if (b_istrue)
                    {
                        b_istrue = false;
                        FADM_Object.Communal._tcpModBus.ReConnect();
                        goto lableTop;
                    }
                    b_istrue = true;
                    goto lableTop;
                }
                Lib_Log.Log.writeLogException("读取异常信息地址 结束");
                return i_state;
            }
            catch (Exception e)
            {
                FADM_Object.Communal.WriteTcpStatus(true); //恢复
                return -1;
            }
            finally
            {
                FADM_Object.Communal.WriteTcpStatus(true); //恢复
            }
        }

        /// <summary>
        /// 读取异常信息地址
        /// </summary>
        /// <returns>返回异常编号和信息</returns>
        public static int GetErrMsgNew(ref int[] ia_array)
        {
            try
            {
                Lib_Log.Log.writeLogException("读取异常信息地址");
                Console.WriteLine("读取异常信息地址");
                bool b_istrue = false;
            //FADM_Object.Communal.WriteTcpStatus(false); //天平先不要轮询

            lableTop:
                int i_state = FADM_Object.Communal._tcpModBus.Read(3000, 100, ref ia_array);
                if (i_state != -1)
                {
                    Lib_Log.Log.writeLogException("异常读取成功");
                    //if (ia_array[0] != 0 && SmartDyeing.FADM_Object.Communal._dic_errModbusNo.ContainsKey(ia_array[0]))
                    //{
                    //    string s_message = SmartDyeing.FADM_Object.Communal._dic_errModbusNo[ia_array[0]];
                    //    sa_rarray[0] = ia_array[0].ToString();
                    //    sa_rarray[1] = s_message;
                    //}
                    //else
                    //{
                    //    Console.WriteLine("未知异常");
                    //    Lib_Log.Log.writeLogException("未知异常");

                    //    sa_rarray[0] = "0";
                    //    sa_rarray[1] = "未知异常";
                    //}

                }
                else
                {
                    if (b_istrue)
                    {
                        b_istrue = false;
                        FADM_Object.Communal._tcpModBus.ReConnect();
                        goto lableTop;
                    }
                    b_istrue = true;
                    goto lableTop;
                }
                Lib_Log.Log.writeLogException("读取异常信息地址 结束");
                return i_state;
            }
            catch (Exception e)
            {
                //FADM_Object.Communal.WriteTcpStatus(true); //恢复
                return -1;
            }
            finally
            {
                //FADM_Object.Communal.WriteTcpStatus(true); //恢复
            }
        }

        /// <summary>
        /// 读取Z轴逻辑位置
        /// </summary>
        /// <returns></returns>
        public static int GetZPosition(ref int i_position)
        {
            try
            {
            Label1:
                if (!FADM_Object.Communal._b_auto)
                {//在手动页面 等待手动页面退出
                    goto Label1;
                }
                Lib_Log.Log.writeLogException("读取Z轴逻辑位置");

                bool b_istrue = false;
                FADM_Object.Communal.WriteTcpStatus(false); //天平先不要轮询
                double d_value = 0.0;

            lableTop:
                int[] ia_array = { 0, 0 };
                int i_state = FADM_Object.Communal._tcpModBus.Read(913, 2, ref ia_array);
                if (i_state != -1)
                {
                    int i_a13 = ia_array[0];
                    int i_a14 = ia_array[1];
                    if (i_a13 < 0)
                    {
                        d_value = (((i_a14 + 1) * 65536 + i_a13));
                    }
                    else
                    {
                        d_value = ((i_a14 * 65536 + i_a13));
                    }
                    Console.WriteLine("读取Z轴位置是" + d_value.ToString());
                    //i_position = d_value < 0 ? Convert.ToInt32(Math.Floor(d_value)) : Convert.ToInt32(Math.Ceiling(d_value));
                    i_position = Convert.ToInt32(d_value);
                    Console.WriteLine("读取Z轴位置是" + i_position.ToString());
                    Lib_Log.Log.writeLogException("读取Z轴位置是" + i_position.ToString());
                }
                else
                {
                    if (b_istrue)
                    {
                        b_istrue = false;
                        FADM_Object.Communal._tcpModBus.ReConnect();
                        goto lableTop;
                    }
                    b_istrue = true;
                    goto lableTop;
                }
                Lib_Log.Log.writeLogException("读取Z轴位置结束!");
                return 0;
            }
            catch (Exception e)
            {
                FADM_Object.Communal.WriteTcpStatus(true); //恢复
                return -1;
            }
            finally
            {
                FADM_Object.Communal.WriteTcpStatus(true); //恢复
            }

        }

        /// <summary>
        /// 计算加水时间
        /// </summary>
        /// <returns></returns>
        public static double GetWaterTime(double d_water)
        {
            //计算加水时间
            double d_blTime = 0;
            if (0 == FADM_Object.Communal._i_waterWay)
            {
                if (Lib_Card.Configure.Parameter.Correcting_Water_RWeight > Lib_Card.Configure.Parameter.Correcting_Water_FWeight)
                    d_blTime = (Lib_Card.Configure.Parameter.Correcting_Water_RWeight - Lib_Card.Configure.Parameter.Correcting_Water_FWeight) / Lib_Card.Configure.Parameter.Correcting_Water_Value + 1;
                else
                {
                    d_blTime = Lib_Card.Configure.Parameter.Correcting_Water_RWeight / Lib_Card.Configure.Parameter.Correcting_Water_Value;
                    if (d_blTime < 0.01)
                        d_blTime = 0.01;
                }
            }
            else
            {
                if (d_water > 13)
                    d_blTime = (d_water - Lib_Card.Configure.Parameter.Correcting_Water_FWeight) / Lib_Card.Configure.Parameter.Correcting_Water_Value + 1;
                else
                    d_blTime = d_water / Lib_Card.Configure.Parameter.Correcting_Water_Value + Lib_Card.Configure.Parameter.Other_Coefficient_Water;
            }
            return d_blTime;
        }

        /// <summary>
        /// 清除执行完成标志位 改为0
        /// </summary>
        public static void ClearSuccessState()
        {
            Lib_Log.Log.writeLogException("执行清除标志位");
            bool b_istrue = false;
            FADM_Object.Communal.WriteTcpStatus(false); //恢复
        //清掉执行完成 标志位
        label2:
            int[] ia_array3 = { 0 };
            int istate = FADM_Object.Communal._tcpModBus.Write(904, ia_array3);
            if (istate != -1)
            {
                //清除标志位成功 重新读一下
                istate = FADM_Object.Communal._tcpModBus.Read(904, 1, ref ia_array3);
                if (istate != -1 && ia_array3[0] == 0)
                {
                    return;
                }
                else
                {
                    goto label2;
                }
            }
            else
            {
                if (b_istrue)
                {
                    b_istrue = false;
                    FADM_Object.Communal._tcpModBus.ReConnect();
                    goto label2;
                }
                b_istrue = true;
                goto label2;
            }
        }
        /// <summary>
        /// 一直读取等待标志位和光幕急停
        /// </summary>
        public static int[] AwaitSuccess()
        {
            bool b_istrue = false;
            int[] ia_array = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0 };
            int i_state = 0;

        Label1:

            i_state = FADM_Object.Communal._tcpModBus.Read(900, 29, ref ia_array);

            if (i_state != -1)
            {

                Communal._ia_d900 = ia_array;

                Communal._b_isUpdateNewData = true;

                Communal._s_plcVersion = "V" + ia_array[27].ToString("d4") + ia_array[28].ToString("d4");

                double d_b = 0.0;
                if (ia_array[1] < 0)
                {
                    d_b = ((ia_array[2] + 1) * 65536 + ia_array[1]) / 1000.0;
                }
                else
                {
                    d_b = (ia_array[2] * 65536 + ia_array[1]) / 1000.0;
                }
                //if (Lib_Card.Configure.Parameter.Machine_BalanceType == 0)
                //{
                //    d_b /= 10;
                //}


                if (Convert.ToString(d_b).Equals("9999"))
                {
                    FADM_Object.Communal._s_balanceValue = "9999";
                }
                else if (Convert.ToString(d_b).Equals("8888"))
                {
                    FADM_Object.Communal._s_balanceValue = "8888";
                }
                else if (Convert.ToString(d_b).Equals("7777"))
                {
                    FADM_Object.Communal._s_balanceValue = "7777";
                }
                else if (Convert.ToString(d_b).Equals("6666"))
                {
                    FADM_Object.Communal._s_balanceValue = "6666";
                }
                else
                {
                    FADM_Object.Communal._s_balanceValue = Convert.ToString(d_b);
                }

                //光幕信号 这上面的MyAlarm不要等待
                int i_a2 = ia_array[3];
                char[] ca_cc = Convert.ToString(i_a2, 2).PadLeft(3, '0').ToArray();
                if (ca_cc[ca_cc.Length - 1].Equals('0'))
                {
                    if (Lib_Card.Configure.Parameter.Machine_IsStopOrFront == 0)
                        Lib_Card.CardObject.bStopScr = true;
                    else
                        Lib_Card.CardObject.bFront = true;
                }
                else
                {
                    if (Lib_Card.Configure.Parameter.Machine_IsStopOrFront == 0)
                        Lib_Card.CardObject.bStopScr = false;
                    else
                        Lib_Card.CardObject.bFront = false;
                }

                if (ca_cc[ca_cc.Length - 3].Equals('0'))
                {
                    Lib_Card.CardObject.bRight = true;
                }
                else
                {
                    Lib_Card.CardObject.bRight = false;
                }

                if (ca_cc[ca_cc.Length - 2].Equals('0'))
                {
                    Lib_Card.CardObject.bLeft = true;
                }
                else
                {
                    Lib_Card.CardObject.bLeft = false;
                }

            }
            else
            {
                Console.WriteLine("TCP返回值-1\n");
                if (b_istrue)
                {
                    b_istrue = false;
                    FADM_Object.Communal._tcpModBus.ReConnect();
                    goto Label1;
                }
                b_istrue = true;
                goto Label1;
            }
            return ia_array;
        }


        /// <summary>
        /// 设置批次启动
        /// </summary>
        public static void SetBatchStart()
        {
            bool b_istrue = false;
            FADM_Object.Communal.WriteTcpStatus(false); //恢复
        //清掉执行完成 标志位
        label2:
            int[] ia_array3 = { 1 };
            int i_state = FADM_Object.Communal._tcpModBus.Write(808, ia_array3);
            if (i_state != -1)
            {
                //清除标志位成功 重新读一下
                i_state = FADM_Object.Communal._tcpModBus.Read(808, 1, ref ia_array3);
                if (i_state != -1 && ia_array3[0] == 1)
                {
                    FADM_Object.Communal.WriteTcpStatus(true); //恢复
                    return;
                }
                else
                {
                    goto label2;
                }
            }
            else
            {
                if (b_istrue)
                {
                    b_istrue = false;
                    FADM_Object.Communal._tcpModBus.ReConnect();
                    goto label2;
                }
                b_istrue = true;
                goto label2;
            }
        }
        /// <summary>
        /// 设置批次关闭
        /// </summary>
        public static void SetBatchClose()
        {
            bool b_istrue = false;
            FADM_Object.Communal.WriteTcpStatus(false); //恢复
        //清掉执行完成 标志位
        label2:
            int[] ia_array3 = { 2 };
            int i_state = FADM_Object.Communal._tcpModBus.Write(808, ia_array3);
            if (i_state != -1)
            {
                //清除标志位成功 重新读一下
                i_state = FADM_Object.Communal._tcpModBus.Read(808, 1, ref ia_array3);
                if (i_state != -1 && ia_array3[0] == 2)
                {
                    FADM_Object.Communal.WriteTcpStatus(true); //恢复
                    return;
                }
                else
                {
                    goto label2;
                }
            }
            else
            {
                if (b_istrue)
                {
                    b_istrue = false;
                    FADM_Object.Communal._tcpModBus.ReConnect();
                    goto label2;
                }
                b_istrue = true;
                goto label2;
            }
        }


        /// <summary>
        /// 复位
        /// </summary>
        public static int MyMachineReset()
        {
            try
            {
            Label1:
                if (!FADM_Object.Communal._b_auto)
                {//在手动页面 等待手动页面退出
                    goto Label1;
                }
                Lib_Log.Log.writeLogException("执行复位");
                bool b_istrue = false;
                FADM_Object.Communal.WriteTcpStatus(false); //天平先不要轮询
                ClearSuccessState();//先清除标志位

            lableTop:

                int i_xPules = 0;
                int i_yPules = 0;

                if (Lib_Card.Configure.Parameter.Other_IsOnlyDrip == 1 || (!Communal._b_isBalanceInDrip))
                {
                    i_xPules = Lib_Card.Configure.Parameter.Coordinate_Bottle_X - (FADM_Object.Communal._i_optBottleNum - 1) %
                    Lib_Card.Configure.Parameter.Machine_Bottle_Column * Lib_Card.Configure.Parameter.Coordinate_Bottle_Interval;
                    i_yPules = Lib_Card.Configure.Parameter.Coordinate_Bottle_Y + (FADM_Object.Communal._i_optBottleNum - 1) /
                        Lib_Card.Configure.Parameter.Machine_Bottle_Column * Lib_Card.Configure.Parameter.Coordinate_Bottle_Interval;
                }
                else
                {
                    int iNo = FADM_Object.Communal._i_optBottleNum;
                    if (Lib_Card.Configure.Parameter.Machine_Bottle_Total - 14 >= iNo)
                    {
                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Bottle_X - (iNo - 1) %
                            Lib_Card.Configure.Parameter.Machine_Bottle_Column * Lib_Card.Configure.Parameter.Coordinate_Bottle_Interval;
                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Bottle_Y + (iNo - 1) /
                            Lib_Card.Configure.Parameter.Machine_Bottle_Column * Lib_Card.Configure.Parameter.Coordinate_Bottle_Interval;
                    }
                    else if (Lib_Card.Configure.Parameter.Machine_Bottle_Total - 7 >= iNo)
                    {
                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Bottle_X -
                            ((iNo + 14 - Lib_Card.Configure.Parameter.Machine_Bottle_Total) % 8 + 2)
                            * Lib_Card.Configure.Parameter.Coordinate_Bottle_Interval;
                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Bottle_Y +
                            ((Lib_Card.Configure.Parameter.Machine_Bottle_Total - 14) /
                            Lib_Card.Configure.Parameter.Machine_Bottle_Column +
                            (iNo + 14 - Lib_Card.Configure.Parameter.Machine_Bottle_Total) / 8)
                            * Lib_Card.Configure.Parameter.Coordinate_Bottle_Interval;


                    }
                    else
                    {
                        i_xPules = Lib_Card.Configure.Parameter.Coordinate_Bottle_X -
                           ((iNo + 14 - Lib_Card.Configure.Parameter.Machine_Bottle_Total) % 8 + 3)
                           * Lib_Card.Configure.Parameter.Coordinate_Bottle_Interval;
                        i_yPules = Lib_Card.Configure.Parameter.Coordinate_Bottle_Y +
                            ((Lib_Card.Configure.Parameter.Machine_Bottle_Total - 14) /
                            Lib_Card.Configure.Parameter.Machine_Bottle_Column +
                            (iNo + 14 - Lib_Card.Configure.Parameter.Machine_Bottle_Total) / 8)
                            * Lib_Card.Configure.Parameter.Coordinate_Bottle_Interval;
                    }
                }
                if (FADM_Object.Communal._i_optBottleNum == 0)
                {
                    FADM_Object.Communal._fadmSqlserver.InsertRun("RobotHand", "复位完成");
                    FADM_Object.Communal.WriteMachineStatus(0);
                    FADM_Object.Communal.WriteTcpStatus(true); //天平先不要轮询
                    return 0;
                }
                int d_1 = 0;
                if (i_xPules > 65536)
                {
                    d_1 = i_xPules / 65536;
                    i_xPules = i_xPules % 65536;
                }
                int d_2 = 0;
                if (i_yPules > 65536)
                {
                    d_2 = i_yPules / 65536;
                    i_yPules = i_yPules % 65536;
                }
                int[] ia_array = { 9, i_xPules, d_1, i_yPules, d_2, 0, 0, 0, 0, 0, 1 };
                int i_state = FADM_Object.Communal._tcpModBus.Write(800, ia_array);
                if (i_state != -1)
                {

                    //判断错误返回值
                    if (GetReturn(0) == -2)
                    {
                        return -2;
                    }
                    else
                    {
                        FADM_Object.Communal._fadmSqlserver.InsertRun("RobotHand", "复位完成");
                        FADM_Object.Communal.WriteMachineStatus(0);
                    }

                }
                else
                {
                    Console.WriteLine("复位编号返回失败,继续写入");
                    if (b_istrue)
                    {
                        b_istrue = false;
                        FADM_Object.Communal._tcpModBus.ReConnect();
                        goto lableTop;
                    }
                    b_istrue = true;
                    goto lableTop;
                    //throw new Exception("10");
                }
                return 0;
            }
            catch (Exception e)
            {
                FADM_Object.Communal.WriteTcpStatus(true); //恢复
                if (e.Message.Equals("-2") || e.Message.Equals("10"))
                {
                    throw e;
                }
                else
                {
                    throw new Exception("-1");
                }
            }
            finally
            {
                FADM_Object.Communal.WriteTcpStatus(true); //恢复
            }


        }



        /// <summary>
        /// 相对移动
        /// </summary>
        /// <param name="i_axis">轴 1.X轴 2.Y轴 3.Z轴</param>
        /// <param name="iPosition">脉冲</param>
        /// <returns></returns>
        public static int TargetMoveRelative(int i_axis, int i_pules, int i_hSpeed, int i_uTime)
        {
            try
            {
                Console.WriteLine("开始相对移动");
                bool b_istrue = false;
                FADM_Object.Communal.WriteTcpStatus(false); //天平先不要轮询
                if (!FADM_Object.Communal._b_auto)
                {
                    for (int i = 0; i < Communal._ia_d900.Length; i++) { Communal._ia_d900[i] = 0; }
                    Communal._b_isUpdateNewData = false;
                    //等待调试页面停止刷新
                    Thread.Sleep(200);
                }
                ClearSuccessState();//先清除标志位
                Console.WriteLine("清除完标志位");
            lableTop:
                /*if (!FADM_Object.Communal._b_auto) {//在手动页面 等待手动页面退出
                    goto Label1;
                }*/
                int[] ia_array;

                int d_1 = 0;
                d_1 = i_pules / 65536;
                if (i_pules < 0) //负数脉冲
                {
                    if (d_1 == 0)
                    {
                        d_1 = -1;
                    }
                    else
                    {
                        if (Math.Abs(i_pules) > 65536)
                        {
                            d_1 = d_1 + -1;
                        }
                    }
                }
                else
                {  //正数脉冲
                    d_1 = i_pules / 65536;
                }
                i_pules = i_pules % 65536;


                int d_2 = 0;
                if (i_hSpeed > 65536)
                {
                    d_2 = i_hSpeed / 65536;
                    i_hSpeed = i_hSpeed % 65536;
                }
                int d_3 = 0;
                if (i_uTime > 65536)
                {
                    d_3 = i_uTime / 65536;
                    i_uTime = i_uTime % 65536;
                }


                if (i_axis == 1)
                {
                    ia_array = new int[] { 8, i_pules, d_1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, i_hSpeed, d_2, i_uTime, d_3 };
                }
                else if (i_axis == 2)
                {
                    ia_array = new int[] { 8, 0, 0, i_pules, d_1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, i_hSpeed, d_2, i_uTime, d_3 };
                }
                else
                {
                    ia_array = new int[] { 8, 0, 0, 0, 0, i_pules, d_1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, i_hSpeed, d_2, i_uTime, d_3 };
                }
                int i_state = FADM_Object.Communal._tcpModBus.Write(800, ia_array);
                if (i_state != -1)
                {
                    //判断错误返回值
                    if (GetReturn(1) == -2)
                    {
                        return -2;
                    }
                    else
                    {

                    }
                }
                else
                {
                    Console.WriteLine("写入相对移动编号返回失败,继续写入");
                    if (b_istrue)
                    {
                        b_istrue = false;
                        FADM_Object.Communal._tcpModBus.ReConnect();
                        goto lableTop;
                    }
                    b_istrue = true;
                    goto lableTop;
                }
                return 0;
            }
            catch (Exception e)
            {
                FADM_Object.Communal.WriteTcpStatus(true); //恢复
                if (e.Message.Equals("-2"))
                {
                    throw e;
                }
                else
                {
                    MessageBox.Show(e.ToString());
                    throw new Exception("-1");
                }
            }
            finally
            {
                FADM_Object.Communal.WriteTcpStatus(true); //恢复
            }
        }




        /// <summary>
        /// 检查针筒
        /// </summary>
        /// <returns></returns>
        public static int Reset()
        {
            try
            {
                Lib_Log.Log.writeLogException("检查针筒方法");

                bool b_istrue = false;
                FADM_Object.Communal.WriteTcpStatus(false); //天平先不要轮询 
                ClearSuccessState();//先清除标志位
                Lib_Log.Log.writeLogException("清除标志位结束");

            lableTop:
                int[] ia_array = { 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 };
                int i_state = FADM_Object.Communal._tcpModBus.Write(800, ia_array);
                if (i_state != -1)
                {
                    //判断错误返回值
                    if (GetReturn(1) == -2)
                    {
                        return -2;
                    }
                    else
                    {

                    }
                }
                else
                {
                    Lib_Log.Log.writeLogException("写入检查复位动作编号返回失败,继续写入");
                    Console.WriteLine("写入检查复位动作编号返回失败,继续写入");
                    if (b_istrue)
                    {
                        b_istrue = false;
                        FADM_Object.Communal._tcpModBus.ReConnect();
                        goto lableTop;
                    }
                    b_istrue = true;
                    goto lableTop;
                }
                return 0;
            }
            catch (Exception e)
            {
                FADM_Object.Communal.WriteTcpStatus(true); //恢复
                if (e.Message.Equals("-2"))
                {
                    throw e;
                }
                else
                {
                    throw new Exception("-1");
                }
            }
            finally
            {
                FADM_Object.Communal.WriteTcpStatus(true); //恢复
            }
        }

        /// <summary>
        /// 检查针筒
        /// </summary>
        /// <returns></returns>
        public static int ResetClosing()
        {
            try
            {
                Lib_Log.Log.writeLogException("检查针筒方法");

                bool istrue = false;
                FADM_Object.Communal.WriteTcpStatus(false); //天平先不要轮询 
                ClearSuccessState();//先清除标志位
                Lib_Log.Log.writeLogException("清除标志位结束");

            lableTop:
                int[] ia_array = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                int i_state = 0;
                i_state = FADM_Object.Communal._tcpModBus.Read(900, 26, ref ia_array);
                if (i_state != -1)
                {
                    int a5 = ia_array[5]; //输入点(双字)
                    string str2 = Convert.ToString(a5, 2).PadLeft(15, '0');
                    if (!str2.Equals("0"))
                    {
                        char[]cc = str2.ToArray();//急停
                        if(cc[cc.Length - 6].Equals('1') ? true : false) //针筒传感器
                        {
                            throw new Exception("发现针筒或杯盖");
                        }
                    }
                }
                else
                {
                    goto lableTop;
                }
                return 0;
            }
            catch (Exception e)
            {
                FADM_Object.Communal.WriteTcpStatus(true); //恢复
                if (e.Message.Equals("-2")|| e.Message.Equals("发现针筒或杯盖"))
                {
                    throw e;
                }
                else
                {
                    throw new Exception("-1");
                }
            }
            finally
            {
                FADM_Object.Communal.WriteTcpStatus(true); //恢复
            }
        }



        /// <summary>
        /// 清除动作错误编号
        /// </summary>
        /// <returns></returns>
        public static void ClearError()
        {
            Lib_Log.Log.writeLogException("清除动作错误编号");
            bool b_istrue = false;
            FADM_Object.Communal.WriteTcpStatus(false); //恢复
        //清掉执行完成 标志位
        label2:
            int[] ia_array3 = { 0 };
            int i_state = FADM_Object.Communal._tcpModBus.Write(900, ia_array3);
            if (i_state != -1)
            {
                //清除标志位成功 重新读一下
                i_state = FADM_Object.Communal._tcpModBus.Read(900, 1, ref ia_array3);
                if (i_state != -1 && ia_array3[0] == 0)
                {
                    FADM_Object.Communal.WriteTcpStatus(true); //恢复
                    return;
                }
                else
                {
                    goto label2;
                }
            }
            else
            {
                if (b_istrue)
                {
                    b_istrue = false;
                    FADM_Object.Communal._tcpModBus.ReConnect();
                    goto label2;
                }
                b_istrue = true;
                goto label2;
            }

        }


        /// <summary>
        /// 天平清零 发送指令成功 就退出此方法 。后面有方法判断是否是0
        /// </summary>
        /// <returns></returns>
        public static void ClearBalance()
        {
            bool b_istrue = false;
            FADM_Object.Communal.WriteTcpStatus(false);
        //清掉执行完成 标志位
        label2:
            int[] ia_array3 = { 29 };
            int i_state = FADM_Object.Communal._tcpModBus.Write(811, ia_array3);
            if (i_state != -1)
            {
                FADM_Object.Communal.WriteTcpStatus(true);//恢复
                /*FADM_Object.Communal.WriteTcpStatus(true); //恢复
              //读天平数据看下是否等于0
            lable3:
                if (Convert.ToDouble(FADM_Object.Communal._s_balanceValue) == 0.00)
                {
                    return;
                }
                else {
                    goto lable3;
                }*/
            }
            else
            {
                if (b_istrue)
                {
                    b_istrue = false;
                    FADM_Object.Communal._tcpModBus.ReConnect();
                    goto label2;
                }
                b_istrue = true;
                goto label2;
            }
        }



        /// <summary>
        /// 天平复位
        /// </summary>
        /// <returns></returns>
        public static void ResetBalance()
        {
            bool b_istrue = false;
            FADM_Object.Communal.WriteTcpStatus(false);
        //清掉执行完成 标志位
        label2:
            int[] ia_array3 = { 30 };
            int i_state = FADM_Object.Communal._tcpModBus.Write(811, ia_array3);
            if (i_state != -1)
            {
                FADM_Object.Communal.WriteTcpStatus(true);//恢复
                /*FADM_Object.Communal.WriteTcpStatus(true); //恢复
              //读天平数据看下是否等于0
            lable3:
                if (Convert.ToDouble(FADM_Object.Communal._s_balanceValue) == 0.00)
                {
                    return;
                }
                else {
                    goto lable3;
                }*/
            }
            else
            {
                if (b_istrue)
                {
                    b_istrue = false;
                    FADM_Object.Communal._tcpModBus.ReConnect();
                    goto label2;
                }
                b_istrue = true;
                goto label2;
            }
        }

    }
}
