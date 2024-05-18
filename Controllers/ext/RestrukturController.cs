using Microsoft.AspNetCore.Mvc;
using sky.recovery.DTOs.RequestDTO;
using sky.recovery.DTOs.ResponsesDTO;
using sky.recovery.Interfaces;
using sky.recovery.Responses;
using sky.recovery.Services;
using System.Threading.Tasks;
using System;

namespace sky.recovery.Controllers.ext
{
    [Route("api/skyrecovery/recovery/[controller]")]
    public class RestrukturController : RecoveryController
    {
        private IRestrukturServices _recoveryService { get; set; }
        private IWorkflowServices _workflowService { get; set; }
        ModellingGeneralResponsesV2 _DataResponses = new ModellingGeneralResponsesV2();

        public RestrukturController(IRestrukturServices recoveryService, IWorkflowServices workflowService) : base(recoveryService)
        {
            _recoveryService = recoveryService;
            _workflowService = workflowService;
        }

        //V2
        [HttpPost("V2/CallbackApproval")]
        public async Task<ActionResult<GeneralResponses>> CallbackApproval([FromBody] CallbackApprovalDTO Entity)

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _workflowService.CallbackApproval(Entity);
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
        [HttpPost("V2/WorkflowSubmit")]
        public async Task<ActionResult<GeneralResponses>> WorkflowSubmit([FromBody] SubmitWorkflowDTO Entity )

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
        [HttpGet("V2/Monitoring/list/{UserId}")]
        public async Task<ActionResult<GeneralResponses>> MonitoringRestrukturV2(string UserId)

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _recoveryService.MonitoringRestrukturV2(UserId);
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
                 wrap.Status  = false;
                return BadRequest(wrap);
            }
        }

        //V2
        [HttpGet("V2/GetDetailRestruktur/{loanid}")]
        public async Task<ActionResult<GeneralResponses>> GetDetailRestruktur(int loanid)

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _recoveryService.GetDetailDraftingRestruktur(loanid);
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
                 wrap.Status  = false;
                return BadRequest(wrap);
            }
        }

        //V2
        [HttpPost("V2/SearchingMonitoringRestruktur")]
        public async Task<ActionResult<GeneralResponses>> SearchingMonitoringRestruktur([FromBody] SearchingRestrukturDTO Entity)

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _recoveryService.SearchingMonitoringRestruktur(Entity);
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
        [HttpGet("V2/GetMasterLoan/list")]
        public async Task<ActionResult<GeneralResponses>> GetMasterLoan()

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _recoveryService.GetMasterLoanV2();
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
                 wrap.Status  = false;
                return BadRequest(wrap);
            }
        }


        //V2
        [HttpPost("V2/CreateDraftRestrukture")]
        public async Task<ActionResult<GeneralResponses>> CreateDraftRestrukture([FromBody] AddRestructureDTO Entity)

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _recoveryService.CreateDraftRestrukture(Entity);
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
        [HttpGet("V2/TaskList/list/{UserId}")]
        public async Task<ActionResult<GeneralResponses>> TaskListRestrukturV2(string UserId)

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _recoveryService.TaskListRestrukturV2(UserId);
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
                 wrap.Status  = false;
                return BadRequest(wrap);
            }
        }


        //V2
        [HttpPost("V2/ActionApproval")]
        public async Task<ActionResult<GeneralResponses>> ActionApproval([FromBody] ApprovalActionDTO Entity)

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _recoveryService.ActionApprovalRestrukture(Entity);
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
        [HttpGet("V2/GetWorkflowHistory/{idrequest:int}")]
        public async Task<ActionResult<GeneralResponses>> GetWorkflowHistory(int idrequest)

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _recoveryService.GetWorkflowHistory(idrequest);
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
        [HttpPost("V2/ConfigPolaRestrukture")]
        public async Task<ActionResult<GeneralResponses>> ConfigPolaRestrukture([FromBody] AddPolaDTO Entity)

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _recoveryService.ConfigPolaRestrukture(Entity);
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
        [HttpPost("V2/SubmitRestrukture")]
        public async Task<ActionResult<GeneralResponses>> SubmitRestrukture([FromBody] SubmitRestruktureDTO Entity)

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _recoveryService.SubmitRestrukture(Entity);
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
        //tambahkan pengecekan iscalculated
        [HttpPost("V2/Analisa")]
        public async Task<ActionResult<GeneralResponses>> AnalisaRestrukture([FromBody]ConfigAnalisaRestruktureDTO  Entity)

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _recoveryService.ConfigAnalisaRestrukture(Entity);
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
        //GetMetodeRestrukture
        [HttpPost("V2/PolaMetode")]
        public async Task<ActionResult<GeneralResponses>> PolaMetodeRestrukture([FromBody] GetPolaDTO Entity)

        {
            var wrap = _DataResponses.Return();

            try
            {
                if(Entity==null)
                {
                    wrap.Message = "Request Not Valid";
                    wrap.Status = false;
                    return BadRequest(wrap);
                }
                if (Entity.idloan == null)
                {
                    wrap.Message = "Loan Harus Dipilih";
                    wrap.Status = false;
                    return BadRequest(wrap);
                }
                if (Entity.idrestrukture == null)
                {
                    wrap.Message = "Restrukture Harus Dipilih";
                    wrap.Status = false;
                    return BadRequest(wrap);
                }

                var GetData = await _recoveryService.GetPolaMetodeRestrukture(Entity.idrestrukture,Entity.idloan);
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
        [HttpPost("V2/Remove/Permasalahan")]
        public async Task<ActionResult<GeneralResponses>> RemovePermasalahanRestrukture([FromBody] RemovePermasalahanDTO Entity)

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _recoveryService.RemovePermasalahanRestrukture(Entity);
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
        [HttpPost("V2/Create/Permasalahan")]
        public async Task<ActionResult<GeneralResponses>> CreatePermasalahan([FromBody] CreatePermasalahanDTO Entity)

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _recoveryService.CreatePermasalahan(Entity);
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
        [HttpPost("V2/Update/Permasalahan")]
        public async Task<ActionResult<GeneralResponses>> UpdatePermasalahan([FromBody] UpdatePermasalahanDTO Entity)

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _recoveryService.UpdatePermasalahan(Entity);
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
        [HttpPost("V2/Detail/Documents")]
        public async Task<ActionResult<GeneralResponses>> DetailDocuments([FromBody] GetDocumentRestruktureDTO Entity)

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _recoveryService.GetMasterDocRule(Entity);
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
        [HttpPost("V2/Documents/Upload")]
        public async Task<ActionResult<GeneralResponses>> UploadDocRestruktur([FromForm] UploadDocRestrukturDTO Entity)

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _recoveryService.UploadDocRestrukture(Entity);
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
        [HttpPost("V2/RemoveDraft")]
        public async Task<ActionResult<GeneralResponses>> RemoveDraftRestrukture([FromBody] RemoveDraftRestruktureDTO Entity)

        {
            var wrap = _DataResponses.Return();

            try
            {
                if(Entity==null)
                {
                    wrap.Message = "Request Not Valid";
                    wrap.Status = false;
                    return BadRequest(wrap);
                }
                var GetData = await _recoveryService.RemoveDraftRestukture(Entity.userid,Entity.loanid,Entity.restruktureid);
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

        public IActionResult Index()
        {
            return View();
        }
    }
}
