using System;
using System.Collections.Generic;

namespace CashHub.Models
{
    public partial class Hub
    {
        public Hub()
        {
            Branches = new HashSet<Branch>();
        }

        public int HubId { get; set; }
        public string HubName { get; set; } = null!;
        public string? HubAddress { get; set; }
        public string? HunRefNum { get; set; }
        public string? Phone { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<Branch> Branches { get; set; }
    }
}
