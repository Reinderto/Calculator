using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace calculator.Operators
{
    internal class MultipleOperation : IOperation
    {
        private IOperation[] operations;

        public MultipleOperation(params IOperation[] operations)
        {
            this.operations = operations;
        }

        public string Pattern => $"(?:{string.Join("|", operations.Select(op => op.Pattern))})";

        public double Operate(double[] values, string expression)
        {
            foreach(IOperation op in operations)
            {
                if(Regex.IsMatch(expression, Calculation.ReformatOperator(op.Pattern)))
                {
                    return op.Operate(values, expression);
                }
            }
            throw new ArgumentException($"expression: \"{expression}\" not valid for any operation pattern in MultipleOperation");
        }
    }
}
