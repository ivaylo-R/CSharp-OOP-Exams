

using EasterRaces.Core.Contracts;
using EasterRaces.Models.Cars;
using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Models.Cars.Entities;
using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Models.Drivers.Entities;
using EasterRaces.Models.Races.Contracts;
using EasterRaces.Models.Races.Entities;
using EasterRaces.Repositories.Contracts;
using EasterRaces.Repositories.Entities;
using EasterRaces.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EasterRaces.Core.Entities
{
    public class ChampionshipController : IChampionshipController
    {
        private CarRepository carRepo;
        private RaceRepository raceRepo;
        private DriverRepository driverRepo;
        private const int MinParticipants = 3;

        public ChampionshipController()
        {
            carRepo = new CarRepository();
            raceRepo = new RaceRepository();
            driverRepo = new DriverRepository();
        }


        public string AddCarToDriver(string driverName, string carModel)
        {
            if (!driverRepo.GetAll().Any(d=>d.Name==driverName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.DriverNotFound, driverName));
            }


            if (!carRepo.GetAll().Any(c=>c.Model==carModel))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.CarNotFound, carModel));
            }

            IDriver driver = driverRepo.GetAll().FirstOrDefault(d=>d.Name==driverName);
            ICar car = carRepo.GetAll().FirstOrDefault(c => c.Model == carModel);
            driver.AddCar(car);

            return string.Format(OutputMessages.CarAdded, driverName, carModel);
        }

        public string AddDriverToRace(string raceName, string driverName)
        {
            if (!raceRepo.GetAll().Any(r=>r.Name==raceName))
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.RaceNotFound, raceName));
            }

            if (!driverRepo.GetAll().Any(d=>d.Name==driverName))
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.DriverNotFound, driverName));
            }

            IDriver driver = driverRepo.GetAll().FirstOrDefault(x => x.Name == driverName);
            IRace race = raceRepo.GetAll().FirstOrDefault(x => x.Name == raceName);

            race.AddDriver(driver);
            return String.Format(OutputMessages.DriverAdded, driverName, raceName);
        }

        public string CreateCar(string type, string model, int horsePower)
        {
            if (this.carRepo.GetAll().Any(m => m.Model == model))
            {
                throw new ArgumentException(String.Format(ExceptionMessages.CarExists, model));
            }

            ICar car = GetTypeOfCar(type, model, horsePower);
            carRepo.Add(car);
            return String.Format(OutputMessages.CarCreated, car.GetType().Name, model);
        }

        private ICar GetTypeOfCar(string type, string model, int horsePower)
        {
            if (type == "Sports")
            {
                return new SportsCar(model, horsePower);
            }

            return new MuscleCar(model, horsePower); 
        }

        public string CreateDriver(string driverName)
        {
            if (this.driverRepo.GetAll().Any(d=>d.Name==driverName))
            {
                string msg = (string.Format(ExceptionMessages.DriversExists, driverName));
                throw new InvalidOperationException(msg);
            }

            IDriver driver = new Driver(driverName);
            driverRepo.Add(driver);
            return string.Format(OutputMessages.DriverCreated, driverName);
        }

        public string CreateRace(string name, int laps)
        {
            if (raceRepo.GetAll().Any(r=>r.Name==name))
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.RaceExists, name));
            }

            IRace race = new Race(name, laps);
            raceRepo.Add(race);
            string output = String.Format(OutputMessages.RaceCreated, name);
            return output;
        }

        public string StartRace(string raceName)
        {
            if (!raceRepo.GetAll().Any(r=>r.Name==raceName))
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.RaceNotFound, raceName));
            }

            IRace race = raceRepo.GetAll().First(r => r.Name == raceName);

            if (race.Drivers.Count < 3)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceInvalid,race.Name, MinParticipants));
            }
            int laps = raceRepo.GetAll().First(r => r.Name == raceName).Laps;

            StringBuilder sb = new StringBuilder();
            IDriver[] drivers = raceRepo.GetAll()
                 .First(r => r.Name == raceName).Drivers
                 .OrderByDescending(d => d.Car.CalculateRacePoints(laps))
                 .ToArray();

            raceRepo.Remove(race);

            IDriver firstDrivr = drivers[0];
            IDriver secondDrivr = drivers[1];
            IDriver thirdDrivr = drivers[2];

            sb.AppendLine(string.Format(OutputMessages.DriverFirstPosition, firstDrivr.Name, raceName));
            sb.AppendLine(string.Format(OutputMessages.DriverSecondPosition, secondDrivr.Name, raceName));
            sb.AppendLine(string.Format(OutputMessages.DriverThirdPosition, thirdDrivr.Name, raceName));

            return sb.ToString().TrimEnd();
        }
    }
}
