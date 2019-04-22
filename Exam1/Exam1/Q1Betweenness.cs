using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace Exam1
{
    public class Q1Betweenness : Processor
    {
        public Q1Betweenness(string testDataName) : base(testDataName)
        {
            this.ExcludeTestCaseRangeInclusive(15, 50);
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long[]>)Solve);


        public long[] Solve(long NodeCount, long[][] edges)
        {
            Graph graph = new Graph(NodeCount, edges, true);
            
            long[] distance = new long[NodeCount];
            for (int i = 0; i < NodeCount; i++)
                distance[i] = int.MaxValue;

            long[] preDec = new long[NodeCount];
            for (int i = 0; i < NodeCount; i++)
                preDec[i] = int.MaxValue;

            List<long> path = new List<long>();

            long[] result = new long[NodeCount];
            for (int i = 0; i < NodeCount; i++)
                result[i] = 0;

            for (int i=0; i<NodeCount; i++)
            {
                ComputeShortestPath(graph, distance, preDec, i, NodeCount);
                for (int j=0; j<NodeCount; j++)
                {
                    if (i != j && distance[j] > 1 && distance[j] != int.MaxValue)
                        path = path.Concat(ComputePath(preDec, i, j)).ToList();
                }
            }

            for (int i = 0; i < result.Length; i++)
                result[i] = path.Where(s => s == i).Count();




            return result;
        }

        private List<long> ComputePath(long[] preDec, long StartNode, long EndNode)
        {
            var temp = EndNode;
            List<long> path = new List<long>();
            while (temp != StartNode)
            {
                path.Add(temp);
                temp = preDec[temp];
            }
            path.Reverse();
           // path.RemoveAt(0);
            path.RemoveAt(path.Count() - 1);

            return path;

        }

        private void ComputeShortestPath(Graph graph, long[] distance, long[] preDec, long StartNode, long NodeCount)
        {
            //compute the length of a shortest path from startNode
            for (int i = 0; i < NodeCount; i++)
                distance[i] = int.MaxValue;

            for (int i = 0; i < NodeCount; i++)
                preDec[i] = int.MaxValue;

            Queue<long> queue = new Queue<long>();

            bool [] visited = new bool[NodeCount];
            for (int i = 0; i < visited.Length; i++)
                visited[i] = false;
            var adjacencyList = graph.adjacencyList;

            int dist = 0;

            distance[StartNode] = 0;
            visited[StartNode] = true;
            queue.Enqueue(StartNode);

            while (queue.Count() > 0)
            {
                dist++;
                long temp = queue.Dequeue();
                for (long i = 0; i < adjacencyList[temp].Count(); i++)
                {
                    var current = adjacencyList[temp][(int)i];
                    if (!visited[current])
                    {
                        preDec[current] = temp;
                        visited[current] = true;
                        distance[current] = distance[preDec[current]] + 1;
                        queue.Enqueue(current);
                    }
                }
            }
        }
    }
}
