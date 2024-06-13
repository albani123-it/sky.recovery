using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyEn.Workflow
{
    public partial class WorkflowProcess
    {
        public long WfpId { get; set; }
        public string WfpProduct { get; set; }
        public long? WfpWfid { get; set; }
        public string WfpMobilePhone { get; set; }
        public DateTime? WfpStartdate { get; set; }
        public DateTime? WfpEnddate { get; set; }
        public string WfpProcessBy { get; set; }
        public string WfpStatus { get; set; }
        public DateTime? WfpCreatedate { get; set; }
        public string WfpTmpTable { get; set; }
    }
}
