using System;
using System.Collections.Generic;
using TestCommon;

namespace A1
{
    public class Q5StronglyConnected: Processor
    {
        public Q5StronglyConnected(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        bool[] visited;
        List<long> order;


        public long Solve(long nodeCount, long[][] edges)
        {
            //Compute the number of strongly connected components of a given directed graph
            //with 𝑛 vertices and 𝑚 edges.

            Graph graph = new Graph(nodeCount, edges, true);
            var adjacencyList = graph.adjacencyList;
            var reverseAdjacencyList = graph.reverseAdjacencyList;

            visited = graph.visited;
            order = new List<long>();
            
            return SCCCount(adjacencyList, reverseAdjacencyList);
        }

        private long SCCCount(List<long>[] adjacencyList, List<long>[] reverseAdjacencyList)
        {
            long finalResult = 0;
            for (int i = 0; i < reverseAdjacencyList.Length; i++)
                if (!visited[i])
                    DFS(i, reverseAdjacencyList);
            order.Reverse();

            for (int i = 0; i < visited.Length; i++)
                visited[i] = false;

            for (int i=0; i<order.Count; i++)
                if (!visited[order[i]])
                {
                    Explore(order[i], adjacencyList);
                    finalResult++;
                }
            return finalResult;
        }

        private void DFS(long start, List<long>[] reverseAdjacencyList)
        {
            visited[start] = true;
            for (int i = 0; i < reverseAdjacencyList[start].Count; i++)
                if (!visited[reverseAdjacencyList[start][i]])
                    DFS(reverseAdjacencyList[start][i], reverseAdjacencyList);
            order.Add(start);
        }

        private void Explore(long start, List<long>[] adjacencyList)
        {
            visited[start] = true;
            for (int i = 0; i < adjacencyList[start].Count; i++)
                if (!visited[adjacencyList[start][i]])
                    Explore(adjacencyList[start][i], adjacencyList);
        }
    }
}
