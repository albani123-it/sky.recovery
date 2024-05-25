using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Public
{
    public partial class Insurance
    {
        public long Id { get; set; }
        public int? LoanId { get; set; }
        public int? MstBranchId { get; set; }
        public int? LastUpdateId { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public int? MstBranchPembukuanId { get; set; }
        public int? MstBranchProsesId { get; set; }
        public string NamaPejabat { get; set; }
        public string Jabatan { get; set; }
        public string NoSertifikat { get; set; }
        public DateTime? TglSertifikat { get; set; }
        public int? AsuransiId { get; set; }
        public string NoPolis { get; set; }
        public DateTime? TglPolis { get; set; }
        public string NoPk { get; set; }
        public double? NilaiTunggakanPokok { get; set; }
        public double? NilaiTunggakanBunga { get; set; }
        public string CatatanPolis { get; set; }
        public string Keterangan { get; set; }
        public double? NilaiKlaim { get; set; }
        public double? NilaiKlaimDibayar { get; set; }
        public DateTime? TglKlaimDibayar { get; set; }
        public int? AsuransiSisaKlaimId { get; set; }
        public double? BakiDebitKlaim { get; set; }
        public string CatatanKlaim { get; set; }
        public string Permasalahan { get; set; }
        public int? StatusId { get; set; }
    }
}
