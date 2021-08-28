using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGInvPullMgr.Models
{
    class PullRequestItemCreate
    {
        [Required]
        public int PullRequestId { get; set; }

        [Required]
        public int SKU { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
