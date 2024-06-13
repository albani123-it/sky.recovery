using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyEn.Workflow
{
    public partial class WorkflowPending
    {
        public long WfpId { get; set; }
        public string WfpRshid { get; set; }
        public string WfpFlowid { get; set; }
        public string WfpTtable { get; set; }
        public string WfpNode { get; set; }
        public string WfpGccode { get; set; }
        public string WfpInfo { get; set; }
        public string WfpMessage { get; set; }
        public DateTime? WfpPendingTime { get; set; }
        public DateTime? WfpProcessTime { get; set; }
    }
}
