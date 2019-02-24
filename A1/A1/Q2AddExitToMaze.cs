using System;
using System.Collections.Generic;
using TestCommon;

namespace A1
{
    public class Q2AddExitToMaze : Processor
    {
        public Q2AddExitToMaze(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        public long Solve(long nodeCount, long[][] edges)
        {
            //Given an undirected graph with 𝑛 vertices and 𝑚 edges, 
            //compute the number of connected components in it.

            Graph graph = new Graph(nodeCount, edges, false);
            var adjacencyList = graph.adjacencyList;
            var visited = graph.visited;
            
            int[] CCNum = new int[nodeCount];
            
            int ans = DFS(adjacencyList, visited, CCNum);
            return ans;
        }

        private int DFS(List<long>[] adj, bool[] visited, int[] CCNum)
        {
            int componentCount = 0;
            Stack<long> myStack = new Stack<long>();

            for (long i=0; i<adj.Length; i++)
            {
                if (!visited[i])
                {
                    componentCount++;
                    myStack.Push(i);
                    while (myStack.Count > 0)
                    {
                        long current = myStack.Pop();
                        CCNum[current] = componentCount;
                        visited[current] = true;

                        foreach (long v in adj[current])
                            if (!visited[v])
                                myStack.Push(v);
                    }
                }
            }

            return componentCount;
        }
    }
}
