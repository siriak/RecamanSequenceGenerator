using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace RecamanSequenceFull
{
    [Serializable]
    internal class SmartList
    {
        public List<Tuple<BigInteger, BigInteger>> FoundIntervals = new List<Tuple<BigInteger, BigInteger>>();

        public BigInteger MinNotFound;
        public BigInteger MaxFound;
        public BigInteger CountFound;

        public double Fillness => Math.Exp(BigInteger.Log(CountFound) - BigInteger.Log(MaxFound + 1));

        public bool Add(BigInteger value)
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

                FoundIntervals.Add(new Tuple<BigInteger, BigInteger>(low.Item1, high.Item2));

                if (MinNotFound == value)
                {
                    MinNotFound = high.Item2 + 1;
                }
            }
            else if (low != null)
            {
                FoundIntervals.Remove(low);

                FoundIntervals.Add(new Tuple<BigInteger, BigInteger>(low.Item1, value));

                if (MinNotFound == value)
                {
                    MinNotFound = value + 1;
                }
            }
            else if (high != null)
            {
                FoundIntervals.Remove(high);

                FoundIntervals.Add(new Tuple<BigInteger, BigInteger>(value, high.Item2));
            }
            else
            {
                FoundIntervals.Add(new Tuple<BigInteger, BigInteger>(value, value));

                if (MinNotFound == value)
                {
                    MinNotFound = value + 1;
                }
            }

            return true;
        }

        public bool Contains(BigInteger value) => FoundIntervals.Any(x => x.Item2 >= value && x.Item1 <= value);

        private static BigInteger Max(BigInteger left, BigInteger right) => left > right ? left : right;
    }
}
