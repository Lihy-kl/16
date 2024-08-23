﻿using System;
using System.Threading;

namespace Lib_Card.ADT8940A1.OutPut.Cylinder.SingleControl
{
    /// <summary>
    /// 单控版
    /// 有条件检查
    /// </summary>
    public class Cylinder_Condition : Cylinder
    {
        public override int CylinderDown()
        {
            /* 条件
             *    1：X轴未运行
             *    2：Y轴未运行
             *    3：X轴未报警
             *    4：Y轴未报警
             *    5：接液盘收回状态
             */
           
          
            bool bReset = false;
            int iCylinderDown = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Cylinder_Down);
            if (-1 == iCylinderDown)
                return -1;
            else if (1 == iCylinderDown)
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

                int iTrayIn = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Tray_In);
                if (-1 == iTrayIn)
                    return -1;

                if (0 == iXStatus && 0 == iYStatus && 0 == iXAlarm &&
                    0 == iYAlarm && 1 == iTrayIn)
                {

                    int iRes = CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Cylinder_Down, 1);
                    if (-1 == iRes)
                        return -1;

                    bool bDelay = false;
                    Thread thread = new Thread(() =>
                    {
                        int iDelay = Convert.ToInt32(Configure.Parameter.Delay_Cylinder * 1000.00);
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
                        iCylinderDown = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Cylinder_Down);
                        if (-1 == iCylinderDown)
                            return -1;
                        else if (1 == iCylinderDown)
                            break;
                        if (bDelay)
                        {
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                s = CardObject.InsertD("气缸下超时", " CylinderDown");
                            else
                                s = CardObject.InsertD("气缸下超时", " CylinderDown");
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
                                s = CardObject.InsertD("接液盘未收回，请检查，确定收回请点是，退出运行请点否", " CylinderDown");
                            else
                                s = CardObject.InsertD("The liquid tray has not been retracted. Please check if it has been retracted. If it has been confirmed, please click Yes. If it has been withdrawn, please click No", " CylinderDown");

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
                                throw new Exception("接液盘未收回");
                            }
                        }
                    }
                }
            }
        }

        public override int CylinderUp()
        {
            
            
            int iCylinderUp = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Cylinder_Up);
            if (-1 == iCylinderUp)
                return -1;
            else if (1 == iCylinderUp)
                return 0;
            else
            {

                bool bDelay = false;
                Thread thread = new Thread(() =>
                {
                    int iDelay = Convert.ToInt32(Configure.Parameter.Delay_Cylinder * 1000.00);
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
                    iCylinderUp = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Cylinder_Up);
                    if (-1 == iCylinderUp)
                        return -1;
                    else if (1 == iCylinderUp)
                        break;
                    if (bDelay)
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            s = CardObject.InsertD("气缸上超时", "CylinderUp");
                        else
                            s = CardObject.InsertD("Cylinder Up timeout", "CylinderUp");
                    }
                    
                }

                if (bDelay)
                    Lib_Card.CardObject.DeleteD(s);

                return 0;
            }
        }

        public override int CylinderMid()
        {
            return -1;
        }
    }
}
