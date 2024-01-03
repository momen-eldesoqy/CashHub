using System;
using System.Collections.Generic;

namespace CashHub.Models
{
    public partial class Sub
    {
        public Sub()
        {
            Lines = new HashSet<Line>();
        }

        public int SubId { get; set; }
        public string SubName { get; set; } = null!;
        public string? SubAddress { get; set; }
        public string? SubRefNum { get; set; }
        public string? SubPhone { get; set; }
        public int? BranchId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual Branch? Branch { get; set; }
        public virtual ICollection<Line> Lines { get; set; }
    }
}
