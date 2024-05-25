using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Public
{
    public partial class PaymentHistoryBk20240513
    {
        public int Id { get; set; }
        public int? LoanId { get; set; }
        public string AccNo { get; set; }
        public DateTime? Tgl { get; set; }
        public double? PokokCicilan { get; set; }
        public double? Bunga { get; set; }
        public double? Denda { get; set; }
        public double? TotalBayar { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? CallBy { get; set; }
    }
}
