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
        ModellingGeneralResponsesV2 _DataResponses = new ModellingGeneralResponsesV2();

        public RestrukturController(IRestrukturServices recoveryService) : base(recoveryService)
        {
            _recoveryService = recoveryService;
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

        public IActionResult Index()
        {
            return View();
        }
    }
}
