using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyEn.Workflow
{
    public partial class AppValueRac
    {
        public long AvrId { get; set; }
        public long? RshId { get; set; }
        public string TmpTable { get; set; }
        public string WorkflowCode { get; set; }
        public DateTime? StartDate { get; set; }
        public string AvrKriteria { get; set; }
        public string AvrValueRecommendation { get; set; }
        public string AvrValueEntry { get; set; }
        public string AvrResult { get; set; }
    }
}
