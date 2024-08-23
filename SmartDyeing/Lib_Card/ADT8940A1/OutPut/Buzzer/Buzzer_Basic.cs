namespace Lib_Card.ADT8940A1.OutPut.Buzzer
{
    /// <summary>
    /// 无条件检查
    /// </summary>
    public class Buzzer_Basic : Buzzer
    {
        public override int Buzzer_Off()
        {
           
            if (-1 == CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Buzzer, 0))
                return -1;
            return 0;
        }

        public override int Buzzer_On()
        {
            
            if (-1 == CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Buzzer, 1))
                return -1;
            return 0;
        }
    }
}
