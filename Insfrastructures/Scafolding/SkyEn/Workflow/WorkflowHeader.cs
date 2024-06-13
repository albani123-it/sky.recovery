using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyEn.Workflow
{
    public partial class WorkflowHeader
    {
        public long WfhId { get; set; }
        public string WfhName { get; set; }
        public string WfhDesc { get; set; }
        public string WfhParamStart { get; set; }
        public bool? WfhIsactive { get; set; }
        public string WfhCreatedby { get; set; }
        public string WfhUpdatedby { get; set; }
        public DateTime? WfhCreatedat { get; set; }
        public DateTime? WfhUpdatedat { get; set; }
    }
}
