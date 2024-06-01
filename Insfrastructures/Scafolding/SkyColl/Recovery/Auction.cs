using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Recovery
{
    public partial class Auction
    {
        public long Id { get; set; }
        public int? Loanid { get; set; }
        public int? Mstbranchid { get; set; }
        public int? Lastupdatedid { get; set; }
        public DateTime? Lastupdatedate { get; set; }
        public int? Mstbranchpembukuanid { get; set; }
        public int? Mstbanchprosesid { get; set; }
        public int? Alasanlelangid { get; set; }
        public string Nopk { get; set; }
        public decimal? Nilailimitlelang { get; set; }
        public decimal? Uangjaminan { get; set; }
        public string Objeklelang { get; set; }
        public string Keterangan { get; set; }
        public int? Balailelangid { get; set; }
        public int? Jenislelangid { get; set; }
        public string Tatacaralelang { get; set; }
        public decimal? Biayalelang { get; set; }
        public string Catatanlelang { get; set; }
        public DateTime? Tglpenetapanlelang { get; set; }
        public string Norekening { get; set; }
        public string Namarekening { get; set; }
        public int? Statusid { get; set; }
        public int? Createdby { get; set; }
        public DateTime? Createddated { get; set; }
        public int? Isactive { get; set; }
    }
}
