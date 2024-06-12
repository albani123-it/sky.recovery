using sky.recovery.DTOs.RequestDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sky.recovery.Insfrastructures
{
    public interface IRestruktureRepository
    {
        public Task<List<dynamic>> CheckingRestruktureExisting(string spname, int? idrestrukture, int? idloan);
        public Task<List<dynamic>> UpdateConfigPolaRestrukture(string spname, int? userid, AddPolaDTO Entity);
        public Task<List<dynamic>> CheckingForSubmitRestrukture(string spname, int? userid, int? idrestrukture, int? idloan);
        public Task<List<dynamic>> SubmitRestrukturApproval(string spname, int? userid, int? approverid, int? idrestrukture);


        public Task<List<dynamic>> GetBranchList(string spname);
        public  Task<List<dynamic>> GetDetailPolaRestruktur(string spname, int? idrestrukture, int? idloan, string accno);
        public Task<List<dynamic>> ActionApproval(string spname,int? userid, ApprovalActionDTO Entity);
        public Task<List<dynamic>> GetWorkflowHistory(string spname, int? idrequest);

        public Task<List<dynamic>> GetRestukture(int Type, string SPName, int?roleid,int?user);
        public Task<List<dynamic>> GetTaskList(string consstring, string spname, int? roleid, int? user);
        public Task<List<dynamic>> GetMonitoring(string consstring, string spname,int? user);
        public  Task<List<dynamic>> GetDetailDrafting(string consstring, string spname, int? LoanId, int idrestrukture);
        public Task<List<dynamic>> GetListFasilitas(string consstring, string spname, int? LoanId);
        public Task<List<dynamic>> GetMasterColateral(string spname, int? loanid);

        public Task<List<dynamic>> SearchingMonitoringRestrukture(string spname, int? Userid,SearchingRestrukturDTO Entity);
        public Task<List<dynamic>> CreateDraftRestrukture(string spname, int? Userid, int RoleId, AddRestructureDTO Entity);
        public Task<List<dynamic>> GetPermasalahanRestrukture(string spname, int? loanid, int idrestrukture);

        public Task<List<dynamic>> RemovePermasalahan(string spname, RemovePermasalahanDTO Entity);

        public Task<List<dynamic>> GetMasterLoan(string spname);
        public  Task<List<dynamic>> CreatePermasalahan(string spname,int iduser, CreatePermasalahanDTO Entity);
        public Task<List<dynamic>> UpdateAnalisaRestrukture(string spname, int? userid, ConfigAnalisaRestruktureDTO Entity);
        public Task<List<dynamic>> CheckingAnalisaRestruktureExisting(string spname, int? userid,int? analisaid, int? idrestrukture, int? idloan);

        public Task<List<dynamic>> UpdatePermasalahan(string spname,int iduser, UpdatePermasalahanDTO Entity);
        public Task<List<dynamic>> RemoveDraftRestrukture(string spname, int? userid, int? idloan, int? idrestrukture);
        public Task<List<dynamic>> CreateAnalisaRestrukture(string spname, int? userid, ConfigAnalisaRestruktureDTO Entity);

        public Task<List<dynamic>> GetMasterDocRule(string spname, string param);
        public  Task<List<dynamic>> GetDocRestrukture(string spname, GetDocumentRestruktureDTO Entity);
        public Task<List<dynamic>> CheckingDocRestrukture(string spname, int? LoanId, int? RestruktureId, int? DocTypeId);
        public  Task<List<dynamic>> InsertDocRestrukture(string spname,
           int? LoanId,
           int? RestruktureId,
           int? DocTypeId,
           string jenisdocdesc,
           string urlpath,
           string urlname,
           int? userid
           );

        public Task<List<dynamic>> UpdateDocRestruktur(string spname,
            int? LoanId,
            int? RestruktureId,
            int? DocTypeId,
            string jenisdocdesc,
            string urlpath,
            string urlname,
            int? userid
            );
    }
}
