using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using sky.recovery.Libs;
using sky.recovery.Model;
using sky.recovery.Model.Entity;

namespace sky.recovery.Services
{
    public class RestrukturServices
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConfiguration conf;
        private readonly CollectiumDBContext entity;

        public RestrukturServices(IHttpContextAccessor httpContextAccessor, IConfiguration conf, CollectiumDBContext entity)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.conf = conf;
            this.entity = entity;
        }
        
        // public async Task<(bool, string)> UploadDokumenRestruktur(UploadDocumentRestructure file)
        // {
        //     try
        //     {
        //
        //         if (file == null)
        //         {
        //             return (false, "Data tidak ditemukan");
        //         }
        //
        //         if (file.File == null)
        //         {
        //             return (false, "Data tidak ditemukan");
        //         }
        //
        //         if (file.DocTypeId == null || file.DocTypeId < 0)
        //         {
        //             return (false, "Tipe Dokumen tidak ditemukan");
        //         }
        //
        //         // var dc = this.ctx.DocumentRestruktur.Find(filter.DocTypeId);
        //         // if (dc == null)
        //         // {
        //         //     wrap.Message = "Tipe Dokumen tidak ditemukan di sistem";
        //         //     return wrap;
        //         // }
        //
        //         var userId = this.httpContextAccessor.HttpContext.Items["userId"];
        //         if (userId is null)
        //         {
        //             return (false, "User tidak ditemukan");
        //         }
        //
        //         var path = conf["PhotoPath"];
        //         path = path + "/restructdok";
        //         if (!Directory.Exists(path))
        //         {
        //             Directory.CreateDirectory(path);
        //         }
        //
        //         var cp = new RestructureDocument();
        //         cp.CreateDate = DateTime.Today;
        //         cp.UserId = userId;
        //         cp.Mime = filter.File.ContentType.ToString();
        //         cp.DocumentResutrukturId = filter.DocTypeId;
        //         this.ctx.RestructureDocument.Add(cp);
        //         this.ctx.SaveChanges();
        //
        //         string ext = Path.GetExtension(file.File.FileName);
        //         
        //         var nm = path + "/" + cp.Id.ToString() + ext;
        //         
        //         using (FileStream filestream = System.IO.File.Create(nm))
        //         {
        //             file.File.CopyTo(filestream);
        //             filestream.Flush();
        //             //  return "\\Upload\\" + objFile.files.FileName;
        //         }
        //         
        //         var url = "restructdok/" + cp.Id.ToString() + ext;
        //         cp.Url = url;
        //         this.ctx.RestructureDocument.Update(cp);
        //         this.ctx.SaveChanges(true);
        //         
        //         wrap.Status = true;
        //         wrap.AddData(cp.Id!.Value);
        //         
        //         return (true, "Berhasil Upload Dokumen");
        //     }
        //     catch (Exception e)
        //     {
        //         Console.WriteLine(e);
        //         throw;
        //     }
        // }

        public async Task<(bool, string)> Create(CreateRestructure param)
        {

            var reqUser = this.httpContextAccessor.HttpContext!.Items["User"] as UserEntity;
            if (reqUser == null)
            {
                return (false, "User Tidak Ditemukan");
            }

            var pr = Validation.IlKeiValidator.Instance.WithPoCo(param)
                .Pick(nameof(param.LoanId)).IsMandatory().AsInteger().Pack()
                .Validate();

            if (pr.Result == false)
            {
                return (false, pr.Message);
            }

            var CekLoan = this.entity.MasterLoan.Find(param.LoanId);
            if (CekLoan is null)
            {
                return (false, "Data Loan tidak ditemukan di sistem");
            }

            if (param.PolaRestrukId != null)
            {
                var vpr = this.entity.PolaRestruktur.Find(param.PolaRestrukId);
                if (vpr == null)
                {
                    return (false, "Data Pola Restruktur tidak ditemukan di sistem");
                }
            }

            if (param.PembayaranGpId != null) { 
                var vpg = this.entity.PembayaranGp.Find(param.PembayaranGpId);
                if (vpg != null)
                {
                    return (false, "Data Pembayaran Grace Periode tidak ditemukan di sistem");
                }
            }

            this.entity.Entry(CekLoan).Reference(r => r.Customer).Load();

            var ne = new RestructureEntity();
            ne.Denda = param.Denda;
            ne.DiskonTunggakanDenda = param.DiskonTunggakanDenda;
            ne.DiskonTunggakanMargin = param.DiskonTunggakanMargin;
            ne.JenisPenguranganId = param.JenisPenguranganId;
            ne.Keterangan = param.Keterangan;
            ne.LoanId = param.LoanId;
            ne.MarginAmount = param.MarginAmount;
            ne.Margin = param.Margin;
            ne.MarginPembayaran = param.MarginPembayaran;
            ne.MarginPinalty = param.MarginPinalty;
            ne.PembayaranGpId = param.PembayaranGpId;
            ne.PenguranganNilaiMargin = param.PenguranganNilaiMargin;
            ne.PeriodeDiskon = param.PeriodeDiskon;
            ne.PolaRestrukId = param.PolaRestrukId;
            ne.PrincipalPembayaran = param.PrincipalPembayaran;
            ne.PrincipalPinalty = param.PrincipalPinalty;
            ne.TglJatuhTempoBaru = param.TglJatuhTempoBaru;
            ne.TotalDiskonMargin = param.TotalDiskonMargin;
            ne.ValueDate = param.ValueDate;
            ne.BranchId = CekLoan.Customer?.BranchId;
            ne.BranchPembukuanId = CekLoan.Customer?.BranchId;
            ne.BranchProsesId = CekLoan.Customer?.BranchId;

            //var json = JsonSerializer.Deserialize<List<string>>(bean.Permasalahan!);

            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            string json = JsonSerializer.Serialize(param.Permasalahan, serializeOptions);
            ne.Permasalahan = json;

            ne.CreateBy = reqUser;
            ne.CreateDate = DateTime.Now;
            string name = "DRAFT";
            ne.Status = this.entity.StatusRestruktur.Where(q => q.Name.Equals(name)).FirstOrDefault();
            this.entity.Restructure.Add(ne);
            this.entity.SaveChanges();

            if (param.CashFlow != null)
            {
                var cf = param.CashFlow;
                var nrcf = new RestructureCashFlowEntity();
                nrcf.PenghasilanNasabah = cf.PenghasilanNasabah;
                nrcf.PenghasilanPasangan = cf.PenghasilanPasangan;
                nrcf.PenghasilanLainnya = cf.PenghasilanLainnya;
                nrcf.TotalPenghasilan = cf.TotalPenghasilan;
                nrcf.BiayaPendidikan = cf.BiayaPendidikan;
                nrcf.BiayaListrikAirTelp = cf.BiayaListrikAirTelp;
                nrcf.BiayaBelanja = cf.BiayaBelanja;
                nrcf.BiayaTransportasi = cf.BiayaTransportasi;
                nrcf.BiayaLainnya = cf.BiayaLainnya;
                nrcf.TotalPengeluaran = cf.TotalPengeluaran;
                nrcf.HutangDiBank = cf.HutangDiBank;
                nrcf.CicilanLainnya = cf.CicilanLainnya;
                nrcf.TotalKewajiban = cf.TotalKewajiban;
                nrcf.PenghasilanBersih = cf.PenghasilanBersih;
                nrcf.Rpc70Persen = cf.Rpc70Persen;
                nrcf.RestructureId = ne.Id;
                this.entity.RestructureCashFlow.Add(nrcf);
                this.entity.SaveChanges();
            }


            var appr = new RestructureApprovalEntity();
            // appr.Execution = this.statusService.GetRecoveryExecution("DRAFT");
            appr.Execution = this.entity.RecoveryExecution.Where(q => q.Name.Equals(name)).FirstOrDefault();
            appr.Sender = reqUser;
            appr.Recipient = reqUser;
            appr.CreateDate = DateTime.Now;
            appr.RestructureId = ne.Id;
            this.entity.RestructureApproval.Add(appr);
            this.entity.SaveChanges();


            if (param.Document != null)
            {
                foreach (var item in param.Document)
                {
                    var doc = this.entity.RestructureDocument.Find(item);
                    if (doc != null)
                    {
                        doc.RestructureId = ne.Id;
                        this.entity.RestructureDocument.Update(doc);
                        this.entity.SaveChanges();
                    }
                }
            }
            return (true, "Berhasil Simpan Data");
        }

    }
}