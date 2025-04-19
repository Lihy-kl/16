namespace Lib_Card.ADT8940A1.OutPut.Blender
{
    /// <summary>
    /// 无条件检查
    /// </summary>
    public class Blender_Basic : Blender
    {
        public override int Blender_Off()
        {

            if (Lib_Card.Configure.Parameter.Machine_BlenderVersion == 1)
            {
                if (-1 == CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Blender, 0))
                    return -1;
            }
            else
            {
                if (-1 == CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Blender, 1))
                    return -1;
            }
            return 0;
        }

        public override int Blender_On()
        {

            if (Lib_Card.Configure.Parameter.Machine_BlenderVersion == 1)
            {
                if (-1 == CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Blender, 1))
                    return -1;
            }
            else
            {
                if (-1 == CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Blender, 0))
                    return -1;
            }
            return 0;
        }
    }
}
