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
using static Dapper.SqlMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using sky.recovery.Libs;

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

        private readonly IConfiguration _config;


        ModellingGeneralResponsesV2 _DataResponses = new ModellingGeneralResponsesV2();
        AydaHelper _aydahelper = new AydaHelper();
        public AydaServices(IConfiguration config,IDocServices DocService, IWorkflowServices workflowServices, IGeneralParam GeneralParam, IWebHostEnvironment environment, IUserService User, IHelperRepository helperRepository, IRestruktureRepository postgreRepository,
      IOptions<DbContextSettings> appsetting) : base(appsetting)
        {
            _config = config;
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
         
            try
            {


                var getdata = await _sky.MasterLoan.
                    AsNoTracking().Where(es => es.Dpd > Convert.ToInt32(_config["Logic:DPD"]
                    .ToString())).ToListAsync<dynamic>();
                wrap.Status = true;
                wrap.Message = "OK";
                wrap.Data = getdata;
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
                    GetData.Statusid= Convert.ToInt32(_config["WorkflowStatus:Review"].ToString());
                    GetData.Createdby = getCallBy.Returns.Data.FirstOrDefault().iduser;
                    GetData.Lastupdatedate = DateTime.Now;
                    GetData.Isactive= 1;
                   _skyRecovery.Entry(GetData).State = EntityState.Modified;
                    await _skyRecovery.SaveChangesAsync();
                   
                     
                    var GetIdAyda = Convert.ToInt32(_config["Fitur:Recovery:Ayda"].ToString());
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
                        Statusid= Convert.ToInt32(_config["WorkflowStatus:Review"].ToString()),
                        Createdby = getCallBy.Returns.Data.FirstOrDefault().iduser,
                        Createddated= DateTime.Now,
                        
                    };
                    await _skyRecovery.Ayda.AddAsync(Data);
                    await _skyRecovery.SaveChangesAsync();

                    var GetIdAyda = Convert.ToInt32(_config["Fitur:Recovery:Ayda"].ToString());
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


        public async Task<(bool Status, string Message, Dictionary<string, List<dynamic>> DataNasabah)> GetDetailAydaForApproval(int AydaId, int loanid, int CustomerId)
        {
            try
            {


                var Data = from mc in _sky.MasterCustomer
                           join ml in _sky.MasterLoan on mc.Id equals ml.CustomerId
                           join pd in _sky.Rfproduct on ml.Product equals pd.PrdId
                           join ps in _sky.RfproductSegment on ml.PrdSegmentId equals ps.PrdSgmId
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

                var DatasFasilitas = from mc in _sky.MasterCustomer
                                     join ml in _sky.MasterLoan on mc.Id equals ml.CustomerId
                                     join pd in _sky.Rfproduct on ml.Product equals pd.PrdId
                                     join ps in _sky.RfproductSegment on ml.PrdSegmentId equals ps.PrdSgmId
                                     where mc.Id == CustomerId
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



                var Dokumen = await _skyRecovery.Masterrepository.Where(es => es.Requestid == AydaId)
                   .Select(es => new
                   {
                       JenisDokumen = es.Doctype,
                       Url = es.Fileurl,
                       FileName = es.Fileurl,
                       UploadDated = es.Uploaddated,
                       Keterangan = es.Keterangan,
                       Status = _skyRecovery.Generalparamdetail.Where(x => x.Id == es.Status).Select(es => es.Title).FirstOrDefault()
                   }

                   )

                    .ToListAsync<dynamic>();

                var DataAyda = await _skyRecovery.Ayda.Where(es => es.Id == AydaId).ToListAsync<dynamic>();

                var Collection = new Dictionary<string, List<dynamic>>();

                Collection["nasabah"] = DataNasabah;
                Collection["DataFasilitas"] = await DatasFasilitas.ToListAsync<dynamic>();
                Collection["Dokumen"] = Dokumen;
                Collection["DataAyda"] = DataAyda;
               
                return (true, "OK", Collection);

            }


            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

        public async Task<(bool? Status, GeneralResponsesV2 Returns)> AydaDraft(string userid,CreateAydaDTO Entity)
        {
            var wrap = _DataResponses.Return();
            var ListData = new List<dynamic>();
            var getCallBy = await _User.GetDataUser(userid);
            var ReturnData = new DraftResponsesDTO();
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
                    GetData.Statusid= Convert.ToInt32(_config["WorkflowStatus:Draft"].ToString());
                    GetData.Createdby = getCallBy.Returns.Data.FirstOrDefault().iduser;
                    GetData.Lastupdatedate = DateTime.Now;
                    GetData.Isactive= 1;
                   _skyRecovery.Entry(GetData).State = EntityState.Modified;
                    await _skyRecovery.SaveChangesAsync();
                    ReturnData.RequestId = GetData.Id;
                    ReturnData.loanid = GetData.Loanid;

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
                        Statusid= Convert.ToInt32( _config["WorkflowStatus:Draft"].ToString()),
                        Createdby = getCallBy.Returns.Data.FirstOrDefault().iduser,
                        Createddated= DateTime.Now,
                        Isactive=1
                    };
                    await _skyRecovery.Ayda.AddAsync(Data);
                    await _skyRecovery.SaveChangesAsync();
                    ReturnData.loanid = Entity.DataNasabahLoan.loanid;
                    ReturnData.RequestId = Data.Id;

                }
                ListData.Add(ReturnData);

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
            var ListDynamic = new List<dynamic?>();
            // var SkyCollConsString = GetSkyCollConsString();

            try
            {
                if (String.IsNullOrEmpty(UserId))
                {
                    wrap.Status = false;
                    wrap.Message = "User Id Harus Diisi";
                }

                var getCallBy = await _User.GetDataUser(UserId);
                

                var DataAyda = from ad in _skyRecovery.Ayda
                               join wf in _skyRecovery.Workflow on ad.Id equals wf.Requestid
                               where wf.Fiturid == Convert.ToInt32(_config["Fitur:Recovery:Ayda"].ToString())
                               && wf.Actor == getCallBy.Returns.Data.FirstOrDefault().iduser
                               && wf.Status == Convert.ToInt32(_config["WorkflowStatus:Requested"].ToString())
                               select new
                               {
                                   Id = ad.Id,
                                   WorkflowId = wf.Id,
                                   StatusId = ad.Statusid,
                                   LoanId = ad.Loanid,
                                   createddated = ad.Createddated,
                                   createdby = ad.Createdby,
                                   FiturId = wf.Fiturid
                               };
                

                var JoinData = from ad in DataAyda.AsEnumerable()
                               join ml in _sky.MasterLoan on ad.LoanId equals ml.Id
                           join mc in _sky.MasterCustomer on ml.CustomerId equals mc.Id
                           join mt in _sky.MasterCollateral on ml.Id equals mt.LoanId
                           join br in _sky.Branch on mc.BranchId equals br.LbrcId
                           join st in _sky.Status on ad.StatusId equals st.StsId
                            
                               select new
                               {
                                   Id = ad.Id,
                                   WorkflowId=ad.WorkflowId,
                                   LoanId = ad.LoanId,
                                   customerid = mc.Id,
                                   cabang = br.LbrcName,
                                   noloan = ml.AccNo,
                                   namanasabah = mc.CuName,
                                   totaltunggakan = ml.TunggakanTotal,
                                   jenisjaminan = mt.ColType,
                                   alamatjaminan = mt.ColAddress,
                                   status = st.StsName,
                                   createddated = ad.createddated,
                                   createdby = ad.createdby,
                                   FiturId = Convert.ToInt32(_config["Fitur:Recovery:Ayda"].ToString())

                               };


                var Datas = JoinData.ToList<dynamic>();
                

                wrap.Status = true;
                wrap.Message = "OK";
                wrap.Data = Datas;
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
           // var SkyCollConsString = GetSkyCollConsString();

            try
            {
                if(String.IsNullOrEmpty(UserId))
                {
                    wrap.Status = false;
                    wrap.Message = "User Id Harus Diisi";
                }

                var getCallBy = await _User.GetDataUser(UserId);
                var id = getCallBy.Returns.Data.FirstOrDefault().iduser;

                var DataAyda =  await _skyRecovery.Ayda.Where(es => es.Createdby == id).ToListAsync();


                var JoinData = from ad in DataAyda.AsEnumerable()
                               join ml in _sky.MasterLoan on ad.Loanid equals ml.Id
                               join mc in _sky.MasterCustomer on ml.CustomerId equals mc.Id
                               join mt in _sky.MasterCollateral on ml.Id equals mt.LoanId
                               join br in _sky.Branch on mc.BranchId equals br.LbrcId
                               join st in _sky.Status on ad.Statusid equals st.StsId

                               select new
                               {
                                   Id = ad.Id,
                                   LoanId = ad.Loanid,
                                   customerid = mc.Id,
                                   cabang = br.LbrcName,
                                   noloan = ml.AccNo,
                                   namanasabah = mc.CuName,
                                   totaltunggakan = ml.TunggakanTotal,
                                   jenisjaminan = mt.ColType,
                                   alamatjaminan = mt.ColAddress,
                                   status = st.StsName,
                                   createddated = ad.Createddated,
                                   createdby = ad.Createdby,
                                   FiturId = Convert.ToInt32(_config["Fitur:Recovery:Ayda"].ToString())

                               };


                var Datas = JoinData.ToList<dynamic>();

              
                wrap.Status = true;
                wrap.Message = "OK";
                wrap.Data = Datas;
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
                   es => new 
                   {
                      Nama=es.CuName,
                      Address=es.CuAddress,
                      KTP=es.CuIdnumber,
                      CuCif=es.CuCif,
                      BranchId=es.BranchId,
                      Branch=_sky.Branch.Where(x=>x.LbrcId==es.BranchId).Select(es=>es.LbrcName).FirstOrDefault(),
                      AccNo=_sky.MasterLoan.Where(es=>es.Id==Entity.LoanId).Select(es=>es.AccNo).FirstOrDefault()

                   }
                   ).ToListAsync<dynamic>();

                var Files = await _skyRecovery.Masterrepository
                    .Where(es => es.Requestid == Entity.AydaId && es.Isactive==1).Select(
                   es => new 
                   {
                      Id=es.Id,
                      url=es.Fileurl,
                      urlname=es.Filename,
                      uploaddated=es.Uploaddated.ToString(),
                      doctype=es.Doctype

                   }
                   ).ToListAsync<dynamic>();

                var DataLoan = await _sky.MasterLoan.Where(es => es.Id == Entity.LoanId).
                    Select(es => new 
                {
                    loanid=es.Id,
                    Fasilitas=es.Fasilitas,
                    Tenor=es.Tenor.ToString(),
                    LoanType=_sky.Rfproduct.Where(x=>x.PrdId==es.Product).Select(es=>es.PrdDesc).FirstOrDefault(),
                    Plafond=es.Plafond.ToString()
                }).ToListAsync<dynamic>();

                var DataCollateral = _skyRecovery.Ayda.Where(es => es.Id== Entity.AydaId).AsEnumerable()
                    .Select(es => new 
                {
                    bankid = es.Hubunganbankid,
                    jumlahayda = es.Jumlahayda.ToString(),
                    kualitas = es.Kualitas,
                    nilaimargin = es.Nilaimargin.ToString(),
                    nilaipembiayaanpokok = es.Nilaipembiayaanpokok.ToString(),
                    nilaiperolehanagunan = es.Nilaiperolehanagunan.ToString(),
                    perkiraanbiayajual = es.Perkiraanbiayajual.ToString(),
                    ppa = es.Ppa.ToString(),
                    tglambilalih = es.Tglambilalih
                 

                    }).ToList<dynamic>();

             

                var Collection = new Dictionary<string, List<dynamic>>();

                Collection["DataNasabah"] = Nasabah;
                Collection["DataFiles"] = Files;
                Collection["DataLoan"] = DataLoan;
                Collection["DataAyda"] = DataCollateral;





                return (true,"OK", Collection);

            }
            catch (Exception ex)
            {
              

                return (false,ex.Message, null);
            }
        }

    }
}
