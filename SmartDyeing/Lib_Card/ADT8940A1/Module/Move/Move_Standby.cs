using System;
using System.Threading;

namespace Lib_Card.ADT8940A1.Module.Move
{
    /// <summary>
    /// 待机位移动
    /// </summary>
    public class Move_Standby : Move
    {
        public override int TargetMove(int iCylinderVersion, int iNo)
        {
            if (iNo == 1 || Lib_Card.Configure.Parameter.Other_ActualPosition == 0)
            {
                if (!Home.Home.Home_XYZFinish)
                {
                    Home.Home home = new Home.Home_Condition();
                    if (-1 == home.Home_XYZ(iCylinderVersion))
                        return -1;
                }


                int iXRes = -1;
                Thread threadX = new Thread(() =>
                {
                    try
                    {
                        iXRes = CardObject.OA1Axis.Absolute_X(iCylinderVersion, Configure.Parameter.Coordinate_Standby_X, 0);
                    }
                    catch (Exception ex)
                    {
                        if ("X轴矢能未接通" == ex.Message)
                            iXRes = -3;
                        if ("X轴伺服器报警" == ex.Message)
                            iXRes = -4;
                        if ("X轴正限位已通" == ex.Message)
                            iXRes = -5;
                        if ("X轴反限位已通" == ex.Message)
                            iXRes = -6;
                    }
                });
                threadX.Start();

                int iYRes = CardObject.OA1Axis.Absolute_Y(iCylinderVersion, Configure.Parameter.Coordinate_Standby_Y, 0);
                if (0 != iYRes)
                    return iYRes;

                threadX.Join();
                if (-1 == iXRes)
                    return -1;
                else if (-2 == iYRes)
                    return -2;
                else if (-3 == iXRes)
                    throw new Exception("X轴矢能未接通");
                else if (-4 == iXRes)
                    throw new Exception("X轴伺服器报警");
                else if (-5 == iXRes)
                    throw new Exception("X轴正限位已通");
                else if (-6 == iXRes)
                    throw new Exception("X轴反限位已通");
            }


            OutPut.X_Power.X_Power x_Power = new OutPut.X_Power.X_Power_Condition();
            if (-1 == x_Power.X_Power_Off())
                return -1;

            OutPut.Y_Power.Y_Power y_Power = new OutPut.Y_Power.Y_Power_Condition();
            if (-1 == y_Power.Y_Power_Off())
                return -1;

            OutPut.Tray.Tray tray = new OutPut.Tray.Tray_Condition();
            if (-1 == tray.Tray_Off())
                return -1;

            if (Lib_Card.Configure.Parameter.Other_ActualPosition == 0)
            {
                Home.Home.Home_XYZFinish = false;
            }

            return 0;

        }
    }
}
