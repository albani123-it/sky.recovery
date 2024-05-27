using Microsoft.AspNetCore.Mvc;
using sky.recovery.Interfaces;
using sky.recovery.Responses;
using sky.recovery.Services;
using System.Threading.Tasks;
using System;
using sky.recovery.DTOs.WorkflowDTO;

namespace sky.recovery.Controllers
{
    [Route("api/skyrecovery/[controller]")]

    public class WorkflowController : ControllerBase
    {
        private IWorkflowServices _workflowService { get; set; }
        ModellingGeneralResponsesV2 _DataResponses = new ModellingGeneralResponsesV2();

        public WorkflowController(IWorkflowServices workflowservice)
        {
            _workflowService = workflowservice;
        }


        //V2
        [HttpPost("V2/WorkflowSubmit")]
        public async Task<ActionResult<GeneralResponses>> WorkflowSubmit([FromBody] SubmitWorkflowDTO Entity)

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _workflowService.SubmitWorkflowStep(Entity);
                if (GetData.Status == true)
                {
                    return Ok(GetData.Returns);
                }
                else
                {
                    return BadRequest(GetData.Returns);
                }

            }

            catch (Exception ex)
            {
                wrap.Message = ex.Message;
                wrap.Status = false;
                return BadRequest(wrap);
            }
        }

        //V2
        [HttpPost("V2/CallbackApproval")]
        public async Task<ActionResult<GeneralResponses>> CallbackApproval([FromBody] CallbackApprovalDTO Entity)

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _workflowService.CallbackApproval_Dummy(Entity);
                if (GetData.Status == true)
                {
                    return Ok(GetData.Returns);
                }
                else
                {
                    return BadRequest(GetData.Returns);
                }

            }

            catch (Exception ex)
            {
                wrap.Message = ex.Message;
                wrap.Status = false;
                return BadRequest(wrap);
            }
        }


     
    }
}
