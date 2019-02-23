using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A1
{
    public class Graph
    {
        long nodeCount;
        long[][] edges;
        bool directed;
        public List<long>[] adjacencyList;
        public List<long>[] reverseAdjacencyList;
        public bool[] visited;
        
        public Graph (long nodeCount, long[][] edges, bool directed)
        {
            this.nodeCount = nodeCount;
            this.edges = edges;
            this.directed = directed;

            visited = new bool[nodeCount];
            for (int i = 0; i < visited.Length; i++)
                visited[i] = false;

            adjacencyList = new List<long>[nodeCount];
            reverseAdjacencyList = new List<long>[nodeCount];
            for (int i = 0; i < adjacencyList.Length; i++)
            {
                adjacencyList[i] = new List<long>();
                reverseAdjacencyList[i] = new List<long>();
            }


            for (int i = 0; i < edges.Length; i++)
            {
                long first = edges[i][0] - 1;
                long second = edges[i][1] - 1;
                adjacencyList[first].Add(second);
                reverseAdjacencyList[second].Add(first);
                if (!directed)
                    adjacencyList[second].Add(first);
            }

        }
    }
}
