using TestCommon;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Exam1
{
    public class Q2Cryptanalyst : Processor
    {
        public Q2Cryptanalyst(string testDataName) : base(testDataName)
        {
            
        }

        public override string Process(string inStr) => Solve(inStr);

        public HashSet<string> Vocab = new HashSet<string>();

        public string Solve(string cipher)
        {
            string[] words = File.ReadAllLines(@"C:\Users\Sara\Documents\Term4\AlgorithmDesign\AD97982\Exam1\Exam1Tests\TestData\TD2\dictionary.txt");

            Dictionary<string, long> values = new Dictionary<string, long>();

            var obj = new PatternMatchingSuffixArray();

            for (int i=0; i<10; i++)
            {
                var encryption = new Encryption($"{i}", ' ', 'z', false);
                string decrypted = encryption.Decrypt(cipher);
                long count = obj.Solve(decrypted, words.Length, words);
                values.Add($"{i}", count);
            }

            var passKey = values.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;

            var finalEnc = new Encryption(passKey, ' ', 'z', false);

            return finalEnc.Decrypt(cipher).GetHashCode().ToString();

        }
    }
}