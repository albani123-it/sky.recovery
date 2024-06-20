using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Recovery
{
    public partial class Recoveryreverselog
    {
        public long RrlId { get; set; }
        public string RrlData { get; set; }
        public DateTime? RrlDated { get; set; }
        public int? RrlCreatedby { get; set; }
        public int? Versions { get; set; }
    }
}
