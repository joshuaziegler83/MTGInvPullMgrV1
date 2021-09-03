﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGInvPullMgr.Models
{
    public class PullRequestDetail
    {
        public Guid PullRequestId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime ExpirationDateTime { get; }
        public bool IsPulled { get; set; }
        public bool IsFinalized { get; set; }
        public bool IsPriority { get; set; }
        public decimal TransactionAmount { get; set; }
    }
}
