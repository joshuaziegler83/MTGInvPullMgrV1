using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGInvPullMgr.Models
{
    public class MtGCard
    {
        public class Rootobject
        {
            public string Object { get; set; }
            public string Id { get; set; }
            public int TcgplayerId { get; set; }
            public int cardmarket_id { get; set; }
            public string name { get; set; }
            public string lang { get; set; }
            public string uri { get; set; }
            public string scryfall_uri { get; set; }
            public string layout { get; set; }
            public bool highres_image { get; set; }
            public string image_status { get; set; }
            public Image_Uris image_uris { get; set; }
            public string mana_cost { get; set; }
            public float cmc { get; set; }
            public string type_line { get; set; }
            public string oracle_text { get; set; }
            public object[] colors { get; set; }
            public string[] color_identity { get; set; }
            public object[] keywords { get; set; }
            public string[] produced_mana { get; set; }
            public Legalities legalities { get; set; }
            public string[] games { get; set; }
            public bool reserved { get; set; }
            public bool foil { get; set; }
            public bool nonfoil { get; set; }
            public bool oversized { get; set; }
            public bool promo { get; set; }
            public bool reprint { get; set; }
            public bool variation { get; set; }
            public string setid { get; set; }
            public string set { get; set; }
            public string setname { get; set; }
            public string settype { get; set; }
            public string seturi { get; set; }
            public string set_search_uri { get; set; }
            public string scryfall_set_uri { get; set; }
            public string rulings_uri { get; set; }
            public string prints_search_uri { get; set; }
            public string collector_number { get; set; }
            public bool digital { get; set; }
            public string rarity { get; set; }
            public string card_back_id { get; set; }
            public string artist { get; set; }
            public string[] artist_ids { get; set; }
            public string illustration_id { get; set; }
            public string border_color { get; set; }
            public string frame { get; set; }
            public bool full_art { get; set; }
            public bool textless { get; set; }
            public bool booster { get; set; }
            public bool story_spotlight { get; set; }
            public Prices prices { get; set; }
            public Related_Uris related_uris { get; set; }
            public Purchase_Uris purchase_uris { get; set; }
        }
        public class Image_Uris
        {
            public string small { get; set; }
            public string normal { get; set; }
            public string large { get; set; }
            public string png { get; set; }
            public string art_crop { get; set; }
            public string border_crop { get; set; }
        }
        public class Legalities
        {
            public string standard { get; set; }
            public string future { get; set; }
            public string historic { get; set; }
            public string gladiator { get; set; }
            public string pioneer { get; set; }
            public string modern { get; set; }
            public string legacy { get; set; }
            public string pauper { get; set; }
            public string vintage { get; set; }
            public string penny { get; set; }
            public string commander { get; set; }
            public string brawl { get; set; }
            public string historicbrawl { get; set; }
            public string paupercommander { get; set; }
            public string duel { get; set; }
            public string oldschool { get; set; }
            public string premodern { get; set; }
        }
        public class Prices
        {
            public string usd { get; set; }
            public object usd_foil { get; set; }
        }
        public class Related_Uris
        {
            public string gatherer { get; set; }
            public string tcgplayer_infinite_articles { get; set; }
            public string tcgplayer_infinite_decks { get; set; }
            public string edhrec { get; set; }
            public string mtgtop8 { get; set; }
        }
        public class Purchase_Uris
        {
            public string tcgplayer { get; set; }
            public string cardmarket { get; set; }
            public string cardhoarder { get; set; }
        }
    }
}
