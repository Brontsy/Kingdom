using Kingdom.Core.Interfaces.Repositories;
using Kingdom.Core.Sql.Repositories;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdom.Core.Sql.Ioc
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IRegionRepository>().To<RegionRepository>();
            this.Bind<IBuildingRepository>().To<BuildingRepository>();
            this.Bind<ITileRepository>().To<TileRepository>();
        }
    }
}
