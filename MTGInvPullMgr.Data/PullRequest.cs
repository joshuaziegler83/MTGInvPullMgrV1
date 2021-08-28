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
        public Guid CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public DateTime ExpirationDateTime
        {
            get
            {
               return DateTime.Now.AddHours(2);
               //return GetExpiration();
            }
        }

        public bool IsPulled { get; set; }

        public bool IsFinalized { get; set; }

        public bool IsPriority { get; set; }

        public decimal TransactionAmount { get; set; }


    }
}
