using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace RecamanSequenceLong
{
    [Serializable]
    internal class Runner
    {
        private const string filename = "recamanlong.dat";
        private const int displayIntervalMs = 10_000;
        private const int saveIntervalMs = 60_000;

        private RecamanSequence sequence = new RecamanSequence();

        public void Run()
        {

            new Timer(DisplaySequenceState, sequence, 0, displayIntervalMs);
            new Timer(Save, this, 0, saveIntervalMs);

            foreach (var point in sequence.Sequence()) { }
        }

        private void DisplaySequenceState(object o)
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
                {
                    return new BinaryFormatter().Deserialize(stream) as Runner;
                }
            }

            return new Runner();
        }

        public static void Save(object runner)
        {
            using (var stream = File.OpenWrite(filename))
            {
                new BinaryFormatter().Serialize(stream, runner);
            }
        }
    }
}
