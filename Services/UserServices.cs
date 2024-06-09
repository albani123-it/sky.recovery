using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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
        public UserServices(IOptions<DbContextSettings> appsetting) : base(appsetting)
        {


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
                var Data =  _skyCoreContext.Users.Where(es => es.UsrUserid== userid).AsEnumerable().
                    Select(es=>new UserDetailDTO
                    {
                        iduser=es.UsrId,
                        branch=es.UsrBranch,
                        
                        acceslevel=es.UsrAccessLevel,
                        email=es.UsrEmail,
                        userid=es.UsrUserid,
                        spvname=es.UsrSupervisor,
                        role= role.Where(x=>x.rl_id== Convert.ToInt32(es.UsrAccessLevel)).AsEnumerable().Select(es=>es.rl_name).FirstOrDefault(),
                        RoleId= role.Where(x => x.rl_id == Convert.ToInt32(es.UsrAccessLevel)).AsEnumerable().Select(es => es.rl_id).FirstOrDefault()

                    }).ToList();

                //var ListData = new List<UserDetailDTO>();
                //var CheckDataRole = await GetRoles(Convert.ToInt32(Data.FirstOrDefault().acceslevel));
                //foreach(var x in Data)
                //{
                //    x.iduser = x.iduser;
                //    x.branch = x.branch;
                //    x.acceslevel = x.acceslevel;
                //    x.email = x.email;
                //    x.userid = x.userid;
                //    x.role = CheckDataRole.Returns.Data.FirstOrDefault().RoleName;
                //    x.RoleId = CheckDataRole.Returns.Data.FirstOrDefault().RoleId;

                //    ListData.Add(x);
                //}
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
