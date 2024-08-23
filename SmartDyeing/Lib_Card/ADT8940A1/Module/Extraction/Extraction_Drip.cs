using System;
using System.Threading;

namespace Lib_Card.ADT8940A1.Module.Extraction
{
    public class Extraction_Drip : Extraction
    {
        public override int FluidExtraction(int iCylinderVersion, int iSyringeType, int iPulse, int iIsAssitant)
        {
            OutPut.Blender.Blender blender = new OutPut.Blender.Blender_Basic();
            if (-1 == blender.Blender_On())
                return -1;

            OutPut.Tray.Tray tray = new OutPut.Tray.Tray_Condition();
            if (-1 == tray.Tray_Off())
                return -1;

            OutPut.Cylinder.Cylinder cylinder;
            if (0 == iCylinderVersion)
                cylinder = new OutPut.Cylinder.SingleControl.Cylinder_Condition();
            else
                cylinder = new OutPut.Cylinder.DualControl.Cylinder_Condition();
            if (-1 == cylinder.CylinderDown())
                return -1;


            OutPut.Tongs.Tongs tongs = new OutPut.Tongs.Tongs_Condition();
            if (-1 == tongs.Tongs_On())
                return -1;

            bool bDelay = false;
            Thread threadS = new Thread(() =>
            {
                int iDelay = Convert.ToInt32(Configure.Parameter.Delay_Syringe * 1000.00);
                Thread.Sleep(iDelay);
                bDelay = true;
            });
            threadS.Start();

            int iSyringe = 0;
           
            while (true)
            {

                Thread.Sleep(1);
                iSyringe = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Syringe);
                if (-1 == iSyringe)
                    return -1;
                else if (1 == iSyringe)
                    break;

                if (bDelay)
                    break;
                
            }

           
            Base.Card.MoveArg s_Movearg = new Base.Card.MoveArg();
            if (bDelay && 0 == iSyringe)
            {
                if (-1 == tongs.Tongs_Off())
                    return -1;

                if (0 == iSyringeType)
                {
                    s_Movearg.Pulse = -Configure.Parameter.Home_Z_Offset;
                    s_Movearg.LSpeed = Configure.Parameter.Move_S_LSpeed;
                    s_Movearg.HSpeed = Configure.Parameter.Move_S_HSpeed;
                    s_Movearg.Time = Configure.Parameter.Move_S_UTime;
                }
                else if (1 == iSyringeType)
                {
                    s_Movearg.Pulse = -Configure.Parameter.Home_Z_Offset;
                    s_Movearg.LSpeed = Configure.Parameter.Move_B_LSpeed;
                    s_Movearg.HSpeed = Configure.Parameter.Move_B_HSpeed;
                    s_Movearg.Time = Configure.Parameter.Move_B_UTime;
                }
                try
                {
                    if (-1 == CardObject.OA1Axis.Relative_Z(s_Movearg, 0))
                        return -1;
                }
                catch (Exception ex)
                {
                    if ("Z轴正限位已通" != ex.Message)
                        throw;
                }

                if (-1 == tongs.Tongs_On())
                    return -1;

                bDelay = false;
                threadS = new Thread(() =>
                {
                    int iDelay = Convert.ToInt32(Configure.Parameter.Delay_Syringe * 1000.00);
                    Thread.Sleep(iDelay);
                    bDelay = true;
                });
                threadS.Start();

                while (true)
                {
                    Thread.Sleep(1);
                    iSyringe = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Syringe);
                    if (-1 == iSyringe)
                        return -1;
                    else if (1 == iSyringe)
                        break;

                    if (bDelay)
                        break;
                   
                }

                if (bDelay && 0 == iSyringe)
                {
                    if (-1 == tongs.Tongs_Off())
                        return -1;
                    if (-1 == cylinder.CylinderUp())
                        return -1;

                    Home.Home home = new Home.Home_Condition();
                    if (-1 == home.Home_Z(iCylinderVersion))
                        throw new Exception("驱动异常");
                    if (-1 == blender.Blender_Off())
                        return -1;
                    throw new Exception("未发现针筒");
                }
            }

            //助剂时做下排空
            if(iIsAssitant == 1)
            {
                int iZRes1 = CardObject.OA1Axis.Absolute_Z(iSyringeType, Configure.Parameter.Other_Z_UpPulse, 0);
                if (0 != iZRes1)
                    return iZRes1;

                try
                {
                    iZRes1 = CardObject.OA1Axis.Absolute_Z(iSyringeType, Configure.Parameter.Other_Z_DownPulse, 0);
                    if (0 != iZRes1)
                        return iZRes1;
                }
                catch (Exception ex)
                {
                    if ("Z轴正限位已通" != ex.Message)
                        throw;
                }
            }

            int iTotalPulse = iPulse - Configure.Parameter.Other_Z_BackPulse;
            int iZRes = CardObject.OA1Axis.Absolute_Z(iSyringeType, iTotalPulse, 0);
            if (0 != iZRes)
                return iZRes;
            if (Configure.Parameter.Other_Push == 1)
            {
                //先反推完在升气缸
                iZRes = CardObject.OA1Axis.Absolute_Z(iSyringeType, iPulse, 0);
                if (0 != iZRes)
                    return iZRes;

                int iCRes = -1;
                Thread threadC = new Thread(() =>
                {
                    try
                    {
                        iCRes = cylinder.CylinderUp();
                    }
                    catch (Exception ex)
                    {
                        if ("气缸上超时" == ex.Message)
                            iCRes = -2;
                        else if ("阻挡气缸收回超时" == ex.Message)
                            iCRes = -3;
                    }

                });

                threadC.Start();
                

                threadC.Join();

                if (-1 == iCRes)
                    return -1;
                else if (-2 == iCRes)
                    throw new Exception("气缸上超时");
                else if (-3 == iCRes)
                    throw new Exception("阻挡气缸收回超时");
            }
            else
            {
                int iCRes = -1;
                Thread threadC = new Thread(() =>
                {
                    try
                    {
                        iCRes = cylinder.CylinderUp();
                    }
                    catch (Exception ex)
                    {
                        if ("气缸上超时" == ex.Message)
                            iCRes = -2;
                        else if ("阻挡气缸收回超时" == ex.Message)
                            iCRes = -3;
                    }

                });

                threadC.Start();
                iZRes = CardObject.OA1Axis.Absolute_Z(iSyringeType, iPulse, 0);
                if (0 != iZRes)
                    return iZRes;

                threadC.Join();

                if (-1 == iCRes)
                    return -1;
                else if (-2 == iCRes)
                    throw new Exception("气缸上超时");
                else if (-3 == iCRes)
                    throw new Exception("阻挡气缸收回超时");
            }

            if (-1 == tray.Tray_On())
                return -1;

            if (-1 == blender.Blender_Off())
                return -1;
            return 0;
        }
    }
}
