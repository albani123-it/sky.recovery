using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Public
{
    public partial class skycollContext : DbContext
    {
        public skycollContext()
        {
        }

        public skycollContext(DbContextOptions<skycollContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountDistribution> AccountDistributions { get; set; }
        public virtual DbSet<AccountDistributionReq> AccountDistributionReqs { get; set; }
        public virtual DbSet<Action> Actions { get; set; }
        public virtual DbSet<ActionGroup> ActionGroups { get; set; }
        public virtual DbSet<ActionGroupReq> ActionGroupReqs { get; set; }
        public virtual DbSet<ActionReq> ActionReqs { get; set; }
        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<AreaReq> AreaReqs { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Bucket> Buckets { get; set; }
        public virtual DbSet<BucketDetail> BucketDetails { get; set; }
        public virtual DbSet<CallRequest> CallRequests { get; set; }
        public virtual DbSet<CallScript> CallScripts { get; set; }
        public virtual DbSet<CollectionAddContact> CollectionAddContacts { get; set; }
        public virtual DbSet<CollectionCall> CollectionCalls { get; set; }
        public virtual DbSet<CollectionContactPhoto> CollectionContactPhotos { get; set; }
        public virtual DbSet<CollectionHistory> CollectionHistories { get; set; }
        public virtual DbSet<CollectionPhoto> CollectionPhotos { get; set; }
        public virtual DbSet<CollectionTrace> CollectionTraces { get; set; }
        public virtual DbSet<CollectionVisit> CollectionVisits { get; set; }
        public virtual DbSet<ContentNotifikasi> ContentNotifikasis { get; set; }
        public virtual DbSet<ContentNotifikasiDetail> ContentNotifikasiDetails { get; set; }
        public virtual DbSet<Counter> Counters { get; set; }
        public virtual DbSet<DocSignature> DocSignatures { get; set; }
        public virtual DbSet<FcMappingMikroCollection> FcMappingMikroCollections { get; set; }
        public virtual DbSet<FcMappingMikroCollectionReq> FcMappingMikroCollectionReqs { get; set; }
        public virtual DbSet<GenerateLetter> GenerateLetters { get; set; }
        public virtual DbSet<GenericParam> GenericParams { get; set; }
        public virtual DbSet<Insurance> Insurances { get; set; }
        public virtual DbSet<LoanBiayaLain> LoanBiayaLains { get; set; }
        public virtual DbSet<LoanKodeao> LoanKodeaos { get; set; }
        public virtual DbSet<LoanKomitekredit> LoanKomitekredits { get; set; }
        public virtual DbSet<LoanKsl> LoanKsls { get; set; }
        public virtual DbSet<LoanPk> LoanPks { get; set; }
        public virtual DbSet<LoanTagihanLain> LoanTagihanLains { get; set; }
        public virtual DbSet<MasterCollateral> MasterCollaterals { get; set; }
        public virtual DbSet<MasterCustomer> MasterCustomers { get; set; }
        public virtual DbSet<MasterLoan> MasterLoans { get; set; }
        public virtual DbSet<MasterLoanHistory> MasterLoanHistories { get; set; }
        public virtual DbSet<MasterNotari> MasterNotaris { get; set; }
        public virtual DbSet<NotifContent> NotifContents { get; set; }
        public virtual DbSet<PaymentHistory> PaymentHistories { get; set; }
        public virtual DbSet<PaymentHistoryBk20240513> PaymentHistoryBk20240513s { get; set; }
        public virtual DbSet<PaymentRecord> PaymentRecords { get; set; }
        public virtual DbSet<Reason> Reasons { get; set; }
        public virtual DbSet<Rfproduct> Rfproducts { get; set; }
        public virtual DbSet<RfproductSegment> RfproductSegments { get; set; }
        public virtual DbSet<Rfresult> Rfresults { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<StgMasterLoan> StgMasterLoans { get; set; }
        public virtual DbSet<Tracking> Trackings { get; set; }
        public virtual DbSet<Workflowconfig> Workflowconfigs { get; set; }

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

            modelBuilder.Entity<AccountDistribution>(entity =>
            {
                entity.ToTable("account_distribution");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .HasMaxLength(20)
                    .HasColumnName("code");

                entity.Property(e => e.CoreCode)
                    .HasMaxLength(20)
                    .HasColumnName("core_code");

                entity.Property(e => e.CreateDate).HasColumnName("create_date");

                entity.Property(e => e.Dpd).HasColumnName("dpd");

                entity.Property(e => e.DpdMax).HasColumnName("dpd_max");

                entity.Property(e => e.DpdMin).HasColumnName("dpd_min");

                entity.Property(e => e.MaxPtp).HasColumnName("max_ptp");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.UpdateDate).HasColumnName("update_date");
            });

            modelBuilder.Entity<AccountDistributionReq>(entity =>
            {
                entity.ToTable("account_distribution_req");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountDistributionId).HasColumnName("account_distribution_id");

                entity.Property(e => e.Code)
                    .HasMaxLength(20)
                    .HasColumnName("code");

                entity.Property(e => e.CoreCode)
                    .HasMaxLength(20)
                    .HasColumnName("core_code");

                entity.Property(e => e.CreateDate).HasColumnName("create_date");

                entity.Property(e => e.Dpd).HasColumnName("dpd");

                entity.Property(e => e.DpdMax).HasColumnName("dpd_max");

                entity.Property(e => e.DpdMin).HasColumnName("dpd_min");

                entity.Property(e => e.MaxPtp).HasColumnName("max_ptp");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.UpdateDate).HasColumnName("update_date");
            });

            modelBuilder.Entity<Action>(entity =>
            {
                entity.ToTable("action");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ActDesc)
                    .HasMaxLength(100)
                    .HasColumnName("act_desc")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.Code)
                    .HasMaxLength(20)
                    .HasColumnName("code")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.CodeCode)
                    .HasMaxLength(20)
                    .HasColumnName("code_code")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.CreateDate).HasColumnName("create_date");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.UpdateDate).HasColumnName("update_date");
            });

            modelBuilder.Entity<ActionGroup>(entity =>
            {
                entity.ToTable("action_group");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ActionId).HasColumnName("action_id");

                entity.Property(e => e.CreateDate).HasColumnName("create_date");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.UpdateDate).HasColumnName("update_date");
            });

            modelBuilder.Entity<ActionGroupReq>(entity =>
            {
                entity.ToTable("action_group_req");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ActionGroupId).HasColumnName("action_group_id");

                entity.Property(e => e.ActionId).HasColumnName("action_id");

                entity.Property(e => e.ApproveUserId).HasColumnName("approve_user_id");

                entity.Property(e => e.CreateDate).HasColumnName("create_date");

                entity.Property(e => e.ReqUserId).HasColumnName("req_user_id");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.UpdateDate).HasColumnName("update_date");
            });

            modelBuilder.Entity<ActionReq>(entity =>
            {
                entity.ToTable("action_req");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ActDesc)
                    .HasMaxLength(100)
                    .HasColumnName("act_desc");

                entity.Property(e => e.ActionId).HasColumnName("action_id");

                entity.Property(e => e.ApproveDate).HasColumnName("approve_date");

                entity.Property(e => e.ApproveUserId).HasColumnName("approve_user_id");

                entity.Property(e => e.Code)
                    .HasMaxLength(20)
                    .HasColumnName("code");

                entity.Property(e => e.CoreCode)
                    .HasMaxLength(20)
                    .HasColumnName("core_code");

                entity.Property(e => e.CreateDate).HasColumnName("create_date");

                entity.Property(e => e.ReqUserId).HasColumnName("req_user_id");

                entity.Property(e => e.StatusId).HasColumnName("status_id");
            });

            modelBuilder.Entity<Area>(entity =>
            {
                entity.ToTable("area");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .HasMaxLength(100)
                    .HasColumnName("code");

                entity.Property(e => e.CoreCode)
                    .HasMaxLength(100)
                    .HasColumnName("core_code");

                entity.Property(e => e.CreateDate).HasColumnName("create_date");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.UpdateDate).HasColumnName("update_date");
            });

            modelBuilder.Entity<AreaReq>(entity =>
            {
                entity.ToTable("area_req");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Adesc)
                    .HasMaxLength(100)
                    .HasColumnName("adesc");

                entity.Property(e => e.AreaId).HasColumnName("area_id");

                entity.Property(e => e.BranchId).HasColumnName("branch_id");

                entity.Property(e => e.CreateDate).HasColumnName("create_date");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.UpdateDate).HasColumnName("update_date");

                entity.Property(e => e.Value)
                    .HasMaxLength(100)
                    .HasColumnName("value");
            });

            modelBuilder.Entity<Branch>(entity =>
            {
                entity.HasKey(e => e.LbrcId)
                    .HasName("branch_pkey");

                entity.ToTable("branch");

                entity.Property(e => e.LbrcId).HasColumnName("lbrc_id");

                entity.Property(e => e.LbrcAddress)
                    .HasMaxLength(1000)
                    .HasColumnName("lbrc_address");

                entity.Property(e => e.LbrcCity)
                    .HasMaxLength(50)
                    .HasColumnName("lbrc_city");

                entity.Property(e => e.LbrcCode)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("lbrc_code");

                entity.Property(e => e.LbrcGroup)
                    .HasMaxLength(50)
                    .HasColumnName("lbrc_group");

                entity.Property(e => e.LbrcIsDelete).HasColumnName("lbrc_is_delete");

                entity.Property(e => e.LbrcName)
                    .HasMaxLength(200)
                    .HasColumnName("lbrc_name");

                entity.Property(e => e.LbrcPhoneNum)
                    .HasMaxLength(100)
                    .HasColumnName("lbrc_phone_num");
            });

            modelBuilder.Entity<Bucket>(entity =>
            {
                entity.HasKey(e => e.BctId)
                    .HasName("bucket_pkey");

                entity.ToTable("bucket");

                entity.Property(e => e.BctId).HasColumnName("bct_id");

                entity.Property(e => e.BctCode)
                    .HasMaxLength(10)
                    .HasColumnName("bct_code");

                entity.Property(e => e.BctCreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("bct_created_by");

                entity.Property(e => e.BctExtCode)
                    .HasMaxLength(10)
                    .HasColumnName("bct_ext_code");

                entity.Property(e => e.BctModifiedBy)
                    .HasMaxLength(50)
                    .HasColumnName("bct_modified_by");

                entity.Property(e => e.BctName)
                    .HasMaxLength(150)
                    .HasColumnName("bct_name");

                entity.Property(e => e.BctStatusId).HasColumnName("bct_status_id");

                entity.Property(e => e.CreateDate).HasColumnName("create_date");

                entity.Property(e => e.UpdateDate).HasColumnName("update_date");
            });

            modelBuilder.Entity<BucketDetail>(entity =>
            {
                entity.HasKey(e => e.BcdId)
                    .HasName("bucket_detail_pkey");

                entity.ToTable("bucket_detail");

                entity.Property(e => e.BcdId).HasColumnName("bcd_id");

                entity.Property(e => e.BcdBctId).HasColumnName("bcd_bct_id");

                entity.Property(e => e.BcdUserid).HasColumnName("bcd_userid");
            });

            modelBuilder.Entity<CallRequest>(entity =>
            {
                entity.ToTable("call_request");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CallId).HasColumnName("call_id");

                entity.Property(e => e.CreateDate).HasColumnName("create_date");

                entity.Property(e => e.Lat).HasColumnName("lat");

                entity.Property(e => e.Lon).HasColumnName("lon");

                entity.Property(e => e.Mime)
                    .HasMaxLength(100)
                    .HasColumnName("mime");

                entity.Property(e => e.PhoneNo)
                    .HasMaxLength(100)
                    .HasColumnName("phone_no");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.Url)
                    .HasMaxLength(250)
                    .HasColumnName("url");

                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<CallScript>(entity =>
            {
                entity.HasKey(e => e.CscId)
                    .HasName("call_script_pkey");

                entity.ToTable("call_script");

                entity.Property(e => e.CscId).HasColumnName("csc_id");

                entity.Property(e => e.CscAccdMax).HasColumnName("csc_accd_max");

                entity.Property(e => e.CscAccdMin).HasColumnName("csc_accd_min");

                entity.Property(e => e.CscApprovedBy)
                    .HasMaxLength(50)
                    .HasColumnName("csc_approved_by");

                entity.Property(e => e.CscApprovedStatus)
                    .HasMaxLength(50)
                    .HasColumnName("csc_approved_status");

                entity.Property(e => e.CscCode)
                    .HasMaxLength(50)
                    .HasColumnName("csc_code");

                entity.Property(e => e.CscCreated).HasColumnName("csc_created");

                entity.Property(e => e.CscCreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("csc_created_by");

                entity.Property(e => e.CscCsScript)
                    .HasMaxLength(5000)
                    .HasColumnName("csc_cs_script");

                entity.Property(e => e.CscDesc)
                    .HasMaxLength(500)
                    .HasColumnName("csc_desc");

                entity.Property(e => e.CscIsActive).HasColumnName("csc_is_active");

                entity.Property(e => e.CscIsDeleted).HasColumnName("csc_is_deleted");

                entity.Property(e => e.CscIsUsed).HasColumnName("csc_is_used");

                entity.Property(e => e.CscModified).HasColumnName("csc_modified");

                entity.Property(e => e.CscModifiedBy)
                    .HasMaxLength(50)
                    .HasColumnName("csc_modified_by");
            });

            modelBuilder.Entity<CollectionAddContact>(entity =>
            {
                entity.ToTable("collection_add_contact");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccNo)
                    .HasMaxLength(20)
                    .HasColumnName("acc_no");

                entity.Property(e => e.AddAddress)
                    .HasMaxLength(200)
                    .HasColumnName("add_address");

                entity.Property(e => e.AddBy).HasColumnName("add_by");

                entity.Property(e => e.AddCity)
                    .HasMaxLength(50)
                    .HasColumnName("add_city");

                entity.Property(e => e.AddDate).HasColumnName("add_date");

                entity.Property(e => e.AddFrom)
                    .HasMaxLength(25)
                    .HasColumnName("add_from");

                entity.Property(e => e.AddId)
                    .HasMaxLength(50)
                    .HasColumnName("add_id");

                entity.Property(e => e.AddPhone)
                    .HasMaxLength(30)
                    .HasColumnName("add_phone");

                entity.Property(e => e.CuCif)
                    .HasMaxLength(20)
                    .HasColumnName("cu_cif");

                entity.Property(e => e.Def).HasColumnName("def");

                entity.Property(e => e.Lat).HasColumnName("lat");

                entity.Property(e => e.Lon).HasColumnName("lon");
            });

            modelBuilder.Entity<CollectionCall>(entity =>
            {
                entity.ToTable("collection_call");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccNo)
                    .HasMaxLength(25)
                    .HasColumnName("acc_no");

                entity.Property(e => e.AddId).HasColumnName("add_id");

                entity.Property(e => e.BranchId).HasColumnName("branch_id");

                entity.Property(e => e.CallAmount).HasColumnName("call_amount");

                entity.Property(e => e.CallBy).HasColumnName("call_by");

                entity.Property(e => e.CallDate).HasColumnName("call_date");

                entity.Property(e => e.CallName)
                    .HasMaxLength(50)
                    .HasColumnName("call_name");

                entity.Property(e => e.CallNotes)
                    .HasMaxLength(1000)
                    .HasColumnName("call_notes");

                entity.Property(e => e.CallReason).HasColumnName("call_reason");

                entity.Property(e => e.CallResultDate).HasColumnName("call_result_date");

                entity.Property(e => e.CallResultHh)
                    .HasMaxLength(2)
                    .HasColumnName("call_result_hh");

                entity.Property(e => e.CallResultHhmm)
                    .HasMaxLength(5)
                    .HasColumnName("call_result_hhmm");

                entity.Property(e => e.CallResultId).HasColumnName("call_result_id");

                entity.Property(e => e.CallResultMm)
                    .HasMaxLength(2)
                    .HasColumnName("call_result_mm");

                entity.Property(e => e.LoanId).HasColumnName("loan_id");
            });

            modelBuilder.Entity<CollectionContactPhoto>(entity =>
            {
                entity.ToTable("collection_contact_photo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CollContactId).HasColumnName("coll_contact_id");

                entity.Property(e => e.CreateDate).HasColumnName("create_date");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.Lat).HasColumnName("lat");

                entity.Property(e => e.Lon).HasColumnName("lon");

                entity.Property(e => e.Mime)
                    .HasMaxLength(255)
                    .HasColumnName("mime");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .HasColumnName("title");

                entity.Property(e => e.Url)
                    .HasMaxLength(255)
                    .HasColumnName("url");

                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<CollectionHistory>(entity =>
            {
                entity.ToTable("collection_history");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccNo)
                    .HasMaxLength(25)
                    .HasColumnName("acc_no");

                entity.Property(e => e.AddId).HasColumnName("add_id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.BranchId).HasColumnName("branch_id");

                entity.Property(e => e.CallBy).HasColumnName("call_by");

                entity.Property(e => e.CallId).HasColumnName("call_id");

                entity.Property(e => e.CallResultHhmm)
                    .HasMaxLength(255)
                    .HasColumnName("call_result_hhmm");

                entity.Property(e => e.CallResultMm)
                    .HasMaxLength(255)
                    .HasColumnName("call_result_mm");

                entity.Property(e => e.Callresulthh)
                    .HasMaxLength(5000)
                    .HasColumnName("callresulthh");

                entity.Property(e => e.CbmId)
                    .HasMaxLength(50)
                    .HasColumnName("cbm_id");

                entity.Property(e => e.Dpd).HasColumnName("dpd");

                entity.Property(e => e.HistoryBy).HasColumnName("history_by");

                entity.Property(e => e.HistoryDate).HasColumnName("history_date");

                entity.Property(e => e.Kolek)
                    .HasMaxLength(50)
                    .HasColumnName("kolek");

                entity.Property(e => e.Latitude).HasColumnName("latitude");

                entity.Property(e => e.Longitude).HasColumnName("longitude");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Note)
                    .HasMaxLength(1000)
                    .HasColumnName("note");

                entity.Property(e => e.Picture)
                    .HasMaxLength(1000)
                    .HasColumnName("picture");

                entity.Property(e => e.Reason).HasColumnName("reason");

                entity.Property(e => e.Result).HasColumnName("result");

                entity.Property(e => e.ResultDate).HasColumnName("result_date");

                entity.Property(e => e.UbmId)
                    .HasMaxLength(50)
                    .HasColumnName("ubm_id");
            });

            modelBuilder.Entity<CollectionPhoto>(entity =>
            {
                entity.ToTable("collection_photo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CollhistoryId).HasColumnName("collhistory_id");

                entity.Property(e => e.CreateDate).HasColumnName("create_date");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.Lat).HasColumnName("lat");

                entity.Property(e => e.Lon).HasColumnName("lon");

                entity.Property(e => e.Mime)
                    .HasMaxLength(255)
                    .HasColumnName("mime");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .HasColumnName("title");

                entity.Property(e => e.Url)
                    .HasMaxLength(255)
                    .HasColumnName("url");

                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<CollectionTrace>(entity =>
            {
                entity.ToTable("collection_trace");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccNo)
                    .HasMaxLength(100)
                    .HasColumnName("acc_no");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.CallBy).HasColumnName("call_by");

                entity.Property(e => e.CallId).HasColumnName("call_id");

                entity.Property(e => e.Dpd).HasColumnName("dpd");

                entity.Property(e => e.Kolek).HasColumnName("kolek");

                entity.Property(e => e.Result).HasColumnName("result");

                entity.Property(e => e.TraceDate).HasColumnName("trace_date");
            });

            modelBuilder.Entity<CollectionVisit>(entity =>
            {
                entity.ToTable("collection_visit");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.AccNo)
                    .HasMaxLength(25)
                    .HasColumnName("acc_no");

                entity.Property(e => e.AddId)
                    .HasMaxLength(50)
                    .HasColumnName("add_id");

                entity.Property(e => e.BranchId).HasColumnName("branch_id");

                entity.Property(e => e.CbmId)
                    .HasMaxLength(50)
                    .HasColumnName("cbm_id");

                entity.Property(e => e.Kolek)
                    .HasMaxLength(50)
                    .HasColumnName("kolek");

                entity.Property(e => e.Latitude).HasColumnName("latitude");

                entity.Property(e => e.Longitude).HasColumnName("longitude");

                entity.Property(e => e.Picture)
                    .HasMaxLength(1000)
                    .HasColumnName("picture");

                entity.Property(e => e.UbmId)
                    .HasMaxLength(50)
                    .HasColumnName("ubm_id");

                entity.Property(e => e.VisitAmount).HasColumnName("visit_amount");

                entity.Property(e => e.VisitBy).HasColumnName("visit_by");

                entity.Property(e => e.VisitDate).HasColumnName("visit_date");

                entity.Property(e => e.VisitId)
                    .HasMaxLength(50)
                    .HasColumnName("visit_id");

                entity.Property(e => e.VisitName)
                    .HasMaxLength(50)
                    .HasColumnName("visit_name");

                entity.Property(e => e.VisitNote)
                    .HasMaxLength(1000)
                    .HasColumnName("visit_note");

                entity.Property(e => e.VisitReason)
                    .HasMaxLength(10)
                    .HasColumnName("visit_reason");

                entity.Property(e => e.VisitResult).HasColumnName("visit_result");

                entity.Property(e => e.VisitResultDate).HasColumnName("visit_result_date");
            });

            modelBuilder.Entity<ContentNotifikasi>(entity =>
            {
                entity.HasKey(e => e.CntId)
                    .HasName("content_notifikasi_pkey");

                entity.ToTable("content_notifikasi");

                entity.Property(e => e.CntId).HasColumnName("cnt_id");

                entity.Property(e => e.CntCode)
                    .HasMaxLength(50)
                    .HasColumnName("cnt_code");

                entity.Property(e => e.CntContent)
                    .HasMaxLength(500)
                    .HasColumnName("cnt_content");

                entity.Property(e => e.CntCreated).HasColumnName("cnt_created");

                entity.Property(e => e.CntCreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("cnt_created_by");

                entity.Property(e => e.CntDay)
                    .HasPrecision(6)
                    .HasColumnName("cnt_day");

                entity.Property(e => e.CntForm)
                    .HasMaxLength(50)
                    .HasColumnName("cnt_form");

                entity.Property(e => e.CntIsActive).HasColumnName("cnt_is_active");

                entity.Property(e => e.CntIsDeleted).HasColumnName("cnt_is_deleted");

                entity.Property(e => e.CntModified).HasColumnName("cnt_modified");

                entity.Property(e => e.CntModifiedBy)
                    .HasMaxLength(50)
                    .HasColumnName("cnt_modified_by");

                entity.Property(e => e.CntRead).HasColumnName("cnt_read");
            });

            modelBuilder.Entity<ContentNotifikasiDetail>(entity =>
            {
                entity.HasKey(e => e.CndId)
                    .HasName("content_notifikasi_detail_pkey");

                entity.ToTable("content_notifikasi_detail");

                entity.Property(e => e.CndId).HasColumnName("cnd_id");

                entity.Property(e => e.CndCntId).HasColumnName("cnd_cnt_id");

                entity.Property(e => e.CndTo)
                    .HasMaxLength(50)
                    .HasColumnName("cnd_to");
            });

            modelBuilder.Entity<Counter>(entity =>
            {
                entity.ToTable("counter");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.BranchId).HasColumnName("branch_id");

                entity.Property(e => e.CounterType)
                    .HasMaxLength(50)
                    .HasColumnName("counter_type");

                entity.Property(e => e.CreateDate).HasColumnName("create_date");

                entity.Property(e => e.Ctr).HasColumnName("ctr");

                entity.Property(e => e.Month).HasColumnName("month");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.UpdateDate).HasColumnName("update_date");

                entity.Property(e => e.Year).HasColumnName("year");
            });

            modelBuilder.Entity<DocSignature>(entity =>
            {
                entity.ToTable("doc_signature");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.BranchId).HasColumnName("branch_id");

                entity.Property(e => e.DocCode)
                    .HasMaxLength(50)
                    .HasColumnName("doc_code");

                entity.Property(e => e.DocName)
                    .HasMaxLength(255)
                    .HasColumnName("doc_name");
            });

            modelBuilder.Entity<FcMappingMikroCollection>(entity =>
            {
                entity.ToTable("fc_mapping_mikro_collection");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CreateDate).HasColumnName("create_date");

                entity.Property(e => e.FcId).HasColumnName("fc_id");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.TypeId)
                    .HasMaxLength(5000)
                    .HasColumnName("type_id");

                entity.Property(e => e.UpdateDate).HasColumnName("update_date");
            });

            modelBuilder.Entity<FcMappingMikroCollectionReq>(entity =>
            {
                entity.ToTable("fc_mapping_mikro_collection_req");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CreateDate).HasColumnName("create_date");

                entity.Property(e => e.FcId).HasColumnName("fc_id");

                entity.Property(e => e.FcMappingMikroCollectionId).HasColumnName("fc_mapping_mikro_collection_id");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.TypeId)
                    .HasMaxLength(5000)
                    .HasColumnName("type_id");

                entity.Property(e => e.UpdateDate).HasColumnName("update_date");
            });

            modelBuilder.Entity<GenerateLetter>(entity =>
            {
                entity.ToTable("generate_letter");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cabang)
                    .HasMaxLength(50)
                    .HasColumnName("cabang");

                entity.Property(e => e.CabangAlamat)
                    .HasMaxLength(200)
                    .HasColumnName("cabang_alamat");

                entity.Property(e => e.CabangFaks)
                    .HasMaxLength(50)
                    .HasColumnName("cabang_faks");

                entity.Property(e => e.CabangKota)
                    .HasMaxLength(100)
                    .HasColumnName("cabang_kota");

                entity.Property(e => e.CabangTelepon)
                    .HasMaxLength(50)
                    .HasColumnName("cabang_telepon");

                entity.Property(e => e.Jumlah)
                    .HasMaxLength(50)
                    .HasColumnName("jumlah");

                entity.Property(e => e.LoanId).HasColumnName("loan_id");

                entity.Property(e => e.NamaNasabah)
                    .HasMaxLength(200)
                    .HasColumnName("nama_nasabah");

                entity.Property(e => e.No)
                    .HasMaxLength(50)
                    .HasColumnName("no");

                entity.Property(e => e.NoKredit)
                    .HasMaxLength(50)
                    .HasColumnName("no_kredit");

                entity.Property(e => e.NoSp1)
                    .HasMaxLength(50)
                    .HasColumnName("no_sp1");

                entity.Property(e => e.NoSp2)
                    .HasMaxLength(50)
                    .HasColumnName("no_sp2");

                entity.Property(e => e.Notaris)
                    .HasMaxLength(50)
                    .HasColumnName("notaris");

                entity.Property(e => e.NotarisDi)
                    .HasMaxLength(50)
                    .HasColumnName("notaris_di");

                entity.Property(e => e.NotarisTgl)
                    .HasMaxLength(50)
                    .HasColumnName("notaris_tgl");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.Terbilang)
                    .HasMaxLength(200)
                    .HasColumnName("terbilang");

                entity.Property(e => e.Tgl)
                    .HasMaxLength(50)
                    .HasColumnName("tgl");

                entity.Property(e => e.TglBayar)
                    .HasMaxLength(50)
                    .HasColumnName("tgl_bayar");

                entity.Property(e => e.TglKredit)
                    .HasMaxLength(50)
                    .HasColumnName("tgl_kredit");

                entity.Property(e => e.TglSp1)
                    .HasMaxLength(50)
                    .HasColumnName("tgl_sp1");

                entity.Property(e => e.TglSp2)
                    .HasMaxLength(50)
                    .HasColumnName("tgl_sp2");

                entity.Property(e => e.TypeLetter)
                    .HasMaxLength(50)
                    .HasColumnName("type_letter");
            });

            modelBuilder.Entity<GenericParam>(entity =>
            {
                entity.HasKey(e => e.GlpId)
                    .HasName("generic_param_pkey");

                entity.ToTable("generic_param");

                entity.Property(e => e.GlpId)
                    .HasColumnName("glp_id")
                    .HasDefaultValueSql("nextval('glp_id_sec'::regclass)");

                entity.Property(e => e.GlpCode)
                    .HasMaxLength(50)
                    .HasColumnName("glp_code");

                entity.Property(e => e.GlpName)
                    .HasMaxLength(50)
                    .HasColumnName("glp_name");

                entity.Property(e => e.GlpType)
                    .HasColumnType("character varying")
                    .HasColumnName("glp_type");
            });

            modelBuilder.Entity<Insurance>(entity =>
            {
                entity.ToTable("insurance");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AsuransiId).HasColumnName("asuransi_id");

                entity.Property(e => e.AsuransiSisaKlaimId).HasColumnName("asuransi_sisa_klaim_id");

                entity.Property(e => e.BakiDebitKlaim).HasColumnName("baki_debit_klaim");

                entity.Property(e => e.CatatanKlaim)
                    .HasMaxLength(900)
                    .HasColumnName("catatan_klaim");

                entity.Property(e => e.CatatanPolis)
                    .HasMaxLength(900)
                    .HasColumnName("catatan_polis");

                entity.Property(e => e.Jabatan)
                    .HasMaxLength(900)
                    .HasColumnName("jabatan");

                entity.Property(e => e.Keterangan)
                    .HasMaxLength(600)
                    .HasColumnName("keterangan");

                entity.Property(e => e.LastUpdateDate).HasColumnName("last_update_date");

                entity.Property(e => e.LastUpdateId).HasColumnName("last_update_id");

                entity.Property(e => e.LoanId).HasColumnName("loan_id");

                entity.Property(e => e.MstBranchId).HasColumnName("mst_branch_id");

                entity.Property(e => e.MstBranchPembukuanId).HasColumnName("mst_branch_pembukuan_id");

                entity.Property(e => e.MstBranchProsesId).HasColumnName("mst_branch_proses_id");

                entity.Property(e => e.NamaPejabat)
                    .HasMaxLength(900)
                    .HasColumnName("nama_pejabat");

                entity.Property(e => e.NilaiKlaim).HasColumnName("nilai_klaim");

                entity.Property(e => e.NilaiKlaimDibayar).HasColumnName("nilai_klaim_dibayar");

                entity.Property(e => e.NilaiTunggakanBunga).HasColumnName("nilai_tunggakan_bunga");

                entity.Property(e => e.NilaiTunggakanPokok).HasColumnName("nilai_tunggakan_pokok");

                entity.Property(e => e.NoPk)
                    .HasMaxLength(500)
                    .HasColumnName("no_pk");

                entity.Property(e => e.NoPolis)
                    .HasMaxLength(900)
                    .HasColumnName("no_polis");

                entity.Property(e => e.NoSertifikat)
                    .HasMaxLength(900)
                    .HasColumnName("no_sertifikat");

                entity.Property(e => e.Permasalahan)
                    .HasMaxLength(900)
                    .HasColumnName("permasalahan");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.TglKlaimDibayar).HasColumnName("tgl_klaim_dibayar");

                entity.Property(e => e.TglPolis).HasColumnName("tgl_polis");

                entity.Property(e => e.TglSertifikat).HasColumnName("tgl_sertifikat");
            });

            modelBuilder.Entity<LoanBiayaLain>(entity =>
            {
                entity.ToTable("loan_biaya_lain");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.AccNo)
                    .HasMaxLength(25)
                    .HasColumnName("acc_no");

                entity.Property(e => e.LoanId).HasColumnName("loan_id");

                entity.Property(e => e.NamaBiayaLain)
                    .HasMaxLength(50)
                    .HasColumnName("nama_biaya_lain");

                entity.Property(e => e.NominalBiayaLain).HasColumnName("nominal_biaya_lain");

                entity.Property(e => e.StgDate).HasColumnName("stg_date");

                entity.Property(e => e.TanggalBiayaLain).HasColumnName("tanggal_biaya_lain");
            });

            modelBuilder.Entity<LoanKodeao>(entity =>
            {
                entity.ToTable("loan_kodeao");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.AccNo)
                    .HasMaxLength(25)
                    .HasColumnName("acc_no");

                entity.Property(e => e.KodeAo)
                    .HasMaxLength(200)
                    .HasColumnName("kode_ao");

                entity.Property(e => e.LoanId).HasColumnName("loan_id");

                entity.Property(e => e.StgDate).HasColumnName("stg_date");

                entity.Property(e => e.TanggalAo).HasColumnName("tanggal_ao");
            });

            modelBuilder.Entity<LoanKomitekredit>(entity =>
            {
                entity.ToTable("loan_komitekredit");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.AccNo)
                    .HasMaxLength(25)
                    .HasColumnName("acc_no");

                entity.Property(e => e.Komite01)
                    .HasMaxLength(200)
                    .HasColumnName("komite01");

                entity.Property(e => e.Komite02)
                    .HasMaxLength(200)
                    .HasColumnName("komite02");

                entity.Property(e => e.Komite03)
                    .HasMaxLength(200)
                    .HasColumnName("komite03");

                entity.Property(e => e.Komite04)
                    .HasMaxLength(200)
                    .HasColumnName("komite04");

                entity.Property(e => e.Komite05)
                    .HasMaxLength(200)
                    .HasColumnName("komite05");

                entity.Property(e => e.Komite06)
                    .HasMaxLength(200)
                    .HasColumnName("komite06");

                entity.Property(e => e.LoanId).HasColumnName("loan_id");

                entity.Property(e => e.NomorPk)
                    .HasMaxLength(150)
                    .HasColumnName("nomor_pk");

                entity.Property(e => e.StgDate).HasColumnName("stg_date");

                entity.Property(e => e.TanggalPk).HasColumnName("tanggal_pk");
            });

            modelBuilder.Entity<LoanKsl>(entity =>
            {
                entity.ToTable("loan_ksl");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.AccNo)
                    .HasMaxLength(25)
                    .HasColumnName("acc_no");

                entity.Property(e => e.LoanId).HasColumnName("loan_id");

                entity.Property(e => e.NamaKsl)
                    .HasMaxLength(50)
                    .HasColumnName("nama_ksl");

                entity.Property(e => e.SaldoKsl)
                    .HasMaxLength(200)
                    .HasColumnName("saldo_ksl");

                entity.Property(e => e.StgDate).HasColumnName("stg_date");

                entity.Property(e => e.TanggalKsl).HasColumnName("tanggal_ksl");
            });

            modelBuilder.Entity<LoanPk>(entity =>
            {
                entity.ToTable("loan_pk");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.AccNo)
                    .HasMaxLength(25)
                    .HasColumnName("acc_no");

                entity.Property(e => e.LoanId).HasColumnName("loan_id");

                entity.Property(e => e.NamaLegal)
                    .HasMaxLength(70)
                    .HasColumnName("nama_legal");

                entity.Property(e => e.NamaNotaris)
                    .HasMaxLength(70)
                    .HasColumnName("nama_notaris");

                entity.Property(e => e.NomorPk)
                    .HasMaxLength(150)
                    .HasColumnName("nomor_pk");

                entity.Property(e => e.StgDate).HasColumnName("stg_date");

                entity.Property(e => e.TanggalPk).HasColumnName("tanggal_pk");
            });

            modelBuilder.Entity<LoanTagihanLain>(entity =>
            {
                entity.ToTable("loan_tagihan_lain");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.AccNo)
                    .HasMaxLength(25)
                    .HasColumnName("acc_no");

                entity.Property(e => e.LoanId).HasColumnName("loan_id");

                entity.Property(e => e.NamaTl)
                    .HasMaxLength(50)
                    .HasColumnName("nama_tl");

                entity.Property(e => e.NominalTl).HasColumnName("nominal_tl");

                entity.Property(e => e.StgDate).HasColumnName("stg_date");

                entity.Property(e => e.TanggalTl).HasColumnName("tanggal_tl");
            });

            modelBuilder.Entity<MasterCollateral>(entity =>
            {
                entity.ToTable("master_collateral");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ColAddress)
                    .HasMaxLength(500)
                    .HasColumnName("col_address");

                entity.Property(e => e.ColDescription)
                    .HasMaxLength(500)
                    .HasColumnName("col_description");

                entity.Property(e => e.ColDocument)
                    .HasMaxLength(500)
                    .HasColumnName("col_document");

                entity.Property(e => e.ColId)
                    .HasMaxLength(100)
                    .HasColumnName("col_id");

                entity.Property(e => e.ColNameAgunan)
                    .HasMaxLength(50)
                    .HasColumnName("col_name_agunan");

                entity.Property(e => e.ColObject)
                    .HasMaxLength(20)
                    .HasColumnName("col_object");

                entity.Property(e => e.ColTglAgunan).HasColumnName("col_tgl_agunan");

                entity.Property(e => e.ColType)
                    .HasMaxLength(20)
                    .HasColumnName("col_type");

                entity.Property(e => e.LoanId).HasColumnName("loan_id");

                entity.Property(e => e.VehBpkbName)
                    .HasMaxLength(60)
                    .HasColumnName("veh_bpkb_name");

                entity.Property(e => e.VehBpkbNo)
                    .HasMaxLength(20)
                    .HasColumnName("veh_bpkb_no");

                entity.Property(e => e.VehBuildYear)
                    .HasMaxLength(4)
                    .HasColumnName("veh_build_year");

                entity.Property(e => e.VehCc)
                    .HasMaxLength(20)
                    .HasColumnName("veh_cc");

                entity.Property(e => e.VehChassisNo)
                    .HasMaxLength(30)
                    .HasColumnName("veh_chassis_no");

                entity.Property(e => e.VehColor)
                    .HasMaxLength(50)
                    .HasColumnName("veh_color");

                entity.Property(e => e.VehEngineNo)
                    .HasMaxLength(30)
                    .HasColumnName("veh_engine_no");

                entity.Property(e => e.VehMerek)
                    .HasMaxLength(40)
                    .HasColumnName("veh_merek");

                entity.Property(e => e.VehModel)
                    .HasMaxLength(30)
                    .HasColumnName("veh_model");

                entity.Property(e => e.VehPlateNo)
                    .HasMaxLength(20)
                    .HasColumnName("veh_plate_no");

                entity.Property(e => e.VehStnkNo)
                    .HasMaxLength(30)
                    .HasColumnName("veh_stnk_no");

                entity.Property(e => e.VehYear)
                    .HasMaxLength(4)
                    .HasColumnName("veh_year");
            });

            modelBuilder.Entity<MasterCustomer>(entity =>
            {
                entity.ToTable("master_customer");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.BranchId).HasColumnName("branch_id");

                entity.Property(e => e.City)
                    .HasMaxLength(200)
                    .HasColumnName("city");

                entity.Property(e => e.CuAddress)
                    .HasMaxLength(200)
                    .HasColumnName("cu_address");

                entity.Property(e => e.CuBorndate).HasColumnName("cu_borndate");

                entity.Property(e => e.CuBornplace)
                    .HasMaxLength(100)
                    .HasColumnName("cu_bornplace");

                entity.Property(e => e.CuCif)
                    .HasMaxLength(20)
                    .HasColumnName("cu_cif");

                entity.Property(e => e.CuCity).HasColumnName("cu_city");

                entity.Property(e => e.CuCompany)
                    .HasMaxLength(50)
                    .HasColumnName("cu_company");

                entity.Property(e => e.CuCusttype).HasColumnName("cu_custtype");

                entity.Property(e => e.CuEmail)
                    .HasMaxLength(50)
                    .HasColumnName("cu_email");

                entity.Property(e => e.CuGender).HasColumnName("cu_gender");

                entity.Property(e => e.CuHmphone)
                    .HasMaxLength(30)
                    .HasColumnName("cu_hmphone");

                entity.Property(e => e.CuIdnumber)
                    .HasMaxLength(30)
                    .HasColumnName("cu_idnumber");

                entity.Property(e => e.CuIdtype).HasColumnName("cu_idtype");

                entity.Property(e => e.CuIncome)
                    .HasMaxLength(200)
                    .HasColumnName("cu_income");

                entity.Property(e => e.CuIncometype).HasColumnName("cu_incometype");

                entity.Property(e => e.CuKecamatan).HasColumnName("cu_kecamatan");

                entity.Property(e => e.CuKelurahan).HasColumnName("cu_kelurahan");

                entity.Property(e => e.CuMaritalstatus).HasColumnName("cu_maritalstatus");

                entity.Property(e => e.CuMobilephone)
                    .HasMaxLength(100)
                    .HasColumnName("cu_mobilephone");

                entity.Property(e => e.CuName)
                    .HasMaxLength(100)
                    .HasColumnName("cu_name");

                entity.Property(e => e.CuNationality).HasColumnName("cu_nationality");

                entity.Property(e => e.CuOccupation).HasColumnName("cu_occupation");

                entity.Property(e => e.CuProvinsi).HasColumnName("cu_provinsi");

                entity.Property(e => e.CuRt)
                    .HasMaxLength(10)
                    .HasColumnName("cu_rt");

                entity.Property(e => e.CuRw)
                    .HasMaxLength(10)
                    .HasColumnName("cu_rw");

                entity.Property(e => e.CuZipcode)
                    .HasMaxLength(10)
                    .HasColumnName("cu_zipcode");

                entity.Property(e => e.Jabatan)
                    .HasMaxLength(200)
                    .HasColumnName("jabatan");

                entity.Property(e => e.Kecamatan)
                    .HasMaxLength(200)
                    .HasColumnName("kecamatan");

                entity.Property(e => e.Kelurahan)
                    .HasMaxLength(200)
                    .HasColumnName("kelurahan");

                entity.Property(e => e.Pekerjaan)
                    .HasMaxLength(200)
                    .HasColumnName("pekerjaan");

                entity.Property(e => e.Provinsi)
                    .HasMaxLength(200)
                    .HasColumnName("provinsi");

                entity.Property(e => e.StgDate).HasColumnName("stg_date");
            });

            modelBuilder.Entity<MasterLoan>(entity =>
            {
                entity.ToTable("master_loan");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccNo)
                    .HasMaxLength(20)
                    .HasColumnName("acc_no");

                entity.Property(e => e.Ccy)
                    .HasMaxLength(10)
                    .HasColumnName("ccy");

                entity.Property(e => e.ChannelBranchCode)
                    .HasMaxLength(50)
                    .HasColumnName("channel_branch_code");

                entity.Property(e => e.CuCif)
                    .HasMaxLength(20)
                    .HasColumnName("cu_cif");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.Dpd).HasColumnName("dpd");

                entity.Property(e => e.EconPhone)
                    .HasMaxLength(20)
                    .HasColumnName("econ_phone");

                entity.Property(e => e.EconRelation)
                    .HasMaxLength(10)
                    .HasColumnName("econ_relation");

                entity.Property(e => e.EconaName)
                    .HasMaxLength(100)
                    .HasColumnName("econa_name");

                entity.Property(e => e.Fasilitas)
                    .HasMaxLength(200)
                    .HasColumnName("fasilitas");

                entity.Property(e => e.FileDate)
                    .HasColumnType("date")
                    .HasColumnName("file_date");

                entity.Property(e => e.Installment).HasColumnName("installment");

                entity.Property(e => e.InstallmentPokok).HasColumnName("installment_pokok");

                entity.Property(e => e.InterestRate).HasColumnName("interest_rate");

                entity.Property(e => e.KewajibanTotal).HasColumnName("kewajiban_total");

                entity.Property(e => e.Kolektibilitas).HasColumnName("kolektibilitas");

                entity.Property(e => e.LastPayDate).HasColumnName("last_pay_date");

                entity.Property(e => e.LoanNumber)
                    .HasMaxLength(100)
                    .HasColumnName("loan_number");

                entity.Property(e => e.MarketingCode)
                    .HasMaxLength(20)
                    .HasColumnName("marketing_code");

                entity.Property(e => e.MaturityDate).HasColumnName("maturity_date");

                entity.Property(e => e.NotarisId).HasColumnName("notaris_id");

                entity.Property(e => e.Outstanding).HasColumnName("outstanding");

                entity.Property(e => e.PayTotal).HasColumnName("pay_total");

                entity.Property(e => e.PayinAccount)
                    .HasMaxLength(500)
                    .HasColumnName("payin_account");

                entity.Property(e => e.Plafond).HasColumnName("plafond");

                entity.Property(e => e.PrdSegmentId).HasColumnName("prd_segment_id");

                entity.Property(e => e.Product).HasColumnName("product");

                entity.Property(e => e.SisaTenor).HasColumnName("sisa_tenor");

                entity.Property(e => e.StartDate).HasColumnName("start_date");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.StgDate).HasColumnName("stg_date");

                entity.Property(e => e.Tenor).HasColumnName("tenor");

                entity.Property(e => e.TunggakanBunga).HasColumnName("tunggakan_bunga");

                entity.Property(e => e.TunggakanDenda).HasColumnName("tunggakan_denda");

                entity.Property(e => e.TunggakanPokok).HasColumnName("tunggakan_pokok");

                entity.Property(e => e.TunggakanTotal).HasColumnName("tunggakan_total");
            });

            modelBuilder.Entity<MasterLoanHistory>(entity =>
            {
                entity.ToTable("master_loan_history");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.AccNo)
                    .HasMaxLength(20)
                    .HasColumnName("acc_no");

                entity.Property(e => e.CallBy).HasColumnName("call_by");

                entity.Property(e => e.Ccy)
                    .HasMaxLength(10)
                    .HasColumnName("ccy");

                entity.Property(e => e.ChannelBranchCode)
                    .HasMaxLength(50)
                    .HasColumnName("channel_branch_code");

                entity.Property(e => e.CuCif)
                    .HasMaxLength(20)
                    .HasColumnName("cu_cif");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.Dpd).HasColumnName("dpd");

                entity.Property(e => e.EconPhone)
                    .HasMaxLength(20)
                    .HasColumnName("econ_phone");

                entity.Property(e => e.EconRelation)
                    .HasMaxLength(10)
                    .HasColumnName("econ_relation");

                entity.Property(e => e.EconaName)
                    .HasMaxLength(100)
                    .HasColumnName("econa_name");

                entity.Property(e => e.Fasilitas)
                    .HasMaxLength(200)
                    .HasColumnName("fasilitas");

                entity.Property(e => e.Installment).HasColumnName("installment");

                entity.Property(e => e.InstallmentPokok).HasColumnName("installment_pokok");

                entity.Property(e => e.InterestRate).HasColumnName("interest_rate");

                entity.Property(e => e.KewajibanTotal).HasColumnName("kewajiban_total");

                entity.Property(e => e.Kolektibilitas).HasColumnName("kolektibilitas");

                entity.Property(e => e.LastPayDate).HasColumnName("last_pay_date");

                entity.Property(e => e.LoanId).HasColumnName("loan_id");

                entity.Property(e => e.MarketingCode)
                    .HasMaxLength(20)
                    .HasColumnName("marketing_code");

                entity.Property(e => e.MaturityDate).HasColumnName("maturity_date");

                entity.Property(e => e.Outstanding).HasColumnName("outstanding");

                entity.Property(e => e.PayTotal).HasColumnName("pay_total");

                entity.Property(e => e.PayinAccount)
                    .HasMaxLength(500)
                    .HasColumnName("payin_account");

                entity.Property(e => e.Plafond).HasColumnName("plafond");

                entity.Property(e => e.PrdSegmentId).HasColumnName("prd_segment_id");

                entity.Property(e => e.Product).HasColumnName("product");

                entity.Property(e => e.SisaTenor).HasColumnName("sisa_tenor");

                entity.Property(e => e.StartDate).HasColumnName("start_date");

                entity.Property(e => e.StgDate).HasColumnName("stg_date");

                entity.Property(e => e.Tenor).HasColumnName("tenor");

                entity.Property(e => e.TunggakanBunga).HasColumnName("tunggakan_bunga");

                entity.Property(e => e.TunggakanDenda).HasColumnName("tunggakan_denda");

                entity.Property(e => e.TunggakanPokok).HasColumnName("tunggakan_pokok");

                entity.Property(e => e.TunggakanTotal).HasColumnName("tunggakan_total");
            });

            modelBuilder.Entity<MasterNotari>(entity =>
            {
                entity.ToTable("master_notaris");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.BranchId).HasColumnName("branch_id");

                entity.Property(e => e.NotarisAddress)
                    .HasMaxLength(200)
                    .HasColumnName("notaris_address");

                entity.Property(e => e.NotarisName)
                    .HasMaxLength(100)
                    .HasColumnName("notaris_name");

                entity.Property(e => e.StatusId).HasColumnName("status_id");
            });

            modelBuilder.Entity<NotifContent>(entity =>
            {
                entity.HasKey(e => e.LscId)
                    .HasName("notif_content_pkey");

                entity.ToTable("notif_content");

                entity.Property(e => e.LscId).HasColumnName("lsc_id");

                entity.Property(e => e.LscApprovedBy)
                    .HasMaxLength(50)
                    .HasColumnName("lsc_approved_by");

                entity.Property(e => e.LscApprovedStatus)
                    .HasMaxLength(50)
                    .HasColumnName("lsc_approved_status");

                entity.Property(e => e.LscCode)
                    .HasMaxLength(50)
                    .HasColumnName("lsc_code");

                entity.Property(e => e.LscContent)
                    .HasMaxLength(500)
                    .HasColumnName("lsc_content");

                entity.Property(e => e.LscCreated).HasColumnName("lsc_created");

                entity.Property(e => e.LscCreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("lsc_created_by");

                entity.Property(e => e.LscDay)
                    .HasMaxLength(100)
                    .HasColumnName("lsc_day");

                entity.Property(e => e.LscIsActive).HasColumnName("lsc_is_active");

                entity.Property(e => e.LscIsDeleted).HasColumnName("lsc_is_deleted");

                entity.Property(e => e.LscIsUsed).HasColumnName("lsc_is_used");

                entity.Property(e => e.LscModified).HasColumnName("lsc_modified");

                entity.Property(e => e.LscModifiedBy)
                    .HasMaxLength(50)
                    .HasColumnName("lsc_modified_by");

                entity.Property(e => e.LscName)
                    .HasMaxLength(100)
                    .HasColumnName("lsc_name");
            });

            modelBuilder.Entity<PaymentHistory>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("payment_history");

                entity.Property(e => e.AccNo)
                    .HasMaxLength(100)
                    .HasColumnName("acc_no");

                entity.Property(e => e.Bunga).HasColumnName("bunga");

                entity.Property(e => e.CallBy).HasColumnName("call_by");

                entity.Property(e => e.CreateDate).HasColumnName("create_date");

                entity.Property(e => e.Denda).HasColumnName("denda");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.LoanId).HasColumnName("loan_id");

                entity.Property(e => e.PokokCicilan).HasColumnName("pokok_cicilan");

                entity.Property(e => e.Tgl).HasColumnName("tgl");

                entity.Property(e => e.TotalBayar).HasColumnName("total_bayar");
            });

            modelBuilder.Entity<PaymentHistoryBk20240513>(entity =>
            {
                entity.ToTable("payment_history_bk20240513");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('payment_history_id_seq'::regclass)");

                entity.Property(e => e.AccNo)
                    .HasMaxLength(100)
                    .HasColumnName("acc_no");

                entity.Property(e => e.Bunga).HasColumnName("bunga");

                entity.Property(e => e.CallBy).HasColumnName("call_by");

                entity.Property(e => e.CreateDate).HasColumnName("create_date");

                entity.Property(e => e.Denda).HasColumnName("denda");

                entity.Property(e => e.LoanId).HasColumnName("loan_id");

                entity.Property(e => e.PokokCicilan).HasColumnName("pokok_cicilan");

                entity.Property(e => e.Tgl).HasColumnName("tgl");

                entity.Property(e => e.TotalBayar).HasColumnName("total_bayar");
            });

            modelBuilder.Entity<PaymentRecord>(entity =>
            {
                entity.ToTable("payment_record");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccNo)
                    .HasMaxLength(100)
                    .HasColumnName("acc_no");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.CallBy).HasColumnName("call_by");

                entity.Property(e => e.CallId).HasColumnName("call_id");

                entity.Property(e => e.RecordDate).HasColumnName("record_date");
            });

            modelBuilder.Entity<Reason>(entity =>
            {
                entity.HasKey(e => e.RsnId)
                    .HasName("reason_pkey");

                entity.ToTable("reason");

                entity.Property(e => e.RsnId).HasColumnName("rsn_id");

                entity.Property(e => e.RsnApprovedBy)
                    .HasMaxLength(50)
                    .HasColumnName("rsn_approved_by");

                entity.Property(e => e.RsnApprovedStatus)
                    .HasMaxLength(50)
                    .HasColumnName("rsn_approved_status");

                entity.Property(e => e.RsnCode)
                    .HasMaxLength(50)
                    .HasColumnName("rsn_code");

                entity.Property(e => e.RsnCreated).HasColumnName("rsn_created");

                entity.Property(e => e.RsnCreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("rsn_created_by");

                entity.Property(e => e.RsnIsActive).HasColumnName("rsn_is_active");

                entity.Property(e => e.RsnIsDc).HasColumnName("rsn_is_dc");

                entity.Property(e => e.RsnIsDeleted).HasColumnName("rsn_is_deleted");

                entity.Property(e => e.RsnIsFc).HasColumnName("rsn_is_fc");

                entity.Property(e => e.RsnIsUsed).HasColumnName("rsn_is_used");

                entity.Property(e => e.RsnModified).HasColumnName("rsn_modified");

                entity.Property(e => e.RsnModifiedBy)
                    .HasMaxLength(50)
                    .HasColumnName("rsn_modified_by");

                entity.Property(e => e.RsnName)
                    .HasMaxLength(500)
                    .HasColumnName("rsn_name");
            });

            modelBuilder.Entity<Rfproduct>(entity =>
            {
                entity.HasKey(e => e.PrdId)
                    .HasName("rfproduct_pkey");

                entity.ToTable("rfproduct");

                entity.Property(e => e.PrdId).HasColumnName("prd_id");

                entity.Property(e => e.PrdCode)
                    .HasMaxLength(50)
                    .HasColumnName("prd_code");

                entity.Property(e => e.PrdCoreCode)
                    .HasMaxLength(50)
                    .HasColumnName("prd_core_code");

                entity.Property(e => e.PrdCreateDate).HasColumnName("prd_create_date");

                entity.Property(e => e.PrdDesc)
                    .HasMaxLength(50)
                    .HasColumnName("prd_desc");

                entity.Property(e => e.PrdSgmId).HasColumnName("prd_sgm_id");

                entity.Property(e => e.PrdStatusId).HasColumnName("prd_status_id");

                entity.Property(e => e.PrdUpdateDate).HasColumnName("prd_update_date");
            });

            modelBuilder.Entity<RfproductSegment>(entity =>
            {
                entity.HasKey(e => e.PrdSgmId)
                    .HasName("rfproduct_segment_pkey");

                entity.ToTable("rfproduct_segment");

                entity.Property(e => e.PrdSgmId).HasColumnName("prd_sgm_id");

                entity.Property(e => e.PrdSgmCode)
                    .HasMaxLength(50)
                    .HasColumnName("prd_sgm_code");

                entity.Property(e => e.PrdSgmCoreCode)
                    .HasMaxLength(50)
                    .HasColumnName("prd_sgm_core_code");

                entity.Property(e => e.PrdSgmCreateDate).HasColumnName("prd_sgm_create_date");

                entity.Property(e => e.PrdSgmDesc)
                    .HasMaxLength(50)
                    .HasColumnName("prd_sgm_desc");

                entity.Property(e => e.PrdSgmStatusId).HasColumnName("prd_sgm_status_id");

                entity.Property(e => e.PrdSgmUpdateDate).HasColumnName("prd_sgm_update_date");
            });

            modelBuilder.Entity<Rfresult>(entity =>
            {
                entity.HasKey(e => e.RfrId)
                    .HasName("rfresult_pkey");

                entity.ToTable("rfresult");

                entity.Property(e => e.RfrId).HasColumnName("rfr_id");

                entity.Property(e => e.RfrCreateDate).HasColumnName("rfr_create_date");

                entity.Property(e => e.RfrIsDc).HasColumnName("rfr_is_dc");

                entity.Property(e => e.RfrIsFc).HasColumnName("rfr_is_fc");

                entity.Property(e => e.RfrRlCode)
                    .HasMaxLength(50)
                    .HasColumnName("rfr_rl_code");

                entity.Property(e => e.RfrRlDesc)
                    .HasMaxLength(50)
                    .HasColumnName("rfr_rl_desc");

                entity.Property(e => e.RfrStatusId).HasColumnName("rfr_status_id");

                entity.Property(e => e.RfrUpdateDate).HasColumnName("rfr_update_date");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => e.StsId)
                    .HasName("status_pkey");

                entity.ToTable("status");

                entity.Property(e => e.StsId).HasColumnName("sts_id");

                entity.Property(e => e.StsName)
                    .HasMaxLength(500)
                    .HasColumnName("sts_name");

                entity.Property(e => e.StsType)
                    .HasMaxLength(500)
                    .HasColumnName("sts_type");
            });

            modelBuilder.Entity<StgMasterLoan>(entity =>
            {
                entity.ToTable("stg_master_loan");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccNo)
                    .HasMaxLength(20)
                    .HasColumnName("acc_no");

                entity.Property(e => e.Ccy)
                    .HasMaxLength(10)
                    .HasColumnName("ccy");

                entity.Property(e => e.ChannelBranchCode)
                    .HasMaxLength(50)
                    .HasColumnName("channel_branch_code");

                entity.Property(e => e.CuCif)
                    .HasMaxLength(20)
                    .HasColumnName("cu_cif");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.Dpd).HasColumnName("dpd");

                entity.Property(e => e.EconPhone)
                    .HasMaxLength(20)
                    .HasColumnName("econ_phone");

                entity.Property(e => e.EconRelation)
                    .HasMaxLength(10)
                    .HasColumnName("econ_relation");

                entity.Property(e => e.EconaName)
                    .HasMaxLength(100)
                    .HasColumnName("econa_name");

                entity.Property(e => e.Fasilitas)
                    .HasMaxLength(200)
                    .HasColumnName("fasilitas");

                entity.Property(e => e.FileDate)
                    .HasColumnType("date")
                    .HasColumnName("file_date");

                entity.Property(e => e.Installment).HasColumnName("installment");

                entity.Property(e => e.InstallmentPokok).HasColumnName("installment_pokok");

                entity.Property(e => e.InterestRate).HasColumnName("interest_rate");

                entity.Property(e => e.KewajibanTotal).HasColumnName("kewajiban_total");

                entity.Property(e => e.Kolektibilitas).HasColumnName("kolektibilitas");

                entity.Property(e => e.LastPayDate).HasColumnName("last_pay_date");

                entity.Property(e => e.LoanNumber)
                    .HasMaxLength(100)
                    .HasColumnName("loan_number");

                entity.Property(e => e.MarketingCode)
                    .HasMaxLength(20)
                    .HasColumnName("marketing_code");

                entity.Property(e => e.MaturityDate).HasColumnName("maturity_date");

                entity.Property(e => e.NotarisId).HasColumnName("notaris_id");

                entity.Property(e => e.Outstanding).HasColumnName("outstanding");

                entity.Property(e => e.PayTotal).HasColumnName("pay_total");

                entity.Property(e => e.PayinAccount)
                    .HasMaxLength(500)
                    .HasColumnName("payin_account");

                entity.Property(e => e.Plafond).HasColumnName("plafond");

                entity.Property(e => e.PrdSegmentId).HasColumnName("prd_segment_id");

                entity.Property(e => e.Product).HasColumnName("product");

                entity.Property(e => e.SisaTenor).HasColumnName("sisa_tenor");

                entity.Property(e => e.StartDate).HasColumnName("start_date");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.StgDate).HasColumnName("stg_date");

                entity.Property(e => e.Tenor).HasColumnName("tenor");

                entity.Property(e => e.TunggakanBunga).HasColumnName("tunggakan_bunga");

                entity.Property(e => e.TunggakanDenda).HasColumnName("tunggakan_denda");

                entity.Property(e => e.TunggakanPokok).HasColumnName("tunggakan_pokok");

                entity.Property(e => e.TunggakanTotal).HasColumnName("tunggakan_total");
            });

            modelBuilder.Entity<Tracking>(entity =>
            {
                entity.HasKey(e => e.TrxId)
                    .HasName("tracking_pkey");

                entity.ToTable("tracking");

                entity.Property(e => e.TrxId).HasColumnName("trx_id");

                entity.Property(e => e.TrxLat).HasColumnName("trx_lat");

                entity.Property(e => e.TrxLon).HasColumnName("trx_lon");

                entity.Property(e => e.TrxTgl).HasColumnName("trx_tgl");

                entity.Property(e => e.TrxUsrid).HasColumnName("trx_usrid");
            });

            modelBuilder.Entity<Workflowconfig>(entity =>
            {
                entity.ToTable("workflowconfig");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Orderstep).HasColumnName("orderstep");

                entity.Property(e => e.Roles).HasColumnName("roles");

                entity.Property(e => e.Testing)
                    .HasColumnType("character varying")
                    .HasColumnName("testing");

                entity.Property(e => e.Testing2)
                    .HasColumnType("character varying")
                    .HasColumnName("testing2");

                entity.Property(e => e.Workflowconfigdetailid).HasColumnName("workflowconfigdetailid");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
