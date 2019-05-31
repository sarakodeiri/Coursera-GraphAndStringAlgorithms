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
        public bool[] visited;
        public long[] parent;
        public long maxFlow;
        public long source;
        public long sink;

        public Residual(long nodeCount, long[][] edges)
        {
            this.nodeCount = nodeCount;
            this.edges = edges;

            adjacencyList = new long[nodeCount, nodeCount];
            
            for (int i = 0; i < edges.Length; i++)
                adjacencyList[edges[i][0] - 1, edges[i][1] - 1] += edges[i][2];

            visited = new bool[nodeCount];
            for (int i = 0; i < visited.Length; i++)
                visited[i] = false;

            parent = new long[nodeCount];
            for (int i = 0; i < parent.Length; i++)
                parent[i] = -1;

            maxFlow = 0;
            source = 0;
            sink = nodeCount - 1;
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
            Queue<long> queue = new Queue<long>();
            queue.Enqueue(source);
            visited[source] = true;
            while (queue.Count != 0)
            {
                var current = queue.Dequeue();
                if (current == sink)
                    return true;

                for (int i = 0; i < adjacencyList.Length; i++)
                    if (adjacencyList[current, i] > 0 && !visited[i])
                    {
                        parent[i] = current;
                        queue.Enqueue(i);
                        visited[i] = true;
                    }
            }
            return false;
        }
    }

}


