using OnlineShop.Common.Constants;
using OnlineShop.Models;
using OnlineShop.Models.Products.Computers;
using OnlineShop.Models.Products.Peripherals;
using OnlineShop.Models.Products.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using OnlineShop.Common.Enums;
using System.Collections;

namespace OnlineShop.Core
{
    public class Controller : IController
    {
        private ICollection<IComputer> computers;
        private ICollection<IComponent> components;
        private ICollection<IPeripheral> peripherals;

        public Controller()
        {
            this.computers = new List<IComputer>();
            this.components = new List<IComponent>();
            this.peripherals = new List<IPeripheral>();
        }


        public string AddComponent(int computerId, int id, string componentType,
            string manufacturer, string model, decimal price, double overallPerformance, int generation)
        {

            if (this.components.Any(c => c.Id == id))
            {
                string exceptionMessage = ExceptionMessages.ExistingComponentId;
                throw new ArgumentException(exceptionMessage);
            }

            IComponent component = CreateComponent(computerId, id, componentType, manufacturer, model, price, overallPerformance, generation);

            IComputer computer = CheckIfComputerExist(computerId);

            this.components.Add(component);
            computer.AddComponent(component);

            return String.Format(SuccessMessages.AddedComponent, componentType, id, computer.Id);
        }


        public string AddComputer(string computerType, int id, string manufacturer, string model, decimal price)
        {
            if (this.computers.Any(c => c.Id == id))
            {
                throw new ArgumentException(ExceptionMessages.ExistingComputerId);
            }

            IComputer computer = CreateComputer(computerType, id, manufacturer, model, price);

            this.computers.Add(computer);

            return String.Format(SuccessMessages.AddedComputer, id);
        }



        public string AddPeripheral(int computerId, int id, string peripheralType, string manufacturer,
            string model, decimal price, double overallPerformance, string connectionType)
        {
            if (peripherals.Any(p => p.Id == id)) throw new ArgumentException(ExceptionMessages.ExistingPeripheralId);

            IPeripheral peripheral = CreatePeripheral(id, peripheralType, manufacturer, model, price, overallPerformance, connectionType);

            IComputer computer = CheckIfComputerExist(computerId);

            computer.AddPeripheral(peripheral);

            this.peripherals.Add(peripheral);

            return String.Format(SuccessMessages.AddedPeripheral, peripheralType, id, computerId);
        }



        public string BuyBest(decimal budget)
        {
            IComputer computer = this.computers.OrderByDescending(c => c.OverallPerformance).ThenByDescending(c => c.Price).FirstOrDefault(c => c.Price <= budget);

            if (computer == null)
            {
                string excMsg = string.Format(ExceptionMessages.CanNotBuyComputer, budget);
                throw new ArgumentException(excMsg);
            }

            string output = computer.ToString();
            this.computers.Remove(computer);

            return output;
        }

        public string BuyComputer(int id)
        {
            IComputer computer = CheckIfComputerExist(id);

            string output = computer.ToString();

            computers.Remove(computer);

            return output;
        }

        public string GetComputerData(int id)
        {
            IComputer comp = CheckIfComputerExist(id);

            return comp.ToString();
        }

        public string RemoveComponent(string componentType, int computerId)
        {
            IComputer computer = CheckIfComputerExist(computerId);

            IComponent component = computer.Components.FirstOrDefault(c => c.GetType().Name == componentType);

            computer.RemoveComponent(componentType);
            this.components.Remove(component);

            return string.Format(SuccessMessages.RemovedComponent, componentType, component.Id);
        }

        private IComputer CheckIfComputerExist(int id)
        {
            IComputer comp = computers.FirstOrDefault(c => c.Id == id);

            if (comp==null)
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }

            return comp;
        }

        public string RemovePeripheral(string peripheralType, int computerId)
        {
            IComputer computer = CheckIfComputerExist(computerId);

            IPeripheral peripheral = computer.Peripherals.FirstOrDefault(p => p.GetType().Name == peripheralType);

            computer.RemovePeripheral(peripheralType);
            this.peripherals.Remove(peripheral);

            return String.Format(SuccessMessages.RemovedPeripheral, peripheralType, peripheral.Id);
        }

        private IComponent CreateComponent
            (int computerId, int id, string componentType,
            string manufacturer, string model, decimal price, double overallPerformance, int generation)
        {
            Enum.TryParse(componentType, out ComponentType typeOfComponent);
            IComponent component = typeOfComponent switch
            {
                ComponentType.CentralProcessingUnit => new CentralProcessingUnit(id, manufacturer, model, price, overallPerformance, generation),
                ComponentType.Motherboard => new Motherboard(id, manufacturer, model, price, overallPerformance, generation),
                ComponentType.PowerSupply => new PowerSupply(id, manufacturer, model, price, overallPerformance, generation),
                ComponentType.RandomAccessMemory => new RandomAccessMemory(id, manufacturer, model, price, overallPerformance, generation),
                ComponentType.SolidStateDrive => new SolidStateDrive(id, manufacturer, model, price, overallPerformance, generation),
                ComponentType.VideoCard => new VideoCard(id, manufacturer, model, price, overallPerformance, generation),
                _ => throw new ArgumentException(ExceptionMessages.InvalidComponentType)
            };
            return component;
        }

        private IComputer CreateComputer(string computerType, int id, string manufacturer, string model, decimal price)
        {
            Enum.TryParse(computerType, out ComputerType typeOfComputer);

            IComputer comp = typeOfComputer switch
            {
                ComputerType.DesktopComputer => new DesktopComputer(id, manufacturer, model, price),
                ComputerType.Laptop => new Laptop(id, manufacturer, model, price),
                _ =>throw new ArgumentException(ExceptionMessages.InvalidComputerType)
            };

            return comp;
        }

        private IPeripheral CreatePeripheral(int id, string peripheralType, string manufacturer, string model, decimal price, double overallPerformance, string connectionType)
        {
            Enum.TryParse(peripheralType, out PeripheralType typeOfPeripheral);

            IPeripheral peripheral = typeOfPeripheral switch
            {
                PeripheralType.Headset => new Headset(id, manufacturer, model, price, overallPerformance, connectionType),
                PeripheralType.Keyboard => new Keyboard(id, manufacturer, model, price, overallPerformance, connectionType),
                PeripheralType.Monitor => new Monitor(id, manufacturer, model, price, overallPerformance, connectionType),
                PeripheralType.Mouse => new Mouse(id, manufacturer, model, price, overallPerformance, connectionType),
                _=> throw new ArgumentException(ExceptionMessages.InvalidPeripheralType)
            };

            return peripheral;
        }
    }
}
