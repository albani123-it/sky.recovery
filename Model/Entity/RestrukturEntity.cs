using sky.recovery.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sky.recovery.Model.Entity
{
    [Table("restructure")]
    public class RestructureEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("loan_id")]
        public int? LoanId { get; set; }

        [ForeignKey(nameof(LoanId))]
        public MasterLoanEntity? Loan { get; set; }

        [Column("mst_branch_id")]
        public int? BranchId { get; set; }

        [ForeignKey(nameof(BranchId))]
        public Branch? Branch { get; set; }

        [Column("last_update_id")]
        public int? LastUpadteById { get; set; }

        [ForeignKey(nameof(LastUpadteById))]
        public UserEntity? LastUpadteBy { get; set; }

        [Column("last_update_date")]
        public DateTime? LastUpadteDate { get; set; }

        [Column("mst_branch_pembukuan_id")]
        public int? BranchPembukuanId { get; set; }

        [ForeignKey(nameof(BranchPembukuanId))]
        public Branch? BranchPembukuan { get; set; }

        [Column("mst_branch_proses_id")]
        public int? BranchProsesId { get; set; }

        [ForeignKey(nameof(BranchProsesId))]
        public Branch? BranchProses { get; set; }

        [Column("principal_pembayaran")]
        public double? PrincipalPembayaran { get; set; }

        [Column("margin_pembayaran")]
        public double? MarginPembayaran { get; set; }

        [Column("principal_pinalty")]
        public double? PrincipalPinalty { get; set; }

        [Column("margin_pinalty")]
        public double? MarginPinalty { get; set; }

        [Column("tgl_jatuh_tempo_baru")]
        public DateTime? TglJatuhTempoBaru { get; set; }

        [Column("keterangan")]
        public string? Keterangan { get; set; }

        [Column("grace_periode")]
        public int? GracePeriode { get; set; }

        [Column("pengurangan_nilai_margin")]
        public int? PenguranganNilaiMargin { get; set; }

        [Column("tgl_awal_periode_diskon")]
        public DateTime? TglAwalPeriodeDiskon { get; set; }

        [Column("tgl_akhir_periode_diskon")]
        public DateTime? TglAkhirPeriodeDiskon { get; set; }

        [Column("periode_diskon")]
        public int? PeriodeDiskon { get; set; }

        [Column("value_date")]
        public DateTime? ValueDate { get; set; }

        [Column("disc_tunggakan_margin")]
        public double? DiskonTunggakanMargin { get; set; }

        [Column("disc_tunggakan_denda")]
        public double? DiskonTunggakanDenda { get; set; }

        [Column("margin")]
        public double? Margin { get; set; }

        [Column("denda")]
        public double? Denda { get; set; }

        [Column("margin_amount")]
        public double? MarginAmount { get; set; }

        [Column("total_diskon_margin")]
        public double? TotalDiskonMargin { get; set; }

        [Column("pola_restruk_id")]
        public int? PolaRestrukId { get; set; }

        [ForeignKey(nameof(PolaRestrukId))]
        public PolaRestruktur? PolaRestruktur { get; set; }

        [Column("pembayaran_gp_id")]
        public int? PembayaranGpId { get; set; }

        [ForeignKey(nameof(PembayaranGpId))]
        public PembayaranGp? PembayaranGp { get; set; }


        [Column("jenis_pengurangan_id")]
        public int? JenisPenguranganId { get; set; }

        [ForeignKey(nameof(JenisPenguranganId))]
        public JenisPengurangan? JenisPengurangan { get; set; }


        [Column("permasalahan")]
        public string? Permasalahan { get; set; }

        [Column("createby_id")]
        public int? CreateById { get; set; }

        [ForeignKey(nameof(CreateById))]
        public UserEntity? CreateBy { get; set; }

        [Column("create_date")]
        public DateTime? CreateDate { get; set; }

        [Column("checkby_id")]
        public int? CheckById { get; set; }

        [ForeignKey(nameof(CheckById))]
        public UserEntity? CheckBy { get; set; }

        [Column("check_date")]
        public DateTime? CheckDate { get; set; }

        [Column("approveby_id")]
        public int? ApproveById { get; set; }

        [ForeignKey(nameof(ApproveById))]
        public UserEntity? ApproveBy { get; set; }

        [Column("approve_date")]
        public DateTime? ApproveDate { get; set; }

        [Column("status_id")]
        public int? StatusId { get; set; }

        [ForeignKey(nameof(StatusId))]
        public StatusRestruktur? Status { get; set; }

        public virtual ICollection<RestructureDocumentEntity>? Document { get; set; }

        public virtual ICollection<RestructureCashFlowEntity>? CashFlow { get; set; }
    }
}