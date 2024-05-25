using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Public
{
    public partial class LoanKodeao
    {
        public int Id { get; set; }
        public int? LoanId { get; set; }
        public string AccNo { get; set; }
        public string KodeAo { get; set; }
        public DateTime? TanggalAo { get; set; }
        public DateTime? StgDate { get; set; }
    }
}
