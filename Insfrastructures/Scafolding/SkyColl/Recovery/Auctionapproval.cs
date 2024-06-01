using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Recovery
{
    public partial class Auctionapproval
    {
        public long Id { get; set; }
        public int? Auctionid { get; set; }
        public int? Senderroleid { get; set; }
        public int? Recipientroleid { get; set; }
        public int? Senderid { get; set; }
        public int? Recipientid { get; set; }
        public int? Executionid { get; set; }
        public string Komentar { get; set; }
        public DateTime? Createddated { get; set; }
    }
}
