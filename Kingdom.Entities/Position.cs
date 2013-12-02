using Kingdom.Common.Constants;
using Kingdom.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdom.Entities
{
    public class Position : IPosition
    {
        public int X { get; set; }

        public int Y { get; set; }

        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public void SetPosition(int x, int y)
        {
            this.X = x < 0 ? 0 : x;
            this.Y = y < 0 ? 0 : y;
        }
    }
    //test

    public class IsoCoordinate : Position, IIsoCoordinate
    {
        public IsoCoordinate(int x, int y) : base(x, y)
        {
        }

        public I2dCoordinate To2dCoordinate(IRegion region)
        {
            return this.To2dPosition().To2dCoordinate(region);
        }

        // screen offset X,Y need to be applied to the ISO Coordinate before converting to a 2d Position
        public I2dPosition To2dPosition()
        {
            int x = ((2 * this.Y + this.X) / 2);
            int y = ((2 * this.Y - this.X) / 2);

            I2dCoordinate coordinate = new TwodCoordinate(x, y);

            double twodX = coordinate.X / (TileConstants.TileWidth / 2);
            double twodY = coordinate.Y / (TileConstants.TileWidth / 2);

            return new TwodPosition((int)Math.Floor(twodX), (int)Math.Floor(twodY));
        }
    }
    
    // 2d Position: 0,0, 4,4, 3,1, 15,15 etc
    public class TwodPosition : Position, I2dPosition
    {
        public TwodPosition(int x, int y) : base(x, y)
        {
        }

        public IIsoCoordinate ToIsoCoordinate(IRegion region)
        {
            int posX = this.X * TileConstants.TileWidth / 2;
            int posY = this.Y * TileConstants.TileHeight / 2;

            int isoX = (posX - posY) - (TileConstants.TileWidth / 2);
            int isoY = ((posX + posY) / 2);

            return new IsoCoordinate(isoX, isoY);
        }

        public I2dCoordinate To2dCoordinate(IRegion region)
        {
            return new TwodCoordinate(this.X * TileConstants.TileWidth, this.Y * TileConstants.TileHeight);
        }
    }
    
    public class TwodCoordinate : Position, I2dCoordinate
    {
        public TwodCoordinate(int x, int y) : base(x, y)
        {
        }
    
        public IIsoCoordinate ToIsoCoordinate(IRegion region)
        {
 	        return this.To2dPosition().ToIsoCoordinate(region);
        }

        public I2dPosition To2dPosition()
        {
            double x = this.X / TileConstants.TileWidth;
            double y = this.Y / TileConstants.TileWidth;

            return new TwodPosition((int)Math.Floor(x), (int)Math.Floor(y));
        }
    }
}
