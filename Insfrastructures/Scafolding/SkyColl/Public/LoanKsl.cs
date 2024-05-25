using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Public
{
    public partial class LoanKsl
    {
        public int Id { get; set; }
        public int? LoanId { get; set; }
        public string AccNo { get; set; }
        public DateTime? TanggalKsl { get; set; }
        public string NamaKsl { get; set; }
        public string SaldoKsl { get; set; }
        public DateTime? StgDate { get; set; }
    }
}
