using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdom.Core.Interfaces.Command
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        ICommandResult Handle(TCommand command);
    }
}
