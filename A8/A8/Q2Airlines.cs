using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A8
{
    public class Q2Airlines : Processor
    {
        public Q2Airlines(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long, long[][], long[]>)Solve);

        public virtual long[] Solve(long flightCount, long crewCount, long[][] info)
        {
            //Construct G'
            //Compute maxFlow
            //Find matching M : return M

            Residual gPrime = new Residual(flightCount, crewCount, info);
            
            long[] result = new long[flightCount];
            for (int i = 0; i < result.Length; i++)
                result[i] = -1;

            gPrime.ComputeMaxFlow();

            for (int i = (int)flightCount + 1; i < gPrime.nodeCount; i++)
                for (int j = 0; j < flightCount; j++)
                    if (gPrime.adjacencyList[i, j + 1] == 1)
                        result[j] = i - flightCount;

            return result;
        }

  }
}
