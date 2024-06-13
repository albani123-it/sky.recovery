using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyEn.Workflow
{
    public partial class WorkflowDetailConnection
    {
        public long WfcId { get; set; }
        public long? WfcDtlid { get; set; }
        public string WfcObjConnection { get; set; }
        public string WfcParentNode { get; set; }
        public long? WfcHeaderid { get; set; }
    }
}
