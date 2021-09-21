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
    public class CardServices
    {
        // public string ScryfallUri { get; set; }
        //public string Name { get; set; }
        //public string SetName { get; set; }

        public MtGCard GetCardByUri(string apiObjectUri)
        {
            var http = new HttpClient();
            var cardRes = http.GetAsync(apiObjectUri).Result;
            var card = JsonConvert.DeserializeObject<MtGCard>
                (cardRes.Content.ReadAsStringAsync().Result);
            return card;
        }

        public MtGCard GetCardsByName(string name)
        {
            var http = new HttpClient();
            var cardRes = http.GetAsync("https://api.scryfall.com/cards/named?exact=" + name).Result;
            var card = JsonConvert.DeserializeObject<MtGCard>
               (cardRes.Content.ReadAsStringAsync().Result);
            return card;
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

        /* public IEnumerable<MtGCard> GetCardsBySet(string set)
          {
              MtGCard mtgCard = new MtGCard();
              var http = new HttpClient();
              var cardListJson = http.GetAsync("https://api.scryfall.com/cards/search?q=set:" + set).Result;
              var cardList = JsonConvert.DeserializeObject<List<RootObject>>(cardListJson.Content.ReadAsStringAsync().Result);
              List<MtGCard> mtgCards = JsonConvert.PopulateObject(cardList, mtgCard);

          }*/

        //TO DO need helper get method to search our own DB by set

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
