
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
namespace sky.recovery.Services.Repository
{
    public class InsuranceRepositorycs :  IRecoveryRepository
    {
        private IConfiguration _config { get; set; }
        private IRepository _User { get; set; }
        
        public InsuranceRepositorycs(IConfiguration config, IRepository User)
        {
            _config = config;
            _User = User;
        }


        public async Task<(bool status, string message, List<dynamic?> Data)> GetMonitor(string userid, string services)
        {
            var SkyColl = _config["DbContextSettings:CustomPostgresql:ConnectionString_coll"].ToString();
            try
            {
                var GetDetailUser = await _User.GetDetailUser(userid);
                if (GetDetailUser.status == true)
                {
                    var db = new Database(SkyColl);
                    using var connection = db.CreateConnection();

                    var GetQuery = @"select Querys from ""RecoveryBusinessV2"".masterquery where ServicesName=@Service";
                    var GetQueryExec = await connection.QueryAsync<string>(GetQuery, new { Service = services });

                    var GetData = @GetQueryExec.FirstOrDefault();
                    var GetDataExec = await connection.QueryAsync<dynamic>(@GetData);


                    return (true, "OK", GetDataExec.ToList<dynamic?>());
                }
                else
                {
                    return (false, GetDetailUser.message, null);
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);

            }
        }

    }
}
