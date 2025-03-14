using SmartDyeing.FADM_Auto;
using System;
using System.Reflection.Emit;
using System.Threading;
using System.Windows.Forms;

namespace Lib_Card.ADT8940A1.OutPut.Cylinder.DualControl
{
    /// <summary>
    /// 双控版
    /// 带条件检查
    /// </summary>
    public class Cylinder_Condition : Cylinder
    {
        public override int CylinderDown(int i_judge)
        {
            /* 条件
              *    1：X轴未运行
              *    2：Y轴未运行
              *    3：X轴未报警
              *    4：Y轴未报警
              *    5：接液盘收回状态
              */
            labelTop:
            bool bReset = false;
            int iCylinderDown = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Cylinder_Down);
            if (-1 == iCylinderDown)
                return -1;
            //else if (1 == iCylinderDown)
            //{
            //    return 0;
            //}
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

                int iTrayOut = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Tray_Out);
                if (-1 == iTrayOut)
                    return -1;

                if (iTrayIn == 0 || iTrayOut == 1)
                {
                    //接液盘回
                    OutPut.Tray.Tray tray = new OutPut.Tray.Tray_Condition();
                    if (-1 == tray.Tray_Off())
                        return -1;
                    goto labelTop;
                }

                if (0 == iXStatus && 0 == iYStatus && 0 == iXAlarm &&
                    0 == iYAlarm && 1 == iTrayIn)
                {

                    Block.Block block = new Block.Block_Condition();
                    if (-1 == block.Block_In())
                        return -1;

                    int iRes = CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Cylinder_Up, 0);
                    if (-1 == iRes)
                        return -1;

                    iRes = CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Cylinder_Down, 1);
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
                            // 用于开关盖判断
                            if (i_judge == 1)
                            {
                                return -9;
                            }
                            else if(i_judge == 2)
                            {
                                return -8;
                            }
                            else
                            {
                                //s = CardObject.InsertD("气缸下超时", " CylinderDown");
                                s = CardObject.InsertD("气缸下超时，请检查，排除异常请点是，退出运行请点否", " CylinderUp");
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
                                    throw new Exception("气缸下超时");
                                }
                            }
                        }


                    }

                    //if (bDelay)
                    //    Lib_Card.CardObject.DeleteD(s);

                    //再判断一下气缸下信号是否有，有就提示，要手动确认没问题才可以继续
                    int iCylinderUp = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Cylinder_Up);
                    if (-1 == iCylinderUp)
                        return -1;
                    else if (1 == iCylinderUp)
                    {
                        s = CardObject.InsertD("气缸上信号已接通，请检查，确定无接通请点是，退出运行请点否", " CylinderDown");
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
                            throw new Exception("气缸上信号已接通");
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
                            string s = CardObject.InsertD("接液盘未收回，请检查，确定收回请点是，退出运行请点否", " CylinderDown");
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
                                throw new Exception("接液盘未收回");
                            }
                        }
                    }
                }
            }
        }

        public override int CylinderUp(int i_judge)
        {

            lable:
            int iCylinderUp = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Cylinder_Up);
            if (-1 == iCylinderUp)
                return -1;
            //else if (1 == iCylinderUp)
            //    return 0;
            else
            {

                int iRes = CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Cylinder_Down, 0);
                if (-1 == iRes)
                    return -1;

                int iCylinderMid = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Cylinder_Mid);
                if (-1 == iCylinderMid)
                    return -1;
                else if (0 == iCylinderMid)
                {
                    Block.Block block = new Block.Block_Condition();
                    if (-1 == block.Block_In())
                        return -1;
                }

                iRes = CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Cylinder_Up, 1);
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
                    iCylinderUp = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Cylinder_Up);
                    if (-1 == iCylinderUp)
                        return -1;
                    else if (1 == iCylinderUp)
                        break;

                    if(i_judge == 2)
                    {
                        //气缸下无信号就退出
                        int iCylinderDown1 = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Cylinder_Down);
                        if (-1 == iCylinderDown1)
                            return -1;
                        else if (0 == iCylinderDown1)
                        {
                            return -8;
                        }
                    }
                    if (bDelay)
                    {
                        //用于开关盖判断
                        if (i_judge == 1)
                        {
                            return -9;
                        }
                        else
                        {
                            //s = CardObject.InsertD("气缸上超时", " CylinderUp");

                            s = CardObject.InsertD("气缸上超时，请检查，排除异常请点是，退出运行请点否", " CylinderUp");
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
                                throw new Exception("气缸上超时");
                            }
                        }
                    }
                }

                //if (bDelay)
                //    Lib_Card.CardObject.DeleteD(s);

                if (1 == iCylinderMid)
                {
                    Block.Block block = new Block.Block_Condition();
                    if (-1 == block.Block_In())
                        return -1;
                }

                //再判断一下气缸下信号是否有，有就提示，要手动确认没问题才可以继续
                int iCylinderDown = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Cylinder_Down);
                if (-1 == iCylinderDown)
                    return -1;
                else if (1 == iCylinderDown)
                {
                    s = CardObject.InsertD("气缸下信号已接通，请检查，确定无接通请点是，退出运行请点否", " CylinderUp");
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
                        throw new Exception("气缸下信号已接通");
                    }
                }
                return 0;
            }
        }

        public override int CylinderMid()
        {
            /* 条件
              *    1：X轴未运行
              *    2：Y轴未运行
              *    3：X轴未报警
              *    4：Y轴未报警
              *    5：接液盘收回状态
              */

            bool bReset = false;
            int iCylinderMid = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Cylinder_Mid);
            if (-1 == iCylinderMid)
                return -1;
            else if (1 == iCylinderMid)
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

                    Block.Block block = new Block.Block_Condition();
                    if (-1 == block.Block_Out())
                        return -1;


                    int iRes = CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Cylinder_Up, 0);
                    if (-1 == iRes)
                        return -1;

                    iRes = CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Cylinder_Down, 1);
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
                        iCylinderMid = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Cylinder_Mid);
                        if (-1 == iCylinderMid)
                            return -1;
                        else if (1 == iCylinderMid)
                            break;
                        if (bDelay)
                            s = CardObject.InsertD("气缸中超时", " CylinderMid");

                    }
                    if (bDelay)
                        Lib_Card.CardObject.DeleteD(s);


                    iRes = CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Cylinder_Up, 0);
                    if (-1 == iRes)
                        return -1;

                    iRes = CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Cylinder_Down, 0);
                    if (-1 == iRes)
                        return -1;

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
                            string s = CardObject.InsertD("接液盘未收回，请检查，确定收回请点是，退出运行请点否", " CylinderMid");
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
                                throw new Exception("接液盘未收回");
                            }
                        }

                    }
                }
            }
        }
    }
}
