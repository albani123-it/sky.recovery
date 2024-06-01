using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Recovery
{
    public partial class Insurancedocument
    {
        public long Id { get; set; }
        public int? Insuranceid { get; set; }
        public int? Doctypeid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Mime { get; set; }
        public double? Lat { get; set; }
        public double? Lon { get; set; }
        public int? Uploadedby { get; set; }
        public DateTime? Upladeddated { get; set; }
    }
}
