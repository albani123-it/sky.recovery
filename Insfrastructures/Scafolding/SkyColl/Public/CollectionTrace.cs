using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Public
{
    public partial class CollectionTrace
    {
        public int Id { get; set; }
        public int? CallId { get; set; }
        public string AccNo { get; set; }
        public int? Result { get; set; }
        public double? Amount { get; set; }
        public DateTime? TraceDate { get; set; }
        public int? Kolek { get; set; }
        public int? Dpd { get; set; }
        public int? CallBy { get; set; }
    }
}
