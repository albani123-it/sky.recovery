using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace sky.recovery.Entities
{
    [Table("restrukture", Schema = "RecoveryBusinessV2")]

    public class restructure
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]

        public int id { get; set; }
        public int? loanid { get; set; }
        public int? mstbranchid { get; set; }
        public int? lastupdatedid { get; set; }
        public int? mstbranchpembukuanid { get; set; }
        public int? mstbranchprosesid { get; set; }
        public int? marginpembayaran { get; set; }
        public int? principalpinalty { get; set; }
        public DateTime? tgljatuhtempobaru { get; set; }
        public string keterangan { get; set; }
        public int? graceperiode { get; set; }
        public int? pengurangannilaimargin { get; set; }
        public DateTime? tglawalperiodediskon { get; set; }
        public DateTime? tglakhirperiodediskon { get; set; }
        public int? periodediskon { get; set; }
        public DateTime? valuedate { get; set; }
        public int? disctunggakanmargin { get; set; }
        public int? disctunggakandenda { get; set; }
        public int? margin { get; set; }
        public int? denda { get; set; }
        public int? totaldiskonmargin { get; set; }
        public int? polarestrukturid { get; set; }
        public int? pembayarangpid { get; set; }
        public int? jenispenguranganid { get; set; }
        public string permasalahan { get; set; }
        public int? createdby { get; set; }
        public DateTime? createddated { get; set; }
        public int? checkby { get; set; }
        public DateTime? checkdate { get; set; }
        public int? approvedby { get; set; }
        public int? statusid { get; set; }
        public int? principalpembayaran { get; set; }
        public int? approver { get; set; }
        public int? approverrole { get; set; }
        public int? requesterrole { get; set; }
        public DateTime? statusmodifydated { get; set; }
       
        
    
    }
}
