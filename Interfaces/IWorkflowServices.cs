﻿using sky.recovery.DTOs.WorkflowDTO;
using sky.recovery.Responses;
using System.Threading.Tasks;

namespace sky.recovery.Interfaces
{
    public interface IWorkflowServices
    {
        public Task<(bool? Status, GeneralResponsesV2 Returns)> SubmitWorkflowStep(SubmitWorkflowDTO Entity);
        public Task<(bool? Status, GeneralResponsesV2 Returns)> CallbackApproval(CallbackApprovalDTO Entity);
        public Task<(bool? Status, GeneralResponsesV2 Returns)> CallbackApproval_Dummy(CallbackApprovalDTO Entity);

    }
}