using MTGInvPullMgr.Data;
using MTGInvPullMgr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGInvPullMgr.Services
{
    public class PullRequestItemServices
    {
        private readonly Guid _userId;
        public PullRequestItemServices(Guid userId)
        {
            _userId = userId;
        }
        public bool CreatePullRequestItem(PullRequestItemCreate model)
        {
            var entity =
                new PullRequestItem()
                {
                    PullRequestId = model.PullRequestId,
                    SKU = model.SKU,
                    Quantity = model.Quantity

                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.PullRequestItems.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<PullRequestItemDetail> GetPullRequestItems()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    from pullRequest in ctx.PullRequests
                    join pullReqItem in ctx.PullRequestItems on pullRequest.PullRequestId equals pullReqItem.PullRequestId
                    where pullRequest.ExpirationDateTime > DateTime.Now && !pullRequest.IsFinalized
                    select new PullRequestItemDetail
                    {
                        PullRequestItemId = pullReqItem.PullRequestItemId,
                        PullRequestId = pullReqItem.PullRequestId,
                        SKU = pullReqItem.SKU,
                        Quantity = pullReqItem.Quantity
                    };
                return query.ToArray();
            }



        }

        public IEnumerable<PullRequestItemDetail> GetPullRequestItemsById(Guid pullRequestId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    from pullRequest in ctx.PullRequests
                    join pullReqItem in ctx.PullRequestItems on pullRequest.PullRequestId equals pullRequestId
                    where pullRequest.ExpirationDateTime > DateTime.Now && !pullRequest.IsFinalized
                    select new PullRequestItemDetail
                    {
                        PullRequestItemId = pullReqItem.PullRequestItemId,
                        PullRequestId = pullReqItem.PullRequestId,
                        SKU = pullReqItem.SKU,
                        Quantity = pullReqItem.Quantity
                    };
                return query.ToArray();
            }
        }

        public IEnumerable<PullRequestItemDetail> GetPullRequestItemsBySku(int sku)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                     from pullRequestItem in ctx.PullRequestItems
                     join pullRequest in ctx.PullRequests on pullRequestItem.PullRequestId equals pullRequest.PullRequestId
                     where pullRequestItem.SKU == sku && pullRequest.ExpirationDateTime > DateTime.Now && !pullRequest.IsFinalized
                     select new PullRequestItemDetail
                     {
                         PullRequestItemId = pullRequestItem.PullRequestItemId,
                         PullRequestId = pullRequestItem.PullRequestId,
                         SKU = pullRequestItem.SKU,
                         Quantity = pullRequestItem.SKU
                     };
                return query.ToArray();
            }
        }

        public bool UpdatePullRequestItem(PullRequestItemEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .PullRequestItems
                        .Single(e => e.PullRequestItemId == model.PullRequestItemId);
                entity.PullRequestItemId = model.PullRequestItemId;
                entity.PullRequestId = model.PullRequestId;
                entity.SKU = model.SKU;
                entity.Quantity = model.Quantity;
                
                return ctx.SaveChanges() == 1;
            }
        }


    }
}

