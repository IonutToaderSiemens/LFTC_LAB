using System;

namespace Lab4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceGrammar service = new ServiceGrammar(@"D:\LFTC_LAB\Lab4\Lab4\Gramatica.txt");
            try
            {
                service.ReadFile();
                service.DisplayInfo();
                service.ConvertGRToFA();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
