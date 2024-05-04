using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Asn1.Ocsp;
using sky.recovery.DTOs.RequestDTO;
using sky.recovery.DTOs.ResponsesDTO;
using sky.recovery.Entities;
using sky.recovery.Helper.Config;
using sky.recovery.Insfrastructures;
using sky.recovery.Interfaces;
using sky.recovery.Libs;

using sky.recovery.Responses;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using sky.recovery.Services.DBConfig;
using sky.recovery.Helper.Enum;

namespace sky.recovery.Services
{
    public class RestrukturServices : PostgreSetting, IRestrukturServices
    {
        private IUserService _User { get; set; }
        private IRestruktureRepository _postgreRepository { get; set; }
        private IHelperRepository _helperRepository { get; set; }

        ModellingGeneralResponsesV2 _DataResponses = new ModellingGeneralResponsesV2();

        public RestrukturServices(IUserService User, IHelperRepository helperRepository, IRestruktureRepository postgreRepository,
        IOptions<DbContextSettings> appsetting) : base(appsetting)
        {
            _User = User;
            _postgreRepository = postgreRepository;
            _helperRepository = helperRepository;

        }

        //SERVICE YANG DIPAKAI
        //MONITORING RESTRUKTUR V2
        public async Task<(bool? Error, GeneralResponsesV2 Returns)> MonitoringRestrukturV2(string UserId)
        {
            var wrap = _DataResponses.Return();
            var SkyCollConsString = GetSkyCollConsString();

            try
            {

                var getCallBy = await _User.GetDataUser(UserId);
             
                if (getCallBy.Returns.Data.FirstOrDefault().role != RestrukturRole.Operator.ToString())
                {
                    wrap.Error = true;
                    wrap.Message = "Not Authorize";
                    return (wrap.Error, wrap);
                }
                var ReturnData = await _postgreRepository.GetRestukture(1, "\""+RecoverySchema.RecoveryBusinessV2.ToString()+"\"."+RecoveryFunctionName.getrestrukture.ToString() + "", "", getCallBy.Returns.Data.FirstOrDefault().iduser.ToString());
                wrap.Error = false;
                wrap.Message = "OK";
                wrap.Data = ReturnData;
                return (wrap.Error, wrap);

            }
            catch (Exception ex)
            {
                wrap.Error = true;
                wrap.Message = ex.Message;

                return (wrap.Error, wrap);
            }
        }


        //SERVICE YANG DIPAKAI
        //TASKLIST RESTRUKTUR V2
        public async Task<(bool? Error, GeneralResponsesV2 Returns)> TaskListRestrukturV2(string UserId)
        {
            var wrap = _DataResponses.Return();

            try
            {

                var getCallBy = await _User.GetDataUser(UserId);
                if (getCallBy.Returns.Data.FirstOrDefault().role != RestrukturRole.Supervisor.ToString())
                {
                    wrap.Error = true;
                    wrap.Message = "Not Authorize";
                    return (wrap.Error, wrap);
                }
                var ReturnData = await _postgreRepository.GetRestukture(2, "\"" + RecoverySchema.RecoveryBusinessV2.ToString() + "\"." + RecoveryFunctionName.tasklistrestrukture.ToString() + "", getCallBy.Returns.Data.FirstOrDefault().role, UserId);
                wrap.Error = false;
                wrap.Message = "OK";
                wrap.Data = ReturnData;
                return (wrap.Error, wrap);

            }
            catch (Exception ex)
            {
                wrap.Error = true;
                wrap.Message = ex.Message;

                return (wrap.Error, wrap);
            }
        }

        //SERVICE YANG DIPAKAI
        //GET MASTER LOAN FOR RESTRUKTUR V2
        public async Task<(bool? Error, GeneralResponsesV2 Returns)> GetMasterLoanV2()
        {
            var wrap = _DataResponses.Return();

            try
            {

               // var getCallBy = await _User.GetDataUser(UserId);
               
                var ReturnData = await _postgreRepository.GetMasterLoan( "\"" + RecoverySchema.RecoveryBusinessV2.ToString() + "\"." + RecoveryFunctionName.getloanmaster.ToString() + "");
                wrap.Error = false;
                wrap.Message = "OK";
                wrap.Data = ReturnData;
                return (wrap.Error, wrap);

            }
            catch (Exception ex)
            {
                wrap.Error = true;
                wrap.Message = ex.Message;

                return (wrap.Error, wrap);
            }
        }

    }
}
