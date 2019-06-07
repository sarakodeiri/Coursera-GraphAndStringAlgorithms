using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A8
{
    public class Residual
    {
        public long nodeCount;
        public long[][] edges;
        public long[,] adjacencyList;
        public long[] parent;
        public long maxFlow;
        public long source;
        public long sink;
        public long flightCount;
        public long crewCount;

        public Residual(long nodeCount, long[][] edges) //For the first question
        {
            this.edges = edges;

            parent = new long[nodeCount];
            for (int i = 0; i < parent.Length; i++)
                parent[i] = -1;

            maxFlow = 0;
            source = 0;
            sink = nodeCount - 1;

            this.nodeCount = nodeCount;

            this.adjacencyList = new long[(int)nodeCount, (int)nodeCount];
            
            for (int i = 0; i < edges.GetLength(0); i++)
                adjacencyList[edges[i][0] - 1, edges[i][1] - 1] += edges[i][2];
            
        }

        public Residual(long flightCount, long crewCount, long[][] edges) //For the second question
        {
            this.nodeCount = flightCount + crewCount + 2;
            this.edges = edges;

            maxFlow = 0;

            this.source = 0;
            this.sink = nodeCount - 1;
            this.flightCount = flightCount;
            this.crewCount = crewCount;

            adjacencyList = new long[nodeCount, nodeCount];

            for (int i = 0; i < edges.Length; i++)
                for (int j = 0; j < edges[i].Length; j++)
                    if (edges[i][j] == 1)
                        adjacencyList[i + 1, j + 1 + flightCount] = 1;

            for (int i = 1; i <= flightCount + crewCount; i++)
            {
                if (i <= this.flightCount)
                    adjacencyList[0, i] = 1;
                else
                    adjacencyList[i, flightCount + crewCount + 1] = 1;
            }

            parent = new long[nodeCount];
            for (int i = 0; i < parent.Length; i++)
                parent[i] = -1;
        }

        public long ComputeMaxFlow()
        {
            while (PathExists())
            {
                long pathFlow = long.MaxValue;

                for (long i = sink; i != source; i = parent[i])
                    pathFlow = Math.Min(pathFlow, adjacencyList[parent[i], i]);

                for (long i = sink; i != source; i = parent[i])
                {
                    adjacencyList[parent[i], i] -= pathFlow;
                    adjacencyList[i, parent[i]] += pathFlow;
                }

                maxFlow += pathFlow;
            }

            return maxFlow;
        }

        private bool PathExists()
        {
            bool[] visited = new bool[nodeCount];
            for (int i = 0; i < visited.Length; i++)
                visited[i] = false;

            Queue<long> queue = new Queue<long>(); //BFS
            queue.Enqueue(source);
            visited[source] = true;

            while (queue.Count != 0)
            {
                long current = queue.Dequeue();

                for (int i = 0; i < adjacencyList.GetLength(1); i++)
                    if (!(adjacencyList[current, i] == 0 || visited[i]))
                    {
                        parent[i] = current;
                        visited[i] = true;
                        queue.Enqueue(i);
                    }
            }

            return visited[sink];
        }

    }

}


