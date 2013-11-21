using System.Web.Mvc;

namespace Kingdom.Web.Areas.Builders
{
    public class BuildersRoutes : AreaRegistration
    {
        public static string BuildRegion = "BuildRegion";
        public static string BuildRegionSave = "BuildRegionSave";


        public override string AreaName
        {
            get
            {
                return "Builders";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(BuildersRoutes.BuildRegion, "builders/region", new { controller = "RegionBuilder", action = "Index" });
            context.MapRoute(BuildersRoutes.BuildRegionSave, "builders/region/save", new { controller = "RegionBuilder", action = "BuildRegion" });


            context.MapRoute("Builders_default", "Builders/{controller}/{action}/{id}", new { action = "Index", id = UrlParameter.Optional });
        }
    }
}
