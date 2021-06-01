using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minedraft.Entities.Providers
{
    public class SolarProvider : Provider
    {
        private const double Energy_From_Sun = 500;
        public SolarProvider(int iD, double energyOutput) 
            : base(iD,energyOutput)
        {
            this.Durability += Energy_From_Sun;
        }
    }
}
