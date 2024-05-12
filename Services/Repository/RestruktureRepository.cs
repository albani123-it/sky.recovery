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
using sky.recovery.DTOs.RequestDTO;
using Microsoft.EntityFrameworkCore.Query.Internal;

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
          

            using (NpgsqlConnection connection = new NpgsqlConnection(consstring))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(spname, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@filterstatus", StatusWorkflow.REQUESTED.ToString());

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


        public async Task<List<dynamic>> SearchingMonitoringRestrukture(string spname, int? Userid, SearchingRestrukturDTO Entity)
        {

            var SkyCollConsString = GetSkyCollConsString();

            using (NpgsqlConnection connection = new NpgsqlConnection(SkyCollConsString.Data.ConnectionSetting))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(spname, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@namauser", string.IsNullOrEmpty(Entity.Nama) ? DBNull.Value : (object)Entity.Nama);
                    command.Parameters.AddWithValue("@accnumber", string.IsNullOrEmpty(Entity.AccNo) ? DBNull.Value : (object)Entity.AccNo);
                    command.Parameters.AddWithValue("@createdbyuser", Userid);

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



        public async Task<List<dynamic>> CreateDraftRestrukture(string spname, int? Userid, int RoleId, AddRestructureDTO Entity)
        {

            var SkyCollConsString = GetSkyCollConsString();

            using (NpgsqlConnection connection = new NpgsqlConnection(SkyCollConsString.Data.ConnectionSetting))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(spname, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@loanid",Entity.LoanId);
                    command.Parameters.AddWithValue("@userid", Userid);
                    command.Parameters.AddWithValue("@requesterrole", RoleId);

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


        public async Task<List<dynamic>> RemovePermasalahan(string spname, RemovePermasalahanDTO Entity)
        {

            var SkyCollConsString = GetSkyCollConsString();

            using (NpgsqlConnection connection = new NpgsqlConnection(SkyCollConsString.Data.ConnectionSetting))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(spname, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@idpermasalahan", Entity.idpermasalahan);
                    command.Parameters.AddWithValue("@idrestrukture", Entity.idrestrukture);


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

        public async Task<List<dynamic>> CreatePermasalahan(string spname,int iduser, CreatePermasalahanDTO Entity)
        {

            var SkyCollConsString = GetSkyCollConsString();

            using (NpgsqlConnection connection = new NpgsqlConnection(SkyCollConsString.Data.ConnectionSetting))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(spname, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@idrestrukture", Entity.idrestrukture);
                    command.Parameters.AddWithValue("@userid", iduser);
                    command.Parameters.AddWithValue("@deskripsi", Entity.deskripsi);

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

        public async Task<List<dynamic>> GetDocRestrukture(string spname, GetDocumentRestruktureDTO Entity)
        {

            var SkyCollConsString = GetSkyCollConsString();

            using (NpgsqlConnection connection = new NpgsqlConnection(SkyCollConsString.Data.ConnectionSetting))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(spname, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@idloan", Entity.IdLoan);
                    command.Parameters.AddWithValue("@idrestrukture", Entity.RestruktureId);


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


        public async Task<List<dynamic>> CheckingDocRestrukture(string spname, int? LoanId, int? RestruktureId, int? DocTypeId)
        {

            var SkyCollConsString = GetSkyCollConsString();

            using (NpgsqlConnection connection = new NpgsqlConnection(SkyCollConsString.Data.ConnectionSetting))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(spname, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@idloan", LoanId);
                    command.Parameters.AddWithValue("@idrestrukture", RestruktureId);
                    command.Parameters.AddWithValue("@doctypeid", DocTypeId);


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


        public async Task<List<dynamic>> InsertDocRestrukture(string spname, 
            int? LoanId, 
            int? RestruktureId,
            int? DocTypeId,
            string jenisdocdesc,
            string urlpath,
            string urlname,
            int? userid
            )
        {

            var SkyCollConsString = GetSkyCollConsString();

            using (NpgsqlConnection connection = new NpgsqlConnection(SkyCollConsString.Data.ConnectionSetting))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(spname, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@idloan", LoanId);
                    command.Parameters.AddWithValue("@idrestrukture", RestruktureId);
                    command.Parameters.AddWithValue("@jenisdoc_id", DocTypeId);
                    command.Parameters.AddWithValue("@jenisdoc_desc", jenisdocdesc);
                    command.Parameters.AddWithValue("@urlpath", urlpath);
                    command.Parameters.AddWithValue("@urlname", urlname);
                    command.Parameters.AddWithValue("@uploaded_by", userid);

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



        public async Task<List<dynamic>> UpdateDocRestruktur(string spname,
            int? LoanId,
            int? RestruktureId,
            int? DocTypeId,
            string jenisdocdesc,
            string urlpath,
            string urlname,
            int? userid
            )
        {

            var SkyCollConsString = GetSkyCollConsString();

            using (NpgsqlConnection connection = new NpgsqlConnection(SkyCollConsString.Data.ConnectionSetting))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(spname, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@idloan", LoanId);
                    command.Parameters.AddWithValue("@idrestrukture", RestruktureId);
                    command.Parameters.AddWithValue("@jenisdoc_id", DocTypeId);
                    command.Parameters.AddWithValue("@jenisdoc_desc", jenisdocdesc);
                    command.Parameters.AddWithValue("@urlpath", urlpath);
                    command.Parameters.AddWithValue("@urlname", urlname);
                    command.Parameters.AddWithValue("@uploaded_by", userid);

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


        public async Task<List<dynamic>> RemoveDraftRestrukture(string spname, int? userid, int? idloan, int? idrestrukture)
        {

            var SkyCollConsString = GetSkyCollConsString();

            using (NpgsqlConnection connection = new NpgsqlConnection(SkyCollConsString.Data.ConnectionSetting))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(spname, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@createdbyuser", userid);
                    command.Parameters.AddWithValue("@idrestrukture", idrestrukture);
                    command.Parameters.AddWithValue("@idloan", idloan);


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


        public async Task<List<dynamic>> GetMasterDocRule(string spname,string param)
        {

            var SkyCollConsString = GetSkyCollConsString();

            using (NpgsqlConnection connection = new NpgsqlConnection(SkyCollConsString.Data.ConnectionSetting))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(spname, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@filters", param);
                   

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

        public async Task<List<dynamic>> UpdatePermasalahan(string spname,int iduser, UpdatePermasalahanDTO Entity)
        {

            var SkyCollConsString = GetSkyCollConsString();

            using (NpgsqlConnection connection = new NpgsqlConnection(SkyCollConsString.Data.ConnectionSetting))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(spname, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@idpermasalahan", Entity.idpermasalahan);
                    command.Parameters.AddWithValue("@idrestrukture", Entity.idrestrukture);
                    command.Parameters.AddWithValue("@userid", iduser);
                    command.Parameters.AddWithValue("@deskripsi", Entity.deskripsi);

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


        public async Task<List<dynamic>> GetPermasalahanRestrukture(string spname, int? loanid)
        {

            var SkyCollConsString = GetSkyCollConsString();

            using (NpgsqlConnection connection = new NpgsqlConnection(SkyCollConsString.Data.ConnectionSetting))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(spname, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@idloan", loanid);
                  

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


        public async Task<List<dynamic>> GetMasterColateral(string spname, int? loanid)
        {

            var SkyCollConsString = GetSkyCollConsString();

            using (NpgsqlConnection connection = new NpgsqlConnection(SkyCollConsString.Data.ConnectionSetting))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(spname, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@idloan", loanid);


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
        public async Task<List<dynamic>> GetDetailDrafting(string consstring, string spname, int? LoanId)
        {
          
            using (NpgsqlConnection connection = new NpgsqlConnection(consstring))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(spname, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@idloan", LoanId);

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


        public async Task<List<dynamic>> GetListFasilitas(string consstring, string spname, int? LoanId)
        {

            using (NpgsqlConnection connection = new NpgsqlConnection(consstring))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(spname, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@idloan", LoanId);

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