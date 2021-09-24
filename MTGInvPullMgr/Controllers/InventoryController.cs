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

        [HttpPost]
        [Route("api/Inventory/")]
        public IHttpActionResult Post(DealerInvItemCreate dealerInvItemCreate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreateDealerInvService();
            if (!service.CreateInvItem(dealerInvItemCreate))
                return InternalServerError();
            return Ok();
        }
        [HttpGet]
        [Route("api/Inventory/sku={sku}")]
        public IHttpActionResult Get(int sku)
        {
            DealerInvServices dealerInvServices = CreateDealerInvService();
            var invItem = dealerInvServices.GetItemBySKU(sku);
            return Ok(invItem);
        }
        [HttpGet]
        [Route("api/Inventory/name={cardName}")]
        public IHttpActionResult GetByCardName(string cardName)
        {
            DealerInvServices dealerInvServices = CreateDealerInvService();
            var cards = dealerInvServices.GetInvByName(cardName);
            return Ok(cards);
        }

        [HttpGet]
        [Route("api/Inventory/setName={setName}")]
        public IHttpActionResult GetBySetName(string setName)
        {
            DealerInvServices dealerInvServices = CreateDealerInvService();
            var setCards = dealerInvServices.GetInvBySetName(setName);
            return Ok(setCards);
        }

        [HttpGet]
        [Route("api/Inventory/set={set}")]
        public IHttpActionResult GetBySet(string set)
        {
            DealerInvServices dealerInvServices = CreateDealerInvService();
            var setCards = dealerInvServices.GetInvBySet(set);
            return Ok(setCards);
        }

        [HttpGet]
        [Route("api/PullRequest/isVariant={isVariant}")]
        public IHttpActionResult GetByVariant(bool isVariant)
        {
            DealerInvServices dealerInvServices = CreateDealerInvService();
            var variantCards = dealerInvServices.GetInvByVariants(isVariant);
            return Ok(variantCards);
        }

        [HttpPut]
        [Route("api/Inventory/")]
        public IHttpActionResult Put(DealerInvItemEdit dealerInvItemEdit)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreateDealerInvService();
            if (!service.UpdateInvItem(dealerInvItemEdit))
                return InternalServerError();
            return Ok();
        }

        [HttpDelete]
        [Route("api/Inventory/")]
        public IHttpActionResult Delete(int sku)
        {
            var service = CreateDealerInvService();

            if (!service.DeleteInvItem(sku))
                return InternalServerError();
            return Ok();
        }
        
    }


}
