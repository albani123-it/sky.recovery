using Microsoft.AspNetCore.Mvc;
using sky.recovery.DTOs.RequestDTO;
using sky.recovery.Interfaces;
using sky.recovery.Responses;
using sky.recovery.Services;
using System.Threading.Tasks;
using System;

namespace sky.recovery.Controllers
{
    [Route("api/skyrecovery/[controller]")]

    public class RepositoryController : Controller
    {
        private IDocServices _documentservices { get; set; }
        ModellingGeneralResponsesV2 _DataResponses = new ModellingGeneralResponsesV2();

        public RepositoryController(IDocServices docservice)
        {
            _documentservices = docservice;
        }


        //V2
        //tidak baca sheet
        [HttpGet("V2/GetMyFile/{userid}")]
        public async Task<ActionResult<GeneralResponses>> GetMyFile(string userid)

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _documentservices.RetriveAllListByUserId(userid);
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
        //tidak baca sheet
        [HttpGet("V2/ReadMySheetFile/{id:int}")]
        public async Task<ActionResult<GeneralResponses>> GetMyFile(int id)

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _documentservices.ReadExcelSheetByFileExisting(id);
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
        //tidak baca sheet
        [HttpGet("V2/ExcelReader")]
        public async Task<ActionResult<GeneralResponses>> ExcelReader()

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _documentservices.ExcelReader();
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
        //tidak baca sheet
        [HttpPost("V2/ExcelReaderByUpload")]
        public async Task<ActionResult<GeneralResponses>> ExcelReaderByUpload([FromForm] UploadExcelDTO Entity)

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _documentservices.ReadExcelByUpload(Entity);
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
        //tidak baca sheet
        [HttpGet("V2/RetrieveSheet")]
        public async Task<ActionResult<GeneralResponses>> RetrieveSheet()

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _documentservices.ReadExcelSheet();
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
        //tidak baca sheet
        [HttpGet("V2/ReadExcelBySheet/{sheetname}")]
        public async Task<ActionResult<GeneralResponses>> GetDataBySheet(string sheetname)

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _documentservices.RetrieveDataBySheet(sheetname);
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
