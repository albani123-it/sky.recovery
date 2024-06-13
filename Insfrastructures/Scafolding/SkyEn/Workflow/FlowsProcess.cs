using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyEn.Workflow
{
    public partial class FlowsProcess
    {
        public long FlpId { get; set; }
        public string FlpAppId { get; set; }
        public long? FlpFlhId { get; set; }
        public string FlpNodesId { get; set; }
        public string FlpNodesType { get; set; }
        public string FlpNodesText { get; set; }
        public short? FlpNodesLeft { get; set; }
        public short? FlpNodesTop { get; set; }
        public short? FlpNodesW { get; set; }
        public short? FlpNodesH { get; set; }
        public DateTime? FlpCreatedDate { get; set; }
    }
}
