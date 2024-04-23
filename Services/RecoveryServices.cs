﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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
using System.Threading.Tasks;

namespace sky.recovery.Services
{
    public class RecoveryServices :SkyCoreConfig, IRecoveryServices
    {
        private IUserService _User { get; set; }
        public RecoveryServices(IUserService User, IOptions<DbContextSettings> appsetting) : base(appsetting)
        {
            _User = User;
        }

        public async Task<(bool Error, GeneralResponses Returns)> ListRestructure()
        {
            try
            {
                //var getCallBy = await _User.GetDataUser(userid);

                var Data =await  restructure.Include(i=>i.master_loan).Include(i=>i.status).Select(es => new ListRestructureDTO
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
                var Result = new GeneralResponses()
                {
                    Error = false,
                    Message = "ok",
                    Data=new ContentResponses()
                    {
                        RestructureDTOs=Data
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

        public async Task<(bool Error, GeneralResponses Returns)> MonitoringListDetail(string userid)
        {
            try
            {
                var getCallBy = await _User.GetDataUser(userid);

                var Data = await master_loan.Include(i => i.master_customer).Include(i=>i.collection_call).Select(es => new MonitoringDetailRestructureDTO
                {

                    Nasabah = es.master_customer.cu_name,
                    cucif = es.cu_cif,
                    acc_no = es.acc_no,
                    TotalKewajiban = es.kewajiban_total,
                    DPD = es.dpd,
                    Kolektibilitas=es.kolektibilitas,
                    CallBy=es.collection_call.call_by,
                    Status=es.status
                    
                }).Where(es=>es.DPD>90 && es.Status==1 && es.CallBy==getCallBy.Returns.DataEntities.userId).ToListAsync();
                var Result = new GeneralResponses()
                {
                    Error = false,
                    Message = "ok",
                    Data = new ContentResponses()
                    {
                        monitoringDetailRestructures = Data
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


        public async Task<(bool Error, GeneralResponses Returns)> GetRestrukturDetail(RequestRestrukturDetail Entity)
        {
            try
            {

                //ambil general info
                var Data = await (Task.Run(() => master_loan.Include(i=>i.rfproduct_segment).Include(i => i.rfproduct).Include(i => i.master_customer).Select(es => new DetailNasabah
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

                }).AsEnumerable().Where(es => es.accno == Entity.Accno)
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
                    LoanNumber=x.loannumber,
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
               .FirstOrDefault()));

                //FASILITAS LAINNYA
                var DataSegment = await (Task.Run(() => master_loan.Include(i => i.rfproduct_segment)
                .Include(i => i.rfproduct)
                .Select(es => new DetailNasabah
                {
                    cucif=es.cu_cif,
                    Segment = es.rfproduct_segment.prd_sgm_desc,
                    Product = es.rfproduct.prd_desc,
                    TanggalMulai = es.start_date,
                    TanggalJatuhTempo = es.maturity_date,
                    Tenor = es.tenor,            
                    OutStanding = es.outstanding,
                    TotalKewajiban = es.kewajiban_total,
                    JumlahAngsuran = es.installment

                }).AsEnumerable().Where(es => es.cucif == Entity.CuCif && es.Segment!=Data.Segment)
              .Select(x => new SegmentNasabahDTO
              {
                 
                  Segment = x.Segment,
                  Product = x.Product,
                  TanggalMulai = x.TanggalMulai.ToString(),
                  TanggalJatuhTempo = x.TanggalJatuhTempo.ToString(),
                  Tenor = x.Tenor.ToString(),
                  OutStanding = x.OutStanding.ToString(),
                  TotalKewajiban = x.TotalKewajiban.ToString(),
                  JumlahAngsuran = x.JumlahAngsuran.ToString()
              })
             .ToList()));

                ////PERMASALAHAN
                //var GetRestrukture = await restructure.Where(es => es.rst_id == Entity.RestrukturId)
                //    .Select(es=> new DetailRestructure { Permasalahan=es.permasalahan})
                //    .FirstOrDefaultAsync();

                //CASH FLOW
                var GetCashFlow = await restructure_cashflow.Where(es => es.rsc_restructure_id == Entity.RestrukturId).Select(es => new ListCashFlowDTO
                {
                    rsc_id=es.rsc_id,
                    rsc_penghasilan_pasangan=es.rsc_penghasilan_pasangan,
                    rsc_penghasilan_nasabah=es.rsc_penghasilan_nasabah,
                    rsc_penghasilan_lainnya=es.rsc_penghasilan_lainnya,
                    rsc_total_penghasilan=es.rsc_total_penghasilan,
                    rsc_biaya_belanja=es.rsc_biaya_belanja,
                    rsc_biaya_lainnya=es.rsc_biaya_lainnya,
                    rsc_biaya_listrik_air_telp=es.rsc_biaya_listrik_air_telp,
                    rsc_biaya_pendidikan=es.rsc_biaya_pendidikan,
                    rsc_biaya_transportasi=es.rsc_biaya_transportasi,
                    rsc_cicilan_lainnya=es.rsc_cicilan_lainnya,
                    rsc_hutang_di_bank=es.rsc_hutang_di_bank,
                    //rsc_pengasilan_bersih=es.rsc_pengasilan_bersih,
                    rsc_restructure_id=es.rsc_restructure_id,
                    rsc_rpc_70_persen=es.rsc_rpc_70_persen,
                    rsc_total_kewajiban=es.rsc_total_kewajiban,
                    rsc_total_pengeluaran=es.rsc_total_pengeluaran,
                    Permasalahan=es.restructure.permasalahan

                }).FirstOrDefaultAsync();

                var Result = new GeneralResponses()
                {
                    Error = false,
                    Message = "ok",
                    Data = new ContentResponses()
                    {
                        DetailNasabah = Data,
                        SegmentListNasabah = DataSegment,
                        //DetailRestructures=GetRestrukture,
                        CashFlowDTO=GetCashFlow
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
                    var Id = getCallBy.Returns.DataEntities.userId;
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

    }
}
