using sky.recovery.DTOs.RequestDTO;
using sky.recovery.DTOs.ResponsesDTO;
using sky.recovery.Responses;
using System.Threading.Tasks;

namespace sky.recovery.Interfaces
{
    public interface IRestrukturServices
    {
        public  Task<(bool? Status, GeneralResponsesV2 Returns)> MonitoringRestrukturV2(string UserId);
        public Task<(bool? Status, GeneralResponsesV2 Returns)> TaskListRestrukturV2(string UserId);
        public Task<(bool? Status, GeneralResponsesV2 Returns)> GetMasterLoanV2();
        public Task<(bool? Status, GeneralResponsesDetailRestrukturV2 Returns)> GetDetailDraftingRestruktur(int? loanid);
        public Task<(bool? Status, GeneralResponsesV2 Returns)> SearchingMonitoringRestruktur(SearchingRestrukturDTO Entity);

        public Task<(bool? Status, GeneralResponsesV2 Returns)> CreateDraftRestrukture(AddRestructureDTO Entity);
        public Task<(bool? Status, GeneralResponsesConfigV2 Returns)> GetPolaMetodeRestrukture(int? idrestrukture, int? idloan);
        public Task<(bool? Status, GeneralResponsesV2 Returns)> ConfigPolaRestrukture(AddPolaDTO Entity);

        public Task<(bool? Status, GeneralResponsesV2 Returns)> RemovePermasalahanRestrukture(RemovePermasalahanDTO Entity);
        public Task<(bool? Status, GeneralResponsesV2 Returns)> UpdatePermasalahan(UpdatePermasalahanDTO Entity);
        public Task<(bool? Status, GeneralResponsesV2 Returns)> CreatePermasalahan(CreatePermasalahanDTO Entity);
        public Task<(bool? Status, GeneralResponsesDocRestrukturV2 Returns)> GetMasterDocRule(GetDocumentRestruktureDTO Entity);
        public Task<(bool? Status, GeneralResponsesDocRestrukturV2 Returns)> UploadDocRestrukture(UploadDocRestrukturDTO Entity);
        public Task<(bool? Status, GeneralResponsesV2 Returns)> RemoveDraftRestukture(string userid, int? idloan, int? idrestrukture);
        public Task<(bool? Status, GeneralResponsesV2 Returns)> ConfigAnalisaRestrukture(ConfigAnalisaRestruktureDTO Entity);

        public Task<(bool? Status, GeneralResponsesConfigV2 Returns)> ConfigPola(RequestPolaDTO Entity);


    }
}
