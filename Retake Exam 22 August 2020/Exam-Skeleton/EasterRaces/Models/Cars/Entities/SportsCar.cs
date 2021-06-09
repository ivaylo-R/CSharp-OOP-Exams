using EasterRaces.Utilities.Messages;
using System;

namespace EasterRaces.Models.Cars.Entities
{
    public class SportsCar : Car
    {
        private const double CC = 3000;
        private const int minHP = 250;
        private const int maxHP = 450;
        private int horsePower;
        public SportsCar(string model, int horsePower) 
            : base(model, horsePower)
        {
            this.CubicCentimeters = CC;
        }

        

        public override int HorsePower
        {
            get { return this.horsePower; }
            protected set
            {
                if (value < minHP || value > maxHP)
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
