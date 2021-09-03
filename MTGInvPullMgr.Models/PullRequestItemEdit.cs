using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGInvPullMgr.Data
{
    public class PullRequestItemEdit
    {
        public Guid PullRequestItemId { get; set; }
        public Guid PullRequestId { get; set; }
        public int SKU { get; set; }
        public int Quantity { get; set; }

    }
}
