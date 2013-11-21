using Kingdom.Core.Enums.Tiles;
using Kingdom.Core.Interfaces;
using Kingdom.Core.Interfaces.Entities;
using Kingdom.Core.Interfaces.Repositories;
using Kingdom.Core.Interfaces.Resolvers.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdom.Core.Services
{
    internal class TileService : ITileService
    {
        private ITileRepository _tileRepository;
        private IAggregateTileResolver _tileResolver;

        public TileService(ITileRepository tileRepository, IAggregateTileResolver tileResolver)
        {
            this._tileRepository = tileRepository;
            this._tileResolver = tileResolver;
        }

        public ITile GetTile(int regionId, int x, int y)
        {
            return this._tileRepository.GetTile(regionId, x, y);
        }

        public void ChangeTileType(TileType type, int regionId, int x, int y)
        {
            ITile tile = this._tileRepository.GetTile(regionId, x, y);

            ITile newTile = this._tileResolver.Resolve(type, regionId, tile.Position);

            newTile.AddBuilding(tile.Building);
            newTile.Units = tile.Units;
            newTile.Id = tile.Id;

            //tile.Type = type;

            this.SaveTile(newTile);
        }

        public void SaveTile(ITile tile)
        {
            this._tileRepository.SaveTile(tile);
        }
    }
}
