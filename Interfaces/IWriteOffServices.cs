using sky.recovery.DTOs.RequestDTO.WO;
using sky.recovery.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sky.recovery.Interfaces
{
    public interface IWriteOffServices
    {
        public Task<(bool? Status, GeneralResponsesV2 Returns)> WOSubmit(string userid, CreateWODTO Entity);

        public Task<(bool? Status, GeneralResponsesV2 Returns)> WODraft(string userid, CreateWODTO Entity);

        public Task<(bool status, string message, List<dynamic> Data)> GetTaskList(string userid);
        public Task<(bool status, string message, List<dynamic> Data)> GetMonitorList(string userid);
        public Task<(bool? Status, string message, Dictionary<string, List<dynamic>> DataNasabah)> GetDetailWO(GetDetailWO Entity);

    }
}
