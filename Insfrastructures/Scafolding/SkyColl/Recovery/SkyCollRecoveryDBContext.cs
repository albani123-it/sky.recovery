using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Recovery
{
    public partial class SkyCollRecoveryDBContext : DbContext
    {
        public SkyCollRecoveryDBContext()
        {
        }

        public SkyCollRecoveryDBContext(DbContextOptions<SkyCollRecoveryDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Auction> Auction { get; set; }
        public virtual DbSet<Auctionapproval> Auctionapproval { get; set; }
        public virtual DbSet<Auctiondocument> Auctiondocument { get; set; }
        public virtual DbSet<Ayda> Ayda { get; set; }
        public virtual DbSet<Generalparamdetail> Generalparamdetail { get; set; }
        public virtual DbSet<Generalparamheader> Generalparamheader { get; set; }
        public virtual DbSet<Insurance> Insurance { get; set; }
        public virtual DbSet<Insuranceapproval> Insuranceapproval { get; set; }
        public virtual DbSet<Insurancedocument> Insurancedocument { get; set; }
        public virtual DbSet<Masterdocrule> Masterdocrule { get; set; }
        public virtual DbSet<Masterdocumenttype> Masterdocumenttype { get; set; }
        public virtual DbSet<Masterflow> Masterflow { get; set; }
        public virtual DbSet<Masterflowengine> Masterflowengine { get; set; }
        public virtual DbSet<Masterlayoutposition> Masterlayoutposition { get; set; }
        public virtual DbSet<Masterrepository> Masterrepository { get; set; }
        public virtual DbSet<Mastertemplate> Mastertemplate { get; set; }
        public virtual DbSet<Masterworkflow> Masterworkflow { get; set; }
        public virtual DbSet<Masterworkflowengine> Masterworkflowengine { get; set; }
        public virtual DbSet<Masterworkflowrule> Masterworkflowrule { get; set; }
        public virtual DbSet<Permasalahanrestrukture> Permasalahanrestrukture { get; set; }
        public virtual DbSet<Recoveryreverselog> Recoveryreverselog { get; set; }
        public virtual DbSet<Restructurecashflow> Restructurecashflow { get; set; }
        public virtual DbSet<Restrukture> Restrukture { get; set; }
        public virtual DbSet<Restrukturedokumen> Restrukturedokumen { get; set; }
        public virtual DbSet<Templatedetail> Templatedetail { get; set; }
        public virtual DbSet<Workflow> Workflow { get; set; }
        public virtual DbSet<Workflowhistory> Workflowhistory { get; set; }
        public virtual DbSet<Writeoff> Writeoff { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=103.53.197.67;Database=sky.coll;User Id=postgres;Password=User123!");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "English_United States.1252");

            modelBuilder.Entity<Auction>(entity =>
            {
                entity.ToTable("auction", "RecoveryBusinessV2");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Alasanlelangid).HasColumnName("alasanlelangid");

                entity.Property(e => e.Balailelangid).HasColumnName("balailelangid");

                entity.Property(e => e.Biayalelang).HasColumnName("biayalelang");

                entity.Property(e => e.Catatanlelang)
                    .HasMaxLength(500)
                    .HasColumnName("catatanlelang");

                entity.Property(e => e.Createdby).HasColumnName("createdby");

                entity.Property(e => e.Createddated)
                    .HasColumnType("date")
                    .HasColumnName("createddated");

                entity.Property(e => e.Isactive).HasColumnName("isactive");

                entity.Property(e => e.Jenislelangid).HasColumnName("jenislelangid");

                entity.Property(e => e.Keterangan)
                    .HasMaxLength(900)
                    .HasColumnName("keterangan");

                entity.Property(e => e.Lastupdatedate).HasColumnName("lastupdatedate");

                entity.Property(e => e.Lastupdatedid).HasColumnName("lastupdatedid");

                entity.Property(e => e.Loanid).HasColumnName("loanid");

                entity.Property(e => e.Mstbanchprosesid).HasColumnName("mstbanchprosesid");

                entity.Property(e => e.Mstbranchid).HasColumnName("mstbranchid");

                entity.Property(e => e.Mstbranchpembukuanid).HasColumnName("mstbranchpembukuanid");

                entity.Property(e => e.Namarekening)
                    .HasMaxLength(900)
                    .HasColumnName("namarekening");

                entity.Property(e => e.Nilailimitlelang).HasColumnName("nilailimitlelang");

                entity.Property(e => e.Nopk)
                    .HasMaxLength(500)
                    .HasColumnName("nopk");

                entity.Property(e => e.Norekening)
                    .HasMaxLength(900)
                    .HasColumnName("norekening");

                entity.Property(e => e.Objeklelang)
                    .HasMaxLength(500)
                    .HasColumnName("objeklelang");

                entity.Property(e => e.Statusid).HasColumnName("statusid");

                entity.Property(e => e.Tatacaralelang)
                    .HasMaxLength(500)
                    .HasColumnName("tatacaralelang");

                entity.Property(e => e.Tglpenetapanlelang).HasColumnName("tglpenetapanlelang");

                entity.Property(e => e.Uangjaminan).HasColumnName("uangjaminan");
            });

            modelBuilder.Entity<Auctionapproval>(entity =>
            {
                entity.ToTable("auctionapproval", "RecoveryBusinessV2");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Auctionid).HasColumnName("auctionid");

                entity.Property(e => e.Createddated)
                    .HasColumnType("date")
                    .HasColumnName("createddated");

                entity.Property(e => e.Executionid).HasColumnName("executionid");

                entity.Property(e => e.Komentar).HasColumnName("komentar");

                entity.Property(e => e.Recipientid).HasColumnName("recipientid");

                entity.Property(e => e.Recipientroleid).HasColumnName("recipientroleid");

                entity.Property(e => e.Senderid).HasColumnName("senderid");

                entity.Property(e => e.Senderroleid).HasColumnName("senderroleid");
            });

            modelBuilder.Entity<Auctiondocument>(entity =>
            {
                entity.ToTable("auctiondocument", "RecoveryBusinessV2");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Auctionid).HasColumnName("auctionid");

                entity.Property(e => e.Descriptions)
                    .HasMaxLength(800)
                    .HasColumnName("descriptions");

                entity.Property(e => e.Doctypeid).HasColumnName("doctypeid");

                entity.Property(e => e.Lat).HasColumnName("lat");

                entity.Property(e => e.Lon).HasColumnName("lon");

                entity.Property(e => e.Mime).HasColumnName("mime");

                entity.Property(e => e.Title)
                    .HasMaxLength(800)
                    .HasColumnName("title");

                entity.Property(e => e.Uploadedby).HasColumnName("uploadedby");

                entity.Property(e => e.Uploadeddated)
                    .HasColumnType("date")
                    .HasColumnName("uploadeddated");

                entity.Property(e => e.Url)
                    .HasMaxLength(800)
                    .HasColumnName("url");
            });

            modelBuilder.Entity<Ayda>(entity =>
            {
                entity.ToTable("ayda", "RecoveryBusinessV2");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Createdby).HasColumnName("createdby");

                entity.Property(e => e.Createddated)
                    .HasColumnType("date")
                    .HasColumnName("createddated");

                entity.Property(e => e.Hubunganbankid).HasColumnName("hubunganbankid");

                entity.Property(e => e.Isactive).HasColumnName("isactive");

                entity.Property(e => e.Jumlahayda).HasColumnName("jumlahayda");

                entity.Property(e => e.Kualitas)
                    .HasMaxLength(700)
                    .HasColumnName("kualitas");

                entity.Property(e => e.Lastupdatedate).HasColumnName("lastupdatedate");

                entity.Property(e => e.Lastupdatedid).HasColumnName("lastupdatedid");

                entity.Property(e => e.Loanid).HasColumnName("loanid");

                entity.Property(e => e.Mstbanchprosesid).HasColumnName("mstbanchprosesid");

                entity.Property(e => e.Mstbranchid).HasColumnName("mstbranchid");

                entity.Property(e => e.Mstbranchpembukuanid).HasColumnName("mstbranchpembukuanid");

                entity.Property(e => e.Nilaimargin).HasColumnName("nilaimargin");

                entity.Property(e => e.Nilaipembiayaanpokok).HasColumnName("nilaipembiayaanpokok");

                entity.Property(e => e.Nilaiperolehanagunan).HasColumnName("nilaiperolehanagunan");

                entity.Property(e => e.Perkiraanbiayajual).HasColumnName("perkiraanbiayajual");

                entity.Property(e => e.Ppa).HasColumnName("ppa");

                entity.Property(e => e.Statusid).HasColumnName("statusid");

                entity.Property(e => e.Tglambilalih).HasColumnName("tglambilalih");
            });

            modelBuilder.Entity<Generalparamdetail>(entity =>
            {
                entity.ToTable("generalparamdetail", "RecoveryBusinessV2");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Createdby).HasColumnName("createdby");

                entity.Property(e => e.Createddated)
                    .HasColumnType("date")
                    .HasColumnName("createddated");

                entity.Property(e => e.Descriptions).HasColumnName("descriptions");

                entity.Property(e => e.Paramheaderid).HasColumnName("paramheaderid");

                entity.Property(e => e.Title).HasColumnName("title");

                entity.Property(e => e.Updatedby).HasColumnName("updatedby");

                entity.Property(e => e.Updateddated)
                    .HasColumnType("date")
                    .HasColumnName("updateddated");
            });

            modelBuilder.Entity<Generalparamheader>(entity =>
            {
                entity.ToTable("generalparamheader", "RecoveryBusinessV2");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Createdby).HasColumnName("createdby");

                entity.Property(e => e.Createddated)
                    .HasColumnType("date")
                    .HasColumnName("createddated");

                entity.Property(e => e.Descriptions).HasColumnName("descriptions");

                entity.Property(e => e.Title).HasColumnName("title");

                entity.Property(e => e.Updatedby).HasColumnName("updatedby");

                entity.Property(e => e.Updateddated)
                    .HasColumnType("date")
                    .HasColumnName("updateddated");
            });

            modelBuilder.Entity<Insurance>(entity =>
            {
                entity.ToTable("insurance", "RecoveryBusinessV2");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Asuransiid).HasColumnName("asuransiid");

                entity.Property(e => e.Asuransisisaklaimid).HasColumnName("asuransisisaklaimid");

                entity.Property(e => e.Bakidebitklaim).HasColumnName("bakidebitklaim");

                entity.Property(e => e.Catatanklaim).HasColumnName("catatanklaim");

                entity.Property(e => e.Catatanpolis).HasColumnName("catatanpolis");

                entity.Property(e => e.Createdby).HasColumnName("createdby");

                entity.Property(e => e.Createddated).HasColumnName("createddated");

                entity.Property(e => e.Isactive).HasColumnName("isactive");

                entity.Property(e => e.Jabatan).HasColumnName("jabatan");

                entity.Property(e => e.Keterangan).HasColumnName("keterangan");

                entity.Property(e => e.Lastupdateddated)
                    .HasColumnType("date")
                    .HasColumnName("lastupdateddated");

                entity.Property(e => e.Lastupdatedid).HasColumnName("lastupdatedid");

                entity.Property(e => e.Loanid).HasColumnName("loanid");

                entity.Property(e => e.Mstbranchid).HasColumnName("mstbranchid");

                entity.Property(e => e.Mstbranchpembukuanid).HasColumnName("mstbranchpembukuanid");

                entity.Property(e => e.Mstbranchprosesid).HasColumnName("mstbranchprosesid");

                entity.Property(e => e.Namapejabat).HasColumnName("namapejabat");

                entity.Property(e => e.Nilaiklaim).HasColumnName("nilaiklaim");

                entity.Property(e => e.Nilaiklaimdibayar).HasColumnName("nilaiklaimdibayar");

                entity.Property(e => e.Nilaitunggakanbunga).HasColumnName("nilaitunggakanbunga");

                entity.Property(e => e.Nilaitunggakanpokok).HasColumnName("nilaitunggakanpokok");

                entity.Property(e => e.Nopk)
                    .HasMaxLength(900)
                    .HasColumnName("nopk");

                entity.Property(e => e.Nopolis)
                    .HasMaxLength(900)
                    .HasColumnName("nopolis");

                entity.Property(e => e.Nosertifikat).HasColumnName("nosertifikat");

                entity.Property(e => e.Permasalahan).HasColumnName("permasalahan");

                entity.Property(e => e.Statusid).HasColumnName("statusid");

                entity.Property(e => e.Tglpolis).HasColumnName("tglpolis");

                entity.Property(e => e.Tglsertifikat).HasColumnName("tglsertifikat");
            });

            modelBuilder.Entity<Insuranceapproval>(entity =>
            {
                entity.ToTable("insuranceapproval", "RecoveryBusinessV2");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Createddated)
                    .HasColumnType("date")
                    .HasColumnName("createddated");

                entity.Property(e => e.Executionid).HasColumnName("executionid");

                entity.Property(e => e.Insuranceid).HasColumnName("insuranceid");

                entity.Property(e => e.Komentar).HasColumnName("komentar");

                entity.Property(e => e.Recipientid).HasColumnName("recipientid");

                entity.Property(e => e.Recipientroleid).HasColumnName("recipientroleid");

                entity.Property(e => e.Senderid).HasColumnName("senderid");

                entity.Property(e => e.Senderroleid).HasColumnName("senderroleid");
            });

            modelBuilder.Entity<Insurancedocument>(entity =>
            {
                entity.ToTable("insurancedocument", "RecoveryBusinessV2");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Doctypeid).HasColumnName("doctypeid");

                entity.Property(e => e.Insuranceid).HasColumnName("insuranceid");

                entity.Property(e => e.Lat).HasColumnName("lat");

                entity.Property(e => e.Lon).HasColumnName("lon");

                entity.Property(e => e.Mime).HasColumnName("mime");

                entity.Property(e => e.Title).HasColumnName("title");

                entity.Property(e => e.Upladeddated)
                    .HasColumnType("date")
                    .HasColumnName("upladeddated");

                entity.Property(e => e.Uploadedby).HasColumnName("uploadedby");

                entity.Property(e => e.Url).HasColumnName("url");
            });

            modelBuilder.Entity<Masterdocrule>(entity =>
            {
                entity.ToTable("masterdocrule", "RecoveryBusinessV2");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Tipedoc).HasColumnName("tipedoc");

                entity.Property(e => e.Tipepage).HasColumnName("tipepage");
            });

            modelBuilder.Entity<Masterdocumenttype>(entity =>
            {
                entity.ToTable("masterdocumenttype", "RecoveryBusinessV2");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description).HasColumnName("description");
            });

            modelBuilder.Entity<Masterflow>(entity =>
            {
                entity.ToTable("masterflow", "RecoveryBusinessV2");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Branchid).HasColumnName("branchid");

                entity.Property(e => e.Descriptions).HasColumnName("descriptions");

                entity.Property(e => e.Fiturid).HasColumnName("fiturid");

                entity.Property(e => e.Masterworkflowid).HasColumnName("masterworkflowid");

                entity.Property(e => e.Orders).HasColumnName("orders");

                entity.Property(e => e.Roleid).HasColumnName("roleid");
            });

            modelBuilder.Entity<Masterflowengine>(entity =>
            {
                entity.ToTable("masterflowengine", "RecoveryBusinessV2");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Fiturid).HasColumnName("fiturid");

                entity.Property(e => e.Flowid).HasColumnName("flowid");

                entity.Property(e => e.Nodesid).HasColumnName("nodesid");

                entity.Property(e => e.Orders).HasColumnName("orders");

                entity.Property(e => e.Title).HasColumnName("title");
            });

            modelBuilder.Entity<Masterlayoutposition>(entity =>
            {
                entity.ToTable("masterlayoutposition", "RecoveryBusinessV2");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description).HasColumnName("description");
            });

            modelBuilder.Entity<Masterrepository>(entity =>
            {
                entity.ToTable("masterrepository", "RecoveryBusinessV2");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Doctype).HasColumnName("doctype");

                entity.Property(e => e.Filename).HasColumnName("filename");

                entity.Property(e => e.Fileurl).HasColumnName("fileurl");

                entity.Property(e => e.Fiturid).HasColumnName("fiturid");

                entity.Property(e => e.Isactive).HasColumnName("isactive");

                entity.Property(e => e.Keterangan).HasColumnName("keterangan");

                entity.Property(e => e.Modifydated).HasColumnName("modifydated");

                entity.Property(e => e.Requestid).HasColumnName("requestid");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Uploaddated).HasColumnName("uploaddated");

                entity.Property(e => e.Userid).HasColumnName("userid");
            });

            modelBuilder.Entity<Mastertemplate>(entity =>
            {
                entity.ToTable("mastertemplate", "RecoveryBusinessV2");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Createdby).HasColumnName("createdby");

                entity.Property(e => e.Createddated).HasColumnName("createddated");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Documenttype).HasColumnName("documenttype");

                entity.Property(e => e.Fiturid).HasColumnName("fiturid");

                entity.Property(e => e.Isactive).HasColumnName("isactive");

                entity.Property(e => e.Modifyby).HasColumnName("modifyby");

                entity.Property(e => e.Modifydated).HasColumnName("modifydated");

                entity.Property(e => e.Title).HasColumnName("title");
            });

            modelBuilder.Entity<Masterworkflow>(entity =>
            {
                entity.ToTable("masterworkflow", "RecoveryBusinessV2");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Fiturid).HasColumnName("fiturid");
            });

            modelBuilder.Entity<Masterworkflowengine>(entity =>
            {
                entity.ToTable("masterworkflowengine", "RecoveryBusinessV2");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Fiturid).HasColumnName("fiturid");

                entity.Property(e => e.Flowid).HasColumnName("flowid");

                entity.Property(e => e.Isactive).HasColumnName("isactive");

                entity.Property(e => e.Wfcode).HasColumnName("wfcode");

                entity.Property(e => e.Wfname).HasColumnName("wfname");
            });

            modelBuilder.Entity<Masterworkflowrule>(entity =>
            {
                entity.ToTable("masterworkflowrule", "RecoveryBusinessV2");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Conditionvalue).HasColumnName("conditionvalue");

                entity.Property(e => e.Conditionvalueint).HasColumnName("conditionvalueint");

                entity.Property(e => e.Fiturid).HasColumnName("fiturid");

                entity.Property(e => e.Isactive).HasColumnName("isactive");

                entity.Property(e => e.Masterworkflowid).HasColumnName("masterworkflowid");

                entity.Property(e => e.Operators).HasColumnName("operators");

                entity.Property(e => e.Value).HasColumnName("value");

                entity.Property(e => e.Variabel).HasColumnName("variabel");
            });

            modelBuilder.Entity<Permasalahanrestrukture>(entity =>
            {
                entity.ToTable("permasalahanrestrukture", "RecoveryBusinessV2");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Createdby).HasColumnName("createdby");

                entity.Property(e => e.Createddated).HasColumnName("createddated");

                entity.Property(e => e.Descriptions).HasColumnName("descriptions");

                entity.Property(e => e.Restruktureid).HasColumnName("restruktureid");
            });

            modelBuilder.Entity<Recoveryreverselog>(entity =>
            {
                entity.HasKey(e => e.RrlId)
                    .HasName("recoveryreverselog_pkey");

                entity.ToTable("recoveryreverselog", "RecoveryBusinessV2");

                entity.Property(e => e.RrlId).HasColumnName("rrl_id");

                entity.Property(e => e.RrlCreatedby).HasColumnName("rrl_createdby");

                entity.Property(e => e.RrlData).HasColumnName("rrl_data");

                entity.Property(e => e.RrlDated).HasColumnName("rrl_dated");

                entity.Property(e => e.Versions).HasColumnName("versions");
            });

            modelBuilder.Entity<Restructurecashflow>(entity =>
            {
                entity.ToTable("restructurecashflow", "RecoveryBusinessV2");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('\"RecoveryBusinessV2\".restrukturecashflow_id_seq'::regclass)");

                entity.Property(e => e.Biayabelanja).HasColumnName("biayabelanja");

                entity.Property(e => e.Biayalainnya).HasColumnName("biayalainnya");

                entity.Property(e => e.Biayalistrkairtelp).HasColumnName("biayalistrkairtelp");

                entity.Property(e => e.Biayapendidikan).HasColumnName("biayapendidikan");

                entity.Property(e => e.Biayatransportasi).HasColumnName("biayatransportasi");

                entity.Property(e => e.Cicilanlainnya).HasColumnName("cicilanlainnya");

                entity.Property(e => e.Createdby).HasColumnName("createdby");

                entity.Property(e => e.Createddated).HasColumnName("createddated");

                entity.Property(e => e.Hutangdibank).HasColumnName("hutangdibank");

                entity.Property(e => e.Pengeluarantotal).HasColumnName("pengeluarantotal");

                entity.Property(e => e.Penghasilanbersih).HasColumnName("penghasilanbersih");

                entity.Property(e => e.Penghasilanlainnya).HasColumnName("penghasilanlainnya");

                entity.Property(e => e.Penghasilannasabah).HasColumnName("penghasilannasabah");

                entity.Property(e => e.Penghasilanpasangan).HasColumnName("penghasilanpasangan");

                entity.Property(e => e.Restruktureid).HasColumnName("restruktureid");

                entity.Property(e => e.Rpc70persen).HasColumnName("rpc70persen");

                entity.Property(e => e.Totalkewajiban).HasColumnName("totalkewajiban");

                entity.Property(e => e.Totalpenghasilan).HasColumnName("totalpenghasilan");
            });

            modelBuilder.Entity<Restrukture>(entity =>
            {
                entity.ToTable("restrukture", "RecoveryBusinessV2");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Approvedby).HasColumnName("approvedby");

                entity.Property(e => e.Approver).HasColumnName("approver");

                entity.Property(e => e.Approverrole).HasColumnName("approverrole");

                entity.Property(e => e.Checkby).HasColumnName("checkby");

                entity.Property(e => e.Checkdate)
                    .HasColumnType("date")
                    .HasColumnName("checkdate");

                entity.Property(e => e.Createdby).HasColumnName("createdby");

                entity.Property(e => e.Createddated).HasColumnName("createddated");

                entity.Property(e => e.Denda).HasColumnName("denda");

                entity.Property(e => e.Disctunggakandenda).HasColumnName("disctunggakandenda");

                entity.Property(e => e.Disctunggakanmargin).HasColumnName("disctunggakanmargin");

                entity.Property(e => e.Graceperiode).HasColumnName("graceperiode");

                entity.Property(e => e.Isactive).HasColumnName("isactive");

                entity.Property(e => e.Iscancel).HasColumnName("iscancel");

                entity.Property(e => e.Jenispenguranganid).HasColumnName("jenispenguranganid");

                entity.Property(e => e.Keterangan)
                    .HasMaxLength(800)
                    .HasColumnName("keterangan");

                entity.Property(e => e.Lastupdatedate)
                    .HasColumnType("date")
                    .HasColumnName("lastupdatedate");

                entity.Property(e => e.Lastupdatedid).HasColumnName("lastupdatedid");

                entity.Property(e => e.Loanid).HasColumnName("loanid");

                entity.Property(e => e.Margin).HasColumnName("margin");

                entity.Property(e => e.Marginpembayaran).HasColumnName("marginpembayaran");

                entity.Property(e => e.Marginpinalty).HasColumnName("marginpinalty");

                entity.Property(e => e.Mstbranchid).HasColumnName("mstbranchid");

                entity.Property(e => e.Mstbranchpembukuanid).HasColumnName("mstbranchpembukuanid");

                entity.Property(e => e.Mstbranchprosesid).HasColumnName("mstbranchprosesid");

                entity.Property(e => e.Pembayarangpid).HasColumnName("pembayarangpid");

                entity.Property(e => e.Pengurangannilaimargin).HasColumnName("pengurangannilaimargin");

                entity.Property(e => e.Periodediskon).HasColumnName("periodediskon");

                entity.Property(e => e.Permasalahan).HasColumnName("permasalahan");

                entity.Property(e => e.Polarestrukturid).HasColumnName("polarestrukturid");

                entity.Property(e => e.Principalpembayaran).HasColumnName("principalpembayaran");

                entity.Property(e => e.Principalpinalty).HasColumnName("principalpinalty");

                entity.Property(e => e.Requesterrole).HasColumnName("requesterrole");

                entity.Property(e => e.Statusid).HasColumnName("statusid");

                entity.Property(e => e.Statusmodifydated).HasColumnName("statusmodifydated");

                entity.Property(e => e.Tglakhirperiodediskon)
                    .HasColumnType("date")
                    .HasColumnName("tglakhirperiodediskon");

                entity.Property(e => e.Tglawalperiodediskon)
                    .HasColumnType("date")
                    .HasColumnName("tglawalperiodediskon");

                entity.Property(e => e.Tgljatuhtempobaru)
                    .HasColumnType("date")
                    .HasColumnName("tgljatuhtempobaru");

                entity.Property(e => e.Totaldiskonmargin).HasColumnName("totaldiskonmargin");

                entity.Property(e => e.Valuedate)
                    .HasColumnType("date")
                    .HasColumnName("valuedate");
            });

            modelBuilder.Entity<Restrukturedokumen>(entity =>
            {
                entity.ToTable("restrukturedokumen", "RecoveryBusinessV2");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Doctypedesc).HasColumnName("doctypedesc");

                entity.Property(e => e.Filepath).HasColumnName("filepath");

                entity.Property(e => e.Fileurl).HasColumnName("fileurl");

                entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");

                entity.Property(e => e.Keterangan).HasColumnName("keterangan");

                entity.Property(e => e.Loanid).HasColumnName("loanid");

                entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");

                entity.Property(e => e.Modifydated).HasColumnName("modifydated");

                entity.Property(e => e.Restruktureid).HasColumnName("restruktureid");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Typedocid).HasColumnName("typedocid");

                entity.Property(e => e.Uploaddated).HasColumnName("uploaddated");

                entity.Property(e => e.Uploadedby).HasColumnName("uploadedby");
            });

            modelBuilder.Entity<Templatedetail>(entity =>
            {
                entity.ToTable("templatedetail", "RecoveryBusinessV2");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Layoutpositionid).HasColumnName("layoutpositionid");

                entity.Property(e => e.Mastertemplateid).HasColumnName("mastertemplateid");

                entity.Property(e => e.PositionX).HasColumnName("position_x");

                entity.Property(e => e.PositionY).HasColumnName("position_y");

                entity.Property(e => e.PositionZ).HasColumnName("position_z");

                entity.Property(e => e.Value).HasColumnName("value");
            });

            modelBuilder.Entity<Workflow>(entity =>
            {
                entity.ToTable("workflow", "RecoveryBusinessV2");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Actor).HasColumnName("actor");

                entity.Property(e => e.Fiturid).HasColumnName("fiturid");

                entity.Property(e => e.Flowid).HasColumnName("flowid");

                entity.Property(e => e.Masterworkflowid).HasColumnName("masterworkflowid");

                entity.Property(e => e.Modifydated).HasColumnName("modifydated");

                entity.Property(e => e.Orders).HasColumnName("orders");

                entity.Property(e => e.Reason).HasColumnName("reason");

                entity.Property(e => e.Requestid).HasColumnName("requestid");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Submitdated).HasColumnName("submitdated");
            });

            modelBuilder.Entity<Workflowhistory>(entity =>
            {
                entity.ToTable("workflowhistory", "RecoveryBusinessV2");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Actor).HasColumnName("actor");

                entity.Property(e => e.Dated).HasColumnName("dated");

                entity.Property(e => e.Reason).HasColumnName("reason");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Workflowid).HasColumnName("workflowid");
            });

            modelBuilder.Entity<Writeoff>(entity =>
            {
                entity.ToTable("writeoff", "RecoveryBusinessV2");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Chargesrate).HasColumnName("chargesrate");

                entity.Property(e => e.Createdby).HasColumnName("createdby");

                entity.Property(e => e.Createddated).HasColumnName("createddated");

                entity.Property(e => e.Currentoverdue).HasColumnName("currentoverdue");

                entity.Property(e => e.Interestrate).HasColumnName("interestrate");

                entity.Property(e => e.Isactive).HasColumnName("isactive");

                entity.Property(e => e.Loanid).HasColumnName("loanid");

                entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");

                entity.Property(e => e.Modifieddated).HasColumnName("modifieddated");

                entity.Property(e => e.Principal).HasColumnName("principal");

                entity.Property(e => e.Statusid).HasColumnName("statusid");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
