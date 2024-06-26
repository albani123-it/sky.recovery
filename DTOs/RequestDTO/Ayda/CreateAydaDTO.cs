using sky.recovery.DTOs.RequestDTO.CommonDTO;
using System;

namespace sky.recovery.DTOs.RequestDTO.Ayda
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
    public class AydaData
    {
        public int? aydaid { get; set; }
        public int? bankid { get; set; }
        public DateTime? tglambilalih { get; set; }
        public string kualitas { get; set; }
        public decimal? nilaipembiayaanpokok { get; set; }
        public decimal? nilaimargin { get; set; }
        public decimal? nilaiperolehanagunan { get; set; }
        public decimal? perkiraanbiayajual { get; set; }

        public decimal? ppa { get; set; }
        public decimal? jumlahayda { get; set; }
    }

    public class CreateAydaDTO
    {
        public DetailUser User { get; set; }
        public AydaData Data { get; set; }
        public DataLoan DataNasabahLoan { get; set; }
    }
}
