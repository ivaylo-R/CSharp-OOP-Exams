namespace Minedraft.Entities.Providers
{
    public class PressureProvider : Provider
    {
        private const double ExtractedEnergyFromDeepEarth = 300;
        private const double IncreasingEnergy = 2;
        public PressureProvider(int iD, double energyOutput) 
            : base(iD,energyOutput)
        {
            this.Durability += ExtractedEnergyFromDeepEarth;
            this.EnergyOutput *= IncreasingEnergy;
        }
    }
}
