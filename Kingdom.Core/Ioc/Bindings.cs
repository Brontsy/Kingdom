using Kingdom.Core.Commands;
using Kingdom.Core.Interfaces;
using Kingdom.Core.Interfaces.Command;
using Kingdom.Core.Services;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Extensions.Conventions;

namespace Kingdom.Core.Ioc
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            this.Bind<ITileService>().To<TileService>();
            this.Bind<IRegionService>().To<RegionService>();
            this.Bind<IBuildingService>().To<BuildingService>();

            this.Bind<ICommandDispatcher>().To<CommandDispatcher>();

            //this.Bind(x => x.FromThisAssembly().SelectAllClasses().InheritedFrom(typeof(ICommandHandler<>)).BindAllInterfaces());

            this.Bind<ICommandHandler<AddBuildingCommand>>().To<AddBuildingCommandHandler>();
        }

    }
}
