﻿using Microsoft.AspNetCore.Mvc;
using sky.recovery.Interfaces;
using sky.recovery.Responses;
using System.Threading.Tasks;
using System;
using sky.recovery.DTOs.RequestDTO.Ayda;

namespace sky.recovery.Controllers.ext
{
    [Route("api/skyrecovery/ayda/[controller]")]

    public class AydaController : RecoveryController
    {

        private IAydaServices _aydaservices{ get; set; }
        private IWorkflowServices _workflowService { get; set; }
        private IRestrukturServices _recoveryService { get; set; }

        private IDocServices _documentservices { get; set; }

        ModellingGeneralResponsesV2 _DataResponses = new ModellingGeneralResponsesV2();

        public AydaController(IRestrukturServices recoveryService, IAydaServices aydaservices, IDocServices documentservices, IWorkflowServices workflowService) : base(recoveryService, aydaservices)
        {
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
                var GetData = await _aydaservices.AydaMonitoring(userid);
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
        [HttpPost("V2/CreateDraft")]
        public async Task<ActionResult<GeneralResponses>> CreateDraft([FromBody] CreateAydaDTO Entity)

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _aydaservices.AydaDraft(Entity);
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
        [HttpPost("V2/CreateSubmit")]
        public async Task<ActionResult<GeneralResponses>> AydaSubmit([FromBody] CreateAydaDTO Entity)

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _aydaservices.AydaSubmit(Entity);
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
        [HttpGet("V2/Tasklist/{userid}")]
        public async Task<ActionResult<GeneralResponses>> TaskList(string userid)

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _aydaservices.AydaMonitoring(userid);
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
