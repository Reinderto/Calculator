using System;
using System.Collections.Generic;
using System.Text;

namespace calculator.Operators
{
    internal class Max : FunctionOperation
    {
        protected override string FunctionName => "max";

        protected override double Operate(double[] values)
        {
            return values.Max();
        }
    }
}
