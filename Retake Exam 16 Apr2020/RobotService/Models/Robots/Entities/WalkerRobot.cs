namespace RobotService.Models.Robots.Entities
{
    public class WalkerRobot : Robot
    {
        public WalkerRobot(string name, int energy, int happiness, int procedureTime) 
            : base(name, energy, happiness, procedureTime)
        {
        }
    }
}
