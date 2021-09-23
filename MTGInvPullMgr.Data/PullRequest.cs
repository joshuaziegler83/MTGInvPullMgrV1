using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGInvPullMgr.Data
{
    public class PullRequest
    {
        [Key]
        public int PullRequestId { get; set; }

        [Required, ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public DateTime ExpirationDateTime { get; set; } = new DateTime(2100, 12, 31, 23, 59, 59);

        public bool IsPulled { get; set; }

        public bool IsFinalized { get; set; }

        public bool IsPriority { get; set; }

        public decimal TransactionAmount { get; set; }


    }
}
