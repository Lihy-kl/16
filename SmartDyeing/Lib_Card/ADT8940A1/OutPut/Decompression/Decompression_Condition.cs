using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lib_Card.ADT8940A1.OutPut.Decompression
{
    public class Decompression_Condition : Decompression
    {
        public override int Decompression_Down()
        {
            /* 条件
             *    1：X轴未运行
             *    2：Y轴未运行
             *    3：X轴未报警
             *    4：Y轴未报警
             *    5：接液盘伸出状态
             */
            labelTop:
            bool bReset = false;
            int iDecompressionDown = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Decompression_Down);
            if (-1 == iDecompressionDown)
                return -1;
            //else if (1 == iDecompressionDown)
            //    return 0;
            else
            {
            lable:
                int iXStatus = CardObject.OA1.ReadAxisStatus(ADT8940A1_IO.Axis_X);
                if (-1 == iXStatus)
                    return -1;

                int iYStatus = CardObject.OA1.ReadAxisStatus(ADT8940A1_IO.Axis_Y);
                if (-1 == iYStatus)
                    return -1;



                int iXAlarm = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_X_Alarm);
                if (-1 == iXAlarm)
                    return -1;

                int iYAlarm = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Y_Alarm);
                if (-1 == iYAlarm)
                    return -1;

                int iTrayOut = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Tray_Out);
                if (-1 == iTrayOut)
                    return -1;
                else if (0 == iTrayOut)
                {
                    //接液盘出
                    Lib_Card.ADT8940A1.OutPut.Tray.Tray tray = new Lib_Card.ADT8940A1.OutPut.Tray.Tray_Condition();
                    if (-1 == tray.Tray_On())
                        return -1;
                    goto labelTop;
                }

                if (0 == iXStatus && 0 == iYStatus && 0 == iXAlarm &&
                    0 == iYAlarm && 1 == iTrayOut)
                {
                    int iRes = CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Decompression, 1);
                    if (-1 == iRes)
                        return -1;

                    bool bDelay = false;
                    Thread thread = new Thread(() =>
                    {
                        int iDelay = Convert.ToInt32(Configure.Parameter.Delay_Decompression * 1000.00);
                        Thread.Sleep(iDelay);
                        bDelay = true;
                    });
                    thread.Start();

                    string sPath = Environment.CurrentDirectory + "\\Config\\DataBase.ini";
                    Lib_DataBank.SQLServer.SQLServerCon con = new Lib_DataBank.SQLServer.SQLServerCon()
                    {
                        Server = Lib_File.Ini.GetIni("FADM", "Server", sPath),
                        Port = Lib_File.Ini.GetIni("FADM", "Port", sPath),
                        Database = Lib_File.Ini.GetIni("FADM", "Database", sPath),
                        UserName = Lib_File.Ini.GetIni("FADM", "UserName", sPath),
                        Password = Lib_File.Ini.GetIni("FADM", "Password", sPath)
                    };
                    Lib_DataBank.SQLServer sQLServer = new Lib_DataBank.SQLServer(con);
                    string s = null;
                    while (true)
                    {
                        Thread.Sleep(1);
                        iDecompressionDown = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Decompression_Down);
                        if (-1 == iDecompressionDown)
                            return -1;
                        else if (1 == iDecompressionDown)
                            break;
                        if (bDelay)
                        {
                            //s = CardObject.InsertD("泄压气缸下超时", "Decompression_Down");
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                s = CardObject.InsertD("泄压气缸下超时，请检查，排除异常请点是，退出运行请点否", " Decompression_Down");
                            else
                                s = CardObject.InsertD("The pressure relief cylinder times out, please check, troubleshoot the exception please click Yes, exit the operation please click no", " Decompression_Down");
                            while (true)
                            {
                                Thread.Sleep(1);
                                if (Lib_Card.CardObject.keyValuePairs[s].Choose != 0)
                                    break;

                            }
                            int Alarm_Choose = Lib_Card.CardObject.keyValuePairs[s].Choose;
                            CardObject.DeleteD(s);
                            if (Alarm_Choose == 1)
                            {
                                goto lable;
                            }
                            else
                            {
                                throw new Exception("泄压气缸下超时");
                            }
                        }


                    }

                    //if (bDelay)
                    //    Lib_Card.CardObject.DeleteD(s);

                    int iDecompressionUp = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Decompression_Up);
                    if (-1 == iDecompressionUp)
                        return -1;
                    else if (1 == iDecompressionUp)
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            s = CardObject.InsertD("泄压上信号已接通，请检查，确定无接通请点是，退出运行请点否", " Decompression_Down");
                        else
                            s = CardObject.InsertD("The pressure relief signal is connected, please check, confirm that it is not connected, please click Yes, please click No to exit the operation", " Decompression_Down");

                        while (true)
                        {
                            Thread.Sleep(1);
                            if (Lib_Card.CardObject.keyValuePairs[s].Choose != 0)
                                break;

                        }
                        int Alarm_Choose = Lib_Card.CardObject.keyValuePairs[s].Choose;
                        CardObject.DeleteD(s);
                        if (Alarm_Choose == 1)
                        {
                            goto labelTop;
                        }
                        else
                        {
                            throw new Exception("泄压上信号已接通");
                        }
                    }
                    return 0;

                }
                else
                {
                    if (1 == iXStatus)
                        throw new Exception("X轴正在运行");

                    else if (1 == iYStatus)
                        throw new Exception("Y轴正在运行");

                    else if (1 == iXAlarm)
                        throw new Exception("X轴伺服器报警");

                    else if (1 == iYAlarm)
                        throw new Exception("Y轴伺服器报警");

                    else
                    {
                        if (!bReset)
                        {
                            Thread.Sleep(1000);
                            bReset = true;
                            goto lable;
                        }
                        else
                        {
                            string s;
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                s = CardObject.InsertD("接液盘未伸出，请检查，确定伸出请点是，退出运行请点否", " Decompression_Down");
                            else
                                s = CardObject.InsertD("The liquid plate is not extended, please check to make sure that the extension point is yes, and the exit point is no", " Decompression_Down");

                            while (true)
                            {
                                Thread.Sleep(1);
                                if (Lib_Card.CardObject.keyValuePairs[s].Choose != 0)
                                    break;

                            }
                            int Alarm_Choose = Lib_Card.CardObject.keyValuePairs[s].Choose;
                            CardObject.DeleteD(s);
                            if (Alarm_Choose == 1)
                            {
                                bReset = false;
                                goto lable;
                            }
                            else
                            {
                                throw new Exception("接液盘未伸出");
                            }
                        }
                    }
                }
            }
        }

        public override int Decompression_Up()
        {
            /* 条件
             *    1：X轴未运行
             *    2：Y轴未运行
             *    3：X轴未报警
             *    4：Y轴未报警
             *    5：接液盘伸出状态
             */
            labelTop:
            bool bReset = false;
            int iDecompressionUp = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Decompression_Up);
            if (-1 == iDecompressionUp)
                return -1;
            //else if (1 == iDecompressionDown)
            //    return 0;
            else
            {
            lable:
                int iXStatus = CardObject.OA1.ReadAxisStatus(ADT8940A1_IO.Axis_X);
                if (-1 == iXStatus)
                    return -1;

                int iYStatus = CardObject.OA1.ReadAxisStatus(ADT8940A1_IO.Axis_Y);
                if (-1 == iYStatus)
                    return -1;

                int iXAlarm = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_X_Alarm);
                if (-1 == iXAlarm)
                    return -1;

                int iYAlarm = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Y_Alarm);
                if (-1 == iYAlarm)
                    return -1;

                //int iTrayOut = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Tray_Out);
                //if (-1 == iTrayOut)
                //    return -1;

                if (0 == iXStatus && 0 == iYStatus && 0 == iXAlarm &&
                    0 == iYAlarm /*&& 1 == iTrayOut*/)
                {
                    int iRes = CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Decompression, 0);
                    if (-1 == iRes)
                        return -1;

                    bool bDelay = false;
                    Thread thread = new Thread(() =>
                    {
                        int iDelay = Convert.ToInt32(Configure.Parameter.Delay_Decompression * 1000.00);
                        Thread.Sleep(iDelay);
                        bDelay = true;
                    });
                    thread.Start();

                    string sPath = Environment.CurrentDirectory + "\\Config\\DataBase.ini";
                    Lib_DataBank.SQLServer.SQLServerCon con = new Lib_DataBank.SQLServer.SQLServerCon()
                    {
                        Server = Lib_File.Ini.GetIni("FADM", "Server", sPath),
                        Port = Lib_File.Ini.GetIni("FADM", "Port", sPath),
                        Database = Lib_File.Ini.GetIni("FADM", "Database", sPath),
                        UserName = Lib_File.Ini.GetIni("FADM", "UserName", sPath),
                        Password = Lib_File.Ini.GetIni("FADM", "Password", sPath)
                    };
                    Lib_DataBank.SQLServer sQLServer = new Lib_DataBank.SQLServer(con);
                    string s = null;
                    while (true)
                    {
                        Thread.Sleep(1);
                        iDecompressionUp = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Decompression_Up);
                        if (-1 == iDecompressionUp)
                            return -1;
                        else if (1 == iDecompressionUp)
                            break;
                        if (bDelay)
                        {
                            //s = CardObject.InsertD("泄压气缸上超时", "Decompression_Up");
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                s = CardObject.InsertD("泄压气缸上超时，请检查，排除异常请点是，退出运行请点否", " Decompression_Up");
                            else
                                s = CardObject.InsertD("The pressure relief cylinder times out, please check, troubleshooting please click Yes, exit please click no", " Decompression_Up");

                            while (true)
                            {
                                Thread.Sleep(1);
                                if (Lib_Card.CardObject.keyValuePairs[s].Choose != 0)
                                    break;

                            }
                            int Alarm_Choose = Lib_Card.CardObject.keyValuePairs[s].Choose;
                            CardObject.DeleteD(s);
                            if (Alarm_Choose == 1)
                            {
                                goto lable;
                            }
                            else
                            {
                                throw new Exception("泄压气缸上超时");
                            }
                        }

                    }

                    //if (bDelay)
                    //    Lib_Card.CardObject.DeleteD(s);

                    int iDecompressionDown = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Decompression_Down);
                    if (-1 == iDecompressionDown)
                        return -1;
                    else if (1 == iDecompressionDown)
                    {
                        s = CardObject.InsertD("泄压下信号已接通，请检查，确定无接通请点是，退出运行请点否", " Decompression_Up");
                        while (true)
                        {
                            Thread.Sleep(1);
                            if (Lib_Card.CardObject.keyValuePairs[s].Choose != 0)
                                break;

                        }
                        int Alarm_Choose = Lib_Card.CardObject.keyValuePairs[s].Choose;
                        CardObject.DeleteD(s);
                        if (Alarm_Choose == 1)
                        {
                            goto labelTop;
                        }
                        else
                        {
                            throw new Exception("泄压下信号已接通");
                        }
                    }
                    return 0;

                }
                else
                {
                    if (1 == iXStatus)
                        throw new Exception("X轴正在运行");

                    else if (1 == iYStatus)
                        throw new Exception("Y轴正在运行");

                    else if (1 == iXAlarm)
                        throw new Exception("X轴伺服器报警");

                    else if (1 == iYAlarm)
                        throw new Exception("Y轴伺服器报警");

                    return 0;

                    //else
                    //{
                    //    if (!bReset)
                    //    {
                    //        Thread.Sleep(1000);
                    //        bReset = true;
                    //        goto lable;
                    //    }
                    //    else
                    //    {
                    //        string s = CardObject.InsertD("接液盘未伸出，请检查，确定伸出请点是，退出运行请点否", " Decompression_Up");
                    //        while (true)
                    //        {
                    //            Thread.Sleep(1);
                    //            if (Lib_Card.CardObject.keyValuePairs[s].Choose != 0)
                    //                break;

                    //        }
                    //        int Alarm_Choose = Lib_Card.CardObject.keyValuePairs[s].Choose;
                    //        CardObject.DeleteD(s);
                    //        if (Alarm_Choose == 1)
                    //        {
                    //            bReset = false;
                    //            goto lable;
                    //        }
                    //        else
                    //        {
                    //            throw new Exception("接液盘未伸出");
                    //        }
                    //    }
                    //}
                }
            }
        }

        public override int Decompression_Down_Right()
        {
            /* 条件
             *    1：X轴未运行
             *    2：Y轴未运行
             *    3：X轴未报警
             *    4：Y轴未报警
             *    5：接液盘伸出状态
             */

            bool bReset = false;
            int iDecompressionDown = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Decompression_Down_Right);
            if (-1 == iDecompressionDown)
                return -1;
            else if (1 == iDecompressionDown)
                return 0;
            else
            {
            lable:
                int iXStatus = CardObject.OA1.ReadAxisStatus(ADT8940A1_IO.Axis_X);
                if (-1 == iXStatus)
                    return -1;

                int iYStatus = CardObject.OA1.ReadAxisStatus(ADT8940A1_IO.Axis_Y);
                if (-1 == iYStatus)
                    return -1;



                int iXAlarm = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_X_Alarm);
                if (-1 == iXAlarm)
                    return -1;

                int iYAlarm = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Y_Alarm);
                if (-1 == iYAlarm)
                    return -1;

                int iTrayOut = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Tray_Out);
                if (-1 == iTrayOut)
                    return -1;

                if (0 == iXStatus && 0 == iYStatus && 0 == iXAlarm &&
                    0 == iYAlarm && 1 == iTrayOut)
                {
                    int iRes = CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Decompression_Right, 1);
                    if (-1 == iRes)
                        return -1;

                    bool bDelay = false;
                    Thread thread = new Thread(() =>
                    {
                        int iDelay = Convert.ToInt32(Configure.Parameter.Delay_Decompression * 1000.00);
                        Thread.Sleep(iDelay);
                        bDelay = true;
                    });
                    thread.Start();

                    string sPath = Environment.CurrentDirectory + "\\Config\\DataBase.ini";
                    Lib_DataBank.SQLServer.SQLServerCon con = new Lib_DataBank.SQLServer.SQLServerCon()
                    {
                        Server = Lib_File.Ini.GetIni("FADM", "Server", sPath),
                        Port = Lib_File.Ini.GetIni("FADM", "Port", sPath),
                        Database = Lib_File.Ini.GetIni("FADM", "Database", sPath),
                        UserName = Lib_File.Ini.GetIni("FADM", "UserName", sPath),
                        Password = Lib_File.Ini.GetIni("FADM", "Password", sPath)
                    };
                    Lib_DataBank.SQLServer sQLServer = new Lib_DataBank.SQLServer(con);
                    string s = null;
                    while (true)
                    {
                        Thread.Sleep(1);
                        iDecompressionDown = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Decompression_Down_Right);
                        if (-1 == iDecompressionDown)
                            return -1;
                        else if (1 == iDecompressionDown)
                            break;
                        if (bDelay)
                        {
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                s = CardObject.InsertD("泄压气缸下超时，请检查，排除异常请点是，退出运行请点否", " Decompression_Down_Right");
                            else
                                s = CardObject.InsertD("The pressure relief cylinder times out, please check, troubleshoot the exception please click Yes, exit the operation please click no", " Decompression_Down_Right");
                            while (true)
                            {
                                Thread.Sleep(1);
                                if (Lib_Card.CardObject.keyValuePairs[s].Choose != 0)
                                    break;

                            }
                            int Alarm_Choose = Lib_Card.CardObject.keyValuePairs[s].Choose;
                            CardObject.DeleteD(s);
                            if (Alarm_Choose == 1)
                            {
                                goto lable;
                            }
                            else
                            {
                                throw new Exception("泄压气缸下超时");
                            }
                        }

                    }

                    //if (bDelay)
                    //    Lib_Card.CardObject.DeleteD(s);
                    return 0;

                }
                else
                {
                    if (1 == iXStatus)
                        throw new Exception("X轴正在运行");

                    else if (1 == iYStatus)
                        throw new Exception("Y轴正在运行");

                    else if (1 == iXAlarm)
                        throw new Exception("X轴伺服器报警");

                    else if (1 == iYAlarm)
                        throw new Exception("Y轴伺服器报警");

                    else
                    {
                        if (!bReset)
                        {
                            Thread.Sleep(1000);
                            bReset = true;
                            goto lable;
                        }
                        else
                        {
                            string s;
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                s = CardObject.InsertD("接液盘未伸出，请检查，确定伸出请点是，退出运行请点否", " Decompression_Down_Right");
                            else
                                s = CardObject.InsertD("The liquid plate is not extended, please check to make sure that the extension point is yes, and the exit point is no", " Decompression_Down_Right");
                            while (true)
                            {
                                Thread.Sleep(1);
                                if (Lib_Card.CardObject.keyValuePairs[s].Choose != 0)
                                    break;

                            }
                            int Alarm_Choose = Lib_Card.CardObject.keyValuePairs[s].Choose;
                            CardObject.DeleteD(s);
                            if (Alarm_Choose == 1)
                            {
                                bReset = false;
                                goto lable;
                            }
                            else
                            {
                                throw new Exception("接液盘未伸出");
                            }
                        }
                    }
                }
            }
        }

        public override int Decompression_Up_Right()
        {
            /* 条件
             *    1：X轴未运行
             *    2：Y轴未运行
             *    3：X轴未报警
             *    4：Y轴未报警
             *    5：接液盘伸出状态
             */

            bool bReset = false;
            int iDecompressionDown = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Decompression_Up_Right);
            if (-1 == iDecompressionDown)
                return -1;
            else if (1 == iDecompressionDown)
                return 0;
            else
            {
            lable:
                int iXStatus = CardObject.OA1.ReadAxisStatus(ADT8940A1_IO.Axis_X);
                if (-1 == iXStatus)
                    return -1;

                int iYStatus = CardObject.OA1.ReadAxisStatus(ADT8940A1_IO.Axis_Y);
                if (-1 == iYStatus)
                    return -1;

                int iXAlarm = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_X_Alarm);
                if (-1 == iXAlarm)
                    return -1;

                int iYAlarm = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Y_Alarm);
                if (-1 == iYAlarm)
                    return -1;

                int iTrayOut = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Tray_Out);
                if (-1 == iTrayOut)
                    return -1;

                if (0 == iXStatus && 0 == iYStatus && 0 == iXAlarm &&
                    0 == iYAlarm && 1 == iTrayOut)
                {
                    int iRes = CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Decompression_Right, 0);
                    if (-1 == iRes)
                        return -1;

                    bool bDelay = false;
                    Thread thread = new Thread(() =>
                    {
                        int iDelay = Convert.ToInt32(Configure.Parameter.Delay_Decompression * 1000.00);
                        Thread.Sleep(iDelay);
                        bDelay = true;
                    });
                    thread.Start();

                    string sPath = Environment.CurrentDirectory + "\\Config\\DataBase.ini";
                    Lib_DataBank.SQLServer.SQLServerCon con = new Lib_DataBank.SQLServer.SQLServerCon()
                    {
                        Server = Lib_File.Ini.GetIni("FADM", "Server", sPath),
                        Port = Lib_File.Ini.GetIni("FADM", "Port", sPath),
                        Database = Lib_File.Ini.GetIni("FADM", "Database", sPath),
                        UserName = Lib_File.Ini.GetIni("FADM", "UserName", sPath),
                        Password = Lib_File.Ini.GetIni("FADM", "Password", sPath)
                    };
                    Lib_DataBank.SQLServer sQLServer = new Lib_DataBank.SQLServer(con);
                    string s = null;
                    while (true)
                    {
                        Thread.Sleep(1);
                        iDecompressionDown = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Decompression_Up_Right);
                        if (-1 == iDecompressionDown)
                            return -1;
                        else if (1 == iDecompressionDown)
                            break;
                        if (bDelay)
                        {
                            //s = CardObject.InsertD("泄压气缸上超时", "Decompression_Up_Right");
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                s = CardObject.InsertD("泄压气缸上超时，请检查，排除异常请点是，退出运行请点否", " Decompression_Up_Right");
                            else
                                s = CardObject.InsertD("The pressure relief cylinder times out, please check, troubleshooting please click Yes, exit please click no", " Decompression_Up_Right");
                            while (true)
                            {
                                Thread.Sleep(1);
                                if (Lib_Card.CardObject.keyValuePairs[s].Choose != 0)
                                    break;

                            }
                            int Alarm_Choose = Lib_Card.CardObject.keyValuePairs[s].Choose;
                            CardObject.DeleteD(s);
                            if (Alarm_Choose == 1)
                            {
                                goto lable;
                            }
                            else
                            {
                                throw new Exception("泄压气缸上超时");
                            }
                        }

                    }

                    //if (bDelay)
                    //    Lib_Card.CardObject.DeleteD(s);
                    return 0;

                }
                else
                {
                    if (1 == iXStatus)
                        throw new Exception("X轴正在运行");

                    else if (1 == iYStatus)
                        throw new Exception("Y轴正在运行");

                    else if (1 == iXAlarm)
                        throw new Exception("X轴伺服器报警");

                    else if (1 == iYAlarm)
                        throw new Exception("Y轴伺服器报警");

                    else
                    {
                        if (!bReset)
                        {
                            Thread.Sleep(1000);
                            bReset = true;
                            goto lable;
                        }
                        else
                        {
                            string s;
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                s = CardObject.InsertD("接液盘未伸出，请检查，确定伸出请点是，退出运行请点否", " Decompression_Up_Right");
                            else
                                s = CardObject.InsertD("The liquid plate is not extended, please check to make sure that the extension point is yes, and the exit point is no", " Decompression_Up_Right");

                            while (true)
                            {
                                Thread.Sleep(1);
                                if (Lib_Card.CardObject.keyValuePairs[s].Choose != 0)
                                    break;

                            }
                            int Alarm_Choose = Lib_Card.CardObject.keyValuePairs[s].Choose;
                            CardObject.DeleteD(s);
                            if (Alarm_Choose == 1)
                            {
                                bReset = false;
                                goto lable;
                            }
                            else
                            {
                                throw new Exception("接液盘未伸出");
                            }
                        }
                    }
                }
            }
        }
    }
}
