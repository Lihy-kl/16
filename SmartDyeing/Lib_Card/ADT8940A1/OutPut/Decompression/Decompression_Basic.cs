using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Card.ADT8940A1.OutPut.Decompression
{
    public class Decompression_Basic : Decompression
    {
        public override int Decompression_Down()
        {
           
            if (-1 == CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Decompression, 1))
                return -1;
            return 0;
        }

        public override int Decompression_Up()
        {
           
            if (-1 == CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Decompression, 0))
                return -1;
            return 0;
        }

        public override int Decompression_Down_Right()
        {

            if (-1 == CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Decompression_Right, 1))
                return -1;
            return 0;
        }

        public override int Decompression_Up_Right()
        {

            if (-1 == CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Decompression_Right, 0))
                return -1;
            return 0;
        }
    }
}
