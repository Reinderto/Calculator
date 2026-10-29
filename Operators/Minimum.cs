using System;
using System.Collections.Generic;
using System.Text;

namespace calculator.Operators
{
    internal class Minimum : FunctionOperation
    {
        protected override string FunctionName => "min";

        protected override double Operate(double[] values)
        {
            return values.Min();
        }
    }
}
