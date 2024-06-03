using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sky.recovery.Responses;
using System.Threading.Tasks;
using System;
using sky.recovery.Interfaces;
using sky.recovery.DTOs.RequestDTO;

using sky.recovery.DTOs.ResponsesDTO;
using sky.recovery.Services;
using Microsoft.Extensions.Primitives;
using System.Linq;

namespace sky.recovery.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("skyrecovery/[controller]")]
    public class RecoveryController : ControllerBase
    {
        private IAydaServices _aydaservices { get; set; }
        private IAuctionService _auctionservice{ get; set; }

        private IRestrukturServices _recoveryService { get; set; }
        ModellingGeneralResponsesV2 _DataResponses = new ModellingGeneralResponsesV2();

        public RecoveryController()
        {
            //_aydaservices = aydaservice;
            //_auctionservice = auctionservice;
            //_recoveryService = recoveryService;
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

        public async Task<(bool Status, string Message)> GetUserAgents()
        {
            try
            {

                HttpContext.Request.Headers.TryGetValue("UserAgent", out StringValues authHeader);
                string headerValue = authHeader.FirstOrDefault();
                return (true, headerValue);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
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
