using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TestCommon;

namespace A1
{
    public class Q4OrderOfCourse: Processor
    {
        public Q4OrderOfCourse(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long[]>)Solve);

        bool[] visited;
        List<long> result;

        public long[] Solve(long nodeCount, long[][] edges)
        {
            //Compute a topological ordering of a given directed acyclic graph 
            //(DAG) with 𝑛 vertices and 𝑚 edges.

            Graph graph = new Graph(nodeCount, edges, true);
            var adjacencyList = graph.adjacencyList;
            visited = graph.visited;

            result = new List<long>();
            
            TopologicalSort(adjacencyList);
            
            result.Reverse();
            return result.ToArray();
        }

        private void TopologicalSort(List<long>[] adjacencyList)
        {
            for (int i = 0; i < adjacencyList.Length; i++)
                if (!visited[i])
                    DFS(adjacencyList, result, i);
        }

        private void DFS(List<long>[] adj, List<long> result, long i)
        {
            visited[i] = true;
            for (int j = 0; j < adj[i].Count; j++)
            {
                long current = adj[i][j];

                if (!visited[current])
                    DFS(adj, result, current);
            }

            result.Add(i+1);
        }

        public override Action<string, string> Verifier { get; set; } = TopSortVerifier;

        /// <summary>
        /// کد شما با متد زیر راست آزمایی میشود
        /// این کد نباید تغییر کند
        /// داده آزمایشی فقط یک جواب درست است
        /// تنها جواب درست نیست
        /// </summary>
        public static void TopSortVerifier(string inFileName, string strResult)
        {
            long[] topOrder = strResult.Split(TestTools.IgnoreChars)
                .Select(x => long.Parse(x)).ToArray();

            long count;
            long[][] edges;
            TestTools.ParseGraph(File.ReadAllText(inFileName), out count, out edges);

            // Build an array for looking up the position of each node in topological order
            // for example if topological order is 2 3 4 1, topOrderPositions[2] = 0, 
            // because 2 is first in topological order.
            long[] topOrderPositions = new long[count];
            for (int i = 0; i < topOrder.Length; i++)
                topOrderPositions[topOrder[i] - 1] = i;
            // Top Order nodes is 1 based (not zero based).

            // Make sure all direct depedencies (edges) of the graph are met:
            //   For all directed edges u -> v, u appears before v in the list
            foreach (var edge in edges)
                if (topOrderPositions[edge[0] - 1] >= topOrderPositions[edge[1] - 1])
                    throw new InvalidDataException(
                        $"{Path.GetFileName(inFileName)}: " +
                        $"Edge dependency violoation: {edge[0]}->{edge[1]}");

        }
    }
}
