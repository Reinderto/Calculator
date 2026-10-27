using System;
using System.Collections.Generic;
using System.Text;

namespace calculator.Operators
{
    internal class ValueStack : SingleOperation
    {
        public override string Pattern => @"\d stack";

        public override double Operate(double[] values)
        {
            return values[0] * 64;
        }
    }
}
