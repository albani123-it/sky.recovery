using Microsoft.Extensions.Options;
using sky.recovery.DTOs.ResponsesDTO;
using sky.recovery.Helper.Config;
using sky.recovery.Insfrastructures;
using sky.recovery.Interfaces;
using sky.recovery.Responses;
using System.Threading.Tasks;
using System;
using sky.recovery.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace sky.recovery.Services
{
    public class AydaServices : SkyCoreConfig, IAydaServices
    {
        private IUserService _User { get; set; }
        public AydaServices(IUserService User, IOptions<DbContextSettings> appsetting) : base(appsetting)
        {
            _User = User;


        }

        //AYDA MONITORING SERVICE
        public async Task<(bool? Error, GenericResponses<aydaDTO> Returns)> ListMonitoringAyda()
        {
            var wrap = new GenericResponses<aydaDTO>
            {
                Error = false,
                Message = ""
            };
            try
            {
                var Data = await ayda.Include(i=>i.master_loan)
                    .Include(i=>i.status).
                    Include(i=>i.master_loan.master_customer).
                    Select(es => new aydaDTO
                {
                    id = es.id,
                    cucif=es.master_loan.cu_cif,
                    accno=es.master_loan.acc_no,
                    Cabang = es.branch.lbrc_name,
                    loannumber = es.master_loan.loan_number,
                    nasabah = es.master_loan.master_customer.cu_name,
                    totaltunggakan = es.master_loan.tunggakan_total,
                    status = es.status.sts_name


                }).ToListAsync();
                wrap.Error = false;
                wrap.Message = "OK";
                wrap.Data = Data;

                return (wrap.Error, wrap);
            }
            catch (Exception ex)
            {
                wrap.Error = false;
                wrap.Message = ex.Message;
                return (wrap.Error, wrap);
            }
        }

        //AYDA TASK LIST SERVICE
        public async Task<(bool? Error, GenericResponses<aydaDTO> Returns)> ListTaskListAyda(string userid)
        {
            string FeedStatus = "";
            var wrap = new GenericResponses<aydaDTO>
            {
                Error = false,
                Message = ""
            };
            try
            {

                var getCallBy = await _User.GetDataUser(userid);

                if (getCallBy.Returns.Data.FirstOrDefault().role == "ADMIN")
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

                var Data = await ayda.Include(i => i.master_loan)
                    .Include(i => i.status).
                    Include(i => i.master_loan.master_customer).
                    Select(es => new aydaDTO
                    {
                        id = es.id,
                        cucif = es.master_loan.cu_cif,
                        accno = es.master_loan.acc_no,
                        Cabang = es.branch.lbrc_name,
                        loannumber = es.master_loan.loan_number,
                        nasabah = es.master_loan.master_customer.cu_name,
                        totaltunggakan = es.master_loan.tunggakan_total,
                        status = es.status.sts_name


                    }).Where(es => es.status == FeedStatus).ToListAsync();
                wrap.Error = false;
                wrap.Message = "OK";
                wrap.Data = Data;

                return (wrap.Error, wrap);
            }
            catch (Exception ex)
            {
                wrap.Error = false;
                wrap.Message = ex.Message;
                return (wrap.Error, wrap);
            }
        }


    }
}
