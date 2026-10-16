using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace calculator
{
    internal class Calculation
    {
        public string Text { get; set; }
        public List<double> Numbers { get; set; }

        public Calculation(string text)
        {
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

        public bool TryOperator(string operatorPattern, out double[] values)
        {
            Match match = MatchOperatorPattern(operatorPattern);
            if (!match.Success)
            {
                values = new double[0];
                return false;
            }
            int[] indexes = OperationNumberIndexes(match);
            values = Array.ConvertAll(indexes, i => Numbers[i]);
            return true;
        }

        private Match MatchOperatorPattern(string operatorPattern)
        {
            operatorPattern = ReformatOperator(operatorPattern);
            return Regex.Match(Text, operatorPattern);
        }

        private int[] OperationNumberIndexes(Match operatorMatch)
        {
            if (!operatorMatch.Success)
            {
                throw new ArgumentException("failed match argument");
            }
            int indexesCount = operatorMatch.Groups.Count - 1;
            int[] indexes = new int[indexesCount];
            for(int i = 1; i <= indexesCount; i++)
            {
                indexes[i - 1] = int.Parse(operatorMatch.Groups[i].Value);
            }
            return indexes;
        }

        private static string ReformatOperator(string operatorPattern)
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
            Text = Helper.ReplaceOnce(Text, match.Value, firstIndex.ToString());
        }

        public override string ToString()
        {
            string output = Text;
            for(int i = Numbers.Count - 1; i >= 0; i--)
            {
                output = output.Replace(i.ToString(), Numbers[i].ToString());
            }
            return output;
        }
    }
}
