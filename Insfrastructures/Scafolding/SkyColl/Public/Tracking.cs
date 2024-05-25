using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Public
{
    public partial class Tracking
    {
        public int TrxId { get; set; }
        public int? TrxUsrid { get; set; }
        public DateTime? TrxTgl { get; set; }
        public double? TrxLat { get; set; }
        public double? TrxLon { get; set; }
    }
}
