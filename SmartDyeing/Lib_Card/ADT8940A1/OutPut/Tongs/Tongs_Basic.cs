namespace Lib_Card.ADT8940A1.OutPut.Tongs
{
    /// <summary>
    /// 无条件检查
    /// </summary>
    public class Tongs_Basic : Tongs
    {
        public override int Tongs_Off()
        {
           
            if (-1 == CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Tongs, 0))
                return -1;
            return 0;
        }

        public override int Tongs_On()
        {
            
            if (-1 == CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Tongs, 1))
                return -1;
            return 0;
        }
    }
}
