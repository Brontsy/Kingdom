using Kingdom.Core.Interfaces.Builders;
using Kingdom.Core.Interfaces.Regions;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdom.Builders.Ioc
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IRegionBuilder>().To<RegionBuilder>();
            this.Bind<IRiverBuilder>().To<RiverBuilder>();
            this.Bind<IResourceBuilder>().To<ForestBuilder>();
            this.Bind<IResourceBuilder>().To<GoldMineBuilder>();
            this.Bind<IResourceBuilder>().To<StoneBuilder>();
        }
    }
}
