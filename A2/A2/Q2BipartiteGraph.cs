using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A2
{
    public class Q2BipartiteGraph : Processor
    {
        public Q2BipartiteGraph(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);


        public long Solve(long NodeCount, long[][] edges)
        {
            //Given an undirected graph with 𝑛 vertices and 𝑚 edges, check whether it is bipartite.
            Graph graph = new Graph(NodeCount, edges, false);
            var adjacencyList = graph.adjacencyList;
            var visited = graph.visited;
            long[] distance = new long[NodeCount];
            for (int i = 0; i < NodeCount; i++)
                distance[i] = int.MaxValue;

            for (int i = 0; i < NodeCount; i++)
                if (distance[i] == int.MaxValue)
                    if (!FirstRes(adjacencyList, i, distance, visited, NodeCount))
                        return 0;
            return 1;

            //return FirstRes(adjacencyList, 0, distance, visited, NodeCount) ? 1 : 0;

            //for (int i = 0; i < V; i++)
            //    if (colorArr[i] == -1)
            //        if (isBipartiteUtil(G, i, colorArr) == false)
            //            return false;

            //return true;

        }

        private bool FirstRes (List<long>[] adjacencyList, int StartNode, 
            long[] distance, bool[] visited, long NodeCount)
        {
            
            Queue<long> queue = new Queue<long>();
            
            long[] preDec = new long[NodeCount];
            for (int i = 0; i < NodeCount; i++)
                preDec[i] = int.MaxValue;

            int dist = 0;

            distance[0] = 0;
            visited[0] = true;
            queue.Enqueue(0);

            while (queue.Count() > 0)
            {
                dist++;
                long temp = queue.Dequeue();
                for (long i = 0; i < adjacencyList[temp].Count(); i++)
                {
                    var current = adjacencyList[temp][(int)i];
                    preDec[current] = temp;
                    if (!visited[current])
                    {
                        visited[current] = true;
                        distance[current] = distance[preDec[current]] + 1;
                        queue.Enqueue(current);
                    }
                }

            }

            for (int i = 0; i < NodeCount - 1; i++)
                for (int j = i + 1; j < NodeCount; j++)
                {
                    if (distance[i] == distance[j])
                        if (adjacencyList[i].Contains(j))
                            return false;
                }

            return true;
        }
    }

}
