using System;
using System.Collections.Generic;
using TestCommon;

namespace A1
{
    public class Q3Acyclic : Processor
    {
        public Q3Acyclic(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        bool[] visited;
        bool[] keepTrack;

        public long Solve(long nodeCount, long[][] edges)
        {
            //Check whether a given directed graph with 𝑛 vertices and 𝑚 edges contains a cycle.

            Graph graph = new Graph(nodeCount, edges, true);
            var adjacencyList = graph.adjacencyList;
            visited = graph.visited;

            keepTrack = new bool[nodeCount];

            for (int i = 0; i < visited.Length; i++)
                keepTrack[i] = false;
            
            for (int i = 0; i < adjacencyList.Length; i++)
                if (IsAcyclic(adjacencyList, i))
                    return 1;
            return 0;
        }
        
        private bool IsAcyclic(List<long>[] adj, long i)
        {
            if (!visited[i])
            {
                visited[i] = true;
                keepTrack[i] = true;

                for (int j = 0; j < adj[i].Count; j++)
                {
                    long current = adj[i][j];
                    if ((!visited[current] && IsAcyclic(adj, current)) || keepTrack[current])
                        return true;
                }
            }
            keepTrack[i] = false;
            return false;
        }
    }
}