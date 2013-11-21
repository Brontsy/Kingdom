using Kingdom.Core.Enums.Tiles;
using Kingdom.Core.Interfaces.Entities;
using Kingdom.Core.Interfaces.Resolvers.Tiles;
using Kingdom.Entities;
using Kingdom.Entities.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdom.Resolvers.Tiles
{
    internal class StoneTileResolver : ITileResolver
    {
        public TileType Type
        {
            get { return TileType.Stone; }
        }

        public ITile Resolve(int regionId, I2dPosition position)
        {
            return new Stone(regionId, position);
        }
    }
}
