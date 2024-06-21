using Microsoft.AspNetCore.Mvc;
using sky.recovery.DTOs.RequestDTO;
using sky.recovery.DTOs.ResponsesDTO;
using sky.recovery.Interfaces;
using sky.recovery.Responses;
using sky.recovery.Services;
using System.Threading.Tasks;
using System;
using sky.recovery.Interfaces.Ext;
using sky.recovery.DTOs.RequestDTO.Restrukture;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System.Linq;
using System.IO;
using System.Net.Mime;
using sky.recovery.DTOs.RepositoryDTO;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using static Dapper.SqlMapper;
using System.Security.Permissions;
using Microsoft.AspNetCore.Authorization;

namespace sky.recovery.Controllers.ext
{
    [Route("skyrecovery/recovery/[controller]")]
    public class RestrukturController : RecoveryController
    {
        private IAydaServices _aydaservices { get; set; }

        private IRestrukturServices _recoveryService { get; set; }
        private IExtRestruktureServices _ExtrecoveryService { get; set; }

        private IWorkflowServices _workflowService { get; set; }
        private IAuctionService _auctionservice { get; set; }
        private IUserService _User { get; set; }
        private IDocServices _documentservices{ get; set; }
        ModellingGeneralResponsesV2 _DataResponses = new ModellingGeneralResponsesV2();
        private readonly IConfiguration _configuration;

        public RestrukturController(IUserService User, IConfiguration configuration, IExtRestruktureServices ExtrecoveryService, IRestrukturServices recoveryService,IAuctionService auctionservice, IAydaServices aydaservices, IDocServices documentservices, IWorkflowServices workflowService) : base(User,recoveryService, configuration)
        {
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

            var GetUserAgent = await Task.Run(()=> GetUserAgents());
            try
            {
                if (GetUserAgent.code == 200)
                {
                    var GetData = await _recoveryService.MonitoringRestrukturV2(GetUserAgent.UserAgent);
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
                 wrap.Status  = false;
                return StatusCode(500,wrap);
            }
        }


        //V2
        [HttpGet("V3/Monitoring/list/{UserId}")]
        public async Task<ActionResult<GeneralResponses>> MonitoringRestrukturV3(string UserId)

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _ExtrecoveryService.GetMonitoringlist(UserId);
                wrap.Data = GetData.Data;
                wrap.Status = GetData.Status;
                wrap.Message = GetData.Message;
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
                return StatusCode(500, wrap);

            }
        }

        //V2
        [HttpGet("V2/GetDetailRestruktur/{loanid}/{idrestrukture}")]
        public async Task<ActionResult<GeneralResponses>> GetDetailRestruktur(int loanid,int idrestrukture)

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _recoveryService.GetDetailDraftingRestruktur(loanid, idrestrukture);
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
                 wrap.Status  = false;
                return BadRequest(wrap);
            }
        }

        //V2
        [HttpPost("V2/SearchingMonitoringRestruktur")]
        public async Task<ActionResult<GeneralResponses>> SearchingMonitoringRestruktur([FromBody] SearchingRestrukturDTO Entity)

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _recoveryService.SearchingMonitoringRestruktur(Entity);
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
        [HttpGet("V2/GetMasterLoan/list")]
        public async Task<ActionResult<GeneralResponses>> GetMasterLoan()

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _recoveryService.GetMasterLoanV2();
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
                 wrap.Status  = false;
                return BadRequest(wrap);
            }
        }


        //V2
        [HttpPost("V2/CreateDraftRestrukture")]
        public async Task<ActionResult<GeneralResponses>> CreateDraftRestrukture([FromBody] AddRestructureDTO Entity)

        {
            var wrap = _DataResponses.Return();
            var GetUserAgent = await Task.Run(() => GetUserAgents());

            try
            {
                if (GetUserAgent.code == 200)
                {

                    var GetData = await _recoveryService.CreateDraftRestrukture(GetUserAgent.UserAgent, Entity);
                    wrap.Status = GetData.Returns.Status;
                    wrap.Message = GetData.Returns.Message;
                    wrap.Data = GetData.Returns.Data;
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
        [HttpGet("V2/TaskList/list")]
        public async Task<ActionResult<GeneralResponses>> TaskListRestrukturV2()

        {
            var wrap = _DataResponses.Return();
            var GetUserAgent = await Task.Run(() => GetUserAgents());

            try
            {
                if (GetUserAgent.code == 200)
                {
                    var GetData = await _recoveryService.TaskListRestrukturV2(GetUserAgent.UserAgent);
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
                 wrap.Status  = false;
                return BadRequest(wrap);
            }
        }

        //V2
        [HttpPost("V2/DetailRestruktureForApprover")]
        public async Task<ActionResult<GeneralResponsesDictionaryV2>> DetailRestruktureForApprover([FromBody] GetDetailForApprover Entity)

        {
            var wrap = _DataResponses.ReturnDictionary();
            var GetUserAgent = await Task.Run(() => GetUserAgents());

            try
            {
                if (GetUserAgent.code == 200)
                {

                    var GetData = await _recoveryService.GetDetailRestruktureForApproval( Entity.Id,Entity.loanid,Entity.customerid);
                wrap.Status = GetData.Status;
                wrap.Message = GetData.Message;
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
                return StatusCode(500,wrap);
            }
        }
        //V2
        [HttpPost("V2/ActionApproval")]
        public async Task<ActionResult<GeneralResponses>> ActionApproval([FromBody] ApprovalActionDTO Entity)

        {
            var wrap = _DataResponses.Return();
            var GetUserAgent = await Task.Run(() => GetUserAgents());

            try
            {
                if (GetUserAgent.code == 200)
                {
                    var GetData = await _recoveryService.ActionApprovalRestrukture(GetUserAgent.UserAgent, Entity);
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
        [HttpGet("V2/GetWorkflowHistory/{idrequest:int}")]
        public async Task<ActionResult<GeneralResponses>> GetWorkflowHistory(int idrequest)

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _recoveryService.GetWorkflowHistory(idrequest);
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
        [HttpPost("V2/ConfigPolaRestrukture")]
        public async Task<ActionResult<GeneralResponses>> ConfigPolaRestrukture([FromBody] AddPolaDTO Entity)

        {
            var wrap = _DataResponses.Return();
            var GetUserAgent = await Task.Run(() => GetUserAgents());

            try
            {
                if (GetUserAgent.code == 200)
                {
                    var GetData = await _recoveryService.ConfigPolaRestrukture(GetUserAgent.UserAgent, Entity);
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
        [HttpPost("V2/SubmitRestrukture")]
        public async Task<ActionResult<GeneralResponses>> SubmitRestrukture([FromBody] SubmitRestruktureDTO Entity)

        {
            var wrap = _DataResponses.Return();
            var GetUserAgent = await Task.Run(() => GetUserAgents());


            try
            {
                if (GetUserAgent.code == 200)
                {
                    var GetData = await _recoveryService.SubmitRestrukture(GetUserAgent.UserAgent, Entity);
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
        //tambahkan pengecekan iscalculated
        [HttpPost("V2/Analisa")]
        public async Task<ActionResult<GeneralResponses>> AnalisaRestrukture([FromBody]ConfigAnalisaRestruktureDTO  Entity)

        {
            var wrap = _DataResponses.Return();
            var GetUserAgent = await Task.Run(() => GetUserAgents());

            try
            {
                if (GetUserAgent.code == 200)
                {
                    var GetData = await _recoveryService.ConfigAnalisaRestrukture(GetUserAgent.UserAgent, Entity);
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

        [HttpPost("V2/DummyBodyRequest")]
        public async Task<ActionResult<GeneralResponses>> DummyBodyRequest([FromBody] dynamic objects)
        {
            var wrap = _DataResponses.Return();
            var ListData = new List<dynamic>();
            try
            {
                wrap.Message = "OK";
                wrap.Status = true;
                ListData.Add(objects[0]);
                wrap.Data = ListData;
                return Ok(wrap);
            }
            catch(Exception ex)
            {
                wrap.Message = ex.Message;
                wrap.Status = false;
                return StatusCode(500, wrap);
            }
        }


        //V2
        //GetMetodeRestrukture
        [HttpPost("V2/PolaMetode")]
        public async Task<ActionResult<GeneralResponses>> PolaMetodeRestrukture([FromBody] GetPolaDTO Entity)

        {
            var wrap = _DataResponses.ReturnDictionary();

            try
            {
                if(Entity==null)
                {
                    wrap.Message = "Request Not Valid";
                    wrap.Status = false;
                    return BadRequest(wrap);
                }
                if (Entity.idloan == null)
                {
                    wrap.Message = "Loan Harus Dipilih";
                    wrap.Status = false;
                    return BadRequest(wrap);
                }
                if (Entity.idrestrukture == null)
                {
                    wrap.Message = "Restrukture Harus Dipilih";
                    wrap.Status = false;
                    return BadRequest(wrap);
                }

                var GetData = await _recoveryService.GetPolaMetodeRestrukture(Entity.idrestrukture,Entity.idloan);
                wrap.Message = "OK";
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

            catch (Exception ex)
            {
                wrap.Message = ex.Message;
                wrap.Status = false;
                return StatusCode(500,wrap);
            }
        }


        //V2
        //GetMetodeRestrukture
        [HttpGet("V2/SetActivation/{activation:int}/{id:int}/{loanid:int}")]
        public async Task<ActionResult<GeneralResponses>> SetActivation(int activation,int id, int loanid)

        {

            var wrap = _DataResponses.Return();
            var GetUserAgent = await Task.Run(() => GetUserAgents());

            try
            {
                if (GetUserAgent.code == 200)
                {
                    if (activation == 0)
                    {
                        var GetData = await _recoveryService.NonActiveRestrukture(GetUserAgent.UserAgent, id,loanid);
                        wrap.Message = GetData.message;
                        wrap.Status = GetData.status;
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
                        var GetData = await _recoveryService.ActiveRestrukture(GetUserAgent.UserAgent, id, loanid);
                        wrap.Message = GetData.message;
                        wrap.Status = GetData.status;
                        if (GetData.status == true)
                        {
                            return Ok(wrap);
                        }
                        else
                        {
                            return BadRequest(wrap);
                        }
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
        [HttpPost("V2/Remove/Permasalahan")]
        public async Task<ActionResult<GeneralResponses>> RemovePermasalahanRestrukture([FromBody] RemovePermasalahanDTO Entity)

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _recoveryService.RemovePermasalahanRestrukture(Entity);
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
        [HttpPost("V2/Create/Permasalahan")]
        public async Task<ActionResult<GeneralResponses>> CreatePermasalahan([FromBody] CreatePermasalahanDTO Entity)

        {
            var wrap = _DataResponses.Return();
            var GetUserAgent = await Task.Run(() => GetUserAgents());

            try
            {
                if (GetUserAgent.code == 200)
                {
                    var GetData = await _recoveryService.CreatePermasalahan(GetUserAgent.UserAgent, Entity);
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
        [HttpPost("V2/Update/Permasalahan")]
        public async Task<ActionResult<GeneralResponses>> UpdatePermasalahan([FromBody] UpdatePermasalahanDTO Entity)

        {
            var wrap = _DataResponses.Return();
            var GetUserAgent = await Task.Run(() => GetUserAgents());

            try
            {
                if (GetUserAgent.code == 200)
                {
                    var GetData = await _recoveryService.UpdatePermasalahan(GetUserAgent.UserAgent, Entity);
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
        [HttpGet("V2/RemoveDoc/{id:int}")]
        public async Task<ActionResult<GeneralResponses>> RemoveDoc(int id)

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _recoveryService.RemoveDocRestrukture(id);
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

            catch (Exception ex)
            {
                wrap.Message = ex.Message;
                wrap.Status = false;
                return StatusCode(500,wrap);
            }
        }



        //V2
        [HttpPost("V2/Detail/Documents")]
        public async Task<ActionResult<GeneralResponses>> DetailDocuments([FromBody] GetDocumentRestruktureDTO Entity)

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _recoveryService.GetMasterDocRule(Entity);
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
        [HttpGet("V2/GetDetailAnalisa/{id:int}")]
        public async Task<ActionResult<GeneralResponses>> GetDetaiAnalisa(int id)

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _recoveryService.GetAnalisaRestrukture(id);
                wrap.Data = GetData.Data;
                wrap.Status = GetData.Status;
                wrap.Message = GetData.Message;
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
        [HttpGet("V2/GetDetaiPolaRestruk/{id:int}")]
        public async Task<ActionResult<GeneralResponses>> GetDetaiPolaRestruk(int id)

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _recoveryService.GetPolaRestrukture(id);
                wrap.Data = GetData.Data;
                wrap.Status = GetData.Status;
                wrap.Message = GetData.Message;
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
                return StatusCode(500, wrap);
            }
        }

        //V2
        [HttpPost("V2/Documents/Upload")]
        public async Task<ActionResult<GeneralResponses>> UploadDocRestruktur([FromForm] UploadDocRestrukturDTO Entity)

        {
            var wrap = _DataResponses.Return();
            var GetUserAgent = await Task.Run(() => GetUserAgents());

            try
            {
                if (GetUserAgent.code == 200)
                {
                    var GetData = await _recoveryService.UploadDocRestrukture(GetUserAgent.UserAgent, Entity);
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
        [HttpPost("V2/RemoveDraft")]
        public async Task<ActionResult<GeneralResponses>> RemoveDraftRestrukture([FromBody] RemoveDraftRestruktureDTO Entity)

        {
            var wrap = _DataResponses.Return();
            var GetUserAgent = await Task.Run(() => GetUserAgents());

            try
            {
                if (GetUserAgent.code == 200)
                {
                    if (Entity==null)
                {
                    wrap.Message = "Request Not Valid";
                    wrap.Status = false;
                    return BadRequest(wrap);
                }
                var GetData = await _recoveryService.RemoveDraftRestukture(GetUserAgent.UserAgent, Entity.userid,Entity.loanid,Entity.restruktureid);
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
