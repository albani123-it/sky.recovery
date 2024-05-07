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


    

     
        public IActionResult Index()
        {
            return View();
        }
    }
}
