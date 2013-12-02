﻿using Kingdom.Core.Interfaces.Command;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdom.Core.Commands
{
    internal class CommandDispatcher : ICommandDispatcher
    {
        private readonly IKernel _kernel;

        public CommandDispatcher(IKernel kernel)
        {
            if (kernel == null)
            {
                throw new ArgumentNullException("kernel");
            }
            _kernel = kernel;
        }

        public void Dispatch<TParameter>(TParameter command) where TParameter : ICommand
        {
            var handler = _kernel.Get<ICommandHandler<TParameter>>();

            handler.Handle(command);
        }

    }
}
