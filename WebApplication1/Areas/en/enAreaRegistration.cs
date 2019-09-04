using System.Web.Mvc;

namespace MevaPro.Areas.en
{
    public class enAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "en";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "en_default",
                "en/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}