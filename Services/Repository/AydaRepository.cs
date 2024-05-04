using Newtonsoft.Json.Linq;
using sky.recovery.Controllers;
using sky.recovery.Helper.Config;
using sky.recovery.Libs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Xml.Linq;
using System.Text;
using Microsoft.Extensions.Options;
using sky.recovery.Insfrastructures;
using Npgsql;
using Microsoft.AspNetCore.Components;

namespace sky.recovery.Services.DBConfig
{
    public class AydaRepository :PostgreSetting, IAydaRepository
    {
        public BaseController bc = new BaseController();

        public AydaRepository(IOptions<DbContextSettings> appsetting) : base(appsetting)
        {
        }

       
       
        public List<dynamic> GetAyda( int Type,string SPName,string FilterStatus)
        {
            var SkyCollConsString = GetSkyCollConsString();
            if(Type==1)
            {
                return GetMonitoring(SkyCollConsString.Data.ConnectionSetting,SPName);
            }
            else
            {
                return GetTaskList(SkyCollConsString.Data.ConnectionSetting, SPName,FilterStatus);

            }


        }


        public List<dynamic> GetTaskList(string consstring, string spname,string FilterStatus)
        {
            string Status = "";
            if(FilterStatus.ToUpper()=="ADMIN")
            {
                Status = "DRAFT";
            }
            if(FilterStatus.ToUpper()=="ADMIN2")
            {
                Status = "PENGAJUAN";
            }
            if(FilterStatus.ToUpper()=="MANAJEMEN")
            {
                Status = "VERIFIKASI";
            }

            using (NpgsqlConnection connection = new NpgsqlConnection(consstring))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(spname, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@filterstatus", Status);

                    // Jika stored procedure memiliki parameter, tambahkan mereka di sini
                    // command.Parameters.AddWithValue("@ParameterName", value);

                    connection.Open();
                    var data = new List<dynamic>();
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Lakukan sesuatu dengan hasil yang diterima dari stored procedure
                            // Misalnya, ambil nilai dari kolom

                            var LoanNumber = reader["LoanNumber"].ToString();
                            var Nasabah = reader["Nasabah"].ToString();

                            data.Add(LoanNumber);
                            data.Add(Nasabah);

                            //  Console.WriteLine(columnValue);
                        }
                    }
                    return data;
                }
            }

        }

        public List<dynamic>GetMonitoring(string consstring,string spname)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(consstring))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(spname, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Jika stored procedure memiliki parameter, tambahkan mereka di sini
                    // command.Parameters.AddWithValue("@ParameterName", value);

                    connection.Open();
                    var data = new List<dynamic>();
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Lakukan sesuatu dengan hasil yang diterima dari stored procedure
                            // Misalnya, ambil nilai dari kolom

                            var LoanNumber = reader["LoanNumber"].ToString();
                            var Nasabah = reader["Nasabah"].ToString();

                            data.Add(LoanNumber);
                            data.Add(Nasabah);

                            //  Console.WriteLine(columnValue);
                        }
                    }
                    return data;
                }
            }

        }
    }
}

