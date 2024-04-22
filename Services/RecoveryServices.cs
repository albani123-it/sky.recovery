using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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

        public async Task<(bool Error, GeneralResponses Returns)> ListRestructure(string userid)
        {
            try
            {
                var getCallBy = await _User.GetDataUser(userid);

                var Data =await  restructure.Include(i=>i.master_loan).Select(es => new ListRestructureDTO
                {
                    createby_id = es.createby_id,
                    Nasabah=es.master_loan.master_customer.cu_name,
                    cucif=es.master_loan.cu_cif,
                    acc_no=es.master_loan.acc_no,
                    rst_id=es.rst_id,
                    rst_loan_id=es.rst_loan_id,
                    BranchName=es.master_loan.master_customer.branch.lbrc_name,
                    principal_pembayaran=es.principal_pembayaran,
                    principal_pinalty=es.principal_pinalty,
                    keterangan=es.keterangan,
                    tgl_akhir_periode_diskon=es.tgl_akhir_periode_diskon,
                    tgl_awal_periode_diskon=es.tgl_awal_periode_diskon,
                    tgl_jatuh_tempo_baru=es.tgl_jatuh_tempo_baru,
                    grace_periode=es.grace_periode,
                    pengurangan_nilai_margin=es.pengurangan_nilai_margin,
                    value_date=es.value_date,
                    disc_tunggakan_denda=es.disc_tunggakan_denda,
                    denda=es.denda,
                    disc_tunggakan_margin=es.disc_tunggakan_margin,
                    margin=es.margin,
                    margin_amount=es.margin_amount,
                    margin_pembayaran=es.margin_pembayaran,
                    margin_pinalty=es.margin_pinalty,
                    total_diskon_margin=es.total_diskon_margin,
                    periode_diskon=es.periode_diskon,
                    permasalahan=es.permasalahan,
                    create_date=es.create_date,
                    rst_status_id=es.rst_status_id
                }).Where(es => es.createby_id == getCallBy.Returns.DataEntities.userId).ToListAsync();
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
