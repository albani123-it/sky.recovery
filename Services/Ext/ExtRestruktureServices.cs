using sky.recovery.Interfaces.Ext;
using sky.recovery.Insfrastructures.Scafolding;
using sky.recovery.Insfrastructures.Scafolding.SkyColl.Recovery;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using sky.recovery.Insfrastructures.Scafolding.SkyCore.Public;
using sky.recovery.Entities;
using System.Linq;
using Npgsql;
using sky.recovery.Helper.Query;
using Microsoft.EntityFrameworkCore;
namespace sky.recovery.Services.Ext
{
    public class ExtRestruktureServices: IExtRestruktureServices
    {
        sky.recovery.Insfrastructures.Scafolding.SkyColl.Recovery.skycollContext _RecoveryContext = new sky.recovery.Insfrastructures.Scafolding.SkyColl.Recovery.skycollContext();
      private IQuery _Query { get; set; }
        private  IConfiguration _configuration { get; set; }
        public ExtRestruktureServices(IConfiguration configuration,IQuery Query)
        {
            _configuration = configuration;
            _Query = Query;
        }

        public IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(_configuration["DbContextSettings:postgresql:ConnectionString_coll"]);
            }
        }
        public async Task<(bool Status,string Message, List<dynamic> Data)> GetMonitoringlist(string userid)
        {
            var ListData = new List<dynamic>();
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    var Data = await Task.Run(()=> dbConnection.Query<dynamic>(_Query.QueryMonitoringList()).ToList());

                    ListData.AddRange(Data);
                    return (true, "OK", ListData);
                }
            }
            catch(Exception ex)
            {
                return (false, ex.Message, null);
            }
        }
     

    }
}
