using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Recovery
{
    public partial class Workflowhistory
    {
        public long Id { get; set; }
        public int? Workflowid { get; set; }
        public int? Actor { get; set; }
        public int? Status { get; set; }
        public DateTime? Dated { get; set; }
        public string Reason { get; set; }
    }
}
