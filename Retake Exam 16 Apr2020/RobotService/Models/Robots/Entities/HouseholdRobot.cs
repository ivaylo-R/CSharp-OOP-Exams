namespace RobotService.Models.Robots.Entities
{
    public class HouseholdRobot : Robot
    {
        public HouseholdRobot(string name, int energy, int happiness, int procedureTime) 
            : base(name, energy, happiness, procedureTime)
        {
        }
    }
}
