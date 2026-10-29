using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace calculator.Operators
{
    internal abstract class FunctionOperation : IOperation
    {
        protected abstract string FunctionName { get; }
        public string Pattern => FunctionName + @"(?<open>\()(?(open)(?<Numbers>\d)*[^\(\)]?(?<open>\()*(?<close-open>\))*)+(?(open)(?!))";

        public double Operate(double[] values, string expression)
        {
            expression = RebaseExpression(expression);
            Calculation calculation = new Calculation(expression, values.ToList());
            string result = Calculator.Calculate(calculation);
            string[] strNumbers = result.Split(',');
            double[] newValues = new double[strNumbers.Length];
            for(int i = 0; i < strNumbers.Length; i++)
            {
                if (double.TryParse(strNumbers[i], out double res))
                {
                    newValues[i] = res;
                }
                else
                {
                    return double.NaN;
                }
            }
            return Operate(newValues);
        }

        protected abstract double Operate(double[] values);

        private string RebaseExpression(string expression)
        {
            MatchCollection matches = Regex.Matches(expression, @"[^\d]+");
            //if(matches.Count == 0) {
            string output = matches[0].Value;
            for (int i = 1; i < matches.Count; i++)
            {
                output += (i - 1) + matches[i].Value;
            }
            return output.Substring(FunctionName.Length + 1, output.Length - 2 - FunctionName.Length);
        }
    }
}
