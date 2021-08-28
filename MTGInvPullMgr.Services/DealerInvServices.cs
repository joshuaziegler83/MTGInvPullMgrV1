using MTGInvPullMgr.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGInvPullMgr.Services
{
    public class DealerInvServices
    {
        //helper method to get the inventory
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

        public int UpdateCurrentInv(int claimedInv, int currentInv)
        {
            return currentInv - claimedInv;
        }
    }

    //helper method

}
