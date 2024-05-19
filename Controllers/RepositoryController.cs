using Microsoft.AspNetCore.Mvc;
using sky.recovery.DTOs.RequestDTO;
using sky.recovery.Interfaces;
using sky.recovery.Responses;
using sky.recovery.Services;
using System.Threading.Tasks;
using System;
using sky.recovery.DTOs.RepositoryDTO;
using System.IO;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

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
        [HttpPost("V2/ReadMyFile")]
        public async Task<ActionResult<GeneralResponses>> ReadMyFileBySheet([FromBody] ReadMyFileByExistingDTO Entity)

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _documentservices.ReadExcelByFileExisting(Entity.path,Entity.sheet);
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
        [HttpGet("V2/GenerateLetterDummy")]
        public async Task<ActionResult> GenerateLetterDummy()

        {
            var wrap = _DataResponses.Return();

            try
            {
                // var GetData = await _documentservices.GetTemplateLetter();
                // Create a new MemoryStream to store the PDF
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                MemoryStream stream = new MemoryStream();

                // Create a new PDF document
                PdfDocument document = new PdfDocument();

                // Add a page to the document
                PdfPage page = document.AddPage();

                // Create a drawing object for the page
                XGraphics gfx = XGraphics.FromPdfPage(page);

                // Set the font
                XFont font = new XFont("Arial", 12, XFontStyle.Regular);

                // Draw the letter content
                gfx.DrawString("Your Company Name", font, XBrushes.Black, new XRect(30, 30, page.Width, page.Height), XStringFormats.TopLeft);
                gfx.DrawString("123 Main Street", font, XBrushes.Black, new XRect(30, 50, page.Width, page.Height), XStringFormats.TopLeft);
                gfx.DrawString("City, State ZIP", font, XBrushes.Black, new XRect(30, 70, page.Width, page.Height), XStringFormats.TopLeft);
                gfx.DrawString("Date: May 20, 2024", font, XBrushes.Black, new XRect(30, 100, page.Width, page.Height), XStringFormats.TopLeft);
                gfx.DrawString("Recipient Name:", font, XBrushes.Black, new XRect(30, 120, page.Width, page.Height), XStringFormats.TopLeft);
                gfx.DrawString("Recipient Address:", font, XBrushes.Black, new XRect(30, 140, page.Width, page.Height), XStringFormats.TopLeft);
                gfx.DrawString("Dear Recipient,", font, XBrushes.Black, new XRect(30, 170, page.Width, page.Height), XStringFormats.TopLeft);
                gfx.DrawString("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis vehicula, ligula nec ultrices viverra, ex turpis sollicitudin nisl, nec placerat lectus quam a odio.", font, XBrushes.Black, new XRect(30, 190, page.Width, page.Height), XStringFormats.TopLeft);
                gfx.DrawString("Sincerely,", font, XBrushes.Black, new XRect(30, 220, page.Width, page.Height), XStringFormats.TopLeft);
                gfx.DrawString("Your Name", font, XBrushes.Black, new XRect(30, 240, page.Width, page.Height), XStringFormats.TopLeft);

                // Save the PDF to the MemoryStream
                document.Save(stream);

                // Reset the stream position to the beginning
                stream.Position = 0;

                // Return the PDF file
                return File(stream, "application/pdf", "Letter.pdf");
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
