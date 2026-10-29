using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace calculator.Operators
{
    internal class Brackets : IOperation
    {
        public string Pattern => @"(?<open>\()(?(open)(?<Numbers>\d)*[^\(\)]?(?<open>\()*(?<close-open>\))*)+(?(open)(?!))";

        public double Operate(double[] values, string expression)
        {
            expression = RebaseExpression(expression);
            Calculation calculation = new Calculation(expression, values.ToList());
            string result = Calculator.Calculate(calculation);
            if(double.TryParse(result, out double res))
            {
                return res;
            }
            else
            {
                return double.NaN;
            }
        }

        private string RebaseExpression(string expression)
        {
            MatchCollection matches = Regex.Matches(expression, @"[^\d]+");
            //if(matches.Count == 0) {
            string output = matches[0].Value;
            for(int i = 1; i < matches.Count; i++)
            {
                output += (i - 1) + matches[i].Value;
            }
            return output.Substring(1, output.Length - 2);
        }
    }
}
