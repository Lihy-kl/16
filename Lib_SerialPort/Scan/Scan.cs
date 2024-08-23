using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lib_SerialPort.Scan
{
   public class Scan : SerialPortBase
    {
        /// <summary>
        /// 串口读取时状态返回
        /// -1:初始化状态
        /// 0：通讯成功
        /// 1：返回异常
        /// 2：通讯超时
        /// 3:未扫描到数据
        /// </summary>
        public static int nState { get; set; }

        public Scan() : base()
        {

        }

        //打开扫码
        public int Start()
        {
            lock (this)
            {
                try
                {
                    nState = -1;
                    bSuccess = false;
                    int time = 0;
label1:
                    datapool.Clear();
                    this.end();

                    datapool.Clear();
                    byte[] sent = new byte[14] { 0x5A, 0x00, 0x00, 0x08, 0x53, 0x52, 0x30, 0x33, 0x30, 0x33, 0x30, 0x31, 0x08, 0xA5 };

                    Write(sent);
                    System.Threading.Thread.Sleep(100);
                    ManualResetEvent P_mre = new ManualResetEvent(false);

                    Thread P_thd_time = new Thread(() =>
                    {
                        bool P_bl = true;
                        Thread P_thd_time_1 = new Thread(() =>
                        {
                            while (P_bl)
                            {

                                if (datapool.Count == 8)
                                {
                                    nState = 0;
                                    P_mre.Set();
                                    break;
                                }
                                else if(datapool.Count >=11)                                                                
                                {

                                    nState = 4;
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
                    if (nState == 0)
                    {
                        datapool.Clear();
                    }
                    else if(nState == 4)
                    {
                        datapool.RemoveRange(0, 8);
                        nState = 0;
                    }

                    //接收数据
                    if (nState == 0)
                    {
                        P_thd_time = new Thread(() => {
                            bool P_bl = true;
                            Thread P_thd_time_1 = new Thread(() =>
                            {
                                while (P_bl)
                                {
                                    if (datapool.Count > 2)
                                    {
                                        int lenght = Convert.ToInt32(datapool[1].ToString(), 16);
                                        if (datapool.Count == lenght + 2)
                                        {


                                            nState = 0;
                                            P_mre.Set();
                                            break;
                                        }
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
                                
                                    nState = 3;
                                    // 等待超时
                                    P_bl = false;
                                    P_thd_time_1.Join();
                               
                            }
                        });

                        // 启动线程
                        P_thd_time.Start();
                        P_thd_time.Join();

                        if (nState == 3)
                        {
                            if (time >= 3)
                            {
                                return -1;
                            }
                            else
                            {
                                nState = 0;
                                time++;
                                goto label1;
                            }
                        }


                        byte[] bytes = new byte[datapool.Count - 2];

                        for (int i = 0; i < datapool.Count - 2; i++)
                        {
                            bytes[i] = datapool[i + 2];
                        }
                        int num = Convert.ToInt32(Encoding.ASCII.GetString(bytes, 0, bytes.Length));




                        return num;

                    }
                    else
                    {
                        return -1;
                    }

                    // Thread.Sleep(50);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }


        public void end()
        {
            lock (this)
            {
                try
                {
                    nState = -1;
                    bSuccess = false;
                    datapool.Clear();

                    byte[] sent = new byte[14] { 0x5A, 0x00, 0x00, 0x08, 0x53, 0x52, 0x30, 0x33, 0x30, 0x33, 0x30, 0x30, 0x09, 0xA5 };

                    Write(sent);
                    System.Threading.Thread.Sleep(100);
                    ManualResetEvent P_mre = new ManualResetEvent(false);

                    Thread P_thd_time = new Thread(() =>
                    {
                        bool P_bl = true;
                        Thread P_thd_time_1 = new Thread(() =>
                        {
                            while (P_bl)
                            {
                                if (datapool.Count == 8)
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



                    // Thread.Sleep(50);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
