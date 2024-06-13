using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyEn.Workflow
{
    public partial class WfCancelRequest
    {
        public long WcrId { get; set; }
        public DateTime? WcrReqdate { get; set; }
        public string WcrRequestby { get; set; }
        public DateTime? WcrApprdate { get; set; }
        public string WcrApprovedby { get; set; }
        public string WcrRshid { get; set; }
        public string WcrLastNode { get; set; }
        public string WcrDecisionCancel { get; set; }
        public string WcrNotes { get; set; }
    }
}
