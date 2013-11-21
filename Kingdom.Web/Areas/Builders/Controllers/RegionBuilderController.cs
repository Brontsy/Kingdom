using Kingdom.Core.Interfaces.Regions;
using Kingdom.Web.Areas.Builders.Models.RegionBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kingdom.Web.Areas.Builders.Controllers
{
    public class RegionBuilderController : Controller
    {
        private IRegionBuilder _regionBuilder;
        
        public RegionBuilderController(IRegionBuilder regionBuilder)
        {
            this._regionBuilder = regionBuilder;
        }

        public ActionResult Index(bool built = false)
        {
            ViewBag.RegionBuilt = built;

            RegionBuilderViewModel viewModel = new RegionBuilderViewModel();

            return View("Index", viewModel);
        }

        public ActionResult BuildRegion(RegionBuilderViewModel viewModel)
        {
            if (this.ModelState.IsValid)
            {
                this._regionBuilder.BuildRegions(viewModel.X.Value, viewModel.Y.Value, viewModel.Size.Value);

                return this.Index(true);
            }

            return this.Index();
        }
    }
}
