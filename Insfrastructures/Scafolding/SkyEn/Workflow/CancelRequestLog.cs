using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyEn.Workflow
{
    public partial class CancelRequestLog
    {
        public long CrlId { get; set; }
        public string CrlRshid { get; set; }
        public string CrlLastNode { get; set; }
        public string CrlLastDecision { get; set; }
        public string CrlDecisionCancel { get; set; }
        public string CrlNotes { get; set; }
        public string CrlUserid { get; set; }
        public DateTime? CrlTimestamp { get; set; }
    }
}
