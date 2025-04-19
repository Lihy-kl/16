using System;
using System.Threading;

namespace Lib_Card.ADT8940A1.OutPut.Water
{
    /// <summary>
    /// 带条件检查
    /// </summary>
    public class Water_Condition : Water
    {
        public override int Water_Off()
        {
            
            if (-1 == CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Water, 0))
                return -1;
            return 0;
        }

        public override int Water_On()
        {
            /* 条件
             *     1：X轴未运行
             *     2：Y轴未运行
             *     3：接液盘伸出状态
             * 
             */

            labelTop:
            bool bReset = false;
            lable:
            int iXStatus = CardObject.OA1.ReadAxisStatus(ADT8940A1_IO.Axis_X);
            if (-1 == iXStatus)
                return -1;

            int iYStatus = CardObject.OA1.ReadAxisStatus(ADT8940A1_IO.Axis_Y);
            if (-1 == iYStatus)
                return -1;

           

            int iTray_Out = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_Tray_Out);
            if (-1 == iTray_Out)
                return -1;
            else if (0 == iTray_Out)
            {
                //接液盘出
                Lib_Card.ADT8940A1.OutPut.Tray.Tray tray = new Lib_Card.ADT8940A1.OutPut.Tray.Tray_Condition();
                if (-1 == tray.Tray_On())
                    return -1;
                goto labelTop;
            }

            if (0 == iXStatus && 0 == iYStatus && 1 == iTray_Out)
            {
                int iRes = CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Water, 1);
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
                            s = CardObject.InsertD("接液盘未伸出，请检查，确定伸出请点是，退出运行请点否", " Water_On");
                        else
                            s = CardObject.InsertD("The liquid plate is not extended, please check to make sure that the extension point is yes, and the exit point is no", " Water_On");

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
                            throw new Exception("接液盘未伸出");
                        }
                    }
                }

            }
        }
    }
}
