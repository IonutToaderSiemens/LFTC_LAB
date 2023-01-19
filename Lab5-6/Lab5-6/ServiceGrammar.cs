using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Lab5_6
{
    public class ServiceGrammar
    {
        private string _path;
        private List<string> _nonTerminals;
        private List<string> _terminals;
        private Dictionary<string, List<string>> _productions;
        public ServiceGrammar(string path)
        {
            this._path = path;
            this._nonTerminals = new List<string>();
            this._terminals = new List<string>();
            this._productions = new Dictionary<string, List<string>>();
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
                    default:
                        var prod = line.Split("=");
                        var key = prod[0];                                          // S
                        var newKey = ((char)(char.Parse(key) - 10)).ToString();     // S'
                        var prods = prod[1].Split("|");
                        var left = false;
                        this._productions[key] = new List<string>();
                        foreach (var pr in prods)
                        {
                            if (this._nonTerminals.Contains(pr[0].ToString()) && pr[0].ToString() == key)
                            {
                                left = true;
                                for (var i = 0; i < this._productions[key].Count; i++)
                                {
                                    this._productions[key][i] = this._productions[key][i] + newKey;
                                }
                                if (!this._productions.ContainsKey(newKey))
                                {
                                    this._productions[newKey] = new List<string>();
                                }
                                this._productions[newKey].Add(pr.Substring(1) + newKey);
                                this._productions[newKey].Add("epsilon");

                            }
                            else
                            {
                                if (left == false)
                                {
                                    this._productions[key].Add(pr);
                                }
                                else
                                {
                                    this._productions[key].Add(pr + newKey);
                                }
                            }
                        }
                        break;

                }
            }
        }

        private void _displayConfiguration(string state, int index, Stack<string> alpha, string beta)
        {
            Console.Write($"({state}, {index + 1}, ");
            _displayStack(alpha, true);
            Console.Write(", ");
            Console.Write(beta);
            Console.Write(")\n");
        }

        private void _displayStack(Stack<string> stack, bool reverse)
        {
            var length = stack.Count;
            var array = stack.ToArray();
            switch (reverse)
            {
                case false:
                    for (var i = 0; i < length; i++)
                    {
                        Console.Write($"{array[i]}");
                    }
                    break;
                case true:
                    for (var i = length - 1; i >= 0; i--)
                    {
                        Console.Write($"{array[i]}");
                    }
                    break;
            }
        }

        public void Analyzer(string w)
        {
            Stack<string> alpha = new Stack<string>();
            string beta = this._productions.Keys.First();
            string state = "q";
            var index = 0;
            var n = w.Length;
            while (state != "t" && state != "e")
            {
                _displayConfiguration(state, index, alpha, beta);
                if (state == "q")
                {
                    if (beta == "" && index == n)
                    {
                        state = "t";
                        _displayConfiguration(state, index, alpha, beta);
                        _buildProductionLine(alpha);
                    }
                    else
                    {
                        var top = beta[0].ToString();
                        if (this._nonTerminals.Contains(top))
                        {
                            alpha.Push($"{top}1");
                            beta = beta.Substring(1);
                            beta = this._productions[top][0] + beta;
                        }
                        else
                        {
                            if (index < n)
                            {
                                if (top == w[index].ToString())
                                {
                                    index += 1;
                                    alpha.Push(top);
                                    beta = beta.Substring(1);
                                }
                                else
                                {
                                    state = "r";
                                }
                            }
                            else
                            {
                                state = "r";
                            }
                        }
                    }
                }
                else
                {
                    if (state == "r")
                    {
                        if (index == 0 && beta == "S")
                        {
                            state = "e";
                            Console.WriteLine("Secventa neacceptata");
                        }
                        else
                        {
                            var alphaPeek = alpha.Peek().ToString();
                            if (this._terminals.Contains(alphaPeek.ToString()))
                            {
                                index -= 1;
                                alpha.Pop();
                                beta = alphaPeek + beta;
                            }
                            else
                            {
                                var term = alphaPeek[0].ToString();
                                var j = int.Parse(alphaPeek[1].ToString());
                                var prod = this._productions[term][j - 1];
                                if (beta.Substring(0, prod.Length) == prod && j < this._productions[term].Count)
                                {
                                    state = "q";
                                    alpha.Pop();
                                    alpha.Push($"{term}{j + 1}");
                                    beta = beta.Remove(0, prod.Length);
                                    beta = this._productions[term][j] + beta;
                                }
                                else
                                {
                                    if (index == 0 && beta == "S")
                                    {
                                        state = "e";
                                    }
                                    else
                                    {
                                        alpha.Pop();
                                        beta = beta.Substring(1);
                                        beta = alphaPeek[0] + beta;
                                    }
                                }
                            }
                        }

                    }
                    else
                    {
                        if (state == "e")
                        {
                            Console.WriteLine("eroare");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("acceptat");
                            _buildProductionLine(alpha);
                        }
                    }
                }
            }
        }

        private void _buildProductionLine(Stack<string> alpha)
        {
            string productionLine = "";
            while (alpha.Count > 0)
            {
                if (!this._terminals.Contains(alpha.Peek()))
                {
                    productionLine = alpha.Peek().Substring(1) + productionLine;
                }
                alpha.Pop();
            }
            Console.Write($"\n\n--- Sir Productii ---\n{productionLine}\n\n");
        }

        public void DisplayInfo()
        {
            Console.Write("--- Non Terminale ---\n");
            foreach(var nonTerm in this._nonTerminals)
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
            foreach(var key in this._productions.Keys)
            {
                Console.Write($"{key} -> ");
                foreach(var production in this._productions[key])
                {
                    Console.Write($"{production} | ");
                }
                Console.Write("\n\n");
            }
        }

    }
}
