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
                int iRes = CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Tongs, 0);
                if (-1 == iRes)
                    return -1;

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
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                s1 = CardObject.InsertD("抓手A打开超时", "Tongs_Off");
                            else
                                s1 = CardObject.InsertD("Grip A opening timeout", "Tongs_Off");
                        }
                        else if (1 == iTongsB)
                        {
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                s2 = CardObject.InsertD("抓手B打开超时", "Tongs_Off");
                            else
                                s2 = CardObject.InsertD("Grip A opening timeout", "Tongs_Off");

                        }
                    }

                }

                if (bDelay)
                {
                    Lib_Card.CardObject.DeleteD(s1);
                    Lib_Card.CardObject.DeleteD(s2);
                }
                return 0;
            }
        }

        public override int Tongs_On()
        {


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
                int iRes = CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Tongs, 1);
                if (-1 == iRes)
                    return -1;

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
                                s1 = CardObject.InsertD("抓手A打开超时", "Tongs_On");
                            else
                                s1 = CardObject.InsertD("Grip A opening timeout", "Tongs_On");
                        }
                        else if (1 == iTongsB)
                        {
                            if (Lib_Card.Configure.Parameter.Other_Language == 0)
                                s2 = CardObject.InsertD("抓手B打开超时", "Tongs_On");
                            else
                                s2 = CardObject.InsertD("Grip A opening timeout", "Tongs_On");

                        }
                    }

                }

                if (bDelay)
                {
                    Lib_Card.CardObject.DeleteD(s1);
                    Lib_Card.CardObject.DeleteD(s2);
                }
                return 0;
            }
        }
    }
}
