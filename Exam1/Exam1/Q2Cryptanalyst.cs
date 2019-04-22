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
            ExcludeTestCaseRangeInclusive(21, 37);
        }

        public override string Process(string inStr) => Solve(inStr);

        public HashSet<string> Vocab = new HashSet<string>();

        public string Solve(string cipher)
        {
            string[] dictionaryWords = File.ReadAllLines(@"Exam1_TestData\TD2\dictionary.txt");

            var obj = new PatternMatchingSuffixArray();
            string passKey = string.Empty;

            for (int i=0; i<100; i++)
            {
                var encryption = new Encryption($"{i}", ' ', 'z', false);
                string decrypted = encryption.Decrypt(cipher);
                string[] words = decrypted.Split(' ');
                int count = 0;
                for (int j = 0; j < words.Length; j++)
                {
                    if (dictionaryWords.Contains(words[j]))
                        count++;
                    if (count == 10)
                    {
                        passKey = $"{i}";
                        break;
                    }
                }
            }


            var finalEnc = new Encryption(passKey, ' ', 'z', false);

            return finalEnc.Decrypt(cipher).GetHashCode().ToString();

        }
    }
}