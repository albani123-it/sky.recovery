using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using sky.recovery.Helper.Config;
using sky.recovery.Helper.Enum;
using sky.recovery.Insfrastructures;
using sky.recovery.Interfaces;
using sky.recovery.Responses;
using sky.recovery.Services.DBConfig;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using sky.recovery.Entities;
using sky.recovery.DTOs.ResponsesDTO.Ayda;
using System.Collections.Generic;
using sky.recovery.DTOs.RequestDTO.Ayda;
using sky.recovery.DTOs.WorkflowDTO;
using sky.recovery.Insfrastructures.Scafolding.SkyColl.Public;
using sky.recovery.DTOs.RequestDTO.CommonDTO;
using sky.recovery.DTOs.ResponsesDTO.Restrukture;
using System.Reflection.Metadata;
using sky.recovery.Insfrastructures.Scafolding.SkyColl.Recovery;
using sky.recovery.Insfrastructures.Scafolding.SkyCore.Public;

namespace sky.recovery.Services
{
    public class AydaServices : SkyCoreConfig, IAydaServices
    {
        sky.recovery.Insfrastructures.Scafolding.SkyColl.Public.SkyCollPublicDBContext _sky = new Insfrastructures.Scafolding.SkyColl.Public.SkyCollPublicDBContext();
        sky.recovery.Insfrastructures.Scafolding.SkyColl.Recovery.SkyCollRecoveryDBContext _skyRecovery= new Insfrastructures.Scafolding.SkyColl.Recovery.SkyCollRecoveryDBContext();
        skycoreContext _skyCoreContext = new skycoreContext();
        private IDocServices _DocService { get; set; }
        private IUserService _User { get; set; }
        private IGeneralParam _GeneralParam { get; set; }
        private readonly IWebHostEnvironment _environment;
        private IRestruktureRepository _postgreRepository { get; set; }
        private IHelperRepository _helperRepository { get; set; }
        private IWorkflowServices _workflowServices { get; set; }
        ModellingGeneralResponsesV2 _DataResponses = new ModellingGeneralResponsesV2();
        AydaHelper _aydahelper = new AydaHelper();
        public AydaServices(IDocServices DocService, IWorkflowServices workflowServices, IGeneralParam GeneralParam, IWebHostEnvironment environment, IUserService User, IHelperRepository helperRepository, IRestruktureRepository postgreRepository,
      IOptions<DbContextSettings> appsetting) : base(appsetting)
        {
            _DocService = DocService;
            _workflowServices = workflowServices;
            _environment = environment;
            _GeneralParam = GeneralParam;
            _User = User;
            _postgreRepository = postgreRepository;
            _helperRepository = helperRepository;

        }


        public async Task<(bool? Status, GeneralResponsesV2 Returns)> GetMasterLoan()
        {
            var wrap = _DataResponses.Return();
            var ListData = new List<dynamic>();
            //var getCallBy = await _User.GetDataUser(Entity.User.UserId);

            // var SkyCollConsString = GetSkyCollConsString();

            try
            {
                var getdata = await _sky.MasterLoan.AsNoTracking().Where(es => es.Dpd > 90).ToListAsync();
                ListData.Add(getdata);
                wrap.Status = true;
                wrap.Message = "OK";
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
        public async Task<(bool? Status, GeneralResponsesV2 Returns)> SetIsActive(int Id, int status)
        {
            var wrap = _DataResponses.Return();
            var ListData = new List<dynamic>();
            //var getCallBy = await _User.GetDataUser(Entity.User.UserId);

            // var SkyCollConsString = GetSkyCollConsString();

            try
            {
                var GetData = await ayda.Where(es => es.id == Id).FirstOrDefaultAsync();
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

        public async Task<(string message,bool? status)> WorkflowSubmit(int? idrequest,int? idfitur, string userid)
        {
            try
            {
                var Data = new SubmitWorkflowDTO()
                {
                    idfitur=idfitur,
                    idrequest=idrequest,
                    userid=userid
                };
                var SubmitWorkflow = await _workflowServices.SubmitWorkflowStep(Data);
                return (SubmitWorkflow.Returns.Message, SubmitWorkflow.Status);
            }
            catch(Exception ex)
            {
                return (ex.Message, false);
            }
        }
                public async Task<(bool? Status, GeneralResponsesV2 Returns)> AydaSubmit(string userid,CreateAydaDTO Entity)
        {
            var wrap = _DataResponses.Return();
            var ListData = new List<dynamic>();
            var getCallBy = await _User.GetDataUser(userid);

            // var SkyCollConsString = GetSkyCollConsString();

            try
            {

               
                //var GetAydaExisting = await ayda.Where(es => es.id == Entity.Data.aydaid).AnyAsync();
                if (Entity.Data.aydaid != null)// update draft
                {
                    var GetData = await _skyRecovery.Ayda.Where(es => es.Id== Entity.Data.aydaid && es.Loanid == Entity.DataNasabahLoan.loanid).FirstOrDefaultAsync();
                    if(_aydahelper.IsRequested(GetData.Statusid)==true )
                    {
                        wrap.Status = false;
                        wrap.Message = "Data tidak bisa diupdate, karena sudah masuk proses approval";
                    }
                    
                    GetData.Loanid= Entity.DataNasabahLoan.loanid;
                    GetData.Mstbranchid = Entity.DataNasabahLoan.BranchId;
                    GetData.Hubunganbankid = Entity.Data.bankid;
                    GetData.Tglambilalih = Entity.Data.tglambilalih;
                    GetData.Kualitas= Entity.Data.kualitas;
                    GetData.Nilaipembiayaanpokok = Entity.Data.nilaipembiayaanpokok;
                    GetData.Nilaimargin= Entity.Data.nilaimargin;
                    GetData.Nilaiperolehanagunan = Entity.Data.nilaiperolehanagunan;
                    GetData.Perkiraanbiayajual = Entity.Data.perkiraanbiayajual;
                    GetData.Ppa= Entity.Data.ppa;
                    GetData.Jumlahayda = Entity.Data.jumlahayda;
                    GetData.Statusid=_sky.Status.Where(es => es.StsName == "REVIEW").Select(es => es.StsId).FirstOrDefault();
                    GetData.Createdby = getCallBy.Returns.Data.FirstOrDefault().iduser;
                    GetData.Lastupdatedate = DateTime.Now;
                    GetData.Isactive= 1;
                    Entry(GetData).State = EntityState.Modified;
                    await SaveChangesAsync();
                   
                     
                    var GetIdAyda = await _skyRecovery.Generalparamdetail.Where(es => es.Title == "Ayda").Select(es => es.Id).FirstOrDefaultAsync();
                    var SubmitWorkflow = await WorkflowSubmit(Entity.Data.aydaid,(int?)GetIdAyda,userid);

                }
                else
                {
                    var Data = new Insfrastructures.Scafolding.SkyColl.Recovery.Ayda()
                    {
                        Loanid = Entity.DataNasabahLoan.loanid,
                        Mstbranchid = Entity.DataNasabahLoan.BranchId,
                        Hubunganbankid = Entity.Data.bankid,
                        Tglambilalih = Entity.Data.tglambilalih,
                        Kualitas = Entity.Data.kualitas,
                        Nilaipembiayaanpokok = Entity.Data.nilaipembiayaanpokok,
                        Nilaimargin = Entity.Data.nilaimargin,
                        Isactive = 1,
                        Nilaiperolehanagunan = Entity.Data.nilaiperolehanagunan,
                        Perkiraanbiayajual = Entity.Data.perkiraanbiayajual,
                        Ppa= Entity.Data.ppa,
                        Jumlahayda = Entity.Data.jumlahayda,
                        Statusid= _sky.Status.Where(es => es.StsName == "REVIEW").Select(es => es.StsId).FirstOrDefault(),
                        Createdby = getCallBy.Returns.Data.FirstOrDefault().iduser,
                        Createddated= DateTime.Now,
                        
                    };
                    await _skyRecovery.Ayda.AddAsync(Data);
                    await SaveChangesAsync();

                    var GetIdAyda = await _skyRecovery.Generalparamdetail.Where(es => es.Title == "Ayda").Select(es => es.Id).FirstOrDefaultAsync();
                    var SubmitWorkflow = await WorkflowSubmit(Entity.Data.aydaid,(int?) GetIdAyda, userid);

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

       

        public async Task<(bool? Status, GeneralResponsesV2 Returns)> AydaDraft(string userid,CreateAydaDTO Entity)
        {
            var wrap = _DataResponses.Return();
            var ListData = new List<dynamic>();
            var getCallBy = await _User.GetDataUser(userid);

            // var SkyCollConsString = GetSkyCollConsString();

            try
            {

                //var GetAydaExisting = await ayda.Where(es => es.id == Entity.Data.aydaid).AnyAsync();
                if(Entity.Data.aydaid!=null)// update draft
                {
                    var GetData = await _skyRecovery.Ayda.Where(es => es.Id == Entity.Data.aydaid && es.Loanid== Entity.DataNasabahLoan.loanid).FirstOrDefaultAsync();
                    if (_aydahelper.IsRequested(GetData.Statusid)==true)
                    {
                        wrap.Status = false;
                        wrap.Message = "Data tidak bisa diupdate, karena sudah masuk proses approval";
                    }
                    GetData.Loanid = Entity.DataNasabahLoan.loanid;
                    GetData.Mstbranchid = Entity.DataNasabahLoan.BranchId;
                    GetData.Hubunganbankid = Entity.Data.bankid;
                    GetData.Tglambilalih= Entity.Data.tglambilalih;
                    GetData.Kualitas= Entity.Data.kualitas;
                    GetData.Nilaipembiayaanpokok = Entity.Data.nilaipembiayaanpokok;
                    GetData.Nilaimargin= Entity.Data.nilaimargin;
                    GetData.Nilaiperolehanagunan = Entity.Data.nilaiperolehanagunan;
                    GetData.Perkiraanbiayajual = Entity.Data.perkiraanbiayajual;
                    GetData.Ppa= Entity.Data.ppa;
                    GetData.Jumlahayda = Entity.Data.jumlahayda;
                    GetData.Statusid= _sky.Status.Where(es => es.StsName== "DRAFT").Select(es => es.StsId).FirstOrDefault();
                    GetData.Createdby = getCallBy.Returns.Data.FirstOrDefault().iduser;
                    GetData.Lastupdatedate = DateTime.Now;
                    GetData.Isactive= 1;
                    Entry(GetData).State = EntityState.Modified;
                    await SaveChangesAsync();


                }
                else
                {
                    var Data = new Insfrastructures.Scafolding.SkyColl.Recovery.Ayda()
                    {
                        Loanid = Entity.DataNasabahLoan.loanid,
                        Mstbranchid = Entity.DataNasabahLoan.BranchId,
                        Hubunganbankid = Entity.Data.bankid,
                        Tglambilalih = Entity.Data.tglambilalih,
                        Kualitas= Entity.Data.kualitas,
                        Nilaipembiayaanpokok = Entity.Data.nilaipembiayaanpokok,
                        Nilaimargin= Entity.Data.nilaimargin,
                        Nilaiperolehanagunan = Entity.Data.nilaiperolehanagunan,
                        Perkiraanbiayajual = Entity.Data.perkiraanbiayajual,
                        Ppa= Entity.Data.ppa,
                        Jumlahayda= Entity.Data.jumlahayda,
                        Statusid= status.Where(es => es.sts_name == "DRAFT").Select(es => es.sts_id).FirstOrDefault(),
                        Createdby = getCallBy.Returns.Data.FirstOrDefault().iduser,
                        Createddated= DateTime.Now,
                        Isactive=1
                    };
                    await _skyRecovery.Ayda.AddAsync(Data);
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

        //SERVICE YANG DIPAKAI
        //MONITORING RESTRUKTUR V2
        public async Task<(bool? Status, GeneralResponsesV2 Returns)> InsertBulk(int banyak)
        {
            var wrap = _DataResponses.Return();

            try
            {
                for (var x = 0; x < banyak; x++)
                {
                    var Data = new restructure()
                    {
                        loanid = 1175,
                        statusid = 4,
                        mstbranchid = 129,
                        createdby = 354
                    };

                    await restructure.AddAsync(Data);
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
        public async Task<(bool? Status, GeneralResponsesV2 Returns)> DummyNasabah(int pagenumber,int pagesieze)
        {
            var wrap = _DataResponses.Return();
            var ListData = new List<dynamic>();
            // var SkyCollConsString = GetSkyCollConsString();

            try
            {

                var Data = await master_loan.AsNoTracking()
                    .Where(es => es.dpd > 10).OrderBy(es => es.id)
                    .Skip((pagenumber - 1) * pagesieze).Take(pagesieze)
                    .ToListAsync();

                wrap.Status = true;
                wrap.Message = "OK";
                ListData.Add(Data);
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

        public async Task<(bool? Status, GeneralResponsesV2 Returns)> AydaTaskList(string UserId)
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

               // var getCallBy = await _User.GetDataUser(UserId);
                // pindah ke dinamis
                //if (getCallBy.Returns.Data.FirstOrDefault().role != RestrukturRole.Operator.ToString())
                //{
                //     wrap.Status  = false;
                //    wrap.Message = "Not Authorize";
                //    return ( wrap.Status , wrap);
                //}
                var ReturnData = await ayda.Include(i => i.master_loan).Where(es => es.statusid==11).Select(
                    es => new MonitoringBean
                    {
                        LoanId=es.loanid,
                        customerid=es.master_loan.customer_id,
                        cabang = es.master_loan.master_customer.branch.lbrc_name,
                        noloan = es.master_loan.acc_no,
                        namanasabah = es.master_loan.master_customer.cu_name,
                        totaltunggakan = es.master_loan.tunggakan_total,
                        jenisjaminan = es.master_loan.master_collateral.col_type,
                        alamatjaminan = es.master_loan.master_collateral.col_address,
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
        public async Task<(bool? Status, GeneralResponsesV2 Returns)> AydaMonitoring(string UserId)
        {
            var wrap = _DataResponses.Return();
            var ListData = new List<dynamic>();
           // var SkyCollConsString = GetSkyCollConsString();

            try
            {
                if(String.IsNullOrEmpty(UserId))
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
                var ReturnData =await  ayda.Include(i => i.master_loan).Where(es => es.createdby == getCallBy.Returns.Data.FirstOrDefault().iduser).Select(
                    es => new MonitoringBean
                    {
                        LoanId = es.loanid,
                        customerid = es.master_loan.customer_id,
                        cabang =es.master_loan.master_customer.branch.lbrc_name,
                        noloan=es.master_loan.acc_no,
                        namanasabah=es.master_loan.master_customer.cu_name,
                        totaltunggakan=es.master_loan.tunggakan_total,
                        jenisjaminan=es.master_loan.master_collateral.col_type,
                        alamatjaminan=es.master_loan.master_collateral.col_address,
                        status=es.status.sts_name

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
        public async Task<(bool? Status,string message, Dictionary<string, List<dynamic>> DataNasabah)> GetDetailAyda(GetDetailAydaDTO Entity)
        {
            var wrap = _DataResponses.Return();
            var ListData = new List<dynamic>();
            // var SkyCollConsString = GetSkyCollConsString();

            try
            {

                var  Nasabah = await _sky.MasterCustomer.Where(es => es.Id == Entity.CustomerId).Select(
                   es => new NasabahAydaDTO
                   {
                      Nama=es.CuName,
                      Address=es.CuAddress,
                      KTP=es.CuIdnumber,
                      CuCif=es.CuCif,
                      BranchId=es.BranchId,
                      Branch=_sky.Branch.Where(x=>x.LbrcId==es.BranchId).Select(es=>es.LbrcName).FirstOrDefault(),
                      AccNo=_sky.MasterLoan.Where(es=>es.Id==Entity.LoanId).Select(es=>es.AccNo).FirstOrDefault()

                   }
                   ).ToListAsync();

                var Files = await _skyRecovery.Masterrepository.Where(es => es.Requestid == Entity.AydaId && es.Isactive==1).Select(
                   es => new RepoAydaDTO
                   {
                      Id=es.Id,
                      url=es.Fileurl,
                      urlname=es.Filename,
                      uploaddated=es.Uploaddated.ToString(),
                      doctype=es.Doctype

                   }
                   ).ToListAsync();

                var DataLoan = await _sky.MasterLoan.Where(es => es.Id == Entity.LoanId).Select(es => new LoanAydaDTO
                {
                    loanid=es.Id,
                    Fasilitas=es.Fasilitas,
                    Tenor=es.Tenor.ToString(),
                    LoanType=_sky.Rfproduct.Where(x=>x.PrdId==es.Product).Select(es=>es.PrdDesc).FirstOrDefault(),
                    Plafond=es.Plafond.ToString()
                }).ToListAsync();

                var DataCollateral = _skyRecovery.Ayda.Where(es => es.Id== Entity.AydaId).AsEnumerable()
                    .Select(es => new JaminanAyda_2
                {
                    bankid = es.Hubunganbankid,
                    status = _sky.Status.Where(x => x.StsId == es.Statusid).Select(es => es.StsName).FirstOrDefault(),
                    jumlahayda = es.Jumlahayda.ToString(),
                    kualitas = es.Kualitas,
                    nilaimargin = es.Nilaimargin.ToString(),
                    nilaipembiayaanpokok = es.Nilaipembiayaanpokok.ToString(),
                    nilaiperolehanagunan = es.Nilaiperolehanagunan.ToString(),
                    perkiraanbiayajual = es.Perkiraanbiayajual.ToString(),
                    ppa = es.Ppa.ToString(),
                    tglambilalih = es.Tglambilalih
              
                }).ToList();

                var DataCreated = _skyRecovery.Ayda.Where(es => es.Id == Entity.AydaId).AsEnumerable()
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
                Collection["DataCollateral"] = new List<dynamic>();
                Collection["CreatedInformation"] = new List<dynamic>();


                Collection["DataNasabah"].Add(Nasabah);
                Collection["DataFiles"].Add(Files);
                Collection["DataLoan"].Add(DataLoan);
                Collection["DataCollateral"].Add(DataCollateral);
                Collection["CreatedInformation"].Add(DataCreated);



                return (true,"OK", Collection);

            }
            catch (Exception ex)
            {
              

                return (false,ex.Message, null);
            }
        }

    }
}
