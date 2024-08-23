namespace Lib_Card.ADT8940A1.OutPut.Waste
{
    /// <summary>
    /// 无条件检查
    /// </summary>
    public class Waste_Basic : Waste
    {
        public override int Waste_Off()
        {
            
            if (-1 == CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Waste, 0))
                return -1;
            return 0;
        }

        public override int Waste_On()
        {
           
            if (-1 == CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Waste, 1))
                return -1;
            return 0;
        }
    }
}
