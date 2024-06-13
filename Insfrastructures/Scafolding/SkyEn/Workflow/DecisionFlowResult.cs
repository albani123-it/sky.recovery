using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyEn.Workflow
{
    public partial class DecisionFlowResult
    {
        public long DfrId { get; set; }
        public long? DfrRshId { get; set; }
        public int? DfrWfId { get; set; }
        public int? DfrCounter { get; set; }
        public string DfrSource { get; set; }
        public string DfrLabel { get; set; }
        public DateTime? DfrDate { get; set; }
        public string DfrResult { get; set; }
    }
}
