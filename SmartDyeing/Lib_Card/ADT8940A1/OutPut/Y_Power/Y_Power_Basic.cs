namespace Lib_Card.ADT8940A1.OutPut.Y_Power
{
    /// <summary>
    /// 无条件检查
    /// </summary>
    public class Y_Power_Basic : Y_Power
    {
        public override int Y_Power_Off()
        {

            if (-1 == CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Y_Power, 0))
                return -1;
            return 0;
        }

        public override int Y_Power_On()
        {

            if (-1 == CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Y_Power, 1))
                return -1;
            return 0;
        }
    }
}
