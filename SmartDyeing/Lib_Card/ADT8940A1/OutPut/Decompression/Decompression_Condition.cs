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

            if (Lib_Card.Configure.Parameter.Other_IsOnlyDrip == 1)
            {
                return 0;
            }

                bool bReset = false;
            int iDecompressionDown = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Decompression_Down);
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
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                s = CardObject.InsertD("泄压气缸下超时", "Decompression_Down");
                            else
                                s = CardObject.InsertD("Pressure relief cylinder down timeout", "Decompression_Down");
                        }

                    }

                    if (bDelay)
                        Lib_Card.CardObject.DeleteD(s);
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
                                s = CardObject.InsertD("The liquid tray is not extended, please check. If it is extended, please click Yes. If it is exited, please click No", " Decompression_Down");

                            while (true)
                            {
                                Thread.Sleep(1);
                                if (Lib_Card.CardObject.keyValuePairs[s].Choose != 0)
                                    break;

                            }
                            CardObject.DeleteD(s);
                            int Alarm_Choose = Lib_Card.CardObject.keyValuePairs[s].Choose;
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
            if (Lib_Card.Configure.Parameter.Other_IsOnlyDrip == 1)
            {
                return 0;
            }
            bool bReset = false;
            int iDecompressionDown = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Decompression_Up);
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
                        iDecompressionDown = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Decompression_Up);
                        if (-1 == iDecompressionDown)
                            return -1;
                        else if (1 == iDecompressionDown)
                            break;
                        if (bDelay)
                        {
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                s = CardObject.InsertD("泄压气缸上超时", "Decompression_Up");
                            else
                                s = CardObject.InsertD("Pressure relief cylinder Up timeout", "Decompression_Up");
                        }

                    }

                    if (bDelay)
                        Lib_Card.CardObject.DeleteD(s);
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
                                s = CardObject.InsertD("接液盘未伸出，请检查，确定伸出请点是，退出运行请点否", " Decompression_Up");
                            else
                                s = CardObject.InsertD("The liquid tray is not extended, please check. If it is extended, please click Yes. If it is exited, please click No", " Decompression_Up");

                            while (true)
                            {
                                Thread.Sleep(1);
                                if (Lib_Card.CardObject.keyValuePairs[s].Choose != 0)
                                    break;

                            }
                            CardObject.DeleteD(s);
                            int Alarm_Choose = Lib_Card.CardObject.keyValuePairs[s].Choose;
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

        public override int Decompression_Down_Right()
        {
            /* 条件
             *    1：X轴未运行
             *    2：Y轴未运行
             *    3：X轴未报警
             *    4：Y轴未报警
             *    5：接液盘伸出状态
             */
            if (Lib_Card.Configure.Parameter.Other_IsOnlyDrip == 1)
            {
                return 0;
            }
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
                                s = CardObject.InsertD("泄压气缸下超时", "Decompression_Down_Right");
                            else
                                s = CardObject.InsertD("Pressure relief cylinder Down timeout", "Decompression_Down_Right");

                        }

                    }

                    if (bDelay)
                        Lib_Card.CardObject.DeleteD(s);
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
                                s = CardObject.InsertD("The liquid tray is not extended, please check. If it is extended, please click Yes. If it is exited, please click No", " Decompression_Down_Right");

                            while (true)
                            {
                                Thread.Sleep(1);
                                if (Lib_Card.CardObject.keyValuePairs[s].Choose != 0)
                                    break;

                            }
                            CardObject.DeleteD(s);
                            int Alarm_Choose = Lib_Card.CardObject.keyValuePairs[s].Choose;
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
            if (Lib_Card.Configure.Parameter.Other_IsOnlyDrip == 1)
            {
                return 0;
            }
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
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                s = CardObject.InsertD("泄压气缸上超时", "Decompression_Up_Right");
                            else
                                s = CardObject.InsertD("Pressure relief cylinder Up timeout", "Decompression_Up_Right");

                        }

                    }

                    if (bDelay)
                        Lib_Card.CardObject.DeleteD(s);
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
                                s = CardObject.InsertD("The liquid tray is not extended, please check. If it is extended, please click Yes. If it is exited, please click No", " Decompression_Up_Right");

                            while (true)
                            {
                                Thread.Sleep(1);
                                if (Lib_Card.CardObject.keyValuePairs[s].Choose != 0)
                                    break;

                            }
                            CardObject.DeleteD(s);
                            int Alarm_Choose = Lib_Card.CardObject.keyValuePairs[s].Choose;
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
