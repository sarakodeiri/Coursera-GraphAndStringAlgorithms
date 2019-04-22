using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace Exam1
{
    public class CunstructSuffixArray
    {

        private long[] SortCharacters(char[] textArray) //count sort
        {
            //char[] allChars = new char[];
            char[] allChars = String.Concat(Enumerable.Range(' ', 96).Select(c => (char)c)).ToCharArray();
            long[] order = new long[textArray.Length];
            int[] count = new int[allChars.Length];
            var charToNum = new Dictionary<char, int>();

            for (int i = 0; i < allChars.Length; i++)
            {
                count[i] = 0;
                charToNum[allChars[i]] = i;
            }
            
            for (int i = 0; i < order.Length; i++)
                count[charToNum[textArray[i]]]++;

            for (int i = 1; i < count.Length; i++)
                count[i] += count[i - 1];

            for (int i = order.Length - 1; i >= 0; i--)
            {
                char ch = textArray[i];
                count[charToNum[ch]]--;
                order[count[charToNum[ch]]] = i;
            }
            return order;
        }


        private long[] ComputeCharClasses(char[] textArray, long[] order) ///////////////////
        {
            long[] @class = new long[textArray.Length];
            @class[order[0]] = 0;

            for (int i = 1; i < textArray.Length; i++)
            {
                if (textArray[order[i]] != textArray[order[i - 1]])
                    @class[order[i]] = @class[order[i - 1]] + 1;
                
                else
                    @class[order[i]] = @class[order[i - 1]];
                
            }
            return @class;
        }

        private long[] SortDoubled(char[] textArray, int l, long[] order, long[] @class)
        {
            int n = textArray.Length;
            long[] count = new long[n];
            for (int i = 0; i < n; i++)
                count[i] = 0;

            long[] newOrder = new long[n];
            for (int i = 0; i < n; i++)
                count[@class[i]]++;

            for (int i = 1; i < n; i++)
                count[i] += count[i - 1];
            
            for (int i = n - 1; i > -1; i--)
            {
                int start = (int)(order[i] - l + n) % n;
                int cl = (int)@class[start];
                count[cl]--;
                newOrder[count[cl]] = start;
            }
            return newOrder;
        }

        private long[] UpdateClasses(long[] order, long[] @class, int l)
        {
            int n = order.Length;
            long[] newClass = new long[n];
            newClass[order[0]] = 0;
            for (int i = 1; i < n; i++)
            {
                int current = (int)order[i];
                int prev = (int)order[i - 1];
                int mid = current + l;
                int midPrev = (prev + l) % n;
                if ((@class[current] != @class[prev]) || @class[mid] != @class[midPrev])
                    newClass[current] = newClass[prev] + 1;
                
                else
                    newClass[current] = newClass[prev];
            }
            return newClass;
        }

        public long[] Solve(string text)
        {
            char[] textArray = text.ToCharArray();
            long[] order = SortCharacters(textArray);
            long[] @class = ComputeCharClasses(textArray, order);
            int l = 1;
            while (l < text.Length)
            {
                order = SortDoubled(textArray, l, order, @class);
                @class = UpdateClasses(order, @class, l);
                l *= 2;
            }
            return order;
        }

        
    }
}
