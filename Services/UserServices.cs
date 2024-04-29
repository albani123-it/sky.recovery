using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using sky.recovery.DTOs.ResponsesDTO;
using sky.recovery.Helper.Config;
using sky.recovery.Insfrastructures;
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
        public UserServices(IOptions<DbContextSettings> appsetting) : base(appsetting)
        {


        }
        public async Task<(bool? Error, GenericResponses<UserDetailDTO> Returns)> GetDataUser(string userid)
        {
            var wrap = new GenericResponses<UserDetailDTO>
            {
                Error = false,
                Message = ""
            };
            try
            {
                var Data = await users.
                    Select(es=>new UserDetailDTO
                    {
                        iduser=es.usr_id,
                        branch=es.usr_branch,
                        
                        acceslevel=es.usr_access_level,
                        email=es.usr_email,
                        userid=es.usr_userid

                    }).
                    Where(es => es.userid== userid).
                    ToListAsync();

                var ListData = new List<UserDetailDTO>();
                var CheckDataRole = await GetRoles(Convert.ToInt32(Data.FirstOrDefault().acceslevel));
                foreach(var x in Data)
                {
                    x.iduser = x.iduser;
                    x.branch = x.branch;
                    x.acceslevel = x.acceslevel;
                    x.email = x.email;
                    x.userid = x.userid;
                    x.role = CheckDataRole.Returns.Data.FirstOrDefault().RoleName;
                    ListData.Add(x);
                }
                wrap.Error = false;
                wrap.Message = "OK";
                wrap.Data = ListData;
              
                return (wrap.Error, wrap);
            }
            catch (Exception ex)
            {
                wrap.Error = false;
                wrap.Message = ex.Message;
                return (wrap.Error, wrap);
            }
        }

        public async Task<(bool? Error, GenericResponses<RoleDTO> Returns)> GetRoles(int userlevel)
        {
            var wrap = new GenericResponses<RoleDTO>
            {
                Error = false,
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
                wrap.Error = false;
                wrap.Message = "OK";
                wrap.Data = Data;

                return (wrap.Error, wrap);
            }
            catch (Exception ex)
            {
                wrap.Error = false;
                wrap.Message = ex.Message;
                return (wrap.Error, wrap);
            }
        }

    }
    }
