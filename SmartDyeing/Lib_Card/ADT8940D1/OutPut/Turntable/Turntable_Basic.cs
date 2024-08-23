namespace Lib_Card.ADT8940D1.OutPut.Turntable
{
    /// <summary>
    /// 无条件检查
    /// </summary>
    public class Turntable_Basic : Turntable
    {
        public override int Turntable_Down()
        {
           
            if (-1 == CardObject.OD1.WriteOutPut(ADT8940D1_IO.OutPut_Turntable, 1))
                return -1;
            return 0;
        }

        public override int Turntable_Up()
        {

           
            if (-1 == CardObject.OD1.WriteOutPut(ADT8940D1_IO.OutPut_Turntable, 0))
                return -1;
            return 0;
        }
    }
}
