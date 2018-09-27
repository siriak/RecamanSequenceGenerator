using System;
using System.Collections.Generic;

namespace RecamanSequenceLong
{
    [Serializable]
    internal class RecamanSequence
    {
        public long MinNotFound => Found.MinNotInList;
        public long MaxFound => Found.MaxInList;
        public long CountFound => Found.CountInList;
        public double Fillness => Found.Fillness;

        public long Current { get; private set; }
        public long Step { get; private set; }

        private SmartList Found = new SmartList();

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
