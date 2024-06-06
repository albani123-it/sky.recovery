using System;

namespace sky.recovery.DTOs.ResponsesDTO.Restrukture
{
    public class NasabahDetailDTO
    {
        public string Nama { get; set; }
        public string NoKTP { get; set; }
        public string tanggallahir { get; set; }
        public string alamat { get; set; }
        public string nohp { get; set; }
        public string pekerjaan { get; set; }
        public string TglCore { get; set; }
    }

    public class DataLoan
    {
        public int? SegmentId { get; set; }
        public int? ProductId { get; set; }
        public string Segment { get; set; }
        public string Produk { get; set; }
        public string JumlahAngsuran { get; set; }
        public string TanggalMulai { get; set; }
        public string TanggalJatuhTempo { get; set; }
        public string Tenor { get; set; }
        public string Plafond { get; set; }
        public string OutStanding { get; set; }
        public string OutStandingActual { get; set; }
        public string Kolektabilitas { get; set; }
        public string DPD { get; set; }
        public string TglBayarTerakhir { get; set; }
        public string TunggakanPokok { get; set; }
        public string TunggakanBunga { get; set; }
        public string TunggakanDenda { get; set; }
        public string TotalTunggakan { get; set; }
        public string TotalKewajiban { get; set; }

    }

    public class DataFasilitasLain
    {
        public int? productid{get;set;}
        public int? segmentid { get; set; }
        public string Segment { get; set; }
        public string Product { get; set; }
        public string JumlahAngsuran { get; set; }
        public string TanggalMulai { get; set; }
        public string TanggalJatuhTempo { get; set; }
        public string Tenor { get; set; }
        public string JumlahPinjaman { get; set; }
        public string Outstanding { get; set; }
    }
    public class DataPermasalahan
    {
        public string Deskripsi { get; set; }
    }

    public class InformationRequest
    {
        public int? CreatedById { get; set; }
        public string createdby { get; set; }
        public DateTime? createddated { get; set; }
    }
    public class DetailPolaRestruk
    {
        public string keterangan { get; set; }
        public int? pengurangannilaimargin { get; set; }
        public int? jenispengurangan { get; set; }
        public int? graceperiod { get; set; }
        public int? polaid { get; set; }
        public string pola_desc { get; set; }
        public string jenis_desc { get; set; }
     
    }

}
