using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace calculator.Operators
{
    internal class Minus : SingleOperation
    {
        public override string Pattern => @"\d \- \d";

        public override double Operate(double[] values)
        {
            Debug.WriteLine($"values: {string.Join(", ", values)}");
            return values[0] - values[1];
        }
    }
}
