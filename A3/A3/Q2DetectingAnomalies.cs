using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;
namespace A3
{
    public class Q2DetectingAnomalies:Processor
    {
        public Q2DetectingAnomalies(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);


        public long Solve(long nodeCount, long[][] edges)
        {
            long[] dist = new long[nodeCount + 1];
            for (int i = 0; i < dist.Length; i++)
                dist[i] = int.MaxValue;
                
            dist[0] = -1;
            dist[1] = 0;

            for (int i=0; i < nodeCount; i++)
                for (int j=0; j < edges.Length; j++)
                {
                    long start = edges[j][0];
                    long end = edges[j][1];
                    long weight = edges[j][2];
                    if (dist[end] > dist[start] + weight)
                        dist[end] = dist[start] + weight;

                }

            for (int i = 0; i < edges.Length; i++)
            {
                long start = edges[i][0];
                long end = edges[i][1];
                long weight = edges[i][2];
                if (dist[end] > dist[start] + weight)
                    return 1;
            }

            return 0;
        }
    }
}
