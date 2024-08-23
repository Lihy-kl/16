namespace Lib_Card.ADT8940A1.OutPut.Tray
{
    /// <summary>
    /// 无条件检查
    /// </summary>
    public class Tray_Basic : Tray
    {
        public override int Tray_Off()
        {
            
            if (-1 == CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Tray, 0))
                return -1;
            return 0;
        }

        public override int Tray_On()
        {
           
            if (-1 == CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Tray, 1))
                return -1;
            return 0;
        }
    }
}
