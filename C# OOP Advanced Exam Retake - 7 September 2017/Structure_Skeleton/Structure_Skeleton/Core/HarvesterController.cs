using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minedraft.Core
{
    public class HarvesterController : IHarvesterController
    {
        List<IHarvester> harvesters;
        private IEnergyRepository energyRepository;
        private IHarvesterFactory factory;

        public HarvesterController(IEnergyRepository energyRepository)
        {
            this.harvesters = new List<IHarvester>();
            this.factory = new HarvesterFactory();
            this.energyRepository = energyRepository;

        }
        public double OreProduced { get; protected set; }

        public string ChangeMode(string mode)
        {
            throw new NotImplementedException();
        }

        public string Produce()
        {
            throw new NotImplementedException();
        }

        public string Register(IList<string> args)
        {
            throw new NotImplementedException();
        }
    }
}
