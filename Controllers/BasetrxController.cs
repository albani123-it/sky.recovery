using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sky.recovery.Libs;
using Newtonsoft.Json.Linq;
using Npgsql;
using System.Data;
using System.Dynamic;
using System.Data.SqlClient;

namespace sky.recovery.Controllers
{
    public class BasetrxController : Controller
    {

        private lDbConn dbconn = new lDbConn();
        private BaseController bc = new BaseController();
        private lGeneral lge = new lGeneral();
    
        private lConvert lc = new lConvert();
        public string execproses(string provider,string schema, string namefile)
        {
            string strout = "";
            var cstrname = dbconn.constringName("skylog");
            string namesp = schema + "." + namefile;

            if (provider == "postgresql")
            {
                JObject jo = new JObject();
                var conn = dbconn.constringList(provider, cstrname);
                NpgsqlTransaction trans;
                NpgsqlConnection connection = new NpgsqlConnection(conn);
                connection.Open();
                trans = connection.BeginTransaction();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand(namesp, connection, trans);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 1300;
                    cmd.ExecuteNonQuery();
                  
                    trans.Commit();
                    strout = "success";
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    strout = ex.Message;
                }
                connection.Close();
                NpgsqlConnection.ClearPool(connection);

            }
            else
            {
                JObject jo = new JObject();
                var conn = dbconn.constringList(provider, cstrname);
                
                SqlTransaction trans;
                SqlConnection connection = new SqlConnection(conn);
                connection.Open();
                trans = connection.BeginTransaction();
                try
                {
                    SqlCommand cmd = new SqlCommand(namesp, connection, trans);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 600;
                    cmd.ExecuteNonQuery();

                    trans.Commit();
                    strout = "success";
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    strout = ex.Message;
                }
                connection.Close();
                SqlConnection.ClearPool(connection);
            }
           
            return strout;
        }

     
    }
}
