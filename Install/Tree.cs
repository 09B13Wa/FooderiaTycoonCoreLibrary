using System;
using System.Collections.Generic;
using System.Linq;

namespace Install
{
    public class Tree
    {
        protected List<Tree> Forest;
        protected object Root;
        protected int NumberOfTrees;
        protected Type NodeType;

        public Tree(object root, List<Tree> forest, Type nodeType)
        {
            Forest = new List<Tree>();
            foreach (Tree tree in forest)
            {
                Forest.Add(tree);
            }

            Root = root;
            NumberOfTrees = 0;
            NodeType = nodeType;
        }

        public List<Tree> GetForest()
        {
            return Forest;
        }

        public object GetRoot()
        {
            return Root;
        }

        public void EmptyTree()
        {
            Forest = null;
            Root = null;
            NumberOfTrees = 0;
        }

        public void InsertTree(int index, Tree newTree)
        {
            if (index <= NumberOfTrees && index >= 0)
            {
                if (NumberOfTrees == 0)
                {
                    Forest = new List<Tree>();
                    Forest.Add(newTree);
                }
                else
                    Forest.Insert(index, newTree);
            }
            else
            {
                string NumberOfTreesStr = NumberOfTrees.ToString();
                Console.Error.WriteLine("Error: Tree deletion index too high. Maximum is : " + NumberOfTreesStr +
                                        "or too low : must be greater or equal to zero.");
            }
        }

        public void DeleteTree(int index)
        {
            if (index >= 0 && index < NumberOfTrees)
            {
                Forest.RemoveAt(index);
            }
            else
            {
                string NumberOfTreesStr = NumberOfTrees.ToString();
                Console.Error.WriteLine("Error: Tree deletion index too high. Maximum is : " + NumberOfTreesStr +
                                        "or too low : must be greater or equal to zero.");
            }

            if (NumberOfTrees == 0)
            {
                Forest = null;
                Root = null;
            }
        }

        public Tree GetTree(int index)
        {
            if (index >= 0 && index < NumberOfTrees)
            {
                return Forest[index];
            }
            //for structure
            else
            {
                string NumberOfTreesStr = NumberOfTrees.ToString();
                Console.Error.WriteLine("Error: Tree deletion index too high. Maximum is : " + NumberOfTreesStr +
                                        "or too low : must be greater or equal to zero.");
                return null;
            }
        }

        public int GetNumberOfTrees()
        {
            return NumberOfTrees;
        }

        public void DepthFirstSearch(Func<object, object> preOrderFucntion, Func<object, object> intermediateFunction,
            Func<object, object> postOrderFunction, Func<object, object> leafFunction)
        {
            if (NumberOfTrees == 0)
            {
                leafFunction(Root);
            }
            else
            {
                preOrderFucntion(Root);
                for (int treeCursor = 0; treeCursor < NumberOfTrees - 2; treeCursor++)
                {
                    Forest[treeCursor].DepthFirstSearch(preOrderFucntion, intermediateFunction, postOrderFunction,
                        leafFunction);
                    intermediateFunction(Root);
                }

                Forest[NumberOfTrees - 1].DepthFirstSearch(preOrderFucntion, intermediateFunction, postOrderFunction,
                    leafFunction);
                postOrderFunction(Root);
            }

        }

        public void BreadthFirstSearch(Func<object, object> rootFunction)
        {
            Stack<Tree> mainStack = new Stack<Tree>();
            mainStack.Push(this);
            while (NumberOfTrees != 0)
            {
                Tree tree = mainStack.First();
                mainStack.Pop();
                rootFunction(Root);
                for (int treeCursor = 0; treeCursor < NumberOfTrees; treeCursor++)
                {
                    mainStack.Push(GetTree(treeCursor));
                }
            }
        }
    }
}