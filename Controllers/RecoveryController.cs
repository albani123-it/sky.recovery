using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sky.recovery.Responses;
using System.Threading.Tasks;
using System;
using sky.recovery.Interfaces;
using sky.recovery.DTOs.RequestDTO;
using sky.recovery.Model.Entity;
using sky.recovery.Model;
using sky.recovery.DTOs.ResponsesDTO;

namespace sky.recovery.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("skyrecovery/[controller]")]
    public class RecoveryController : BaseController
    {
        private IRestrukturServices _recoveryService { get; set; }
        public RecoveryController(IRestrukturServices recoveryService)
        {
            _recoveryService = recoveryService;
        }
        public IActionResult Index()
        {
            return View();
        }

        //API YANG DIPAKAI
        //monitroing restruktur - list add restruktur
        [HttpGet("nasabah/list/{userid}")]
        public async Task<ActionResult<GenericResponses<MonitoringDetailRestructureDTO>>> ListDeskCall(string userid)
        {
            try
            {
                var GetData = await _recoveryService.MonitoringListDetail(userid);
                if (GetData.Error == true)
                {
                    return BadRequest(GetData.Returns);
                }
                else
                {
                    return Ok(GetData.Returns);
                }

            }
            catch (Exception ex)
            {
                var Return = new GeneralResponses()
                {
                    Message = ex.Message,
                    Error = true
                };
                return BadRequest(Return);
            }
        }

        //API YANG DIPAKAI
        //monitor restruktur search list
        [HttpPost("nasabah/listSearch")]
        public async Task<ActionResult<GenericResponses<MonitoringDetailRestructureDTO>>> ListSearchDeskCall([FromBody] SearchListRestrucutre Entity)
        {
            try
            {
                var GetData = await _recoveryService.ListSearchMonitoringListDetail(Entity);
                if (GetData.Error == true)
                {
                    return BadRequest(GetData.Returns);
                }
                else
                {
                    return Ok(GetData.Returns);
                }

            }
            catch (Exception ex)
            {
                var Return = new GeneralResponses()
                {
                    Message = ex.Message,
                    Error = true
                };
                return BadRequest(Return);
            }
        }


        [HttpGet("DummyTest/{Error}")]
        public async Task<ActionResult<GeneralResponses>> DummyTest(string Error)
        {
            try
            {
                var GetData = Error;
                if (GetData == "Error")
                {
                    return BadRequest(GetData);
                }
                else
                {
                    return Ok(GetData);
                }

            }
            catch (Exception ex)
            {
                var Return = new GeneralResponses()
                {
                    Message = ex.Message,
                    Error = true
                };
                return BadRequest(Return);
            }
        }


    }
}
