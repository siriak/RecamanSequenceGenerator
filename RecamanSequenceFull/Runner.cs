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

        public RecamanSequence sequence = new RecamanSequence();
        private readonly BigInteger[] appearedBuffer = new BigInteger[100_000];

        public void Run()
        {
            DisplaySequenceState();

            var enumerator = sequence.Sequence().GetEnumerator();

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

        public static Runner Load()
        {
            if (File.Exists(statefilename))
            {
                using (var stream = File.OpenRead(statefilename))
                using (var reader = new StreamReader(stream))
                {
                    return JsonConvert.DeserializeObject<Runner>(reader.ReadLine());
                }
            }

            return new Runner();
        }

        private void SaveAppeared()
        {
            using (var stream = File.OpenWrite($"elements/elements_{sequence.Step}.json"))
            using (var writer = new StreamWriter(stream))
            {
                writer.WriteLine(JsonConvert.SerializeObject(appearedBuffer));
            }
        }

        public void SaveState()
        {
            using (var stream = File.OpenWrite("state.json"))
            using (var writer = new StreamWriter(stream))
            {
                writer.WriteLine(JsonConvert.SerializeObject(this));
            }

            using (var stream = File.OpenWrite($"states/state_{sequence.Step}.json"))
            using (var writer = new StreamWriter(stream))
            {
                writer.WriteLine(JsonConvert.SerializeObject(this));
            }
        }
    }
}
