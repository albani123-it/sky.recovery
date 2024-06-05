using System;

namespace sky.recovery.DTOs.RequestDTO.Auction
{
    public class GetDetailAuction
    {
    }

    public class NasabahAuctionDTO
    {
        public int customerid { get; set; }
        public string Nama { get; set; }
        public string KTP { get; set; }
        public string AccNo { get; set; }
        public string CuCif { get; set; }
        public string Branch { get; set; }
        public int? BranchId { get; set; }
        public string Address { get; set; }
    }

    public class RepoAuctionDTO
    {
        public long Id { get; set; }
        public string url { get; set; }
        public string urlname { get; set; }
        public string uploaddated { get; set; }
        public int? doctype { get; set; }
    }
    public class LoanAuctionDTO
    {
        public int loanid { get; set; }
        public string Fasilitas { get; set; }
        public string Plafond { get; set; }
        public string LoanType { get; set; }
        public string Tenor { get; set; }
    }

    public class DataAuction
    {
        
       public int? alasanlelangid { get; set; }
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
        public int? statusid { get; set; }
        public string status { get; set; }
        public string createdbywho { get; set; }
        public int? createdby { get; set; }
        public DateTime? createddated { get; set; }
    }
  

}
