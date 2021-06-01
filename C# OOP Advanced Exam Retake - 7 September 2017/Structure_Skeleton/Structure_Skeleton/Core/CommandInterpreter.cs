using System;
using System.Collections.Generic;

namespace Minedraft.Core
{
    public class CommandInterpreter : ICommandInterpreter
    {
        public IHarvesterController HarvesterController { get; }

        public IProviderController ProviderController { get; }

        public string ProcessCommand(IList<string> args)
        {
            throw new NotImplementedException();
        }
    }
}
