using RobotService.Models.Robots.Contracts;
using RobotService.Utilities.Messages;
using System;

namespace RobotService.Models.Procedures.Entities
{
    public class Chip : Procedure
    {
        private const int Reduce_Happines_Value = 5;
        public override void DoService(IRobot robot, int procedureTime)
        {
            base.DoService(robot, procedureTime);


            if (robot.IsChipped)
            {
                string excMsg = string.Format(ExceptionMessages.AlreadyChipped,robot.Name);
                throw new ArgumentException(excMsg);
            }

            robot.Happiness -= Reduce_Happines_Value;
            robot.ProcedureTime -= procedureTime;
            robot.IsChipped = true;

        }

    }
}
