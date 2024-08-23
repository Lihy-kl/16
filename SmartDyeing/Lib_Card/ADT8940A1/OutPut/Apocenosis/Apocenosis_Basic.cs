using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Card.ADT8940A1.OutPut.Apocenosis
{
    public class Apocenosis_Basic : Apocenosis
    {
        public override int Apocenosis_Off()
        {
           
            if (-1 == CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Apocenosis, 0))
                return -1;
            return 0;
        }

        public override int Apocenosis_On()
        {
           
            if (-1 == CardObject.OA1.WriteOutPut(ADT8940A1_IO.OutPut_Apocenosis, 1))
                return -1;
            return 0;
        }
    }
}
