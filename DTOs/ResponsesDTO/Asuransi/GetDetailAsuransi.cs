namespace sky.recovery.DTOs.ResponsesDTO.Asuransi
{
    public class GetDetailAsuransi
    {
    }
    public class NasabahAsuransiDTO
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

    public class RepoAsuransiDTO
    {
        public long Id { get; set; }
        public string url { get; set; }
        public string urlname { get; set; }
        public string uploaddated { get; set; }
        public int? doctype { get; set; }
    }
    public class LoanAsuransiDTO
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

    public class DetailAsuransi
    {
        public string NamaPejabat { get; set; }
        public string Jabatan { get; set; }
        public string NoSertifikat { get; set; }
        public string TglSertifikat { get; set; }
        public string nopolis { get; set; }
        public string tglpolis { get; set; }
        public string nopk { get; set; }
        public decimal? nilaitunggakanpokok { get; set; }
        public decimal? nilaitunggakanbunga { get; set; }
        public string catatanpolis { get; set; }
        public string keterangan { get; set; }
        public decimal? nilaiklaim { get; set; }
        public decimal? nilaiklaimdibayar { get; set; }
        public decimal? asuransisisaklaimid { get; set; }
        public decimal? bakidebitklaim { get; set; }
        public string catatanklaim { get; set; }
        public string permasalahan { get; set; }
        public int? statusid { get; set; }
        public string createddated { get; set; }
    }
}
