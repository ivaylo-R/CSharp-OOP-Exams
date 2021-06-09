using EasterRaces.Models.Cars.Contracts;
using System.Collections.Generic;

namespace EasterRaces.Repositories.Entities
{
    class CarRepository : Repository<ICar>
    {
        private readonly ICollection<ICar> carz;

        public CarRepository()
        {
            carz = new List<ICar>();
        }
    }
}
