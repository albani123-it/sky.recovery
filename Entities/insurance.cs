using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sky.recovery.Entities
{
    [Table("insurance", Schema = "RecoveryBusinessV2")]

    public class insurance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        [ForeignKey(nameof(loanid))]
        public master_loan? master_loan { get; set; }
        public int? loanid { get; set; }
        public int? mstbranchid { get; set; }
        public int? lastupdatedid { get; set; }
        public DateTime? lastupdateddated { get; set; }
        public int? mstbranchpembukuanid { get; set; }
        public int? mstbranchprosesid { get; set; }
        public string namapejabat { get; set; }
        public string jabatan { get; set; }
        public string nosertifikat { get; set; }
        public string tglsertifikat { get; set; }
        public int? asuransiid { get; set; }
        public string nopolis { get; set; }
        public string tglpolis { get; set; }
        public string nopk { get; set; }
        public decimal? nilaitunggakanpokok { get; set; }
        public decimal? nilaitunggakanbunga {get;set;}
        public string catatanpolis { get; set; }
        public string keterangan { get; set; }
        public decimal? nilaiklaim { get; set; }
        public decimal? nilaiklaimdibayar { get; set; }
        public int? asuransisisaklaimid { get; set; }
        public decimal? bakidebitklaim { get; set; }
        public string catatanklaim { get; set; }
        public string permasalahan { get; set; }
        public int? statusid { get; set; }
        [ForeignKey(nameof(statusid))]
        public status? status { get; set; }
        public int? createdby { get; set; }
        public DateTime? createddated { get; set; }
        public int? isactive { get; set; }
    }
}
