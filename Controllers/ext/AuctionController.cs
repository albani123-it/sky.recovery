using Microsoft.AspNetCore.Mvc;
using sky.recovery.Interfaces;
using sky.recovery.Responses;
using sky.recovery.Services;
using System.Threading.Tasks;
using System;
using sky.recovery.DTOs.RequestDTO.Auction;
using Microsoft.Extensions.Configuration;

namespace sky.recovery.Controllers.ext
{
    [Route("skyrecovery/auction/[controller]")]

    public class AuctionController : RecoveryController
    {
        private IAuctionService _auctionservice { get; set; }

        private IAydaServices _aydaservices { get; set; }
    private IWorkflowServices _workflowService { get; set; }
    private IRestrukturServices _recoveryService { get; set; }

    private IDocServices _documentservices { get; set; }

    ModellingGeneralResponsesV2 _DataResponses = new ModellingGeneralResponsesV2();
        private readonly IConfiguration _configuration;
        private IUserService _User { get; set; }
        public AuctionController(IUserService User, IConfiguration configuration, IRestrukturServices recoveryService,IAuctionService auctionservice, IAydaServices aydaservices, IDocServices documentservices, IWorkflowServices workflowService) : base(User,recoveryService, configuration)
    {
            _User = User;
            _configuration = configuration;
            _auctionservice = auctionservice;
        _recoveryService = recoveryService;
        _aydaservices = aydaservices;
        _documentservices = documentservices;
        _workflowService = workflowService;
    }


        //V2
        [HttpGet("V2/Monitoring")]
        public async Task<ActionResult<GeneralResponses>> Monitoring()

        {
            var GetUserAgent = await Task.Run(() => GetUserAgents());

            var wrap = _DataResponses.Return();

            try
            {
                if (GetUserAgent.code == 200)
                {
                    var GetData = await _auctionservice.AuctionMonitoring(GetUserAgent.UserAgent);
                if (GetData.Status == true)
                {
                    return Ok(GetData.Returns);
                }
                else
                {
                    return BadRequest(GetData.Returns);
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
                return BadRequest(wrap);
            }
        }


        //V2
        [HttpPost("V2/Detail")]
        public async Task<ActionResult<GeneralResponsesDetailRestrukturV2>> Detail([FromBody] GetDetailAuctionDTO Entity)

        {
            var GetUserAgent = await Task.Run(() => GetUserAgents());

            var wrap = _DataResponses.ReturnDictionary();

            try
            {
                if (GetUserAgent.code == 200)
                {
                    var GetData = await _auctionservice.GetDetailAuction(Entity);
                    wrap.Message = GetData.message;
                    wrap.Status = GetData.Status;
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

        //V2
        [HttpGet("V2/SetActive/{id:int}/{status:int}")]
        public async Task<ActionResult<GeneralResponses>> SetActive(int id, int status)

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _auctionservice.SetIsActive(id,status);
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
        [HttpGet("V2/TaskList")]
        public async Task<ActionResult<GeneralResponses>> TaskList()

        {
            var GetUserAgent = await Task.Run(() => GetUserAgents());

            var wrap = _DataResponses.Return();

            try
            {
                if (GetUserAgent.code == 200)
                {
                    var GetData = await _auctionservice.AuctionTaskList(GetUserAgent.UserAgent);
                if (GetData.Status == true)
                {
                    return Ok(GetData.Returns);
                }
                else
                {
                    return BadRequest(GetData.Returns);
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
                return BadRequest(wrap);
            }
        }


        
    }
}
