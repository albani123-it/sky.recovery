using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace sky.recovery.Entities
{
    public class auction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int id { get; set; }
        public int? loanid { get; set; }
        [ForeignKey(nameof(loanid))]
        public master_loan? master_loan { get; set; }
        public int? mstbranchid { get; set; }
        [ForeignKey(nameof(mstbranchid))]
        public branch? branch { get; set; }
        public int? lastupdateid { get; set; }
        public DateTime? lastupdatedate { get; set; }
        public int? mstbranchpembukuanid { get; set; }
        public int? mstbranchprosesid { get; set; }
        public int? alasanlelangid { get; set; }
        public string nopk { get; set; }
        public double? nilailimitlelang { get; set; }
        public double? uangjaminan { get; set; }
        public string objeklelang { get; set; }
        public string keterangan { get; set; }
        public int? balailelangid { get; set; }
        public int? jenislelangid { get; set; }
        public string tatacaralelang { get; set; }
        public double? biayalelang { get; set; }
        public string catatanlelang { get; set; }
        public DateTime? tglpenetapanlelang { get; set; }
        public string norekening { get; set; }
        public string namarekening { get; set; }
        public int? statusid { get; set; }
        [ForeignKey(nameof(statusid))]
        public status? status { get; set; }

        public int? createdby { get; set; }
        public DateTime? createddated { get; set; }
        public int? isactive { get; set; }
    }
}
