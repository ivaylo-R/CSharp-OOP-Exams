using RobotService.Models.Procedures.Contracts;
using RobotService.Models.Robots.Contracts;
using RobotService.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace RobotService.Models.Procedures.Entities
{
    public abstract class Procedure : IProcedure
    {
        protected IList<IRobot> Robots { get; }

        public Procedure()
        {
            this.Robots = new List<IRobot>();
        }

        public virtual void DoService(IRobot robot, int procedureTime)
        {

            if (robot.ProcedureTime < procedureTime)
            {
                string exceptionMessage = ExceptionMessages.InsufficientProcedureTime;
                throw new ArgumentException(exceptionMessage);

            }

            this.Robots.Add(robot);
        }

        public string History()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{this.GetType().Name}");
            foreach (var robot in this.Robots)
            {
                sb.AppendLine(robot.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
