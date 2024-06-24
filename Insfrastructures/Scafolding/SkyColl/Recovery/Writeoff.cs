using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Recovery
{
    public partial class Writeoff
    {
        public long Id { get; set; }
        public int? Loanid { get; set; }
        public decimal? Interestrate { get; set; }
        public decimal? Chargesrate { get; set; }
        public decimal? Principal { get; set; }
        public decimal? Currentoverdue { get; set; }
        public int? Createdby { get; set; }
        public DateTime? Createddated { get; set; }
        public int? Modifiedby { get; set; }
        public DateTime? Modifieddated { get; set; }
        public bool? Isactive { get; set; }
        public int? Statusid { get; set; }
    }
}
