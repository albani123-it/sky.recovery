using sky.recovery.DTOs.ResponsesDTO;
using sky.recovery.Responses;
using System.Threading.Tasks;

namespace sky.recovery.Interfaces
{
    public interface IAydaServices
    {
        public Task<(bool? Error, GenericResponses<aydaDTO> Returns)> ListMonitoringAyda();
        public Task<(bool? Error, GenericResponses<aydaDTO> Returns)> ListTaskListAyda(string userid);
        public Task<(bool? Error, GeneralResponsesV2 Returns)> MonitoringAYDAV2();
        public Task<(bool? Error, GeneralResponsesV2 Returns)> TaskListAYDAV2(string UserId);

    }
}
