using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyEn.Workflow
{
    public partial class DeclowParameterResult
    {
        public long DprId { get; set; }
        public string DprDecflowCode { get; set; }
        public string DprDecflowField { get; set; }
        public DateTime? DprTimestamp { get; set; }
    }
}
