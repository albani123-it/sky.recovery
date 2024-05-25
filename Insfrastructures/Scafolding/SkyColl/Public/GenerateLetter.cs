using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Public
{
    public partial class GenerateLetter
    {
        public int Id { get; set; }
        public int? LoanId { get; set; }
        public string TypeLetter { get; set; }
        public string No { get; set; }
        public string Tgl { get; set; }
        public string NamaNasabah { get; set; }
        public string Jumlah { get; set; }
        public string Terbilang { get; set; }
        public string TglBayar { get; set; }
        public string NoKredit { get; set; }
        public string TglKredit { get; set; }
        public string Notaris { get; set; }
        public string NotarisDi { get; set; }
        public string NotarisTgl { get; set; }
        public string NoSp1 { get; set; }
        public string TglSp1 { get; set; }
        public string NoSp2 { get; set; }
        public string TglSp2 { get; set; }
        public int? StatusId { get; set; }
        public string Cabang { get; set; }
        public string CabangAlamat { get; set; }
        public string CabangTelepon { get; set; }
        public string CabangFaks { get; set; }
        public string CabangKota { get; set; }
    }
}
