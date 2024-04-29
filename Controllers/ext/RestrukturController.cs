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
    [Route("skyrecovery/recovery/[controller]")]
    public class RestrukturController : RecoveryController
    {
        private IRestrukturServices _recoveryService { get; set; }
        private IAydaServices _aydaServices{ get; set; }

        public RestrukturController(IRestrukturServices recoveryService, IAydaServices aydaServices) : base(recoveryService,aydaServices)
        {
            _recoveryService = recoveryService;
        }



        [HttpGet("dokumenrestruktur/list")]
        public async Task<ActionResult<GeneralResponses>> ListDokumenRestruktur()

        {
            try
            {
                var GetData = await _recoveryService.GetDokumenParam();
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


        [HttpGet("jenispengurangan/list")]
        public async Task<ActionResult<GeneralResponses>> ListJenisPengurangan()

        {
            try
            {
                var GetData = await _recoveryService.GetJenisPengurangan();
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

        [HttpGet("branch/list")]
        public async Task<ActionResult<GeneralResponses>> GetListBranch()

        {
            try
            {
                var GetData = await _recoveryService.GetListBranch();
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

        [HttpGet("polarestruktur/list")]
        public async Task<ActionResult<GeneralResponses>> ListPolaRestruktur()

        {
            try
            {
                var GetData = await _recoveryService.GetPolaRestrukturParam();
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


        [HttpPost("create")]
        public async Task<ActionResult<GeneralResponses>> CreateRestructure([FromBody] CreateNewRestructure Entity)

        {
            try
            {
                var GetData = await _recoveryService.CreateRestructure(Entity);
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


        //API YANG DIPAKAI
        //restruktur-monitoring
        [HttpGet("monitoring/list")]
        public async Task<ActionResult<GenericResponses<ListRestructureDTO>>> RestructureListMonitoring()
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

        //API YANG DIPAKAI
        //TASKLIST RESTRUKTUR
        [HttpGet("Approval/List/{userid}")]
        public async Task<ActionResult<GeneralResponses>> ApprovalListMonitoring(string userid)
        {
            try
            {
                var GetData = await _recoveryService.TasklistRestrukture(userid);
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
        public IActionResult Index()
        {
            return View();
        }
    }
}
