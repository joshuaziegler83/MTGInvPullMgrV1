using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGInvPullMgr.Models
{
    public class PullRequestItemCreate
    {
        [Required]
        public Guid PullRequestId { get; set; }

        [Required]
        public int SKU { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
