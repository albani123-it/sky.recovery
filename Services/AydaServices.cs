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

namespace sky.recovery.Services
{
    public class AydaServices : SkyCoreConfig, IAydaServices
    {

        private IUserService _User { get; set; }
        private IGeneralParam _GeneralParam { get; set; }
        private readonly IWebHostEnvironment _environment;
        private IRestruktureRepository _postgreRepository { get; set; }
        private IHelperRepository _helperRepository { get; set; }

        ModellingGeneralResponsesV2 _DataResponses = new ModellingGeneralResponsesV2();
        AydaHelper _aydahelper = new AydaHelper();
        public AydaServices(IGeneralParam GeneralParam, IWebHostEnvironment environment, IUserService User, IHelperRepository helperRepository, IRestruktureRepository postgreRepository,
      IOptions<DbContextSettings> appsetting) : base(appsetting)
        {
            _environment = environment;
            _GeneralParam = GeneralParam;
            _User = User;
            _postgreRepository = postgreRepository;
            _helperRepository = helperRepository;

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
                    GetData.statusid = status.Where(es => es.sts_name == "REQUESTED").Select(es => es.sts_id).FirstOrDefault();
                    GetData.createdby = getCallBy.Returns.Data.FirstOrDefault().iduser;
                    GetData.lastupdatedate = DateTime.Now;

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
                        statusid = status.Where(es => es.sts_name == "REQUESTED").Select(es => es.sts_id).FirstOrDefault(),
                        createdby = getCallBy.Returns.Data.FirstOrDefault().iduser,
                        createddated = DateTime.Now
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
                        createddated = DateTime.Now
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
                        cabang=es.master_loan.master_customer.branch.lbrc_name,
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

    }
}
