using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Models.Cars.Entities;
namespace EasterRaces.Models.Cars
{
    public static class CarFactory
    {
        public static ICar CreaterCar(string type,string model,int hp)
        {
            if (type=="Muscle")
            {
                return new MuscleCar(model,hp);
            }
            return new SportsCar(model,hp);
        }
    }
}
