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
using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using sky.recovery.Entities;
using sky.recovery.Helper.Enum;

namespace sky.recovery.Services.DBConfig
{
    public class RestruktureRepository : PostgreSetting, IRestruktureRepository
    {
        public BaseController bc = new BaseController();

        public RestruktureRepository(IOptions<DbContextSettings> appsetting) : base(appsetting)
        {
        }



        public async Task<List<dynamic>> GetRestukture(int Type, string SPName, string FilterStatus, string UserId)
        {
            var SkyCollConsString = GetSkyCollConsString();
            if (Type == 1)
            {
                return await GetMonitoring(SkyCollConsString.Data.ConnectionSetting, SPName, UserId);
            }
            else
            {
                return await GetTaskList(SkyCollConsString.Data.ConnectionSetting, SPName, FilterStatus, UserId);

            }


        }


        public async Task<List<dynamic>> GetTaskList(string consstring, string spname, string FilterStatus, string UserId)
        {
            string Status = "";
            if (FilterStatus == RestrukturRole.Operator.ToString())
            {
                Status = StatusWorkflow.DRAFT.ToString();
            }

            if (FilterStatus == RestrukturRole.Supervisor.ToString())
            {
                Status = StatusWorkflow.REQUESTED.ToString();
            }

            using (NpgsqlConnection connection = new NpgsqlConnection(consstring))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(spname, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@filterstatus", Status);

                    // Jika stored procedure memiliki parameter, tambahkan mereka di sini
                    // command.Parameters.AddWithValue("@ParameterName", value);

                    var data = new List<dynamic>();
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            dynamic result = new ExpandoObject();
                            var dict = (IDictionary<string, object>)result;

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                string columnName = reader.GetName(i);
                                object value = reader.GetValue(i);
                                dict[columnName] = value;
                            }
                            data.Add(result);
                        }
                    }
                    return data;
                }
            }

        }

        //SP PARAM ONLY USER ID
        public async Task<List<dynamic>> GetMonitoring(string consstring, string spname, string UserId)
        {
            var data = new List<dynamic>();
            using (NpgsqlConnection connection = new NpgsqlConnection(consstring))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(spname, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@createdbyuser", Convert.ToInt32(UserId));

                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {

                        while (await reader.ReadAsync())
                        {
                            dynamic result = new ExpandoObject();
                            var dict = (IDictionary<string, object>)result;

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                string columnName = reader.GetName(i);
                                object value = reader.GetValue(i);
                                dict[columnName] = value;
                            }
                            data.Add(result);

                        }
                    }


                }


            }
            return data;
        }



        //SP NON PARAM
        public async Task<List<dynamic>> GetMasterLoan(string spname)
        {
            var data = new List<dynamic>();
            var SkyCollConsString = GetSkyCollConsString();

            using (NpgsqlConnection connection = new NpgsqlConnection(SkyCollConsString.Data.ConnectionSetting))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(spname, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {

                        while (await reader.ReadAsync())
                        {
                            dynamic result = new ExpandoObject();
                            var dict = (IDictionary<string, object>)result;

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                string columnName = reader.GetName(i);
                                object value = reader.GetValue(i);
                                dict[columnName] = value;
                            }
                            data.Add(result);

                        }
                    }


                }


            }
            return data;
        }
    }
}