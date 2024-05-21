using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sky.recovery.Entities
{
    [Table("ayda", Schema = "RecoveryBusinessV2")]

    public class ayda
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
        public branch? branch{ get; set; }
        public int? lastupdatedid { get; set; }
        public DateTime? lastupdatedate { get; set; }
        public int? mstbranchpembukuanid { get; set; }
        public int? mstbanchprosesid { get; set; }
        public int? hubunganbankid { get; set; }
        public DateTime? tglambilalih { get; set; }
        public string kualitas { get; set; }
        public decimal? nilaipembiayaanpokok { get; set; }
        public decimal? nilaimargin { get; set; }
        public decimal? nilaiperolehanagunan { get; set; }
        public decimal? perkiraanbiayajual { get; set; }
        public decimal? ppa { get; set; }
        public decimal? jumlahayda { get; set; }
        public int? statusid { get; set; }
        [ForeignKey(nameof(statusid))]
        public status? status { get; set; }
        public int? createdby { get; set; }
        public DateTime? createddated { get; set; }
    }
}
