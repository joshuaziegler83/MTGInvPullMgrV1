using MTGInvPullMgr.Data;
using MTGInvPullMgr.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
            
       
        
      //  private readonly DealerInvServices _dealer = new DealerInvServices(_userId);;
        public bool CreatePullRequestItem(PullRequestItemCreate model)
        {
            var entity =
                new PullRequestItem()
                {
                    PullRequestId = model.PullRequestId,
                    SKU = model.SKU,
                    Quantity = model.Quantity,
                    Price = GetPriceBySku(model.SKU)

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

        public IEnumerable<PullRequestItemDetail> GetPullRequestItemsById(int pullRequestId)
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

        public decimal GetPriceBySku(int sku)
        {
            DealerInvServices dealer = new DealerInvServices(_userId);
            var http = new HttpClient();
            DealerInvDetail dealerInvDetail = dealer.GetItemBySKU(sku);
            var cardRes = http.GetAsync(dealerInvDetail.ApiObjectURI).Result;
            var card = JsonConvert.DeserializeObject<MtGCard>
               (cardRes.Content.ReadAsStringAsync().Result);
            decimal price;
            if (card.Foil)
            {
                price = Convert.ToDecimal(card.Prices.Usd);//fix this here.
            }
            else
            {
                price = Convert.ToDecimal(card.Prices.Usd);
            }
            return price;
        }

        /*public int GetAvailableInv(int sku, int currentInv)
        {
            int claimedInv = GetClaimedInv(sku);
            int availableInventory = currentInv - claimedInv;
            return availableInventory;
        }

        public int GetClaimedInv(int sku)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var skuList = from pullReq in ctx.PullRequests
                              join pullItem in ctx.PullRequestItems
                              on pullReq.PullRequestId
                              equals pullItem.PullRequestId
                              where pullReq.ExpirationDateTime > DateTime.Now
                               && !pullReq.IsFinalized && pullItem.SKU == sku
                              select new
                              {
                                  pullItem.SKU,
                                  pullItem.Quantity
                              };
                int claimedInv = 0;
                foreach (var item in skuList)
                {
                    claimedInv += item.Quantity;
                }
                return claimedInv;
            }
        }

        public DealerInvDetail GetItemBySKU(int sku)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .DealerInventories
                        .Single(e => e.SKU == sku);
                return
                    new DealerInvDetail
                    {
                        SKU = entity.SKU,
                        Name = entity.Name,
                        ApiObjectURI = entity.ApiObjectURI,
                        CurrentInventory = entity.CurrentInventory,
                        AvailableInventory = GetAvailableInv(entity.SKU, entity.CurrentInventory),
                        SetName = entity.SetName,
                        Set = entity.Set,
                        CollectorNumber = entity.CollectorNumber,
                        IsFoil = entity.IsFoil,
                        IsVariant = entity.IsVariant,
                        Rarity = entity.Rarity,
                        Lang = entity.Lang
                    };
            }
        }*/
    }
}

