using System;
using System.Collections.Generic;

namespace CashHub.Models
{
    public partial class Branch
    {
        public Branch()
        {
            Subs = new HashSet<Sub>();
        }

        public int BranchId { get; set; }
        public string BranchName { get; set; } = null!;
        public string? BranchAddress { get; set; }
        public string? BranchRefNum { get; set; }
        public string? BranchPhone { get; set; }
        public int? HubId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual Hub? Hub { get; set; }
        public virtual ICollection<Sub> Subs { get; set; }
    }
}
