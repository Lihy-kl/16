using System;
using System.Threading;

namespace Lib_Card.ADT8940A1.OutPut.Tongs
{
    /// <summary>
    /// 带条件检查
    /// </summary>
    public class Tongs_Condition : Tongs
    {
        public override int Tongs_Off()
        {

            lable:
            int iTongsA = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Tongs_A);
            if (-1 == iTongsA)
                return -1;
            int iTongsB = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Tongs_B);
            if (-1 == iTongsB)
                return -1;
            if (1 == iTongsA && 1 == iTongsB)
                return 0;
            else
            {
                if (Lib_Card.Configure.Parameter.Machine_TongsVersion == 0)
                {
                    int iRes = CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Tongs, 0);
                    if (-1 == iRes)
                        return -1;
                }
                else
                {
                    int iRes = CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Tongs, 0);
                    if (-1 == iRes)
                        return -1;
                    int iRes1 = CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_TongsOff, 1);
                    if (-1 == iRes1)
                        return -1;
                }

                bool bDelay = false;
                Thread thread = new Thread(() =>
                {
                    int iDelay = Convert.ToInt32(Configure.Parameter.Delay_Tongs * 1000.00);
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
                string s1 = null, s2 = null;
                while (true)
                {
                    Thread.Sleep(1);
                    iTongsA = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Tongs_A);
                    if (-1 == iTongsA)
                        return -1;

                    iTongsB = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Tongs_B);
                    if (-1 == iTongsB)
                        return -1;

                    if (1 == iTongsA && 1 == iTongsB)
                        break;

                    if (bDelay)
                    {
                        if (1 == iTongsA)
                        {
                            //s1 = CardObject.InsertD("抓手A打开超时", "Tongs_Off");
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                s1 = CardObject.InsertD("抓手A打开超时，请检查，排除异常请点是，退出运行请点否", " Tongs_Off");
                            else
                                s1 = CardObject.InsertD("Gripper A opens the timeout, please check, troubleshoot the exception, please click Yes, exit the run, please click No", " Tongs_Off");
                            while (true)
                            {
                                Thread.Sleep(1);
                                if (Lib_Card.CardObject.keyValuePairs[s1].Choose != 0)
                                    break;

                            }
                            int Alarm_Choose = Lib_Card.CardObject.keyValuePairs[s1].Choose;
                            CardObject.DeleteD(s1);
                            if (Alarm_Choose == 1)
                            {
                                goto lable;
                            }
                            else
                            {
                                throw new Exception("抓手A打开超时");
                            }
                        }
                        else if (1 == iTongsB)
                        {
                            //s2 = CardObject.InsertD("抓手B打开超时", "Tongs_Off");
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                s2 = CardObject.InsertD("抓手B打开超时，请检查，排除异常请点是，退出运行请点否", " Tongs_Off");
                            else
                                s2 = CardObject.InsertD("Gripper B opens the timeout, please check, troubleshoot the exception, please click Yes, exit the run, please click No", " Tongs_Off");

                            while (true)
                            {
                                Thread.Sleep(1);
                                if (Lib_Card.CardObject.keyValuePairs[s2].Choose != 0)
                                    break;

                            }
                            int Alarm_Choose = Lib_Card.CardObject.keyValuePairs[s2].Choose;
                            CardObject.DeleteD(s2);
                            if (Alarm_Choose == 1)
                            {
                                goto lable;
                            }
                            else
                            {
                                throw new Exception("抓手B打开超时");
                            }
                        }
                    }

                }

                //if (bDelay)
                //{
                //    Lib_Card.CardObject.DeleteD(s1);
                //    Lib_Card.CardObject.DeleteD(s2);
                //}
                return 0;
            }
        }

        public override int Tongs_On()
        {

            lable:
            int iTongsA = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Tongs_A);
            if (-1 == iTongsA)
                return -1;
            int iTongsB = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Tongs_B);
            if (-1 == iTongsB)
                return -1;
            if (0 == iTongsA && 0 == iTongsB)
                return 0;
            else
            {
                if (Lib_Card.Configure.Parameter.Machine_TongsVersion == 0)
                {
                    int iRes = CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Tongs, 1);
                    if (-1 == iRes)
                        return -1;
                }
                else
                {
                    int iRes = CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Tongs, 1);
                    if (-1 == iRes)
                        return -1;

                    int iRes1 = CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_TongsOff, 0);
                    if (-1 == iRes1)
                        return -1;
                }

                bool bDelay = false;
                Thread thread = new Thread(() =>
                {
                    int iDelay = Convert.ToInt32(Configure.Parameter.Delay_Tongs * 1000.00);
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
                string s1 =null, s2 =null;
                while (true)
                {
                    Thread.Sleep(1);
                    iTongsA = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Tongs_A);
                    if (-1 == iTongsA)
                        return -1;

                    iTongsB = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Tongs_B);
                    if (-1 == iTongsB)
                        return -1;

                    if (0 == iTongsA && 0 == iTongsB)
                        break;

                    if (bDelay)
                    {
                        if (1 == iTongsA)
                        {
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                s1 = CardObject.InsertD("抓手A关闭超时，请检查，排除异常请点是，退出运行请点否", " Tongs_On");
                            else
                                s1 = CardObject.InsertD("Gripper A closes the timeout. Please check. For troubleshooting exceptions, click Yes. For exiting the operation, click No", " Tongs_On");

                            while (true)
                            {
                                Thread.Sleep(1);
                                if (Lib_Card.CardObject.keyValuePairs[s1].Choose != 0)
                                    break;

                            }
                            int Alarm_Choose = Lib_Card.CardObject.keyValuePairs[s1].Choose;
                            CardObject.DeleteD(s1);
                            if (Alarm_Choose == 1)
                            {
                                goto lable;
                            }
                            else
                            {
                                throw new Exception("抓手A关闭超时");
                            }
                        }
                        else if (1 == iTongsB)
                        {
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                s2 = CardObject.InsertD("抓手B关闭超时，请检查，排除异常请点是，退出运行请点否", " Tongs_On");
                            else
                                s2 = CardObject.InsertD("Gripper B closes the timeout. Please check. For troubleshooting exceptions, click Yes. For exiting the operation, click No", " Tongs_On");

                            while (true)
                            {
                                Thread.Sleep(1);
                                if (Lib_Card.CardObject.keyValuePairs[s2].Choose != 0)
                                    break;

                            }
                            int Alarm_Choose = Lib_Card.CardObject.keyValuePairs[s2].Choose;
                            CardObject.DeleteD(s2);
                            if (Alarm_Choose == 1)
                            {
                                goto lable;
                            }
                            else
                            {
                                throw new Exception("抓手B关闭超时");
                            }
                        }
                    }

                }

                //if (bDelay)
                //{
                //    Lib_Card.CardObject.DeleteD(s1);
                //    Lib_Card.CardObject.DeleteD(s2);
                //}
                return 0;
            }
        }
    }
}
