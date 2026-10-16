using calculator.Operators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace calculator
{
    internal class Calculator
    {
        public static List<IOperation> Operators = new List<IOperation>();

        public static string calculate(string calculation)
        {
            Calculation calc = new Calculation(calculation);
            Debug.WriteLine($"calc: {calc}  text: {calc.Text}  numbers: {string.Join(", ", calc.Numbers)}");
            foreach (IOperation op in Operators)
            {
                while (calc.TryOperator(op.Pattern, out double[] values))
                {
                    double result = op.Operate(values);
                    calc.ReplaceOperation(op.Pattern, result);
                    Debug.WriteLine($"pattern: {op.Pattern}  result: {result}  calc: {calc}");
                }
            }
            return calc.ToString();
        }
    }
}
