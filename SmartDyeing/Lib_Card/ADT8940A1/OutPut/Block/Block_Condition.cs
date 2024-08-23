﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lib_Card.ADT8940A1.OutPut.Block
{
    public class Block_Condition : Block
    {
        public override int Block_In()
        {
            /* 条件
             *    1：气缸在上限位
             */






            int iRes = CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Block, 0);
            if (-1 == iRes)
                return -1;

            bool bDelay = false;
            Thread thread = new Thread(() =>
            {
                int iDelay = Convert.ToInt32(Configure.Parameter.Delay_Block * 1000.00);
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

            int iBlockOut = 0;
            string s = null;
            while (true)
            {
                Thread.Sleep(1);
                iBlockOut = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Block_In);
                if (-1 == iBlockOut)
                    return -1;
                else if (1 == iBlockOut)
                    break;
                if (bDelay)
                {
                    if (Lib_Card.Configure.Parameter.Other_Language == 0)
                        s = CardObject.InsertD("阻挡气缸收回超时", "Block_In");
                    else
                        s = CardObject.InsertD("Blocking cylinder retraction timeout", "Block_In");
                }


            }

            if (bDelay)
                Lib_Card.CardObject.DeleteD(s);

            return 0;


        }

        public override int Block_Out()
        {
            /* 条件
             *    1：气缸在上限位
             */


            bool bReset = false;
        lable:
            int iCylinderUp = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Cylinder_Up);
            if (-1 == iCylinderUp)
                return -1;



            if (1 == iCylinderUp)
            {
                int iRes = CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Block, 1);
                if (-1 == iRes)
                    return -1;

                bool bDelay = false;
                Thread thread = new Thread(() =>
                {
                    int iDelay = Convert.ToInt32(Configure.Parameter.Delay_Block * 1000.00);
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
                int iBlockOut = 0;
                string s = null;
                while (true)
                {
                    Thread.Sleep(1);
                    iBlockOut = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Block_Out);
                    if (-1 == iBlockOut)
                        return -1;
                    else if (1 == iBlockOut)
                        break;
                    if (bDelay)
                    {
                        if (Lib_Card.Configure.Parameter.Other_Language == 0)
                            s = CardObject.InsertD("阻挡气缸伸出超时", "Block_Out");
                        else
                            s = CardObject.InsertD("Blocking cylinder extension timeout", "Block_Out");
                    }

                }

                if (bDelay)
                    Lib_Card.CardObject.DeleteD(s);

                return 0;

            }
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
                        s = CardObject.InsertD("气缸未在上限位，请检查，确定到位请点是，退出运行请点否", " Block_Out");
                    else
                        s = CardObject.InsertD("The cylinder is not in the upper limit position, please check. If it is in place, please click Yes. If it is out of operation, please click No", " Block_Out");
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
