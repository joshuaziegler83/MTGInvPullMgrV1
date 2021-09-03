using MTGInvPullMgr.Data;
using MTGInvPullMgr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGInvPullMgr.Services
{
    public class PullRequestServices
    {
        public bool CreatePullRequest(PullRequestCreate model)
        {
            var entity =
                new PullRequest()
                {
                    CustomerId = model.CustomerId,
                    IsPulled = model.IsPulled,
                    IsFinalized = model.IsFinalized,
                    IsPriority = model.IsPriority,
                    TransactionAmount = model.TransactionAmount
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.PullRequests.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public PullRequestDetail GetPullRequestByCustomerId(Guid customerId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .PullRequests
                        .Single(e => e.CustomerId == customerId && e.ExpirationDateTime > DateTime.Now);
                return
                    new PullRequestDetail
                    {
                        CustomerId = entity.CustomerId,
                        IsPulled = entity.IsPulled,
                        IsFinalized = entity.IsFinalized,
                        IsPriority = entity.IsPriority,
                        TransactionAmount = entity.TransactionAmount
                    };
            }
        }

        public IEnumerable<PullRequestDetail> GetActivePullRequests()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .PullRequests
                        .Where(e => e.ExpirationDateTime > DateTime.Now && !e.IsFinalized)
                        .Select(
                            e =>
                                new PullRequestDetail
                                {
                                    CustomerId = e.CustomerId,
                                    IsPulled = e.IsPulled,
                                    IsFinalized = e.IsFinalized,
                                    IsPriority = e.IsPriority,
                                    TransactionAmount = e.TransactionAmount
                                }
                        );
                return query.ToArray();
            }
        }

        public IEnumerable<PullRequestDetail> GetPriorityPullRequests()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .PullRequests
                        .Where(e => e.ExpirationDateTime > DateTime.Now && !e.IsFinalized && e.IsPriority)
                        .Select(
                            e =>
                                new PullRequestDetail
                                {
                                    CustomerId = e.CustomerId,
                                    IsPulled = e.IsPulled,
                                    IsFinalized = e.IsFinalized,
                                    IsPriority = e.IsPriority,
                                    TransactionAmount = e.TransactionAmount
                                }
                        );
                return query.ToArray();
            }
        }

        public bool UpdatePullRequest(PullRequestEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .PullRequests
                        .Single(e => e.PullRequestId == model.PullRequestId && e.ExpirationDateTime > DateTime.Now && !e.IsFinalized);
                entity.PullRequestId = model.PullRequestId;
                entity.IsPulled = model.IsPulled;
                entity.IsFinalized = model.IsFinalized;
                entity.IsPriority = model.IsPriority;
                entity.TransactionAmount = model.TransactionAmount;
                               
                return ctx.SaveChanges() == 1;
            }
        }
    }
}

