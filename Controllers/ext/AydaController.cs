using Microsoft.AspNetCore.Mvc;
using sky.recovery.Responses;
using System.Threading.Tasks;
using System;
using sky.recovery.Interfaces;
using sky.recovery.DTOs.ResponsesDTO;

namespace sky.recovery.Controllers.ext
{
    [Route("skyrecovery/recovery/[controller]")]

    public class AydaController : RecoveryController
    {
        public IActionResult Index()
        {
            return View();
        }
        private IAydaServices _aydaServices { get; set; }
        private IRestrukturServices _recoveryService { get; set; }

        public AydaController(IRestrukturServices restrukturServices, IAydaServices aydaServices) : base(restrukturServices, aydaServices)
        {
            _aydaServices = aydaServices;
        }



        //V2
        [HttpGet("V2/Monitoring/list")]
        public async Task<ActionResult<GeneralResponses>> MonitoringAydaV2()

        {
            try
            {
                var GetData = await _aydaServices.MonitoringAYDAV2();
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

        //V2
        [HttpGet("V2/TaskList/list/{UserId}")]
        public async Task<ActionResult<GeneralResponses>> TaskListAydaV2(string UserId)

        {
            try
            {
                var GetData = await _aydaServices.TaskListAYDAV2(UserId);
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
        public async Task<ActionResult<GenericResponses<aydaDTO>>> TaskList(string userid)
        {
            try
            {
                var GetData = await _aydaServices.ListTaskListAyda(userid);
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
        [HttpGet("monitoring/List")]
        public async Task<ActionResult<GenericResponses<aydaDTO>>> MonitoringList()
        {
            try
            {
                var GetData = await _aydaServices.ListMonitoringAyda();
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

    }
}
