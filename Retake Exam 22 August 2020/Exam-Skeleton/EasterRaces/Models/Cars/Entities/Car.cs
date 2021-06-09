
using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Utilities.Messages;
using System;

namespace EasterRaces.Models.Cars.Entities
{
    public abstract class Car : ICar
    {
        private string model;
        

        protected Car(string model, int horsePower)
        {
            this.Model = model;
            this.HorsePower = horsePower;
        }


        public string Model
        {
            get => this.model;
            protected set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 4)
                {
                    string excMsg = string.Format(ExceptionMessages.InvalidModel, value,4);
                    throw new ArgumentException(excMsg);
                }
                this.model = value;
            }
        }

        public abstract int HorsePower { get; protected set; }

        public abstract double CubicCentimeters { get;  }

        public double CalculateRacePoints(int laps)
            => this.CubicCentimeters / (this.HorsePower * laps);
    }
}
