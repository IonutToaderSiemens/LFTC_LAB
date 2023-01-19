using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace lab3
{
    public class TAD
    {


        private List<string> alphabet = new List<string>();
        private List<string> states = new List<string>();
        private List<string> finalStates = new List<string>();
        private Dictionary<string, List<string[]>> transitions = new Dictionary<string, List<string[]>>();
        private string path;
        public string initialState;

        public TAD(string filePath)
        {
            this.alphabet = new List<string>();
            this.states = new List<string>();
            this.finalStates = new List<string>();
            this.transitions = new Dictionary<string, List<string[]>>();
            this.path = filePath;

        }
        

        public void readFile()
        {
            var index = 0;
            foreach (string line in File.ReadLines(this.path))
            {
                if (index < 4)
                {
                    switch (index)
                    {
                        case 0:
                            foreach (var str in line.Split(','))
                                states.Add(str);
                            break;
                        case 1:
                            foreach (var str in line.Split(','))
                                alphabet.Add(str);
                            break;
                        case 2:
                            initialState = line;
                            break;
                        case 3:
                            foreach (var str in line.Split(','))
                                finalStates.Add(str);
                            break;

                    }
                }
                else
                {
                    var tr = line.Split(',');
                    string[] arr = new string[]{ tr[1], tr[2]};

                    if (!transitions.ContainsKey(tr[0]))
                    {
                        transitions[tr[0]] = new List<string[]>();
                    }

                    transitions[tr[0]].Add(arr);
                }

                index++;
                        
            }
        }

        public void convertFAToGR()
        {
            List<string> nonTerminals = this.states;
            List<string> terminals = this.alphabet;
            Dictionary<string, List<string>> productions = new Dictionary<string, List<string>>();
            string startSymbol = this.initialState;

            foreach (KeyValuePair<string, List<string[]>> elem in this.transitions)
            {
                if (!productions.ContainsKey(elem.Key))
                {
                    productions[elem.Key] = new List<string>();
                }
                foreach (var e in elem.Value)
                {
                    if(elem.Key == e[1])
                    {
                        productions[elem.Key].Add(e[0]);
                    }
                    else
                    {
                        productions[elem.Key].Add(e[0] + e[1]);
                    }
                }
            }
            Console.Write("--- Non Terminale ---\n");
            foreach (var nonTerm in nonTerminals)
            {
                Console.Write($"{nonTerm} ");
            }
            Console.Write("\n\n\n");
            Console.Write("--- Terminale ---\n");
            foreach (var term in terminals)
            {
                Console.Write($"{term} ");
            }
            Console.Write("\n\n\n");
            Console.Write("--- Productii ---\n");
            foreach (var key in productions.Keys)
            {
                Console.Write($"{key} -> ");
                foreach (var production in productions[key])
                {
                    Console.Write($"{production} | ");
                }
                Console.Write("\n\n");
            }
        }
        public bool verifySequence(string sequence)
        {
            var state = initialState;
            foreach(var s in sequence)
            {
                if (!alphabet.Contains(s.ToString()))
                {
                    Console.WriteLine($"Secventa invalida!");
                    return false;
                }

                bool found = false;

                if (transitions.ContainsKey(state))
                {
                    foreach(var array in transitions[state])
                    {
                        if (array[0] == s.ToString())
                        {
                            Console.WriteLine($"d ( {state}, {s} ) = {array[1]}");
                            state = array[1];
                            found = true;
                            break;
                        }
                    }
                }
                if (found == false)
                {
                    Console.WriteLine($"Secventa invalida");
                    return false;
                }
            }

            if (finalStates.Contains(state))
                return true;
            return false;
        }

        public void result(string state, string s)
        {
            if (transitions.ContainsKey(state))
            {
                foreach (var array in transitions[state])
                {
                    if (array[0] == s.ToString())
                    {
                        Console.WriteLine($"d ( {state}, {s} ) = {array[1]}");
                    }
                }
            }
        }

        public void displayStates()
        {
            Console.Write($"\nStari: ");
            foreach(var state in states)
            {
                Console.Write($"{state} ");
            }
            Console.WriteLine("\n\n");
        }

        public void displayAlphabet()
        {
            Console.Write("\nAlfabet: ");
            foreach(var a in alphabet)
            {
                Console.Write($"{a} ");
            }
            Console.WriteLine("\n\n");
        }

        public void displayFinalStates()
        {
            Console.Write($"\nStari finale: ");
            foreach (var state in finalStates)
            {
                Console.Write($"{state} ");
            }
            Console.WriteLine("\n\n");
        }

        public void displayTransitions()
        {
            Console.Write("\nTranzatii: \n");
            foreach (KeyValuePair<string, List<string[]>> elem in transitions)
            {
                foreach (var e in elem.Value)
                {
                    Console.WriteLine($"d ( {elem.Key}, {e[0]} ) = {e[1]}");
                }
            }
        }

    }
}
