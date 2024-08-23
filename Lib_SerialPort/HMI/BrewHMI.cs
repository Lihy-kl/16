using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lib_SerialPort.HMI
{
    public class BrewHMI : SerialPortBase
    {
        /// <summary>
        /// 串口读取时状态返回
        /// -1:初始化状态
        /// 0：通讯成功
        /// 1：返回异常
        /// 2：通讯超时
        /// </summary>
        public static int nState { get; set; }

        public BrewHMI() : base()
        {

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
                    System.Threading.Thread.Sleep(100);
                    ManualResetEvent P_mre = new ManualResetEvent(false);

                    Thread P_thd_time = new Thread(() =>
                    {
                        bool P_bl = true;
                        Thread P_thd_time_1 = new Thread(() =>
                        {
                            while (P_bl)
                            {
                                //if (datapool.Count >= 3)
                                //{
                                //    if (datapool[0] == 0x01)
                                //    {
                                //        if (datapool[1] == 0x03)
                                //        {
                                //            int len = datapool[2];
                                //            if (datapool.Count == len + 5)
                                //            {
                                //                nState = 0;
                                //                P_mre.Set();
                                //                break;
                                //            }
                                //        }
                                //        else if ((datapool[1] == 0x06 || datapool[1] == 0x10) && datapool.Count == 8)
                                //        {
                                //            nState = 0;
                                //            P_mre.Set();
                                //            break;
                                //        }
                                //    }
                                //}
                                if(bSuccess)
                                {
                                    nState = 0;
                                    P_mre.Set();
                                    break;
                                }
                                Thread.Sleep(1);
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
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
