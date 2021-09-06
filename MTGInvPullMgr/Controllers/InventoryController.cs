using Microsoft.AspNet.Identity;
using MTGInvPullMgr.Models;
using MTGInvPullMgr.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MTGInvPullMgr.Controllers
{
    [Authorize]
    public class InventoryController : ApiController
    {
        private DealerInvServices CreateDealerInvService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var inventoryService = new DealerInvServices(userId);
            return inventoryService;
        }

        public IHttpActionResult Post(DealerInvItemCreate dealerInvItemCreate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreateDealerInvService();
            if (!service.CreateInvItem(dealerInvItemCreate))
                return InternalServerError();
            return Ok();
        }
        public IHttpActionResult Get(int sku)
        {
            DealerInvServices dealerInvServices = CreateDealerInvService();
            var invItem = dealerInvServices.GetItemBySKU(sku);
            return Ok(invItem);
        }

        
        public IHttpActionResult Get(string setName)
        {
            DealerInvServices dealerInvServices = CreateDealerInvService();
            var setCards = dealerInvServices.GetInvBySetName(setName);
            return Ok(setCards);
        }

        //To do, get by set

        public IHttpActionResult Put(DealerInvItemEdit dealerInvItemEdit)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreateDealerInvService();
            if (!service.UpdateInvItem(dealerInvItemEdit))
                return InternalServerError();
            return Ok();
        }

        public IHttpActionResult Delete(int sku)
        {
            var service = CreateDealerInvService();

            if (!service.DeleteInvItem(sku))
                return InternalServerError();
            return Ok();
        }
        
    }


}
