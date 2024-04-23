using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace sky.recovery.Entities
{
    public class restructure
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("rst_id")]
        public int rst_id { get; set; }
        public int? rst_loan_id { get; set; }

        [ForeignKey(nameof(rst_loan_id))]
        public master_loan master_loan { get; set; }

        public int? rst_mst_branch_id { get; set; }

        [ForeignKey(nameof(rst_mst_branch_id))]

        public branch branch { get; set; }

        public DateTime? last_update_date { get; set; }
        public int? mst_branch_pembukuan_id { get; set; }
        public int? mst_branch_proses_id { get; set; }
        public int? margin_pembayaran { get; set; }
        public int? principal_pinalty { get; set; }
        public int? margin_pinalty { get; set; }
        public DateTime? tgl_jatuh_tempo_baru { get; set; }
        public string keterangan { get; set; }
        public int? grace_periode { get; set; }
        public int? pengurangan_nilai_margin { get; set; }
        public DateTime? tgl_awal_periode_diskon { get; set; }
        public DateTime? tgl_akhir_periode_diskon { get; set; }
        public int? periode_diskon { get; set; }
        public DateTime? value_date { get; set; }
        public int? disc_tunggakan_margin { get; set; }
        public int? disc_tunggakan_denda { get; set; }
        public int? margin { get; set; }
        public int? denda { get; set; }
        public int? margin_amount { get; set; }
        public int? total_diskon_margin { get; set; }
        public int? rst_pola_restruk_id { get; set; }

        [ForeignKey(nameof(rst_pola_restruk_id))]
        public generic_param? generic_param_pola_restruk { get; set; }
        public int? pembayaran_gp_id { get; set; }
        public int? jenis_pengurangan_id { get; set; }
        public string permasalahan { get; set; }
        public int? createby_id { get; set; }
        public DateTime? create_date { get; set; }
        public int? checkby_id { get; set; }
        public DateTime? check_date { get; set; }
        public int? approveby_id { get; set; }
        public DateTime? approve_date { get; set; }
        public int? rst_status_id { get; set; }
        [ForeignKey(nameof(rst_status_id))]
        public status? status{ get; set; }
        public int? principal_pembayaran { get; set; }
    
    }
}
