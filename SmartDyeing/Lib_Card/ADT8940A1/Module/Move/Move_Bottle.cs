using SmartDyeing.FADM_Object;
using System;
using System.Drawing;
using System.Threading;

namespace Lib_Card.ADT8940A1.Module.Move
{
    /// <summary>
    /// 母液瓶移动
    /// </summary>
    public class Move_Bottle : Move
    {
        public override int TargetMove(int iCylinderVersion, int iNo, int iX, int iY, int iType)
        {
            if(0 >= iNo || iNo > Configure.Parameter.Machine_Bottle_Total)
            {
                throw new Exception("非法瓶号");
            }

            if (!Home.Home.Home_XYZFinish)
            {
                Home.Home home = new Home.Home_Condition();
                if (-1 == home.Home_XYZ(iCylinderVersion))
                    return -1;
               
            }

            if (Communal._b_isBalanceInDrip)
            {
                //计算坐标
                int iXPules = 0;
                int iYPules = 0;

                if (Configure.Parameter.Machine_Bottle_Total < iNo)
                    throw new Exception("非法瓶号");
                else
                {
                    if (Configure.Parameter.Machine_Bottle_Total - 14 >= iNo)
                    {
                        iXPules = Configure.Parameter.Coordinate_Bottle_X - (iNo - 1) %
                            Configure.Parameter.Machine_Bottle_Column * Configure.Parameter.Coordinate_Bottle_Interval;
                        iYPules = Configure.Parameter.Coordinate_Bottle_Y + (iNo - 1) /
                            Configure.Parameter.Machine_Bottle_Column * Configure.Parameter.Coordinate_Bottle_Interval;
                    }
                    else if (Configure.Parameter.Machine_Bottle_Total - 7 >= iNo)
                    {
                        iXPules = Configure.Parameter.Coordinate_Bottle_X -
                            ((iNo + 14 - Configure.Parameter.Machine_Bottle_Total) % 8 + 2)
                            * Configure.Parameter.Coordinate_Bottle_Interval;
                        iYPules = Configure.Parameter.Coordinate_Bottle_Y +
                            ((Configure.Parameter.Machine_Bottle_Total - 14) /
                            Configure.Parameter.Machine_Bottle_Column +
                            (iNo + 14 - Configure.Parameter.Machine_Bottle_Total) / 8)
                            * Configure.Parameter.Coordinate_Bottle_Interval;


                    }
                    else
                    {
                        iXPules = Configure.Parameter.Coordinate_Bottle_X -
                           ((iNo + 14 - Configure.Parameter.Machine_Bottle_Total) % 8 + 3)
                           * Configure.Parameter.Coordinate_Bottle_Interval;
                        iYPules = Configure.Parameter.Coordinate_Bottle_Y +
                            ((Configure.Parameter.Machine_Bottle_Total - 14) /
                            Configure.Parameter.Machine_Bottle_Column +
                            (iNo + 14 - Configure.Parameter.Machine_Bottle_Total) / 8)
                            * Configure.Parameter.Coordinate_Bottle_Interval;
                    }
                }




                int iXRes = -1;
                Thread threadX = new Thread(() =>
                {
                    try
                    {
                        iXRes = CardObject.OA1Axis.Absolute_X(iCylinderVersion, iXPules, 0);
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

                int iYRes = CardObject.OA1Axis.Absolute_Y(iCylinderVersion, iYPules, 0);
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

                return 0;
            }
            else
            {
                //计算坐标
                int iXPules = 0;
                int iYPules = 0;

                if (Configure.Parameter.Machine_Bottle_Total < iNo)
                    throw new Exception("非法瓶号");
                else
                {
                    //if (Configure.Parameter.Machine_Bottle_Total - 14 >= iNo)
                    //{
                        iXPules = Configure.Parameter.Coordinate_Bottle_X - (iNo - 1) %
                            Configure.Parameter.Machine_Bottle_Column * Configure.Parameter.Coordinate_Bottle_Interval;
                        iYPules = Configure.Parameter.Coordinate_Bottle_Y + (iNo - 1) /
                            Configure.Parameter.Machine_Bottle_Column * Configure.Parameter.Coordinate_Bottle_Interval;
                    //}
                    //else if (Configure.Parameter.Machine_Bottle_Total - 7 >= iNo)
                    //{
                    //    iXPules = Configure.Parameter.Coordinate_Bottle_X -
                    //        ((iNo + 14 - Configure.Parameter.Machine_Bottle_Total) % 8 + 2)
                    //        * Configure.Parameter.Coordinate_Bottle_Interval;
                    //    iYPules = Configure.Parameter.Coordinate_Bottle_Y +
                    //        ((Configure.Parameter.Machine_Bottle_Total - 14) /
                    //        Configure.Parameter.Machine_Bottle_Column +
                    //        (iNo + 14 - Configure.Parameter.Machine_Bottle_Total) / 8)
                    //        * Configure.Parameter.Coordinate_Bottle_Interval;


                    //}
                    //else
                    //{
                    //    iXPules = Configure.Parameter.Coordinate_Bottle_X -
                    //       ((iNo + 14 - Configure.Parameter.Machine_Bottle_Total) % 8 + 3)
                    //       * Configure.Parameter.Coordinate_Bottle_Interval;
                    //    iYPules = Configure.Parameter.Coordinate_Bottle_Y +
                    //        ((Configure.Parameter.Machine_Bottle_Total - 14) /
                    //        Configure.Parameter.Machine_Bottle_Column +
                    //        (iNo + 14 - Configure.Parameter.Machine_Bottle_Total) / 8)
                    //        * Configure.Parameter.Coordinate_Bottle_Interval;
                    //}
                }




                int iXRes = -1;
                Thread threadX = new Thread(() =>
                {
                    try
                    {
                        iXRes = CardObject.OA1Axis.Absolute_X(iCylinderVersion, iXPules, 0);
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

                int iYRes = CardObject.OA1Axis.Absolute_Y(iCylinderVersion, iYPules, 0);
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

                return 0;
            }
        }
    }
}
