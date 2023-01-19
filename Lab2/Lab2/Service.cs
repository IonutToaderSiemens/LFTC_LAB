using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;

namespace Lab2
{

    public class Service
    {
        public List<string[]> FIP = new List<string[]>();
        public List<string> TS = new List<string>();
        public List<string> Errors = new List<string>();

        public void readFile(string path)
        {
            var keyWords = Atoms.GetKeyWords();
            var atoms = Atoms.GetAllAtoms();
            int index = 0;
            foreach (string line in File.ReadLines(path))
            {
                string[] tokens = StringTokenizer.Tokenize(line);

                foreach(var token in tokens)
                {
                    if (atoms.Contains(token))
                    {
                        FIP.Add(new string[] { token, atoms.IndexOf(token).ToString(), TS.IndexOf(token).ToString() });
                    }
                    else
                    {
                        if (isConstant(token) || isVariable(token))
                        {

                            if (!TS.Contains(token))
                            {
                                TS.Add(token);
                            }
                            FIP.Add(new string[] { token, (isConstant(token) == true) ? "1" : "0", TS.IndexOf(token).ToString() });
                        }
                        else
                        {
                            Errors.Add($"Error - line: {index}");
                        }
                    }
                }
                index++;
            }
        }

        public bool isVariable(string str)
        {
            return Regex.IsMatch(str, @"^[a-zA-Z][0-9A-Za-z]");
        }

        public bool isConstant(string str)
        {
            if (Regex.IsMatch(str, @"-?0[0-9]"))
                return false;

            if (!double.TryParse(str, out _))
                return false;

            return true;
        }

        public void displayTS()
        {
            Console.WriteLine(string.Format("{0, -10} {1, -10}", "Position", "Name"));

            for(var i = 0; i < TS.Count; i++)
            {
                Console.WriteLine(string.Format("{0, -10} {1, -10}", i, TS[i]));
            }
        }
        
        public void displayFIP()
        {
            Console.WriteLine(string.Format("\n\n{0, -15} {1, -15} {2, -15}", "Atom code", "TS Position", "Atom"));
            var atoms = Atoms.GetAllAtoms();
            foreach (var list in FIP)
            {
                Console.WriteLine(string.Format("{0, -15} {1, -15} {2, -15}", list[1], list[2], list[0]));
            }
        }
    }
}
