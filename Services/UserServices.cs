using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Asn1.Ocsp;
using sky.recovery.DTOs.ResponsesDTO;
using sky.recovery.Helper.Config;
using sky.recovery.Insfrastructures;
using sky.recovery.Insfrastructures.Scafolding.SkyCore.Public;
using sky.recovery.Interfaces;
using sky.recovery.Libs;
using sky.recovery.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sky.recovery.Services
{
    public class UserServices : SkyCollConfig, IUserService
    {
        skycoreContext _skyCoreContext = new skycoreContext();
        SkyCorePublicDBContext _SkyCoreDBContext = new SkyCorePublicDBContext();
        public UserServices(IOptions<DbContextSettings> appsetting) : base(appsetting)
        {


        }

        public async Task<(bool status, string message, bool Result)> GetValidationPermission(string userid,string url)
        {

            var GetRole = await _SkyCoreDBContext.Users.Where(es => es.UsrUserid == userid).
                Select(es=>es.UsrAccessLevel).FirstOrDefaultAsync();
            var GetIdRole = Convert.ToInt32(GetRole);
            var GetAkses = await _SkyCoreDBContext.Roleapipermission.Where(es => es.RlId == GetIdRole &&
            es.Urlapi==url && es.Isaccessed==true
            
            ).AnyAsync();

            if(GetAkses==false)
            {
                return (true, "OK", true);
            }
            else
            {
                return (false, "Not Authorized", false);
            }
        }


        public async Task<(bool status, string message, bool Result)> GetValidationPermission_2(string userid, string url)
        {

            var GetRole = await _SkyCoreDBContext.Users.Where(es => es.UsrUserid == userid).
                Select(es => es.UsrAccessLevel).FirstOrDefaultAsync();
            var GetIdRole = Convert.ToInt32(GetRole);
            var GetAkses = await _SkyCoreDBContext.Roleapipermission.Where(es => es.RlId == GetIdRole &&
            es.Urlapi == url && es.Isaccessed == true

            ).AnyAsync();

            if (GetAkses == true)
            {
                return (true, "OK", true);
            }
            else
            {
                return (false, "Not Authorized", false);
            }
        }

        public async Task<(bool? Status, GenericResponses<UserDetailDTO> Returns)> GetDataUser(string userid)
        {
            var wrap = new GenericResponses<UserDetailDTO>
            {
                Status = false,
                Message = ""
            };
            try
            {
                var Datas = from u in _skyCoreContext.Users
                            join r in _skyCoreContext.Roles on Convert.ToInt32(u.UsrAccessLevel) equals r.RlId
                            where u.UsrUserid==userid
                            select new UserDetailDTO
                            {
                                iduser = u.UsrId,
                                branch = u.UsrBranch,

                                acceslevel = u.UsrAccessLevel,
                                email = u.UsrEmail,
                                userid = u.UsrUserid,
                                spvname = u.UsrSupervisor,
                                role = r.RlName,
                                RoleId =r.RlId 

                            };
                var Data = Datas.ToList();
              
                wrap.Status = false;
                wrap.Message = "OK";
                wrap.Data = Data;
              
                return (wrap.Status, wrap);
            }
            catch (Exception ex)
            {
                wrap.Status = false;
                wrap.Message = ex.Message;
                return (wrap.Status, wrap);
            }
        }

        public async Task<(bool? Status, GenericResponses<RoleDTO> Returns)> GetRoles(int userlevel)
        {
            var wrap = new GenericResponses<RoleDTO>
            {
                Status = false,
                Message = ""
            };
            try
            {
                var Data = await role.
                    Select(es => new RoleDTO
                    {
                        RoleId = es.rl_id,
                        RoleDesc = es.rl_description,
                        RoleName = es.rl_name
                      

                    }).
                    Where(es => es.RoleId == userlevel).
                    ToListAsync();
                wrap.Status = false;
                wrap.Message = "OK";
                wrap.Data = Data;

                return (wrap.Status, wrap);
            }
            catch (Exception ex)
            {
                wrap.Status = false;
                wrap.Message = ex.Message;
                return (wrap.Status, wrap);
            }
        }

    }
    }
