using sky.recovery.Responses;
using System.Threading.Tasks;

namespace sky.recovery.Interfaces
{
    public interface IAuctionService
    {

        public Task<(bool? Status, GeneralResponsesV2 Returns)> SetIsActive(int Id, int status);
        public Task<(bool? Status, GeneralResponsesV2 Returns)> AuctionMonitoring(string UserId);


    }
}
