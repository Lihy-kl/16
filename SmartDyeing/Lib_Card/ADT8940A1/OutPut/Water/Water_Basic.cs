namespace Lib_Card.ADT8940A1.OutPut.Water
{
    /// <summary>
    /// 无条件检查
    /// </summary>
    public class Water_Basic : Water
    {
        public override int Water_Off()
        {
           
            if (-1 == CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Water, 0))
                return -1;
            return 0;
        }

        public override int Water_On()
        {
          
            if (-1 == CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Water, 1))
                return -1;
            return 0;
        }
    }
}
