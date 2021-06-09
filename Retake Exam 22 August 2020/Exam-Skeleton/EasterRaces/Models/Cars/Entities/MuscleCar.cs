using EasterRaces.Utilities.Messages;
using System;

namespace EasterRaces.Models.Cars.Entities
{
    public class MuscleCar : Car
    {
        private const double CC = 5000;
        protected const int minHp = 400;
        private const int maxHp = 600;
        private int horsePower;

        public MuscleCar(string model, int horsePower)
            : base(model, horsePower)
        {
            this.CubicCentimeters = CC;
        }

        public override int HorsePower
        {
            get => this.horsePower;
            protected set
            {
                if (value < minHp || value > maxHp)
                {
                    string exc = string.Format(ExceptionMessages.InvalidHorsePower, value);
                    throw new ArgumentException(exc);
                }

                this.horsePower = value;
            }
        }

        public override double CubicCentimeters { get; }

    }
}
