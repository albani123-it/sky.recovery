using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sky.recovery.Responses;
using System.Threading.Tasks;
using System;
using sky.recovery.Interfaces;
using sky.recovery.DTOs.RequestDTO;

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


        [HttpGet("restruktur/monitoring/list")]
        public async Task<ActionResult<GeneralResponses>> RestructureListMonitoring()
        {
            try
            {
                var GetData = await _recoveryService.ListRestructure();
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

        [HttpPost("restruktur/NasabahDetail")]
        public async Task<ActionResult<GeneralResponses>> RestrukturNasabahDetail([FromBody] RequestRestrukturDetail Entity)
        {
            try
            {
                var GetData = await _recoveryService.GetRestrukturDetail(Entity);
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

        [HttpPost("restruktur/UpdatePengajuan")]
        public async Task<ActionResult<GeneralResponses>> UpdatePengajuan([FromBody] UpdateRestrukturisasi Entity)
        {
            try
            {
                var GetData = await _recoveryService.UpdatePengajuanRestrukturisasi(Entity);
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

        [HttpGet("restruktur/MonitoringResktrukturDetail/{accno}")]
        public async Task<ActionResult<GeneralResponses>> MonitoringResktrukturDetail(string accno)
        {
            try
            {
                var GetData = await _recoveryService.GetRestrukturDetailByAccno(accno);
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
        [HttpGet("restruktur/ApprovalList/Monitoring/{userid}")]
        public async Task<ActionResult<GeneralResponses>> ApprovalListMonitoring()
        {
            try
            {
                var GetData = await _recoveryService.ListRestructure();
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

        [HttpGet("Restruktur/monitoring/list/detail/{userid}")]
        public async Task<ActionResult<GeneralResponses>> MonitoringListDetail(string userid)
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

        [HttpPost("Restruktur/monitoring/list/Search")]
        public async Task<ActionResult<GeneralResponses>> ListSearchRestructure([FromBody] SearchListRestrucutre Entity)
        {
            try
            {
                var GetData = await _recoveryService.ListSearchRestructure(Entity);
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
