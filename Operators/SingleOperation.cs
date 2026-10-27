using System;
using System.Collections.Generic;
using System.Text;

namespace calculator.Operators
{
    abstract class SingleOperation : IOperation
    {
        public abstract string Pattern { get; }

        public double Operate(double[] values, string expression)
        {
            return Operate(values);
        }

        public abstract double Operate(double[] values);
    }
}
