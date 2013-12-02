using Kingdom.Core.Interfaces.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdom.Core.Commands
{
    public class AddBuildingCommandHandler : ICommandHandler<AddBuildingCommand>
    {
        public ICommandResult Handle(AddBuildingCommand command)
        {
            return null;
        }
    }
}
