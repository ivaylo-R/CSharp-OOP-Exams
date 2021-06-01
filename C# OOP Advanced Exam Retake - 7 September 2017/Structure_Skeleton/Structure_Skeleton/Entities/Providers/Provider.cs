public abstract class Provider : IProvider
{
    private const double DurabilyDefault = 1000;
    private ProviderController providerController;
    protected Provider(int iD,double energyOutput)
    {
        EnergyOutput = energyOutput;
        ID = iD;
        Durability = DurabilyDefault;

    }

    public double EnergyOutput { get; protected set; }

    public int ID { get; protected set; }

    public double Durability { get; protected set; }

    public void Broke()
    {
        throw new System.NotImplementedException();
    }

    public double Produce()
    {
        throw new System.NotImplementedException();
    }

    public void Repair(double val)
    {
        throw new System.NotImplementedException();
    }
}