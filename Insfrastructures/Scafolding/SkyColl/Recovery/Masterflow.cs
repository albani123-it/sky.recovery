using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Recovery
{
    public partial class Masterflow
    {
        public long Id { get; set; }
        public int? Orders { get; set; }
        public int? Fiturid { get; set; }
        public string Descriptions { get; set; }
        public int? Roleid { get; set; }
        public string Branchid { get; set; }
        public int? Masterworkflowid { get; set; }
    }
}
