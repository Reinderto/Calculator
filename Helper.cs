using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace calculator
{
    internal class Helper
    {
        public static string ReplaceOnce(string old, string pattern, string replacement)
        {
            Regex regexPattern = new Regex(Regex.Escape(pattern));
            return regexPattern.Replace(old, replacement, 1);
        }

        public static string ReplaceRegexOnce(string old, string pattern, string replacement)
        {
            Regex regexPattern = new Regex(pattern);
            return regexPattern.Replace(old, replacement, 1);
        }
    }
}
