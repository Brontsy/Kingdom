using Kingdom.Core.Enums.Tiles;
using Kingdom.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdom.Core.Interfaces.Resolvers.Tiles
{
    public interface IAggregateTileResolver
    {
        ITile Resolve(TileType tileType, int regionId, I2dPosition position);

        ITile Resolve(TileType tileType, int regionId, int x, int y);
    }
}
