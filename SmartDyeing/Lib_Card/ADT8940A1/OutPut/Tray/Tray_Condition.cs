using Lib_Card.ADT8940A1.OutPut.Tongs;
using System;
using System.Reflection.Emit;
using System.Threading;

namespace Lib_Card.ADT8940A1.OutPut.Tray
{
    /// <summary>
    /// 带条件检查
    /// </summary>
    public class Tray_Condition : Tray
    {
        public override int Tray_Off()
        {
            /* 条件
             *    1：Z轴未运行
             *    2：气缸在上限位
             */


            labelTop:
            bool  bReset = false;
            int iTrayIn = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Tray_In);
            if (-1 == iTrayIn)
                return -1;
            //else if (1 == iTrayIn)
            //    return 0;
            else
            {
                lable:
                int iZStatus = CardObject.OA1.ReadAxisStatus(ADT8940A1_IO.Axis_Z);
                if (-1 == iZStatus)
                    return -1;



                int iCylinderUp = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Cylinder_Up);
                if (-1 == iCylinderUp)
                    return -1;
                int iCylinderDown = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Cylinder_Down);
                if (-1 == iCylinderDown)
                    return -1;
                if (iCylinderUp == 0 || iCylinderDown == 1)
                {
                    //气缸上
                    OutPut.Cylinder.Cylinder cylinder;
                    if (0 == Lib_Card.Configure.Parameter.Machine_CylinderVersion)
                        cylinder = new OutPut.Cylinder.SingleControl.Cylinder_Condition();
                    else
                        cylinder = new OutPut.Cylinder.DualControl.Cylinder_Condition();

                    if (-1 == cylinder.CylinderUp(0))
                        return -1;

                    goto labelTop;
                }

                if (0 == iZStatus && 1 == iCylinderUp)
                {
                    int iRes = CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Tray, 0);
                    if (-1 == iRes)
                        return -1;

                    bool bDelay = false;
                    Thread thread = new Thread(() =>
                    {
                        int iDelay = Convert.ToInt32(Configure.Parameter.Delay_Tray * 1000.00);
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
                        iTrayIn = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Tray_In);
                        if (-1 == iTrayIn)
                            return -1;
                        else if (1 == iTrayIn)
                            break;
                        if (bDelay)
                        {
                            //s = CardObject.InsertD("接液盘收回超时", "Tray_Off");
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                s = CardObject.InsertD("接液盘收回超时，请检查，排除异常请点是，退出运行请点否", " Tray_On");
                            else
                                s = CardObject.InsertD("Liquid disk recovery timeout, please check, troubleshoot the exception, please click Yes, exit the operation, please click No", " Tray_On");

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
                                throw new Exception("接液盘收回超时");
                            }
                        }

                    }
                    //if(bDelay)
                    //Lib_Card.CardObject.DeleteD(s);

                    //再判断一下接液盘回信号是否有，有就提示，要手动确认没问题才可以继续
                    int iTrayOut = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Tray_Out);
                    if (-1 == iTrayOut)
                        return -1;
                    else if (1 == iTrayOut)
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            s = CardObject.InsertD("接液盘出信号已接通，请检查，确定无接通请点是，退出运行请点否", " Tray_Off");
                        else
                            s = CardObject.InsertD("The outgoing signal of the liquid plate is connected, please check, confirm that it is not connected, please click Yes, please click No to exit the operation", " Tray_Off");

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
                            throw new Exception("接液盘出信号已接通");
                        }
                    }

                    return 0;

                }
                else
                {
                    if (1 == iZStatus)
                        throw new Exception("Z轴正在运行");

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
                                s = CardObject.InsertD("气缸未在上限位，请检查，确定到位请点是，退出运行请点否", " Tray_On");
                            else
                                s = CardObject.InsertD("The cylinder is not in the upper limit, please check, confirm in place, please click Yes, exit operation, please click No", " Tray_On");

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
                                throw new Exception("气缸未在上限位");
                            }

                        }
                    }

                }
            }
        }

        public override int Tray_On()
        {
        /* 条件
        *    1：Z轴未运行
        *    2：气缸在上限位
        */


        labelTop:
            bool bReset = false;
            int iTrayOut = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Tray_Out);
            if (-1 == iTrayOut)
                return -1;
            //else if (1 == iTrayOut)
            //    return 0;
            else
            {
                lable:
                int iZStatus = CardObject.OA1.ReadAxisStatus(ADT8940A1_IO.Axis_Z);
                if (-1 == iZStatus)
                    return -1;



                int iCylinderUp = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Cylinder_Up);
                if (-1 == iCylinderUp)
                    return -1;
                int iCylinderDown = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Cylinder_Down);
                if (-1 == iCylinderDown)
                    return -1;
                if(iCylinderUp==0 || iCylinderDown==1)
                {
                    //气缸上
                    OutPut.Cylinder.Cylinder cylinder;
                    if (0 == Lib_Card.Configure.Parameter.Machine_CylinderVersion)
                        cylinder = new OutPut.Cylinder.SingleControl.Cylinder_Condition();
                    else
                        cylinder = new OutPut.Cylinder.DualControl.Cylinder_Condition();

                    if (-1 == cylinder.CylinderUp(0))
                        return -1;

                    goto labelTop;
                }

                if (0 == iZStatus && 1 == iCylinderUp)
                {
                    int iRes = CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Tray, 1);
                    if (-1 == iRes)
                        return -1;

                    bool bDelay = false;
                    Thread thread = new Thread(() =>
                    {
                        int iDelay = Convert.ToInt32(Configure.Parameter.Delay_Tray * 1000.00);
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
                        iTrayOut = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Tray_Out);
                        if (-1 == iTrayOut)
                            return -1;
                        else if (1 == iTrayOut)
                            break;
                        if (bDelay)
                        {
                            //s = CardObject.InsertD("接液盘伸出超时", "Tray_On");
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                s = CardObject.InsertD("接液盘伸出超时，请检查，排除异常请点是，退出运行请点否", " Tray_On");
                            else
                                s = CardObject.InsertD("Liquid plate extension time out, please check, troubleshooting please click Yes, exit please click no", " Tray_On");

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
                                throw new Exception("接液盘伸出超时");
                            }
                        }
                        
                    }
                    //if (bDelay)
                    //    Lib_Card.CardObject.DeleteD(s);

                    //再判断一下接液盘回信号是否有，有就提示，要手动确认没问题才可以继续
                    int iTrayIn = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Tray_In);
                    if (-1 == iTrayIn)
                        return -1;
                    else if (1 == iTrayIn)
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            s = CardObject.InsertD("接液盘回信号已接通，请检查，确定无接通请点是，退出运行请点否", " Tray_On");
                        else
                            s = CardObject.InsertD("The return signal of the liquid tray is connected, please check, confirm that it is not connected, please click Yes, please click No to exit the operation", " Tray_On");

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
                            throw new Exception("接液盘回信号已接通");
                        }
                    }
                    return 0;

                }
                else
                {
                    if (1 == iZStatus)
                        throw new Exception("Z轴正在运行");

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
                                s = CardObject.InsertD("气缸未在上限位，请检查，确定到位请点是，退出运行请点否", " Tray_On");
                            else
                                s = CardObject.InsertD("The cylinder is not in the upper limit, please check, confirm in place, please click Yes, exit operation, please click No", " Tray_On");

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
                                throw new Exception("气缸未在上限位");
                            }
                        }
                    }

                }
            }
        }
    }
}
