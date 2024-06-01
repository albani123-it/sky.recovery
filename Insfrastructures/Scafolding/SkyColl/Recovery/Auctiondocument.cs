using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Recovery
{
    public partial class Auctiondocument
    {
        public long Id { get; set; }
        public int? Auctionid { get; set; }
        public int? Doctypeid { get; set; }
        public string Title { get; set; }
        public string Descriptions { get; set; }
        public string Url { get; set; }
        public string Mime { get; set; }
        public double? Lat { get; set; }
        public double? Lon { get; set; }
        public int? Uploadedby { get; set; }
        public DateTime? Uploadeddated { get; set; }
    }
}
