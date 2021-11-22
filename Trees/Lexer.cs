using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Trees
{
    public class Lexer
    {
        public Queue<Token> Tokens { get; }
        
        public Lexer(string expression)
        {
            var tokens = new Queue<Token>();
            for (var i = 0; i < expression.Length;)
            {
                var ch = expression[i];
                if (char.IsLetterOrDigit(ch))
                {
                    var stringTempNum = new StringBuilder(string.Empty);
                    while (i<expression.Length && char.IsLetterOrDigit(expression[i]))
                    {
                        stringTempNum.Append(expression[i]);
                        i++;
                    }

                    tokens.Enqueue(new Token(Helper.RecognizeToken(stringTempNum.ToString(), false)));
                }
                else
                {
                    tokens.Enqueue(new Token(Helper.RecognizeToken(expression[i].ToString(), true)));
                    i++;
                }
            }

            Tokens = tokens;
        }
    }
}