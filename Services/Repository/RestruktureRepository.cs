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



        public async Task<List<dynamic>> GetRestukture(int Type, string SPName, int?roleid, int?user)
        {
            var SkyCollConsString = GetSkyCollConsString();
            if (Type == 1)
            {
                return await GetMonitoring(SkyCollConsString.Data.ConnectionSetting, SPName, user);
            }
            else
            {
                return await GetTaskList(SkyCollConsString.Data.ConnectionSetting, SPName, roleid, user);

            }


        }


        public async Task<List<dynamic>> GetTaskList(string consstring, string spname, int? roleid, int? user)
        {
          

            using (NpgsqlConnection connection = new NpgsqlConnection(consstring))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(spname, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@roleid", roleid);
                    command.Parameters.AddWithValue("@users", user);

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

        public async Task<List<dynamic>> ActionApproval(string spname,int? userid, ApprovalActionDTO Entity)
        {

            var SkyCollConsString = GetSkyCollConsString();

            using (NpgsqlConnection connection = new NpgsqlConnection(SkyCollConsString.Data.ConnectionSetting))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(spname, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@approverid", userid);
                    command.Parameters.AddWithValue("@idrestrukture", Entity.idrestrukture);
                    command.Parameters.AddWithValue("@actionreason", Entity.actionreason);
                    command.Parameters.AddWithValue("@workflowid", Entity.workflowid);
                    command.Parameters.AddWithValue("@actions", Entity.actions);
                    command.Parameters.AddWithValue("@idfitur", 9);

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


        public async Task<List<dynamic>> GetWorkflowHistory(string spname, int? idrequest)
        {

            var SkyCollConsString = GetSkyCollConsString();

            using (NpgsqlConnection connection = new NpgsqlConnection(SkyCollConsString.Data.ConnectionSetting))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(spname, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@idrequest", idrequest);
                  

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

        public async Task<List<dynamic>> CheckingRestruktureExisting(string spname, int? idrestrukture, int? idloan)
        {

            var SkyCollConsString = GetSkyCollConsString();

            using (NpgsqlConnection connection = new NpgsqlConnection(SkyCollConsString.Data.ConnectionSetting))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(spname, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
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


        public async Task<List<dynamic>> UpdateConfigPolaRestrukture(string spname, int? userid, AddPolaDTO Entity)
        {

            var SkyCollConsString = GetSkyCollConsString();

            using (NpgsqlConnection connection = new NpgsqlConnection(SkyCollConsString.Data.ConnectionSetting))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(spname, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@idrestrukture", Entity.idrestrukture);
                    command.Parameters.AddWithValue("@idloan", Entity.idloan);
                    command.Parameters.AddWithValue("@branchid", Entity.branchid);
                    command.Parameters.AddWithValue("@pola_restrukture", Entity.PolaId);

                    command.Parameters.AddWithValue("@keterangans", Entity.keterangan);
                    command.Parameters.AddWithValue("@pengurangan_nilai_margin", Entity.pengurangannilaimargin);
                    command.Parameters.AddWithValue("@jenis_pengurangan", Entity.jenispengurangan);
                    command.Parameters.AddWithValue("@grace_periode", Entity.graceperiode);
                   // command.Parameters.AddWithValue("@tgl_jatuhtempo_baru", Entity.tgljatuhtempobaru);

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


        public async Task<List<dynamic>> CheckingForSubmitRestrukture(string spname, int? userid, int? idrestrukture, int? idloan)
        {

            var SkyCollConsString = GetSkyCollConsString();

            using (NpgsqlConnection connection = new NpgsqlConnection(SkyCollConsString.Data.ConnectionSetting))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(spname, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@idrestrukture", idrestrukture);
                    command.Parameters.AddWithValue("@idloan", idloan);
                    command.Parameters.AddWithValue("@userid", userid);
                   
                    // command.Parameters.AddWithValue("@tgl_jatuhtempo_baru", Entity.tgljatuhtempobaru);

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


        public async Task<List<dynamic>> SubmitRestrukturApproval(string spname, int? userid, int? approverid, int? idrestrukture)
        {

            var SkyCollConsString = GetSkyCollConsString();

            using (NpgsqlConnection connection = new NpgsqlConnection(SkyCollConsString.Data.ConnectionSetting))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(spname, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@idrestrukture", idrestrukture);
                    command.Parameters.AddWithValue("@idfitur", 9);

                    command.Parameters.AddWithValue("@requestorid", userid);
                    command.Parameters.AddWithValue("@approverid", approverid);

                    // command.Parameters.AddWithValue("@tgl_jatuhtempo_baru", Entity.tgljatuhtempobaru);

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


        public async Task<List<dynamic>> GetDetailPolaRestruktur(string spname,int? idrestrukture, int? idloan, string accno)
        {

            var SkyCollConsString = GetSkyCollConsString();

            using (NpgsqlConnection connection = new NpgsqlConnection(SkyCollConsString.Data.ConnectionSetting))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(spname, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@idrestrukture", idrestrukture);
                    command.Parameters.AddWithValue("@idloan", idloan);
                    command.Parameters.AddWithValue("@accno", accno);

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

        public async Task<List<dynamic>> GetBranchList(string spname)
        {

            var Connstring = GetSkyCoreConsString();
            using (NpgsqlConnection connection = new NpgsqlConnection(Connstring.Data.ConnectionSetting))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(spname, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                   // command.Parameters.AddWithValue("@filterstatus", StatusWorkflow.REQUESTED.ToString());

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


        public async Task<List<dynamic>> CreateAnalisaRestrukture(string spname, int? userid, ConfigAnalisaRestruktureDTO Entity)
        {

            var SkyCollConsString = GetSkyCollConsString();

            using (NpgsqlConnection connection = new NpgsqlConnection(SkyCollConsString.Data.ConnectionSetting))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(spname, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@userid", userid);
                    command.Parameters.AddWithValue("@idrestrukture", Entity.idrestrukture);
                    command.Parameters.AddWithValue("@nasabahpenghasilan", Entity.nasabahpenghasilan);
                    command.Parameters.AddWithValue("@pasanganpenghasilan", Entity.pasanganpenghasilan);
                    command.Parameters.AddWithValue("@lainnyapenghasilan", Entity.lainnyapenghasilan);
                    command.Parameters.AddWithValue("@penghasilantotal", Entity.penghasilantotal);
                    command.Parameters.AddWithValue("@pendidikanbiaya", Entity.pendidikanbiaya);
                    command.Parameters.AddWithValue("@listrikairteleponbiaya", Entity.listrikairteleponbiaya);
                    command.Parameters.AddWithValue("@belanjarumahtanggabiaya", Entity.belanjarumahtanggabiaya);
                    command.Parameters.AddWithValue("@transportasibiaya", Entity.transportasibiaya);
                    command.Parameters.AddWithValue("@lainnyabiaya", Entity.lainnyabiaya);
                    command.Parameters.AddWithValue("@totalpengeluaran", Entity.totalpengeluaran);
                    command.Parameters.AddWithValue("@bankhutang", Entity.bankhutang);
                    command.Parameters.AddWithValue("@kewajibantotal", Entity.kewajibantotal);
                    command.Parameters.AddWithValue("@lainnyacicilan", Entity.lainnyacicilan);
                    command.Parameters.AddWithValue("@bersihpenghasilan", Entity.bersihpenghasilan);
                    command.Parameters.AddWithValue("@rpc", Entity.rpc);
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


        public async Task<List<dynamic>> CheckingAnalisaRestruktureExisting(string spname, int? userid,int? analisaid, int? idrestrukture, int? idloan)
        {

            var SkyCollConsString = GetSkyCollConsString();

            using (NpgsqlConnection connection = new NpgsqlConnection(SkyCollConsString.Data.ConnectionSetting))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(spname, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@userid", userid);
                    command.Parameters.AddWithValue("@idrestrukture", idrestrukture);
                    command.Parameters.AddWithValue("@analisaid", analisaid);
                   
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
        public async Task<List<dynamic>> UpdateAnalisaRestrukture(string spname, int? userid, ConfigAnalisaRestruktureDTO Entity)
        {

            var SkyCollConsString = GetSkyCollConsString();

            using (NpgsqlConnection connection = new NpgsqlConnection(SkyCollConsString.Data.ConnectionSetting))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(spname, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@analisaid", Entity.analisaid);
                    command.Parameters.AddWithValue("@userid", userid);
                    command.Parameters.AddWithValue("@idrestrukture", Entity.idrestrukture);
                    command.Parameters.AddWithValue("@nasabahpenghasilan", Entity.nasabahpenghasilan);
                    command.Parameters.AddWithValue("@pasanganpenghasilan", Entity.pasanganpenghasilan);
                    command.Parameters.AddWithValue("@lainnyapenghasilan", Entity.lainnyapenghasilan);
                    command.Parameters.AddWithValue("@penghasilantotal", Entity.penghasilantotal);
                    command.Parameters.AddWithValue("@pendidikanbiaya", Entity.pendidikanbiaya);
                    command.Parameters.AddWithValue("@listrikairteleponbiaya", Entity.listrikairteleponbiaya);
                    command.Parameters.AddWithValue("@belanjarumahtanggabiaya", Entity.belanjarumahtanggabiaya);
                    command.Parameters.AddWithValue("@transportasibiaya", Entity.transportasibiaya);
                    command.Parameters.AddWithValue("@lainnyabiaya", Entity.lainnyabiaya);
                    command.Parameters.AddWithValue("@totalpengeluaran", Entity.totalpengeluaran);
                    command.Parameters.AddWithValue("@bankhutang", Entity.bankhutang);
                    command.Parameters.AddWithValue("@kewajibantotal", Entity.kewajibantotal);
                    command.Parameters.AddWithValue("@lainnyacicilan", Entity.lainnyacicilan);
                    command.Parameters.AddWithValue("@bersihpenghasilan", Entity.bersihpenghasilan);
                    command.Parameters.AddWithValue("@rpc", Entity.rpc);

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
        public async Task<List<dynamic>> GetMonitoring(string consstring, string spname, int? UserId)
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