namespace Trees
{
    public class Node
    {
        public Token Token { get; private set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        
        public Node(Token token, Node left = null, Node right = null)
        {
            Token = token;
            Left = left;
            Right = right;
        }
        
        public Node(int value, Node left = null, Node right = null)
        {
            Token = new Token(value);
            Left = left;
            Right = right;
        }
    }
}