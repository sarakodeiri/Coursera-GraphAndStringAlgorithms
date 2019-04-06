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
            // write your code here
            List<Edge> edges = new List<Edge>();
            //List<string> result = new List<string>();


            long indexInGraph = 0;
            foreach (string pattern in patterns)
            {
                var currentNode = new Node(0);
                for (int i=0; i < pattern.Length; i++)
                {
                    bool alreadyAnEdge = false;
                    var currentSymbol = pattern[i];

                    for (int j=0; j < edges.Count(); j++)
                    {
                        if (edges[j].from.index == currentNode.index && edges[j].label == currentSymbol)
                        {
                            currentNode = edges[j].to;
                            alreadyAnEdge = true;
                            break;
                        }
                    }
                    
                    if (!alreadyAnEdge)
                    {
                        indexInGraph++;
                        var newNode = new Node(indexInGraph); //HERE!
                        edges.Add(new Edge(currentNode, newNode, currentSymbol));
                        currentNode = newNode;
                    }
                    

                }
            }

            //0->1:A
            string[] result = new string[edges.Count()];

            for (int i=0; i<result.Length; i++)
            {
                result[i] = $"{edges[i].from.index}->{edges[i].to.index}:{edges[i].label}";
            }

            return result;
        }


        private class Edge
        {
            public Node from, to;
            public char label;
            //public Edge(Node from, char label)
            //{
            //    this.from = from;
            //    this.label = label;
            //}

            public Edge(Node from, Node to, char label)
            {
                this.from = from;
                this.to = to;
                this.label = label;
            }
        }

        private class Node
        {
            public long index;
            public Node (long index)
            {
                this.index = index;
            }
        }
    }
}
