using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyEn.Workflow
{
    public partial class FlowsEdgesVersionLog
    {
        public long FevId { get; set; }
        public long? FleId { get; set; }
        public long? FleFlhId { get; set; }
        public string FleEdgesSource { get; set; }
        public string FleEdgesTarget { get; set; }
        public string FleEdgesLabel { get; set; }
        public string FleEdgesId { get; set; }
        public string FleEdgesType { get; set; }
        public string FevUserid { get; set; }
        public DateTime? FevTimestamp { get; set; }
        public string FevVersionCode { get; set; }
        public string FleEdgesSourcehandle { get; set; }
        public string FleEdgesTargethandle { get; set; }
    }
}
