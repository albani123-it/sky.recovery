using System;

namespace sky.recovery.DTOs.RequestDTO.WO
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
    public class WOData
    {
        public int? woid { get; set; }
       public decimal? InterestRate { get; set; }
        public decimal? ChargesRate { get; set; }
        public decimal? Principal { get; set; }
        public decimal? CurrentOverdue { get; set; }
        
    
    }

   
    public class CreateWODTO
    {
        public DetailUser User { get; set; }
        public WOData Data { get; set; }
        public DataLoan DataNasabahLoan { get; set; }
    }
}
