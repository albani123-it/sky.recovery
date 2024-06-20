using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sky.recovery.Responses;
using System.Threading.Tasks;
using System;
using sky.recovery.Interfaces;
using sky.recovery.DTOs.RequestDTO;

using sky.recovery.DTOs.ResponsesDTO;
using sky.recovery.Services;
using Microsoft.Extensions.Primitives;
using System.Linq;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Parameters;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using sky.recovery.Model;
using sky.recovery.Insfrastructures.Scafolding.SkyColl.Recovery;
using Microsoft.Extensions.Configuration;

namespace sky.recovery.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("skyrecovery/[controller]")]
    public class RecoveryController : ControllerBase
    {
        private IAydaServices _aydaservices { get; set; }
        private IAuctionService _auctionservice{ get; set; }

        private IRestrukturServices _recoveryService { get; set; }
        ModellingGeneralResponsesV2 _DataResponses = new ModellingGeneralResponsesV2();
        private readonly IConfiguration _configuration;

        public RecoveryController(IRestrukturServices recoveryService, IConfiguration configuration)
        {
            //_aydaservices = aydaservice;
            //_auctionservice = auctionservice;
            _recoveryService = recoveryService;
            _configuration = configuration;
        }


        ////API YANG DIPAKAI
        ////monitroing restruktur - list add restruktur
        //[HttpGet("nasabah/list/{userid}")]
        //public async Task<ActionResult<GenericResponses<MonitoringDetailRestructureDTO>>> ListDeskCall(string userid)
        //{
        //    var wrap = _DataResponses.Return();

        //    try
        //    {
        //        var GetData = await _recoveryService.MonitoringListDetail(userid);
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

        //        wrap.Message = ex.Message;
        //        wrap.Error = true;

        //        return BadRequest(wrap);
        //    }
        //}

        ////API YANG DIPAKAI
        ////monitor restruktur search list
        //[HttpPost("nasabah/listSearch")]
        //public async Task<ActionResult<GenericResponses<MonitoringDetailRestructureDTO>>> ListSearchDeskCall([FromBody] SearchListRestrucutre Entity)
        //{
        //    try
        //    {
        //        var GetData = await _recoveryService.ListSearchMonitoringListDetail(Entity);
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


        [HttpGet("GetFiturId")]
        public async Task<ActionResult<GeneralResponses>> GetFiturId()
        {
            var wrap = _DataResponses.Return();
            try
            {
                var Data = await _recoveryService.GetFiturId();
                wrap.Data = Data.Data;
                wrap.Message = Data.Message;
                wrap.Status = Data.Status;
                if(Data.Status==true)
                {
                    return Ok(wrap);
                }
                else
                {
                    return BadRequest(wrap);
                }
            }
            catch(Exception ex)
            {
                wrap.Message = ex.Message;
                wrap.Status = false;
                return StatusCode(500, wrap);
            }
        }


        [HttpGet("GetDocStatus")]
        public async Task<ActionResult<GeneralResponses>> GetDocStatus()
        {
            var wrap = _DataResponses.Return();
            try
            {
                var Data = await _recoveryService.GetStatusDocument();
                wrap.Data = Data.Data;
                wrap.Message = Data.message;
                wrap.Status = Data.status;
                if (Data.status == true)
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

        [HttpGet("GetUserAgent")]
        public async Task<(bool Status,int code, string Message,string UserAgent)> GetUserAgents()
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
                    var authToken= Request.Headers["Authorization"].ToString();
                    if (authToken.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                    {
                        var token = authToken.Substring("Bearer ".Length).Trim();
                        HttpContext.Request.Headers.TryGetValue("UserAgent", out StringValues authHeader);
                        var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                        var jwtToken = (JwtSecurityToken)validatedToken;
                        var userIdClaim = jwtToken.Claims.First(x => x.Type == "sub");


                        string UserAgentToken = authHeader.FirstOrDefault();
                        if(userIdClaim.Value==UserAgentToken)
                        {
                            return (true, 200,"Authorized", UserAgentToken);

                        }
                        else
                        {
                            return (true, 401, "Invalid User", null);

                        }

                    }
                    else
                    {
                       // return Unauthorized();
                        return (false,401, "Not Authorize",null);
                    }
                }
                else
                {
                    return (false, 401, "Invalid Token",null);
                }
                      
              
            }
            catch (Exception ex)
            {
                return (false, 500,ex.Message,null);
            }
        }


        [HttpGet("DummyTest/{Error}")]
        public async Task<ActionResult<GeneralResponses>> DummyTest(string Error)
        {
            try
            {
                var GetData = Error;
                if (GetData == "Error")
                {
                    return BadRequest(GetData);
                }
                else
                {
                    return Ok(GetData);
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


        [HttpGet("GetMasterStatus")]
        public async Task<ActionResult<GeneralResponses>> GetMasterStatus()
        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _recoveryService.GetMasterStatus();
                wrap.Message = GetData.message;
                wrap.Status = GetData.Status;
                wrap.Data = GetData.Data;
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
                var Return = new GeneralResponses()
                {
                    Message = ex.Message,
                    Error = true
                };
                return StatusCode(500,Return);
            }
        }

    }
}
