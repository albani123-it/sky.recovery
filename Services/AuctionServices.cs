using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using sky.recovery.Helper.Config;
using sky.recovery.Insfrastructures;
using sky.recovery.Interfaces;
using sky.recovery.Responses;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using sky.recovery.DTOs.ResponsesDTO.Ayda;
using sky.recovery.DTOs.ResponsesDTO.Aucton;
using sky.recovery.DTOs.WorkflowDTO;
using sky.recovery.DTOs.RequestDTO.Ayda;
using sky.recovery.Entities;
using sky.recovery.DTOs.RequestDTO.Auction;
using sky.recovery.Insfrastructures.Scafolding.SkyColl.Recovery;
using sky.recovery.Insfrastructures.Scafolding.SkyCore.Public;
using sky.recovery.DTOs.ResponsesDTO.Restrukture;

namespace sky.recovery.Services
{
    public class AuctionServices : SkyCoreConfig, IAuctionService
    {
        sky.recovery.Insfrastructures.Scafolding.SkyColl.Recovery.SkyCollRecoveryDBContext _recoveryContext = new Insfrastructures.Scafolding.SkyColl.Recovery.SkyCollRecoveryDBContext();

        sky.recovery.Insfrastructures.Scafolding.SkyColl.Public.SkyCollPublicDBContext _collContext = new Insfrastructures.Scafolding.SkyColl.Public.SkyCollPublicDBContext();
        skycoreContext _skyCoreContext = new skycoreContext();
        private IWorkflowServices _workflowServices { get; set; }
        private IUserService _User { get; set; }
        private IGeneralParam _GeneralParam { get; set; }
        private readonly IWebHostEnvironment _environment;
        private IRestruktureRepository _postgreRepository { get; set; }
        private IHelperRepository _helperRepository { get; set; }

        ModellingGeneralResponsesV2 _DataResponses = new ModellingGeneralResponsesV2();
        AydaHelper _aydahelper = new AydaHelper();
        public AuctionServices(IWorkflowServices workflowServices, IGeneralParam GeneralParam, IWebHostEnvironment environment, IUserService User, IHelperRepository helperRepository, IRestruktureRepository postgreRepository,
      IOptions<DbContextSettings> appsetting) : base(appsetting)
        {
            _workflowServices = workflowServices;
            _environment = environment;
            _GeneralParam = GeneralParam;
            _User = User;
            _postgreRepository = postgreRepository;
            _helperRepository = helperRepository;

        }



        public async Task<(bool? Status, GeneralResponsesV2 Returns)> AuctionTaskList(string UserId)
        {
            var wrap = _DataResponses.Return();
            var ListData = new List<dynamic>();
            // var SkyCollConsString = GetSkyCollConsString();

            try
            {
                if (String.IsNullOrEmpty(UserId))
                {
                    wrap.Status = false;
                    wrap.Message = "User Id Harus Diisi";
                }

                var getCallBy = await _User.GetDataUser(UserId);
                // pindah ke dinamis
                //if (getCallBy.Returns.Data.FirstOrDefault().role != RestrukturRole.Operator.ToString())
                //{
                //     wrap.Status  = false;
                //    wrap.Message = "Not Authorize";
                //    return ( wrap.Status , wrap);
                //}
                var ReturnData = await auction.Include(i => i.master_loan).Where(es => 
               
                es.statusid==11).Select(
                    es => new DTOs.ResponsesDTO.Aucton.MonitoringBean
                    {
                        branch = es.master_loan.master_customer.branch.lbrc_name,
                        noaccount = es.master_loan.acc_no,
                        cif = es.master_loan.cu_cif,
                        nama = es.master_loan.master_customer.cu_name,
                        loanid = es.master_loan.id,
                        status = es.status.sts_name

                    }
                    ).ToListAsync();
                wrap.Status = true;
                wrap.Message = "OK";
                ListData.Add(ReturnData);
                wrap.Data = ListData;
                return (wrap.Status, wrap);

            }
            catch (Exception ex)
            {
                wrap.Status = false;
                wrap.Message = ex.Message;

                return (wrap.Status, wrap);
            }
        }

        public async Task<(bool? Status, GeneralResponsesV2 Returns)> AuctionMonitoring(string UserId)
        {
            var wrap = _DataResponses.Return();
            var ListData = new List<dynamic>();
            // var SkyCollConsString = GetSkyCollConsString();

            try
            {
                if (String.IsNullOrEmpty(UserId))
                {
                    wrap.Status = false;
                    wrap.Message = "User Id Harus Diisi";
                }

                var getCallBy = await _User.GetDataUser(UserId);
                // pindah ke dinamis
                //if (getCallBy.Returns.Data.FirstOrDefault().role != RestrukturRole.Operator.ToString())
                //{
                //     wrap.Status  = false;
                //    wrap.Message = "Not Authorize";
                //    return ( wrap.Status , wrap);
                //}
                var ReturnData = await auction.Include(i => i.master_loan).Where(es => es.createdby == getCallBy.Returns.Data.FirstOrDefault().iduser).Select(
                    es => new DTOs.ResponsesDTO.Aucton.MonitoringBean
                    {
                         branch = es.master_loan.master_customer.branch.lbrc_name,
                        noaccount = es.master_loan.acc_no,
                        cif=es.master_loan.cu_cif,
                        nama = es.master_loan.master_customer.cu_name,
                        loanid = es.master_loan.id,                    
                        status = es.status.sts_name

                    }
                    ).ToListAsync();
                wrap.Status = true;
                wrap.Message = "OK";
                ListData.Add(ReturnData);
                wrap.Data = ListData;
                return (wrap.Status, wrap);

            }
            catch (Exception ex)
            {
                wrap.Status = false;
                wrap.Message = ex.Message;

                return (wrap.Status, wrap);
            }
        }


        //GET DETAIL AYDA
        public async Task<(bool? Status, string message, Dictionary<string, List<dynamic>> DataNasabah)> GetDetailAuction(GetDetailAuctionDTO Entity)
        {
            var wrap = _DataResponses.Return();
            var ListData = new List<dynamic>();
            // var SkyCollConsString = GetSkyCollConsString();

            try
            {

                var Nasabah = await _collContext.MasterCustomer.Where(es => es.Id == Entity.CustomerId).Select(
                   es => new NasabahAuctionDTO
                   {
                       Nama = es.CuName,
                       Address = es.CuAddress,
                       KTP = es.CuIdnumber,
                       CuCif = es.CuCif,
                       BranchId = es.BranchId,
                       Branch = _collContext.Branch.Where(x => x.LbrcId == es.BranchId).Select(es => es.LbrcName).FirstOrDefault(),
                       AccNo = _collContext.MasterLoan.Where(es => es.Id == Entity.LoanId).Select(es => es.AccNo).FirstOrDefault()

                   }
                   ).ToListAsync();

                var Files = await _recoveryContext.Masterrepository.Where(es => es.Requestid == Entity.AuctionId && es.Isactive == 1).Select(
                   es => new RepoAydaDTO
                   {
                       Id = es.Id,
                       url = es.Fileurl,
                       urlname = es.Filename,
                       uploaddated = es.Uploaddated.ToString(),
                       doctype = es.Doctype

                   }
                   ).ToListAsync();

                var DataLoan = await _collContext.MasterLoan.Where(es => es.Id == Entity.LoanId).Select(es => new LoanAuctionDTO
                {
                    loanid = es.Id,
                    Fasilitas = es.Fasilitas,
                    Tenor = es.Tenor.ToString(),
                    LoanType = _collContext.Rfproduct.Where(x => x.PrdId == es.Product).Select(es => es.PrdDesc).FirstOrDefault(),
                    Plafond = es.Plafond.ToString()
                }).ToListAsync();

                var DataAuction = await _recoveryContext.Auction.Where(es => es.Id == Entity.AuctionId).Select(es => new DataAuction
                {
                   alasanlelangid=es.Alasanlelangid,
                   nopk=es.Nopk,
                   nilailimitlelang=es.Nilailimitlelang,
                   uangjaminan=es.Uangjaminan,
                   objeklelang=es.Objeklelang,
                   keterangan=es.Keterangan,
                   balailelangid=es.Balailelangid,
                   jenislelangid=es.Jenislelangid,
                   tatacaralelang=es.Tatacaralelang,
                   biayalelang=es.Biayalelang,
                   catatanlelang=es.Catatanlelang,
                   tglpenetapanlelang=es.Tglpenetapanlelang,
                   norekening=es.Norekening,
                   namarekening=es.Namarekening,
                   status=_collContext.Status.Where(x=>x.StsId==es.Statusid).Select(es=>es.StsName).FirstOrDefault(),
                   statusid=es.Statusid,
                   createdby=es.Createdby,
                    createdbywho = _skyCoreContext.Users.Where(x => x.UsrId == es.Createdby).Select(s => s.UsrUserid).FirstOrDefault(),

                   createddated = es.Createddated


                }).ToListAsync();

                var DataCreated = _recoveryContext.Auction.Where(es => es.Id == Entity.AuctionId).AsEnumerable()
                   .Select(es => new InformationRequest
                   {
                       createdby = _skyCoreContext.Users.Where(x => x.UsrId == es.Createdby).Select(es => es.UsrUserid).FirstOrDefault(),
                       createddated = es.Createddated,
                       CreatedById = es.Createdby
                   }).ToList();

                var Collection = new Dictionary<string, List<dynamic>>();




                Collection["DataNasabah"] = new List<dynamic>();
                Collection["DataFiles"] = new List<dynamic>();
                Collection["DataLoan"] = new List<dynamic>();
                Collection["DataAuction"] = new List<dynamic>();
                Collection["CreatedInformation"] = new List<dynamic>();


                Collection["DataNasabah"].Add(Nasabah);
                Collection["DataFiles"].Add(Files);
                Collection["DataLoan"].Add(DataLoan);
                Collection["DataAuction"].Add(DataAuction);
                Collection["CreatedInformation"].Add(DataCreated);



                return (true, "OK", Collection);

            }
            catch (Exception ex)
            {


                return (false, ex.Message, null);
            }
        }

        public async Task<(bool? Status, GeneralResponsesV2 Returns)> AuctionSubmit(CreateAuctionDTO Entity)
        {
            var wrap = _DataResponses.Return();
            var ListData = new List<dynamic>();
            var getCallBy = await _User.GetDataUser(Entity.User.UserId);

            // var SkyCollConsString = GetSkyCollConsString();

            try
            {

                //var GetAydaExisting = await ayda.Where(es => es.id == Entity.Data.aydaid).AnyAsync();
                if (Entity.Data != null)// update draft
                {
                    var GetData = await _recoveryContext.Auction.Where(es => es.Id== Entity.Data.AuctionId && es.Loanid== Entity.DataNasabahLoan.loanid).FirstOrDefaultAsync();
                    if (_aydahelper.IsRequested(GetData.Statusid) == true)
                    {
                        wrap.Status = false;
                        wrap.Message = "Data tidak bisa diupdate, karena sudah masuk proses approval";
                    }

                    GetData.Mstbranchid = Entity.DataNasabahLoan.BranchId;
                    GetData.Alasanlelangid = Entity.Data.AlasanLelangId;
                    GetData.Nopk = Entity.Data.nopk;
                    GetData.Nilailimitlelang = Entity.Data.nilailimitlelang;
                    GetData.Uangjaminan = Entity.Data.uangjaminan;
                    GetData.Objeklelang = Entity.Data.objeklelang;
                    GetData.Keterangan = Entity.Data.keterangan;
                    GetData.Balailelangid = Entity.Data.balailelangid;
                    GetData.Jenislelangid = Entity.Data.jenislelangid;
                    GetData.Tatacaralelang = Entity.Data.tatacaralelang;
                    GetData.Biayalelang = Entity.Data.biayalelang;
                    GetData.Catatanlelang = Entity.Data.catatanlelang;
                    GetData.Tglpenetapanlelang = Entity.Data.tglpenetapanlelang;
                    GetData.Norekening = Entity.Data.norekening;
                    GetData.Namarekening = Entity.Data.namarekening;

                    GetData.Statusid=  _collContext.Status.Where(es => es.StsName == "REVIEW").Select(es => es.StsId).FirstOrDefault();
                    GetData.Createdby = getCallBy.Returns.Data.FirstOrDefault().iduser;
                    GetData.Lastupdatedate = DateTime.Now;

                    Entry(GetData).State = EntityState.Modified;
                    await SaveChangesAsync();
                    var GetIdAyda = await _recoveryContext.Generalparamdetail.Where(es => es.Title== "Auction").Select(es => es.Id).FirstOrDefaultAsync();
                    var SubmitWorkflow = await WorkflowSubmit(Entity.Data.AuctionId, (int?)GetIdAyda, Entity.User.UserId);

                }
                else
                {
                    var Data = new Auction()
                    {
                        Loanid = Entity.DataNasabahLoan.loanid,
                        Mstbranchid = Entity.DataNasabahLoan.BranchId,

                        Alasanlelangid = Entity.Data.AlasanLelangId,
                        Nopk = Entity.Data.nopk,
                        Nilailimitlelang = Entity.Data.nilailimitlelang,
                        Uangjaminan = Entity.Data.uangjaminan,
                        Objeklelang = Entity.Data.objeklelang,
                        Keterangan = Entity.Data.keterangan,
                        Balailelangid = Entity.Data.balailelangid,
                        Jenislelangid = Entity.Data.jenislelangid,
                        Tatacaralelang = Entity.Data.tatacaralelang,
                        Biayalelang = Entity.Data.biayalelang,
                        Catatanlelang = Entity.Data.catatanlelang,
                        Tglpenetapanlelang = Entity.Data.tglpenetapanlelang,
                        Norekening = Entity.Data.norekening,
                        Namarekening = Entity.Data.namarekening,


                        Statusid =  _collContext.Status.Where(es => es.StsName== "REQUESTED").Select(es => es.StsId).FirstOrDefault(),
                        Createdby = getCallBy.Returns.Data.FirstOrDefault().iduser,
                        Createddated = DateTime.Now
                    };
                    await _recoveryContext.Auction.AddAsync(Data);
                    await SaveChangesAsync();
                    var GetIdAyda = await _recoveryContext.Generalparamdetail.Where(es => es.Title== "Auction").Select(es => es.Id).FirstOrDefaultAsync();
                    var SubmitWorkflow = await WorkflowSubmit(Entity.Data.AuctionId, (int?)Data.Id, Entity.User.UserId);

                }

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
         


        public async Task<(bool? Status, GeneralResponsesV2 Returns)> AuctionDraft(string userid,CreateAuctionDTO Entity)
        {
            var wrap = _DataResponses.Return();
            var ListData = new List<dynamic>();
            var getCallBy = await _User.GetDataUser(userid);

            // var SkyCollConsString = GetSkyCollConsString();

            try
            {

                //var GetAydaExisting = await ayda.Where(es => es.id == Entity.Data.aydaid).AnyAsync();
                if (Entity.Data != null)// update draft
                {
                    var GetData = await _recoveryContext.Auction.Where(es => es.Id == Entity.Data.AuctionId && es.Loanid == Entity.DataNasabahLoan.loanid).FirstOrDefaultAsync();
                    if (_aydahelper.IsRequested(GetData.Statusid) == true)
                    {
                        wrap.Status = false;
                        wrap.Message = "Data tidak bisa diupdate, karena sudah masuk proses approval";
                    }

                    GetData.Mstbranchid = Entity.DataNasabahLoan.BranchId;
                    GetData.Alasanlelangid = Entity.Data.AlasanLelangId;
                    GetData.Nopk = Entity.Data.nopk;
                    GetData.Nilailimitlelang = Entity.Data.nilailimitlelang;
                    GetData.Uangjaminan = Entity.Data.uangjaminan;
                    GetData.Objeklelang = Entity.Data.objeklelang;
                    GetData.Keterangan = Entity.Data.keterangan;
                    GetData.Balailelangid = Entity.Data.balailelangid;
                    GetData.Jenislelangid = Entity.Data.jenislelangid;
                    GetData.Tatacaralelang = Entity.Data.tatacaralelang;
                    GetData.Biayalelang = Entity.Data.biayalelang;
                    GetData.Catatanlelang = Entity.Data.catatanlelang;
                    GetData.Tglpenetapanlelang = Entity.Data.tglpenetapanlelang;
                    GetData.Norekening = Entity.Data.norekening;
                    GetData.Namarekening = Entity.Data.namarekening;
                    


                    GetData.Statusid = _collContext.Status.Where(es => es.StsName == "REQUESTED").Select(es => es.StsId).FirstOrDefault();
                    GetData.Createdby = getCallBy.Returns.Data.FirstOrDefault().iduser;
                    GetData.Lastupdatedate = DateTime.Now;
                    GetData.Lastupdatedid = getCallBy.Returns.Data.FirstOrDefault().iduser;

                    Entry(GetData).State = EntityState.Modified;
                    await SaveChangesAsync();
                    var GetIdAyda = await _recoveryContext.Generalparamdetail.Where(es => es.Title == "Auction").Select(es => es.Id).FirstOrDefaultAsync();
                    var SubmitWorkflow = await WorkflowSubmit(Entity.Data.AuctionId, (int?)GetIdAyda, Entity.User.UserId);

                }
                else
                {
                    var Data = new Auction()
                    {
                        Loanid = Entity.DataNasabahLoan.loanid,
                        Mstbranchid = Entity.DataNasabahLoan.BranchId,

                    Alasanlelangid = Entity.Data.AlasanLelangId,
                    Nopk= Entity.Data.nopk,
                        Nilailimitlelang = Entity.Data.nilailimitlelang,
                        Uangjaminan = Entity.Data.uangjaminan,
                        Objeklelang = Entity.Data.objeklelang,
                        Keterangan = Entity.Data.keterangan,
                        Balailelangid = Entity.Data.balailelangid,
                        Jenislelangid = Entity.Data.jenislelangid,
                        Tatacaralelang = Entity.Data.tatacaralelang,
                        Biayalelang = Entity.Data.biayalelang,
                        Catatanlelang = Entity.Data.catatanlelang,
                        Tglpenetapanlelang = Entity.Data.tglpenetapanlelang,
                        Norekening = Entity.Data.norekening,
                        Namarekening = Entity.Data.namarekening,


                        Statusid = _collContext.Status.Where(es => es.StsName == "REQUESTED").Select(es => es.StsId).FirstOrDefault(),
                        Createdby = getCallBy.Returns.Data.FirstOrDefault().iduser,
                        Createddated = DateTime.Now
                    };
                    await _recoveryContext.Auction.AddAsync(Data);
                    await SaveChangesAsync();
                    var GetIdAyda = await _recoveryContext.Generalparamdetail.Where(es => es.Title == "Auction").Select(es => es.Id).FirstOrDefaultAsync();
                    var SubmitWorkflow = await WorkflowSubmit(Entity.Data.AuctionId, (int?)Data.Id, Entity.User.UserId);

                }

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


        public async Task<(bool? Status, GeneralResponsesV2 Returns)> SetIsActive(int Id, int status)
        {
            var wrap = _DataResponses.Return();
            var ListData = new List<dynamic>();
            //var getCallBy = await _User.GetDataUser(Entity.User.UserId);

            // var SkyCollConsString = GetSkyCollConsString();

            try
            {
                var GetData = await auction.Where(es => es.id == Id).FirstOrDefaultAsync();
                GetData.isactive = status;
                GetData.lastupdatedate = DateTime.Now;
                Entry(GetData).State = EntityState.Modified;
                await SaveChangesAsync();
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

    }
}
