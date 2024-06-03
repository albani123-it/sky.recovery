using sky.recovery.DTOs.RequestDTO.Insurance;
using sky.recovery.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sky.recovery.Interfaces
{
    public interface IAsuransiServices
    {
        public Task<(bool? Status, GeneralResponsesV2 Returns)> SetIsActive(int Id, int status);
        public Task<(bool? Status, GeneralResponsesV2 Returns)> InsuranceMonitoring(string userid);
        public Task<(bool? Status, GeneralResponsesV2 Returns)> InsuranceTaskList(string userid);
        public Task<(bool? Status, string message, Dictionary<string, List<dynamic>> DataNasabah)> GetDetailAsuransi(GetDetailAsuransiDTO Entity);




    }
}
