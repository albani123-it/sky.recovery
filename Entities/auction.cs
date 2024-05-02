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
        public int? loan_id { get; set; }
        [ForeignKey(nameof(loan_id))]
        public master_loan? master_loan { get; set; }
        public int? mst_branch_id { get; set; }
        [ForeignKey(nameof(mst_branch_id))]
        public branch? branch { get; set; }
        public int? last_update_id { get; set; }
        public DateTime? last_update_date { get; set; }
        public int? mst_branch_pembukuan_id { get; set; }
        public int? mst_branch_proses_id { get; set; }
        public int? alasan_lelang_id { get; set; }
        public string no_pk { get; set; }
        public double? nilai_limit_lelang { get; set; }
        public double? uang_jaminan { get; set; }
        public string objek_lelang { get; set; }
        public string keterangan { get; set; }
        public int? balai_lelang_id { get; set; }
        public int? jenis_lelang_id { get; set; }
        public string tata_cara_lelang { get; set; }
        public double? biaya_lelang { get; set; }
        public string catatan_lelang { get; set; }
        public DateTime? tgl_penetapan_lelang { get; set; }
        public string no_rekening { get; set; }
        public string nama_rekening { get; set; }
        public int? status_id { get; set; }
        [ForeignKey(nameof(status_id))]
        public status? status { get; set; }
    }
}
