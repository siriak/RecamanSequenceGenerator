using Newtonsoft.Json;
using System;
using System.IO;

namespace RecamanSequenceGenerator
{
    [Serializable]
    internal class Runner
    {
        private const string statefilename = "state.json";

        public RecamanSequenceGenerator sequence = new RecamanSequenceGenerator();

        public void Run()
        {
            DisplaySequenceState();

            var enumerator = sequence.GetSequence().GetEnumerator();

            while (true)
            {
                for (var i = 0; i < 1_000_000; i++)
                {
                    enumerator.MoveNext();
                }

                DisplaySequenceState();
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

        public void SaveState()
        {
            var objectString = JsonConvert.SerializeObject(this);
            File.WriteAllText(statefilename, objectString);
            File.WriteAllText($"states/state_{sequence.Step}.json", objectString);
        }
    }
}
