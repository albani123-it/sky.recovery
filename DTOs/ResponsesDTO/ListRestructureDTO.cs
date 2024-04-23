using sky.recovery.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace sky.recovery.DTOs.ResponsesDTO
{
    public class ListCashFlowDTO
    {
        public int rsc_id { get; set; }
        public int? rsc_penghasilan_nasabah { get; set; }
       // public int? rsc_penghasilan_bersih { get; set; }

        public string Permasalahan { get; set; }
        public int? rsc_penghasilan_pasangan { get; set; }
        public int? rsc_penghasilan_lainnya { get; set; }
        public int? rsc_total_penghasilan { get; set; }
        public int? rsc_biaya_pendidikan { get; set; }
        public int? rsc_biaya_listrik_air_telp { get; set; }
        public int? rsc_biaya_belanja { get; set; }

        public int? rsc_biaya_transportasi { get; set; }
        public int? rsc_biaya_lainnya { get; set; }
        public int? rsc_total_pengeluaran { get; set; }
        public int? rsc_hutang_di_bank { get; set; }
        public int? rsc_cicilan_lainnya { get; set; }
        public int? rsc_total_kewajiban { get; set; }
        public int? rsc_pengasilan_bersih { get; set; }
        public int? rsc_rpc_70_persen { get; set; }
      
        public int? rsc_restructure_id { get; set; }
    }

    public class ListRestructureDTO
    {
        public int rst_id { get; set; }
        public string acc_no { get; set; }
        public string cucif { get; set; }
      public int? loanId { get; set; }
public string BranchName { get; set; }
        public string Nasabah { get; set; }
     public string Status { get; set; }
        public int? CustomerId { get; set; }
        public string PolaRestruktur { get; set; }
     
    }
}
