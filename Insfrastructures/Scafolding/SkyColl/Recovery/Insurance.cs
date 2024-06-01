using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Recovery
{
    public partial class Insurance
    {
        public long Id { get; set; }
        public int? Loanid { get; set; }
        public int? Mstbranchid { get; set; }
        public int? Lastupdatedid { get; set; }
        public DateTime? Lastupdateddated { get; set; }
        public int? Mstbranchpembukuanid { get; set; }
        public int? Mstbranchprosesid { get; set; }
        public string Namapejabat { get; set; }
        public string Jabatan { get; set; }
        public string Nosertifikat { get; set; }
        public string Tglsertifikat { get; set; }
        public int? Asuransiid { get; set; }
        public string Nopolis { get; set; }
        public string Tglpolis { get; set; }
        public string Nopk { get; set; }
        public decimal? Nilaitunggakanpokok { get; set; }
        public decimal? Nilaitunggakanbunga { get; set; }
        public string Catatanpolis { get; set; }
        public string Keterangan { get; set; }
        public decimal? Nilaiklaim { get; set; }
        public decimal? Nilaiklaimdibayar { get; set; }
        public int? Asuransisisaklaimid { get; set; }
        public decimal? Bakidebitklaim { get; set; }
        public string Catatanklaim { get; set; }
        public string Permasalahan { get; set; }
        public int? Statusid { get; set; }
        public int? Createdby { get; set; }
        public DateTime? Createddated { get; set; }
        public int? Isactive { get; set; }
    }
}
