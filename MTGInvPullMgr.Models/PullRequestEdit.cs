﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGInvPullMgr.Models
{
    public class PullRequestEdit
    {
        public int PullRequestId { get; set; }
        public bool IsPulled { get; set; }
        public bool IsFinalized { get; set; }
        public bool IsPriority { get; set; }
        public DateTime ExpirationDateTime { get; set; }
        public decimal TransactionAmount { get; set; }
    }
}
