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
using System.Reflection.Metadata.Ecma335;
using System.IO;
using static System.Net.WebRequestMethods;
using System.Net.NetworkInformation;

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
        //REMOVE PERMASALAHAN RESTRUKTUR V2
        public async Task<(bool? Status, GeneralResponsesV2 Returns)> RemovePermasalahanRestrukture(RemovePermasalahanDTO Entity)
        {
            var wrap = _DataResponses.Return();

            try
            {

               // var getCallBy = await _User.GetDataUser(UserId);
                // pindah ke dinamis
                if(Entity.idpermasalahan==null)
                {
                    wrap.Status = false;
                    wrap.Message = "Anda Harus Memilih Permasalahan yang akan di Remove";
                    return (wrap.Status, wrap);
                }
                if (Entity.idrestrukture == null)
                {
                    wrap.Status = false;
                    wrap.Message = "Anda Harus Memilih Restrukture Permasalahan yang akan di Remove";
                    return (wrap.Status, wrap);
                }
                var ReturnData = await _postgreRepository.RemovePermasalahan("\"" + RecoverySchema.RecoveryBusinessV2.ToString() + "\"." + RecoveryFunctionName.removepermasalahanrestrukture.ToString() + "", Entity);
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


        //SERVICE YANG DIPAKAI
        //UploadDocRestrukture RESTRUKTUR V2
        public async Task<(bool? Status, GeneralResponsesDocRestrukturV2 Returns)> UploadDocRestrukture(UploadDocRestrukturDTO Entity)
        {
            var wrap = _DataResponses.GeneralResponseDocRestruktur();

            try
            {

                var getCallBy = await _User.GetDataUser(Entity.UserId);
                // pindah ke dinamis

                if(Entity==null)
                {
                    wrap.Status = false;
                    wrap.Message = "Request Not Valid";
                }
                if(Entity.IdLoan==null || Entity.IdRestrukture==null || Entity.IdDocType==null)
                {
                    wrap.Status = false;
                    wrap.Message = "Id Loan, Jenis Doc dan Id Restrukture Harus Diisi";
                }
                var path = "~~/wwwroot/Documents";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var ReturnCheckingDoc = await _postgreRepository.CheckingDocRestrukture("\"" + RecoverySchema.RecoveryBusinessV2.ToString() + "\"." + RecoveryFunctionName.checkingavailabledoc.ToString() + "", Entity.IdLoan,Entity.IdRestrukture,Entity.IdDocType);

                if(ReturnCheckingDoc.Count==0)
                {

                    string ext = Path.GetExtension(Entity.File.FileName);

                    var nm = path+ ext;

                    using (FileStream filestream = System.IO.File.Create(nm))
                    {
                        Entity.File.CopyTo(filestream);
                        filestream.Flush();
                        //  return "\\Upload\\" + objFile.files.FileName;
                    }

                    var ReturnInsertOoc = await _postgreRepository.InsertDocRestrukture("\"" + RecoverySchema.RecoveryBusinessV2.ToString() + "\"." + RecoveryFunctionName.insertdocrestrukture.ToString() + ""
                        , Entity.IdLoan, 
                        Entity.IdRestrukture, 
                        Entity.IdDocType,
                        Entity.jenisdocdesc
                        ,nm
                        ,Entity.File.FileName,
                        getCallBy.Returns.Data.FirstOrDefault().iduser
                        );

                    var ReturnDocStrukture = await _postgreRepository.GetMasterDocRule("\"" + RecoverySchema.RecoveryBusinessV2.ToString() + "\"." + RecoveryFunctionName.getmasterdocrules.ToString() + "", "Restrukture");


                    wrap.Status = true;
                    wrap.Message = "OK";
                    var Data = new DataDocRestrukture()
                    {
                        DocStrukturRule = ReturnDocStrukture,
                        DocRestruktur = ReturnInsertOoc
                    };
                    wrap.Data = Data;
                        
                }
                else
                {
                    string ext = Path.GetExtension(Entity.File.FileName);

                    var nm = path + ext;

                    using (FileStream filestream = System.IO.File.Create(nm))
                    {
                        Entity.File.CopyTo(filestream);
                        filestream.Flush();
                        //  return "\\Upload\\" + objFile.files.FileName;
                    }
                    var ReturnUpdateDoc= await _postgreRepository.UpdateDocRestruktur("\"" + RecoverySchema.RecoveryBusinessV2.ToString() + "\"." + RecoveryFunctionName.updateddocrestrukture.ToString() + "", 
                        Entity.IdLoan,
                        Entity.IdRestrukture,
                        Entity.IdDocType,
                        Entity.jenisdocdesc
                        , nm
                        , Entity.File.FileName,
                        getCallBy.Returns.Data.FirstOrDefault().iduser

                        );

                    var ReturnDocStrukture = await _postgreRepository.GetMasterDocRule("\"" + RecoverySchema.RecoveryBusinessV2.ToString() + "\"." + RecoveryFunctionName.getmasterdocrules.ToString() + "", "Restrukture");


                    wrap.Status = true;
                    wrap.Message = "OK";
                    var Data = new DataDocRestrukture()
                    {
                        DocStrukturRule = ReturnDocStrukture,
                        DocRestruktur = ReturnUpdateDoc
                    };
                    wrap.Data = Data;

                }

                return (wrap.Status, wrap);

            }
            catch (Exception ex)
            {
                wrap.Status = false;
                wrap.Message = ex.Message;

                return (wrap.Status, wrap);
            }
        }

        //SERVICE YANG DIPAKAI
        //GetMasterDocRuke RESTRUKTUR V2
        public async Task<(bool? Status, GeneralResponsesDocRestrukturV2 Returns)> GetMasterDocRule(GetDocumentRestruktureDTO Entity)
        {
            var wrap = _DataResponses.GeneralResponseDocRestruktur();

            try
            {

                // var getCallBy = await _User.GetDataUser(UserId);
                // pindah ke dinamis
               
                var ReturnDocStrukture = await _postgreRepository.GetMasterDocRule("\"" + RecoverySchema.RecoveryBusinessV2.ToString() + "\"." + RecoveryFunctionName.getmasterdocrules.ToString() + "", "Restrukture");
                var ReturnDoc = await _postgreRepository.GetDocRestrukture("\"" + RecoverySchema.RecoveryBusinessV2.ToString() + "\"." + RecoveryFunctionName.getdocrestruktrue.ToString() + "", Entity);

                wrap.Status = true;
                wrap.Message = "OK";
                var Data = new DataDocRestrukture()
                {
                    DocRestruktur=ReturnDoc,
                    DocStrukturRule=ReturnDocStrukture

                };
                wrap.Data = Data;
                return (wrap.Status, wrap);

            }
            catch (Exception ex)
            {
                wrap.Status = false;
                wrap.Message = ex.Message;

                return (wrap.Status, wrap);
            }
        }




        //SERVICE YANG DIPAKAI
        //REMOVE PERMASALAHAN RESTRUKTUR V2
        public async Task<(bool? Status, GeneralResponsesV2 Returns)> CreatePermasalahan(CreatePermasalahanDTO Entity)
        {
            var wrap = _DataResponses.Return();

            try
            {

                var getCallBy = await _User.GetDataUser(Entity.userid);
                // pindah ke dinamis
               
                if (Entity.idrestrukture == null)
                {
                    wrap.Status = false;
                    wrap.Message = "Anda Harus Memilih Restrukture Permasalahan yang akan di Tambahkan Permasalahan nya";
                    return (wrap.Status, wrap);
                }
                if(Entity.deskripsi==null || Entity.deskripsi=="")
                {
                    wrap.Status = false;
                    wrap.Message = "Deskripsi Permasalahan Harus Diisi";
                    return (wrap.Status, wrap);

                }
                var ReturnData = await _postgreRepository.CreatePermasalahan("\"" + RecoverySchema.RecoveryBusinessV2.ToString() + "\"." + RecoveryFunctionName.createpermasalahanrestrukture.ToString() + "",getCallBy.Returns.Data.FirstOrDefault().iduser, Entity);
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

        //SERVICE YANG DIPAKAI
        //UPDATE PERMASALAHAN RESTRUKTUR V2
        public async Task<(bool? Status, GeneralResponsesV2 Returns)> UpdatePermasalahan(UpdatePermasalahanDTO Entity)
        {
            var wrap = _DataResponses.Return();

            try
            {

                 var getCallBy = await _User.GetDataUser(Entity.userid);
                // pindah ke dinamis
                if (Entity.idpermasalahan == null)
                {
                    wrap.Status = false;
                    wrap.Message = "Anda Harus Memilih Permasalahan yang akan diubah";
                    return (wrap.Status, wrap);
                }
                if (Entity.idrestrukture == null)
                {
                    wrap.Status = false;
                    wrap.Message = "Anda Harus Memilih Restrukture Permasalahan yang akan di Ubah Permasalahan nya";
                    return (wrap.Status, wrap);
                }
                if (Entity.deskripsi == null || Entity.deskripsi == "")
                {
                    wrap.Status = false;
                    wrap.Message = "Deskripsi Permasalahan Harus Diisi";
                    return (wrap.Status, wrap);

                }
                var ReturnData = await _postgreRepository.UpdatePermasalahan("\"" + RecoverySchema.RecoveryBusinessV2.ToString() + "\"." + RecoveryFunctionName.updatepermasalahanrestrukture.ToString() + "",getCallBy.Returns.Data.FirstOrDefault().iduser, Entity);
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

                var GetPermasalahan = await _postgreRepository.GetPermasalahanRestrukture("\"" + RecoverySchema.RecoveryBusinessV2.ToString() + "\"." + RecoveryFunctionName.getpermasalahanrestrukture.ToString() + "", loanid);
                var GetCollateral = await _postgreRepository.GetMasterColateral("\"" + RecoverySchema.RecoveryBusinessV2.ToString() + "\"." + RecoveryFunctionName.getmastercollateral.ToString() + "", loanid);

                wrap.Status  = true;
                wrap.Message = "OK";
                List<dynamic> xs = null;
                List<dynamic> xst = null;

                var response = new DataDetailRestruktur
                {
                    DetailNasabah = ReturnDetail,
                    FasilitasLainnya = ReturnFasilitas,
                    DataAgunan=GetCollateral,
                    Permasalahan=GetPermasalahan
                    
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
        //CREATE DRAFT FOR RESTRUKTUR V2
        public async Task<(bool? Status, GeneralResponsesV2 Returns)> CreateDraftRestrukture(AddRestructureDTO Entity)
        {
            var wrap = _DataResponses.Return();

            try
            {

                if(Entity==null)
                {
                    wrap.Status = false;
                    wrap.Message = "Request Tidak Valid";
                  
                }
                if(Entity.LoanId==null)
                {
                    wrap.Status = false;
                    wrap.Message = "Loan Id Harus Diisi";
                   
                }

                 var getCallBy = await _User.GetDataUser(Entity.UserId);
                if(getCallBy.Returns.Data.FirstOrDefault().role!=RestrukturRole.Operator.ToString())
                {
                    wrap.Status = false;
                    wrap.Message = "Not Authorize";
                }
                var ReturnData = await _postgreRepository.CreateDraftRestrukture("\"" + RecoverySchema.RecoveryBusinessV2.ToString() + "\"." + RecoveryFunctionName.createdraftrestrukture.ToString() + "",getCallBy.Returns.Data.FirstOrDefault().iduser,getCallBy.Returns.Data.FirstOrDefault().RoleId,Entity);
                wrap.Status = true;
                wrap.Message = "OK";

                return (wrap.Status, wrap);

            }
            catch (Exception ex)
            {
                wrap.Status = false;
                wrap.Message = ex.Message;

                return (wrap.Status, wrap);
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
