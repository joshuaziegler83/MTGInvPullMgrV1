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
        private readonly Guid _userId;
        public CardServices(Guid userId)
        {
            _userId = userId;
        }

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
