using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace RecamanSequenceLongJsonExport
{
    [Serializable]
    internal class RecamanSequence
    {
        [JsonIgnore]
        public long MinNotFound => Found.MinNotFound;
        [JsonIgnore]
        public long MaxFound => Found.MaxFound;
        [JsonIgnore]
        public long CountFound => Found.CountFound;
        [JsonIgnore]
        public double Fillness => Found.Fillness;

        public long Current;
        public long Step;

        public SmartList Found = new SmartList();

        public IEnumerable<long> Sequence()
        {
            while (true)
            {
                var next = Current - Step;
                if (!Found.Add(next))
                {
                    next = Current + Step;
                    Found.Add(next);
                }

                Current = next;
                Step++;

                yield return Current;
            }
        }
    }
}
