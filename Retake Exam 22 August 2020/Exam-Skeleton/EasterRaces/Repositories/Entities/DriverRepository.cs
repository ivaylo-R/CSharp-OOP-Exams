
using EasterRaces.Models.Drivers.Contracts;
using System.Collections.Generic;

namespace EasterRaces.Repositories.Entities
{
    public class DriverRepository : Repository<IDriver>
    {
        private ICollection<IDriver> driverz;

        public DriverRepository()
        {
            this.driverz = new List<IDriver>();
        }

    }
}
