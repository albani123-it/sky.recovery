using System;

namespace sky.recovery.DTOs.ResponsesDTO.Asuransi
{
   
   

    public class DataLoan
    {
        public int? loanid { get; set; }
        public int? BranchId { get; set; }

    }
    public class AsuransiData
    {
        public int? AsuransiId { get; set; }
        public int? loanid { get; set; }
        public string namapejabat { get; set; }
        public string pejabat { get; set; }
        public string nosertifikat { get; set; }
        public string tglsertifikat { get; set; }
        public string nopolis { get; set; }
        public string tglpolis { get; set; }
        public string nopk { get; set; }
        public decimal? nilaitunggakanpokok { get; set; }
        public decimal? nilaitunggakanbunga { get; set; }
        public string catatanpolis { get; set; }
        public string keterangan { get; set; }
        public decimal? nilaiklaim { get; set; }
        public decimal? nilaiklaimdibayar { get; set; }
        public decimal? bakidebitklaim { get; set; }
        public string catatanklaim { get; set; }
        public string permasalahan { get; set; }


    }

    public class CreateAsuransiDTO
    {
        public AsuransiData Data { get; set; }
        public DataLoan DataNasabahLoan { get; set; }
    }
}
