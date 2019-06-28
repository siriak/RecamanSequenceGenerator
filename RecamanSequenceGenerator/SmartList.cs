using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace RecamanSequenceGenerator
{
    [Serializable]
    public class SmartList
    {
        private readonly Dictionary<BigInteger, BigInteger> startToEnd = new Dictionary<BigInteger, BigInteger>();
        private readonly Dictionary<BigInteger, BigInteger> endToStart = new Dictionary<BigInteger, BigInteger>();

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

            var lowValue = value - 1;
            var highValue = value + 1;
            var hasLow = endToStart.ContainsKey(lowValue);
            var hasHigh = startToEnd.ContainsKey(highValue);

            if (hasLow && hasHigh)
            {
                var low = endToStart[lowValue];
                var high = startToEnd[highValue];
                Remove(low, lowValue);
                Remove(highValue, high);

                Add(low, high);

                if (MinNotFound == value)
                {
                    MinNotFound = high + 1;
                }
            }
            else if (hasLow)
            {
                var low = endToStart[lowValue];
                Remove(low, lowValue);

                Add(low, value);

                if (MinNotFound == value)
                {
                    MinNotFound = highValue;
                }
            }
            else if (hasHigh)
            {
                var high = startToEnd[value + 1];
                Remove(value + 1, high);

                Add(value, high);
            }
            else
            {
                Add(value, value);

                if (MinNotFound == value)
                {
                    MinNotFound = highValue;
                }
            }

            return true;
        }

        public bool Contains(BigInteger value)
        {
            if (value > MaxFound || endToStart.Count == 0)
            {
                return false;
            }

            if (endToStart.ContainsKey(value) || startToEnd.ContainsKey(value))
            {
                return true;
            }

            var i = 1;
            while (true)
            {
                var t = value - i;
                if (startToEnd.ContainsKey(t))
                {
                    return startToEnd[t] > value;
                }

                if (endToStart.ContainsKey(t))
                {
                    return false;
                }

                t = value + i;
                if (endToStart.ContainsKey(t))
                {
                    return endToStart[t] < value;
                }

                if (startToEnd.ContainsKey(t))
                {
                    return false;
                }

                i++;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static BigInteger Max(BigInteger left, BigInteger right) => left > right ? left : right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Remove(BigInteger start, BigInteger end)
        {
            endToStart.Remove(end);
            startToEnd.Remove(start);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Add(BigInteger start, BigInteger end)
        {
            endToStart.Add(end, start);
            startToEnd.Add(start, end);
        }
    }
}
