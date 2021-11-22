using System;
using System.Collections.Generic;
using System.Linq;

namespace Trees
{
    public class Parser
    {
        public List<Token> InfixForm { get; private set; }
        public List<Token> PrefixForm { get; private set; }
        public List<Token> PostfixForm { get; private set; }

        public Parser(string expression)
        {
            PostfixForm = new List<Token>();
            
            var tokens = new Lexer(expression).Tokens;
            InfixForm = new List<Token>();
            InfixForm.AddRange(tokens);

            Stack<Token> stack = new Stack<Token>();

            foreach (var el in tokens.ToList())
            {
                if (!tokens.Peek().IsOperator)
                {
                    PostfixForm.Add(tokens.Dequeue());
                }
                else if (tokens.Peek().Type is Types.OpenBracket)
                {
                    stack.Push(tokens.Dequeue());
                }
                else if (tokens.Peek().Type is Types.CloseBracket)
                {
                    while (stack.Count > 0 &&
                           !(stack.Peek().Type is Types.OpenBracket))
                    {
                        PostfixForm.Add(stack.Pop());
                    }
 
                    if (stack.Count > 0 && !(stack.Peek().Type is Types.OpenBracket))
                    {
                        throw new ArgumentException("Invalid expression");
                    }

                    stack.Pop();
                    tokens.Dequeue();
                }
                else
                {
                    while (stack.Count > 0 && tokens.Peek().Priority <=
                        Operations[stack.Peek().Type])
                    {
                        PostfixForm.Add(stack.Pop());
                    }
                    stack.Push(tokens.Dequeue());
                }
            }
            PostfixForm.AddRange(stack);
            
        }
        
        private static readonly Dictionary<Types, int> Operations =
            new()
            {
                {Types.Addition, 1},
                {Types.Minus, 1},
                {Types.Multiply, 2},
                {Types.Divide, 2},
                {Types.OpenBracket, -1},
                {Types.CloseBracket, 4},
                {Types.Power, 5}
            };

    }
}