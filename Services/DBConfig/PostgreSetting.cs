using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using sky.recovery.Controllers;
using sky.recovery.Helper.Config;
using sky.recovery.Libs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sky.recovery.Services.DBConfig
{
    public class PostgreSetting
    {
        private lDbConn dbconn = new lDbConn();
        private DbContextSettings _appsetting { get; set; }

        public PostgreSetting(IOptions<DbContextSettings> appsetting)
        {
            _appsetting = appsetting.Value;
        }
        public SQLPostgre DataObjectSQLPostgre()
        {
            var wrap = new SQLPostgre()
            {
                Error = false,
                Message = "",
                Data = new SQLConstring()
                {
                    Connstring = "",
                    Provider = "",
                    ConnectionSetting=""
                }

            };
            return wrap;
        }




        public SQLPostgre GetSkyCollConsString()
        {
            var wrap = DataObjectSQLPostgre();
            try
            {
                var provider = dbconn.sqlprovider();
                var cstrname = dbconn.constringName("skycoll");
                var ConnectionSetting = _appsetting.postgresql.ConnectionString_coll;
                wrap.Error = false;
                wrap.Message = "OK";
                wrap.Data.Connstring = cstrname;
                wrap.Data.ConnectionSetting = ConnectionSetting;
                wrap.Data.Provider = provider;
                
                return  wrap;
            }
            catch(Exception ex)
            {
                wrap.Error = true;
                wrap.Message = ex.Message;
                return  wrap;
            }
        }


        public SQLPostgre GetSkyCoreConsString()
        {
            var wrap = DataObjectSQLPostgre();
            try
            {
                var provider = dbconn.sqlprovider();
                var cstrname = dbconn.constringName("skycore");
                var ConnectionSetting = _appsetting.postgresql.ConnectionString_core;

                wrap.Error = false;
                wrap.Message = "OK";
                wrap.Data.ConnectionSetting = ConnectionSetting;
                wrap.Data.Connstring = cstrname;
                wrap.Data.Provider = provider;
                return wrap;
            }
            catch (Exception ex)
            {
                wrap.Error = true;
                wrap.Message = ex.Message;
                return wrap;
            }
        }

    }
}
