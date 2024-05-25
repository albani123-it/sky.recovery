namespace sky.recovery.DTOs.RequestDTO.Insurance
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
    public class InsuranceData
    {

    }

    public class CreateInsuranceDTO
    {
        public DetailUser User { get; set; }
        public InsuranceData Data { get; set; }
        public DataLoan DataNasabahLoan { get; set; }
    }
  
}
