using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A2
{
    public class Q1ShortestPath : Processor
    {
        public Q1ShortestPath(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long,long[][], long, long, long>)Solve);
        

        public long Solve(long NodeCount, long[][] edges, long StartNode,  long EndNode)
        {
            //Write your code here
            Graph graph = new Graph(NodeCount, edges, false);
            var adjacencyList = graph.adjacencyList;
            Queue<long> queue = new Queue<long>();
            var visited = graph.visited;
            long[] distance = new long[NodeCount];
            for (int i = 0; i < NodeCount; i++)
                distance[i] = int.MaxValue;
            long[] preDec = new long[NodeCount];
            for (int i = 0; i < NodeCount; i++)
                preDec[i] = int.MaxValue;

            int dist = 0;

            StartNode--;
            EndNode--;

            distance[StartNode] = 0;
            visited[StartNode] = true;
            queue.Enqueue(StartNode);

            while (queue.Count() > 0)
            {
                dist++;
                long temp = queue.Dequeue();
                for (long i=0; i<adjacencyList[temp].Count(); i++)
                {
                    var current = adjacencyList[temp][(int)i];
                    preDec[current] = temp;
                    if (!visited[current])
                    {
                        visited[current] = true;
                        distance[current] = distance[preDec[current]]+1;
                        queue.Enqueue(current);
                    }
                }
                    
            }

            if (distance[EndNode] == int.MaxValue)
                return -1;

            return distance[EndNode];
        }
    }
}


