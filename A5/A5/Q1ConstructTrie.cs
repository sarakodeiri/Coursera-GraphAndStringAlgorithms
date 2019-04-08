using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A5
{
    public class Q1ConstructTrie : Processor
    {
        public Q1ConstructTrie(string testDataName) : base(testDataName)
        {
            this.VerifyResultWithoutOrder = true;
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<long, String[], String[]>)Solve);

        public string[] Solve(long n, string[] patterns)
        {
            var edges = TrieMaker(patterns);

            string[] result = new string[edges.Count()];

            for (int i=0; i<result.Length; i++)
                result[i] = $"{edges[i].from.index}->{edges[i].to.index}:{edges[i].label}";
            
            return result;
        }

        public List<Edge> TrieMaker(string[] patterns)
        {
            List<Edge> edges = new List<Edge>();
            Node root = new Node(0);
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
                        edges.Add(newEdge);
                        currentNode = newNode;
                    }
                }
            }
            return edges;
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
            public Node (long index)
            {
                this.index = index;
            }
        }
    }
}
