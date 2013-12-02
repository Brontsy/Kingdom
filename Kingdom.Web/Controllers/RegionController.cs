using Kingdom.Core.Commands;
using Kingdom.Core.Interfaces;
using Kingdom.Core.Interfaces.Command;
using Kingdom.Core.Interfaces.Entities;
using Kingdom.Web.Models.Position;
using Kingdom.Web.Models.Region;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kingdom.Web.Controllers
{
    public class RegionController : Controller
    {
        private IRegionService _regionService;
        private ICommandDispatcher _commandDispatcher;

        public RegionController(IRegionService regionService, ICommandDispatcher commandDispatcher)
        {
            this._regionService = regionService;
            this._commandDispatcher = commandDispatcher;
        }

        public ActionResult Index(int regionId = 1, int zoomLevel = 1)
        {
            this._commandDispatcher.Dispatch<AddBuildingCommand>(new AddBuildingCommand());

            IList<IRegion> regions = new List<IRegion>();// this._regionService.GetRegions();
            regions.Add(_regionService.GetRegion(regionId));
            ViewBag.ZoomLevel = zoomLevel;
            //regions.Add(_regionService.GetRegion(regionId + 1));
            //regions.Add(_regionService.GetRegion(regionId + 2));
            ////regions.Add(_regionService.GetRegion(regionId + 3));
            //regions.Add(_regionService.GetRegion(11));
            //regions.Add(_regionService.GetRegion(12));
            //regions.Add(_regionService.GetRegion(13));
            ////regions.Add(_regionService.GetRegion(14));
            //regions.Add(_regionService.GetRegion(21));
            //regions.Add(_regionService.GetRegion(22));
            //regions.Add(_regionService.GetRegion(23));
            /////regions.Add(_regionService.GetRegion(24));
            //regions.Add(_regionService.GetRegion(31));
            //regions.Add(_regionService.GetRegion(32));
            //regions.Add(_regionService.GetRegion(33));
            ////regions.Add(_regionService.GetRegion(34));


            return View("index", regions.Select(o => new RegionViewModel(o, zoomLevel)).ToList());//this.ConvertRegions(regions));
        }

        public ActionResult Get(int x, int y)
        {
            return View("region", new RegionViewModel(_regionService.GetRegion(x, y)));
        }

        public ActionResult CreateRegions(int x, int y)
        {
            this._regionService.CreateRegions(x, y);

            return this.Index();
        }

        public ActionResult GetRange(int topLeftX, int topLeftY, int topRightX, int topRightY, int bottomLeftX, int bottomLeftY, int bottomRightX, int bottomRightY, IList<int> exclude)
        {
            IList<IRegion> regions = this._regionService.GetRegions(topLeftX, topLeftY, topRightX, topRightY, bottomLeftX, bottomLeftY, bottomRightX, bottomRightY, exclude);

            if (regions.Any())
            {
                return this.PartialView("regions", regions.Select(o => new RegionViewModel(o)).ToList());
            }

            return null;
        }

        public ActionResult GetVisibleRegions(int screenWidth, int screenHeight, int x, int y, List<int> exclude, int zoomLevel = 1)
        {
            exclude = exclude ?? new List<int>();
            IList<IRegion> regions = this._regionService.GetVisibleRegionPositions(screenWidth, screenHeight, x, y, exclude);

            if (regions.Any())
            {
                ViewBag.ZoomLevel = zoomLevel;
                return this.PartialView("regions", regions.Select(o => new RegionViewModel(o, zoomLevel)).ToList());
            }

            return null;
        }

        public ActionResult GetVisibleRegionIds(int screenWidth, int screenHeight, int x, int y, int zoomLevel = 1)
        {
            IList<int> regionIds = this._regionService.GetVisibleRegionIds(screenWidth, screenHeight, x, y);

            return this.Json(regionIds);
        }
    }
}
