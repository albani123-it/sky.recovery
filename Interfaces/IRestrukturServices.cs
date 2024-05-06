using sky.recovery.DTOs.RequestDTO;
using sky.recovery.DTOs.ResponsesDTO;
using sky.recovery.Responses;
using System.Threading.Tasks;

namespace sky.recovery.Interfaces
{
    public interface IRestrukturServices
    {
        public  Task<(bool? Error, GeneralResponsesV2 Returns)> MonitoringRestrukturV2(string UserId);
        public Task<(bool? Error, GeneralResponsesV2 Returns)> TaskListRestrukturV2(string UserId);
        public Task<(bool? Error, GeneralResponsesV2 Returns)> GetMasterLoanV2();
        public Task<(bool? Error, GeneralResponsesV2 Returns)> GetDetailDraftingRestruktur(int? LoanId, string AccNo);





    }
}
