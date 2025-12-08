using NUnit.Framework;
using System.IO;
using System.Linq;
using System.Numerics;

namespace RecamanSequenceGenerator.Tests
{
    public static class RecamanSequenceGeneratorTests
    {
        [Test]
        public static void FirstElementsCorrect()
        {
            var expectedSequence = GetExpectedSequenceFirstTerms();
            var actualSequence = new RecamanSequenceGenerator().GetSequence().Take(expectedSequence.Length).ToArray();
            Assert.That(actualSequence, Is.EqualTo(expectedSequence));
        }

        private static BigInteger[] GetExpectedSequenceFirstTerms() => File.ReadAllLines(@"../../../FirstMillionRealTerms.txt").Select(BigInteger.Parse).ToArray();
    }
}
