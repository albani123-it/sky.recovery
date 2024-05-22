using sky.recovery.Responses;
using System.Threading.Tasks;

namespace sky.recovery.Interfaces
{
    public interface IAsuransiServices
    {
        public Task<(bool? Status, GeneralResponsesV2 Returns)> SetIsActive(int Id, int status);
        public Task<(bool? Status, GeneralResponsesV2 Returns)> InsuranceMonitoring(string UserId);
        public Task<(bool? Status, GeneralResponsesV2 Returns)> InsuranceTaskList(string UserId);



    }
}
