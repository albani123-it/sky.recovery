using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Recovery
{
    public partial class ayda
    {
        public long Id { get; set; }
        public int? Loanid { get; set; }
        public int? Mstbranchid { get; set; }
        public int? Lastupdatedid { get; set; }
        public DateTime? Lastupdatedate { get; set; }
        public int? Mstbranchpembukuanid { get; set; }
        public int? Mstbanchprosesid { get; set; }
        public int? Hubunganbankid { get; set; }
        public DateTime? Tglambilalih { get; set; }
        public string Kualitas { get; set; }
        public decimal? Nilaipembiayaanpokok { get; set; }
        public decimal? Nilaimargin { get; set; }
        public decimal? Nilaiperolehanagunan { get; set; }
        public decimal? Perkiraanbiayajual { get; set; }
        public decimal? Ppa { get; set; }
        public decimal? Jumlahayda { get; set; }
        public int? Statusid { get; set; }
        public int? Createdby { get; set; }
        public DateTime? Createddated { get; set; }
        public int? Isactive { get; set; }
    }
}
