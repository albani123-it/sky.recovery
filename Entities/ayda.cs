using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sky.recovery.Entities
{
    public class ayda
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int id { get; set; }
        public int? loan_id { get; set; }
        [ForeignKey(nameof(loan_id))]
        public master_loan? master_loan { get; set; }
        public int? mst_branch_id { get; set; }
        [ForeignKey(nameof(mst_branch_id))]
        public branch? branch{ get; set; }
        public int? last_update_id { get; set; }
        public DateTime? last_update_date { get; set; }
        public int? mst_branch_pembukuan_id { get; set; }
        public int? mst_branch_proses_id { get; set; }
        public int? hubungan_bank_id { get; set; }
        public DateTime? tgl_ambil_alih { get; set; }
        public string kualitas { get; set; }
        public decimal? nilai_pembiayaan_pokok { get; set; }
        public decimal? nilai_margin { get; set; }
        public decimal? nilai_perolehan_agunan { get; set; }
        public decimal? perkiraan_biaya_jual { get; set; }
        public decimal? ppa { get; set; }
        public decimal? jumlah_ayda { get; set; }
        public int? status_id { get; set; }
        [ForeignKey(nameof(status_id))]
        public status? status { get; set; }
    }
}
