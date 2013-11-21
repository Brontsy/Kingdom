using Kingdom.Core.Enums.Tiles;
using Kingdom.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdom.Core.Interfaces.Resolvers.Tiles
{
    public interface ITileResolver
    {
        /// <summary>
        /// The type of tile that this resolver can handle
        /// </summary>
        TileType Type { get; }

        ITile Resolve(int regionId, I2dPosition position);
    }
}
