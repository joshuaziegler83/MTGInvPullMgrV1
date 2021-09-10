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
    public class PullRequestItemController : ApiController
    {
        private PullRequestItemServices PullRequestItemServices()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var pullRequestServices = new PullRequestItemServices(userId);
            return pullRequestServices;
        }
        [HttpPost]
        [Route("api/PullRequestItem/")]
        public IHttpActionResult Post(PullRequestItemCreate pullRequestItemCreate)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var service = PullRequestItemServices();
            if (!service.CreatePullRequestItem(pullRequestItemCreate))
                return InternalServerError();
            return Ok();
        }
        [HttpGet]
        [Route("api/PullRequestItem/")]
        public IHttpActionResult GetAllPullItems()
        {
            PullRequestItemServices pullRequestItemService = PullRequestItemServices();
            var pullReqItems = pullRequestItemService.GetPullRequestItems();
            return Ok(pullReqItems);
        }
        [HttpGet]
        [Route("api/PullRequestItem/pullRequestId={pullRequestId}")]
        public IHttpActionResult GetItemsByPullRequestId(int pullRequestId)
        {
            PullRequestItemServices service = PullRequestItemServices();
            var pullReqItems = service.GetPullRequestItemsById(pullRequestId);
            return Ok(pullReqItems);
        }
        [HttpGet]
        [Route("api/PullRequestItem/sku={sku}")]
        public IHttpActionResult GetPullRequestItemsBySku(int sku)
        {
            PullRequestItemServices service = PullRequestItemServices();
            var pullReqItems = service.GetPullRequestItemsBySku(sku);
           return Ok(pullReqItems);
        }

        public IHttpActionResult Put(PullRequestItemEdit pullItemEdit)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = PullRequestItemServices();
            if (!service.UpdatePullRequestItem(pullItemEdit))
                return InternalServerError();
            return Ok();
        }
    }
}
