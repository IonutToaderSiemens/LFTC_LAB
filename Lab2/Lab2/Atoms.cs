using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lab2
{
    public class Atoms
    {
        static public List<string> GetAllAtoms()
        {
            return new List<string>() {"identificator", "constanta", "STOP", "STRING", "ARRAY", "CHAR", "CONST", "DO", "ELSE", "IF", "INTEGER", "BEGIN", "END", "OF", "PROGRAM", "READ", "THEN", "VAR", "WHILE", "WRITE", "START", "BOOLEAN", "REAL", "+", "-", "*", "/", "=", ">", ">=", "<", "<=", "%", "!=", "(", ")", "[", "]", " ", ":", ";" };
        }
        static public List<string> GetKeyWords()
        {
            return new List<string>() { "STOP", "STRING", "ARRAY", "CHAR", "CONST", "DO", "ELSE", "IF", "INTEGER", "OF", "PROGRAM", "READ", "THEN", "VAR", "WHILE", "WRITE", "START", "BOOLEAN", "REAL" };
        }

        static public List<string> GetDelimiers()
        {
            return new List<string>() { "+", "-", "*", "/", "=", ">", ">=", "<", "<=", "%", "!=", "(", ")", "[", "]", " ", ":" };
        }
    }
}
