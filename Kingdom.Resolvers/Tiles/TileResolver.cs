using Kingdom.Core.Enums.Tiles;
using Kingdom.Core.Interfaces.Entities;
using Kingdom.Core.Interfaces.Resolvers.Tiles;
using Kingdom.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdom.Resolvers.Tiles
{
    internal class TileResolver : IAggregateTileResolver
    {
        private Dictionary<TileType, ITileResolver> _resolvers;

        public TileResolver(IList<ITileResolver> resolvers)
        {
            this._resolvers = new Dictionary<TileType, ITileResolver>();

            foreach (ITileResolver resolver in resolvers)
            {
                this._resolvers.Add(resolver.Type, resolver);
            }
        }

        public ITile Resolve(TileType type, int regionId, I2dPosition position)
        {
            return this._resolvers[type].Resolve(regionId, position);
        }


        public ITile Resolve(TileType type, int regionId, int x, int y)
        {
            return this._resolvers[type].Resolve(regionId, new TwodPosition(x, y));
        }
    }
}
