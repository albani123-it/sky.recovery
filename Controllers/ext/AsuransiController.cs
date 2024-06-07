using Microsoft.AspNetCore.Mvc;
using sky.recovery.Interfaces;
using sky.recovery.Responses;
using sky.recovery.Services;
using System.Threading.Tasks;
using System;
using sky.recovery.DTOs.RequestDTO.Ayda;
using sky.recovery.DTOs.RequestDTO.Insurance;
using sky.recovery.DTOs.ResponsesDTO.Asuransi;
using Microsoft.Extensions.Configuration;

namespace sky.recovery.Controllers.ext
{
    [Route("skyrecovery/Asuransi/[controller]")]

    public class AsuransiController : RecoveryController
    {
        private IAuctionService _auctionservice { get; set; }
        private IAsuransiServices _asuransiservices{ get; set; }

        private IAydaServices _aydaservices { get; set; }
    private IWorkflowServices _workflowService { get; set; }
    private IRestrukturServices _recoveryService { get; set; }

    private IDocServices _documentservices { get; set; }

    ModellingGeneralResponsesV2 _DataResponses = new ModellingGeneralResponsesV2();
        private readonly IConfiguration _configuration;

        public AsuransiController(IConfiguration configuration, IRestrukturServices recoveryService,IAuctionService auctionservice, IAydaServices aydaservices, IAsuransiServices asuransiservices, IDocServices documentservices, IWorkflowServices workflowService) : base( recoveryService,configuration)
    {
            _configuration = configuration;
            _asuransiservices = asuransiservices;
            _auctionservice = auctionservice;
        _recoveryService = recoveryService;
        _aydaservices = aydaservices;
        _documentservices = documentservices;
        _workflowService = workflowService;
    }


        //V2
        [HttpPost("V2/CreateDraft")]
        public async Task<ActionResult<GeneralResponses>> AsuransiDraft([FromBody] CreateAsuransiDTO Entity)

        {
            var wrap = _DataResponses.Return();
            var GetUserAgent = await Task.Run(() => GetUserAgents());

            try
            {
                if (GetUserAgent.code == 200)
                {
                    var GetData = await _asuransiservices.AsuransiDraft(GetUserAgent.UserAgent, Entity);
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
                return StatusCode(500, wrap);
            }
        }



        //V2
        [HttpPost("V2/GetDetail")]
        public async Task<ActionResult<GeneralResponses>> GetDetail([FromBody] GetDetailAsuransiDTO Entity)

        {
            var wrap = _DataResponses.ReturnDictionary();
            var GetUserAgent = await Task.Run(() => GetUserAgents());

            try
            {
                if (GetUserAgent.code == 200)
                {


                    var GetData = await _asuransiservices.GetDetailAsuransi(Entity);
                    wrap.Data = GetData.DataNasabah;
                    wrap.Message = GetData.message;
                    wrap.Status = GetData.Status;
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
                return BadRequest(wrap);
            }
        }

        //V2
        [HttpPost("V2/CreateSubmit")]
        public async Task<ActionResult<GeneralResponses>> AsuransiSubmit([FromBody] CreateAsuransiDTO Entity)

        {
            var wrap = _DataResponses.Return();
            var GetUserAgent = await Task.Run(() => GetUserAgents());

            try
            {
                if (GetUserAgent.code == 200)
                {


                    var GetData = await _asuransiservices.AsuransiSubmit(GetUserAgent.UserAgent, Entity);
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
        [HttpGet("V2/Monitoring")]
        public async Task<ActionResult<GeneralResponses>> Monitoring()

        {
            var wrap = _DataResponses.Return();
            var GetUserAgent = await Task.Run(() => GetUserAgents());

            try
            {
                if (GetUserAgent.code == 200)
                {
                    var GetData = await _asuransiservices.InsuranceMonitoring(GetUserAgent.UserAgent);
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
        [HttpGet("V2/SetActive/{id:int}/{status:int}")]
        public async Task<ActionResult<GeneralResponses>> SetActive(int id, int status)

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _asuransiservices.SetIsActive(id,status);
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
        public async Task<ActionResult<GeneralResponses>> TaskList(string userid)

        {
            var wrap = _DataResponses.Return();
            var GetUserAgent = await Task.Run(() => GetUserAgents());
            try
            {
                if (GetUserAgent.code == 200)
                {
                    var GetData = await _asuransiservices.InsuranceTaskList(userid);
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
