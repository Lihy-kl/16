using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Card.ADT8940A1.OutPut.Block
{
    public class Block_Basic : Block
    {
        public override int Block_In()
        {
           
            if (-1 == CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Block, 0))
                return -1;
            return 0;
        }

        public override int Block_Out()
        {
           
            if (-1 == CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Block, 1))
                return -1;
            return 0;
        }
    }
}
