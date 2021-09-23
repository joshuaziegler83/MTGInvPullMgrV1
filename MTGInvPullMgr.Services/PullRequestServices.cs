using MTGInvPullMgr.Data;
using MTGInvPullMgr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MTGInvPullMgr.Services
{

    public class PullRequestServices
    {
        private readonly Guid _userId;
        public PullRequestServices(Guid userId)
        {
            _userId = userId;
        }
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

        public PullRequestDetail GetPullRequestByCustomerId(int customerId)
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
                        PullRequestId = entity.PullRequestId,
                        CustomerId = entity.CustomerId,
                        ExpirationDateTime = entity.ExpirationDateTime,
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
                                    PullRequestId = e.PullRequestId,
                                    CustomerId = e.CustomerId,
                                    ExpirationDateTime = e.ExpirationDateTime,
                                    IsPulled = e.IsPulled,
                                    IsFinalized = e.IsFinalized,
                                    IsPriority = e.IsPriority,
                                    TransactionAmount = e.TransactionAmount
                                    
                                }
                        );
                return query.ToArray();//this seems to return the entity note model. I notice that the expiry was returing in the response
                
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
                                    PullRequestId = e.PullRequestId,
                                    CustomerId = e.CustomerId,
                                    ExpirationDateTime = e.ExpirationDateTime,
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
            DateTime expiry = DateTime.Now.AddHours(2);

            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .PullRequests
                        .Single(e => e.PullRequestId == model.PullRequestId && e.ExpirationDateTime > DateTime.Now && !e.IsFinalized);
                entity.PullRequestId = model.PullRequestId;
                entity.IsFinalized = model.IsFinalized;
                entity.IsPriority = model.IsPriority;
                entity.TransactionAmount = model.TransactionAmount;
                if (model.IsPulled && !entity.IsPulled)
                {
                    entity.IsPulled = model.IsPulled;
                    entity.ExpirationDateTime = expiry;
                }
                    
                if (model.IsFinalized)
                {
                    entity.TransactionAmount = GetTransactionAmt(model.PullRequestId);
                    entity.ExpirationDateTime = DateTime.Now;
                    UpdateInvFromPullReqFinalization(model.PullRequestId);
                }
                            
                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeletePullRequest(int prId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .PullRequests
                        .Single(e => e.PullRequestId == prId);
                ctx.PullRequests.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        //HELPER METHODS

        public decimal GetTransactionAmt(int pullRequestId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .PullRequestItems
                        .Where(e => e.PullRequestId == pullRequestId)
                        .Select(
                            e =>
                                new PullRequestItemDetail
                                {
                                    PullRequestItemId = e.PullRequestItemId,
                                    PullRequestId = e.PullRequestId,
                                    SKU = e.SKU,
                                    Quantity = e.Quantity,
                                    Price = e.Price
                                }
                                );
                List<PullRequestItemDetail> queryList = query.ToList();
                decimal transactionAmt = 0;
                foreach (var item in queryList)
                {
                    transactionAmt += (item.Price * item.Quantity);
                }
                return transactionAmt;
            }
                
        }

        public List<PullRequestItemDetail> GetPRItemsToUpdt(int pullRequestId)
        {
            PullRequestItemServices pullReqItems = new PullRequestItemServices(_userId);
            var http = new HttpClient();
            List<PullRequestItemDetail> ItemDetail = pullReqItems.GetPullRequestItemsById(pullRequestId);
            return ItemDetail;

        }
        
        public List <DealerInvDetail> GetItemsFromInv(IEnumerable <PullRequestItemDetail> requestItems)
        {
            DealerInvServices inventory = new DealerInvServices(_userId);
            List<DealerInvDetail> invList = new List<DealerInvDetail>();
            foreach(var request in requestItems)
            {
                DealerInvDetail invDetail = inventory.GetItemBySKU(request.SKU);
                invList.Add(invDetail);
            }
            return invList;
        }

        public bool UpdateInvFromPullReqFinalization(int pullRequestId)
        {
            DealerInvServices inventory = new DealerInvServices(_userId);
            var http = new HttpClient();
            List<PullRequestItemDetail> ListItems = GetPRItemsToUpdt(pullRequestId);
            List<DealerInvDetail> ItemList = GetItemsFromInv(ListItems);
            int index = 0;
            int updtCnt = 0;
            for(int i = 0; i < ItemList.Count; i++)
            {
               
                DealerInvItemEdit invEdit = new DealerInvItemEdit();
                index = ListItems.FindIndex(a => a.SKU == ItemList[i].SKU);
                invEdit.SKU = ItemList[i].SKU;
                invEdit.ApiObjectURI = ItemList[i].ApiObjectURI;
                invEdit.CollectorNumber = ItemList[i].CollectorNumber;
                invEdit.CurrentInventory = ItemList[i].CurrentInventory - ListItems[index].Quantity;
                invEdit.IsFoil = ItemList[i].IsFoil;
                invEdit.IsVariant = ItemList[i].IsVariant;
                invEdit.Lang = ItemList[i].Lang;
                invEdit.Name = ItemList[i].Name;
                invEdit.Rarity = ItemList[i].Rarity;
                invEdit.Set = ItemList[i].Set;
                invEdit.SetName = ItemList[i].SetName;
                if (inventory.UpdateInvItem(invEdit))
                    updtCnt++;
            }
            if (updtCnt == ItemList.Count)
                return true;
            return false;
        }
    }
}

