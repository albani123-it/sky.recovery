using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sky.recovery.Responses;
using System.Threading.Tasks;
using System;
using sky.recovery.Interfaces;
using sky.recovery.DTOs.RequestDTO;

using sky.recovery.DTOs.ResponsesDTO;

namespace sky.recovery.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/skyrecovery/[controller]")]
    public class RecoveryController : Controller
    {
        private IRestrukturServices _recoveryService { get; set; }
        private IAydaServices _aydaService{ get; set; }
        ModellingGeneralResponsesV2 _DataResponses = new ModellingGeneralResponsesV2();

        public RecoveryController(IRestrukturServices recoveryService, IAydaServices aydaService)
        {
            _recoveryService = recoveryService;
            _aydaService = aydaService;
        }
        public IActionResult Index()
        {
            return View();
        }

        ////API YANG DIPAKAI
        ////monitroing restruktur - list add restruktur
        //[HttpGet("nasabah/list/{userid}")]
        //public async Task<ActionResult<GenericResponses<MonitoringDetailRestructureDTO>>> ListDeskCall(string userid)
        //{
        //    var wrap = _DataResponses.Return();

        //    try
        //    {
        //        var GetData = await _recoveryService.MonitoringListDetail(userid);
        //        if (GetData.Error == true)
        //        {
        //            return BadRequest(GetData.Returns);
        //        }
        //        else
        //        {
        //            return Ok(GetData.Returns);
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        wrap.Message = ex.Message;
        //        wrap.Error = true;
                   
        //        return BadRequest(wrap);
        //    }
        //}

        ////API YANG DIPAKAI
        ////monitor restruktur search list
        //[HttpPost("nasabah/listSearch")]
        //public async Task<ActionResult<GenericResponses<MonitoringDetailRestructureDTO>>> ListSearchDeskCall([FromBody] SearchListRestrucutre Entity)
        //{
        //    try
        //    {
        //        var GetData = await _recoveryService.ListSearchMonitoringListDetail(Entity);
        //        if (GetData.Error == true)
        //        {
        //            return BadRequest(GetData.Returns);
        //        }
        //        else
        //        {
        //            return Ok(GetData.Returns);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        var Return = new GeneralResponses()
        //        {
        //            Message = ex.Message,
        //            Error = true
        //        };
        //        return BadRequest(Return);
        //    }
        //}


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
