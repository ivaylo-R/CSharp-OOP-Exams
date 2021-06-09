using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Utilities.Messages;
using System;
using System.Collections.Generic;

namespace EasterRaces.Models.Drivers.Entities
{
    public class Driver : IDriver
    {
        private bool canParticipate;
        private string name;
        public Driver(string name)
        {
            this.Name = name;
            this.CanParticipate = false;
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

        public ICar Car { get; private set; }

        public int NumberOfWins { get; private set; }

        public bool CanParticipate
        {
            get => this.canParticipate;
            private set
            {
                if (this.Car != null)
                {
                    this.canParticipate = true;
                }

            }
        }

        public void AddCar(ICar car)
        {
            if (car == null)
            {
                throw new ArgumentException(ExceptionMessages.CarInvalid);
            }
            this.Car = car;
        }

        public void WinRace()
            => this.NumberOfWins++;
    }
}
