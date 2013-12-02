using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdom.Core.Interfaces.Command
{
    public interface ICommandDispatcher
    {
        void Dispatch<TParameter>(TParameter command) where TParameter : ICommand;
    }
}
