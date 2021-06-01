using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minedraft.Entities.Providers
{
    public class StandartProvider : Provider
    {
        public StandartProvider(int ID, double energyOutput) 
            : base(ID,energyOutput)
        {
        }
    }
}
