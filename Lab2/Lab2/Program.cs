using System;
using System.Text.RegularExpressions;

namespace Lab2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Service service = new Service();
            service.readFile(@"C:\Users\Ionut\source\repos\lftc\Lab2\Lab2\Atoms.txt");
            service.displayTS();
            service.displayFIP();
        }
    }
}
