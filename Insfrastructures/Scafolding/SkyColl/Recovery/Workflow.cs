using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Recovery
{
    public partial class Workflow
    {
        public long Id { get; set; }
        public int? Fiturid { get; set; }
        public long? Requestid { get; set; }
        public int? Flowid { get; set; }
        public int? Actor { get; set; }
        public int? Status { get; set; }
        public DateTime? Submitdated { get; set; }
        public DateTime? Modifydated { get; set; }
        public string Reason { get; set; }
        public int? Orders { get; set; }
        public int? Masterworkflowid { get; set; }
    }
}
