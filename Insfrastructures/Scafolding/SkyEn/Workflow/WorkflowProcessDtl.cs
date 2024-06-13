using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyEn.Workflow
{
    public partial class WorkflowProcessDtl
    {
        public long WfdId { get; set; }
        public long? WfdHeaderid { get; set; }
        public long? WfpObjid { get; set; }
        public DateTime? WfpStartdate { get; set; }
        public DateTime? WfpEnddate { get; set; }
        public string WfpStatus { get; set; }
    }
}
