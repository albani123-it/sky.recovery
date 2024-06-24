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
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;

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

        private readonly IConfiguration _config;

        ModellingGeneralResponsesV2 _DataResponses = new ModellingGeneralResponsesV2();
        AydaHelper _aydahelper = new AydaHelper();
        public AuctionServices(IConfiguration config, IWorkflowServices workflowServices, IGeneralParam GeneralParam, IWebHostEnvironment environment, IUserService User, IHelperRepository helperRepository, IRestruktureRepository postgreRepository,
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



        public async Task<(bool? Status, GeneralResponsesV2 Returns)> AuctionTaskList(string UserId)
        {
            var wrap = _DataResponses.Return();
            // var SkyCollConsString = GetSkyCollConsString();
            var ReturnData = new List<dynamic?>();

            try
            {
              //  var x = Convert.ToInt32(_config["WorklowStatus:Requested"].ToString());
                //var y = Convert.ToInt32(_config["Fitur:Recovery:Auction"].ToString());

                var getCallBy = await _User.GetDataUser(UserId);

                var MonitoringData = from ad in _recoveryContext.Auction
                               join wf in _recoveryContext.Workflow on ad.Id equals wf.Requestid
                               where wf.Fiturid == Convert.ToInt32(_config["Fitur:Recovery:Auction"].ToString()) 
                               && wf.Actor == getCallBy.Returns.Data.FirstOrDefault().iduser
                               && wf.Status == Convert.ToInt32(_config["WorklowStatus:Requested"].ToString())
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


                var JoinData = from ad in MonitoringData.AsEnumerable()
                               join ml in _collContext.MasterLoan on ad.LoanId equals ml.Id
                               join mc in _collContext.MasterCustomer on ml.CustomerId equals mc.Id
                               join mt in _collContext.MasterCollateral on ml.Id equals mt.LoanId
                               join br in _collContext.Branch on mc.BranchId equals br.LbrcId
                               join st in _collContext.Status on ad.StatusId equals st.StsId

                               select new
                               {
                                   Id = ad.Id,
                                   WorkflowId = ad.WorkflowId,
                                   noaccount = ml.AccNo,
                                   LoanId = ad.LoanId,
                                   customerid = mc.Id,
                                   cabang = br.LbrcName,
                                  cif = ml.CuCif,
                                   namanasabah = mc.CuName,
                                   status = st.StsName,
                                   createddated = ad.createddated,
                                   createdby = ad.createdby,
                                   FiturId = Convert.ToInt32(_config["Fitur:Recovery:Auction"].ToString())

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

        public async Task<(bool? Status, GeneralResponsesV2 Returns)> AuctionMonitoring(string UserId)
        {
            var wrap = _DataResponses.Return();
            // var SkyCollConsString = GetSkyCollConsString();

            try
            {
                

                var getCallBy = await _User.GetDataUser(UserId);

                var ReturnData = await auction.Where(es => es.createdby == getCallBy.Returns.Data.FirstOrDefault().iduser)
                    .ToListAsync();



                var JoinData = from ad in ReturnData.AsEnumerable()
                               join ml in _collContext.MasterLoan on ad.loanid equals ml.Id
                               join mc in _collContext.MasterCustomer on ml.CustomerId equals mc.Id
                               join mt in _collContext.MasterCollateral on ml.Id equals mt.LoanId
                               join br in _collContext.Branch on mc.BranchId equals br.LbrcId
                               join st in _collContext.Status on ad.statusid equals st.StsId

                               select new
                               {
                                   Id = ad.id,
                                   noaccount = ml.AccNo,
                                   LoanId = ad.loanid,
                                   customerid = mc.Id,
                                   cabang = br.LbrcName,
                                   cif = ml.CuCif,
                                   namanasabah = mc.CuName,
                                   status = st.StsName,
                                   createddated = ad.createddated,
                                   createdby = ad.createdby,
                                   FiturId = Convert.ToInt32(_config["Fitur:Recovery:Auction"].ToString())

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
        public async Task<(bool? Status, string message, Dictionary<string, List<dynamic>> DataNasabah)> GetDetailAuction(GetDetailAuctionDTO Entity)
        {
            var wrap = _DataResponses.Return();
            // var SkyCollConsString = GetSkyCollConsString();

            try
            {

                var Nasabah = await _collContext.MasterCustomer.Where(es => es.Id == Entity.CustomerId).Select(
                   es => new 
                   {
                       Nama = es.CuName,
                       Address = es.CuAddress,
                       KTP = es.CuIdnumber,
                       CuCif = es.CuCif,
                       BranchId = es.BranchId,
                       Branch = _collContext.Branch.Where(x => x.LbrcId == es.BranchId).Select(es => es.LbrcName).FirstOrDefault(),
                       AccNo = _collContext.MasterLoan.Where(es => es.Id == Entity.LoanId).Select(es => es.AccNo).FirstOrDefault()

                   }
                   ).ToListAsync<dynamic>();

                var Files = await _recoveryContext.Masterrepository.
                    Where(es => es.Requestid == Entity.AuctionId && es.Isactive == 1).Select(
                   es => new 
                   {
                       Id = es.Id,
                       url = es.Fileurl,
                       urlname = es.Filename,
                       uploaddated = es.Uploaddated.ToString(),
                       doctype = es.Doctype

                   }
                   ).ToListAsync<dynamic>();

                var DataLoan = await _collContext.MasterLoan.Where(es => es.Id == Entity.LoanId)
                    .Select(es => new 
                {
                    loanid = es.Id,
                    Fasilitas = es.Fasilitas,
                    Tenor = es.Tenor.ToString(),
                    LoanType = _collContext.Rfproduct.Where(x => x.PrdId == es.Product).Select(es => es.PrdDesc).FirstOrDefault(),
                    Plafond = es.Plafond.ToString()
                }).ToListAsync<dynamic>();

                var DataAuction =  _recoveryContext.Auction.Where(es => es.Id == Entity.AuctionId)
                    .Select(es => new 
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
                   namarekening=es.Namarekening
 
                }).ToList<dynamic>();

              

                var Collection = new Dictionary<string, List<dynamic>>();




                Collection["DataNasabah"] = Nasabah;
                Collection["DataFiles"] = Files;
                Collection["DataLoan"] = DataLoan;
                Collection["DataAuction"] = DataAuction;


             



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

                    GetData.Statusid = Convert.ToInt32(_config["WorklowStatus:Requested"].ToString());
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


                        Statusid = Convert.ToInt32(_config["WorklowStatus:Requested"].ToString()),
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
                    


                    GetData.Statusid = Convert.ToInt32(_config["WorklowStatus:Draft"].ToString());
                    GetData.Createdby = getCallBy.Returns.Data.FirstOrDefault().iduser;
                    GetData.Lastupdatedate = DateTime.Now;
                    GetData.Lastupdatedid = getCallBy.Returns.Data.FirstOrDefault().iduser;

                    Entry(GetData).State = EntityState.Modified;
                    await SaveChangesAsync();
                   
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
                        Statusid = Convert.ToInt32(_config["WorklowStatus:Draft"].ToString()),
                        Createdby = getCallBy.Returns.Data.FirstOrDefault().iduser,
                        Createddated = DateTime.Now
                    };
                    await _recoveryContext.Auction.AddAsync(Data);
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

        public async Task<(bool Status, string Message, Dictionary<string, List<dynamic>> DataNasabah)> GetDetailAuctionForApproval(int AuctionId, int loanid, int CustomerId)
        {
            try
            {


                var Data = from mc in _collContext.MasterCustomer
                           join ml in _collContext.MasterLoan on mc.Id equals ml.CustomerId
                           join pd in _collContext.Rfproduct on ml.Product equals pd.PrdId
                           join ps in _collContext.RfproductSegment on ml.PrdSegmentId equals ps.PrdSgmId
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

                var DatasFasilitas = from mc in _collContext.MasterCustomer
                                     join ml in _collContext.MasterLoan on mc.Id equals ml.CustomerId
                                     join pd in _collContext.Rfproduct on ml.Product equals pd.PrdId
                                     join ps in _collContext.RfproductSegment on ml.PrdSegmentId equals ps.PrdSgmId
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



                var Dokumen = await _recoveryContext.Masterrepository.Where(es => es.Requestid == AuctionId)
                   .Select(es => new
                   {
                       JenisDokumen = es.Doctype,
                       Url = es.Fileurl,
                       FileName = es.Fileurl,
                       UploadDated = es.Uploaddated,
                       Keterangan = es.Keterangan,
                       Status = _recoveryContext.Generalparamdetail.Where(x => x.Id == es.Status).Select(es => es.Title).FirstOrDefault()
                   }

                   )

                    .ToListAsync<dynamic>();

                var DataAsuransi = await _recoveryContext.Insurance.Where(es => es.Id == AuctionId).ToListAsync<dynamic>();

                var Collection = new Dictionary<string, List<dynamic>>();

                Collection["nasabah"] = DataNasabah;
                Collection["DataFasilitas"] = await DatasFasilitas.ToListAsync<dynamic>();
                Collection["Dokumen"] = Dokumen;

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
