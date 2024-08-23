namespace Lib_Card.ADT8940D1.InPut
{
    /// <summary>
    /// 原始版
    /// </summary>
    public class InPut_Basic : InPut
    {
        public override int InPutStatus(int iInPutNo)
        {
          
            int iRes = CardObject.OD1.ReadInPut(iInPutNo);
            if (-1 == iRes)
                return -1;

            if (iInPutNo == ADT8940D1_IO.InPut_Stop)
            {
                if (0 == iRes)
                    return 0;
                else
                    return 1;
            }
            else
            {
                if (0 == iRes)
                    return 1;
                else
                    return 0;
            }

        }
    }
}
