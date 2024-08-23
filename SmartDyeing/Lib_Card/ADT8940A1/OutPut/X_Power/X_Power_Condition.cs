using System;

namespace Lib_Card.ADT8940A1.OutPut.X_Power
{
    /// <summary>
    /// 带条件检查
    /// </summary>
    public class X_Power_Condition : X_Power
    {
        public override int X_Power_Off()
        {
            /* 条件
             *     1：X轴未运行
             * 
             */

           

            int iXStatus = CardObject.OA1.ReadAxisStatus(ADT8940A1_IO.Axis_X);
            if (-1 == iXStatus)
                return -1;

            if (0 == iXStatus)
            {
                int iRes = CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_X_Power, 0);
                if (-1 == iRes)
                    return -1;
                return 0;
            }
            else
            {
                throw new Exception("X轴正在运行");
            }
        }

        public override int X_Power_On()
        {
            /* 条件
             *     1：X轴未报警
             *     2：X轴未运行
             * 
             */

          
           

            int iXAlarm = CardObject.OA1Input.InPutStatus(ADT8940A1_IO.InPut_X_Alarm);
            if (-1 == iXAlarm)
                return -1;

            int iXStatus = CardObject.OA1.ReadAxisStatus(ADT8940A1_IO.Axis_X);
            if (-1 == iXStatus)
                return -1;

            if (0 == iXAlarm && 0 == iXStatus)
            {
                int iRes = CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_X_Power, 1);
                if (-1 == iRes)
                    return -1;
                return 0;
            }
            else
            {
                if (1 == iXAlarm)
                    throw new Exception("X轴伺服器报警");
                else
                    throw new Exception("X轴正在运行");
            }
        }
    }
}
