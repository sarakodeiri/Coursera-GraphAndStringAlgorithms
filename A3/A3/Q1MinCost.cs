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
        
        public long Solve(long nodeCount,long [][] edges, long startNode, long endNode)
        {
            long[] dist = new long[nodeCount];
            long[] distCop = new long[nodeCount];
            List<long> priorityQueue = new List<long>(); //keys are distances

            for (int i=0; i<dist.Length; i++)
            {
                dist[i] = int.MaxValue;
                distCop[i] = int.MaxValue;
                priorityQueue.Add(dist[i]);
            }
            dist[startNode - 1] = 0;
            distCop[startNode - 1] = 0;
            priorityQueue[0] = 0;
            

            while (priorityQueue.Count() != 0)
            {
                //Extract Min
                var u = priorityQueue.Min(); //u is min distance from start node
                var index = Array.IndexOf(distCop, u); //index is index of node with distance of u
                priorityQueue.Remove(u);
                distCop[index] = -1;

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
                            distCop[v] = dist[index] + edgeCost;
                            
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
                
            }

            return dist[endNode - 1] == int.MaxValue ? -1 : dist[endNode - 1];
        }
    }
}
