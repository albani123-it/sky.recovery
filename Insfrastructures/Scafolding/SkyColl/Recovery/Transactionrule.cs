using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Recovery
{
    public partial class Transactionrule
    {
        public long Id { get; set; }
        public string Rulecode { get; set; }
        public string Rulename { get; set; }
        public DateTime? Createddated { get; set; }
        public int? Createdby { get; set; }
        public int? Isactive { get; set; }
    }
}
