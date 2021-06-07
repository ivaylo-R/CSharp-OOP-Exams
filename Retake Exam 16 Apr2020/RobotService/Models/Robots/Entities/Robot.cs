using RobotService.Models.Robots.Contracts;
using RobotService.Utilities.Messages;
using System;

namespace RobotService.Models.Robots.Entities
{
    public abstract class Robot : IRobot
    {
        private int happiness;
        private int energy;
        private int procedureTime;
        private string owner;
        private bool isBought;
        private bool isChipped;
        private bool isChecked;

        protected Robot(string name, int energy, int happiness, int procedureTime)
        {
            this.Owner = "Service";
            this.Name = name;
            this.Energy = energy;
            this.Happiness = happiness;
            this.ProcedureTime = procedureTime;
            this.IsChipped = false;
            this.IsBought = false;
            this.IsChecked = false;
        }

        public string Name { get; }

        public int Happiness
        {
            get => this.happiness;
            set
            {
                if (value > 100 || value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidHappiness);
                }

                this.happiness = value;
            }
        }

        public int Energy
        {
            get
            {
                return this.energy;
            }
            set
            {
                if (value > 100 || value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidEnergy);
                }

                this.energy = value;
            }
        }
        public int ProcedureTime
        {
            get => this.procedureTime;
            set => this.procedureTime = value;
        }
        public string Owner
        {
            get => this.owner;
            set
            {
                this.owner = value;
            }
        }
        public bool IsBought
        {
            get => this.isBought;
            set => this.isBought = value;
        }
        public bool IsChipped
        {
            get => this.isChipped;
            set => this.isChipped = value;
        }
        public bool IsChecked
        {
            get => this.isChecked;
            set => this.isChecked = value;
        }

        public override string ToString()
        {
            return $" Robot type: {this.GetType().Name} - {this.Name} - Happiness: {this.Happiness} - Energy: {this.Energy}".ToString();
        }
    }
}
