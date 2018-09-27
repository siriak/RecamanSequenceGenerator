using System;
using System.Collections.Generic;
using System.Linq;

namespace RecamanSequenceLongJsonExport
{
    [Serializable]
    internal class SmartList
    {
        public List<Tuple<long, long>> FoundIntervals = new List<Tuple<long, long>>();

        public long MinNotFound;
        public long MaxFound;
        public long CountFound;

        public double Fillness => 1d * CountFound / (MaxFound + 1);

        public bool Add(long value)
        {
            if (value < MinNotFound || Contains(value))
            {
                return false;
            }

            CountFound++;
            MaxFound = Max(MaxFound, value);

            var low = FoundIntervals.FirstOrDefault(x => x.Item2 == value - 1);
            var high = FoundIntervals.FirstOrDefault(x => x.Item1 == value + 1);

            if (low != null && high != null)
            {
                FoundIntervals.Remove(low);
                FoundIntervals.Remove(high);

                FoundIntervals.Add(new Tuple<long, long>(low.Item1, high.Item2));

                if (MinNotFound == value)
                {
                    MinNotFound = high.Item2 + 1;
                }
            }
            else if (low != null)
            {
                FoundIntervals.Remove(low);

                FoundIntervals.Add(new Tuple<long, long>(low.Item1, value));

                if (MinNotFound == value)
                {
                    MinNotFound = value + 1;
                }
            }
            else if (high != null)
            {
                FoundIntervals.Remove(high);

                FoundIntervals.Add(new Tuple<long, long>(value, high.Item2));
            }
            else
            {
                FoundIntervals.Add(new Tuple<long, long>(value, value));

                if (MinNotFound == value)
                {
                    MinNotFound = value + 1;
                }
            }

            return true;
        }

        public bool Contains(long value) => FoundIntervals.Any(x => x.Item2 >= value && x.Item1 <= value);

        private long Max(long left, long right) => left > right ? left : right;
    }
}
