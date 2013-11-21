using Kingdom.Core.Enums.Tiles;
using Kingdom.Core.Interfaces;
using Kingdom.Core.Interfaces.Builders;
using Kingdom.Core.Interfaces.Entities;
using Kingdom.Core.Interfaces.Regions;
using Kingdom.Core.Interfaces.Resolvers.Tiles;
using Kingdom.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdom.Builders
{
    internal class RiverBuilder : IRiverBuilder
    {
        private enum Direction { North, East, South, West };

        private IAggregateTileResolver _tileResolver;
        private ITileService _tileService;

        private IList<River> _rivers;

        public RiverBuilder(IAggregateTileResolver tileResolver, ITileService tileService)
        {
            this._tileResolver = tileResolver;
            this._tileService = tileService;

            this._rivers = new List<River>();
            this.CreateRivers();
        }

        public void BuildRiver(IList<IRegion> regions)
        {
            IRegion firstRegion = regions.First();

            int firstTile = new Random().Next(0, 9);

            Position position = new Position(0, firstTile);

            this.ConvertToWaterTile(firstRegion, 0, firstTile);

            // "east", "north", "south"

            ITile[,] tiles = this.ConvertRegions(regions);

            foreach(River river in this._rivers)
            {
                foreach (Direction direction in river.Directions)
                {
                    if (direction == Direction.North)
                    {
                        position.Y = position.Y - 1;
                    }

                    if (direction == Direction.South)
                    {
                        position.Y = position.Y + 1;
                    }

                    if (direction == Direction.East)
                    {
                        position.X = position.X + 1;
                    }

                    if (direction == Direction.West)
                    {
                        position.X = position.X - 1;
                    }

                    //if (position.X >= 0 && position.X < tiles.Length && position.Y >= 0 && position.Y < tiles.Length)
                    //{
                        IRegion region = this.GetRegion(regions, position.X, position.Y);
                        ITile tile = tiles[position.X, position.Y];

                        this.ConvertToWaterTile(region, tile.Position.X, tile.Position.Y);
                    //}
                }
            }
        }

        private void CreateRivers()
        {
            this._rivers = new List<River>();
            this._rivers.Add(new River() { Directions = new List<Direction>() { Direction.East, Direction.East, Direction.East, Direction.South, Direction.South, Direction.East, Direction.East, Direction.East, Direction.East, Direction.South, Direction.South, Direction.South, Direction.West, Direction.South, Direction.South, Direction.South, Direction.East, Direction.East, Direction.East, Direction.East, Direction.East } });
            this._rivers.Add(new River() { Directions = new List<Direction>() { Direction.East, Direction.East, Direction.East, Direction.South, Direction.South, Direction.East, Direction.East, Direction.East, Direction.East, Direction.South, Direction.South, Direction.South, Direction.West, Direction.South, Direction.South, Direction.South, Direction.East, Direction.East, Direction.East, Direction.East, Direction.East } });
        }

        private void ConvertToWaterTile(IRegion region, int x, int y)
        {
            x = x % 10;
            y = y % 10;

            ITile tile = _tileResolver.Resolve(TileType.Water, region.Id, x, y);
            tile.Id = region.Tiles[x, y].Id;

            region.Tiles[x, y] = tile;

            _tileService.SaveTile(tile);
        }

        private ITile[,] ConvertRegions(IList<IRegion> regions)
        {
            int arraySize = (int)Math.Sqrt(regions.Count) * 10;

            ITile[,] tiles = new ITile[arraySize, arraySize];


            foreach (IRegion region in regions)
            {
                foreach (ITile tile in region.Tiles)
                {
                    tiles[tile.Position.X, tile.Position.Y] = tile;
                }
            }

            return tiles;
        }

        
        private IRegion GetRegion(IList<IRegion> regions, int tileX, int tileY)
        {
            foreach (IRegion region in regions)
            {
                int x = region.Position.X * 10;
                int y = region.Position.Y * 10;

                if (tileX / 10 == region.Position.X && tileY / 10 == region.Position.Y)
                {
                    return region;
                }
            }

            return null;
        }

        static IEnumerable<int> GetRandoms(int min, int max)
        {
            var rand = new Random();
            while (true)
            {
                yield return rand.Next(min, max);
            }
        }

        private class River
        {
            public IList<Direction> Directions { get; set; }

            public River()
            {
                this.Directions = new List<Direction>();
            }
        }
    }
}
