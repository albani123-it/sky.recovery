using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyEn.Workflow
{
    public partial class RptProcessLogPic
    {
        public long PlpId { get; set; }
        public long PlpRshId { get; set; }
        public string PlpAppNo { get; set; }
        public string PlpPic { get; set; }
        public string PlpProcess { get; set; }
        public DateTime? PlpStart { get; set; }
        public DateTime? PlpEnd { get; set; }
        public string PlpSla { get; set; }
        public string PlpStatus { get; set; }
        public string PlpDecision { get; set; }
        public DateTime? PlpCreatedDate { get; set; }
    }
}
