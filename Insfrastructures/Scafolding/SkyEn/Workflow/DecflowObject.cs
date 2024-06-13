using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyEn.Workflow
{
    public partial class DecflowObject
    {
        public long DeoId { get; set; }
        public string DeoObjId { get; set; }
        public string DeoFieldName { get; set; }
        public string DeoType { get; set; }
        public DateTime? DeoTimestamp { get; set; }
    }
}
