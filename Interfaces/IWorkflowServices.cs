using sky.recovery.DTOs.RequestDTO.Workflow;
using sky.recovery.DTOs.WorkflowDTO;
using sky.recovery.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sky.recovery.Interfaces
{
    public interface IWorkflowServices
    {
        public Task<(bool status, string message, List<dynamic> Data)> CreateNodesEngine(long flowid, int FiturId);

        public Task<(bool status, string message, List<dynamic> Data)> GetNodesWorkflowEngine(long flowid);

        public Task<(bool Status, string message)> CreateWorkflowEngine(AddWorkflowEngineDTO Entity);

        public Task<(bool Status, string message, List<dynamic?> Data)> GetListWorkflow();

        public Task<(bool? Status, GeneralResponsesV2 Returns)> CallbackApproval_Dummy_Engine(string userid, CallbackApprovalDTO Entity);

        public Task<(bool? Status, GeneralResponsesV2 Returns)> SubmitWorkflowStep(SubmitWorkflowDTO Entity);
        public Task<(bool? Status, GeneralResponsesV2 Returns)> CallbackApproval(CallbackApprovalDTO Entity);
        public Task<(bool? Status, GeneralResponsesV2 Returns)> CallbackApproval_Dummy(string userid,CallbackApprovalDTO Entity);

        public  Task<(bool Status, string message,
             List<dynamic> WorkflowDetail,
             List<dynamic> WorkflowHistory)>
             GetDetailWorkflow(GetDetailWFDTO Entity);
    }
}
