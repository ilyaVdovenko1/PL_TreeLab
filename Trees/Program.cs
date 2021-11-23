using System;
using System.Diagnostics;
using System.Linq;

namespace Trees
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter a task:(1 or 2)");
            Task2();

            
            
        }

        static void Task1()
        {
            Console.WriteLine("Please enter tree sequences:");
            var seq = Console.ReadLine();
            if (seq != null)
            {
                var bstTree = new BinaryTree(seq.Split(',').ToList().Select(int.Parse).ToList());
                BinaryTree.Print(bstTree.RootOfTree, isBst: true);
                bstTree.Order(bstTree.RootOfTree);
                Console.WriteLine("Pre order:");
                Console.WriteLine(string.Join(',', bstTree.GetPreOrderValues.Select(x => x.Num.ToString())));
                Console.WriteLine("In order:");
                Console.WriteLine(string.Join(',', bstTree.GetInOrderValues.Select(x => x.Num.ToString())));
                Console.WriteLine("Post order:");
                Console.WriteLine(string.Join(',', bstTree.GetPostOrderValues.Select(x => x.Num)));
            }
            Main(null);
        }

        static void Task2()
        {
            Console.WriteLine("Please enter an expression:");
            var exp = Console.ReadLine() ?? throw new ArgumentNullException(nameof(Console.ReadLine));
            var parser = new Parser(exp);
            Console.WriteLine("Postfix form of expression:");
            Console.WriteLine($"{String.Join(',',parser.PostfixForm.Select(x=>x.Value))}");
            var tree = new BinaryTree(parser.PostfixForm);
            BinaryTree.Print(tree.RootOfTree);
            tree.Order(tree.RootOfTree);
            Console.WriteLine("Pre order:");
            Console.WriteLine(string.Join(',', tree.GetPreOrderValues.Select(x => x.Value)));
            Console.WriteLine("In order:");
            Console.WriteLine(string.Join(',', tree.GetInOrderValues.Select(x => x.Value)));
            Console.WriteLine("Post order:");
            Console.WriteLine(string.Join(',', tree.GetPostOrderValues.Select(x => x.Value)));
            Main(null);
        }
        
    }
}