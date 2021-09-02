using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGInvPullMgr.Data
{
    public class PullRequestItem
    {
        [Key]
        public Guid PullRequestItemId{ get; set; }

        [Required, ForeignKey(nameof(PullRequest))]
        public Guid PullRequestId { get; set; }

        public virtual PullRequest PullRequest { get; set; }

        [Required, ForeignKey(nameof(DealerInventory))]
        public int SKU { get; set; }

        public virtual DealerInventory DealerInventory { get; set; }

        public int Quantity { get; set; }



    }
}
