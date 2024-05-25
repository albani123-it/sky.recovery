using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Public
{
    public partial class MasterLoan
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int? PrdSegmentId { get; set; }
        public string CuCif { get; set; }
        public string AccNo { get; set; }
        public string Ccy { get; set; }
        public int? Product { get; set; }
        public double? Plafond { get; set; }
        public DateTime? MaturityDate { get; set; }
        public DateTime? StartDate { get; set; }
        public int? SisaTenor { get; set; }
        public int? Tenor { get; set; }
        public double? InstallmentPokok { get; set; }
        public double? InterestRate { get; set; }
        public double? Installment { get; set; }
        public double? TunggakanPokok { get; set; }
        public double? TunggakanBunga { get; set; }
        public double? TunggakanDenda { get; set; }
        public double? TunggakanTotal { get; set; }
        public double? KewajibanTotal { get; set; }
        public DateTime? LastPayDate { get; set; }
        public double? Outstanding { get; set; }
        public double? PayTotal { get; set; }
        public int? Dpd { get; set; }
        public int? Kolektibilitas { get; set; }
        public string EconaName { get; set; }
        public string EconPhone { get; set; }
        public string EconRelation { get; set; }
        public string MarketingCode { get; set; }
        public string ChannelBranchCode { get; set; }
        public int? NotarisId { get; set; }
        public DateTime? StgDate { get; set; }
        public string Fasilitas { get; set; }
        public int? Status { get; set; }
        public string PayinAccount { get; set; }
        public DateTime? FileDate { get; set; }
        public string LoanNumber { get; set; }
    }
}
