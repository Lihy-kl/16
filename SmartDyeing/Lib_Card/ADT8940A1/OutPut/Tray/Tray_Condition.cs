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

           
           
            bool  bReset = false;
            int iTrayIn = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Tray_In);
            if (-1 == iTrayIn)
                return -1;
            else if (1 == iTrayIn)
                return 0;
            else
            {
                lable:
                int iZStatus = CardObject.OA1.ReadAxisStatus(ADT8940A1_IO.Axis_Z);
                if (-1 == iZStatus)
                    return -1;



                int iCylinderUp = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Cylinder_Up);
                if (-1 == iCylinderUp)
                    return -1;

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
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                s = CardObject.InsertD("接液盘伸出超时", "Tray_Off");
                            else
                                s = CardObject.InsertD("Liquid tray extension timeout", "Tray_Off");

                        }

                    }

                    Lib_Card.CardObject.DeleteD(s);
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
                                s = CardObject.InsertD("气缸未在上限位，请检查，确定到位请点是，退出运行请点否", " Tray_Off");
                            else
                                s = CardObject.InsertD("The cylinder is not in the upper limit position, please check. If it is in place, please click Yes. If it is out of operation, please click No", " Tray_Off");
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

         
          
            bool bReset = false;
            int iTrayOut = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Tray_Out);
            if (-1 == iTrayOut)
                return -1;
            else if (1 == iTrayOut)
                return 0;
            else
            {
                lable:
                int iZStatus = CardObject.OA1.ReadAxisStatus(ADT8940A1_IO.Axis_Z);
                if (-1 == iZStatus)
                    return -1;



                int iCylinderUp = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Cylinder_Up);
                if (-1 == iCylinderUp)
                    return -1;

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
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                s = CardObject.InsertD("接液盘伸出超时", "Tray_On");
                            else
                                s = CardObject.InsertD("Liquid tray extension timeout", "Tray_On");

                        }
                    }

                   Lib_Card.CardObject.DeleteD(s);
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
                                s = CardObject.InsertD("The cylinder is not in the upper limit position, please check. If it is in place, please click Yes. If it is out of operation, please click No", " Tray_On");
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
                                throw new Exception("气缸未在上限位");
                            }
                        }
                    }

                }
            }
        }
    }
}
