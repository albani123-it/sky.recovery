using Npgsql;
using sky.recovery.Insfrastructures;
using sky.recovery.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Threading.Tasks;

namespace sky.recovery.Services.Repository
{
    public class HelperRepository : IHelperRepository
    {

        public async Task<List<GeneralParamHeader>> GetParamConfig(string consstring, string spname)
        {

            var data = new List<GeneralParamHeader>();

            using (NpgsqlConnection connection = new NpgsqlConnection(consstring))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(spname, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Jika stored procedure memiliki parameter, tambahkan mereka di sini
                    // command.Parameters.AddWithValue("@ParameterName", value);

                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var Datas = new GeneralParamHeader();
                            Datas.Id =Convert.ToInt32(reader["headerid"].ToString());
                            Datas.Title = reader["headertitle"].ToString();
                            Datas.Descriptions = reader["headerdescriptions"].ToString();
                            Datas.ParamHeaderId= Convert.ToInt32(reader["paramheaderid"].ToString());
                            Datas.DetailTitle= reader["detailtitle"].ToString();
                            Datas.DetailDescriptions= reader["detaildescriptions"].ToString();

                            data.Add(Datas);
                        }
                    }
                    return data;
                }
            }

        }

    }
}
