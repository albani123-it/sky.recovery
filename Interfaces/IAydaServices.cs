using sky.recovery.DTOs.RequestDTO.Ayda;
using sky.recovery.Responses;
using System.Threading.Tasks;

namespace sky.recovery.Interfaces
{
    public interface IAydaServices
    {
        public Task<(bool? Status, GeneralResponsesV2 Returns)> AydaMonitoring(string UserId);
        public Task<(bool? Status, GeneralResponsesV2 Returns)> AydaDraft(CreateAydaDTO Entity);
        public  Task<(bool? Status, GeneralResponsesV2 Returns)> AydaSubmit(CreateAydaDTO Entity);


    }
}
