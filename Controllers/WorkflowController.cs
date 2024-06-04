using Microsoft.AspNetCore.Mvc;
using sky.recovery.Interfaces;
using sky.recovery.Responses;
using sky.recovery.Services;
using System.Threading.Tasks;
using System;
using sky.recovery.DTOs.WorkflowDTO;

namespace sky.recovery.Controllers
{
    [Route("skyrecovery/[controller]")]

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
        [HttpPost("V2/GetDetailWorkflow")]
        public async Task<ActionResult<GeneralResponsesDictionaryV2>> GetDetailWorkflow([FromBody] GetDetailWFDTO Entity)

        {
            var wrap = _DataResponses.ReturnDictionary();

            try
            {
                var GetData = await _workflowService.GetDetailWorkflow(Entity);
                wrap.Data = GetData.DataWorkflow;
                wrap.Status = GetData.Status;
                wrap.Message = GetData.message;
                if (GetData.Status == true)
                {
                    return Ok(wrap);
                }
                else
                {
                    return BadRequest(wrap);
                }

            }

            catch (Exception ex)
            {
                wrap.Message = ex.Message;
                wrap.Status = false;
                return StatusCode(500,wrap);
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
