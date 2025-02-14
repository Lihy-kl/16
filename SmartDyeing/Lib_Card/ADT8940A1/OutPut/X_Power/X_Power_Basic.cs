using System.Threading;

namespace Lib_Card.ADT8940A1.OutPut.X_Power
{
    /// <summary>
    /// 无条件检查
    /// </summary>
    public class X_Power_Basic : X_Power
    {
        public override int X_Power_Off()
        {
            
            if (-1 == CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_X_Power, 0))
                return -1;
            return 0;
        }

        public override int X_Power_On()
        {
           
            if (-1 == CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_X_Power, 1))
                return -1;
            if (Lib_Card.Configure.Parameter.Machine_Type == 0 && Lib_Card.Configure.Parameter.Machine_Type_Lv == 1)
            {
                Thread.Sleep(500);
            }
            return 0;
        }
    }
}
