using Kingdom.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdom.Core.Interfaces.Repositories
{
    public interface ITileRepository
    {
        ITile GetTile(int regionId, int x, int y);

        ITile[,] GetTiles(IRegion region);

        void SaveTile(ITile tile);
    }
}
