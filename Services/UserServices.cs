using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using sky.recovery.Helper.Config;
using sky.recovery.Insfrastructures;
using sky.recovery.Interfaces;
using sky.recovery.Responses;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace sky.recovery.Services
{
    public class UserServices : SkyCollConfig, IUserService
    {
        public UserServices(IOptions<DbContextSettings> appsetting) : base(appsetting)
        {


        }
        public async Task<(bool Error, GeneralResponses Returns)> GetDataUser(string userid)
        {
            try
            {
                var Data = await users.Where(es => es.usr_userid == userid).Select(es=>es.usr_id).FirstOrDefaultAsync();
                var Result = new GeneralResponses()
                {
                    Error=false,
                    Message="ok",
                    DataEntities=new ContentResponsesEntity()
                    {
                        userId=Data
                    }
                };
                return (Result.Error, Result);
            }
            catch (Exception ex)
            {
                var Result = new GeneralResponses()
                {
                    Error=true,
                    Message=ex.Message
                };
                return (Result.Error, Result);
            }
        }
    }
    }
