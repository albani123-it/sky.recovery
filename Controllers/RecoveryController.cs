using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sky.recovery.Responses;
using System.Threading.Tasks;
using System;
using sky.recovery.Interfaces;

namespace sky.recovery.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("skyrecovery/[controller]")]
    public class RecoveryController : Controller
    {
        private IRecoveryServices _recoveryService { get; set; }
        public RecoveryController(IRecoveryServices recoveryService)
        {
            _recoveryService = recoveryService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("nasabah/list/{userid}")]
        public async Task<ActionResult<GeneralResponses>> ListDeskCall(string userid)
        {
            try
            {
                var GetData = await _recoveryService.ListCollection(userid);
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


        [HttpGet("restruktur/monitoring/list/{userid}")]
        public async Task<ActionResult<GeneralResponses>> RestructureListMonitoring(string userid)
        {
            try
            {
                var GetData = await _recoveryService.ListRestructure(userid);
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
