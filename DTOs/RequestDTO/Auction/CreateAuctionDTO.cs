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
    public int AuctionId { get; set; }
    }

    public class CreateAuctionDTO
    {
        public DetailUser User { get; set; }
        public AuctionData Data { get; set; }
        public DataLoan DataNasabahLoan { get; set; }
    }
}
