using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace RecamanSequenceBigInt
{
    [Serializable]
    internal class SmartList
    {
        private readonly List<Tuple<BigInteger, BigInteger>> list = new List<Tuple<BigInteger, BigInteger>>();

        public BigInteger MinNotInList { get; private set; }
        public BigInteger MaxInList { get; private set; }
        public BigInteger CountInList { get; private set; }

        public double Fillness => Math.Exp(BigInteger.Log(CountInList) - BigInteger.Log(MaxInList + 1));

        public bool Add(BigInteger value)
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

                list.Add(new Tuple<BigInteger, BigInteger>(low.Item1, high.Item2));

                if (MinNotInList == value)
                {
                    MinNotInList = high.Item2 + 1;
                }
            }
            else if (low != null)
            {
                list.Remove(low);

                list.Add(new Tuple<BigInteger, BigInteger>(low.Item1, value));

                if (MinNotInList == value)
                {
                    MinNotInList = value + 1;
                }
            }
            else if (high != null)
            {
                list.Remove(high);

                list.Add(new Tuple<BigInteger, BigInteger>(value, high.Item2));
            }
            else
            {
                list.Add(new Tuple<BigInteger, BigInteger>(value, value));

                if (MinNotInList == value)
                {
                    MinNotInList = value + 1;
                }
            }

            return true;
        }

        public bool Contains(BigInteger value) => list.Any(x => x.Item2 >= value && x.Item1 <= value);

        private BigInteger Max(BigInteger left, BigInteger right) => left > right ? left : right;
    }
}
