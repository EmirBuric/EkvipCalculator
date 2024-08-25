using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Ekvip_Calculator
{
    internal class Calculator
    {
        string equation;
        public Calculator(string str) {
            equation = str;
        }
        //Method to parse the correctly inputed equation from a string using Datatable().Compute
        public float Result()
        {
            var result = new DataTable().Compute(equation, null);
            float res=Convert.ToSingle(result);
            return res;
        }
    }
}
