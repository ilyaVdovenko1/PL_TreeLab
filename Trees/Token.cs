namespace Trees
{
    public class Token
    {
        public string Value { get; }
        public Types Type { get; }
        public int Priority { get; set; }
        public bool IsOperator { get; }
        
        public int Num { get; }
        
        public Token((string value,Types type, int priotity, bool isOperator) token)
        {
            Value = token.value;
            Type = token.type;
            Priority = token.priotity;
            IsOperator = token.isOperator;
        }

        public Token(int value)
        {
            Num = value;
        }
    }
}