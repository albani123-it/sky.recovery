using sky.recovery.DTOs.ResponsesDTO;
using sky.recovery.Responses;
using System.Threading.Tasks;

namespace sky.recovery.Interfaces
{
    public interface IAydaServices
    {
        public Task<(bool? Error, GenericResponses<aydaDTO> Returns)> ListMonitoringAyda(int userlevel);
        public Task<(bool? Error, GenericResponses<aydaDTO> Returns)> ListTaskListAyda(string userid);

    }
}
