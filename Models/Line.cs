using System;
using System.Collections.Generic;

namespace CashHub.Models
{
    public partial class Line
    {
        public Line()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int LineId { get; set; }
        public string? LineNumber { get; set; }
        public decimal? LineBalance { get; set; }
        public decimal? LineMaxAmount { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? LineId1 { get; set; }

        public virtual Sub? LineId1Navigation { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
