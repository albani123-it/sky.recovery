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
        public async Task<(bool? Status, GeneralResponsesV2 Returns)> MonitoringRestrukturV2(string UserId)
        {
            var wrap = _DataResponses.Return();
            var SkyCollConsString = GetSkyCollConsString();

            try
            {

                var getCallBy = await _User.GetDataUser(UserId);
             // pindah ke dinamis
                if (getCallBy.Returns.Data.FirstOrDefault().role != RestrukturRole.Operator.ToString())
                {
                     wrap.Status  = false;
                    wrap.Message = "Not Authorize";
                    return ( wrap.Status , wrap);
                }
                var ReturnData = await _postgreRepository.GetRestukture(1, "\""+RecoverySchema.RecoveryBusinessV2.ToString()+"\"."+RecoveryFunctionName.getrestrukture.ToString() + "", "", getCallBy.Returns.Data.FirstOrDefault().iduser.ToString());
                 wrap.Status  = true;
                wrap.Message = "OK";
              
                wrap.Data = ReturnData;
                return ( wrap.Status , wrap);

            }
            catch (Exception ex)
            {
                 wrap.Status  = false;
                wrap.Message = ex.Message;

                return ( wrap.Status , wrap);
            }
        }


        //SERVICE YANG DIPAKAI
        //TASKLIST RESTRUKTUR V2
        public async Task<(bool? Status, GeneralResponsesV2 Returns)> TaskListRestrukturV2(string UserId)
        {
            var wrap = _DataResponses.Return();

            try
            {

                var getCallBy = await _User.GetDataUser(UserId);
                // pindah ke dinamis
                if (getCallBy.Returns.Data.FirstOrDefault().role != RestrukturRole.Supervisor.ToString())
                {
                     wrap.Status  = false;
                    wrap.Message = "Not Authorize";
                    return ( wrap.Status , wrap);
                }
                var ReturnData = await _postgreRepository.GetRestukture(2, "\"" + RecoverySchema.RecoveryBusinessV2.ToString() + "\"." + RecoveryFunctionName.tasklistrestrukture.ToString() + "", getCallBy.Returns.Data.FirstOrDefault().role, UserId);
                 wrap.Status  = true;
                wrap.Message = "OK";
                wrap.Data = ReturnData;
                return ( wrap.Status , wrap);

            }
            catch (Exception ex)
            {
                 wrap.Status  = false;
                wrap.Message = ex.Message;

                return ( wrap.Status , wrap);
            }
        }


        //SERVICE YANG DIPAKAI
        //GET DETAIL DRAFTING RESTRUKTUR V2
        public async Task<(bool? Status, GeneralResponsesDetailRestrukturV2 Returns)> GetDetailDraftingRestruktur(int? loanid)
        {
            var wrap = _DataResponses.GeneralResponseDetailRestruktur();
            var SkyCollConsString = GetSkyCollConsString();

            try
            {

                //var getCallBy = await _User.GetDataUser(UserId);
                if (loanid==null)
                {
                     wrap.Status  = false;
                    wrap.Message = "Anda Harus Memilih Pinjaman yang akan direstrukturisasi";
                    return ( wrap.Status , wrap);
                }
                var ReturnDetail = await _postgreRepository.GetDetailDrafting(SkyCollConsString.Data.ConnectionSetting, "\"" + RecoverySchema.RecoveryBusinessV2.ToString() + "\"." + RecoveryFunctionName.getdetailfordraftingrestruktur.ToString() + "", loanid);
                
                
                var ReturnFasilitas = await _postgreRepository.GetListFasilitas(SkyCollConsString.Data.ConnectionSetting, "\"" + RecoverySchema.RecoveryBusinessV2.ToString() + "\"." + RecoveryFunctionName.getlistfasilitas.ToString() + "", loanid);

                 wrap.Status  = true;
                wrap.Message = "OK";
                var response = new DataDetailRestruktur
                {
                    DetailNasabah = ReturnDetail,
                    FasilitasLainnya = ReturnFasilitas,
                    
                };
                wrap.Data = response;
                //wrap.Data.FasilitasLainnya = response.FasilitasLainnya;

                return ( wrap.Status , wrap);

            }
            catch (Exception ex)
            {
                 wrap.Status  = false;
                wrap.Message = ex.Message;

                return ( wrap.Status , wrap);
            }
        }

        //SERVICE YANG DIPAKAI
        //GET MASTER LOAN FOR RESTRUKTUR V2
        public async Task<(bool? Status, GeneralResponsesV2 Returns)> GetMasterLoanV2()
        {
            var wrap = _DataResponses.Return();

            try
            {

               // var getCallBy = await _User.GetDataUser(UserId);
               
                var ReturnData = await _postgreRepository.GetMasterLoan( "\"" + RecoverySchema.RecoveryBusinessV2.ToString() + "\"." + RecoveryFunctionName.getloanmaster.ToString() + "");
                 wrap.Status  = true;
                wrap.Message = "OK";
               wrap.Data = ReturnData;
                
                return ( wrap.Status , wrap);

            }
            catch (Exception ex)
            {
                 wrap.Status  = false;
                wrap.Message = ex.Message;

                return ( wrap.Status , wrap);
            }
        }


        //SERVICE YANG DIPAKAI
        //SEARCHING MONITORING FOR RESTRUKTUR V2
        public async Task<(bool? Status, GeneralResponsesV2 Returns)> SearchingMonitoringRestruktur(SearchingRestrukturDTO Entity)
        {
            var wrap = _DataResponses.Return();

            try
            {

                // var getCallBy = await _User.GetDataUser(UserId);

                if(Entity==null)
                {

                    wrap.Status = false;
                    wrap.Message = "Request Not Valid";
                }

                if(Entity.UserId==null)
                {
                    wrap.Status = false;
                    wrap.Message = "User Not Authorize";

                }
                if (String.IsNullOrEmpty(Entity.Nama) && String.IsNullOrEmpty(Entity.AccNo))
                {
                    wrap.Status = false;
                    wrap.Message = "Anda Harus Mengisi Keyword Pencarian";

                }
                if (Entity.Nama=="" && Entity.AccNo=="")
                {
                    wrap.Status = false;
                    wrap.Message = "Anda Harus Mengisi Keyword Pencarian";

                }
                var getCallBy = await _User.GetDataUser(Entity.UserId);
                // pindah ke dinamis
                if (getCallBy.Returns.Data.FirstOrDefault().role != RestrukturRole.Operator.ToString())
                {
                    wrap.Status = false;
                    wrap.Message = "Not Authorize";
                    return (wrap.Status, wrap);
                }
                var ReturnData = await _postgreRepository.SearchingMonitoringRestrukture("\"" + RecoverySchema.RecoveryBusinessV2.ToString() + "\"." + RecoveryFunctionName.searchingrestruktur.ToString() + "",getCallBy.Returns.Data.FirstOrDefault().iduser,Entity);
                wrap.Status = true;
                wrap.Message = "OK";
                wrap.Data = ReturnData;

                return (wrap.Status, wrap);

            }
            catch (Exception ex)
            {
                wrap.Status = false;
                wrap.Message = ex.Message;

                return (wrap.Status, wrap);
            }
        }
    }
}
