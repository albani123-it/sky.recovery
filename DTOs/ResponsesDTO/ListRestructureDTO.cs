using sky.recovery.Entities;
using System;

namespace sky.recovery.DTOs.ResponsesDTO
{
    public class ListRestructureDTO
    {
        public int rst_id { get; set; }
        public string acc_no { get; set; }
        public string cucif { get; set; }
        public int? rst_loan_id { get; set; }

        public int? rst_mst_branch_id { get; set; }
public string BranchName { get; set; }
        public string Nasabah { get; set; }
     
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
        public string permasalahan { get; set; }
        public int? createby_id { get; set; }
        public DateTime? create_date { get; set; }
     
        public int? rst_status_id { get; set; }
        public int? principal_pembayaran { get; set; }
    }
}
