using Kingdom.Core.Interfaces.Regions;
using Kingdom.Core.Interfaces.Resolvers.Tiles;
using Kingdom.Core.Sql.Resolvers;
using Kingdom.Resolvers.Tiles;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdom.Resolvers.Ioc
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IAggregateTileResolver>().To<TileResolver>();
            this.Bind<ITileResolver>().To<GrassTileResolver>();
            this.Bind<ITileResolver>().To<ForestTileResolver>();
            this.Bind<ITileResolver>().To<WaterTileResolver>();
            this.Bind<ITileResolver>().To<GoldTileResolver>();
            this.Bind<ITileResolver>().To<StoneTileResolver>();

            //this.Bind<IAggregateBuildingResolver>().To<BuildingResolver>();
            //this.Bind<IBuildingResolver>().To<TownhallResolver>();

            //this.Bind<IAggregateUnitResolver>().To<UnitResolver>();
            //this.Bind<IUnitResolver>().To<ArcherResolver>();

            this.Bind<IRegionResolver>().To<RegionResolver>();
        }
    }
}
