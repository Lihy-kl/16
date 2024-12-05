namespace Lib_Card.ADT8940A1.OutPut.Cylinder.SingleControl
{
    /// <summary>
    /// 单控版
    /// 无条件检查
    /// </summary>
    public class Cylinder_Basic : Cylinder
    {
        public override int CylinderDown(int i_judge)
        {
            
            if (-1 == CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Cylinder_Down, 1))
                return -1;
            return 0;
        }

        public override int CylinderUp(int i_judge)
        {
           
            if (-1 == CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Cylinder_Down, 0))
                return -1;
            return 0;
        }

        public override int CylinderMid()
        {
            return -1;
        }
    }
}
