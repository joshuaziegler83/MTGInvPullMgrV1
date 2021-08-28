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

        public bool CreateInvItem(DealerInvItemCreate model)
        {
            var entity =
                new DealerInventory()
                {
                    SKU = model.SKU,
                    Name = model.Name,
                    CurrentInventory = model.CurrentInventory,
                    SetName = model.SetName,
                    Set = model.Set,
                    CollectorNumber = model.CollectorNumber,
                    IsFoil = model.IsFoil,
                    IsVariant = model.IsVariant

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
                        IsVariant = entity.IsVariant
                    };
            }
        }

        public IEnumerable<DealerInvListItem> GetInvByName(string name)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .DealerInventories
                        .Where(e => e.Name.Contains(name))
                        .Select(
                            e =>
                                new DealerInvListItem
                                {
                                    SKU = e.SKU,
                                    Name = e.Name,
                                    CurrentInventory = e.CurrentInventory,
                                    AvailableInventory = GetAvailableInv(e.SKU, e.CurrentInventory),
                                    SetName = e.SetName,
                                    Set = e.Set,
                                    CollectorNumber = e.CollectorNumber,
                                    IsFoil = e.IsFoil,
                                    IsVariant = e.IsVariant
                                }
                        );
                return query.ToArray();
            }
        }

        public IEnumerable<DealerInvListItem> GetInvBySetName(string setName)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .DealerInventories
                        .Where(e => e.SetName.Contains(setName))
                        .Select(
                            e =>
                                new DealerInvListItem
                                {
                                    SKU = e.SKU,
                                    Name = e.Name,
                                    CurrentInventory = e.CurrentInventory,
                                    AvailableInventory = GetAvailableInv(e.SKU, e.CurrentInventory),
                                    SetName = e.SetName,
                                    Set = e.Set,
                                    CollectorNumber = e.CollectorNumber,
                                    IsFoil = e.IsFoil,
                                    IsVariant = e.IsVariant
                                }
                        );
                return query.ToArray();
            }
        }

        public IEnumerable<DealerInvListItem> GetOutOfStockBySetName(string setName)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .DealerInventories
                        .Where(e => e.CurrentInventory == 0 && e.SetName.Contains(setName))
                        .Select(
                            e =>
                                new DealerInvListItem
                                {
                                    SKU = e.SKU,
                                    Name = e.Name,
                                    CurrentInventory = e.CurrentInventory,
                                    AvailableInventory = GetAvailableInv(e.SKU, e.CurrentInventory),
                                    SetName = e.SetName,
                                    Set = e.Set,
                                    CollectorNumber = e.CollectorNumber,
                                    IsFoil = e.IsFoil,
                                    IsVariant = e.IsVariant
                                }
                        );
                return query.ToArray();
            }
        }
        public IEnumerable<DealerInvListItem> GetInvBySet(string set)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .DealerInventories
                        .Where(e => e.Set.Contains(set))
                        .Select(
                            e =>
                                new DealerInvListItem
                                {
                                    SKU = e.SKU,
                                    Name = e.Name,
                                    CurrentInventory = e.CurrentInventory,
                                    AvailableInventory = GetAvailableInv(e.SKU, e.CurrentInventory),
                                    SetName = e.SetName,
                                    Set = e.Set,
                                    CollectorNumber = e.CollectorNumber,
                                    IsFoil = e.IsFoil,
                                    IsVariant = e.IsVariant
                                }
                        );
                return query.ToArray();
            }
        }

        public IEnumerable<DealerInvListItem> GetInvByFoils()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .DealerInventories
                        .Where(e => e.IsFoil == true)
                        .Select(
                            e =>
                                new DealerInvListItem
                                {
                                    SKU = e.SKU,
                                    Name = e.Name,
                                    CurrentInventory = e.CurrentInventory,
                                    AvailableInventory = GetAvailableInv(e.SKU, e.CurrentInventory),
                                    SetName = e.SetName,
                                    Set = e.Set,
                                    CollectorNumber = e.CollectorNumber,
                                    IsFoil = e.IsFoil,
                                    IsVariant = e.IsVariant
                                }
                        );
                return query.ToArray();
            }
        }

        public IEnumerable<DealerInvListItem> GetInvByVariants()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .DealerInventories
                        .Where(e => e.IsVariant == true)
                        .Select(
                            e =>
                                new DealerInvListItem
                                {
                                    SKU = e.SKU,
                                    Name = e.Name,
                                    CurrentInventory = e.CurrentInventory,
                                    AvailableInventory = GetAvailableInv(e.SKU, e.CurrentInventory),
                                    SetName = e.SetName,
                                    Set = e.Set,
                                    CollectorNumber = e.CollectorNumber,
                                    IsFoil = e.IsFoil,
                                    IsVariant = e.IsVariant
                                }
                        );
                return query.ToArray();
            }
        }

        //helper method to get the inventory

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

    //helper method

}
