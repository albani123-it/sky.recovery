using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dapper;
using System.Linq;

namespace sky.recovery.Services.Repository
{
    public class UserRepository : IRepository
    {
        private IConfiguration _config { get; set; }
        public UserRepository(IConfiguration config)
        {
            _config = config;
        }
        public async Task<(bool status, string message, List<dynamic> Data)> GetDetailUser(string userid)
        {
            var SkyCore = _config["DbContextSettings:CustomPostgresql:ConnectionString_core"].ToString();

            try
            {

                var RawQuery = @"select*from ""public"".users where usr_userid=@id";
                var db = new Database(SkyCore);
                using var connection = db.CreateConnection();
                var users = await connection.QueryAsync<dynamic>(RawQuery, new { id = userid });
                return (true, "OK", users.ToList<dynamic>());

            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }


    }
}
