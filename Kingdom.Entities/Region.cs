using Kingdom.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdom.Entities
{
    public class Region : IRegion
    {
        public int Id { get; set; }

        public IPosition Position { get; internal set; }

        public ITile[,] Tiles { get; set; }

        public Region(int x, int y)
        { 
            this.Position = new Position(x, y);
        }
    }
}
