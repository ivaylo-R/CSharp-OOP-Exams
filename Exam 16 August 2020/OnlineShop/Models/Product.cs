using OnlineShop.Common.Constants;
using OnlineShop.Models.Products;
using System;

namespace OnlineShop.Models
{
    public abstract class Product : IProduct
    {
        private int id;
        private string manufacturer;
        private string model;
        private decimal price;
        private double overallPerfomance;

        protected Product(int id, string manufacturer, 
            string model, decimal price, double overallPerformance)
        {
            Id = id;
            Manufacturer = manufacturer;
            Model = model;
            Price = price;
            OverallPerformance = overallPerformance;
        }

        public int Id
        {
            get => this.id;

            private set
            {
                if (value >= 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidProductId);
                }

                this.id = value;
            }
        }

        public string Manufacturer
        {
            get => this.manufacturer;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidManufacturer);
                }

                this.manufacturer = value;
            }
        }

        public string Model
        {
            get => this.model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidModel);
                }
                this.model = value;
            }

        }


        public virtual decimal Price
        {
            get => this.price;

            protected set
            {
                if (value<=0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidPrice);
                }
                this.price = value;
            }
        }

        public virtual double OverallPerformance
        {
            get => this.overallPerfomance;

            protected set
            {
                if (value<=0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidOverallPerformance);

                }
                this.overallPerfomance = value;
            }
        }


        public override string ToString()
        {
            return $"Overall Performance: " +
                $"{this.OverallPerformance:F2}. Price: {this.Price:F2} - {this.GetType().Name}:" +
                $" {this.Manufacturer} {this.Model} (Id: {this.Id})";
        }
    }
}
