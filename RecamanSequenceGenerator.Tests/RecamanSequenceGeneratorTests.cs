using NUnit.Framework;
using System.Numerics;
using System.Linq;
using System.IO;

namespace RecamanSequenceGenerator.Tests
{
    public class RecamanSequenceGeneratorTests
    {
        [Test]
        public void FirstElementsCorrect()
        {
            var expectedSequence = GetExpectedSequenceFirstTerms();
            var actualSequence = new RecamanSequenceGenerator().GetSequence().Take(expectedSequence.Length);

            Assert.AreEqual(expectedSequence, actualSequence);
        }

        BigInteger[] GetExpectedSequenceFirstTerms() => File.ReadAllLines(@"..\..\..\FirstMillionRealTerms.txt").Select(BigInteger.Parse).ToArray();
    }
}
