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
    public class PullRequestController : ApiController
    {
        private PullRequestServices PullRequestServices()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var pullRequestServices = new PullRequestServices(userId);
            return pullRequestServices;
        }

        public IHttpActionResult Post(PullRequestCreate pullRequestCreate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = PullRequestServices();
            if (!service.CreatePullRequest(pullRequestCreate))
                return InternalServerError();
            return Ok();
        }

        public IHttpActionResult Get()//get all active PullRequests
        {
            var service = PullRequestServices();
            var pullRequestList = service.GetActivePullRequests();
            return Ok(pullRequestList);
        }

        public IHttpActionResult Get(Guid customerId)
        {
            PullRequestServices pullRequestServices = PullRequestServices();
            var pullRequest = pullRequestServices.GetPullRequestByCustomerId(customerId);
            return Ok(pullRequest);
        }

        public IHttpActionResult Get(bool priority)//this input is just here to differentiate from the Get All. The var is really not needed
        {
            var service = PullRequestServices();
            var priorityPullReqs = service.GetPriorityPullRequests();
            return Ok(priorityPullReqs);
        }

        public IHttpActionResult Put(PullRequestEdit pullRequestEdit)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = PullRequestServices();
            if (!service.UpdatePullRequest(pullRequestEdit))
                return InternalServerError();
            return Ok();
        }

    }
}
