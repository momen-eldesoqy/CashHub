using System;
using System.Collections.Generic;

namespace CashHub.Models
{
    public partial class Transaction
    {
        public int TransId { get; set; }
        public int? TransType { get; set; }
        public decimal? Amount { get; set; }
        public string? Description { get; set; }
        public int? SubId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual Line? Sub { get; set; }
    }
}
