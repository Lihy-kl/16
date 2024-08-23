using System;

namespace Lib_Card.ADT8940D1.OutPut.Water
{
    /// <summary>
    /// 带条件检查
    /// </summary>
    public class Water_Condition : Water
    {
        public override int Water_Off()
        {
          
            if (-1 == CardObject.OD1.WriteOutPut(ADT8940D1_IO.OutPut_Water, 0))
                return -1;
            return 0;
        }

        public override int Water_On()
        {
            /* 条件
             *     1：转盘未运行
             * 
             */

           

            int iTStatus = CardObject.OD1.ReadAxisStatus(ADT8940D1_IO.Axis_T);
            if (-1 == iTStatus)
                return -1;

            if (0 == iTStatus)
            {
                int iRes = CardObject.OD1.WriteOutPut(ADT8940D1_IO.OutPut_Water, 1);
                if (-1 == iRes)
                    return -1;                
                return 0;

            }
            else
            {
                throw new Exception("转盘正在运行");
            }
        }
    }
}
