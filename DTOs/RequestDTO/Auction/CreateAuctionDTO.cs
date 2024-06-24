using System;

namespace sky.recovery.DTOs.RequestDTO.Auction
{
    
    public class DetailUser
    {
        public string UserId { get; set; }

    }

    public class DataLoan
    {
        public int? loanid { get; set; }
        public int? BranchId { get; set; }

    }
    public class AuctionData
    {
    public int? AuctionId { get; set; }
        public int? AlasanLelangId { get; set; }
        public string nopk { get; set; }
        public decimal? nilailimitlelang { get; set; }
        public decimal? uangjaminan { get; set; }
        public string objeklelang { get; set; }
        public string keterangan { get; set; }
        public int? balailelangid { get; set; }
        public int? jenislelangid { get; set; }
        public string tatacaralelang { get; set; }
        public decimal? biayalelang { get; set; }
        public string catatanlelang { get; set; }
        public DateTime? tglpenetapanlelang { get; set; }
        public string norekening { get; set; }
        public string namarekening { get; set; }
       
    }

    public class CreateAuctionDTO
    {
        public DetailUser User { get; set; }
        public AuctionData Data { get; set; }
        public DataLoan DataNasabahLoan { get; set; }
    }
}
