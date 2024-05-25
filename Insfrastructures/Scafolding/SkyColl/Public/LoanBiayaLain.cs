using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Public
{
    public partial class LoanBiayaLain
    {
        public int Id { get; set; }
        public int? LoanId { get; set; }
        public string AccNo { get; set; }
        public DateTime? TanggalBiayaLain { get; set; }
        public string NamaBiayaLain { get; set; }
        public double? NominalBiayaLain { get; set; }
        public DateTime? StgDate { get; set; }
    }
}
