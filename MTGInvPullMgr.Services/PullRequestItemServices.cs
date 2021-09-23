using MTGInvPullMgr.Data;
using MTGInvPullMgr.Models;
using MTGInvPullMgr.Services;
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
            // DealerInvDetail dealerInvDetail = GetItemBySKU(model.SKU);
            int availInv = GetAvailInvFromService(model.SKU);
            //if (dealerInvDetail.AvailableInventory > model.Quantity)
            if(availInv > model.Quantity)
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
            return false;

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
                        Quantity = pullReqItem.Quantity,
                        Price = pullReqItem.Price
                    };
                return query.ToArray();
            }

        }

        //public IEnumerable<PullRequestItemDetail> GetPullRequestItemsById(int pullRequestId)
        public List<PullRequestItemDetail> GetPullRequestItemsById(int pullRequestId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    from pullRequest in ctx.PullRequests
                        //join pullReqItem in ctx.PullRequestItems on pullRequest.PullRequestId equals pullRequestId
                    join pullReqItem in ctx.PullRequestItems on pullRequest.PullRequestId equals pullReqItem.PullRequestId
                    where pullRequest.PullRequestId == pullRequestId && pullRequest.ExpirationDateTime > DateTime.Now && !pullRequest.IsFinalized
                    select new PullRequestItemDetail
                    {
                        PullRequestItemId = pullReqItem.PullRequestItemId,
                        PullRequestId = pullReqItem.PullRequestId,
                        SKU = pullReqItem.SKU,
                        Quantity = pullReqItem.Quantity,
                        Price = pullReqItem.Price

                    };
                //return query.ToArray();
                return query.ToList();
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
                         Quantity = pullRequestItem.Quantity,
                         Price = pullRequestItem.Price
                     };
                return query.ToArray();
            }
        }

        public bool UpdatePullRequestItem(PullRequestItemEdit model)
        {
            //DealerInvDetail dealerInvDetail = GetItemBySKU(model.SKU);
            //if (dealerInvDetail.AvailableInventory > model.Quantity)
            int availInv = GetAvailInvFromService(model.SKU);
            if (availInv > model.Quantity)
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
                    entity.Price = GetPriceBySku(model.SKU);


                    return ctx.SaveChanges() == 1;
                }
            }
            return false;
                
        }

        //HELPER METHODS
        public decimal GetPriceBySku(int sku)
        {
            DealerInvServices dealer = new DealerInvServices(_userId);
            var http = new HttpClient();
            DealerInvDetail dealerInvDetail = dealer.GetItemBySKU(sku);
            var cardRes = http.GetAsync(dealerInvDetail.ApiObjectURI).Result;
            var card = JsonConvert.DeserializeObject<MtGCard>
               (cardRes.Content.ReadAsStringAsync().Result);
            decimal price;
            if (dealerInvDetail.IsFoil)
            {
                price = Convert.ToDecimal(card.Prices.Usd_Foil);
            }
            else
            {
                price = Convert.ToDecimal(card.Prices.Usd);
            }
            return price;
        }

        public int GetAvailInvFromService(int sku)
        {
            DealerInvServices inventory = new DealerInvServices(_userId);
            DealerInvDetail invDetail = inventory.GetItemBySKU(sku);
            if (invDetail != null)
                return invDetail.AvailableInventory;
            return 0;
        }

    }
}

