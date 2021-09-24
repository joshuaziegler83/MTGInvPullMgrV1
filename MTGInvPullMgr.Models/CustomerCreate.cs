using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGInvPullMgr.Models
{
    public class CustomerCreate
    {
        [Required]
        public string Email { get; set; }
        public string NameFirst { get; set; }
        public string NameLast { get; set; }
    }
}
