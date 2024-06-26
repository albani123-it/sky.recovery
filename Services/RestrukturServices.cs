﻿using Microsoft.AspNetCore.Http;
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
using Microsoft.AspNetCore.Hosting;
using sky.recovery.DTOs.WorkflowDTO;
using sky.recovery.DTOs.ResponsesDTO.Restrukture;
using sky.recovery.Insfrastructures.Scafolding.SkyColl.Public;
using sky.recovery.Insfrastructures.Scafolding.SkyColl.Recovery;
using static Dapper.SqlMapper;
using sky.recovery.Insfrastructures.Scafolding.SkyCore.Public;
using sky.recovery.DTOs.RequestDTO.CommonDTO;
using Microsoft.Extensions.Configuration;
using sky.recovery.DTOs.RequestDTO.Restrukture;

namespace sky.recovery.Services
{
    public class RestrukturServices : PostgreSetting, IRestrukturServices
    {
        sky.recovery.Insfrastructures.Scafolding.SkyColl.Recovery.SkyCollRecoveryDBContext _recoveryContext = new Insfrastructures.Scafolding.SkyColl.Recovery.SkyCollRecoveryDBContext();

        sky.recovery.Insfrastructures.Scafolding.SkyColl.Public.skycollContext _collContext = new Insfrastructures.Scafolding.SkyColl.Public.skycollContext();

        skycoreContext _skyCoreContext = new skycoreContext();
        private IUserService _User { get; set; }
        private IGeneralParam _GeneralParam { get; set; }
        private readonly IWebHostEnvironment _environment;
        private IRestruktureRepository _postgreRepository { get; set; }
        private IHelperRepository _helperRepository { get; set; }
        private IWorkflowServices _workflowServices { get; set; }

        ModellingGeneralResponsesV2 _DataResponses = new ModellingGeneralResponsesV2();

        private IConfiguration _config { get; set; }
        public RestrukturServices (IConfiguration config, IWorkflowServices workflowServices, IGeneralParam GeneralParam, IWebHostEnvironment environment, IUserService User, IHelperRepository helperRepository, IRestruktureRepository postgreRepository,
        IOptions<DbContextSettings> appsetting) : base(appsetting)
        {
            _config = config;
            _workflowServices = workflowServices;
            _environment = environment;
            _GeneralParam = GeneralParam;
            _User = User;
            _postgreRepository = postgreRepository;
            _helperRepository = helperRepository;

        }

        public async Task<(bool Status,string message)> RemoveDocRestrukture(int id)
        {
            try
            {
                var Data = await _recoveryContext.Restrukturedokumen.Where(es => es.Id == id).FirstOrDefaultAsync();
                Data.Isdeleted = 1;
                _recoveryContext.Entry(Data).State = EntityState.Modified;
                await _recoveryContext.SaveChangesAsync();

                return (false, "OK");
            }
            catch(Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool Status, string Message, List<dynamic> Data)> GetAnalisaRestrukture(int RestruktureId)
        {
            try
            {
                var Data = await _recoveryContext.Restructurecashflow.Where(es => es.Restruktureid == RestruktureId)
                    .ToListAsync<dynamic>();
                return (true, "OK", Data);
            }
            catch(Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

      
        public async Task<(bool Status,string Message, List<dynamic>Data)> GetFiturId()
        {
            try
            {
                var Data = await _recoveryContext.Generalparamdetail
                    .Where(ES=>ES.Descriptions=="Fitur").ToListAsync<dynamic>();
                return (true, "OK", Data);
            }
            catch(Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

        public async Task<(bool Status,string Message, Dictionary<string, List<dynamic>> DataNasabah)>GetDetailRestruktureForApproval(int restruktureid, int loanid, int CustomerId)
        {
            try
            {

  
                var Data = from mc in _collContext.MasterCustomers
                           join ml in _collContext.MasterLoans on mc.Id equals ml.CustomerId
                           join pd in _collContext.Rfproducts on ml.Product equals pd.PrdId
                           join ps in _collContext.RfproductSegments on ml.PrdSegmentId equals ps.PrdSgmId
                           where ml.Id == loanid 
                           select new
                           {
                               Nama = mc.CuName,
                               NoKTP = mc.CuIdnumber,
                               alamat = mc.CuAddress,
                               nohp = mc.CuHmphone,
                               pekerjaan = mc.Pekerjaan,
                               tanggallahir = mc.CuBorndate,
                               TglCore = mc.StgDate,
                               DataLoan = new
                               {
                                   SegmentId = ml.PrdSegmentId,
                                   Segment = ps.PrdSgmDesc,
                                   ProductId = ml.Product,
                                   Product = pd.PrdDesc,
                                   JumlahAngsuran = ml.Installment,
                                   TanggalMulai = ml.StartDate,
                                   TanggalJatuhTempo = ml.MaturityDate,
                                   Tenor = ml.Tenor,
                                   Plafond = ml.Plafond,
                                   OutStanding = ml.Outstanding,
                                   Kolektabilitas = ml.Kolektibilitas,
                                   DPD = ml.Dpd,
                                   TglBayarTerakhir = ml.LastPayDate,
                                   TunggakanPokok = ml.TunggakanPokok,
                                   TunggakanBunga = ml.TunggakanBunga,
                                   TunggakanDenda = ml.TunggakanDenda,
                                   TotalTunggakan = ml.TunggakanTotal,
                                   TotalKewajiban = ml.KewajibanTotal
                               }
                           };
                var DataNasabah = await Data.ToListAsync<dynamic>();

                var DatasFasilitas = from mc in _collContext.MasterCustomers
                                     join ml in _collContext.MasterLoans on mc.Id equals ml.CustomerId
                                     join pd in _collContext.Rfproducts on ml.Product equals pd.PrdId
                                     join ps in _collContext.RfproductSegments on ml.PrdSegmentId equals ps.PrdSgmId
                                     where mc.Id==CustomerId
                                     select new
                                     {
                                         CuCif = ml.CuCif,
                                         AccNo = ml.AccNo,
                                         productid = ml.Product,
                                         Product = pd.PrdDesc,
                                         segmentid = ml.PrdSegmentId,
                                         Segment = ps.PrdSgmDesc,
                                         JumlahAngsuran = ml.Installment,
                                         TanggalMulai = ml.StartDate,
                                         TanggalJatuhTempo = ml.MaturityDate,
                                         Tenor = ml.Tenor,
                                         JumlahPinjaman = ml.KewajibanTotal,
                                         Outstanding = ml.Outstanding
                                     };


               


                var DataPermasalahan = await _recoveryContext.Permasalahanrestrukture
              
                    .Where(es => es.Restruktureid == restruktureid).Select(es => new 
                      {
                    Descriptions = es.Descriptions
                }).ToListAsync<dynamic>();

                var Dokumen = await _recoveryContext.Restrukturedokumen.Where(es => es.Restruktureid == restruktureid)
                   .Select(es=> new
                   {
                       JenisDokumen = es.Doctypedesc,
                       Url = es.Filepath,
                       FileName = es.Fileurl,
                       UploadDated = es.Uploaddated,
                       Keterangan =es.Keterangan,
                       Status = _recoveryContext.Generalparamdetail.Where(x=>x.Id==es.Status).Select(es=>es.Title).FirstOrDefault()
                   }
                   
                   )
                    
                    .ToListAsync<dynamic>();

                var DataRestruk = from rs in _recoveryContext.Restrukture
                                  join ra in _recoveryContext.Restructurecashflow on rs.Id equals ra.Restruktureid
                                 join gd in _recoveryContext.Generalparamdetail on rs.Polarestrukturid equals gd.Id
                                  join gde in _recoveryContext.Generalparamdetail on rs.Jenispenguranganid equals gde.Id

                                  where rs.Id == restruktureid
                                  select new
                                  {
                                      Analisa = new
                                      {
                                          Id=ra.Id,
                                          PenghasilanNasabah=ra.Penghasilanbersih,
                                          PenghasilanPasangan=ra.Penghasilanpasangan,
                                          PenghasilanLainnya=ra.Penghasilanlainnya,
                                          TotalPenghasilan=ra.Totalpenghasilan,
                                          BiayaPendidikan=ra.Biayapendidikan,
                                          BiayaListrikAirTelp = ra.Biayalistrkairtelp,
                                          BiayaBelanja=ra.Biayabelanja,
                                          BiayaTransportasi=ra.Biayatransportasi,
                                          BiayaLainnya = ra.Biayalainnya,
                                          PengeluaranTotal=ra.Pengeluarantotal,
                                          HutangdiBank = ra.Hutangdibank,
                                          CicilanLainnya = ra.Cicilanlainnya,
                                          TotalKewajiban = ra.Totalkewajiban,
                                          PenghasilanBersih = ra.Penghasilanbersih,
                                          RPC70Persen = ra.Rpc70persen

                                      },
                                      PolaRestruk = new
                                      {
                                          keterangan =rs.Keterangan,
                                          poladesc = gd.Title,
                                          pengurangannilaimargin = rs.Pengurangannilaimargin,
                                          jenispengurangan = rs.Jenispenguranganid,
                                          jenispengurangandesc = gde.Title,
                                          graceperiod = rs.Graceperiode,
                                          polaid = rs.Polarestrukturid
                                      }
                                  };
                var DataRestrukList = await DataRestruk.ToListAsync<dynamic>();

             




                var Collection = new Dictionary<string, List<dynamic>>();

                Collection["nasabah"] =  DataNasabah;
                //Collection["DataLoan"] = DataLoan;
                Collection["DataFasilitas"] = await DatasFasilitas.ToListAsync<dynamic>();
                Collection["Permasalahan"] = DataPermasalahan;
                Collection["Dokumen"] = Dokumen;
                //Collection["Analisa"] = Analisa;
                //Collection["PolaRestruk"] = PolaRestruk;
                Collection["DataRestruk"] = DataRestrukList;

                return (true, "OK", Collection);

            }


            catch(Exception ex)
            { 
                return (false, ex.Message,null);
            }
        }

        //mengcancel yang statusnya draft
        public async Task<(bool status, string message)> CancelPengajuanRestrukture(string userid, long id, long loanid)
        {
            try
            {
                var getCallBy = await _User.GetDataUser(userid);

                var GetData = await _recoveryContext.Restrukture.Where(es => es.Id == id && es.Loanid == loanid).FirstOrDefaultAsync();
                GetData.Isactive = 0;
                GetData.Iscancel = 1;
                GetData.Lastupdatedate = DateTime.Now;
                GetData.Lastupdatedid = getCallBy.Returns.Data.FirstOrDefault().iduser;
                _recoveryContext.Entry(GetData).State = EntityState.Modified;
                await _recoveryContext.SaveChangesAsync();

                return (true, "OK");
            }
            catch(Exception ex)
            {
                return (false, ex.Message);
            }
        }

        //mengcancel yang statusnya bukan draft
        public async Task<(bool status, string message)> NonActiveRestrukture(string userid, long id, long loanid)
        {
            try
            {
                var getCallBy = await _User.GetDataUser(userid);

                var GetData = await _recoveryContext.Restrukture.Where(es => es.Id == id && es.Loanid == loanid).FirstOrDefaultAsync();
                GetData.Isactive = 0;
                GetData.Iscancel = 0;
                GetData.Lastupdatedid = getCallBy.Returns.Data.FirstOrDefault().iduser;
                _recoveryContext.Entry(GetData).State = EntityState.Modified;
                await _recoveryContext.SaveChangesAsync();

                return (true, "OK");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        //mengnoncancel yang statusnya bukan draft
        public async Task<(bool status, string message)> ActiveRestrukture(string userid, long id, long loanid)
        {
            try
            {
                var getCallBy = await _User.GetDataUser(userid);

                var GetData = await _recoveryContext.Restrukture.Where(es => es.Id == id && es.Loanid == loanid).FirstOrDefaultAsync();
                GetData.Isactive = 1;
                GetData.Iscancel = 0;
                GetData.Lastupdatedid = getCallBy.Returns.Data.FirstOrDefault().iduser;
                _recoveryContext.Entry(GetData).State = EntityState.Modified;
                await _recoveryContext.SaveChangesAsync();

                return (true, "OK");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool Status, string Message, List<dynamic> Data)> GetPolaRestrukture(int RestruktureId)
        {
            var ListData = new List<dynamic>();
            try
            {
                var Data = await _recoveryContext.Restrukture. 
                    Where(es => es.Id == RestruktureId)
                    .Select(es => new GetPolaRestrukDTO
                    {

                        idloan = es.Loanid,
                        idrestrukture = (int)es.Id,
                        branchid = es.Mstbranchid,
                        polaid = es.Polarestrukturid,
                        keterangan = es.Keterangan,
                        pengurangannilaimargin = es.Pengurangannilaimargin,
                        jenispenguranganid = es.Jenispenguranganid,
                        graceperiode = es.Graceperiode

                    }).ToListAsync();
                  
                foreach(var x in Data)
                {

                    var Wraps = new GetPolaRestrukDTO();
                    Wraps.idloan = x.idloan;
                    Wraps.idrestrukture = x.idrestrukture;
                    Wraps.branchid = x.branchid;
                    Wraps.polaid = x.polaid;
                    Wraps.keterangan = x.keterangan;
                    Wraps.pengurangannilaimargin = x.pengurangannilaimargin;
                    Wraps.jenispenguranganid = x.jenispenguranganid;
                    Wraps.graceperiode = x.graceperiode;
                    Wraps.cabang = await _collContext.Branches.Where(es => es.LbrcId == x.branchid).Select(es => es.LbrcName).FirstOrDefaultAsync();
                    Wraps.jenispengurangan = await _recoveryContext.Generalparamdetail.Where(es => es.Id == x.jenispenguranganid).Select(es => es.Title).FirstOrDefaultAsync();
                    Wraps.pola = await _recoveryContext.Generalparamdetail.Where(es => es.Id == x.polaid).Select(es => es.Title).FirstOrDefaultAsync();

                    ListData.Add(Wraps);
                };

               
                return (true, "OK", ListData);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }


        public async Task<(string message, bool? status)> WorkflowSubmit(int? idrequest, int? idfitur, string userid)
        {
            try
            {
                var Data = new SubmitWorkflowDTO()
                {
                    idfitur = idfitur,
                    idrequest = idrequest,
                    userid = userid
                };
                var SubmitWorkflow = await _workflowServices.SubmitWorkflowStep(Data);
                return (SubmitWorkflow.Returns.Message, SubmitWorkflow.Status);
            }
            catch (Exception ex)
            {
                return (ex.Message, false);
            }
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
          
                var ReturnData = await _postgreRepository.GetRestukture(1, "\""+RecoverySchema.RecoveryBusinessV2.ToString()+"\"."+RecoveryFunctionName.getrestrukture.ToString() + "", Convert.ToInt32(getCallBy.Returns.Data.FirstOrDefault().acceslevel), getCallBy.Returns.Data.FirstOrDefault().iduser);
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
        //Get Pola Metode RESTRUKTUR V2
        public async Task<(bool? Status, string message, Dictionary<string, List<dynamic>> DataNasabah)> GetPolaMetodeRestrukture(int? idrestrukture, int? idloan)
        {
            var wrap = _DataResponses.ReturnDictionary();
            var SkyCollConsString = GetSkyCollConsString();

            try
            {
                var GetMetodeRestruktur = await _recoveryContext.Generalparamdetail.Where(es => es.Paramheaderid == 4).ToListAsync<dynamic>();
                var GetJenisPengurangan = await _recoveryContext.Generalparamdetail.Where(es => es.Paramheaderid == 5).ToListAsync<dynamic>();
                //  var GetBranchList = await _postgreRepository.GetBranchList("\"" + CoreSchema.param.ToString() + "\"." + CoreFunctionName.getallbranchactived.ToString()+"");
                var GetBranchList = await _collContext.Branches.Where(es=>es.LbrcIsDelete==false).ToListAsync<dynamic>();
                var GetDetailPolaRestrukture = await _postgreRepository.GetDetailPolaRestruktur("\"" + RecoverySchema.RecoveryBusinessV2.ToString() + "\"." + RecoveryFunctionName.getdetailpolarestrukture.ToString() + "",idrestrukture,idloan,"test");

                wrap.Status = true;
                wrap.Message = "OK";
                var Collection = new Dictionary<string, List<dynamic>>();
                Collection["MetodeRestruktur"] = GetMetodeRestruktur;
                Collection["JenisPengurangan"] = GetJenisPengurangan;
                Collection["DataRestrukture"] = GetDetailPolaRestrukture;
               Collection["BranchList"] = GetBranchList;

              

                return (wrap.Status, wrap.Message, Collection);
            }
            catch (Exception ex)
            {
                wrap.Status = false;
                wrap.Message = ex.Message;

                return (wrap.Status,wrap.Message, null);
            }
        }


        //SERVICE YANG DIPAKAI
        //CREATE POLA RESTRUKTUR V2
        public async Task<(bool? Status, GeneralResponsesConfigV2 Returns)> ConfigPola(RequestPolaDTO Entity)
        {
            var wrap = _DataResponses.GeneralResponsesConfigData();
            var SkyCollConsString = GetSkyCollConsString();

            try
            {
              

                var GetMetodeRestruktur = await _GeneralParam.GetParamDetail(4);
                var GetJenisPengurangan = await _GeneralParam.GetParamDetail(5);

                wrap.Status = true;
                wrap.Message = "OK";
                wrap.MetodeRestruktur = GetMetodeRestruktur.DataDetail;
                wrap.JenisPengurangan = GetJenisPengurangan.DataDetail;

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
        //MONITORING RESTRUKTUR V2
        public async Task<(bool? Status, GeneralResponsesV2 Returns)> ConfigAnalisaRestrukture(string userid,ConfigAnalisaRestruktureDTO Entity)
        {
            var wrap = _DataResponses.Return();
            var SkyCollConsString = GetSkyCollConsString();

            try
            {

                var getCallBy = await _User.GetDataUser(userid);
                // pindah ke dinamis
                //if (getCallBy.Returns.Data.FirstOrDefault().role != RestrukturRole.Operator.ToString())
                //{
                //    wrap.Status = false;
                //    wrap.Message = "Not Authorize";
                //    return (wrap.Status, wrap);
                //}
                var ReturnData = await _postgreRepository.CheckingAnalisaRestruktureExisting("\"" + RecoverySchema.RecoveryBusinessV2.ToString() + "\"." + RecoveryFunctionName.checkinganalisaexistingrestrukture.ToString() + "", getCallBy.Returns.Data.FirstOrDefault().iduser,Entity.analisaid,Entity.idrestrukture,Entity.loanid);
                if(ReturnData.Count>0)
                {
                    //update
                    var UpdateAnalisa = await _postgreRepository.UpdateAnalisaRestrukture("\"" + RecoverySchema.RecoveryBusinessV2.ToString() + "\"." + RecoveryFunctionName.updateanalisarestrukture.ToString() + "", getCallBy.Returns.Data.FirstOrDefault().iduser,  Entity);
                    wrap.Status = true;
                    wrap.Message = "OK";

                    wrap.Data = UpdateAnalisa;
                }
                else
                {
                    //insert
                    var InsertAnalisa = await _postgreRepository.CreateAnalisaRestrukture("\"" + RecoverySchema.RecoveryBusinessV2.ToString() + "\"." + RecoveryFunctionName.configanalisarestruktur.ToString() + "", getCallBy.Returns.Data.FirstOrDefault().iduser,  Entity);
                    wrap.Status = true;
                    wrap.Message = "OK";

                    wrap.Data = InsertAnalisa;
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

        public async Task<(bool Status,string message, List<dynamic> Data)> GetMasterStatus()
        {
            var ListData = new List<dynamic>();
            try
            {
                var Data = await _collContext.Statuses.ToListAsync();
                ListData.Add(Data);
                return (true, "OK", ListData);
            }
            catch(Exception ex)
            {
                return (false, ex.Message,null);
            }
        }


        //SERVICE YANG DIPAKAI
        //MONITORING RESTRUKTUR V2
        public async Task<(bool? Status, GeneralResponsesV2 Returns)> ConfigPolaRestrukture(string userid,AddPolaDTO Entity)
        {
            var wrap = _DataResponses.Return();
            var SkyCollConsString = GetSkyCollConsString();

            try
            {

                var getCallBy = await _User.GetDataUser(userid);
               
               
                    //insert
                    var InsertAnalisa = await _postgreRepository.UpdateConfigPolaRestrukture("\"" + RecoverySchema.RecoveryBusinessV2.ToString() + "\"." + RecoveryFunctionName.configpolarestrukture.ToString() + "", getCallBy.Returns.Data.FirstOrDefault().iduser, Entity);
                    wrap.Status = true;
                    wrap.Message = "OK";

                wrap.Data = null;
                


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
        //MONITORING RESTRUKTUR V2
        public async Task<(bool? Status, GeneralResponsesV2 Returns)> SubmitRestrukture(string userid,SubmitRestruktureDTO Entity)
        {
            var wrap = _DataResponses.Return();
            var SkyCollConsString = GetSkyCollConsString();

            try
            {

                var getCallBy = await _User.GetDataUser(userid);
                var getCallBySPV = await _User.GetDataUser(getCallBy.Returns.Data.FirstOrDefault().spvname);


                //insert
                var CheckingDataRestrukture = await _postgreRepository.SubmitRestrukturApproval("\"" + RecoverySchema.RecoveryBusinessV2.ToString() + "\"." + RecoveryFunctionName.submitapprovalrestrukture.ToString() + "", getCallBy.Returns.Data.FirstOrDefault().iduser,getCallBySPV.Returns.Data.FirstOrDefault().iduser,Entity.idrestrukture);
               
                
                if (CheckingDataRestrukture.Count > 0)
                {
                    wrap.Status = true;
                    wrap.Message = "OK";

                    wrap.Data = null;

                }
                else
                {
                    wrap.Status = false;
                    wrap.Message = "Data Belum Sepenuhnya Lengkap, Proses Submit Approval Belum Dapat Dilakukan";
                    wrap.Data = null;

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
        //REMOVE DRAFT RESTRUKTUR V2
        public async Task<(bool? Status, GeneralResponsesV2 Returns)> RemoveDraftRestukture(string user,string userid, int? idloan,int? idrestrukture)
        {
            var wrap = _DataResponses.Return();

            try
            {

                 var getCallBy = await _User.GetDataUser(user);
                // pindah ke dinamis

              

                if (idloan == null)
                {
                    wrap.Status = false;
                    wrap.Message = "Anda Harus Memilih pinjaman yang akan di Remove";
                    return (wrap.Status, wrap);
                }
                if (idrestrukture== null)
                {
                    wrap.Status = false;
                    wrap.Message = "Anda Harus Memilih Restrukture yang akan di Remove";
                    return (wrap.Status, wrap);
                }
                var ReturnData = await _postgreRepository.RemoveDraftRestrukture("\"" + RecoverySchema.RecoveryBusinessV2.ToString() + "\"." + RecoveryFunctionName.removedraftrestrukture.ToString() + "",getCallBy.Returns.Data.FirstOrDefault().iduser,idloan,idrestrukture);
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
        public async Task<(bool? Status, GeneralResponsesDocRestrukturV2 Returns)> UploadDocRestrukture(string userid,UploadDocRestrukturDTO Entity)
        {
            var wrap = _DataResponses.GeneralResponseDocRestruktur();

            try
            {

                var getCallBy = await _User.GetDataUser(userid);
                // pindah ke dinamis

                if(Entity==null)
                {
                    wrap.Status = false;
                    wrap.Message = "Request Not Valid";
                }
                if(Entity.File.Length<0)
                {
                    wrap.Status = false;
                    wrap.Message = "File harus diupload";
                }
                if(Entity.File.FileName==null)
                {
                    wrap.Status = false;
                    wrap.Message = "File harus diupload";
                }
                if(Entity.IdLoan==null || Entity.IdRestrukture==null || Entity.IdDocType==null)
                {
                    wrap.Status = false;
                    wrap.Message = "Id Loan, Jenis Doc dan Id Restrukture Harus Diisi";
                }
                //var path = "wwwroot/Documents";
                //var path = Path.Combine(_environment.WebRootPath, "File");
                var GetNamingFolder = await _recoveryContext.Generalparamdetail.Where(es => es.Id == 9).Select(es => es.Title).FirstOrDefaultAsync();

                var path = Path.Combine(_environment.WebRootPath, "File/"+GetNamingFolder);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var ReturnCheckingDoc = await _postgreRepository.CheckingDocRestrukture("\"" + RecoverySchema.RecoveryBusinessV2.ToString() + "\"." + RecoveryFunctionName.checkingavailabledoc.ToString() + "", Entity.IdLoan,Entity.IdRestrukture,Entity.IdDocType);

                if(ReturnCheckingDoc.Count==0)
                {

                    string ext = Path.GetExtension(Entity.File.FileName);

                    var nm = Path.Combine(path,Entity.File.FileName+ext);

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
                    var nm = Path.Combine(path, Entity.File.FileName + ext);

                   // var nm = path + "/" + Entity.File.FileName + ext;


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
        public async Task<(bool? Status, GeneralResponsesV2 Returns)> CreatePermasalahan(string userid,CreatePermasalahanDTO Entity)
        {
            var wrap = _DataResponses.Return();

            try
            {

                var getCallBy = await _User.GetDataUser(userid);
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
        public async Task<(bool? Status, GeneralResponsesV2 Returns)> UpdatePermasalahan(string userid,UpdatePermasalahanDTO Entity)
        {
            var wrap = _DataResponses.Return();

            try
            {

                 var getCallBy = await _User.GetDataUser(userid);
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

        public async Task<(bool Status, string Message, long Id)> GetMyFitur()
        {
            var Data = await _recoveryContext.Generalparamdetail.Where(es => es.Title == "Restrukture").Select(es => es.Id).FirstOrDefaultAsync();
            return (true, "OK", Data);
        
        }

        //SERVICE YANG DIPAKAI
        //TASKLIST RESTRUKTUR V2
        public async Task<(bool? Status, GeneralResponsesV2 Returns)> TaskListRestrukturV2(string UserId)
        {
            var wrap = _DataResponses.Return();

            try
            {

                var getCallBy = await _User.GetDataUser(UserId);
               
               

               
                var ReturnData = await _postgreRepository.GetRestukture(2, "\"" + RecoverySchema.RecoveryBusinessV2.ToString() + "\"." + RecoveryFunctionName.tasklistrestrukture.ToString() + "", Convert.ToInt32(getCallBy.Returns.Data.FirstOrDefault().acceslevel), getCallBy.Returns.Data.FirstOrDefault().iduser);
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
        public async Task<(bool? Status, GeneralResponsesV2 Returns)> ActionApprovalRestrukture(string userid,ApprovalActionDTO Entity)
        {
            var wrap = _DataResponses.Return();

            try
            {

                var getCallBy = await _User.GetDataUser(userid);
                // pindah ke dinamis
                //if (getCallBy.Returns.Data.FirstOrDefault().acceslevel != ConfigSPVNumber.SPVC.ToString()
                //    || getCallBy.Returns.Data.FirstOrDefault().acceslevel != ConfigSPVNumber.SPVG.ToString()
                //    )
                //{
                //     wrap.Status  = false;
                //    wrap.Message = "Not Authorize";
                //    return ( wrap.Status , wrap);
                //}
                var ReturnData = await _postgreRepository.ActionApproval("\"" + RecoverySchema.RecoveryBusinessV2.ToString() + "\"." + RecoveryFunctionName.actionapproval.ToString() + "", getCallBy.Returns.Data.FirstOrDefault().iduser,Entity);
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
        public async Task<(bool? Status, GeneralResponsesV2 Returns)> GetWorkflowHistory(int? idrequest)
        {
            var wrap = _DataResponses.Return();

            try
            {

                //var getCallBy = await _User.GetDataUser(Entity.UserId);
                // pindah ke dinamis
                //if (getCallBy.Returns.Data.FirstOrDefault().acceslevel != ConfigSPVNumber.SPVC.ToString()
                //    || getCallBy.Returns.Data.FirstOrDefault().acceslevel != ConfigSPVNumber.SPVG.ToString()
                //    )
                //{
                //     wrap.Status  = false;
                //    wrap.Message = "Not Authorize";
                //    return ( wrap.Status , wrap);
                //}
                var ReturnData = await _postgreRepository.GetWorkflowHistory("\"" + RecoverySchema.RecoveryBusinessV2.ToString() + "\"." + RecoveryFunctionName.getworkflowhistory.ToString() + "", idrequest);
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
        //GET DETAIL DRAFTING RESTRUKTUR V2
        public async Task<(bool? Status, GeneralResponsesDetailRestrukturV2 Returns)> GetDetailDraftingRestruktur(int? loanid, int idrestrukture)
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
                var ReturnDetail = await _postgreRepository.GetDetailDrafting(SkyCollConsString.Data.ConnectionSetting, "\"" + RecoverySchema.RecoveryBusinessV2.ToString() + "\"." + RecoveryFunctionName.getdetailfordraftingrestruktur.ToString() + "", loanid,idrestrukture);
                
                
                var ReturnFasilitas = await _postgreRepository.GetListFasilitas(SkyCollConsString.Data.ConnectionSetting, "\"" + RecoverySchema.RecoveryBusinessV2.ToString() + "\"." + RecoveryFunctionName.getlistfasilitas.ToString() + "", loanid);

                var GetPermasalahan = await _postgreRepository.GetPermasalahanRestrukture("\"" + RecoverySchema.RecoveryBusinessV2.ToString() + "\"." + RecoveryFunctionName.getpermasalahanrestrukture.ToString() + "", loanid,idrestrukture);
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

        public async Task<(bool status,string message,List<dynamic>Data)>GetStatusDocument()
        {
            try
            {
                var Datas = await _recoveryContext.Generalparamdetail.Where(es => es.Paramheaderid == 8).
                    ToListAsync<dynamic>();
                return (true, "OK", Datas);
            }
            catch(Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

        //SERVICE YANG DIPAKAI
        //CREATE DRAFT FOR RESTRUKTUR V2
        public async Task<(bool? Status, GeneralResponsesV2 Returns)> CreateDraftRestrukture(string userid,AddRestructureDTO Entity)
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

                 var getCallBy = await _User.GetDataUser(userid);
                //if(getCallBy.Returns.Data.FirstOrDefault().role!=RestrukturRole.Operator.ToString())
                //{
                //    wrap.Status = false;
                //    wrap.Message = "Not Authorize";
                //}
                var ReturnData = await _postgreRepository.CreateDraftRestrukture("\"" + RecoverySchema.RecoveryBusinessV2.ToString() + "\"." + RecoveryFunctionName.createdraftrestrukture.ToString() + "",getCallBy.Returns.Data.FirstOrDefault().iduser,getCallBy.Returns.Data.FirstOrDefault().RoleId,Entity);
                wrap.Data = ReturnData;
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
