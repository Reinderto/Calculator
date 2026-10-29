using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace calculator
{
    internal class Calculation
    {
        public string Text { get; set; }
        public List<double> Numbers { get; set; }

        public Calculation(string text)
        {
            text = Regex.Replace(text, @"(?<=(\d|\)))-(?=\d)", "+-");
            const string numRegex = @"-?\d+([\.,]\d+)?";
            List<Match> matches = Regex.Matches(text, numRegex).ToList();
            Numbers = new List<double>(matches.Count);
            string repacePattern = @"^(.*)";
            foreach (Match match in matches)
            {
                Numbers.Add(double.Parse(match.Value));
                repacePattern += match.Value + "(.*)";
                //text = Helper.ReplaceRegexOnce(text, numRegex, (Numbers.Count - 1).ToString());
            }
            repacePattern += "$";
            Match textMatch = Regex.Match(text, repacePattern);
            this.Text = textMatch.Groups[1].Value;
            for(int i = 2; i < textMatch.Groups.Count; i++)
            {
                this.Text += i - 2 + textMatch.Groups[i].Value;
            }
        }

        /*public static string ConvertToSimpleNumbersCalculation(string calculation, out List<double> oldValues)
        {
            //calculation = Regex.Replace(calculation, @"(?<=(\d|\)))-(?=\d)", "+-");
            const string numRegex = @"-?\d+([\.,]\d+)?";
            List<Match> matches = Regex.Matches(calculation, numRegex).ToList();
            oldValues = new List<double>(matches.Count);
            string repacePattern = @"^(.*)";
            foreach (Match match in matches)
            {
                oldValues.Add(double.Parse(match.Value));
                repacePattern += match.Value + "(.*)";
                //text = Helper.ReplaceRegexOnce(text, numRegex, (Numbers.Count - 1).ToString());
            }
            repacePattern += "$";
            Match textMatch = Regex.Match(text, repacePattern);
            this.Text = textMatch.Groups[1].Value;
            for (int i = 2; i < textMatch.Groups.Count; i++)
            {
                this.Text += i - 2 + textMatch.Groups[i].Value;
            }
        }*/

        public Calculation(string calculation, List<double> numbers)
        {
            Text = calculation;
            Numbers = numbers;
        }

        public bool TryOperator(string operatorPattern, out double[] values, out string expression)
        {
            Match match = MatchOperatorPattern(operatorPattern);
            if (!match.Success)
            {
                values = new double[0];
                expression = "";
                return false;
            }
            expression = match.Value;
            int[] indexes = OperationNumberIndexes(match);
            values = Array.ConvertAll(indexes, i => Numbers[i]);
            return true;
        }

        private Match MatchOperatorPattern(string operatorPattern)
        {
            operatorPattern = ReformatOperator(operatorPattern);
            return Regex.Match(Text, operatorPattern);
        }

        private static int[] OperationNumberIndexes(Match operatorMatch)
        {
            if (!operatorMatch.Success)
            {
                throw new ArgumentException("failed match argument");
            }
            List<int> indexes = new List<int>();
            if (operatorMatch.Groups.ContainsKey("Numbers"))
            {
                foreach (Capture cap in operatorMatch.Groups["Numbers"].Captures)
                {
                    indexes.Add(int.Parse(cap.Value));
                }
            }
            else
            {
                for (int i = 1; i <= operatorMatch.Groups.Count - 1; i++)
                {
                    Group group = operatorMatch.Groups[i];
                    if (group.Success)
                    {
                        indexes.Add(int.Parse(operatorMatch.Groups[i].Value));
                    }
                }
            }
            return indexes.ToArray();
            /*int indexesCount = operatorMatch.Groups.Count - 1;
            int[] indexes = new int[indexesCount];
            for(int i = 1; i <= indexesCount; i++)
            {
                indexes[i - 1] = int.Parse(operatorMatch.Groups[i].Value);
            }
            return indexes;*/
        }

        public static string ReformatOperator(string operatorPattern)
        {
            return operatorPattern.Replace(@"\d", @"(\d+)").Replace(" ", " *");
        }

        public void ReplaceOperation(string operatorPattern, double result)
        {
            Match match = MatchOperatorPattern(operatorPattern);
            int[] indexes = OperationNumberIndexes(match);
            int firstIndex;
            if (indexes.Length > 0)
            {
                firstIndex = indexes[0];
                Numbers[firstIndex] = result;
            }
            else
            {
                firstIndex = Numbers.Count;
                Numbers.Add(result);
            }
            string replacement = firstIndex.ToString();
            if(Regex.IsMatch(Text, @"\d *" + match.Value))//num detection
            {
                replacement = "+" + replacement;
            }
            if (Regex.IsMatch(Text,  match.Value + @" *\d"))
            {
                replacement += "+";
            }
            //if(Regex.IsMatch(Text, ))
            Text = Helper.ReplaceOnce(Text, match.Value, replacement);
        }

        public override string ToString()
        {
            string output = Text;
            for(int i = Numbers.Count - 1; i >= 0; i--)
            {
                output = Helper.ReplaceOnce(output, i.ToString(), Numbers[i].ToString());
            }
            return output;
        }
    }
}
