using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Models.Races.Contracts;
using EasterRaces.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EasterRaces.Models.Races.Entities
{
    public class Race : IRace
    {
        private string name;
        private int laps;
        private List<IDriver> drivers;

        public Race(string name,int laps)
        {
            this.drivers = new List<IDriver>();
            this.Name = name;
            this.Laps = laps;
        }

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 5)
                {
                    string exc = string.Format(ExceptionMessages.InvalidName, value, 5);
                    throw new ArgumentException(exc);
                }

                this.name = value;
            }
        }

        public int Laps
        {
            get => this.laps;
            private set
            {
                if (value<1)
                {
                    string exc = string.Format(ExceptionMessages.InvalidNumberOfLaps,1);
                }

                this.laps = value;
            }
            
        }

        public IReadOnlyCollection<IDriver> Drivers => this.drivers;

        public void AddDriver(IDriver driver)
        {
            if (driver==null)
            {
                throw new ArgumentException(ExceptionMessages.DriverInvalid);
            }

            if (driver.Car == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.DriverNotParticipate, driver.Name));
            }

            if (drivers.Any(d=>d.Name==driver.Name))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.DriverAlreadyAdded, driver.Name, this.Name));
            }

            this.drivers.Add(driver);
        }
    }
}
