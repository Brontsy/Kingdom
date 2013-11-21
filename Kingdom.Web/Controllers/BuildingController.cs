using Kingdom.Core.Enums.Buildings;
using Kingdom.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kingdom.Web.Controllers
{
    public class BuildingController : Controller
    {
        private IBuildingService _buildingService;

        public BuildingController(IBuildingService buildingService)
        {
            this._buildingService = buildingService;
        }

        public ActionResult Add(int regionId, int x, int y, BuildingType type)
        {
            this._buildingService.Add(regionId, x, y, type);

            return this.RedirectToAction("Index", "Region");
        }

    }
}
