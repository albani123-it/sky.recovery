using System;
using System.Collections.Generic;

namespace sky.recovery.DTOs.ResponsesDTO
{
    public class DetailNasabah
    {
        public string NoKTP { get; set; }
        public string Nasabah { get; set; }
        public string cucif { get; set; }
        public string accno { get; set; }
        public string loannumber { get; set; }
        public DateTime? TanggalLahir { get; set; }
        public string Alamat { get; set; }
        public string noTelp { get; set; }
        public string NoHp { get; set; }
        public string Pekerjaan { get; set; }
        public DateTime? TanggalCore { get; set; }
        public string Segment { get; set; }
        public string Product { get; set; }
        public double? JumlahAngsuran { get; set; }
        public DateTime? TanggalMulai { get; set; }
        public DateTime? TanggalJatuhTempo { get; set; }
        public int? Tenor { get; set; }
        public double? Plafond { get; set; }
        public double? OutStanding { get; set; }
        public double? OutStandingActual { get; set; }
        public int? Kolektabilitas { get; set; }
        public int? DPD { get; set; }
        public DateTime? TanggalBayarTerakhir { get; set; }
        public double? TunggakanPokok { get; set; }
        public double? TunggakanBunga { get; set; }
        public double? TunggakanDenda { get; set; }
        public double? TotalTunggakan { get; set; }
        public double? TotalKewajiban { get; set; }

    }

    public class SegmentNasabahDTO
    {
        public string Segment { get; set; }
        public string Product { get; set; }
        public string JumlahAngsuran { get; set; }
        public string TanggalMulai { get; set; }
        public string TanggalJatuhTempo { get; set; }
        public string Tenor { get; set; }
        public string OutStanding { get; set; }
        public string TotalKewajiban { get; set; }

    }
    
    public class DetailNasabahDTO
    {
        public string NoKTP { get; set; }
        public string LoanNumber { get; set; }
        public string Nasabah { get; set; }
        public string accno { get; set; }
        public string TanggalLahir { get; set; }
        public string Alamat { get; set; }
        public string noTelp { get; set; }
        public string NoHp { get; set; }
        public string Pekerjaan { get; set; }
        public string TanggalCore { get; set; }
        public string Segment { get; set; }
        public string Product { get; set; }
        public string JumlahAngsuran { get; set; }
        public string TanggalMulai { get; set; }
        public string TanggalJatuhTempo { get; set; }
        public string Tenor { get; set; }
        public string Plafond { get; set; }
        public string OutStanding { get; set; }
        public string OutStandingActual { get; set; }
        public string Kolektabilitas { get; set; }
        public string DPD { get; set; }
        public string TanggalBayarTerakhir { get; set; }
        public string TunggakanPokok { get; set; }
        public string TunggakanBunga { get; set; }
        public string TunggakanDenda { get; set; }
        public string TotalTunggakan { get; set; }
        public string TotalKewajiban { get; set; }

    }
}
