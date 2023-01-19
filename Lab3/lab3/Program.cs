using System;
using System.ComponentModel.Design;

namespace lab3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var okay = true;
            while (okay)
            {
                defaultMenu();
                Console.Write("Option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        var tad = new TAD(@"C:\Users\Ionut\source\repos\lftc\lab3\lab3\TextFile1.txt");
                        runTad(tad);
                        break;
                    case "2":
                        var tadIdent = new TAD(@"C:\Users\Ionut\source\repos\lftc\lab3\lab3\identificatori.txt");
                        runTad(tadIdent);
                        break;
                    case "3":
                        var tadConst = new TAD(@"C:\Users\Ionut\source\repos\lftc\lab3\lab3\constante.txt");
                        runTad(tadConst);
                        break;
                    case "x":
                        okay = false;
                        break;
                }
            }
        }

        private static void runTad(TAD tad)
        {
            tad.readFile();
            var run = true;

            while (run)
            {
                menuTad();
                Console.Write("Option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        tad.displayStates();
                        break;
                    case "2":
                        tad.displayAlphabet();
                        break;
                    case "3":
                        Console.WriteLine($"Stare initiala: {tad.initialState}");
                        break;
                    case "4":
                        tad.displayFinalStates();
                        break;
                    case "5":
                        tad.displayTransitions();
                        break;
                    case "6":
                        Console.Write("Secventa: ");
                        string sequence = Console.ReadLine();
                        if (tad.verifySequence(sequence))
                            Console.WriteLine("Okay");
                        else
                            Console.WriteLine("Not Okay");
                        break;
                    case "x":
                        run = false;
                        break;

                }
            }
        }
        private static void menuTad() 
        {
            Console.WriteLine("--- Menu ---\n\n1. Stari\n2. Alfabet\n3. Stare initiala\n4. Stari finale\n5. Tranzitii\n6. Verifica secventa\nx. Exit\n\n");
        }

        private static void defaultMenu()
        {
            Console.WriteLine("--- Menu ---\n\n1. Fisier Default\n2. Fisier Identificatori\n3. Fisier Constante\nx.Exit");
        }
    }
}
