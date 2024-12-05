using static Lib_Card.Base.Card;
using System;

namespace Lib_Card.ADT8940A1.Module.Put
{
    public class Put_Condition : Put
    {
        public override int PutSyringe(int iCylinderVersion, int iSyringeType)
        {
            OutPut.Tray.Tray tray = new OutPut.Tray.Tray_Condition();
            if (-1 == tray.Tray_Off())
                return -1;

            OutPut.Cylinder.Cylinder cylinder;
            if (0 == iCylinderVersion)
                cylinder = new OutPut.Cylinder.SingleControl.Cylinder_Condition();
            else
                cylinder = new OutPut.Cylinder.DualControl.Cylinder_Condition();

            if(-1 == cylinder.CylinderDown(0))
                return -1;


            int iZRes = CardObject.OA1Axis.Absolute_Z(iSyringeType, 0, 0);
            if (0 != iZRes)
                return iZRes;

            OutPut.Tongs.Tongs tongs = new OutPut.Tongs.Tongs_Condition();
            if(-1 == tongs.Tongs_Off())
                return -1;

            if(-1 == cylinder.CylinderUp(0))
                return -1;

            return 0;
            
        }
    }
}
