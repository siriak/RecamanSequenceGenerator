using System;
using System.Collections.Generic;
using System.Numerics;
using Newtonsoft.Json;

namespace RecamanSequenceFull
{
    [Serializable]
    internal class RecamanSequenceGenerator
    {
        [JsonIgnore]
        public BigInteger MinNotFound => Found.MinNotFound;
        [JsonIgnore]
        public BigInteger MaxFound => Found.MaxFound;
        [JsonIgnore]
        public BigInteger CountFound => Found.CountFound;
        [JsonIgnore]
        public double Fillness => Found.Fillness;

        public BigInteger Current;
        public BigInteger Step;

        public SmartList Found = new SmartList();

        public IEnumerable<BigInteger> GetSequence()
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
