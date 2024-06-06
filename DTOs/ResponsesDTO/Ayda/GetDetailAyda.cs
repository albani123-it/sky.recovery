using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;

namespace sky.recovery.DTOs.ResponsesDTO.Ayda
{
    public class GetDetailAyda
    {

    }
    public class NasabahAydaDTO
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

    public class RepoAydaDTO
    {
        public long Id { get; set; }
        public string url { get; set; }
        public string urlname { get; set; }
        public string uploaddated { get; set; }
        public int? doctype { get; set; }
    }
    public class LoanAydaDTO
    {
        public int loanid { get; set; }
        public string Fasilitas { get; set; }
        public string Plafond { get; set; }
        public string LoanType { get; set; }
        public string Tenor { get; set; }
    }

    public class JaminanAyda_1
    {
        public int JaminanId { get; set; }
        public string Type { get; set; }
        public string Object { get; set; }
        public string Address { get; set; }
        public string DocumentType { get; set; }
  
    }
    public class JaminanAyda_2
    {
        public int? bankid { get; set; }
        public DateTime? tglambilalih { get; set; }
        public string kualitas { get; set; }
        public string nilaipembiayaanpokok { get; set; }
        public string nilaimargin { get; set; }
        public string nilaiperolehanagunan { get; set; }
        public string perkiraanbiayajual { get; set; }
        public string ppa { get; set; }
        public string jumlahayda { get; set; }
        public int? statusid { get; set; }
        public string status { get; set; }
        public int? usercreated { get; set; }
        public string createdby { get; set; }
        public DateTime? createddated { get; set; }
       
    }
}
