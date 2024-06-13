using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyEn.Workflow
{
    public partial class Flows
    {
        public short FlhId { get; set; }
        public string FlhName { get; set; }
        public string FlhStatus { get; set; }
        public string FlhDesc { get; set; }
        public string FlhType { get; set; }
        public string FlhCode { get; set; }
        public string FlhVersionCode { get; set; }
        public string FlhApproverStatus { get; set; }
        public bool? FlnIsdelete { get; set; }
        public string FlhApplied { get; set; }
        public string FlhCategory { get; set; }
    }
}
