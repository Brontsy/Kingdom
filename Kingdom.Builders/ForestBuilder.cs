using Kingdom.Common.Utils;
using Kingdom.Core.Enums.Tiles;
using Kingdom.Core.Interfaces;
using Kingdom.Core.Interfaces.Builders;
using Kingdom.Core.Interfaces.Entities;
using Kingdom.Core.Interfaces.Regions;
using Kingdom.Core.Interfaces.Resolvers.Tiles;
using Kingdom.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdom.Builders
{
    internal class ForestBuilder : IResourceBuilder
    {
        private IAggregateTileResolver _tileResolver;
        private ITileService _tileService;
        
        public ForestBuilder(IAggregateTileResolver tileResolver, ITileService tileService)
        {
            this._tileResolver = tileResolver;
            this._tileService = tileService;
        }

        public void BuildResources(IList<IRegion> regions)
        {
            foreach (IRegion region in regions)
            {
                bool cont = true;
                int added = 0;

                while (cont)
                {
                    int x = RandomUtil.Random(0, 9);
                    int y = RandomUtil.Random(0, 9);

                    if (region.Tiles[x, y].Type == TileType.Grass)
                    {
                        ITile tile = this._tileResolver.Resolve(TileType.Forest, region.Id, x, y);
                        tile.Id = region.Tiles[x, y].Id;

                        region.Tiles[x, y] = tile;

                        this._tileService.SaveTile(tile);

                        added++;
                        if (added == 3)
                        {
                            cont = false;
                        }
                    }
                }
            }
        }
    }
}
