using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using sky.recovery.Interfaces.Ext;
using sky.recovery.Interfaces;
using sky.recovery.Responses;
using System.Threading.Tasks;
using System;
using sky.recovery.DTOs.RequestDTO.WO;

namespace sky.recovery.Controllers.ext
{
    [Route("skyrecovery/WriteOff/[controller]")]
    public class WriteOffController : RecoveryController
    {
        private IWriteOffServices _WOServices { get; set; }
        private IAydaServices _aydaservices { get; set; }

        private IRestrukturServices _recoveryService { get; set; }
        private IExtRestruktureServices _ExtrecoveryService { get; set; }

        private IWorkflowServices _workflowService { get; set; }
        private IAuctionService _auctionservice { get; set; }
        private IUserService _User { get; set; }
        private IDocServices _documentservices { get; set; }
        ModellingGeneralResponsesV2 _DataResponses = new ModellingGeneralResponsesV2();
        private readonly IConfiguration _configuration;

        public WriteOffController(IWriteOffServices WOServices, IUserService User, IConfiguration configuration, IExtRestruktureServices ExtrecoveryService, IRestrukturServices recoveryService, IAuctionService auctionservice, IAydaServices aydaservices, IDocServices documentservices, IWorkflowServices workflowService) : base(User, recoveryService, configuration)
        {
            _WOServices = WOServices;
            _User = User;
            _configuration = configuration;
            _auctionservice = auctionservice;
            _recoveryService = recoveryService;
            _aydaservices = aydaservices;
            _documentservices = documentservices;
            _workflowService = workflowService;
            _ExtrecoveryService = ExtrecoveryService;
        }


        //V2
        [HttpGet("V2/Monitoring/list")]
        //[Authorize(Policy = "lvl_rcv_rst_mtr_list")]

        public async Task<ActionResult<GeneralResponses>> MonitoringRestrukturV2()

        {
            var wrap = _DataResponses.Return();

            var GetUserAgent = await Task.Run(() => GetUserAgents());
            try
            {
                if (GetUserAgent.code == 200)
                {
                    var GetData = await _WOServices.GetMonitorList(GetUserAgent.UserAgent);
                    wrap.Status = GetData.status;
                    wrap.Message = GetData.message;
                    wrap.Data = GetData.Data;
                    if (GetData.status== true)
                    {
                        return Ok(wrap);
                    }
                    else
                    {
                        return BadRequest(wrap);
                    }
                }
                else
                {
                    wrap.Message = GetUserAgent.Message;
                    wrap.Status = false;
                    return StatusCode(GetUserAgent.code, wrap);
                }

            }

            catch (Exception ex)
            {
                wrap.Message = ex.Message;
                wrap.Status = false;
                return StatusCode(500, wrap);
            }
        }


        //V2
        [HttpGet("V2/Task/list")]
        //[Authorize(Policy = "lvl_rcv_rst_mtr_list")]

        public async Task<ActionResult<GeneralResponses>> Tasklist()

        {
            var wrap = _DataResponses.Return();

            var GetUserAgent = await Task.Run(() => GetUserAgents());
            try
            {
                if (GetUserAgent.code == 200)
                {
                    var GetData = await _WOServices.GetTaskList(GetUserAgent.UserAgent);
                    wrap.Status = GetData.status;
                    wrap.Message = GetData.message;
                    wrap.Data = GetData.Data;
                    if (GetData.status == true)
                    {
                        return Ok(wrap);
                    }
                    else
                    {
                        return BadRequest(wrap);
                    }
                }
                else
                {
                    wrap.Message = GetUserAgent.Message;
                    wrap.Status = false;
                    return StatusCode(GetUserAgent.code, wrap);
                }

            }

            catch (Exception ex)
            {
                wrap.Message = ex.Message;
                wrap.Status = false;
                return StatusCode(500, wrap);
            }
        }


        //V2
        [HttpPost("V2/Detail")]
        //[Authorize(Policy = "lvl_rcv_rst_mtr_list")]

        public async Task<ActionResult<GeneralResponses>> Detail([FromBody] GetDetailWO Entity)

        {
            var wrap = _DataResponses.ReturnDictionary();

            var GetUserAgent = await Task.Run(() => GetUserAgents());
            try
            {
                if (GetUserAgent.code == 200)
                {
                    var GetData = await _WOServices.GetDetailWO(Entity);
                    
                    wrap.Status = GetData.Status;
                    wrap.Message = GetData.message;
                    wrap.Data = GetData.DataNasabah;
                    if (GetData.Status == true)
                    {
                        return Ok(wrap);
                    }
                    else
                    {
                        return BadRequest(wrap);
                    }
                }
                else
                {
                    wrap.Message = GetUserAgent.Message;
                    wrap.Status = false;
                    return StatusCode(GetUserAgent.code, wrap);
                }

            }

            catch (Exception ex)
            {
                wrap.Message = ex.Message;
                wrap.Status = false;
                return StatusCode(500, wrap);
            }
        }

    }
}
