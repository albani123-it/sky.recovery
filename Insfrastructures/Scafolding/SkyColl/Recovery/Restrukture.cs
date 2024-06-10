using System;
using System.Collections.Generic;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Recovery
{
    public partial class Restrukture
    {
        public long Id { get; set; }
        public int? Loanid { get; set; }
        public int? Mstbranchid { get; set; }
        public int? Lastupdatedid { get; set; }
        public DateTime? Lastupdatedate { get; set; }
        public int? Mstbranchpembukuanid { get; set; }
        public int? Mstbranchprosesid { get; set; }
        public decimal? Marginpembayaran { get; set; }
        public decimal? Principalpinalty { get; set; }
        public decimal? Marginpinalty { get; set; }
        public DateTime? Tgljatuhtempobaru { get; set; }
        public string Keterangan { get; set; }
        public int? Graceperiode { get; set; }
        public int? Pengurangannilaimargin { get; set; }
        public DateTime? Tglawalperiodediskon { get; set; }
        public DateTime? Tglakhirperiodediskon { get; set; }
        public int? Periodediskon { get; set; }
        public DateTime? Valuedate { get; set; }
        public decimal? Disctunggakanmargin { get; set; }
        public decimal? Disctunggakandenda { get; set; }
        public decimal? Margin { get; set; }
        public decimal? Denda { get; set; }
        public decimal? Totaldiskonmargin { get; set; }
        public long? Polarestrukturid { get; set; }
        public int? Pembayarangpid { get; set; }
        public long? Jenispenguranganid { get; set; }
        public string Permasalahan { get; set; }
        public int? Createdby { get; set; }
        public DateTime? Createddated { get; set; }
        public int? Checkby { get; set; }
        public DateTime? Checkdate { get; set; }
        public int? Approvedby { get; set; }
        public int? Statusid { get; set; }
        public decimal? Principalpembayaran { get; set; }
        public int? Approver { get; set; }
        public int? Approverrole { get; set; }
        public int? Requesterrole { get; set; }
        public DateTime? Statusmodifydated { get; set; }
    }
}
