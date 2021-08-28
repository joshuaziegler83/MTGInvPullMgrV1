using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGInvPullMgr.Models
{
    public class PullRequestCreate
    {
        [Required]
        public Guid CustomerId { get; set; }
    }
}
