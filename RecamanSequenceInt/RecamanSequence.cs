using System;
using System.Collections.Generic;

namespace RecamanSequenceInt
{
    [Serializable]
    internal class RecamanSequence
    {
        public int MinNotFound => Found.MinNotInList;
        public int MaxFound => Found.MaxInList;
        public int CountFound => Found.CountInList;
        public double Fillness => Found.Fillness;

        public int Current { get; private set; }
        public int Step { get; private set; }

        private SmartList Found = new SmartList();

        public IEnumerable<int> Sequence()
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
