using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingdom.Core.Interfaces.Entities;
using Kingdom.Core.Enums.Tiles;

namespace Kingdom.Core.Interfaces
{
    public interface ITileService
    {
        ITile GetTile(int regionId, int x, int y);

        void ChangeTileType(TileType type, int regionId, int x, int y);

        void SaveTile(ITile tile);
    }
}
