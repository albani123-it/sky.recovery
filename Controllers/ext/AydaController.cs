﻿using Microsoft.AspNetCore.Mvc;
using sky.recovery.Interfaces;
using sky.recovery.Responses;
using System.Threading.Tasks;
using System;
using sky.recovery.DTOs.RequestDTO.Ayda;

namespace sky.recovery.Controllers.ext
{
    [Route("skyrecovery/ayda/[controller]")]

    public class AydaController : RecoveryController
    {
        private IAuctionService _auctionservice { get; set; }

        private IAydaServices _aydaservices{ get; set; }
        private IWorkflowServices _workflowService { get; set; }
        private IRestrukturServices _recoveryService { get; set; }

        private IDocServices _documentservices { get; set; }

        ModellingGeneralResponsesV2 _DataResponses = new ModellingGeneralResponsesV2();

        public AydaController(IRestrukturServices recoveryService,IAuctionService auctionservice, IAydaServices aydaservices, IDocServices documentservices, IWorkflowServices workflowService) : base()
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
        [HttpPost("V2/GetDetail")]
        public async Task<ActionResult<GeneralResponsesDictionaryV2>> GetDetail([FromBody] GetDetailAydaDTO Entity)

        {
            var wrap = _DataResponses.ReturnDictionary();

            try
            {
                var GetData = await _aydaservices.GetDetailAyda(Entity);
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

            catch (Exception ex)
            {
                wrap.Message = ex.Message;
                wrap.Status = false;
                return StatusCode(500,wrap);
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
                return StatusCode(500,wrap);
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
        [HttpGet("V2/SetActive/{id:int}/{status:int}")]
        public async Task<ActionResult<GeneralResponses>> SetActive(int id, int status)

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _aydaservices.SetIsActive(id,status);
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
        [HttpGet("V2/DummyLoan/{pagenumber:int}/{pagesize:int}")]
        public async Task<ActionResult<GeneralResponses>> DummyLoan(int pagenumber, int pagesize)

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _aydaservices.DummyNasabah(pagenumber, pagesize);
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
        [HttpGet("V2/InsertBulk/{banyak:int}")]
        public async Task<ActionResult<GeneralResponses>> InsertBulk(int banyak)

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _aydaservices.InsertBulk(banyak);
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
                var GetData = await _aydaservices.AydaTaskList(userid);
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
        [HttpGet("V2/GetMasterLoan")]
        public async Task<ActionResult<GeneralResponses>> GetMasterLoan()

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _aydaservices.GetMasterLoan();
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



    }
}
