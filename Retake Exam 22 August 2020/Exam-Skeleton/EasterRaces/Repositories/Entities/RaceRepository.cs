using EasterRaces.Models.Races.Contracts;
using System.Collections.Generic;

namespace EasterRaces.Repositories.Entities
{
    public class RaceRepository : Repository<IRace>
    {
        private ICollection<IRace> racez;
        public RaceRepository()
        {
            racez = new List<IRace>();
        }
    }
}
