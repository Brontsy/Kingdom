using Kingdom.Core.Enums.Tiles;
using Kingdom.Core.Interfaces;
using Kingdom.Core.Interfaces.Entities;
using Kingdom.Core.Interfaces.Regions;
using Kingdom.Core.Interfaces.Repositories;
using Kingdom.Core.Interfaces.Resolvers.Tiles;
using Kingdom.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdom.Core.Services
{
    internal class RegionService : IRegionService
    {
        private IRegionRepository _regionRepository;
        private IRegionResolver _regionResolver;
        private IAggregateTileResolver _tileResolver;
        private ITileService _tileService;

        public RegionService(IRegionRepository regionRepository, IRegionResolver regionResolver, IAggregateTileResolver tileResolver, ITileService tileService)
        {
            _regionRepository = regionRepository;
            _regionResolver = regionResolver;
            _tileResolver = tileResolver;
            _tileService = tileService;
            
        }

        public IRegion GetRegion(int regionId)
        {
            return _regionRepository.GetRegion(regionId);
        }

        public IRegion GetRegion(int x, int y)
        {
            return _regionRepository.GetRegion(x, y);
        }

        public IList<IRegion> GetRegions()
        {
            return _regionRepository.GetRegions();
        }

        public IRegion SaveRegion(IRegion region)
        {
            return this._regionRepository.SaveRegion(region);
        }

        public IList<IRegion> GetRegions(int topLeftX, int topLeftY, int topRightX, int topRightY, int bottomLeftX, int bottomLeftY, int bottomRightX, int bottomRightY, IList<int> exclude)
        {
            IPosition topLeft = new Position(topLeftX, topLeftY);
            IPosition topRight = new Position(topRightX, topRightY);
            IPosition bottomLeft = new Position(bottomLeftX, bottomLeftY);
            IPosition bottomRight = new Position(bottomRightX, bottomRightY);

            return this._regionRepository.GetRegions(topLeft, topRight, bottomLeft, bottomRight, exclude);
        }

        public void CreateRegion(int size)
        {
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {

                }
            }
        }

        public void CreateRegions(int x, int y)
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    IRegion region = this._regionResolver.CreateRegion(i, j, 100);

                    region = this._regionRepository.SaveRegion(region);

                    int regionSize = (int)Math.Sqrt(100);
                    for (int xCol = 0; xCol < regionSize; xCol++)
                    {
                        for (int yCol = 0; yCol < regionSize; yCol++)
                        {
                            ITile tile = this._tileResolver.Resolve(TileType.Grass, region.Id, xCol, yCol);

                            this._tileService.SaveTile(tile);
                        }
                    }
                }
            }
        }



        public IList<IRegion> GetVisibleRegionPositions(int screenWidth, int screenHeight, int x, int y, IList<int> exclude)
        {
            I2dPosition topLeft = new IsoCoordinate(x, y).To2dPosition();
            I2dPosition topRight = new IsoCoordinate(x + screenWidth, y).To2dPosition();
            I2dPosition bottomLeft = new IsoCoordinate(x, y + screenHeight).To2dPosition();
            I2dPosition bottomRight = new IsoCoordinate(x + screenWidth, y + screenHeight).To2dPosition();

            IList<int> xList = new List<int>() { (int)topLeft.X / 10, (int)topRight.X / 10, (int)bottomLeft.X / 10, (int)bottomRight.X / 10 };
            IList<int> yList = new List<int>() { (int)topLeft.Y / 10, (int)topRight.Y / 10, (int)bottomLeft.Y / 10, (int)bottomRight.Y / 10 };

            int minX = xList.Min();
            int maxX = xList.Max();

            int minY = yList.Min();
            int maxY = yList.Max();


            return this._regionRepository.GetRegions(minX, maxX, minY, maxY, exclude);
        }


        public IList<int> GetVisibleRegionIds(int screenWidth, int screenHeight, int x, int y)
        {
            I2dPosition topLeft = new IsoCoordinate(x, y).To2dPosition();
            I2dPosition topRight = new IsoCoordinate(x + screenWidth, y).To2dPosition();
            I2dPosition bottomLeft = new IsoCoordinate(x, y + screenHeight).To2dPosition();
            I2dPosition bottomRight = new IsoCoordinate(x + screenWidth, y + screenHeight).To2dPosition();

            IList<int> xList = new List<int>() { (int)topLeft.X / 10, (int)topRight.X / 10, (int)bottomLeft.X / 10, (int)bottomRight.X / 10 };
            IList<int> yList = new List<int>() { (int)topLeft.Y / 10, (int)topRight.Y / 10, (int)bottomLeft.Y / 10, (int)bottomRight.Y / 10 };

            int minX = xList.Min();
            int maxX = xList.Max();

            int minY = yList.Min();
            int maxY = yList.Max();


            return this._regionRepository.GetRegionIds(minX, maxX, minY, maxY);
        }
    }
}
