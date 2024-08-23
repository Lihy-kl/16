using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lib_Card.ADT8940A1.Module.Move
{
    /// <summary>
    /// 配液杯盖移动
    /// </summary>
    public class Move_Cupcover : Move
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

            //计算坐标
            int iXPules = 0, iYPules = 0;

            iXPules = Configure.Parameter.Coordinate_AreaCover_IntervalX -
                            ((iNo - 1) % Configure.Parameter.Machine_AreaCover_Row) * Configure.Parameter.Coordinate_AreaCover_IntervalX;
            iYPules = Configure.Parameter.Coordinate_Area1_Y -
                ((iNo - 1) / Configure.Parameter.Machine_AreaCover_Row) * Configure.Parameter.Coordinate_AreaCover_IntervalY;



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
