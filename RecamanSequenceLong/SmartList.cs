using System;
using System.Collections.Generic;
using System.Linq;

namespace RecamanSequenceLong
{
    [Serializable]
    internal class SmartList
    {
        private readonly List<Tuple<long, long>> list = new List<Tuple<long, long>>();

        public long MinNotInList { get; private set; }
        public long MaxInList { get; private set; }
        public long CountInList { get; private set; }

        public double Fillness => 1d * CountInList / (MaxInList + 1);

        public bool Add(long value)
        {
            if (value < MinNotInList || Contains(value))
            {
                return false;
            }

            CountInList++;
            MaxInList = Max(MaxInList, value);

            var low = list.FirstOrDefault(x => x.Item2 == value - 1);
            var high = list.FirstOrDefault(x => x.Item1 == value + 1);

            if (low != null && high != null)
            {
                list.Remove(low);
                list.Remove(high);

                list.Add(new Tuple<long, long>(low.Item1, high.Item2));

                if (MinNotInList == value)
                {
                    MinNotInList = high.Item2 + 1;
                }
            }
            else if (low != null)
            {
                list.Remove(low);

                list.Add(new Tuple<long, long>(low.Item1, value));

                if (MinNotInList == value)
                {
                    MinNotInList = value + 1;
                }
            }
            else if (high != null)
            {
                list.Remove(high);

                list.Add(new Tuple<long, long>(value, high.Item2));
            }
            else
            {
                list.Add(new Tuple<long, long>(value, value));

                if (MinNotInList == value)
                {
                    MinNotInList = value + 1;
                }
            }

            return true;
        }

        public bool Contains(long value) => list.Any(x => x.Item2 >= value && x.Item1 <= value);

        private long Max(long left, long right) => left > right ? left : right;
    }
}