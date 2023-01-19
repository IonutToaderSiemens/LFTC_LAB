using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;

namespace Lab5_6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceGrammar service = new ServiceGrammar(@"C:\Users\z004et0s\source\repos\Lab5-6\Lab5-6\File.txt");
            service.ReadFile();
            service.DisplayInfo();
            service.Analyzer("aacbc");
        }
    }
}
