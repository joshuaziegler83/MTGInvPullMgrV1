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

        }
        {

        }
    }
}
