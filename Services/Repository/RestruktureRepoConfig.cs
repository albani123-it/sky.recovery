using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using sky.recovery.Entities.RecoveryConfig;
using sky.recovery.Helper.Config;
using sky.recovery.Insfrastructures;
using sky.recovery.Interfaces;
using sky.recovery.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sky.recovery.Services.Repository
{
    public class RestruktureRepoConfig : SkyCoreConfig,IGeneralParam
    {
        public RestruktureRepoConfig(IOptions<DbContextSettings> appsetting) : base(appsetting)
        {
        }

        public class GeneralParamReturn
        {
            public bool Status { get; set; }
            public string Message { get; set; }
            public List<GeneralParamDetail> DataDetail { get; set; }
            public List<GeneralParamHeader> DataHeader { get; set; }

        }
        public class ReturnObjectConfig
        {
            public GeneralParamReturn ReturnObject(bool status,string message, List<GeneralParamDetail> ReturnData)
            {
                var DataObject = new GeneralParamReturn()
                {
                    Status = status,
                    Message = message,
                    DataDetail = ReturnData
                };
                return DataObject;
            }
        }
        public async Task<GeneralParamReturn> GetParamDetail(int IdParamHeader)
        {
            var Return = new ReturnObjectConfig();
            try
            {
                var Data = await generalparamdetail.Where(es => es.paramheaderid == IdParamHeader).ToListAsync();
                return Return.ReturnObject(true, "OK", Data);
            }
            catch(Exception ex)
            {
                return Return.ReturnObject(false, ex.Message, null);

            }
        }

        }
    }
