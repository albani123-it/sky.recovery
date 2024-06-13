using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyEn.Workflow
{
    public partial class WorkflowDetail
    {
        public long WfdId { get; set; }
        public long? WfdHeaderid { get; set; }
        public string WfdObjType { get; set; }
        public string WfdObjCode { get; set; }
        public int? WfdObjOutput { get; set; }
        public long? WfdParentid { get; set; }
        public string WfdObjX { get; set; }
        public string WfdObjY { get; set; }
        public string WfdUpdatedby { get; set; }
        public DateTime? WfdUpdatedat { get; set; }
        public string WfdOutputPos { get; set; }
    }
}
