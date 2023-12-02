using Eco.Gameplay.Players;
using Eco.WebServer.Web.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Eco.WebServer.Web.Authorization;

namespace Eco.Plugins.EcoLiveDataExporter.Utils
{
    [Route("api/v1/plugins/EcoLiveDataExporter")]
    public class DataController : Controller
    {
        [HttpGet("username")]
        [Authorize(PolicyNames.RequiresEcoUser)]
        public string GetUsername()
        {
            return $"{{ \"username\": \"{getUserFromContext().Player.DisplayName}\" }}";
        }
        
        [HttpPost("updateShopPricing")]
        [Authorize(PolicyNames.RequiresEcoUser)]
        public void UpdateShopPricing()
        {
            var user = getUserFromContext();
           
         
        }

        private User getUserFromContext()
        {
            return (HttpContext.User.Identity as EcoUserIdentity)?.User;
        }
    }
}