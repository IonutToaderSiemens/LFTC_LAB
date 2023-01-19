using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Lab2
{
    public class StringTokenizer
    {
        public static string[] Tokenize(string str)
        {

            string separators = @"(\s)|(\+)|(\-)|(\*)|(\%)|(\/)|(\()|(\))|(\:)|(\;)|(\==)|(\=)|(\<=)|(\>=)|(\>)|(\>)";
            return Regex.Split(str, separators);
        }

    }
}
