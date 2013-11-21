using Kingdom.Core.Enums.Buildings;
using Kingdom.Core.Interfaces;
using Kingdom.Core.Interfaces.Entities;
using Kingdom.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdom.Core.Services
{
    internal class BuildingService : IBuildingService
    {
        private IBuildingRepository _buildingRepository;
        private ITileService _tileService;

        public BuildingService(ITileService tileService, IBuildingRepository buildingRepository)
        {
            this._tileService = tileService;
            this._buildingRepository = buildingRepository;
        }

        public void Add(int regionId, int x, int y, BuildingType type)
        {
            IBuilding building = null;// this._buildingRepository.Get(type);
            ITile tile = this._tileService.GetTile(regionId, x, y );

            tile.AddBuilding(building);

            this._tileService.SaveTile(tile);
        }
    }
}
