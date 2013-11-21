using Kingdom.Core.Enums.Tiles;
using Kingdom.Core.Interfaces;
using Kingdom.Core.Interfaces.Builders;
using Kingdom.Core.Interfaces.Entities;
using Kingdom.Core.Interfaces.Regions;
using Kingdom.Core.Interfaces.Repositories;
using Kingdom.Core.Interfaces.Resolvers.Tiles;
using Kingdom.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdom.Builders
{
    internal class RegionBuilder : IRegionBuilder
    {
        private IRegionService _regionService;
        private IRegionResolver _regionResolver;
        private IAggregateTileResolver _tileResolver;
        private ITileService _tileService;
        private IRiverBuilder _riverBuilder;

        private IList<IResourceBuilder> _resourceBuilders;

        public RegionBuilder(IRegionService regionService, IRiverBuilder riverBuilder, IList<IResourceBuilder> resourceBuilders, IRegionResolver regionResolver, IAggregateTileResolver tileResolver, ITileService tileService)
        {
            _regionService = regionService;
            _regionResolver = regionResolver;
            _tileResolver = tileResolver;
            _tileService = tileService;
            _riverBuilder = riverBuilder;

            _resourceBuilders = resourceBuilders;
        }

        public IList<IRegion> BuildRegions(int x, int y, int size)
        {
            IList<IRegion> regions = new List<IRegion>();

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    IRegion region = this._regionResolver.CreateRegion(i, j, 100);

                    region = this._regionService.SaveRegion(region);

                    int regionSize = (int)Math.Sqrt(100);
                    for (int xCol = 0; xCol < regionSize; xCol++)
                    {
                        for (int yCol = 0; yCol < regionSize; yCol++)
                        {
                            ITile tile = this._tileResolver.Resolve(TileType.Grass, region.Id, xCol, yCol);

                            this._tileService.SaveTile(tile);
                        }
                    }

                    regions.Add(this._regionService.GetRegion(region.Id));
                }
            }

            this._riverBuilder.BuildRiver(regions);

            foreach (IResourceBuilder builder in this._resourceBuilders)
            {
                builder.BuildResources(regions);
            }

            //foreach (IRegion region in regions)
            //{
            //    IList<int> resourceTiles = new List<int>();

            //    resourceTiles.Add(new Random().Next(1, 5)); // gold
            //    resourceTiles.Add(new Random().Next(1, 5)); //stone
            //    resourceTiles.Add(new Random().Next(1, 5)); //forest

            //    if (resourceTiles.Sum() > 12)
            //    {
            //        int i = 0;
            //        while (resourceTiles.Sum() > 12)
            //        {
            //            resourceTiles[i]--;
            //            i = (i >= resourceTiles.Count ? 0 : i++);
            //        }
            //    }

            //    if (resourceTiles.Sum() < 12)
            //    {
            //        int i = 0;
            //        while (resourceTiles.Sum() < 12)
            //        {
            //            resourceTiles[i]++;
            //            i = (i >= resourceTiles.Count ? 0 : i++);
            //        }
            //    }

            //    // gold
            //    for (int i = 0; i < resourceTiles[0]; i++)
            //    {
            //        this.CreateTiles(region, TileType.Gold);
            //    }

            //    // stone
            //    for (int i = 0; i < resourceTiles[1]; i++)
            //    {
            //        this.CreateTiles(region, TileType.Stone);
            //    }

            //    // forest
            //    for (int i = 0; i < resourceTiles[2]; i++)
            //    {
            //        this.CreateTiles(region, TileType.Forest);
            //    }
            //}


            return regions;
        }

        private void CreateTiles(IRegion region, TileType type)
        {
            int xCol = new Random().Next(0, 9);
            int yCol = new Random().Next(0, 9);

            while (region.Tiles[xCol, yCol].Type != TileType.Grass)
            {
                xCol = new Random().Next(0, 9);
                yCol = new Random().Next(0, 9);
            }

            ITile tile = this._tileResolver.Resolve(type, region.Id, xCol, yCol);
            tile.Id = region.Tiles[xCol, yCol].Id;

            region.Tiles[xCol, yCol] = tile;

            this._tileService.SaveTile(tile);
        }
    }
}
