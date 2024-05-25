using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Public
{
    public partial class LoanPk
    {
        public int Id { get; set; }
        public int? LoanId { get; set; }
        public string AccNo { get; set; }
        public DateTime? TanggalPk { get; set; }
        public string NomorPk { get; set; }
        public string NamaNotaris { get; set; }
        public string NamaLegal { get; set; }
        public DateTime? StgDate { get; set; }
    }
}
