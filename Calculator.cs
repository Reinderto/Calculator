using calculator.Operators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace calculator
{
    internal class Calculator
    {
        public static List<IOperation> Operators = new List<IOperation>();

        public static string Calculate(string calculation)
        {
            return Calculate(new Calculation(calculation));
        }

        public static string Calculate(Calculation calculation)
        {
            Debug.WriteLine("----------------------------------------------------------------------");
            Debug.WriteLine($"calc: {calculation}  text: {calculation.Text}  numbers: {string.Join(", ", calculation.Numbers)}");
            foreach (IOperation op in Operators)
            {
                while (calculation.TryOperator(op.Pattern, out double[] values, out string expression))
                {
                    double result = op.Operate(values, expression);
                    calculation.ReplaceOperation(op.Pattern, result);
                    Debug.WriteLine($"pattern: {op.Pattern}  result: {result}  calc: {calculation}  text: {calculation.Text}  numbers: {string.Join(", ", calculation.Numbers)}");
                }
            }
            return calculation.ToString();
        }
    }
}
