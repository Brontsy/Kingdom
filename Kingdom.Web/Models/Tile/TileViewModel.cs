using Kingdom.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kingdom.Web.Models.Tile
{
    public class TileViewModel
    {
        public int Id { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int IsoX { get; set; }

        public int IsoY { get; set; }

        public int ZIndex { get; set; }

        public string Type { get; set; }

        public TileViewModel(IRegion region, ITile tile, int zoomLevel = 1)
        {
            this.Id = tile.Id;
            this.X = tile.Position.X;
            this.Y = tile.Position.Y;

            IIsoCoordinate isoCoordinate = tile.Position.ToIsoCoordinate(region);

            this.IsoX = isoCoordinate.X * zoomLevel;
            this.IsoY = isoCoordinate.Y * zoomLevel;

            this.ZIndex = 1000 + this.X + this.Y;

            this.Type = tile.Type.ToString();
        }
    }
}