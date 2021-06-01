namespace Minedraft.Core.Commands
{
    public class DayCommand : Command
    {
        public override string Execute()
        {
            return this.DraftManager.Day();
        }
    }
}
