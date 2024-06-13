using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyEn.Workflow
{
    public partial class FlowsEdges
    {
        public short FleId { get; set; }
        public int? FleFlhId { get; set; }
        public string FleEdgesSource { get; set; }
        public string FleEdgesTarget { get; set; }
        public string FleEdgesLabel { get; set; }
        public string FleEdgesId { get; set; }
        public string FleEdgesType { get; set; }
        public string FleEdgesSourcehandle { get; set; }
        public string FleEdgesTargethandle { get; set; }
    }
}
