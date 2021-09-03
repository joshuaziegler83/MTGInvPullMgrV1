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

      /* public IEnumerable<MtGCard> GetCardsBySet(string set)
        {
            MtGCard mtgCard = new MtGCard();
            var http = new HttpClient();
            var cardListJson = http.GetAsync("https://api.scryfall.com/cards/search?q=set:" + set).Result;
            var cardList = JsonConvert.DeserializeObject<List<RootObject>>(cardListJson.Content.ReadAsStringAsync().Result);
            List<MtGCard> mtgCards = JsonConvert.PopulateObject(cardList, mtgCard);

        }*/

        //TO DO need helper get method to search our own DB by set




    }
}
