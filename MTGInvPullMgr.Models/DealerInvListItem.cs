using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGInvPullMgr.Models
{
    public class DealerInvListItem
    {
        public int SKU { get; set; }
        public string Name { get; set; }
        public int CurrentInventory { get; set; }
        public int AvailableInventory { get; }
        public string SetName { get; set; }
        public string Set { get; set; }
        public int CollectorNumber { get; set; }
        public bool IsFoil { get; set; }
        public bool IsVariant { get; set; }
    }
}
