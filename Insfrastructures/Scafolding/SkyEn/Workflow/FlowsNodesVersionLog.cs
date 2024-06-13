using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyEn.Workflow
{
    public partial class FlowsNodesVersionLog
    {
        public long FnvId { get; set; }
        public long? FlnId { get; set; }
        public long? FlnFlhId { get; set; }
        public string FlnNodesId { get; set; }
        public string FlnNodesType { get; set; }
        public string FlnNodesText { get; set; }
        public decimal? FlnNodesLeft { get; set; }
        public decimal? FlnNodesTop { get; set; }
        public decimal? FlnNodesW { get; set; }
        public decimal? FlnNodesH { get; set; }
        public string FnvUserid { get; set; }
        public DateTime? FnvTimestamp { get; set; }
        public string FnvVersionCode { get; set; }
    }
}
