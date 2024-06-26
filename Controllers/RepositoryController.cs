﻿using Microsoft.AspNetCore.Mvc;
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
using sky.recovery.DTOs.HelperDTO;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using sky.recovery.DTOs.RequestDTO.CommonDTO;
using System.Net.Mime;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace sky.recovery.Controllers
{
    [Route("skyrecovery/[controller]")]

    public class RepositoryController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        private IRepositoryServices _RepositoryServices { get; set; }
        private IDocServices _documentservices { get; set; }
        ModellingGeneralResponsesV2 _DataResponses = new ModellingGeneralResponsesV2();
        private readonly IConfiguration _configuration;

        public RepositoryController(IConfiguration configuration, IRepositoryServices RepositoryServices, IWebHostEnvironment environment, IDocServices docservice)
        {

            _RepositoryServices = RepositoryServices;
            _environment = environment;
            _configuration = configuration;
            _documentservices = docservice;
        }


        [HttpGet("GetUserAgent")]
        public async Task<(bool Status, int code, string Message, string UserAgent)> GetUserAgents()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var Key = _configuration.GetSection("TokenAuthentication:SecretKey").Value;
            var EncodingKey = Encoding.ASCII.GetBytes(Key);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(EncodingKey)
            };
            try
            {
                if (Request.Headers.ContainsKey("Authorization"))
                {
                    var authToken = Request.Headers["Authorization"].ToString();
                    if (authToken.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                    {
                        var token = authToken.Substring("Bearer ".Length).Trim();
                        HttpContext.Request.Headers.TryGetValue("UserAgent", out StringValues authHeader);
                        var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                        var jwtToken = (JwtSecurityToken)validatedToken;
                        var userIdClaim = jwtToken.Claims.First(x => x.Type == "sub");


                        string UserAgentToken = authHeader.FirstOrDefault();
                        if (userIdClaim.Value == UserAgentToken)
                        {
                            return (true, 200, "Authorized", UserAgentToken);

                        }
                        else
                        {
                            return (true, 401, "Invalid User", null);

                        }

                    }
                    else
                    {
                        // return Unauthorized();
                        return (false, 401, "Not Authorize", null);
                    }
                }
                else
                {
                    return (false, 401, "Invalid Token", null);
                }


            }
            catch (Exception ex)
            {
                return (false, 500, ex.Message, null);
            }
        }


        [HttpPost("download")]
        public async Task<IActionResult> DownloadFile([FromBody] DownloadDTO Entity)
        {
            var wrap = _DataResponses.Return();
            try
            {
                //var filePath = Path.Combine(_filePath, fileName);
                var GetFilePath = await _RepositoryServices.RetrieveFilePath(Entity.repoid, Entity.fiturid);
                var filePath = GetFilePath.path;

                //if (!System.IO.File.Exists(filePath))
                //{
                //    wrap.Message = "File Not Exist";
                //    wrap.Status = false;
                //    return StatusCode(404, "File Not Found");
                //}

                var memory = new MemoryStream();
                using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                return File(memory, MediaTypeNames.Application.Octet, GetFilePath.name);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [HttpPost("DownloadFromFTP")]
        public async Task<IActionResult> DownloadFromFTP([FromBody] DownloadDTO Entity)
        {
            var wrap = _DataResponses.Return();
            try
            {
                //var filePath = Path.Combine(_filePath, fileName);
                var GetFilePath = await _RepositoryServices.RetrieveFilePath(Entity.repoid, Entity.fiturid);
                var filePath = GetFilePath.path;

                var Data = await _RepositoryServices.DownloadFromFTP(filePath,GetFilePath.name);
               
                Data.x.Position = 0;
                return File(Data.x, MediaTypeNames.Application.Octet, GetFilePath.name);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //V2
        //tidak baca sheet
        [HttpPost("V2/Upload")]
        public async Task<ActionResult<GeneralResponses>> Upload([FromForm] RepoReqDTO Entity)

        {
            var wrap = _DataResponses.Return();
            var GetUserAgent = await Task.Run(() => GetUserAgents());
           

                try
                {
                if (GetUserAgent.code == 200)
                {
                    var GetData = await _RepositoryServices.UploadToLocalServices(GetUserAgent.UserAgent, Entity);
                wrap.Message = GetData.Message;
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
                return StatusCode(500,wrap);
            }
        }


        //V2
        //tidak baca sheet
        [HttpPost("V2/UploadToFTP")]
        public async Task<ActionResult<GeneralResponses>> UploadToFTP([FromForm] RepoReqDTO Entity)

        {
            var wrap = _DataResponses.Return();
            var GetUserAgent = await Task.Run(() => GetUserAgents());


            try
            {
                if (GetUserAgent.code == 200)
                {
                    var GetData = await _RepositoryServices.UploadToFTPServices(GetUserAgent.UserAgent, Entity);
                    wrap.Message = GetData.Message;
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
                return StatusCode(500, wrap);
            }
        }

        //V2
        //tidak baca sheet
        [HttpGet("V2/RemoveDoc/{id:int}")]
        public async Task<ActionResult<GeneralResponses>> RemoveDoc(int id)

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _RepositoryServices.RemoveDoc(id);
                wrap.Message = GetData.Message;
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
                return StatusCode(500, wrap);
            }
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
        [HttpPost("V2/GenerateLetterDummy")]
        public async Task<ActionResult> GenerateLetterDummy([FromBody] FormatDTO Entity)

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
                var path = Path.Combine(_environment.WebRootPath, "File");
                var nm = Path.Combine(path, "arthagraha.png");

                // Draw the letter content
               // gfx.DrawString("Logo Company", font, XBrushes.Black, new XRect(30, 30, page.Width, page.Height), XStringFormats.TopLeft);
                XImage image = XImage.FromFile(nm);

                // Draw the image onto the page
                gfx.DrawImage(image, 30, 30, 200, 100);

                var FormatHeaderAlamat = Entity.Header.AlamatPT + "\n" +  Entity.Header.NamaGedung 
                    + "\n" + Entity.Header.AlamatJalanSurat
                    + "\n" + Entity.Header.fax + "\n" + Entity.Header.phone + "\n"
                    + Entity.Header.email
                    + "\n" + Entity.Header.namakota + "," + DateTime.Now.ToString("dd MMMM yyyy");
                string[] lines = FormatHeaderAlamat.Split('\n');

                var FormatKepada = "Kepada YTH." + "\n" + Entity.KepadaDTO.Namakepada + "\n" + "Di Tempat";
                string[] linesKepada = FormatKepada.Split('\n');

                var FormatPerihal = "Perihal : " + Entity.Perihal.Perihal;
                //  var path2 = Path.Combine(_environment.WebRootPath, "File");
                // var nm2 = Path.Combine(path2, "logoalamat.png");
                //  XImage image2 = XImage.FromFile(nm2);

                // Draw the image onto the page
                // gfx.DrawImage(image2, 10, 30, 200, 100);
                //header alamat pt
                XPoint startPoint = new XPoint(100, 100);

                foreach (var x in lines)
                {
                    gfx.DrawString(x, font, XBrushes.Black, new XRect(Entity.Header.x, Entity.Header.y, page.Width, page.Height), XStringFormats.TopRight);
                    Entity.Header.y += 15;
                }

                //gfx.DrawString(Entity.Header.NamaGedung, font, XBrushes.Black, new XRect(Entity.Header.x, Entity.Header.y+10, page.Width, page.Height), XStringFormats.TopRight);
                //gfx.DrawString(Entity.Header.AlamatJalanSurat, font, XBrushes.Black, new XRect(Entity.Header.x, Entity.Header.y+30, page.Width, page.Height), XStringFormats.TopRight);
                //gfx.DrawString(Entity.Header.fax, font, XBrushes.Black, new XRect(Entity.Header.x, Entity.Header.y+40, page.Width, page.Height), XStringFormats.TopRight);
                //gfx.DrawString(Entity.Header.phone, font, XBrushes.Black, new XRect(Entity.Header.x, Entity.Header.y+50, page.Width, page.Height), XStringFormats.TopRight);
                //gfx.DrawString(Entity.Header.email, font, XBrushes.Black, new XRect(Entity.Header.x, Entity.Header.y+60, page.Width, page.Height), XStringFormats.TopRight);

                gfx.DrawString("No: "+Entity.NoSurat.nomor, font, XBrushes.Black, new XRect(Entity.NoSurat.x, Entity.NoSurat.y, page.Width, page.Height), XStringFormats.TopLeft);
               foreach(var x in linesKepada)
                {
                    gfx.DrawString(x, font, XBrushes.Black, new XRect(Entity.KepadaDTO.x, Entity.KepadaDTO.y, page.Width, page.Height), XStringFormats.TopLeft);
                    Entity.KepadaDTO.y += 15;

                }

                gfx.DrawString(FormatPerihal, font, XBrushes.Black, new XRect(Entity.Perihal.x, Entity.Perihal.y, page.Width, page.Height), XStringFormats.TopLeft);
                //gfx.DrawString("test/ntest", font, XBrushes.Black, new XRect(Entity.Perihal.x, Entity.Perihal.y, page.Width, page.Height), XStringFormats.TopLeft);

                gfx.DrawString("Dengan hormat,", font, XBrushes.Black, new XRect(30, Entity.Perihal.y+20, page.Width, page.Height), XStringFormats.TopLeft);
                gfx.DrawString(Entity.Paragraf_pertama.text, font, XBrushes.Black, new XRect(Entity.Paragraf_pertama.x, Entity.Paragraf_pertama.y, page.Width, page.Height), XStringFormats.TopLeft);
                gfx.DrawString(Entity.Penutup.text, font, XBrushes.Black, new XRect(Entity.Penutup.x, Entity.Penutup.y, page.Width, page.Height), XStringFormats.TopLeft);
                //   gfx.DrawString(FormatPerihal, font, XBrushes.Black, new XRect(Entity.Perihal.x, Entity.Perihal.y, page.Width, page.Height), XStringFormats.TopLeft);

                // gfx.DrawString(FormatPerihal, font, XBrushes.Black, new XRect(Entity.Perihal.x, Entity.Perihal.y, page.Width, page.Height), XStringFormats.TopLeft);


                //gfx.DrawString("Recipient Name:", font, XBrushes.Black, new XRect(30, 120, page.Width, page.Height), XStringFormats.TopLeft);
                //gfx.DrawString("Recipient Address:", font, XBrushes.Black, new XRect(30, 140, page.Width, page.Height), XStringFormats.TopLeft);
                //gfx.DrawString("Dear Recipient,", font, XBrushes.Black, new XRect(30, 170, page.Width, page.Height), XStringFormats.TopLeft);
                //gfx.DrawString("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis vehicula, ligula nec ultrices viverra, ex turpis sollicitudin nisl, nec placerat lectus quam a odio.", font, XBrushes.Black, new XRect(30, 190, page.Width, page.Height), XStringFormats.TopLeft);
                //gfx.DrawString("Sincerely,", font, XBrushes.Black, new XRect(30, 220, page.Width, page.Height), XStringFormats.TopLeft);
                //gfx.DrawString("Your Name", font, XBrushes.Black, new XRect(30, 240, page.Width, page.Height), XStringFormats.TopLeft);

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


     
    }
}
