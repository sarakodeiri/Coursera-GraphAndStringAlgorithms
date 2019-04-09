using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A5
{
    public class Q2MultiplePatternMatching : Processor
    {
        public Q2MultiplePatternMatching(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, long, String[], long[]>)Solve);

        public long[] Solve(string text, long n, string[] patterns)
        {
            List<long> result = new List<long>();

            Node root = new Node(0);
            TrieMaker(patterns, ref root);
            long index = 0;

            while (text.Count() != 0)
            {
                Node currentNode = root;
                for (int i=0; i<text.Length; i++)
                {
                    var child = currentNode.afterEdges.Find(e => e.label == text[i]);
                    if (child == null)
                        break;
                    else
                    {
                        if (child.to.patternEnd)
                            result.Add(index);

                        currentNode = child.to;
                        continue;
                    }
                }
                text = text.Remove(0,1);
                index++;
            }
            
            result = result.Distinct().ToList();
            if (result.Count() == 0)  result.Add(-1);
            return result.ToArray();
        }

        

        public void TrieMaker(string[] patterns, ref Node root)
        {
            long indexInGraph = 0;

            foreach (string pattern in patterns)
            {
                Node currentNode = root;
                for (int i = 0; i < pattern.Length; i++)
                {
                    bool alreadyAnEdge = false;
                    char currentSymbol = pattern[i];

                    foreach (Edge outEdge in currentNode.afterEdges)
                        if (outEdge.label == currentSymbol)
                        {
                            currentNode = outEdge.to;
                            alreadyAnEdge = true;
                        }

                    if (!alreadyAnEdge)
                    {
                        indexInGraph++;
                        var newNode = new Node(indexInGraph);
                        var newEdge = new Edge(currentNode, newNode, currentSymbol);
                        currentNode.Children.Add(newNode);
                        currentNode.afterEdges.Add(newEdge);
                        currentNode = newNode;
                    }

                    if (i == pattern.Length - 1)
                        currentNode.patternEnd = true;
                }
            }
        }


        public class Edge
        {
            public Node from, to;
            public char label;

            public Edge(Node from, Node to, char label)
            {
                this.from = from;
                this.to = to;
                this.label = label;
            }
        }

        public class Node
        {
            public long index;
            public List<Node> Children = new List<Node>();
            public List<Edge> afterEdges = new List<Edge>();
            public Node(long index)
            {
                this.index = index;
            }

            public bool patternEnd = false;
        }
    }
}
