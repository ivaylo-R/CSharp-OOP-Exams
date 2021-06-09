using NUnit.Framework;
using System;
using TheRace;

namespace TheRace.Tests
{
    public class RaceEntryTests
    {
        private RaceEntry race;
        private UnitCar car;
        private UnitCar car1;
        private UnitCar car2;
        private UnitDriver driver;
        private UnitDriver driver1;
        private UnitDriver driver2;

        [SetUp]
        public void Setup()
        {
            car = new UnitCar("test", 250, 2000);
            car1 = new UnitCar("pejo", 250, 2000);
            car2 = new UnitCar("Bentley", 250, 2000);
            driver = new UnitDriver("LeMan", car);
            driver1 = new UnitDriver("Mitko", car1);
            driver2 = new UnitDriver("Bran", car2);
            race = new RaceEntry();
        }

        [Test]
        public void NameOfTheDriverCannotBeNull()
        {
            Assert.Throws<ArgumentNullException>(() => driver = new UnitDriver(null, car));
        }

        [Test]
        public void RacesCountShouldWorkCorrectAsExpectd()
        {
            race.AddDriver(driver);

            int expCount = 1;

            Assert.IsTrue(race.Counter == expCount);
        }

        [Test]
        public void RaceWithIncorrectDrvrShoudlReturnExc()
        {
            Assert.That(() => race.AddDriver(null), Throws.InvalidOperationException);
        }

        [Test]
        public void ListOfRacesShouldHaveOnlyUniqueDriver()
        {
            race.AddDriver(driver);
            Assert.That(() => race.AddDriver(driver), Throws.InvalidOperationException);
        }

        [Test]
        public void AverageHPShouldReturnCorrecrResult()
        {
            race.AddDriver(driver);
            Assert.That(() => race.CalculateAverageHorsePower(), Throws.InvalidOperationException);

            race.AddDriver(driver2);
            race.AddDriver(driver1);
            double expectedAverageHP = 250;
            double actualAverageHP =race.CalculateAverageHorsePower();
            Assert.AreEqual(expectedAverageHP,actualAverageHP);
        }


    }
}