using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGInvPullMgr.Models
{
    public class DealerInvItemCreate
    {
        [Required]
        public int SKU { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int CurrentInventory { get; set; }
        public string ApiObjectURI { get; set; }
        public string ScryfallUri { get; set; }
        public string SetName { get; set; }
        public string Set { get; set; }
        public int CollectorNumber { get; set; }
        public bool IsFoil { get; set; }
        public bool IsVariant { get; set; }
        public string Rarity { get; set; }
        public string Lang { get; set; }

    }
}
