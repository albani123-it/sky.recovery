using Microsoft.AspNetCore.Mvc;
using sky.recovery.Interfaces;
using sky.recovery.Responses;
using sky.recovery.Services;
using System.Threading.Tasks;
using System;

namespace sky.recovery.Controllers.ext
{
    [Route("api/skyrecovery/auction/[controller]")]

    public class AuctionController : RecoveryController
    {
        private IAuctionService _auctionservice { get; set; }

        private IAydaServices _aydaservices { get; set; }
    private IWorkflowServices _workflowService { get; set; }
    private IRestrukturServices _recoveryService { get; set; }

    private IDocServices _documentservices { get; set; }

    ModellingGeneralResponsesV2 _DataResponses = new ModellingGeneralResponsesV2();

    public AuctionController(IRestrukturServices recoveryService,IAuctionService auctionservice, IAydaServices aydaservices, IDocServices documentservices, IWorkflowServices workflowService) : base(recoveryService, aydaservices,auctionservice)
    {
            _auctionservice = auctionservice;
        _recoveryService = recoveryService;
        _aydaservices = aydaservices;
        _documentservices = documentservices;
        _workflowService = workflowService;
    }


        //V2
        [HttpGet("V2/Monitoring/{userid}")]
        public async Task<ActionResult<GeneralResponses>> Monitoring(string userid)

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _auctionservice.AuctionMonitoring(userid);
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
        [HttpGet("V2/TaskList/{userid}")]
        public async Task<ActionResult<GeneralResponses>> TaskList(string userid)

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _auctionservice.AuctionTaskList(userid);
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


        public IActionResult Index()
        {
            return View();
        }
    }
}
