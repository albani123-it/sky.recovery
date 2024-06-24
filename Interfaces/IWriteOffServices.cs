using sky.recovery.DTOs.RequestDTO.WO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sky.recovery.Interfaces
{
    public interface IWriteOffServices
    {
        public Task<(bool status, string message, List<dynamic> Data)> GetTaskList(string userid);
        public Task<(bool status, string message, List<dynamic> Data)> GetMonitorList(string userid);
        public Task<(bool? Status, string message, Dictionary<string, List<dynamic>> DataNasabah)> GetDetailWO(GetDetailWO Entity);

    }
}
