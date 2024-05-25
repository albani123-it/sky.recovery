using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Public
{
    public partial class LoanTagihanLain
    {
        public int Id { get; set; }
        public int? LoanId { get; set; }
        public string AccNo { get; set; }
        public DateTime? TanggalTl { get; set; }
        public string NamaTl { get; set; }
        public double? NominalTl { get; set; }
        public DateTime? StgDate { get; set; }
    }
}
