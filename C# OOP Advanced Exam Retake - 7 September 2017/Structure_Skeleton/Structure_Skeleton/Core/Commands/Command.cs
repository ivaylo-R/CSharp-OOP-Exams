using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minedraft.Core.Commands
{
    public abstract class Command : ICommand
    {
        protected DraftManager DraftManager;
        public Command(params string [] args)
        {
            this.Arguments = args.ToList();
        }
        public IList<string> Arguments { get; }

        public abstract string Execute();
        
    }
}
