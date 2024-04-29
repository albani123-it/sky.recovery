using Microsoft.AspNetCore.Mvc;
using sky.recovery.DTOs.RequestDTO;
using sky.recovery.Responses;
using System.Threading.Tasks;
using System;

namespace sky.recovery.Controllers
{
    public class CollectionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        //[HttpPost("deskcall/detail")]
        //public async Task<ActionResult<GeneralResponses>> RestrukturNasabahDetail([FromBody] RequestRestrukturDetail Entity)
        //{
        //    try
        //    {
        //        var GetData = await _recoveryService.GetGeneralDetailNasabah(Entity);
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

    }
}
