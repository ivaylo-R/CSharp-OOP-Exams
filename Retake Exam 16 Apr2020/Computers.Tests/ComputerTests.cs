namespace Computers.Tests
{
    using NUnit.Framework;
    using System;

    public class ComputerTests
    {
        private Computer computer;
        private Part part;

        [SetUp]
        public void Setup()
        {
            computer = new Computer("Macintosh");
            part = new Part("Ram", 205);
        }

        [Test]
        public void ComputerNameShoudBeCorrect()
        {
            Assert.Throws<ArgumentNullException>(() => computer = new Computer(null));
            Assert.Throws<ArgumentNullException>(() => computer = new Computer(" "));

            computer = new Computer("HP");
            Assert.AreEqual(computer.Name, "HP");
                     
        }

        [Test]
        public void PartsShoudlBeAddedToComputerSuccesfully()
        {
            computer.AddPart(part);

            Assert.That(computer.GetPart(part.Name) == part);

        }

        [Test]
        public void PartShouldBeCorrect()
        {
            Assert.That(() => computer.AddPart(null), Throws.InvalidOperationException);
        }

        [Test]
        public void ListOfPartsShouldWorkCorrect()
        {
            computer.AddPart(part);
            int expectedCount = 1;
            int actualCount = computer.Parts.Count;
            Assert.AreEqual(actualCount, expectedCount);

            expectedCount = 0;
            computer.RemovePart(part);
            actualCount = computer.Parts.Count;
            Assert.AreEqual(actualCount, expectedCount);
        }

        [Test]
        public void TotalPriceOfPartsShouldWorkCorrect()
        {
            computer.AddPart(new Part("keyboard", 1));
            computer.AddPart(new Part("mouse", 5));
            decimal actualSumOfParts = computer.TotalPrice;
            decimal expectedSumOfParts = 6;
            Assert.AreEqual(expectedSumOfParts, actualSumOfParts);
        }
    }
}