using System;
using System.IO;

using Newtonsoft.Json;

namespace RecamanSequenceLongJsonExport
{
    [Serializable]
    internal class Runner
    {
        private const string filename = "recamanlongjsonexport.json";

        public RecamanSequence sequence = new RecamanSequence();

        public void Run()
        {
            DisplaySequenceState();

            var enumerator = sequence.Sequence().GetEnumerator();

            while (true)
            {
                for (var i = 0; i < 100_000; i++)
                {
                    enumerator.MoveNext();
                }

                DisplaySequenceState();
                Save();
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
            if (File.Exists(filename))
            {
                using (var stream = File.OpenRead(filename))
                using (var reader = new StreamReader(stream))
                {
                    return JsonConvert.DeserializeObject<Runner>(reader.ReadLine());
                }
            }

            return new Runner();
        }

        public void Save()
        {
            using (var stream = File.OpenWrite(filename))
            using (var writer = new StreamWriter(stream))
            {
                writer.WriteLine(JsonConvert.SerializeObject(this));
            }
        }
    }
}
