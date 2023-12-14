using System.Collections.Generic;
using System.Linq;
using Eco.Gameplay.Components;
using Eco.Gameplay.Components.Store;
using Eco.Gameplay.Economy;
using Eco.Gameplay.Items;
using Eco.Gameplay.Players;
using Eco.Gameplay.Systems.TextLinks;
using Eco.Shared.Utils;
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
            return $"{{ \"username\": \"r3sist3nt\" }}";
        }

        [HttpPost("updateShopPricing")]
        [Authorize(PolicyNames.RequiresEcoUser)]
        public void UpdateShopPricing([FromBody] UpdateItemDTO updateItemDto)
        {
            var user = getUserFromContext();
            if (user == null)
            {
                return;
            }

            TradeUtil.Stores
                .Where(store => store.Owners.ContainsUser(user))
                .Select(store => store.AllOffers)
                .SelectMany(x => x)
                .Where(offer => "I_" + offer?.Stack?.Item?.DisplayName == updateItemDto.ItemId)
                .ForEach(offer => { offer.Price = updateItemDto.NewPrice; });
        }

        private User getUserFromContext()
        {
            return (HttpContext.User.Identity as EcoUserIdentity)?.User;
        }
    }

    public class UpdateItemDTO
    {
        public string ItemId { get; set; }
        public float NewPrice { get; set; }
    }
}