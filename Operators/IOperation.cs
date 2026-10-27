using System;
using System.Collections.Generic;
using System.Text;

namespace calculator.Operators
{
    internal interface IOperation
    {
        public string Pattern { get; }

        public double Operate(double[] values, string expression);
    }
}
