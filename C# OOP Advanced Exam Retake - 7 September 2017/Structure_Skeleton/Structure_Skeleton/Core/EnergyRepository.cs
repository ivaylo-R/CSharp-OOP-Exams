using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minedraft.Core
{
    public class EnergyRepository : IEnergyRepository
    {
        private double energyStore;
        public double EnergyStored
        {
            get => this.energyStore;
            private set
            {
                this.energyStore = value;
            }
        }
        
        public void StoreEnergy(double energy)
        {
            this.EnergyStored += energy;
        }

        public bool TakeEnergy(double energyNeeded)
        {
            if (energyNeeded > this.EnergyStored)
            {
                return false;
            }

            this.EnergyStored -= energyNeeded;
            return true;
        }
    }
}
