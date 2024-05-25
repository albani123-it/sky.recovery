using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Public
{
    public partial class PaymentRecord
    {
        public int Id { get; set; }
        public int? CallId { get; set; }
        public string AccNo { get; set; }
        public double? Amount { get; set; }
        public DateTime? RecordDate { get; set; }
        public int? CallBy { get; set; }
    }
}
