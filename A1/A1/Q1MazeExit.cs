using System;
using System.Collections.Generic;
using TestCommon;

namespace A1
{
    public class Q1MazeExit : Processor
    {
        public Q1MazeExit(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long, long, long>)Solve);

        public long Solve(long nodeCount, long[][] edges, long StartNode, long EndNode)
        {
            //Given an undirected graph and two distinct vertices 𝑢 and 𝑣, 
            //check if there is a path between 𝑢 and 𝑣.

            Graph graph = new Graph(nodeCount, edges, false);
            var adjacencyList = graph.adjacencyList;
            var visited = graph.visited;

            Explore(adjacencyList, StartNode - 1, visited);

            if (visited[EndNode - 1])
                return 1;
            return 0;
        }
        
        private void Explore(List<long>[] adj, long Start, bool[] visited)
        {
            visited[Start] = true;
            foreach (var v in adj[Start])
                if (!visited[v])
                    Explore(adj, v, visited);
        }
     }
}
