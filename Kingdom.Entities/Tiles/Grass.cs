using Kingdom.Core.Enums.Tiles;
using Kingdom.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdom.Entities.Tiles
{
    public class Grass : ITile
    {
        public int Id { get; set; }

        public int RegionId { get; set; }

        public I2dPosition Position { get; set; }

        public TileType Type
        {
            get { return TileType.Grass; }
        }

        public IBuilding Building { get; set; }

        public IList<IUnit> Units { get; set; }

        public Grass(I2dPosition position)
        {
            this.Position = position;
        }

        public void AddBuilding(IBuilding building)
        {
            this.Building = building;
        }
    }
}
