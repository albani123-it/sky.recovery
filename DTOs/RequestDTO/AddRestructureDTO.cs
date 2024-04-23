using System.Collections.Generic;
using System;

namespace sky.recovery.DTOs.RequestDTO
{
    public class AddRestructureDTO
    {
    }
    public class CreateNewRestructure
    {
        public string UserId { get; set; }
        public int LoanId { get; set; }

        public int? PrincipalPembayaran { get; set; }

        public int? MarginPembayaran { get; set; }

        public int? PrincipalPinalty { get; set; }

        public int? MarginPinalty { get; set; }

        public DateTime? TglJatuhTempoBaru { get; set; }

        public string? Keterangan { get; set; }

        public int? GracePeriode { get; set; }

        public int? PenguranganNilaiMargin { get; set; }

        public DateTime? TglAwalPeriodeDiskon { get; set; }

        public DateTime? TglAkhirPeriodeDiskon { get; set; }

        public int? PeriodeDiskon { get; set; }

        public DateTime? ValueDate { get; set; }

        public int? DiskonTunggakanMargin { get; set; }

        public int? DiskonTunggakanDenda { get; set; }

        public int? Margin { get; set; }

        public int? Denda { get; set; }

        public int? MarginAmount { get; set; }

        public int? TotalDiskonMargin { get; set; }

        public int? PolaRestrukId { get; set; }

        public int? PembayaranGpId { get; set; }

        public int? JenisPenguranganId { get; set; }

        public List<string>? Permasalahan { get; set; }

        public RestructureNewCashFlowBean? CashFlow { get; set; }

        public List<int>? Document { get; set; }
    }

    public class RestructureNewCashFlowBean
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
}
