using sky.recovery.DTOs.RequestDTO.Insurance;
using sky.recovery.DTOs.ResponsesDTO.Asuransi;
using sky.recovery.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sky.recovery.Interfaces
{
    public interface IAsuransiServices
    {
        public Task<(bool status, string message, List<dynamic?> Data)> GetMonitoring(string userid, string services);

        public Task<(bool Status, string Message, Dictionary<string, List<dynamic>> DataNasabah)> GetDetailAsuransiForApproval(int AsuransiId, int loanid, int CustomerId);

        public Task<(bool? Status, GeneralResponsesV2 Returns)> SetIsActive(int Id, int status);
        public Task<(bool? Status, GeneralResponsesV2 Returns)> InsuranceMonitoring(string userid);
        public Task<(bool? Status, GeneralResponsesV2 Returns)> InsuranceTaskList(string userid);
        public Task<(bool? Status, string message, Dictionary<string, List<dynamic>> DataNasabah)> GetDetailAsuransi(GetDetailAsuransiDTO Entity);
        public Task<(bool? Status, GeneralResponsesV2 Returns)> AsuransiSubmit(string userid, CreateAsuransiDTO Entity);

        public Task<(bool? Status, GeneralResponsesV2 Returns)> AsuransiDraft(string userid, CreateAsuransiDTO Entity);



    }
}
