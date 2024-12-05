using Lib_Card.ADT8940A1.Module.Home;
using Lib_Card.ADT8940A1.OutPut.Blender;
using Lib_Card.ADT8940A1;
using Lib_Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lib_Card.ADT8940A1.OutPut.Tongs;

namespace Lib_Card.ADT8940A1.Module
{
    public class GetOrPutCover
    {
        /// <summary>
        /// 拿盖
        /// </summary>
        /// <param name="iCylinderVersion">0：单控上下气缸；1：双控上下气缸</param>
        /// <param name="iType">0:开盖 1：关盖</param>
        /// <returns>0：正常；-1：异常；-2：收到退出消息</returns>
        public int GetCover(int iCylinderVersion, int iType)
        {

            OutPut.Tray.Tray tray = new OutPut.Tray.Tray_Condition();
            if (-1 == tray.Tray_Off())
                return -1;

            OutPut.Cylinder.Cylinder cylinder;
            if (0 == iCylinderVersion)
                cylinder = new OutPut.Cylinder.SingleControl.Cylinder_Condition();
            else
                cylinder = new OutPut.Cylinder.DualControl.Cylinder_Condition();
            if (-1 == cylinder.CylinderDown(0))
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
                if (Lib_Card.Configure.Parameter.Machine_isSyringe == 1)
                {
                    iSyringe = 1;
                }
                if (-1 == iSyringe)
                    return -1;
                else if (1 == iSyringe)
                    break;

                if (bDelay)
                    break;

            }


            if (bDelay)
            {
                if (-1 == tongs.Tongs_Off())
                    return -1;

                if (-1 == cylinder.CylinderUp(0))
                    return -1;

                if (-1 == cylinder.CylinderDown(0))
                    return -1;

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
                    if (Lib_Card.Configure.Parameter.Machine_isSyringe == 1)
                    {
                        iSyringe = 1;
                    }
                    if (-1 == iSyringe)
                        return -1;
                    else if (1 == iSyringe)
                        break;

                    if (bDelay)
                        break;

                }

                if (bDelay)
                {
                    if (-1 == tongs.Tongs_Off())
                        return -1;
                    if (-1 == cylinder.CylinderUp(0))
                        return -1;

                    Home.Home home = new Home.Home_Condition();
                    if (-1 == home.Home_Z(iCylinderVersion))
                        throw new Exception("驱动异常");
                    throw new Exception("未发现杯盖");
                }
                int res = cylinder.CylinderUp(1);
                if (-1 == res)
                    return -1;
                else if (-9 == res)
                {
                    //拿着盖子升不到位，先气缸下，松开抓手，再升一次，看看是否因为气压不够导致
                    if (-1 == cylinder.CylinderDown(0))
                        return -1;
                    if (-1 == tongs.Tongs_Off())
                        return -1;

                    res = cylinder.CylinderUp(1);
                    if (-1 == res)
                        return -1;
                    else if (-9 == res)
                    {
                        //throw new Exception("气缸上超时");
                        cylinder.CylinderUp(0);
                    }
                    //松开杯盖可以正常升到位
                    if (iType == 1)
                    {
                        //杯盖区拿盖失败
                        throw new Exception("放盖区取盖失败");
                    }
                    else
                    {
                        throw new Exception("配液杯取盖失败");
                    }
                }


            }

            //获取盖子
            return 0;
        }

        /// <summary>
        /// 放盖
        /// </summary>
        /// <param name="iCylinderVersion">0：单控上下气缸；1：双控上下气缸</param>
        /// <param name="iType">0:开盖 1：关盖</param>
        /// <returns>0：正常；-1：异常；-2：收到退出消息</returns>
        public int PutCover(int iCylinderVersion, int iType)
        {
            //放盖子
            OutPut.Tray.Tray tray = new OutPut.Tray.Tray_Condition();
            if (-1 == tray.Tray_Off())
                return -1;

            OutPut.Cylinder.Cylinder cylinder;
            if (0 == iCylinderVersion)
                cylinder = new OutPut.Cylinder.SingleControl.Cylinder_Condition();
            else
                cylinder = new OutPut.Cylinder.DualControl.Cylinder_Condition();
            int res = cylinder.CylinderDown(1);
            OutPut.Tongs.Tongs tongs = new OutPut.Tongs.Tongs_Condition();
            if (-1 == res)
                return -1;
            else if (-9 == res)
            {
                
                //第一次放盖子失败
                if (-1 == cylinder.CylinderUp(0))
                    return -1;
                res = cylinder.CylinderDown(1);
                if (-1 == res)
                    return -1;
                else if (-9 == res)
                {
                    //第二次放盖子失败,松开抓手
                    
                    if (-1 == tongs.Tongs_Off())
                        return -1;
                    if (iType == 1)
                    {
                        throw new Exception("关盖失败");
                    }
                    else
                    {
                        throw new Exception("放盖失败");
                    }
                }
                
            }
            if (-1 == tongs.Tongs_Off())
                return -1;
            if (-1 == cylinder.CylinderUp(0))
                return -1;

            return 0;
        }
    }
}
