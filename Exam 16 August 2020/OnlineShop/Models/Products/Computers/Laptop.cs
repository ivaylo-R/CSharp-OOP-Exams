namespace OnlineShop.Models.Products.Computers
{
    public class Laptop : Computer
    {
        private const double OverallPerfomance_Value = 10;

        public Laptop(int id, string manufacturer, string model, decimal price) : 
            base(id, manufacturer, model, price, OverallPerfomance_Value)
        {
            
        }

       
    }
}
