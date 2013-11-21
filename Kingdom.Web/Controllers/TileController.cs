using Kingdom.Core.Enums.Tiles;
using Kingdom.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kingdom.Web.Controllers
{
    public class TileController : Controller
    {
        private ITileService _tileService;

        public TileController(ITileService tileService)
        {
            this._tileService = tileService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Change(TileType tileType, int regionId, int x, int y)
        {
            this._tileService.ChangeTileType(tileType, regionId, x, y);

            return null;
        }
    }
}
