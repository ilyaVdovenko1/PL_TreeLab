using System;
using System.Collections.Generic;

namespace Trees
{
    public static class Helper
    {
        private static readonly Dictionary<string, (Types operation, int priority)> Operations =
            new Dictionary<string, (Types operation, int priority)>
            {
                {"+", (Types.Addition, 1)},
                {"-", (Types.Minus, 1)},
                {"*", (Types.Multiply, 2)},
                {"/", (Types.Divide, 2)},
                {"(", (Types.OpenBracket, -1)},
                {")", (Types.CloseBracket, 4)},
                {"^", (Types.Power, 3)}
            };
        public static (string value, Types type, int priotity, bool isOperator) RecognizeToken(string value, bool isOperator)
        {
            try
            {
                Types type;
                int priority;
                if (isOperator)
                {
                    type = Operations[value].operation;
                    priority = Operations[value].priority;
                }
                else
                {
                    type = Types.Number;
                    priority = -1;
                }
                return (value, type, priority, isOperator);
            }
            catch (KeyNotFoundException e)
            {
                Console.WriteLine(e);
                throw new ArgumentException($"Unexpected token {value}", value);
            }
            

            
        }
    }
}