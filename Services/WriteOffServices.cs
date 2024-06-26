﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using sky.recovery.DTOs.RequestDTO.Ayda;
using sky.recovery.DTOs.RequestDTO.WO;
using sky.recovery.DTOs.WorkflowDTO;
using sky.recovery.Helper.Config;
using sky.recovery.Insfrastructures;
using sky.recovery.Insfrastructures.Scafolding.SkyColl.Public;
using sky.recovery.Insfrastructures.Scafolding.SkyColl.Recovery;
using sky.recovery.Interfaces;
using sky.recovery.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sky.recovery.Services
{
    public class WriteOffServices : SkyCoreConfig, IWriteOffServices
    {

        private IUserService _User { get; set; }
        private IGeneralParam _GeneralParam { get; set; }
        private readonly IWebHostEnvironment _environment;
        private IRestruktureRepository _postgreRepository { get; set; }
        private IHelperRepository _helperRepository { get; set; }
        private readonly IConfiguration _config;

        ModellingGeneralResponsesV2 _DataResponses = new ModellingGeneralResponsesV2();
        AydaHelper _aydahelper = new AydaHelper();
        private IWorkflowServices _workflowServices { get; set; }

        SkyCollRecoveryDBContext _RecoveryContext = new SkyCollRecoveryDBContext();
        SkyCollPublicDBContext _sky = new SkyCollPublicDBContext();
        public WriteOffServices(IWorkflowServices workflowServices,IConfiguration config,IGeneralParam GeneralParam, IWebHostEnvironment environment, IUserService User, IHelperRepository helperRepository, IRestruktureRepository postgreRepository,
      IOptions<DbContextSettings> appsetting) : base(appsetting)
        {
            _workflowServices = workflowServices;
            _config = config;
            _environment = environment;
            _GeneralParam = GeneralParam;
            _User = User;
            _postgreRepository = postgreRepository;
            _helperRepository = helperRepository;

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
        public async Task<(bool? Status, GeneralResponsesV2 Returns)> WOSubmit(string userid, CreateWODTO Entity)
        {
            var wrap = _DataResponses.Return();
            var ListData = new List<dynamic>();
            var getCallBy = await _User.GetDataUser(userid);

            // var SkyCollConsString = GetSkyCollConsString();

            try
            {


                //var GetAydaExisting = await ayda.Where(es => es.id == Entity.Data.aydaid).AnyAsync();
                if (Entity.Data.woid != null)// update draft
                {
                    var GetData = await _RecoveryContext.Writeoff.Where(es => es.Id == Entity.Data.woid && es.Loanid == Entity.DataNasabahLoan.loanid).FirstOrDefaultAsync();
                    if (_aydahelper.IsRequested(GetData.Statusid) == true)
                    {
                        wrap.Status = false;
                        wrap.Message = "Data tidak bisa diupdate, karena sudah masuk proses approval";
                    }

                    GetData.Loanid = Entity.DataNasabahLoan.loanid;
                    GetData.Principal = Entity.Data.Principal;
                    GetData.Currentoverdue = Entity.Data.CurrentOverdue;
                    GetData.Chargesrate = Entity.Data.ChargesRate;
                    GetData.Interestrate = Entity.Data.InterestRate;

                    GetData.Statusid = Convert.ToInt32(_config["WorkflowStatus:Requested"].ToString());
                    ;
                    GetData.Modifiedby = getCallBy.Returns.Data.FirstOrDefault().iduser;
                    GetData.Modifieddated = DateTime.Now;
                    GetData.Isactive = true;
                    _RecoveryContext.Entry(GetData).State = EntityState.Modified;
                    await _RecoveryContext.SaveChangesAsync();


                    var GetIdAyda = Convert.ToInt32(_config["Fitur:Recovery:WriteOff"].ToString());
                    var SubmitWorkflow = await WorkflowSubmit(Entity.Data.woid, (int?)GetIdAyda, userid);

                }
                else
                {
                    var Data = new Insfrastructures.Scafolding.SkyColl.Recovery.Writeoff()
                    {
                        Loanid = Entity.DataNasabahLoan.loanid,
                        Principal = Entity.Data.Principal,
                        Chargesrate = Entity.Data.ChargesRate,
                        Currentoverdue = Entity.Data.CurrentOverdue,
                        Interestrate = Entity.Data.InterestRate,


                        Statusid= Convert.ToInt32(_config["WorkflowStatus:Requested"].ToString()),
                    Createdby = getCallBy.Returns.Data.FirstOrDefault().iduser,
                        Createddated = DateTime.Now,
                        Isactive = true
                    };
                    await _RecoveryContext.Writeoff.AddAsync(Data);
                    await _RecoveryContext.SaveChangesAsync();

                    var GetIdAyda = Convert.ToInt32(_config["Fitur:Recovery:WriteOff"].ToString());
                    var SubmitWorkflow = await WorkflowSubmit(Entity.Data.woid, (int?)GetIdAyda, userid);

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

        public async Task<(bool? Status, GeneralResponsesV2 Returns)> WODraft(string userid, CreateWODTO Entity)
        {
            var wrap = _DataResponses.Return();
            var ListData = new List<dynamic>();
            var getCallBy = await _User.GetDataUser(userid);
            var ReturnData = new DraftResponsesDTO();

            // var SkyCollConsString = GetSkyCollConsString();

            try
            {

                //var GetAydaExisting = await ayda.Where(es => es.id == Entity.Data.aydaid).AnyAsync();
                if (Entity.Data.woid!= null)// update draft
                {
                    var GetData = await _RecoveryContext.Writeoff.Where(es => es.Id == Entity.Data.woid && es.Loanid == Entity.DataNasabahLoan.loanid).FirstOrDefaultAsync();
                    if (_aydahelper.IsRequested(GetData.Statusid) == true)
                    {
                        wrap.Status = false;
                        wrap.Message = "Data tidak bisa diupdate, karena sudah masuk proses approval";
                    }
                    GetData.Loanid = Entity.DataNasabahLoan.loanid;
                    GetData.Principal = Entity.Data.Principal;
                    GetData.Currentoverdue = Entity.Data.CurrentOverdue;
                    GetData.Chargesrate = Entity.Data.ChargesRate;
                    GetData.Interestrate = Entity.Data.InterestRate;
                    
                    GetData.Statusid = Convert.ToInt32(_config["WorkflowStatus:Draft"].ToString());
                    GetData.Modifiedby = getCallBy.Returns.Data.FirstOrDefault().iduser;
                    GetData.Modifieddated = DateTime.Now;
                    GetData.Isactive = true;
                    _RecoveryContext.Entry(GetData).State = EntityState.Modified;
                    await _RecoveryContext.SaveChangesAsync();
                    ReturnData.RequestId = GetData.Id;
                    ReturnData.loanid = GetData.Loanid;

                }
                else
                {
                    var Data = new Insfrastructures.Scafolding.SkyColl.Recovery.Writeoff()
                    {
                        Loanid = Entity.DataNasabahLoan.loanid,
                        Principal = Entity.Data.Principal,
                        Chargesrate = Entity.Data.ChargesRate,
                        Currentoverdue=Entity.Data.CurrentOverdue,
                        Interestrate=Entity.Data.InterestRate,

                    
                        Statusid = Convert.ToInt32(_config["WorkflowStatus:Draft"].ToString()),
                        Createdby = getCallBy.Returns.Data.FirstOrDefault().iduser,
                        Createddated = DateTime.Now,
                        Isactive = true
                    };
                    await _RecoveryContext.Writeoff.AddAsync(Data);
                    await _RecoveryContext.SaveChangesAsync();
                    ReturnData.loanid = Entity.DataNasabahLoan.loanid;
                    ReturnData.RequestId = Data.Id;

                }
                ListData.Add(ReturnData);
                wrap.Data = ListData;
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


        public async Task<(bool? Status, string message, Dictionary<string, List<dynamic>> DataNasabah)> GetDetailWO(GetDetailWO Entity)
        {
            var Data = from mc in _sky.MasterCustomer
                       join ml in _sky.MasterLoan on mc.Id equals ml.CustomerId
                       join pd in _sky.Rfproduct on ml.Product equals pd.PrdId
                       join ps in _sky.RfproductSegment on ml.PrdSegmentId equals ps.PrdSgmId
                       where ml.Id == Entity.LoanId
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
                                 where mc.Id == Entity.CustomerId
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

            var DataCollateral =await  _sky.MasterCollateral.Where(es => es.LoanId == Entity.LoanId).ToListAsync<dynamic>();
            var Collection = new Dictionary<string, List<dynamic>>();

            Collection["DataNasabah"] = DataNasabah;
            Collection["DataFasilitas"] = await DatasFasilitas.ToListAsync<dynamic>();
            Collection["DataCollateral"] = DataCollateral;


            return (true, "OK", Collection);


        }

        public async Task<(bool status, string message, List<dynamic> Data)> GetMonitorList(string userid)
        {
            try
            {
                var getCallBy = await _User.GetDataUser(userid);
                var DataWO = _RecoveryContext.Writeoff.
                     Where(es => es.Createdby == getCallBy.Returns.Data.FirstOrDefault().iduser).AsEnumerable();

                var JoinData = from ad in DataWO
                               join ml in _sky.MasterLoan on ad.Loanid equals ml.Id
                               join mc in _sky.MasterCustomer on ml.CustomerId equals mc.Id
                               join st in _sky.Status on ad.Statusid equals st.StsId

                               select new
                               {
                                   Id = ad.Id,
                                   LoanId = ad.Loanid,
                                   customerid = mc.Id,
                                   noloan = ml.AccNo,
                                   namanasabah = mc.CuName,
                                   interestrate = ad.Interestrate,
                                   chargesrate = ad.Chargesrate,
                                   principal = ad.Principal,
                                   currentoverdue = ad.Currentoverdue,
                                   status = st.StsName,
                                   createddated = ad.Createddated,
                                   createdby = ad.Createdby,
                                   FiturId = Convert.ToInt32(_config["Fitur:Recovery:WriteOff"].ToString())

                               };


                var Datas = JoinData.ToList<dynamic>();

                return (true, "OK", Datas);


            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

            public async Task<(bool status, string message, List<dynamic> Data)> GetTaskList(string userid)
            {
                try
                {
                    var getCallBy = await _User.GetDataUser(userid);



                var DataWO = from ad in _RecoveryContext.Writeoff
                               join wf in _RecoveryContext.Workflow on ad.Id equals wf.Requestid
                               where wf.Fiturid == Convert.ToInt32(_config["Fitur:Recovery:WriteOff"].ToString())
                               && wf.Actor == getCallBy.Returns.Data.FirstOrDefault().iduser
                               && wf.Status == Convert.ToInt32(_config["WorkflowStatus:Requested"].ToString())
                               select new
                               {
                                   Id = ad.Id,
                                   WorkflowId = wf.Id,
                                   StatusId = ad.Statusid,
                                   LoanId = ad.Loanid,
                                   interestrate = ad.Interestrate,
                                   chargesrate = ad.Chargesrate,
                                   principal = ad.Principal,
                                   currentoverdue = ad.Currentoverdue,
                                   createddated = ad.Createddated,
                                   createdby = ad.Createdby,
                                   FiturId = wf.Fiturid
                               };
                var JoinData = from ad in DataWO.AsEnumerable()
                                   join ml in _sky.MasterLoan on ad.LoanId equals ml.Id
                                   join mc in _sky.MasterCustomer on ml.CustomerId equals mc.Id
                                   join st in _sky.Status on ad.StatusId equals st.StsId

                                   select new
                                   {
                                       Id = ad.Id,
                                       LoanId = ad.LoanId,
                                       customerid = mc.Id,
                                       noloan = ml.AccNo,
                                       namanasabah = mc.CuName,
                                       interestrate = ad.interestrate,
                                       chargesrate = ad.chargesrate,
                                       principal = ad.principal,
                                       currentoverdue = ad.currentoverdue,
                                       status = st.StsName,
                                       createddated = ad.createddated,
                                       createdby = ad.createdby,
                                       FiturId = Convert.ToInt32(_config["Fitur:Recovery:WriteOff"].ToString())

                                   };


                    var Datas = JoinData.ToList<dynamic>();

                    return (true, "OK", Datas);


                }
                catch (Exception ex)
                {
                    return (false, ex.Message, null);
                }

            }



    }
}
