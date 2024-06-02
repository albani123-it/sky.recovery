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

namespace sky.recovery.Services
{
    public class AydaServices : SkyCoreConfig, IAydaServices
    {
        skycollContext _sky = new skycollContext();
        Insfrastructures.Scafolding.SkyColl.Recovery.skycollContext _skyRecovery = new Insfrastructures.Scafolding.SkyColl.Recovery.skycollContext();

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
                var getdata = await _sky.MasterLoans.AsNoTracking().Where(es => es.Dpd > 90).ToListAsync();
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
                public async Task<(bool? Status, GeneralResponsesV2 Returns)> AydaSubmit(CreateAydaDTO Entity)
        {
            var wrap = _DataResponses.Return();
            var ListData = new List<dynamic>();
            var getCallBy = await _User.GetDataUser(Entity.User.UserId);

            // var SkyCollConsString = GetSkyCollConsString();

            try
            {

               
                //var GetAydaExisting = await ayda.Where(es => es.id == Entity.Data.aydaid).AnyAsync();
                if (Entity.Data.aydaid != null)// update draft
                {
                    var GetData = await ayda.Where(es => es.id == Entity.Data.aydaid && es.loanid == Entity.DataNasabahLoan.loanid).FirstOrDefaultAsync();
                    if(_aydahelper.IsDraft(GetData.statusid)==true )
                    {
                        wrap.Status = false;
                        wrap.Message = "Data tidak bisa diupdate, karena sudah masuk proses approval";
                    }
                    
                    GetData.loanid = Entity.DataNasabahLoan.loanid;
                    GetData.mstbranchid = Entity.DataNasabahLoan.BranchId;
                    GetData.hubunganbankid = Entity.Data.bankid;
                    GetData.tglambilalih = Entity.Data.tglambilalih;
                    GetData.kualitas = Entity.Data.kualitas;
                    GetData.nilaipembiayaanpokok = Entity.Data.nilaipembiayaanpokok;
                    GetData.nilaimargin = Entity.Data.nilaimargin;
                    GetData.nilaiperolehanagunan = Entity.Data.nilaiperolehanagunan;
                    GetData.perkiraanbiayajual = Entity.Data.perkiraanbiayajual;
                    GetData.ppa = Entity.Data.ppa;
                    GetData.jumlahayda = Entity.Data.jumlahayda;
                    GetData.statusid = status.Where(es => es.sts_name == "REVIEW").Select(es => es.sts_id).FirstOrDefault();
                    GetData.createdby = getCallBy.Returns.Data.FirstOrDefault().iduser;
                    GetData.lastupdatedate = DateTime.Now;
                    GetData.isactive = 1;
                    Entry(GetData).State = EntityState.Modified;
                    await SaveChangesAsync();
                   
                     
                    var GetIdAyda = await generalparamdetail.Where(es => es.title == "Ayda").Select(es => es.Id).FirstOrDefaultAsync();
                    var SubmitWorkflow = await WorkflowSubmit(Entity.Data.aydaid,GetIdAyda,Entity.User.UserId);

                }
                else
                {
                    var Data = new ayda()
                    {
                        loanid = Entity.DataNasabahLoan.loanid,
                        mstbranchid = Entity.DataNasabahLoan.BranchId,
                        hubunganbankid = Entity.Data.bankid,
                        tglambilalih = Entity.Data.tglambilalih,
                        kualitas = Entity.Data.kualitas,
                        nilaipembiayaanpokok = Entity.Data.nilaipembiayaanpokok,
                        nilaimargin = Entity.Data.nilaimargin,
                        isactive = 1,
                        nilaiperolehanagunan = Entity.Data.nilaiperolehanagunan,
                        perkiraanbiayajual = Entity.Data.perkiraanbiayajual,
                        ppa = Entity.Data.ppa,
                        jumlahayda = Entity.Data.jumlahayda,
                        statusid = status.Where(es => es.sts_name == "REVIEW").Select(es => es.sts_id).FirstOrDefault(),
                        createdby = getCallBy.Returns.Data.FirstOrDefault().iduser,
                        createddated = DateTime.Now,
                        
                    };
                    await ayda.AddAsync(Data);
                    await SaveChangesAsync();

                    var GetIdAyda = await generalparamdetail.Where(es => es.title == "Ayda").Select(es => es.Id).FirstOrDefaultAsync();
                    var SubmitWorkflow = await WorkflowSubmit(Entity.Data.aydaid, Data.id, Entity.User.UserId);

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

       

        public async Task<(bool? Status, GeneralResponsesV2 Returns)> AydaDraft(CreateAydaDTO Entity)
        {
            var wrap = _DataResponses.Return();
            var ListData = new List<dynamic>();
            var getCallBy = await _User.GetDataUser(Entity.User.UserId);

            // var SkyCollConsString = GetSkyCollConsString();

            try
            {

                //var GetAydaExisting = await ayda.Where(es => es.id == Entity.Data.aydaid).AnyAsync();
                if(Entity.Data.aydaid!=null)// update draft
                {
                    var GetData = await ayda.Where(es => es.id == Entity.Data.aydaid && es.loanid == Entity.DataNasabahLoan.loanid).FirstOrDefaultAsync();
                    if (_aydahelper.IsRequested(GetData.statusid)==true)
                    {
                        wrap.Status = false;
                        wrap.Message = "Data tidak bisa diupdate, karena sudah masuk proses approval";
                    }
                    GetData.loanid = Entity.DataNasabahLoan.loanid;
                    GetData.mstbranchid = Entity.DataNasabahLoan.BranchId;
                    GetData.hubunganbankid = Entity.Data.bankid;
                    GetData.tglambilalih = Entity.Data.tglambilalih;
                    GetData.kualitas = Entity.Data.kualitas;
                    GetData.nilaipembiayaanpokok = Entity.Data.nilaipembiayaanpokok;
                    GetData.nilaimargin = Entity.Data.nilaimargin;
                    GetData.nilaiperolehanagunan = Entity.Data.nilaiperolehanagunan;
                    GetData.perkiraanbiayajual = Entity.Data.perkiraanbiayajual;
                    GetData.ppa = Entity.Data.ppa;
                    GetData.jumlahayda = Entity.Data.jumlahayda;
                    GetData.statusid = status.Where(es => es.sts_name == "DRAFT").Select(es => es.sts_id).FirstOrDefault();
                    GetData.createdby = getCallBy.Returns.Data.FirstOrDefault().iduser;
                    GetData.lastupdatedate = DateTime.Now;
                    GetData.isactive = 1;
                    Entry(GetData).State = EntityState.Modified;
                    await SaveChangesAsync();


                }
                else
                {
                    var Data = new ayda()
                    {
                        loanid = Entity.DataNasabahLoan.loanid,
                        mstbranchid = Entity.DataNasabahLoan.BranchId,
                        hubunganbankid = Entity.Data.bankid,
                        tglambilalih = Entity.Data.tglambilalih,
                        kualitas = Entity.Data.kualitas,
                        nilaipembiayaanpokok = Entity.Data.nilaipembiayaanpokok,
                        nilaimargin = Entity.Data.nilaimargin,
                        nilaiperolehanagunan = Entity.Data.nilaiperolehanagunan,
                        perkiraanbiayajual = Entity.Data.perkiraanbiayajual,
                        ppa = Entity.Data.ppa,
                        jumlahayda = Entity.Data.jumlahayda,
                        statusid = status.Where(es => es.sts_name == "DRAFT").Select(es => es.sts_id).FirstOrDefault(),
                        createdby = getCallBy.Returns.Data.FirstOrDefault().iduser,
                        createddated = DateTime.Now,
                        isactive=1
                    };
                    await ayda.AddAsync(Data);
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

                var  Nasabah = await _sky.MasterCustomers.Where(es => es.Id == Entity.CustomerId).Select(
                   es => new NasabahAydaDTO
                   {
                      Nama=es.CuName,
                      Address=es.CuAddress,
                      KTP=es.CuIdnumber,
                      CuCif=es.CuCif,
                      BranchId=es.BranchId,
                      Branch=_sky.Branches.Where(x=>x.LbrcId==es.BranchId).Select(es=>es.LbrcName).FirstOrDefault(),
                      AccNo=_sky.MasterLoans.Where(es=>es.Id==Entity.LoanId).Select(es=>es.AccNo).FirstOrDefault()

                   }
                   ).ToListAsync();

                var Files = await _skyRecovery.masterrepository.Where(es => es.Requestid == Entity.AydaId && es.Isactive==1).Select(
                   es => new RepoAydaDTO
                   {
                      Id=es.Id,
                      url=es.Fileurl,
                      urlname=es.Filename,
                      uploaddated=es.Uploaddated.ToString(),
                      doctype=es.Doctype

                   }
                   ).ToListAsync();

                var DataLoan = await _sky.MasterLoans.Where(es => es.Id == Entity.LoanId).Select(es => new LoanAydaDTO
                {
                    loanid=es.Id,
                    Fasilitas=es.Fasilitas,
                    Tenor=es.Tenor.ToString(),
                    LoanType=_sky.Rfproducts.Where(x=>x.PrdId==es.Product).Select(es=>es.PrdDesc).FirstOrDefault(),
                    Plafond=es.Plafond.ToString()
                }).ToListAsync();

                var DataCollateral = await ayda.Where(es => es.id == Entity.AydaId).Select(es => new JaminanAyda_2
                {
                    bankid = es.hubunganbankid,
                    //status = _sky.Statuses.Where(x => x.StsId == es.statusid).Select(es => es.StsName).FirstOrDefault(),
                    jumlahayda = es.jumlahayda.ToString(),
                    kualitas = es.kualitas,
                    nilaimargin = es.nilaimargin.ToString(),
                    nilaipembiayaanpokok = es.nilaipembiayaanpokok.ToString(),
                    nilaiperolehanagunan = es.nilaiperolehanagunan.ToString(),
                    perkiraanbiayajual = es.perkiraanbiayajual.ToString(),
                    ppa = es.ppa.ToString(),
                    tglambilalih = es.tglambilalih
                }).ToListAsync();

                var Collection = new Dictionary<string, List<dynamic>>();

                Collection["DataNasabah"] = new List<dynamic>();
                Collection["DataFiles"] = new List<dynamic>();
                Collection["DataLoan"] = new List<dynamic>();
                Collection["DataCollateral"] = new List<dynamic>();


                Collection["DataNasabah"].Add(Nasabah);
                Collection["DataFiles"].Add(Files);
                Collection["DataLoan"].Add(DataLoan);
                Collection["DataCollateral"].Add(DataCollateral);



                return (true,"OK", Collection);

            }
            catch (Exception ex)
            {
              

                return (false,ex.Message, null);
            }
        }

    }
}
