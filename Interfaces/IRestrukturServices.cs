using sky.recovery.DTOs.RequestDTO;
using sky.recovery.DTOs.RequestDTO.Restrukture;
using sky.recovery.DTOs.ResponsesDTO;
using sky.recovery.DTOs.ResponsesDTO.Restrukture;
using sky.recovery.Insfrastructures.Scafolding.SkyColl.Recovery;
using sky.recovery.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sky.recovery.Interfaces
{
    public interface IRestrukturServices
    {
        public Task<(bool Status, string message)> RemoveDocRestrukture(int id);



        public Task<(bool Status, string Message, Dictionary<string, List<dynamic>> DataNasabah)>
            GetDetailRestruktureForApproval(int restruktureid, int loanid, int CustomerId);

        public Task<(bool Status, string Message, List<dynamic> Data)> GetAnalisaRestrukture(int RestruktureId);
        public Task<(bool Status, string Message, List<dynamic> Data)> GetPolaRestrukture(int RestruktureId);

        public Task<(bool? Status, GeneralResponsesV2 Returns)> MonitoringRestrukturV2(string UserId);
        public Task<(bool? Status, GeneralResponsesV2 Returns)> TaskListRestrukturV2(string UserId);
        public Task<(bool? Status, GeneralResponsesV2 Returns)> GetMasterLoanV2();
        public Task<(bool? Status, GeneralResponsesDetailRestrukturV2 Returns)> GetDetailDraftingRestruktur(int? loanid, int idrestrukture);
        public Task<(bool? Status, GeneralResponsesV2 Returns)> SearchingMonitoringRestruktur(SearchingRestrukturDTO Entity);
        public Task<(bool Status, string message, List<dynamic>Data)> GetMasterStatus();
        public Task<(bool Status, string Message, List<dynamic> Data)> GetFiturId();
        public Task<(bool status, string message)> ActiveRestrukture(string userid, long id, long loanid);
        public Task<(bool status, string message)> NonActiveRestrukture(string userid, long id, long loanid);
        public Task<(bool status, string message, List<dynamic> Data)> GetStatusDocument();

        public Task<(bool? Status, GeneralResponsesV2 Returns)> CreateDraftRestrukture(string userid, AddRestructureDTO Entity);
        public Task<(bool? Status, string message, Dictionary<string, List<dynamic>> DataNasabah)> GetPolaMetodeRestrukture(int? idrestrukture, int? idloan);
        public Task<(bool? Status, GeneralResponsesV2 Returns)> ConfigPolaRestrukture(string userid,AddPolaDTO Entity);
        public Task<(bool? Status, GeneralResponsesV2 Returns)> SubmitRestrukture(string userid,SubmitRestruktureDTO Entity);
        public Task<(bool? Status, GeneralResponsesV2 Returns)> ActionApprovalRestrukture(string userid,ApprovalActionDTO Entity);
        public Task<(bool? Status, GeneralResponsesV2 Returns)> GetWorkflowHistory(int? idrequest);

        public Task<(bool? Status, GeneralResponsesV2 Returns)> RemovePermasalahanRestrukture(RemovePermasalahanDTO Entity);
        public Task<(bool? Status, GeneralResponsesV2 Returns)> UpdatePermasalahan(string userid,UpdatePermasalahanDTO Entity);
        public Task<(bool? Status, GeneralResponsesV2 Returns)> CreatePermasalahan(string userid,CreatePermasalahanDTO Entity);
        public Task<(bool? Status, GeneralResponsesDocRestrukturV2 Returns)> GetMasterDocRule(GetDocumentRestruktureDTO Entity);
        public Task<(bool? Status, GeneralResponsesDocRestrukturV2 Returns)> UploadDocRestrukture(string userid,UploadDocRestrukturDTO Entity);
        public Task<(bool? Status, GeneralResponsesV2 Returns)> RemoveDraftRestukture(string user, string userid, int? idloan, int? idrestrukture);
        public Task<(bool? Status, GeneralResponsesV2 Returns)> ConfigAnalisaRestrukture(string userid,ConfigAnalisaRestruktureDTO Entity);

        public Task<(bool? Status, GeneralResponsesConfigV2 Returns)> ConfigPola(RequestPolaDTO Entity);


    }
}
