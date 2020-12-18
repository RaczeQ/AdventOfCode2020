using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day18
{
    public class CustomPostfixCalculator
    {
        // Edited code from: https://dotnetfiddle.net/1gua3a
        public static List<string> InfixToPostfix(string[] elements, bool inverted)
        {
            var result = new List<string>();

            // initializing empty stack
            Stack<string> stack = new Stack<string>();

            for (int i = 0; i < elements.Length; ++i)
            {
                string c = elements[i];

                // If the scanned character is an operand, add it to output.
                if (int.TryParse(c, out int a))
                    result.Add(c);

                // If the scanned character is an '(', push it to the stack.
                else if (c == "(")
                    stack.Push(c);

                //  If the scanned character is an ')', pop and output from the stack 
                // until an '(' is encountered.
                else if (c == ")")
                {
                    while (stack.Count != 0 && stack.Peek() != "(")
                        result.Add(stack.Pop());

                    if (stack.Count != 0 && stack.Peek() != "(")
                        throw new ArgumentException("Invalid expression"); // invalid expression                
                    else
                        stack.Pop();
                }
                else // an operator is encountered
                {
                    while (stack.Count != 0 && Prec(c, inverted) <= Prec(stack.Peek(), inverted))
                        result.Add(stack.Pop());
                    stack.Push(c);
                }
            }

            // pop all the operators from the stack
            while (stack.Count != 0)
                result.Add(stack.Pop());

            return result;
        }

        public static int Prec(string element, bool inverted)
        {
            return inverted ? InvertedPrec(element) : EqualPrec(element);
        }

        public static int EqualPrec(string element)
        {
            return "*+/-".Contains(element) ? 1 : 0;
        }

        public static int InvertedPrec(string element)
        {
            switch (element)
            {
                case "+":
                case "-":
                    return 2;

                case "*":
                case "/":
                    return 1;
            }
            return 0;
        }

        // Copied code from: https://stackoverflow.com/a/10031887/7766101
        public static long Calculate(List<string> elements)
        {
            Stack<long> eval = new Stack<long>();
            foreach (var element in elements)
            {
                if ("*+/-".Contains(element))
                {
                    long temp1;
                    long temp2;

                    switch (element)
                    {
                        case "*":
                            eval.Push(eval.Pop() * eval.Pop());
                            break;
                        case "-":
                            temp1 = eval.Pop();
                            temp2 = eval.Pop();
                            eval.Push(temp2 - temp1);
                            break;
                        case "+":
                            eval.Push(eval.Pop() + eval.Pop());
                            break;
                        case "/":
                            temp1 = eval.Pop();
                            temp2 = eval.Pop();
                            eval.Push(temp2 / temp1);
                            break;
                    }

                }
                else
                    eval.Push(Convert.ToInt32(element));
            }

            return eval.Pop();
        }
    }
}
