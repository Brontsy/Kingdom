using Kingdom.Core.Enums.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdom.Core.Interfaces.Entities
{
    public interface ITile
    {
        int Id { get; set; }

        int RegionId { get; }
        
        I2dPosition Position { get; }

        TileType Type { get; }

        IBuilding Building { get; }

        IList<IUnit> Units { get; set; }

        void AddBuilding(IBuilding building);
    }
}
