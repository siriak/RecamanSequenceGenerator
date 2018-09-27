using System;
using System.Collections.Generic;
using System.Numerics;

namespace RecamanSequenceBigInt
{
    [Serializable]
    internal class RecamanSequence
    {
        public BigInteger MinNotFound => Found.MinNotInList;
        public BigInteger MaxFound => Found.MaxInList;
        public BigInteger CountFound => Found.CountInList;
        public double Fillness => Found.Fillness;

        public BigInteger Current { get; private set; }
        public BigInteger Step { get; private set; }

        private SmartList Found = new SmartList();

        public IEnumerable<BigInteger> Sequence()
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
