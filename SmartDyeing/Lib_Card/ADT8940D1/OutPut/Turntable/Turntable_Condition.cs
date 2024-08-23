using System;
using System.Threading;

namespace Lib_Card.ADT8940D1.OutPut.Turntable
{
    /// <summary>
    /// 带条件检查
    /// </summary>
    public class Turntable_Condition : Turntable
    {
        public override int Turntable_Down()
        {
            /* 条件
             *     1：转盘未运行
             * 
             */



            int iTStatus = CardObject.OD1.ReadAxisStatus(ADT8940D1_IO.Axis_T);
            if (-1 == iTStatus)
                return -1;



            if (0 == iTStatus)
            {
                int iRes = CardObject.OD1.WriteOutPut(ADT8940D1_IO.OutPut_Turntable, 1);
                if (-1 == iRes)
                    return -1;

                bool bDelay = false;
                Thread thread = new Thread(() =>
                {
                    int iDelay = Convert.ToInt32(Configure.Parameter.Delay_Turntable * 1000.00);
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

                int iTurntableDown = 0;
                string s = null;
                while (true)
                {
                    Thread.Sleep(1);
                    iTurntableDown = CardObject.OD1Input.InPutStatus(ADT8940D1_IO.InPut_T_Down);
                    if (-1 == iTurntableDown)
                        return -1;
                    else if (1 == iTurntableDown)
                        break;
                    if (bDelay)
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            s = CardObject.InsertD("转盘下超时", "Turntable_Down");
                        else
                            s = CardObject.InsertD("Turntable Down timeout", "Turntable_Down");

                    }
                }

                if (bDelay)
                    Lib_Card.CardObject.DeleteD(s);
                return 0;

            }
            else
            {
                throw new Exception("转盘正在运行");
            }
        }

        public override int Turntable_Up()
        {
            /* 条件
             *     1：转盘未运行
             *     2：加水未打开
             */



            int iTStatus = CardObject.OD1.ReadAxisStatus(ADT8940D1_IO.Axis_T);
            if (-1 == iTStatus)
                return -1;

            int iWater = CardObject.OD1.ReadOutPut(ADT8940D1_IO.OutPut_Water);
            if (-1 == iWater)
                return -1;



            if (0 == iTStatus && 0 == iWater)
            {
                int iRes = CardObject.OD1.WriteOutPut(ADT8940D1_IO.OutPut_Turntable, 0);
                if (-1 == iRes)
                    return -1;

                bool bDelay = false;
                Thread thread = new Thread(() =>
                {
                    int iDelay = Convert.ToInt32(Configure.Parameter.Delay_Turntable * 1000.00);
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

                int iTurntableUp = 0;
                string s = null;
                while (true)
                {
                    Thread.Sleep(1);
                    iTurntableUp = CardObject.OD1Input.InPutStatus(ADT8940D1_IO.InPut_T_Up);
                    if (-1 == iTurntableUp)
                        return -1;
                    else if (1 == iTurntableUp)
                        break;
                    if (bDelay)
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            s = CardObject.InsertD("转盘上超时", "Turntable_Up");
                        else
                            s = CardObject.InsertD("Turntable Up timeout", "Turntable_Up");

                    }

                }
                if (bDelay)
                    Lib_Card.CardObject.DeleteD(s);

                return 0;

            }
            else
            {
                if (1 == iTStatus)
                    throw new Exception("转盘正在运行");
                else
                    throw new Exception("加水正在运行");
            }
        }
    }
}
