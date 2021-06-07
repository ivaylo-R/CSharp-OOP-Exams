using RobotService.Core.Contracts;
using RobotService.Models.Garages.Contracts;
using RobotService.Models.Robots.Contracts;
using RobotService.Utilities.Messages;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using RobotService.Models.Garages.Entities;
using RobotService.Models.Procedures.Contracts;
using RobotService.Models.Procedures.Entities;

namespace RobotService.Core
{
    public class Controller : IController
    {
        private IGarage garage;
        private IProcedure chip;
        private IProcedure techCheck;
        private IProcedure work;
        private IProcedure rest;
        private IProcedure charge;
        private IProcedure polish;

        public Controller()
        {
            garage = new Garage();
            this.chip = new Chip();
            this.techCheck = new TechCheck();
            this.work = new Work();
            this.rest = new Rest();
            this.charge = new Charge();
            this.polish = new Polish();
        }

        public string Charge(string robotName, int procedureTime)
        {
            IRobot robot = IsRobotExistInGarage(robotName);

            charge.DoService(robot, procedureTime);

            return String.Format(OutputMessages.ChipProcedure, robotName);
        }

        public string Chip(string robotName, int procedureTime)
        {
            IRobot robot = IsRobotExistInGarage(robotName);

            chip.DoService(robot, procedureTime);

            return String.Format(OutputMessages.ChipProcedure, robotName);
        }

        public string History(string procedureType)
        {
            
            IProcedure procedure = TakeCurrectProcedure(procedureType);
            return procedure.History();
        }

        private IProcedure TakeCurrectProcedure(string procedureType)
        {
            switch (procedureType)
            {
                case "Charge": return this.charge;
                case "Chip": return this.chip;
                case "Polish": return this.polish;
                case "Rest": return this.rest;
                case "TechCheck": return this.techCheck;
                case "Work": return this.work;
            }
            return null;
        }

        

        public string Manufacture(string robotType, string name, int energy, int happiness, int procedureTime)
        {
            Type typeOfRobot = Assembly.GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(t => t.Name == robotType);

            if (typeOfRobot == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.InvalidRobotType, robotType));
            }


            if (energy<0 || energy>100)
            {
                throw new ArgumentException(ExceptionMessages.InvalidEnergy);
            }

            if (happiness < 0 || happiness > 100)
            {
                throw new ArgumentException(ExceptionMessages.InvalidHappiness);
            }

            var paramsOfRobot = new object[4] { name, energy, happiness, procedureTime };
            IRobot robot = (IRobot)Activator.CreateInstance(typeOfRobot, paramsOfRobot);

            this.garage.Manufacture(robot);
            return string.Format(OutputMessages.RobotManufactured, name);

        }

        public string Polish(string robotName, int procedureTime)
        {
            IRobot robot = IsRobotExistInGarage(robotName);

            polish.DoService(robot, procedureTime);

            return String.Format(OutputMessages.ChipProcedure, robotName);
        }

        public string Rest(string robotName, int procedureTime)
        {
            IRobot robot = IsRobotExistInGarage(robotName);

            rest.DoService(robot, procedureTime);

            return String.Format(OutputMessages.RestProcedure, robotName);
        }

        public string Sell(string robotName, string ownerName)
        {
            IRobot robot = IsRobotExistInGarage(robotName);

            garage.Sell(robotName, ownerName);

            if (robot.IsChipped)
            {
                return string.Format(OutputMessages.SellChippedRobot, ownerName);
            }

            return string.Format(OutputMessages.SellNotChippedRobot, ownerName);
        }

        public string TechCheck(string robotName, int procedureTime)
        {
            IRobot robot = IsRobotExistInGarage(robotName);

            techCheck.DoService(robot, procedureTime);

            return string.Format(OutputMessages.TechCheckProcedure, robotName);
        }

        public string Work(string robotName, int procedureTime)
        {
            IRobot robot = IsRobotExistInGarage(robotName);

            work.DoService(robot, procedureTime);

            return String.Format(OutputMessages.WorkProcedure, robotName, procedureTime);
        }

        private IRobot IsRobotExistInGarage(string robotName)
        {
            if (!this.garage.Robots.ContainsKey(robotName))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.InexistingRobot, robotName));
            }
            else
            {
                return this.garage.Robots[robotName];
            }


        }
    }
}
