using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Lab4
{
    public class ServiceGrammar
    {
        private string _path;
        private List<string> _nonTerminals;
        private List<string> _terminals;
        private Dictionary<string, List<string>> _productions;
        private string _startSymbol;
        public ServiceGrammar(string path)
        {
            this._path = path;
            this._nonTerminals = new List<string>();
            this._terminals = new List<string>();
            this._productions = new Dictionary<string, List<string>>();
            this._startSymbol = "";
        }

        public void ReadFile()
        {
            var counter = 0;
            foreach (string line in File.ReadLines(this._path))
            {
                switch (counter)
                {
                    case 0:
                        var nonTerminals = line.Split(",");
                        foreach (var nonTerminal in nonTerminals)
                        {
                            this._nonTerminals.Add(nonTerminal);
                        }
                        counter++;
                        break;
                    case 1:
                        var terminals = line.Split(",");
                        foreach (var terminal in terminals)
                        {
                            this._terminals.Add(terminal);
                        }
                        counter++;
                        break;
                    case 2:
                        this._startSymbol = line[0].ToString();
                        counter++;
                        break;
                    default:
                        var prod = line.Split("=");
                        var key = prod[0];
                        if (!this._nonTerminals.Contains(key))
                        {
                            throw new Exception("Error - Gramatica nu este regulara");
                        }
                        var prods = prod[1].Split("|");
                        this._productions[key] = new List<string>();
                        foreach (var pr in prods)
                        {
                            if (this._nonTerminals.Contains(pr[0].ToString()) || pr.Length > 2)
                            {
                                throw new Exception("Gramatica nu este regulara");
                            }
                            else
                            {
                                this._productions[key].Add(pr);
                            }
                        }
                        break;

                }
            }
        }

        public void ConvertGRToFA()
        {
            List<string> states = this._nonTerminals;
            List<string> alphabet = this._terminals;
            List<string> finalStates = new List<string>();
            Dictionary<string, List<string[]>> transitions = new Dictionary<string, List<string[]>>();
            string initialState = this._startSymbol;
            states.Add("K");
            finalStates.Add("K");
            if (this._productions[this._startSymbol].Contains("epsilon"))
            {
                finalStates.Add(initialState);
            }
            foreach(var nonTerm in this._productions.Keys)
            {
                foreach(var prod in this._productions[nonTerm])
                {
                    if (!transitions.ContainsKey(nonTerm))
                    {
                        transitions[nonTerm] = new List<string[]>();
                    }
                    if (prod.Length == 1)
                    {
                        transitions[nonTerm].Add(new string[] { prod[0].ToString(), "K"});
                    }
                    else
                    {
                        transitions[nonTerm].Add(new string[] { prod[0].ToString(), prod[1].ToString() });
                    }
                }
            }
            _displayInfoFA(transitions, states, alphabet, finalStates, initialState);
        }

        private void _displayInfoFA(Dictionary<string, List<string[]>> transitions, List<string> states, List<string> alphabet, List<string> finalStates, string initialState)
        {
            displayStates(states);
            displayAlphabet(alphabet);
            Console.Write($"--- Stare Initiala ---\n{initialState}\n\n");
            displayFinalStates(finalStates);
            displayTransitions(transitions);
        }

        private void displayStates(List<string> states)
        {
            Console.Write($"--- Stari ---\n");
            foreach (var state in states)
            {
                Console.Write($"{state} ");
            }
            Console.WriteLine("\n\n");
        }

        private void displayAlphabet(List<string> alphabet)
        {
            Console.Write("--- Alfabet ---\n");
            foreach (var a in alphabet)
            {
                Console.Write($"{a} ");
            }
            Console.WriteLine("\n\n");
        }

        private void displayFinalStates(List<string> finalStates)
        {
            Console.Write($"--- Stari finale ---\n");
            foreach (var state in finalStates)
            {
                Console.Write($"{state} ");
            }
            Console.WriteLine("\n\n");
        }

        private void displayTransitions(Dictionary<string, List<string[]>> transitions)
        {
            Console.Write("--- Tranzatii ---\n");
            foreach (KeyValuePair<string, List<string[]>> elem in transitions)
            {
                foreach (var e in elem.Value)
                {
                    Console.WriteLine($"d ( {elem.Key}, {e[0]} ) = {e[1]}");
                }
            }
        }

        public void DisplayInfo()
        {
            Console.Write("--- Non Terminale ---\n");
            foreach (var nonTerm in this._nonTerminals)
            {
                Console.Write($"{nonTerm} ");
            }
            Console.Write("\n\n\n");
            Console.Write("--- Terminale ---\n");
            foreach (var term in this._terminals)
            {
                Console.Write($"{term} ");
            }
            Console.Write("\n\n\n");
            Console.Write("--- Productii ---\n");
            foreach (var key in this._productions.Keys)
            {
                Console.Write($"{key} -> ");
                foreach (var production in this._productions[key])
                {
                    Console.Write($"{production} | ");
                }
                Console.Write("\n\n");
            }
        }

    }
}
