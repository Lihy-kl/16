using Lib_Card.ADT8940A1.Module.OpenCover;
using Lib_Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Card.ADT8940A1.Module.PutCover
{
    public class PutCover_Condition : PutCover
    {
        public override int Put(int iCylinderVersion)
        {
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
            if (-1 == tongs.Tongs_Off())
                return -1;

            if (-1 == cylinder.CylinderUp())
                return -1;

            return 0;

        }
    }

}
