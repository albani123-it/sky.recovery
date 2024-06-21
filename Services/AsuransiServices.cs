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
using sky.recovery.DTOs.WorkflowDTO;
using sky.recovery.Insfrastructures.Scafolding.SkyColl.Public;
using sky.recovery.DTOs.RequestDTO.Ayda;
using sky.recovery.DTOs.ResponsesDTO.Ayda;
using sky.recovery.DTOs.RequestDTO.Insurance;
using sky.recovery.DTOs.ResponsesDTO.Asuransi;
using sky.recovery.Insfrastructures.Scafolding.SkyColl.Recovery;
using sky.recovery.Entities;
using sky.recovery.Insfrastructures.Scafolding.SkyCore.Public;
using sky.recovery.DTOs.ResponsesDTO.Restrukture;

namespace sky.recovery.Services
{
    public class AsuransiServices : SkyCoreConfig, IAsuransiServices
    {
        Insfrastructures.Scafolding.SkyColl.Public.SkyCollPublicDBContext _sky = new Insfrastructures.Scafolding.SkyColl.Public.SkyCollPublicDBContext();
        sky.recovery.Insfrastructures.Scafolding.SkyColl.Recovery.SkyCollRecoveryDBContext _skyRecovery= new Insfrastructures.Scafolding.SkyColl.Recovery.SkyCollRecoveryDBContext();
        skycoreContext _skyCoreContext = new skycoreContext();

        private IUserService _User { get; set; }
        private IGeneralParam _GeneralParam { get; set; }
        private readonly IWebHostEnvironment _environment;
        private IRestruktureRepository _postgreRepository { get; set; }
        private IHelperRepository _helperRepository { get; set; }
        private IWorkflowServices _workflowServices { get; set; }
        ModellingGeneralResponsesV2 _DataResponses = new ModellingGeneralResponsesV2();
        AydaHelper _aydahelper = new AydaHelper();
        public AsuransiServices(IWorkflowServices workflowServices, IGeneralParam GeneralParam, IWebHostEnvironment environment, IUserService User, IHelperRepository helperRepository, IRestruktureRepository postgreRepository,
      IOptions<DbContextSettings> appsetting) : base(appsetting)
        {
            _workflowServices = workflowServices;
            _environment = environment;
            _GeneralParam = GeneralParam;
            _User = User;
            _postgreRepository = postgreRepository;
            _helperRepository = helperRepository;

        }

        //GET DETAIL AYDA
        public async Task<(bool? Status, string message, Dictionary<string, List<dynamic>> DataNasabah)> GetDetailAsuransi(GetDetailAsuransiDTO
            Entity)
        {
            var wrap = _DataResponses.Return();
            // var SkyCollConsString = GetSkyCollConsString();

            try
            {

                var Nasabah = await _sky.MasterCustomer.Where(es => es.Id == Entity.CustomerId).Select(
                   es => new 
                   {
                       Nama = es.CuName,
                       Address = es.CuAddress,
                       KTP = es.CuIdnumber,
                       CuCif = es.CuCif,
                       BranchId = es.BranchId,
                       Branch = _sky.Branch.Where(x => x.LbrcId == es.BranchId).Select(es => es.LbrcName).FirstOrDefault(),
                       AccNo = _sky.MasterLoan.Where(es => es.Id == Entity.LoanId).Select(es => es.AccNo).FirstOrDefault()

                   }
                   ).ToListAsync<dynamic>();

                var Files = await _skyRecovery.Masterrepository.
                    Where(es => es.Requestid == Entity.AsuransiId && es.Isactive == 1).Select(
                   es => new 
                   {
                       Id = es.Id,
                       url = es.Fileurl,
                       urlname = es.Filename,
                       uploaddated = es.Uploaddated.ToString(),
                       doctype = es.Doctype

                   }
                   ).ToListAsync<dynamic>();

                var DataLoan = await _sky.MasterLoan.Where(es => es.Id == Entity.LoanId)
                    .Select(es => new 
                {
                    loanid = es.Id,
                    Fasilitas = es.Fasilitas,
                    Tenor = es.Tenor.ToString(),
                    LoanType = _sky.Rfproduct.Where(x => x.PrdId == es.Product).Select(es => es.PrdDesc).FirstOrDefault(),
                    Plafond = es.Plafond.ToString()
                }).ToListAsync<dynamic>();

                var DataAsuransi =await  _skyRecovery.Insurance.Where(es => es.Id == Entity.AsuransiId)
                    .Select(es => new 
                {
                    createddated=es.Createddated.ToString(),
                    nopk=es.Nopk,
                    nopolis=es.Nopolis,
                    NamaPejabat=es.Namapejabat,
                    Jabatan=es.Jabatan,
                    NoSertifikat=es.Nosertifikat,
                    TglSertifikat=es.Tglsertifikat,
                    nilaiklaim=es.Nilaiklaim,
                    nilaitunggakanbunga=es.Nilaitunggakanbunga,
                    nilaiklaimdibayar=es.Nilaiklaimdibayar,
                    nilaitunggakanpokok=es.Nilaitunggakanpokok,
                    asuransisisaklaimid=es.Asuransisisaklaimid,
                    bakidebitklaim=es.Bakidebitklaim,
                    catatanklaim=es.Catatanklaim,
                    permasalahan=es.Permasalahan,
                    catatanpolis=es.Catatanpolis,
                    keterangan=es.Keterangan,
                    tglpolis=es.Tglpolis
                
                }).ToListAsync<dynamic>();




                var Collection = new Dictionary<string, List<dynamic>>();

                Collection["DataNasabah"] = Nasabah;
                Collection["DataFiles"] = Files;
                Collection["DataLoan"] = DataLoan;
                Collection["DataAsuransi"] = DataAsuransi;

 
                return (true, "OK", Collection);

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


        public async Task<(bool? Status, GeneralResponsesV2 Returns)> AsuransiSubmit(string userid, CreateAsuransiDTO Entity)
        {
            var wrap = _DataResponses.Return();
            var ListData = new List<dynamic>();
            var getCallBy = await _User.GetDataUser(userid);

            // var SkyCollConsString = GetSkyCollConsString();

            try
            {


                //var GetAydaExisting = await ayda.Where(es => es.id == Entity.Data.aydaid).AnyAsync();
                if (Entity.Data.AsuransiId!= null)// update draft
                {
                    var GetData = await _skyRecovery.Insurance.Where(es => es.Id== Entity.Data.AsuransiId && es.Loanid== Entity.DataNasabahLoan.loanid).FirstOrDefaultAsync();
                    if (_aydahelper.IsRequested(GetData.Statusid) == true)
                    {
                        wrap.Status = false;
                        wrap.Message = "Data tidak bisa diupdate, karena sudah masuk proses approval";
                    }

                    GetData.Loanid= Entity.DataNasabahLoan.loanid;
                    GetData.Namapejabat= Entity.Data.namapejabat;
                    GetData.Jabatan= Entity.Data.pejabat;
                    GetData.Nosertifikat= Entity.Data.nosertifikat;
                    GetData.Tglsertifikat = Entity.Data.tglsertifikat;
                    GetData.Nopk= Entity.Data.nopk;
                    GetData.Nopolis = Entity.Data.nopolis;
                    GetData.Tglpolis = Entity.Data.tglpolis;
                    GetData.Nilaiklaim = Entity.Data.nilaiklaim;
                    GetData.Nilaiklaimdibayar = Entity.Data.nilaiklaimdibayar;
                    GetData.Nilaitunggakanbunga = Entity.Data.nilaitunggakanbunga;
                    GetData.Nilaitunggakanpokok = Entity.Data.nilaitunggakanpokok;
                    GetData.Bakidebitklaim = Entity.Data.bakidebitklaim;
                    GetData.Catatanklaim = Entity.Data.catatanklaim;
                    GetData.Catatanpolis = Entity.Data.catatanpolis;
                    GetData.Keterangan = Entity.Data.keterangan;
                    GetData.Permasalahan = Entity.Data.permasalahan;

                    GetData.Statusid= status.Where(es => es.sts_name == "REVIEW").Select(es => es.sts_id).FirstOrDefault();
                    GetData.Createdby = getCallBy.Returns.Data.FirstOrDefault().iduser;
                    GetData.Lastupdateddated = DateTime.Now;
                    GetData.Isactive= 1;
                    Entry(GetData).State = EntityState.Modified;
                    await SaveChangesAsync();



                    var GetIdAyda = await _skyRecovery.Generalparamdetail.Where(es => es.Title== "Insurance").Select(es => es.Id).FirstOrDefaultAsync();
                    var SubmitWorkflow = await WorkflowSubmit(Entity.Data.AsuransiId, (int?)GetIdAyda, userid);

                }
                else
                {

                    sky.recovery.Insfrastructures.Scafolding.SkyColl.Recovery.Insurance Data = new sky.recovery.Insfrastructures.Scafolding.SkyColl.Recovery.Insurance()
                    {


                        Loanid = Entity.DataNasabahLoan.loanid,
                        Namapejabat = Entity.Data.namapejabat,
                        Jabatan = Entity.Data.pejabat,
                        Nosertifikat = Entity.Data.nosertifikat,
                        Tglsertifikat = Entity.Data.tglsertifikat,
                        Nopk = Entity.Data.nopk,
                        Nopolis = Entity.Data.nopolis,
                        Tglpolis = Entity.Data.tglpolis,
                        Nilaiklaim = Entity.Data.nilaiklaim,
                        Nilaiklaimdibayar = Entity.Data.nilaiklaimdibayar,
                        Nilaitunggakanbunga = Entity.Data.nilaitunggakanbunga,
                        Nilaitunggakanpokok = Entity.Data.nilaitunggakanpokok,
                        Bakidebitklaim= Entity.Data.bakidebitklaim,
                        Catatanklaim = Entity.Data.catatanklaim,
                        Catatanpolis = Entity.Data.catatanpolis,
                        Keterangan = Entity.Data.keterangan,
                        Permasalahan = Entity.Data.permasalahan,
                        Isactive = 1,
                        Createdby = getCallBy.Returns.Data.FirstOrDefault().iduser,
                        Createddated= DateTime.Now
                    };
                    await _skyRecovery.Insurance.AddAsync(Data);

                    await SaveChangesAsync();



                    var GetIdAyda = await _skyRecovery.Generalparamdetail.Where(es => es.Title== "Insurance").Select(es => es.Id).FirstOrDefaultAsync();
                    var SubmitWorkflow = await WorkflowSubmit(Entity.Data.AsuransiId, (int?)GetIdAyda, userid);

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

        public async Task<(bool? Status, GeneralResponsesV2 Returns)> AsuransiDraft(string userid, CreateAsuransiDTO Entity)
        {
            var wrap = _DataResponses.Return();
            var ListData = new List<dynamic>();
            var getCallBy = await _User.GetDataUser(userid);

            // var SkyCollConsString = GetSkyCollConsString();

            try
            {

                //var GetAydaExisting = await ayda.Where(es => es.id == Entity.Data.aydaid).AnyAsync();
                if (Entity.Data.AsuransiId != null)// update draft
                {
                    var GetData = await _skyRecovery.Insurance.Where(es => es.Id == Entity.Data.AsuransiId &&
                    es.Loanid == Entity.DataNasabahLoan.loanid).FirstOrDefaultAsync();
                    if (_aydahelper.IsRequested(GetData.Statusid) == true)
                    {
                        wrap.Status = false;
                        wrap.Message = "Data tidak bisa diupdate, karena sudah masuk proses approval";
                    }

                    GetData.Loanid = Entity.DataNasabahLoan.loanid;
                        GetData.Namapejabat = Entity.Data.namapejabat;
                    GetData.Jabatan = Entity.Data.pejabat;
                    GetData.Nosertifikat = Entity.Data.nosertifikat;
                    GetData.Tglsertifikat = Entity.Data.tglsertifikat;
                    GetData.Nopk = Entity.Data.nopk;
                    GetData.Nopolis = Entity.Data.nopolis;
                    GetData.Tglpolis = Entity.Data.tglpolis;
                    GetData.Nilaiklaim = Entity.Data.nilaiklaim;
                    GetData.Nilaiklaimdibayar = Entity.Data.nilaiklaimdibayar;
                    GetData.Nilaitunggakanbunga = Entity.Data.nilaitunggakanbunga;
                    GetData.Nilaitunggakanpokok = Entity.Data.nilaitunggakanpokok;
                    GetData.Bakidebitklaim = Entity.Data.bakidebitklaim;
                    GetData.Catatanklaim = Entity.Data.catatanklaim;
                    GetData.Catatanpolis = Entity.Data.catatanpolis;
                    GetData.Keterangan = Entity.Data.keterangan;
                    GetData.Permasalahan = Entity.Data.permasalahan;

                    GetData.Statusid= status.Where(es => es.sts_name == "DRAFT").Select(es => es.sts_id).FirstOrDefault();
                    GetData.Createdby = getCallBy.Returns.Data.FirstOrDefault().iduser;
                    GetData.Lastupdateddated = DateTime.Now;
                    GetData.Isactive = 1;
                    Entry(GetData).State = EntityState.Modified;
                    await SaveChangesAsync();


                }
                else
                {
                     Insfrastructures.Scafolding.SkyColl.Recovery.Insurance Data = new Insfrastructures.Scafolding.SkyColl.Recovery.Insurance()
                    {

                   
                        Loanid= Entity.DataNasabahLoan.loanid,
                        Namapejabat = Entity.Data.namapejabat,
                        Jabatan= Entity.Data.pejabat,
                        Nosertifikat = Entity.Data.nosertifikat,
                        Tglsertifikat = Entity.Data.tglsertifikat,
                        Nopk= Entity.Data.nopk,
                        Nopolis = Entity.Data.nopolis,
                        Tglpolis = Entity.Data.tglpolis,
                        Nilaiklaim = Entity.Data.nilaiklaim,
                        Nilaiklaimdibayar = Entity.Data.nilaiklaimdibayar,
                        Nilaitunggakanbunga= Entity.Data.nilaitunggakanbunga,
                        Nilaitunggakanpokok= Entity.Data.nilaitunggakanpokok,
                        Bakidebitklaim= Entity.Data.bakidebitklaim,
                        Catatanklaim = Entity.Data.catatanklaim,
                        Catatanpolis= Entity.Data.catatanpolis,
                        Keterangan=Entity.Data.keterangan,
                        Permasalahan=Entity.Data.permasalahan,
                        Isactive=1,
                        Createdby=getCallBy.Returns.Data.FirstOrDefault().iduser,
                        Createddated=DateTime.Now,
                        Statusid= status.Where(es => es.sts_name == "DRAFT").Select(es => es.sts_id).FirstOrDefault()

                };
                    await  _skyRecovery.Insurance.AddAsync(Data);

                    await SaveChangesAsync();


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


        public async Task<(bool? Status, GeneralResponsesV2 Returns)> InsuranceTaskList(string UserId)
        {
            var wrap = _DataResponses.Return();
            var ReturnData = new List<dynamic?>();
            // var SkyCollConsString = GetSkyCollConsString();

            try
            {
               

                var getCallBy = await _User.GetDataUser(UserId);
                

                var MonitoringData = await _skyRecovery.Workflow.Where(es => es.Fiturid == 16 &&
 es.Actor == getCallBy.Returns.Data.FirstOrDefault().iduser
 && es.Status == 11).Select(es => es.Requestid).ToListAsync();

               

                     ReturnData = await insurance.Include(i => i.master_loan).
                    Where(es => MonitoringData.Contains(es.Id)).Select(
                    es => new
                    {
                        asuransiid = es.Id,
                        customerid = es.master_loan.customer_id,
                        branch = es.master_loan.master_customer.branch.lbrc_name,
                        noaccount = es.master_loan.acc_no,
                        cif = es.master_loan.cu_cif,
                        nama = es.master_loan.master_customer.cu_name,
                        loanid = es.master_loan.id,
                        status = es.status.sts_name,
                        createddated = es.createddated,
                        createdby = es.createdby,
                        FiturId=16
                    }
                    ).ToListAsync<dynamic>();
                
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

        public async Task<(bool? Status, GeneralResponsesV2 Returns)> InsuranceMonitoring(string UserId)
        {
            var wrap = _DataResponses.Return();
            // var SkyCollConsString = GetSkyCollConsString();

            try
            {  
                

                var getCallBy = await _User.GetDataUser(UserId);
                
                var ReturnData = await insurance.Include(i => i.master_loan)
                    .Where(es => es.createdby == getCallBy.Returns.Data.FirstOrDefault().iduser)
                    .Select(
                    es => new 
                    {
                        asuransiid=es.Id,
                        customerid=es.master_loan.customer_id,
                        branch = es.master_loan.master_customer.branch.lbrc_name,
                        noaccount = es.master_loan.acc_no,
                        cif = es.master_loan.cu_cif,
                        nama = es.master_loan.master_customer.cu_name,
                        loanid = es.master_loan.id,
                        status = es.status.sts_name,
                        createddated = es.createddated,
                        createdby = es.createdby

                    }
                    ).ToListAsync<dynamic>();
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

        public async Task<(bool? Status, GeneralResponsesV2 Returns)> SetIsActive(int Id, int status)
        {
            var wrap = _DataResponses.Return();
            var ListData = new List<dynamic>();
            //var getCallBy = await _User.GetDataUser(Entity.User.UserId);

            // var SkyCollConsString = GetSkyCollConsString();

            try
            {
                var GetData = await insurance.Where(es => es.Id == Id).FirstOrDefaultAsync();
                GetData.isactive = status;
                GetData.lastupdateddated= DateTime.Now;
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
