namespace OnlineShop.Models.Products.Components
{
    public abstract class Component : Product, IComponent
    {
        protected Component(int id, string manufacturer,
            string model, decimal price, double overallPerformance, int generation)
            : base(id, manufacturer, model, price, overallPerformance)
        {
            this.Generation = generation;
        }

        public int Generation { get; private set; }

        public override string ToString()
        => $"Overall Performance: {this.OverallPerformance}. " +
            $"Price: {this.Price} - {this.GetType().Name}:" +
            $" {this.Manufacturer} {this.Model} (Id: {this.Id}) Generation: {this.Generation}";
    }
}
