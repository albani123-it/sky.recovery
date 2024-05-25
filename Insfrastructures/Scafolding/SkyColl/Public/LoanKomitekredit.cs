using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Public
{
    public partial class LoanKomitekredit
    {
        public int Id { get; set; }
        public int? LoanId { get; set; }
        public string AccNo { get; set; }
        public string NomorPk { get; set; }
        public DateTime? TanggalPk { get; set; }
        public string Komite01 { get; set; }
        public string Komite02 { get; set; }
        public string Komite03 { get; set; }
        public string Komite04 { get; set; }
        public string Komite05 { get; set; }
        public string Komite06 { get; set; }
        public DateTime? StgDate { get; set; }
    }
}
