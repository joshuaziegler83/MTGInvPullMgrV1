using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGInvPullMgr.Models
{
    public class CustomerDetail
    {
        public string Email { get; set; }
        [Display(Name = "First Name")]
        public string NameFirst { get; set; }
        [Display(Name = "Last Name")]
        public string NameLast { get; set; }
    }
}
