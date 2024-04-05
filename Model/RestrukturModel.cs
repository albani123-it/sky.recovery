using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace sky.recovery.Model
{
    public class UploadDocumentRestructure
    {
        public int? DocTypeId { get; set; }

        public IFormFile? File { get; set; }
    }

    // public class RestructureDocument
    // {
    //     public long 
    // }
    
    public class CreateRestructure
    {
        public int LoanId { get; set; }

        public double? PrincipalPembayaran { get; set; }

        public double? MarginPembayaran { get; set; }

        public double? PrincipalPinalty { get; set; }

        public double? MarginPinalty { get; set; }

        public DateTime? TglJatuhTempoBaru { get; set; }

        public string? Keterangan { get; set; }

        public int? GracePeriode { get; set; }

        public int? PenguranganNilaiMargin { get; set; }

        public DateTime? TglAwalPeriodeDiskon { get; set; }

        public DateTime? TglAkhirPeriodeDiskon { get; set; }

        public int? PeriodeDiskon { get; set; }

        public DateTime? ValueDate { get; set; }

        public double? DiskonTunggakanMargin { get; set; }

        public double? DiskonTunggakanDenda { get; set; }

        public double? Margin { get; set; }

        public double? Denda { get; set; }

        public double? MarginAmount { get; set; }

        public double? TotalDiskonMargin { get; set; }

        public int? PolaRestrukId { get; set; }

        public int? PembayaranGpId { get; set; }

        public int? JenisPenguranganId { get; set; }

        public List<string>? Permasalahan { get; set; }

        public RestructureCashFlowBean? CashFlow { get; set; }

        public List<int>? Document { get; set; }
    }
    
    public class RestructureCashFlowBean
    {
        public int? PenghasilanNasabah { get; set; }

        public int? PenghasilanPasangan { get; set; }

        public int? PenghasilanLainnya { get; set; }


        public int? TotalPenghasilan { get; set; }

        public int? BiayaPendidikan { get; set; }

        public int? BiayaListrikAirTelp { get; set; }

        public int? BiayaBelanja { get; set; }

        public int? BiayaTransportasi { get; set; }

        public int? BiayaLainnya { get; set; }

        public int? TotalPengeluaran { get; set; }

        public int? HutangDiBank { get; set; }

        public int? CicilanLainnya { get; set; }

        public int? TotalKewajiban { get; set; }

        public int? PenghasilanBersih { get; set; }

        public int? Rpc70Persen { get; set; }
    }

    public class cekLoan
    {
        public int v_id { get; set; }
        public string v_cu_cif { get; set; }
        public string v_acc_no { get; set; }
        public double v_tunggakan_pokok { get; set; }
        public double v_tunggakan_denda { get; set; }
        public double v_tunggakan_total { get; set; }
        public double v_kewajiban_total { get; set; }
        public int v_dpd { get; set; }
        public int v_kolek { get; set; }
    }
}