using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A3
{
    public class Q1MinCost:Processor
    {
        public Q1MinCost(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long,long,long>)Solve);

        //public class Node {

        //    long distanceFromStart;
        //    long index;

        //    private Node (long Dist, long Index)
        //    {
        //        this.distanceFromStart = Dist;
        //        this.index = Index;
        //    }
        //}


        public long Solve(long nodeCount,long [][] edges, long startNode, long endNode)
        {
            //Write Your Code Here
            Graph graph = new Graph(nodeCount, edges, true);
            var adjacencyList = graph.adjacencyList;

            long[] dist = new long[nodeCount];

            for (int i=0; i<dist.Length; i++)
                dist[i] = int.MaxValue;
            dist[startNode - 1] = 0;


            //int[] priorityQueue = new int[nodeCount];
            List<long> priorityQueue = new List<long>(); //keys are distances
            for (int i = 0; i < dist.Length; i++)
                priorityQueue.Add(dist[i]);

            while(priorityQueue.Count() != 0)
            {
                //Extract Min
                var u = priorityQueue.Min(); //u is min distance from start node
                var index = Array.IndexOf(dist, u); //index is index of node with distance of u
                priorityQueue.Remove(u);
                
                for (int i=0; i < edges.Length; i++)
                {
                    
                    if (edges[i][0] == index + 1)
                    {
                        long v = edges[i][1] - 1;
                        long oldVDistance = dist[v];
                        long edgeCost = edges[i][2];
                        if (dist[v] > dist[index] + edgeCost)
                        {
                            dist[v] = dist[index] + edgeCost;
                            //change priority
                            for (int j=0; j < priorityQueue.Count(); j++)

                                if (priorityQueue[j] == oldVDistance)
                                {
                                    priorityQueue[j] = dist[v];
                                    break;
                                }
                                    
                            
                        }
                    }
                }
                //var u = priorityQueue[0];
                //var index = Array.IndexOf(priorityQueue, u);
                //priorityQueue[index] = null;
            }

            return dist[endNode - 1];
        }
    }
}
