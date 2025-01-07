namespace Lib_Card.ADT8940A1.OutPut.Tongs
{
    /// <summary>
    /// 无条件检查
    /// </summary>
    public class Tongs_Basic : Tongs
    {
        public override int Tongs_Off()
        {
            if (Lib_Card.Configure.Parameter.Machine_TongsVersion == 0)
            {
                if (-1 == CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Tongs, 0))
                    return -1;
            }
            else
            {
                if (-1 == CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Tongs, 0))
                    return -1;
                if (-1 == CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_TongsOff, 1))
                    return -1;
            }
            return 0;
        }

        public override int Tongs_On()
        {
            if (Lib_Card.Configure.Parameter.Machine_TongsVersion == 0)
            {
                if (-1 == CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Tongs, 1))
                    return -1;
            }
            else
            {
                if (-1 == CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Tongs, 1))
                    return -1;
                if (-1 == CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_TongsOff, 0))
                    return -1;
            }
            return 0;
        }
    }
}
