﻿using Kingdom.Core.Enums.Tiles;
using Kingdom.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdom.Entities.Tiles
{
    public class Stone : ITile
    {
        public int Id { get; set; }

        public int RegionId { get; set; }

        public I2dPosition Position { get; set; }

        public TileType Type
        {
            get { return TileType.Stone; }
        }

        public IBuilding Building { get; set; }

        public IList<IUnit> Units { get; set; }

        public Stone(int regionId, I2dPosition position)
        {
            this.RegionId = regionId;
            this.Position = position;
        }

        public void AddBuilding(IBuilding building)
        {
            this.Building = building;
        }
    }
}
