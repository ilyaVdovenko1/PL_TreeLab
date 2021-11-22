using System;
using System.Collections.Generic;
using System.Reflection;

namespace Trees
{
    public class BinaryTree
    {
        public Node RootOfTree { get; private set; }
        public List<Token> GetPreOrderValues { get; private set; }
        public List<Token> GetInOrderValues { get; private set; }
        public List<Token> GetPostOrderValues { get; private set; }

        public BinaryTree(List<Token> postfixForm)
        {
            GetPreOrderValues = new List<Token>();
            GetInOrderValues = new List<Token>();
            GetPostOrderValues = new List<Token>();
            var stack = new Stack<Node>();
            foreach (var token in postfixForm)
            {
                if (!token.IsOperator)
                {
                    stack.Push(new Node(token));
                }
                else
                {
                    var right = stack.Pop();
                    var left = stack.Pop();
                    stack.Push(new Node(token, left, right));
                }
            }

            RootOfTree = stack.Peek();
        }
        
        public BinaryTree(List<int> keys)
        {
            GetPreOrderValues = new List<Token>();
            GetInOrderValues = new List<Token>();
            GetPostOrderValues = new List<Token>();
            RootOfTree = new Node(keys[0]);
            keys.RemoveAt(0);
            foreach (var token in keys)
            {
                InsertElement(RootOfTree,token);
            }

           
        }

        private void InsertElement(Node node,int key)
        {
            if (node.Token.Num <= key)
            {
                if (node.Right is null)
                {
                    node.Right = new Node(key);

                }
                else
                {
                    InsertElement(node.Right, key);
                }
            }
            else
            {
                if (node.Left is null)
                {
                    node.Left = new Node(key);

                }
                else
                {
                    InsertElement(node.Left, key);
                }
            }
        }

        public void Order(Node node)
        {
            if (!(node is null))
            {
                GetPreOrderValues.Add(node.Token);
                Order(node.Left);
                GetInOrderValues.Add(node.Token);
                Order(node.Right);
                GetPostOrderValues.Add(node.Token);
            }
        }

    public static void Print(Node root, int topMargin = 2, int leftMargin = 2, bool isBst = false)
    {
        if (root == null) return;
        int rootTop = Console.CursorTop + topMargin;
        var last = new List<NodeInfo>();
        var next = root;
        for (int level = 0; next != null; level++)
        {
            var item = new NodeInfo { Node = next, Text = isBst ? next.Token.Num.ToString() : next.Token.Value };
            if (level < last.Count)
            {
                item.StartPos = last[level].EndPos + 1;
                last[level] = item;
            }
            else
            {
                item.StartPos = leftMargin;
                last.Add(item);
            }
            if (level > 0)
            {
                item.Parent = last[level - 1];
                if (next == item.Parent.Node.Left)
                {
                    item.Parent.Left = item;
                    item.EndPos = Math.Max(item.EndPos, item.Parent.StartPos);
                }
                else
                {
                    item.Parent.Right = item;
                    item.StartPos = Math.Max(item.StartPos, item.Parent.EndPos);
                }
            }
            next = next.Left ?? next.Right;
            for (; next == null; item = item.Parent)
            {
                Print(item, rootTop + 2 * level);
                if (--level < 0) break;
                if (item == item.Parent.Left)
                {
                    item.Parent.StartPos = item.EndPos;
                    next = item.Parent.Node.Right;
                }
                else
                {
                    if (item.Parent.Left == null)
                        item.Parent.EndPos = item.StartPos;
                    else
                        item.Parent.StartPos += (item.StartPos - item.Parent.EndPos) / 2;
                }
            }
        }
        Console.SetCursorPosition(0, rootTop + 2 * last.Count - 1);
    }

    private static void Print(NodeInfo item, int top)
    {
        SwapColors();
        Print(item.Text, top, item.StartPos);
        SwapColors();
        if (item.Left != null)
            PrintLink(top + 1, "┌", "┘", item.Left.StartPos + item.Left.Size / 2, item.StartPos);
        if (item.Right != null)
            PrintLink(top + 1, "└", "┐", item.EndPos - 1, item.Right.StartPos + item.Right.Size / 2);
    }

    private static void PrintLink(int top, string start, string end, int startPos, int endPos)
    {
        Print(start, top, startPos);
        Print("─", top, startPos + 1, endPos);
        Print(end, top, endPos);
    }

    private static void Print(string s, int top, int left, int right = -1)
    {
        Console.SetCursorPosition(left, top);
        if (right < 0) right = left + s.Length;
        while (Console.CursorLeft < right) Console.Write(s);
    }

    private static void SwapColors()
    {
        (Console.ForegroundColor, Console.BackgroundColor) = (Console.BackgroundColor, Console.ForegroundColor);
    }
    }
}