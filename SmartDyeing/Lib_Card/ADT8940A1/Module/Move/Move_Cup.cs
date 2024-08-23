using System;
using System.Threading;

namespace Lib_Card.ADT8940A1.Module.Move
{
    /// <summary>
    /// 配液杯移动
    /// </summary>
    public class Move_Cup : Move
    {
        public override int TargetMove(int iCylinderVersion, int iNo)
        {
            if (0 >= iNo || iNo > Configure.Parameter.Machine_Cup_Total)
            {
                throw new Exception("非法杯号");
            }

            if (!Home.Home.Home_XYZFinish)
            {
                Home.Home home = new Home.Home_Condition();
                if (-1 == home.Home_XYZ(iCylinderVersion))
                    return -1;
            }
            if (Configure.Parameter.Other_IsOnlyDrip == 1)
            {
                //计算坐标
                int iXPules = 0, iYPules = 0;
                //在区域1
                iXPules = Configure.Parameter.Coordinate_Area1_X +
                    ((iNo - Configure.Parameter.Machine_Area1_CupMin) % Configure.Parameter.Machine_Area1_Col) * Configure.Parameter.Coordinate_Area1_IntervalX;
                iYPules = Configure.Parameter.Coordinate_Area1_Y +
                    ((iNo - Configure.Parameter.Machine_Area1_CupMin) / Configure.Parameter.Machine_Area1_Col) * Configure.Parameter.Coordinate_Area1_IntervalY;
                

                return 0;
            }
            else
            {

                //现有两种布局
                //1:三个打板滴液区(竖放)+加一个滴液(10杯横放)
                //2:六个正常滴液打板区
                //两种情况主要横放时，1,2,3区域计算坐标和4,5,6区域有区别，请留意

                //计算坐标
                int iXPules = 0, iYPules = 0;

                if (iNo >= Configure.Parameter.Machine_Area1_CupMin && iNo <= Configure.Parameter.Machine_Area1_CupMax)
                {
                    if (Configure.Parameter.Machine_Area1_Layout == 1)
                    {
                        //在区域1
                        iXPules = Configure.Parameter.Coordinate_Area1_X -
                            ((iNo - Configure.Parameter.Machine_Area1_CupMin) / Configure.Parameter.Machine_Area1_Row) * Configure.Parameter.Coordinate_Area1_IntervalX;

                        iYPules = Configure.Parameter.Coordinate_Area1_Y +
                            ((iNo - Configure.Parameter.Machine_Area1_CupMin) % Configure.Parameter.Machine_Area1_Row) * Configure.Parameter.Coordinate_Area1_IntervalY;

                    }
                    else
                    {
                        //在区域1
                        iXPules = Configure.Parameter.Coordinate_Area1_X -
                            ((iNo - Configure.Parameter.Machine_Area1_CupMin) % Configure.Parameter.Machine_Area1_Col) * Configure.Parameter.Coordinate_Area1_IntervalX;
                        iYPules = Configure.Parameter.Coordinate_Area1_Y -
                            ((iNo - Configure.Parameter.Machine_Area1_CupMin) / Configure.Parameter.Machine_Area1_Col) * Configure.Parameter.Coordinate_Area1_IntervalY;
                    }
                }
                else if (iNo >= Configure.Parameter.Machine_Area2_CupMin && iNo <= Configure.Parameter.Machine_Area2_CupMax)
                {
                    if (Configure.Parameter.Machine_Area2_Layout == 1)
                    {
                        //在区域2
                        iXPules = Configure.Parameter.Coordinate_Area2_X -
                      ((iNo - Configure.Parameter.Machine_Area2_CupMin) / Configure.Parameter.Machine_Area2_Row) * Configure.Parameter.Coordinate_Area2_IntervalX;

                        iYPules = Configure.Parameter.Coordinate_Area2_Y +
                        ((iNo - Configure.Parameter.Machine_Area2_CupMin) % Configure.Parameter.Machine_Area2_Row) * Configure.Parameter.Coordinate_Area2_IntervalY;

                    }
                    else
                    {
                        //在区域2
                        iXPules = Configure.Parameter.Coordinate_Area2_X -
                          ((iNo - Configure.Parameter.Machine_Area2_CupMin) % Configure.Parameter.Machine_Area2_Col) * Configure.Parameter.Coordinate_Area2_IntervalX;

                        iYPules = Configure.Parameter.Coordinate_Area2_Y -
                        ((iNo - Configure.Parameter.Machine_Area2_CupMin) / Configure.Parameter.Machine_Area2_Col) * Configure.Parameter.Coordinate_Area2_IntervalY;
                    }
                }
                else if (iNo >= Configure.Parameter.Machine_Area3_CupMin && iNo <= Configure.Parameter.Machine_Area3_CupMax)
                {
                    if (Configure.Parameter.Machine_Area3_Layout == 1)
                    {
                        //在区域3
                        iXPules = Configure.Parameter.Coordinate_Area3_X -
                    ((iNo - Configure.Parameter.Machine_Area3_CupMin) / Configure.Parameter.Machine_Area3_Row) * Configure.Parameter.Coordinate_Area3_IntervalX;

                        iYPules = Configure.Parameter.Coordinate_Area3_Y +
                        ((iNo - Configure.Parameter.Machine_Area3_CupMin) % Configure.Parameter.Machine_Area3_Row) * Configure.Parameter.Coordinate_Area3_IntervalY;

                    }
                    else
                    {
                        //在区域3
                        iXPules = Configure.Parameter.Coordinate_Area3_X -
                          ((iNo - Configure.Parameter.Machine_Area3_CupMin) % Configure.Parameter.Machine_Area3_Col) * Configure.Parameter.Coordinate_Area3_IntervalX;

                        iYPules = Configure.Parameter.Coordinate_Area3_Y -
                        ((iNo - Configure.Parameter.Machine_Area3_CupMin) / Configure.Parameter.Machine_Area3_Col) * Configure.Parameter.Coordinate_Area3_IntervalY;
                    }
                }
                else if (iNo >= Configure.Parameter.Machine_Area4_CupMin && iNo <= Configure.Parameter.Machine_Area4_CupMax)
                {
                    if (Configure.Parameter.Machine_Area4_Layout == 1)
                    {
                        //在区域4
                        iXPules = Configure.Parameter.Coordinate_Area4_X -
                    ((iNo - Configure.Parameter.Machine_Area4_CupMin) / Configure.Parameter.Machine_Area4_Row) * Configure.Parameter.Coordinate_Area4_IntervalX;

                        iYPules = Configure.Parameter.Coordinate_Area4_Y +
                        ((iNo - Configure.Parameter.Machine_Area4_CupMin) % Configure.Parameter.Machine_Area4_Row) * Configure.Parameter.Coordinate_Area4_IntervalY;

                    }
                    else
                    {
                        //在区域4
                        iXPules = Configure.Parameter.Coordinate_Area4_X +
                          ((iNo - Configure.Parameter.Machine_Area4_CupMin) % Configure.Parameter.Machine_Area4_Col) * Configure.Parameter.Coordinate_Area4_IntervalX;

                        iYPules = Configure.Parameter.Coordinate_Area4_Y +
                        ((iNo - Configure.Parameter.Machine_Area4_CupMin) / Configure.Parameter.Machine_Area4_Col) * Configure.Parameter.Coordinate_Area4_IntervalY;
                    }
                }
                else if (iNo >= Configure.Parameter.Machine_Area5_CupMin && iNo <= Configure.Parameter.Machine_Area5_CupMax)
                {
                    if (Configure.Parameter.Machine_Area5_Layout == 1)
                    {
                        //在区域5
                        iXPules = Configure.Parameter.Coordinate_Area5_X -
                    ((iNo - Configure.Parameter.Machine_Area5_CupMin) / Configure.Parameter.Machine_Area5_Row) * Configure.Parameter.Coordinate_Area5_IntervalX;

                        iYPules = Configure.Parameter.Coordinate_Area5_Y +
                        ((iNo - Configure.Parameter.Machine_Area5_CupMin) % Configure.Parameter.Machine_Area5_Row) * Configure.Parameter.Coordinate_Area5_IntervalY;
                    }
                    else
                    {
                        //在区域5
                        iXPules = Configure.Parameter.Coordinate_Area5_X +
                      ((iNo - Configure.Parameter.Machine_Area5_CupMin) % Configure.Parameter.Machine_Area5_Col) * Configure.Parameter.Coordinate_Area5_IntervalX;

                        iYPules = Configure.Parameter.Coordinate_Area5_Y +
                        ((iNo - Configure.Parameter.Machine_Area5_CupMin) / Configure.Parameter.Machine_Area5_Col) * Configure.Parameter.Coordinate_Area5_IntervalY;
                    }
                }
                else
                {
                    if (Configure.Parameter.Machine_Area6_Layout == 1)
                    {
                        //在区域6
                        iXPules = Configure.Parameter.Coordinate_Area6_X -
                    ((iNo - Configure.Parameter.Machine_Area6_CupMin) / Configure.Parameter.Machine_Area6_Row) * Configure.Parameter.Coordinate_Area6_IntervalX;

                        iYPules = Configure.Parameter.Coordinate_Area6_Y +
                        ((iNo - Configure.Parameter.Machine_Area6_CupMin) % Configure.Parameter.Machine_Area6_Row) * Configure.Parameter.Coordinate_Area6_IntervalY;
                    }
                    else
                    {
                        //在区域6
                        iXPules = Configure.Parameter.Coordinate_Area6_X +
                     ((iNo - Configure.Parameter.Machine_Area6_CupMin) % Configure.Parameter.Machine_Area6_Col) * Configure.Parameter.Coordinate_Area6_IntervalX;
                        iYPules = Configure.Parameter.Coordinate_Area6_Y +
                        ((iNo - Configure.Parameter.Machine_Area6_CupMin) / Configure.Parameter.Machine_Area6_Col) * Configure.Parameter.Coordinate_Area6_IntervalY;
                    }

                }


                // iXPules = Configure.Parameter.Coordinate_Cup_X +
                //    ((iNo - 1) / (Configure.Parameter.Machine_Cup_Total / Configure.Parameter.Machine_Cup_Column)) *
                //    Configure.Parameter.Coordinate_Cup_IntervalX +
                //    ((iNo - 1) / 10) * Configure.Parameter.Coordinate_Cup_IntervalM;


                //if ((iNo >= 1 && iNo <= 5) || (iNo >= 11 && iNo <= 15) || (iNo >= 21 && iNo <= 25))
                //{
                //    iYPules = Configure.Parameter.Coordinate_Cup_Y +
                //       ((iNo - 1) % (Configure.Parameter.Machine_Cup_Total / Configure.Parameter.Machine_Cup_Column))
                //       * Configure.Parameter.Coordinate_Cup_IntervalY;
                //}
                //else
                //{
                //    if (0 != iNo % 10)
                //    {
                //        iYPules = Configure.Parameter.Coordinate_Cup_Y + (10 - (iNo % 10)) * Configure.Parameter.Coordinate_Cup_IntervalY;
                //    }
                //    else
                //    {
                //        iYPules = Configure.Parameter.Coordinate_Cup_Y;
                //    }
                //}




                // iXPules = Configure.Parameter.Coordinate_Cup_X +
                //    ((iNo - 1) / (Configure.Parameter.Machine_Cup_Total / Configure.Parameter.Machine_Cup_Column)) *
                //    Configure.Parameter.Coordinate_Cup_IntervalX +
                //    ((iNo - 1) / 10) * Configure.Parameter.Coordinate_Cup_IntervalM;


                //if ((iNo >= 1 && iNo <= 5) || (iNo >= 11 && iNo <= 15) || (iNo >= 21 && iNo <= 25))
                //{
                //    iYPules = Configure.Parameter.Coordinate_Cup_Y +
                //       ((iNo - 1) % (Configure.Parameter.Machine_Cup_Total / Configure.Parameter.Machine_Cup_Column))
                //       * Configure.Parameter.Coordinate_Cup_IntervalY;
                //}
                //else
                //{
                //    if (0 != iNo % 10)
                //    {
                //        iYPules = Configure.Parameter.Coordinate_Cup_Y + (10 - (iNo % 10)) * Configure.Parameter.Coordinate_Cup_IntervalY;
                //    }
                //    else
                //    {
                //        iYPules = Configure.Parameter.Coordinate_Cup_Y;
                //    }
                //}



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
