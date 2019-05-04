using System;
using System.IO;
using System.Numerics;

using Newtonsoft.Json;

namespace RecamanSequenceFull
{
    [Serializable]
    internal class Runner
    {
        private const string statefilename = "state.json";

        public RecamanSequenceGenerator sequence = new RecamanSequenceGenerator();
        private readonly BigInteger[] appearedBuffer = new BigInteger[100_000];

        public void Run()
        {
            DisplaySequenceState();

            var enumerator = sequence.GetSequence().GetEnumerator();

            while (true)
            {
                for (var i = 0; i < 100_000; i++)
                {
                    enumerator.MoveNext();
                    appearedBuffer[i] = enumerator.Current;
                }

                DisplaySequenceState();
                SaveAppeared();
                SaveState();
            }
        }

        private void DisplaySequenceState()
        {
            Console.Clear();

            Console.WriteLine($"Max found:     {sequence.MaxFound:n0}       ");
            Console.WriteLine($"Min not found: {sequence.MinNotFound:n0}    ");
            Console.WriteLine("                                             ");
            Console.WriteLine($"Count found:   {sequence.CountFound:n0}     ");
            Console.WriteLine($"Fillness:      {100 * sequence.Fillness:n4}%");
            Console.WriteLine("                                             ");
            Console.WriteLine($"Step:          {sequence.Step:n0}           ");
        }

        public static Runner Load() => File.Exists(statefilename) ? JsonConvert.DeserializeObject<Runner>(File.ReadAllText(statefilename)) : new Runner();

        private void SaveAppeared() => File.WriteAllText($"elements/elements_{sequence.Step}.json", JsonConvert.SerializeObject(appearedBuffer));

        public void SaveState()
        {
            var objectString = JsonConvert.SerializeObject(this);
            File.WriteAllText(statefilename, objectString);
            File.WriteAllText($"states/state_{sequence.Step}.json", objectString);
        }
    }
}
