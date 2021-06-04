using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Computers.Tests
{
    public class Tests
    {
        private Computer computer;
        private Computer computer2;
        private ICollection<Computer> computers;
        ComputerManager computerManager;
        [SetUp]
        public void Setup()
        {
            computer = new Computer("Asus", "ROG", 2899m);
            computers = new List<Computer>();
            computerManager = new ComputerManager();
        }

        [Test]
        public void ComputerManagerShouldReturnListOfComputersAsExpected()
        {

            CollectionAssert.IsEmpty(computerManager.Computers);
            int expectedCount = 0;
            Assert.That(computerManager.Count, Is.EqualTo(expectedCount));
        }

        [Test]
        public void AddComputerShouldWorkCorrectly()
        {
            computerManager.AddComputer(computer);
            int expectedCount = 1;
            Assert.IsTrue(computerManager.Computers.Any(c => c == computer));
            Assert.IsTrue(computerManager.Computers.Count == expectedCount);
        }

        [Test]
        public void AddComputerShouldThrowExcWhenNullValueIsTryingToAdd()
        => Assert.That(() => computerManager.AddComputer(null),
                Throws.ArgumentNullException);

        [Test]
        public void AddTheSameComputerShouldReturnExc()
        {
            computerManager.AddComputer(new Computer("Asus", "ROG", 2100));

            Assert.Throws<ArgumentException>(() => computerManager.AddComputer(computer));
        }

        [Test]
        public void RemoveMethodShouldWorkCorrectly()
        {
            //Arrange
            int expectedCount = 0;

            //Act
            computerManager.AddComputer(computer);
            computerManager.RemoveComputer(computer.Manufacturer,computer.Model);

            //Assert
            Assert.IsTrue(!computerManager.Computers.Any(c => c == computer));
            Assert.IsTrue(computerManager.Computers.Count==expectedCount);
        }

        [Test]
        public void RemoveShoudlThrowExceptionIfNullData()
        {
            //Act
            computerManager.AddComputer(computer);


            //Assert
            Assert.Throws<ArgumentNullException>(() => computerManager.RemoveComputer(null, null));
        }

        [Test]
        public void GetComputerShouldThrowExceptionIfIncorrectDataIsGiven()
        {
            computerManager.AddComputer(computer);
            Assert.That(() => computerManager.GetComputer(computer.Manufacturer, null), Throws.ArgumentNullException);
            Assert.That(() => computerManager.GetComputer(null, computer.Model), Throws.ArgumentNullException);
            Assert.That(() => computerManager.GetComputer(null, null), Throws.ArgumentNullException);
            Assert.That(() => computerManager.GetComputer("Acer","Nitro 5"), Throws.ArgumentException);
            Computer expectedComputer=computerManager.GetComputer(computer.Manufacturer, computer.Model);
            Assert.IsTrue(expectedComputer == computer);
        }

        [Test]
        public void GetComputerByManufacturerShouldReturnCollectionOfComputersAsAGivenCriteria()
        {
            //Act
            this.computerManager.AddComputer(computer);
            this.computers.Add(computer);

            //Assert
            CollectionAssert.AreEquivalent(computerManager.GetComputersByManufacturer(computer.Manufacturer), computers);
            CollectionAssert.IsEmpty(computerManager.GetComputersByManufacturer("Dell"));
        }

        [Test]
        public void GetComputerByManufacturerShouldThrowExcWhenNullObjectIsGiven()
        {
            Assert.Throws<ArgumentNullException>(() => computerManager.GetComputersByManufacturer(null));
        }
    }
}