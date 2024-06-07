using Microsoft.AspNetCore.Mvc;
using sky.recovery.Interfaces;
using sky.recovery.Responses;
using sky.recovery.Services;
using System.Threading.Tasks;
using System;
using sky.recovery.DTOs.WorkflowDTO;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Linq;

namespace sky.recovery.Controllers
{
    [Route("skyrecovery/[controller]")]

    public class WorkflowController : ControllerBase
    {
        private IWorkflowServices _workflowService { get; set; }
        ModellingGeneralResponsesV2 _DataResponses = new ModellingGeneralResponsesV2();

        public WorkflowController(IWorkflowServices workflowservice)
        {
            _workflowService = workflowservice;
        }

        [HttpGet("GetUserAgent")]
        public async Task<(bool Status, int code, string Message, string UserAgent)> GetUserAgents()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var Key = "Skyworx C0n5ult1n9 2017";
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
        //V2
        [HttpPost("V2/WorkflowSubmit")]
        public async Task<ActionResult<GeneralResponses>> WorkflowSubmit([FromBody] SubmitWorkflowDTO Entity)

        {
            var wrap = _DataResponses.Return();

            try
            {
                var GetData = await _workflowService.SubmitWorkflowStep(Entity);
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
        [HttpPost("V2/GetDetailWorkflow")]
        public async Task<ActionResult<GeneralResponsesDictionaryV2>> GetDetailWorkflow([FromBody] GetDetailWFDTO Entity)

        {
            var wrap = _DataResponses.ReturnDictionary();

            try
            {
                var GetData = await _workflowService.GetDetailWorkflow(Entity);
                wrap.Data = GetData.DataWorkflow;
                wrap.Status = GetData.Status;
                wrap.Message = GetData.message;
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
        [HttpPost("V2/CallbackApproval")]
        public async Task<ActionResult<GeneralResponses>> CallbackApproval([FromBody] CallbackApprovalDTO Entity)

        {
            var wrap = _DataResponses.Return();
            var GetUserAgent = await Task.Run(() => GetUserAgents());

            try
            {
                if (GetUserAgent.code == 200)
                {
                    var GetData = await _workflowService.CallbackApproval_Dummy(GetUserAgent.UserAgent, Entity);
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
