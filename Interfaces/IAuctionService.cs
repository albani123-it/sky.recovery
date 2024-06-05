using sky.recovery.DTOs.RequestDTO.Auction;
using sky.recovery.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sky.recovery.Interfaces
{
    public interface IAuctionService
    {

        public Task<(bool? Status, GeneralResponsesV2 Returns)> SetIsActive(int Id, int status);
        public Task<(bool? Status, GeneralResponsesV2 Returns)> AuctionMonitoring(string UserId);
        public Task<(bool? Status, GeneralResponsesV2 Returns)> AuctionTaskList(string UserId);

        public Task<(bool? Status, string message, Dictionary<string, List<dynamic>> DataNasabah)> GetDetailAuction(GetDetailAuctionDTO Entity);

    }
}
