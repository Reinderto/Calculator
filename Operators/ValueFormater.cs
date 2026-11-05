using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

namespace calculator.Operators
{
    public class ValueFormater : IOperation
    {
        private readonly (string name, long size)[] Steps;
        public string Pattern => GetPattern();
        //(?:(?:\dstx )?\dle )?\deb \d
        //(?:(?<Numbers>\d)stx )?(?:(?<Numbers>\d)le )?(?:(?<Numbers>\d)eb )?(?(Numbers)(?<Numbers>\d)?|(?!))

        public ValueFormater(params (string name, long size)[] steps)
        {
            Steps = steps;
        }

        private string GetPattern()
        {
            if(Steps.Length <= 0)
            {
                return @"\d";
            }
            string pattern = "";
            foreach((string name, long size) step in Steps)
            {
                pattern += $@"(?:(?<Numbers>\d){step.name} )?";
            }
            pattern += $@"(?(Numbers)(?<Numbers>\d)?|(?!))";
            /*for (int i = 0; i < Steps.Length - 1; i++)
            {
                pattern = @$"(?:{pattern}\d{Steps[i].name} )?";
            }
            pattern += @$"\d{Steps.Last().name} \d";*/
            return pattern;
        }

        public double Operate(double[] values, string expression)
        {
            bool isNegative = values[0] < 0;
            if (isNegative)
            {
                values[0] *= -1;
            }
            double result = 0;
            int valuesIndex = 0;
            foreach ((string name, long size) step in Steps)
            {
                if(Regex.IsMatch(expression, $@"{Regex.Escape(Calculation.NUMBER_PLACEHOLDER_LEFT)}\d+{Regex.Escape(Calculation.NUMBER_PLACEHOLDER_RIGHT)}{step.name}(?: |{Regex.Escape(Calculation.NUMBER_PLACEHOLDER_LEFT)}|$)"))
                {
                    result += values[valuesIndex++];
                }
                result *= step.size;
            }
            if(valuesIndex < values.Length)
            {
                result += values.Last();
            }
            if (isNegative)
            {
                result = -result;
            }
            return result;
            /*double result = values[0];
            int stepsOffset = Steps.Length - (values.Length - 1);
            for (int i = 1; i < values.Length; i++)
            {
                result *= Steps[i + stepsOffset - 1].size;
                result += values[i];
            }
            return result;*/
        }

        public string FormatTo(double value)
        {
            bool isNegative = value < 0;
            if (isNegative)
            {
                value = -value;
            }
            string output = "";
            for(int i = Steps.Length - 1; i >= 0; i--)
            {
                double rest = value % Steps[i].size;
                if(rest != 0)
                {
                    if (i != Steps.Length - 1)
                    {
                        output = Steps[i + 1].name + " " + output;
                    }
                    output = rest + output;
                }
                value = Math.Floor(value / Steps[i].size);
            }
            if(value != 0)
            {
                if (Steps.Length >= 1)
                {
                    output = Steps[0].name + " " + output;
                }
                output = value + output;
            }
            if(output.Length == 0)
            {
                output = "0";
            }
            if (isNegative)
            {
                output = "-" + output;
            }
            return output;
        }
    }
}
