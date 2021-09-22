using MTGInvPullMgr.Data;
using MTGInvPullMgr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGInvPullMgr.Services
{
    public class DealerInvServices
    {
        private readonly Guid _userId;
        public DealerInvServices(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateInvItem(DealerInvItemCreate model)
        {
            var entity =
                new DealerInventory()
                {
                    SKU = model.SKU,
                    Name = model.Name,
                    ApiObjectURI = model.ApiObjectURI,
                    CurrentInventory = model.CurrentInventory,
                    SetName = model.SetName,
                    Set = model.Set,
                    CollectorNumber = model.CollectorNumber,
                    IsFoil = model.IsFoil,
                    IsVariant = model.IsVariant,
                    Rarity = model.Rarity,
                    Lang = model.Lang
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.DealerInventories.Add(entity);
                return ctx.SaveChanges() == 1;
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
        }

        public IEnumerable<DealerInvListItem> GetInvByName(string cardName)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .DealerInventories
                        .Where(e => e.Name.Contains(cardName));
                return QueryToList(query);
            }
        }

        public IEnumerable<DealerInvListItem> GetInvBySetName(string setName)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .DealerInventories
                        .Where(e => e.SetName.Contains(setName));
                return QueryToList(query);
            }
        }

        public IEnumerable<DealerInvListItem> GetOutOfStockBySetName(string setName)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .DealerInventories
                        .Where(e => e.CurrentInventory == 0 && e.SetName.Contains(setName));
                return QueryToList(query);
                        
            }
        }

       public IEnumerable<DealerInvListItem> GetInvBySet(string set)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .DealerInventories
                        .Where(e => e.Set.Contains(set));
                return QueryToList(query);
            }
        }

        public IEnumerable<DealerInvListItem> GetInvByFoils()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .DealerInventories
                        .Where(e => e.IsFoil == true);
                return QueryToList(query);
            }
        }

        public IEnumerable<DealerInvListItem> GetInvByVariants(bool isVariant) //only need this to get to controller method
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .DealerInventories
                        .Where(e => e.IsVariant == true);
                        return QueryToList(query);
            }
        }

        public bool UpdateInvItem(DealerInvItemEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .DealerInventories
                        .Single(e => e.SKU == model.SKU);
                entity.Name = model.Name;
                entity.Set = model.Set;
                entity.SetName = model.SetName;
                entity.ApiObjectURI = model.ApiObjectURI;
                entity.CollectorNumber = model.CollectorNumber;
                entity.CurrentInventory = model.CurrentInventory;
                entity.IsFoil = model.IsFoil;
                entity.IsVariant = model.IsVariant;
                entity.Rarity = model.Rarity;
                entity.Lang = model.Lang;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteInvItem(int sku)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .DealerInventories
                        .Single(e => e.SKU == sku);
                ctx.DealerInventories.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        //HELPER METHODS

        private IEnumerable<DealerInvListItem> QueryToList(IQueryable<DealerInventory> query)
        {
            var items = query.Select(
                            e =>
                                new DealerInvListItem
                                {
                                    SKU = e.SKU,
                                    Name = e.Name,
                                    CurrentInventory = e.CurrentInventory,
                                    AvailableInventory = 0, //GetAvailableInv(e.SKU, e.CurrentInventory),
                                    SetName = e.SetName,
                                    Set = e.Set,
                                    CollectorNumber = e.CollectorNumber,
                                    IsFoil = e.IsFoil,
                                    IsVariant = e.IsVariant,
                                    Rarity = e.Rarity,
                                    Lang = e.Lang
                                }
                        );
            List<DealerInvListItem> queryList = items.ToList();
            foreach (var item in queryList)
            {
                item.AvailableInventory = GetAvailableInv(item.SKU, item.CurrentInventory);
            }
            return queryList;
        }
        public int GetAvailableInv(int sku, int currentInv)
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
    }
}
