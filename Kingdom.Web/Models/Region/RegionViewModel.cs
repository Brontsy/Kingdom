using Kingdom.Core.Interfaces.Entities;
using Kingdom.Web.Models.Position;
using Kingdom.Web.Models.Tile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kingdom.Web.Models.Region
{
    public class RegionViewModel
    {
        public int Id { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public TileViewModel[,] Tiles { get; set; }

        public IList<PositionViewModel> Neighbours { get; set; }

        public RegionViewModel(IRegion region, int zoomLevel = 1)
        {
            this.Id = region.Id;
            this.X = region.Position.X;
            this.Y = region.Position.Y;
            this.Tiles = new TileViewModel[10, 10];

            for (int i = 0; i < region.Tiles.GetLength(0); i++)
            {
                for (int j = 0; j < region.Tiles.GetLength(1); j++)
                {
                    this.Tiles[i, j] = new TileViewModel(region, region.Tiles[i, j], zoomLevel);
                }
            }

            this.Neighbours = new List<PositionViewModel>();
            if (this.X - 1 >= 0)
            {
                // left
                this.Neighbours.Add(new PositionViewModel(this.X - 1, this.Y));
                if (this.Y - 1 >= 0)
                {
                    // top left
                    this.Neighbours.Add(new PositionViewModel(this.X - 1, this.Y - 1));
                }
            }

            if (this.Y - 1 >= 0)
            {
                // top
                this.Neighbours.Add(new PositionViewModel(this.X, this.Y - 1));
            }

            // top right
            this.Neighbours.Add(new PositionViewModel(this.X + 1, this.Y - 1));
            // right
            this.Neighbours.Add(new PositionViewModel(this.X + 1, this.Y));
            // bottom right
            this.Neighbours.Add(new PositionViewModel(this.X + 1, this.Y + 1));
            // bottom keft
            this.Neighbours.Add(new PositionViewModel(this.X - 1, this.Y + 1));
            // bottom
            this.Neighbours.Add(new PositionViewModel(this.X, this.Y + 1));

        }
    }
}