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

namespace sky.recovery.Services
{
    public class RestrukturServices :SkyCoreConfig, IRestrukturServices
    {
        private IUserService _User { get; set; }
        private IRestruktureRepository _postgreRepository { get; set; }

        ModellingGeneralResponsesV2 _DataResponses = new ModellingGeneralResponsesV2();
       
            public RestrukturServices(IUserService User,  IRestruktureRepository postgreRepository,
            IOptions<DbContextSettings> appsetting) : base(appsetting)
        {
            _User = User;
            _postgreRepository = postgreRepository;
         
        }

        //SERVICE YANG DIPAKAI
        //MONITORING RESTRUKTUR V2
        public async Task<(bool? Error, GeneralResponsesV2 Returns)> MonitoringRestrukturV2()
        {
            var wrap = _DataResponses.Return();
        
            try
            {

               // var getCallBy = await _User.GetDataUser(UserId);
                var ReturnData =await(Task.Run(()=> _postgreRepository.GetRestukture(1,"\"RecoveryBusinessV2\".getrestrukture","")));
                wrap.Error = false;
                wrap.Message = "OK";
                wrap.Data = ReturnData;
                return (wrap.Error, wrap);

            }
            catch (Exception ex)
            {
                wrap.Error = true;
                wrap.Message = ex.Message;

                return (wrap.Error, wrap);
            }
        }


        //SERVICE YANG DIPAKAI
        //TASKLIST RESTRUKTUR V2
        public async Task<(bool? Error, GeneralResponsesV2 Returns)> TaskListRestrukturV2(string UserId)
        {
            var wrap = _DataResponses.Return();

            try
            {

                var getCallBy = await _User.GetDataUser(UserId);
                var ReturnData = await (Task.Run(() => _postgreRepository.GetRestukture(2, "\"RecoveryBusinessV2\".tasklistrestrukture", getCallBy.Returns.Data.FirstOrDefault().acceslevel)));
                wrap.Error = false;
                wrap.Message = "OK";
                wrap.Data = ReturnData;
                return (wrap.Error, wrap);

            }
            catch (Exception ex)
            {
                wrap.Error = true;
                wrap.Message = ex.Message;

                return (wrap.Error, wrap);
            }
        }



        //SERVICE YANG DIPAKAI
        // MONITORING RESTRUKTUR
        public async Task<(bool? Error, GenericResponses<ListRestructureDTO> Returns)> ListRestructure()
        {
            var wrap = new GenericResponses<ListRestructureDTO>
            {
                Error = false,
                Message = ""
            };
            try
            {
               

                //var getCallBy = await _User.GetDataUser(UserId);

                var Data =await  restructure.
                    Include(i=>i.master_loan).
                    Include(i=>i.status).
                    Select(es => new ListRestructureDTO
                {
                  
                    Nasabah=es.master_loan.master_customer.cu_name,
                    CustomerId=es.master_loan.customer_id,
                    loanId=es.master_loan.id,
                    cucif=es.master_loan.cu_cif,
                    acc_no=es.master_loan.acc_no,
                    rst_id=es.rst_id,                 
                    BranchName=es.master_loan.master_customer.branch.lbrc_name,
                    Status=es.status.sts_name,
                    PolaRestruktur=es.generic_param_pola_restruk.glp_name
                  
                }).ToListAsync();

                wrap.Error = false;
                wrap.Message = "OK";
                wrap.Data = Data;
                return (wrap.Error,wrap);

            }
            catch (Exception ex)
            {
                wrap.Error = true;
                wrap.Message = ex.Message;
               
                return (wrap.Error, wrap);
            }
        }

        //SERVICE YANG DIPAKAI
        // TASKLIST RESTRUKTUR
        public async Task<(bool? Error, GenericResponses<ListRestructureDTO> Returns)> TasklistRestrukture(string userid)
        {
            string FeedStatus="";
            var wrap = new GenericResponses<ListRestructureDTO>
            {
                Error = false,
                Message = ""
            };
            try
            {


                var getCallBy = await _User.GetDataUser(userid);
                
                if(getCallBy.Returns.Data.FirstOrDefault().role=="ADMIN")
                {
                    FeedStatus = "DRAFT";
                }
                if (getCallBy.Returns.Data.FirstOrDefault().role == "ADMIN2")
                {
                    FeedStatus = "PENGAJUAN";
                }
                if (getCallBy.Returns.Data.FirstOrDefault().role == "MANAJEMEN")
                {
                    FeedStatus = "VERIFIKASI";
                }
                var Data = await restructure.
                    Include(i => i.master_loan).
                    Include(i => i.status).
                    Select(es => new ListRestructureDTO
                    {

                        Nasabah = es.master_loan.master_customer.cu_name,
                        CustomerId = es.master_loan.customer_id,
                        loanId = es.master_loan.id,
                        cucif = es.master_loan.cu_cif,
                        acc_no = es.master_loan.acc_no,
                        rst_id = es.rst_id,
                        BranchName = es.master_loan.master_customer.branch.lbrc_name,
                        Status = es.status.sts_name,
                        PolaRestruktur = es.generic_param_pola_restruk.glp_name

                    }).Where(es=>es.Status==FeedStatus).ToListAsync();

                wrap.Error = false;
                wrap.Message = "OK";
                wrap.Data = Data;
                return (wrap.Error, wrap);

            }
            catch (Exception ex)
            {
                wrap.Error = true;
                wrap.Message = ex.Message;

                return (wrap.Error, wrap);
            }
        }
        public async Task<(bool Error, GeneralResponses Returns)> ListSearchRestructure(SearchListRestrucutre Entity)
        {
            try
            {
                //var getCallBy = await _User.GetDataUser(userid);

                var Data = await restructure.Include(i => i.master_loan).Include(i => i.status).Select(es => new ListRestructureDTO
                {

                    Nasabah = es.master_loan.master_customer.cu_name,
                    CustomerId = es.master_loan.customer_id,
                    loanId = es.master_loan.id,
                    cucif = es.master_loan.cu_cif,
                    acc_no = es.master_loan.acc_no,
                    rst_id = es.rst_id,
                    BranchName = es.master_loan.master_customer.branch.lbrc_name,
                    Status = es.status.sts_name,
                    PolaRestruktur = es.generic_param_pola_restruk.glp_name

                }).Where(es=>es.Nasabah.Contains(Entity.SearchName) || es.acc_no.Contains(Entity.SearchAccNo)).ToListAsync();

            

                var Result = new GeneralResponses()
                {
                    Error = false,
                    Message = "ok",
                    Data = new ContentResponses()
                    {
                        RestructureDTOs = Data
                    }
                };
                return (Result.Error, Result);

            }
            catch (Exception ex)
            {
                var Result = new GeneralResponses()
                {
                    Error = true,
                    Message = ex.Message
                };
                return (Result.Error, Result);
            }
        }

        //SERVICE YANG DIPAKAI
        public async Task<(bool? Error, GenericResponses<MonitoringDetailRestructureDTO> Returns)> MonitoringListDetail(string userid)
        {
            var wrap = new GenericResponses<MonitoringDetailRestructureDTO>
            {
                Error = false,
                Message = ""
            };
            try
            {
                var getCallBy = await _User.GetDataUser(userid);

                if(getCallBy.Returns==null)
                {
                    wrap.Error = true;
                    wrap.Message = "User Not Found";
                    return (wrap.Error,wrap);
                }
                var Data = await master_loan.Include(i => i.master_customer).
                    Include(i=>i.collection_call).
                    Select(es => new MonitoringDetailRestructureDTO
                {

                    Nasabah = es.master_customer.cu_name,
                    CustomerId=es.master_customer.Id,
                    LoanId=es.id,
                    cucif = es.cu_cif,
                    acc_no = es.acc_no,
                    TotalKewajiban = es.kewajiban_total,
                    DPD = es.dpd,
                    Kolektibilitas=es.kolektibilitas,
                    CallBy=es.collection_call.call_by,
                    Status=es.status
                    
                }).Where(es=>es.DPD>90 && es.Status==1 && es.CallBy==getCallBy.Returns.Data.FirstOrDefault().iduser).ToListAsync();
                wrap.Error = false;
                wrap.Message = "OK";
                wrap.Data = Data;
                return (wrap.Error, wrap);

            }
            catch (Exception ex)
            {
                wrap.Error = true;
                wrap.Message = ex.Message;
                return (wrap.Error, wrap);
            }
        }


        public async Task<(bool Error, GeneralResponses Returns)> GetGeneralDetailNasabah(RequestRestrukturDetail Entity)
        {
            try
            {

                //ambil general info
                var Data = await master_loan.Include(i=>i.rfproduct_segment).Include(i => i.rfproduct).Include(i => i.master_customer).Select(es => new DetailNasabah
                {
                    accno = es.acc_no,
                    loannumber=es.loan_number,
                    cucif = es.cu_cif,
                    Nasabah = es.master_customer.cu_name,
                    NoKTP = es.master_customer.cu_idnumber,
                    TanggalLahir = es.master_customer.cu_borndate,
                    Alamat = es.master_customer.cu_address,
                    noTelp = es.master_customer.cu_hmphone,
                    NoHp = es.master_customer.cu_mobilephone,
                    Pekerjaan = es.master_customer.pekerjaan,
                    BranchName=es.master_customer.branch.lbrc_name,
                    TanggalCore = es.master_customer.stg_date,
                    Segment = es.rfproduct_segment.prd_sgm_desc,
                    Product = es.rfproduct.prd_desc,
                    TanggalMulai = es.start_date,
                    TanggalJatuhTempo = es.maturity_date,
                    Tenor = es.tenor,
                    Plafond = es.plafond,
                    BranchAreaCode=es.master_customer.branch.lbrc_code,
                    OutStanding = es.outstanding,
                    //OutStandingActual=es.outstanding,
                    Kolektabilitas = es.kolektibilitas,
                    DPD = es.dpd,
                    TanggalBayarTerakhir = es.last_pay_date,
                    TunggakanPokok = es.tunggakan_pokok,
                    TunggakanBunga = es.tunggakan_bunga,
                    TunggakanDenda = es.tunggakan_denda,
                    TotalTunggakan = es.tunggakan_total,
                    TotalKewajiban = es.kewajiban_total,
                    JumlahAngsuran = es.installment

                }).Where(es => es.accno == Entity.Accno).FirstOrDefaultAsync();

                

                var Result = new GeneralResponses()
                {
                    Error = false,
                    Message = "ok",
                    Data = new ContentResponses()
                    {
                        DetailNasabah = Data
                     
                    }
                };
                return (Result.Error, Result);

            }
            catch (Exception ex)
            {
                var Result = new GeneralResponses()
                {
                    Error = true,
                    Message = ex.Message
                };
                return (Result.Error, Result);
            }
        }

        public async Task<(bool Error, GeneralResponses Returns)> GetDokumenParam()
        {
            try
            {
                var DataDokumenRestrukture = await generic_param.Select(es=>new GenericParamDTO
                {
                    glp_code=es.glp_code,
                    glp_id=es.glp_id,
                    glp_name=es.glp_name,
                    glp_type=es.glp_type
                }).Where(es=>es.glp_type=="DOCR").OrderBy(es => es.glp_name).ToListAsync();

                var Result = new GeneralResponses()
                {
                    Error = false,
                    Message = "ok",
                    DataGenericParam = new ContentResponsesGenericParam()
                    {
                        DokumenRestruktur = DataDokumenRestrukture

                    }
                };
                return (Result.Error, Result);
            }
            catch (Exception ex)
            {
                var Result = new GeneralResponses()
                {
                    Error = true,
                    Message = ex.Message
                };
                return (Result.Error, Result);
            }
        }

        public async Task<(bool Error, GeneralResponses Returns)> GetJenisPengurangan()
        {
            try
            {
                var DataJenisPengurangan= await generic_param.Select(es => new GenericParamDTO
                {
                    glp_code = es.glp_code,
                    glp_id = es.glp_id,
                    glp_name = es.glp_name,
                    glp_type = es.glp_type
                }).Where(es => es.glp_type == "JENP").OrderBy(es => es.glp_name).ToListAsync();

                var Result = new GeneralResponses()
                {
                    Error = false,
                    Message = "ok",
                    DataGenericParam = new ContentResponsesGenericParam()
                    {
                        JenisPengurangan = DataJenisPengurangan

                    }
                };
                return (Result.Error, Result);
            }
            catch (Exception ex)
            {
                var Result = new GeneralResponses()
                {
                    Error = true,
                    Message = ex.Message
                };
                return (Result.Error, Result);
            }
        }

        //SERVICE YANG DIPAKAI
        public async Task<(bool Error, GeneralResponses Returns)> GetListBranch()
        {
            try
            {
                var DataBranch= await branch.Select(es => new BranchDTO
                {
                    lbrc_name= es.lbrc_name,
                    lbrc_id=es.lbrc_id,
                    lbrc_is_delete=es.lbrc_is_delete
                  
                }).Where(es => es.lbrc_is_delete== false).ToListAsync();

                var Result = new GeneralResponses()
                {
                    Error = false,
                    Message = "ok",
                    DataMasterDTO = new ContentMasterDTO()
                    {
                        BranchDTO = DataBranch

                    }
                };
                return (Result.Error, Result);
            }
            catch (Exception ex)
            {
                var Result = new GeneralResponses()
                {
                    Error = true,
                    Message = ex.Message
                };
                return (Result.Error, Result);
            }
        }

        //SERVICE YANG DIPAKAI
        public async Task<(bool Error,GeneralResponses Returns)> CreateRestructure(CreateNewRestructure Entity)
        {
            try
            {
                var reqUser = await _User.GetDataUser(Entity.UserId);
                if (reqUser.Returns == null)
                {
                    var Returns = new GeneralResponses()
                    {
                        Error = true,
                        Message = "UserTidak Ditemukan"
                    };
                    return (false, Returns);
                }
                var CekLoan = master_loan.Find(Entity.LoanId);
                if (CekLoan is null)
                {
                    var Returns = new GeneralResponses()
                    {
                        Error = true,
                        Message = "Data Loan Tidak Ditemukan"
                    };
                    return (false, Returns);
                }


                var ne = new restructure();
                ne.denda = Entity.Denda;
                ne.disc_tunggakan_denda = Entity.DiskonTunggakanDenda;
                ne.disc_tunggakan_margin = Entity.DiskonTunggakanMargin;
                ne.jenis_pengurangan_id = Entity.JenisPenguranganId;
                ne.keterangan = Entity.Keterangan;
                ne.rst_loan_id = Entity.LoanId;
                ne.margin_amount = Entity.MarginAmount;
                ne.margin = Entity.Margin;
                ne.margin_pembayaran = Entity.MarginPembayaran;
                ne.margin_pinalty = Entity.MarginPinalty;
                ne.pembayaran_gp_id = Entity.PembayaranGpId;
                ne.pengurangan_nilai_margin = Entity.PenguranganNilaiMargin;
                ne.periode_diskon = Entity.PeriodeDiskon;
                ne.rst_pola_restruk_id = Entity.PolaRestrukId;
                ne.principal_pembayaran = Entity.PrincipalPembayaran;
                ne.principal_pinalty = Entity.PrincipalPinalty;
                ne.tgl_jatuh_tempo_baru = Entity.TglJatuhTempoBaru;
                ne.total_diskon_margin = Entity.TotalDiskonMargin;
                ne.value_date = Entity.ValueDate;
                ne.rst_mst_branch_id = CekLoan.master_customer?.branch_id;
                ne.mst_branch_pembukuan_id = CekLoan.master_customer?.branch_id;
                ne.mst_branch_proses_id = CekLoan.master_customer?.branch_id;

                //var json = JsonSerializer.Deserialize<List<string>>(bean.Permasalahan!);

                var serializeOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                };
                string json = JsonSerializer.Serialize(Entity.Permasalahan, serializeOptions);
                ne.permasalahan = json;

                ne.createby_id = reqUser.Returns.Data.FirstOrDefault().iduser;
                ne.create_date = DateTime.Now;
                string name = "DRAFT";
                ne.status = status.Where(q => q.sts_name.Equals(name)).FirstOrDefault();
                await restructure.AddAsync(ne);
                await SaveChangesAsync();

                if (Entity.CashFlow != null)
                {
                    var cf = Entity.CashFlow;
                    var nrcf = new restructure_cashflow();
                    nrcf.rsc_penghasilan_nasabah = cf.PenghasilanNasabah;
                    nrcf.rsc_penghasilan_pasangan = cf.PenghasilanPasangan;
                    nrcf.rsc_penghasilan_lainnya = cf.PenghasilanLainnya;
                    nrcf.rsc_total_penghasilan = cf.TotalPenghasilan;
                    nrcf.rsc_biaya_pendidikan = cf.BiayaPendidikan;
                    nrcf.rsc_biaya_listrik_air_telp = cf.BiayaListrikAirTelp;
                    nrcf.rsc_biaya_belanja = cf.BiayaBelanja;
                    nrcf.rsc_biaya_transportasi = cf.BiayaTransportasi;
                    nrcf.rsc_biaya_lainnya = cf.BiayaLainnya;
                    nrcf.rsc_total_pengeluaran = cf.TotalPengeluaran;
                    nrcf.rsc_hutang_di_bank = cf.HutangDiBank;
                    nrcf.rsc_cicilan_lainnya = cf.CicilanLainnya;
                    nrcf.rsc_total_kewajiban = cf.TotalKewajiban;
                    nrcf.rsc_penghasilan_bersih = cf.PenghasilanBersih;
                    nrcf.rsc_rpc_70_persen = cf.Rpc70Persen;
                    nrcf.rsc_restructure_id = ne.rst_id;
                    await restructure_cashflow.AddAsync(nrcf);
                    await SaveChangesAsync();
                }


                //var appr = new RestructureApprovalEntity();
                //// appr.Execution = this.statusService.GetRecoveryExecution("DRAFT");
                //appr.Execution = this.entity.RecoveryExecution.Where(q => q.Name.Equals(name)).FirstOrDefault();
                //appr.Sender = reqUser;
                //appr.Recipient = reqUser;
                //appr.CreateDate = DateTime.Now;
                //appr.RestructureId = ne.Id;
                //this.entity.RestructureApproval.Add(appr);
                //this.entity.SaveChanges();


                //if (Entity.Document != null)
                //{
                //    foreach (var item in Entity.Document)
                //    {
                //        var doc = this.entity.RestructureDocument.Find(item);
                //        if (doc != null)
                //        {
                //            doc.RestructureId = ne.Id;
                //            this.entity.RestructureDocument.Update(doc);
                //            this.entity.SaveChanges();
                //        }
                //    }
                //}
                var Result = new GeneralResponses()
                {
                    Error = false,
                    Message = "OK"
                };
                return (Result.Error, Result);
            }
            catch(Exception ex)
            {
                var Result = new GeneralResponses()
                {
                    Error = true,
                    Message = ex.Message
                };
                return (Result.Error, Result);

            }
        }
        public async Task<(bool Error, GeneralResponses Returns)> GetPolaRestrukturParam()
        {
            try
            {
                var DataPolaRestruktur= await generic_param.Select(es => new GenericParamDTO
                {
                    glp_code = es.glp_code,
                    glp_id = es.glp_id,
                    glp_name = es.glp_name,
                    glp_type = es.glp_type
                }).Where(es => es.glp_type == "POLR").OrderBy(es => es.glp_name).ToListAsync();

                var Result = new GeneralResponses()
                {
                    Error = false,
                    Message = "ok",
                    DataGenericParam = new ContentResponsesGenericParam()
                    {
                        PolaRestruktur = DataPolaRestruktur

                    }
                };
                return (Result.Error, Result);
            }
            catch (Exception ex)
            {
                var Result = new GeneralResponses()
                {
                    Error = true,
                    Message = ex.Message
                };
                return (Result.Error, Result);
            }
        }

        public async Task<(bool Error, GeneralResponses Returns)> UpdatePengajuanRestrukturisasi(UpdateRestrukturisasi Entity)
        {
            try
            {
                

                var Data = await restructure.Where(es => es.rst_id == Entity.retrukid).FirstOrDefaultAsync();
                if (Data == null)
                {
                    var Result = new GeneralResponses()
                    {
                        Error = true,
                        Message = "Data Restrukturisasi Tidak Ditemukan"
                    };
                    return (Result.Error, Result);
                }
                else
                {
                    Data.rst_pola_restruk_id = Entity.PolaId;
                    Data.rst_status_id = 8;
                    Data.keterangan = Entity.Catatan;
                    Data.last_update_date = DateTime.Now;
                    Entry(Data).State = EntityState.Modified;
                    SaveChanges();
                    var Result = new GeneralResponses()
                    {
                        Error = false,
                        Message = "Permohonan Sedang Diajukan"
                    };
                    return (Result.Error, Result);
                }
            }
            catch (Exception ex)
            {
                var Result = new GeneralResponses()
                {
                    Error = true,
                    Message = ex.Message
                };
                return (Result.Error, Result);

            }
        }
                public async Task<(bool Error, GeneralResponses Returns)> GetRestrukturDetailByAccno(string accno)
        {
            try
            {


                var Data = await (Task.Run(() => master_loan.Include(i => i.rfproduct_segment).Include(i => i.rfproduct).Include(i => i.master_customer).Select(es => new DetailNasabah
                {
                    accno = es.acc_no,
                    cucif = es.cu_cif,
                    Nasabah = es.master_customer.cu_name,
                    NoKTP = es.master_customer.cu_idnumber,
                    TanggalLahir = es.master_customer.cu_borndate,
                    Alamat = es.master_customer.cu_address,
                    noTelp = es.master_customer.cu_hmphone,
                    NoHp = es.master_customer.cu_mobilephone,
                    Pekerjaan = es.master_customer.pekerjaan,
                    TanggalCore = es.master_customer.stg_date,
                    Segment = es.rfproduct_segment.prd_sgm_desc,
                    Product = es.rfproduct.prd_desc,
                    TanggalMulai = es.start_date,
                    TanggalJatuhTempo = es.maturity_date,
                    Tenor = es.tenor,
                    Plafond = es.plafond,
                    OutStanding = es.outstanding,
                    //OutStandingActual=es.outstanding,
                    Kolektabilitas = es.kolektibilitas,
                    DPD = es.dpd,
                    TanggalBayarTerakhir = es.last_pay_date,
                    TunggakanPokok = es.tunggakan_pokok,
                    TunggakanBunga = es.tunggakan_bunga,
                    TunggakanDenda = es.tunggakan_denda,
                    TotalTunggakan = es.tunggakan_total,
                    TotalKewajiban = es.kewajiban_total,
                    JumlahAngsuran = es.installment

                }).AsEnumerable().Where(es => es.accno == accno)
                .Select(x => new DetailNasabahDTO
                {
                    accno = x.accno,
                    Nasabah = x.Nasabah,
                    NoKTP = x.NoKTP,
                    TanggalLahir = x.TanggalLahir.ToString(),
                    Alamat = x.Alamat,
                    noTelp = x.noTelp,
                    NoHp = x.NoHp,
                    Pekerjaan = x.Pekerjaan,
                    TanggalCore = x.TanggalCore.ToString(),
                    Segment = x.Segment,
                    Product = x.Product,
                    TanggalMulai = x.TanggalMulai.ToString(),
                    TanggalJatuhTempo = x.TanggalJatuhTempo.ToString(),
                    Tenor = x.Tenor.ToString(),
                    Plafond = x.Plafond.ToString(),
                    OutStanding = x.OutStanding.ToString(),
                    //OutStandingActual=es.outstanding,
                    Kolektabilitas = x.Kolektabilitas.ToString(),
                    DPD = x.DPD.ToString(),
                    TanggalBayarTerakhir = x.TanggalBayarTerakhir.ToString(),
                    TunggakanPokok = x.TunggakanPokok.ToString(),
                    TunggakanBunga = x.TunggakanBunga.ToString(),
                    TunggakanDenda = x.TunggakanDenda.ToString(),
                    TotalTunggakan = x.TotalTunggakan.ToString(),
                    TotalKewajiban = x.TotalKewajiban.ToString(),
                    JumlahAngsuran = x.JumlahAngsuran.ToString()
                })
               .ToList()));
                var Result = new GeneralResponses()
                {
                    Error = false,
                    Message = "ok",
                    Data = new ContentResponses()
                    {
                        DetaillistNasabah = Data
                    }
                };
                return (Result.Error, Result);

            }
            catch (Exception ex)
            {
                var Result = new GeneralResponses()
                {
                    Error = true,
                    Message = ex.Message
                };
                return (Result.Error, Result);
            }
        }
        public async Task<(bool Error, GeneralResponses Returns)> ListCollection(string userid)
        {
            try
            {

                var getCallBy =await _User.GetDataUser(userid);
              
                if (getCallBy.Returns == null)
                {
                    var ResultUser = new GeneralResponses()
                    {
                        Error = true,
                        Message = "User Not Found"
                    };
                    return (ResultUser.Error, ResultUser);
                }
                else
                {
                    var Id = getCallBy.Returns.Data.FirstOrDefault().iduser;
                    var Data = await master_loan.Include(i => i.master_customer)
                        .Include(i => i.rfproduct)
                        .Include(i => i.collection_call)
                        .Select(es => new ListNasabahDTO
                        {
                            AccNo = es.acc_no,
                            CallBy = es.collection_call.call_by,
                            CifNo = es.cu_cif,
                            NasabahName = es.master_customer.cu_name,
                            BranchName = es.master_customer.branch.lbrc_name,
                            Status=es.status

                        }).Where(es => es.CallBy == Id && es.Status==1).ToListAsync();
                    var Result = new GeneralResponses()
                    {
                        Error = false,
                        Message = "ok",
                        Data = new ContentResponses()
                        {
                            Nasabah = Data
                        }
                    };
                    return (Result.Error, Result);
                }
            }
            catch (Exception ex)
            {
                var Result = new GeneralResponses()
                {
                    Error = true,
                    Message = ex.Message
                };
                return (Result.Error, Result);

            }
        }

        //SERVICE YANG DIPAKAI
        public async Task<(bool? Error,  GenericResponses<MonitoringDetailRestructureDTO> Returns)> ListSearchMonitoringListDetail(SearchListRestrucutre Entity)
        {
            var wrap = new GenericResponses<MonitoringDetailRestructureDTO>
            {
                Error = false,
                Message = ""
            };
            try
            {

                var getCallBy = await _User.GetDataUser(Entity.UserId);

               
                if (getCallBy.Returns == null)
                {
                    wrap.Error = true;
                    wrap.Message = "User Not Found";
                    return (wrap.Error, wrap);
                }
                else
                {
                    var Id = getCallBy.Returns.Data.FirstOrDefault().iduser;
                    var Data = await master_loan.
                        Include(i => i.master_customer).
                        Include(i => i.collection_call).Select(es => new MonitoringDetailRestructureDTO
                    {

                        Nasabah = es.master_customer.cu_name,
                        CustomerId = es.master_customer.Id,
                        LoanId = es.id,
                        cucif = es.cu_cif,
                        acc_no = es.acc_no,
                        TotalKewajiban = es.kewajiban_total,
                        DPD = es.dpd,
                        Kolektibilitas = es.kolektibilitas,
                        CallBy = es.collection_call.call_by,
                        Status = es.status

                    }).Where(es => es.Nasabah.Contains(Entity.SearchName) ||
                        es.acc_no.Contains(Entity.SearchAccNo) &&
                        es.DPD > 90 && es.Status == 1 
                        && es.CallBy == getCallBy.Returns.Data.FirstOrDefault().iduser).ToListAsync();


                    wrap.Error = false;
                    wrap.Message = "OK";
                    wrap.Data = Data;
                    return (wrap.Error, wrap);
                }
            }
            catch (Exception ex)
            {
                wrap.Error = true;
                wrap.Message = ex.Message;
                return (wrap.Error, wrap);

            }
        }

    }
}
