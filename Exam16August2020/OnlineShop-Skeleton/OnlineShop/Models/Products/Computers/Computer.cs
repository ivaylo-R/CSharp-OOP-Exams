using OnlineShop.Common.Constants;
using OnlineShop.Models.Products.Components;
using OnlineShop.Models.Products.Peripherals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineShop.Models.Products.Computers
{
    public abstract class Computer : Product, IComputer
    {
        private ICollection<IComponent> components;
        private ICollection<IPeripheral> peripherals;
        protected Computer(int id, string manufacturer,
            string model, decimal price, double overallPerformance)
            : base(id, manufacturer, model, price, overallPerformance)
        {
            this.components = new List<IComponent>();
            this.peripherals = new List<IPeripheral>();
        }

        public override double OverallPerformance
         => ReturnTheSumOfComputerOverallPerfomance();


        public override decimal Price
            => base.Price
                    + this.Components.Sum(c => c.Price)
                    + this.Peripherals.Sum(p => p.Price);
        private double ReturnTheSumOfComputerOverallPerfomance()
        {
            if (!this.Components.Any())
            {
                return base.OverallPerformance;
            }

            return base.OverallPerformance + this.Components.Average(ap => ap.OverallPerformance);

        }

        public IReadOnlyCollection<IComponent> Components => this.components.ToList().AsReadOnly();

        public IReadOnlyCollection<IPeripheral> Peripherals => this.peripherals.ToList().AsReadOnly();

        public void AddComponent(IComponent component)
        {
            if (this.Components.Any(c => c.GetType().Name == component.GetType().Name))
            {
                string componentType = component.GetType().Name;
                string computerType = this.GetType().Name;
                int id = this.Id;
                throw new ArgumentException(string.Format(ExceptionMessages.ExistingComponent, componentType, computerType, id));
            }
            this.components.Add(component);
        }

        public void AddPeripheral(IPeripheral peripheral)
        {
            if (this.Peripherals.Any(p => p.GetType().Name == peripheral.GetType().Name))
            {
                string exceptionMessage = String.Format(ExceptionMessages.ExistingPeripheral
                    , peripheral.GetType().Name, this.GetType().Name, this.Id);

                throw new ArgumentException(exceptionMessage);
            }

            this.peripherals.Add(peripheral);
        }

        public IComponent RemoveComponent(string componentType)
        {
            if (!this.Components.Any() || !this.Components.Any(c => c.GetType().Name == componentType))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.NotExistingComponent, componentType.GetType().Name
                    , this.GetType().Name, this.Id));
            }
            IComponent componentToRemove = this.components.FirstOrDefault(c => c.GetType().Name == componentType);
            this.components.Remove(componentToRemove);

            return componentToRemove;
        }

        public IPeripheral RemovePeripheral(string peripheralType)
        {
            if (!this.Peripherals.Any() || !this.Peripherals.Any(pe => pe.GetType().Name == peripheralType))
            {
                string exceptionMessage = string.Format(ExceptionMessages.NotExistingPeripheral, peripheralType
                    , this.GetType().Name, this.Id);

                throw new ArgumentException(exceptionMessage);
            }
            IPeripheral peripheralToRemove = this.peripherals.FirstOrDefault(pe => pe.GetType().Name == peripheralType);
            this.peripherals.Remove(peripheralToRemove);

            return peripheralToRemove;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            
            sb.AppendLine($" Components ({this.Components.Count}):");

            foreach (var component in this.Components)
            {
                sb.AppendLine($"  {component}");
            }

            double overrallP = this.peripherals.Any() ? this.Peripherals.Average(p => p.OverallPerformance) : 0;

            sb.AppendLine($" Peripherals ({this.Peripherals.Count}); " +
                $"Average Overall Performance ({overrallP:F2}):");

            foreach (var peripheral in this.Peripherals)
            {
                sb.AppendLine($"  {peripheral}");
            }

            return base.ToString() + Environment.NewLine + sb.ToString().TrimEnd();
        }
    }
}
