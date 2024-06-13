using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyEn.Workflow
{
    public partial class WorkflowDetailParaminput
    {
        public long WdpId { get; set; }
        public long? WdpDtlid { get; set; }
        public int? WdpMapid { get; set; }
        public string WdpLabel { get; set; }
    }
}
