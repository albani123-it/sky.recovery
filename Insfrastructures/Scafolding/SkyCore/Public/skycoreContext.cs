using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyCore.Public
{
    public partial class skycoreContext : DbContext
    {
        public skycoreContext()
        {
        }

        public skycoreContext(DbContextOptions<skycoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<LevelMaster> LevelMasters { get; set; }
        public virtual DbSet<LevelUser> LevelUsers { get; set; }
        public virtual DbSet<LogActivity> LogActivities { get; set; }
        public virtual DbSet<LogCategory> LogCategories { get; set; }
        public virtual DbSet<LoginLog> LoginLogs { get; set; }
        public virtual DbSet<MasterTableLog> MasterTableLogs { get; set; }
        public virtual DbSet<MenuList> MenuLists { get; set; }
        public virtual DbSet<MenuList20230713> MenuList20230713s { get; set; }
        public virtual DbSet<MenuList20230802> MenuList20230802s { get; set; }
        public virtual DbSet<MenuList20231102> MenuList20231102s { get; set; }
        public virtual DbSet<MenuList20231128> MenuList20231128s { get; set; }
        public virtual DbSet<MenuList20240123> MenuList20240123s { get; set; }
        public virtual DbSet<MenuListLog> MenuListLogs { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RoleDetail> RoleDetails { get; set; }
        public virtual DbSet<RoleDetailLog> RoleDetailLogs { get; set; }
        public virtual DbSet<RoleLog> RoleLogs { get; set; }
        public virtual DbSet<RoleMaster> RoleMasters { get; set; }
        public virtual DbSet<RoleMaster20231128> RoleMaster20231128s { get; set; }
        public virtual DbSet<RoleMaster20242301> RoleMaster20242301s { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserReference> UserReferences { get; set; }
        public virtual DbSet<UsersLog> UsersLogs { get; set; }
        public virtual DbSet<UsersLogin> UsersLogins { get; set; }
        public virtual DbSet<UsersNotif> UsersNotifs { get; set; }
        public virtual DbSet<UsersPosition> UsersPositions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=103.53.197.67;Database=sky.core;User Id=postgres;Password=User123!");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "English_United States.1252");

            modelBuilder.Entity<LevelMaster>(entity =>
            {
                entity.ToTable("level_master");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.LmDescription)
                    .HasMaxLength(150)
                    .HasColumnName("lm_description")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.LmModul)
                    .HasMaxLength(255)
                    .HasColumnName("lm_modul");

                entity.Property(e => e.LmName)
                    .HasMaxLength(100)
                    .HasColumnName("lm_name")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.LmParentid).HasColumnName("lm_parentid");

                entity.Property(e => e.LmUrut).HasColumnName("lm_urut");
            });

            modelBuilder.Entity<LevelUser>(entity =>
            {
                entity.HasKey(e => e.LuId)
                    .HasName("level_user_pkey");

                entity.ToTable("level_user");

                entity.Property(e => e.LuId).HasColumnName("lu_id");

                entity.Property(e => e.LuLevelCode)
                    .HasMaxLength(50)
                    .HasColumnName("lu_level_code")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.LuLevelDescription)
                    .HasMaxLength(150)
                    .HasColumnName("lu_level_description")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.LuLevelName)
                    .HasMaxLength(150)
                    .HasColumnName("lu_level_name")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.LuLimitFrom)
                    .HasPrecision(18, 2)
                    .HasColumnName("lu_limit_from");

                entity.Property(e => e.LuLimitTo)
                    .HasPrecision(18, 2)
                    .HasColumnName("lu_limit_to");

                entity.Property(e => e.LuStatus).HasColumnName("lu_status");
            });

            modelBuilder.Entity<LogActivity>(entity =>
            {
                entity.HasKey(e => e.LogId)
                    .HasName("log_activity_pkey");

                entity.ToTable("log_activity");

                entity.Property(e => e.LogId).HasColumnName("log_id");

                entity.Property(e => e.ClientIp)
                    .HasMaxLength(100)
                    .HasColumnName("client_ip")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.LogActionDate).HasColumnName("log_action_date");

                entity.Property(e => e.LogActivity1)
                    .HasMaxLength(500)
                    .HasColumnName("log_activity")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.LogBrowser)
                    .HasMaxLength(50)
                    .HasColumnName("log_browser")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.LogCode)
                    .HasMaxLength(100)
                    .HasColumnName("log_code")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.LogHttps)
                    .HasMaxLength(5)
                    .HasColumnName("log_https")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.LogOperatingSystem)
                    .HasMaxLength(100)
                    .HasColumnName("log_operating_system")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.LogPageUrl)
                    .HasMaxLength(100)
                    .HasColumnName("log_page_url")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.LogQueryString)
                    .HasMaxLength(20000)
                    .HasColumnName("log_query_string")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.LogServerName)
                    .HasMaxLength(100)
                    .HasColumnName("log_server_name")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.LogUserid)
                    .HasMaxLength(50)
                    .HasColumnName("log_userid")
                    .HasDefaultValueSql("NULL::character varying");
            });

            modelBuilder.Entity<LogCategory>(entity =>
            {
                entity.HasKey(e => e.LgcId)
                    .HasName("log_category_pkey");

                entity.ToTable("log_category");

                entity.Property(e => e.LgcId).HasColumnName("lgc_id");

                entity.Property(e => e.LgcLogCode)
                    .HasMaxLength(100)
                    .HasColumnName("lgc_log_code");

                entity.Property(e => e.LgcName)
                    .HasMaxLength(250)
                    .HasColumnName("lgc_name");

                entity.Property(e => e.LgcVal)
                    .HasMaxLength(100)
                    .HasColumnName("lgc_val");
            });

            modelBuilder.Entity<LoginLog>(entity =>
            {
                entity.HasKey(e => e.LogId)
                    .HasName("login_log_pkey");

                entity.ToTable("login_log");

                entity.Property(e => e.LogId).HasColumnName("log_id");

                entity.Property(e => e.LogActionDate).HasColumnName("log_action_date");

                entity.Property(e => e.LogActionMode)
                    .HasMaxLength(100)
                    .HasColumnName("log_action_mode")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.LogUsr)
                    .HasMaxLength(100)
                    .HasColumnName("log_usr")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.UsrAccessLevel)
                    .HasMaxLength(1000)
                    .HasColumnName("usr_access_level")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.UsrBranch)
                    .HasMaxLength(1000)
                    .HasColumnName("usr_branch")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.UsrEfectiveDate)
                    .HasColumnType("date")
                    .HasColumnName("usr_efective_date");

                entity.Property(e => e.UsrEmail)
                    .HasMaxLength(100)
                    .HasColumnName("usr_email")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.UsrFailLogin).HasColumnName("usr_fail_login");

                entity.Property(e => e.UsrId).HasColumnName("usr_id");

                entity.Property(e => e.UsrImgProfile).HasColumnName("usr_img_profile");

                entity.Property(e => e.UsrIpAddress)
                    .HasMaxLength(50)
                    .HasColumnName("usr_ip_address")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.UsrIsLogin).HasColumnName("usr_is_login");

                entity.Property(e => e.UsrLastAccess).HasColumnName("usr_last_access");

                entity.Property(e => e.UsrLastAccessF)
                    .HasColumnType("date")
                    .HasColumnName("usr_last_access_f");

                entity.Property(e => e.UsrName)
                    .HasMaxLength(1000)
                    .HasColumnName("usr_name")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.UsrNip)
                    .HasMaxLength(50)
                    .HasColumnName("usr_nip")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.UsrPosition)
                    .HasMaxLength(100)
                    .HasColumnName("usr_position")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.UsrStatus).HasColumnName("usr_status");

                entity.Property(e => e.UsrSupervisor)
                    .HasMaxLength(100)
                    .HasColumnName("usr_supervisor")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.UsrUserid)
                    .HasMaxLength(20)
                    .HasColumnName("usr_userid")
                    .HasDefaultValueSql("NULL::character varying");
            });

            modelBuilder.Entity<MasterTableLog>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("master_table_log");

                entity.Property(e => e.TblAction)
                    .HasMaxLength(50)
                    .HasColumnName("tbl_action");

                entity.Property(e => e.TblCode)
                    .HasMaxLength(250)
                    .HasColumnName("tbl_code");

                entity.Property(e => e.TblDatabase)
                    .HasMaxLength(150)
                    .HasColumnName("tbl_database");

                entity.Property(e => e.TblFor)
                    .HasMaxLength(10)
                    .HasColumnName("tbl_for");

                entity.Property(e => e.TblId).HasColumnName("tbl_id");

                entity.Property(e => e.TblLogid).HasColumnName("tbl_logid");

                entity.Property(e => e.TblName)
                    .HasMaxLength(500)
                    .HasColumnName("tbl_name");

                entity.Property(e => e.TblSchema)
                    .HasMaxLength(100)
                    .HasColumnName("tbl_schema");

                entity.Property(e => e.TblTimestamp).HasColumnName("tbl_timestamp");

                entity.Property(e => e.TblTmpField)
                    .HasMaxLength(50)
                    .HasColumnName("tbl_tmp_field");

                entity.Property(e => e.TblUniqueField)
                    .HasMaxLength(500)
                    .HasColumnName("tbl_unique_field");

                entity.Property(e => e.TblUniqueFieldType)
                    .HasMaxLength(50)
                    .HasColumnName("tbl_unique_field_type");

                entity.Property(e => e.TblUsrby)
                    .HasMaxLength(50)
                    .HasColumnName("tbl_usrby");
            });

            modelBuilder.Entity<MenuList>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("menu_list");

                entity.Property(e => e.Breadcrumb)
                    .HasMaxLength(1000)
                    .HasColumnName("breadcrumb");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.MnAcl)
                    .HasMaxLength(100)
                    .HasColumnName("mn_acl");

                entity.Property(e => e.MnIcon)
                    .HasMaxLength(50)
                    .HasColumnName("mn_icon");

                entity.Property(e => e.MnLink)
                    .HasMaxLength(150)
                    .HasColumnName("mn_link");

                entity.Property(e => e.MnModule)
                    .HasMaxLength(500)
                    .HasColumnName("mn_module");

                entity.Property(e => e.MnName)
                    .HasMaxLength(100)
                    .HasColumnName("mn_name");

                entity.Property(e => e.MnOrder).HasColumnName("mn_order");

                entity.Property(e => e.MnParentid).HasColumnName("mn_parentid");
            });

            modelBuilder.Entity<MenuList20230713>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("menu_list_20230713");

                entity.Property(e => e.Breadcrumb)
                    .HasMaxLength(1000)
                    .HasColumnName("breadcrumb");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.MnAcl)
                    .HasMaxLength(100)
                    .HasColumnName("mn_acl");

                entity.Property(e => e.MnIcon)
                    .HasMaxLength(50)
                    .HasColumnName("mn_icon");

                entity.Property(e => e.MnLink)
                    .HasMaxLength(150)
                    .HasColumnName("mn_link");

                entity.Property(e => e.MnModule)
                    .HasMaxLength(500)
                    .HasColumnName("mn_module");

                entity.Property(e => e.MnName)
                    .HasMaxLength(100)
                    .HasColumnName("mn_name");

                entity.Property(e => e.MnOrder).HasColumnName("mn_order");

                entity.Property(e => e.MnParentid).HasColumnName("mn_parentid");
            });

            modelBuilder.Entity<MenuList20230802>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("menu_list_20230802");

                entity.Property(e => e.Breadcrumb)
                    .HasMaxLength(1000)
                    .HasColumnName("breadcrumb");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.MnAcl)
                    .HasMaxLength(100)
                    .HasColumnName("mn_acl");

                entity.Property(e => e.MnIcon)
                    .HasMaxLength(50)
                    .HasColumnName("mn_icon");

                entity.Property(e => e.MnLink)
                    .HasMaxLength(150)
                    .HasColumnName("mn_link");

                entity.Property(e => e.MnModule)
                    .HasMaxLength(500)
                    .HasColumnName("mn_module");

                entity.Property(e => e.MnName)
                    .HasMaxLength(100)
                    .HasColumnName("mn_name");

                entity.Property(e => e.MnOrder).HasColumnName("mn_order");

                entity.Property(e => e.MnParentid).HasColumnName("mn_parentid");
            });

            modelBuilder.Entity<MenuList20231102>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("menu_list_20231102");

                entity.Property(e => e.Breadcrumb)
                    .HasMaxLength(1000)
                    .HasColumnName("breadcrumb");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.MnAcl)
                    .HasMaxLength(100)
                    .HasColumnName("mn_acl");

                entity.Property(e => e.MnIcon)
                    .HasMaxLength(50)
                    .HasColumnName("mn_icon");

                entity.Property(e => e.MnLink)
                    .HasMaxLength(150)
                    .HasColumnName("mn_link");

                entity.Property(e => e.MnModule)
                    .HasMaxLength(500)
                    .HasColumnName("mn_module");

                entity.Property(e => e.MnName)
                    .HasMaxLength(100)
                    .HasColumnName("mn_name");

                entity.Property(e => e.MnOrder).HasColumnName("mn_order");

                entity.Property(e => e.MnParentid).HasColumnName("mn_parentid");
            });

            modelBuilder.Entity<MenuList20231128>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("menu_list_20231128");

                entity.Property(e => e.Breadcrumb)
                    .HasMaxLength(1000)
                    .HasColumnName("breadcrumb");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.MnAcl)
                    .HasMaxLength(100)
                    .HasColumnName("mn_acl");

                entity.Property(e => e.MnIcon)
                    .HasMaxLength(50)
                    .HasColumnName("mn_icon");

                entity.Property(e => e.MnLink)
                    .HasMaxLength(150)
                    .HasColumnName("mn_link");

                entity.Property(e => e.MnModule)
                    .HasMaxLength(500)
                    .HasColumnName("mn_module");

                entity.Property(e => e.MnName)
                    .HasMaxLength(100)
                    .HasColumnName("mn_name");

                entity.Property(e => e.MnOrder).HasColumnName("mn_order");

                entity.Property(e => e.MnParentid).HasColumnName("mn_parentid");
            });

            modelBuilder.Entity<MenuList20240123>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("menu_list_20240123");

                entity.Property(e => e.Breadcrumb)
                    .HasMaxLength(1000)
                    .HasColumnName("breadcrumb");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.MnAcl)
                    .HasMaxLength(100)
                    .HasColumnName("mn_acl");

                entity.Property(e => e.MnIcon)
                    .HasMaxLength(50)
                    .HasColumnName("mn_icon");

                entity.Property(e => e.MnLink)
                    .HasMaxLength(150)
                    .HasColumnName("mn_link");

                entity.Property(e => e.MnModule)
                    .HasMaxLength(500)
                    .HasColumnName("mn_module");

                entity.Property(e => e.MnName)
                    .HasMaxLength(100)
                    .HasColumnName("mn_name");

                entity.Property(e => e.MnOrder).HasColumnName("mn_order");

                entity.Property(e => e.MnParentid).HasColumnName("mn_parentid");
            });

            modelBuilder.Entity<MenuListLog>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("menu_list_log");

                entity.Property(e => e.Breadcrumb)
                    .HasMaxLength(1000)
                    .HasColumnName("breadcrumb");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.LogAction)
                    .HasMaxLength(100)
                    .HasColumnName("log_action");

                entity.Property(e => e.LogDate).HasColumnName("log_date");

                entity.Property(e => e.LogId).HasColumnName("log_id");

                entity.Property(e => e.MnAcl)
                    .HasMaxLength(100)
                    .HasColumnName("mn_acl");

                entity.Property(e => e.MnIcon)
                    .HasMaxLength(50)
                    .HasColumnName("mn_icon");

                entity.Property(e => e.MnLink)
                    .HasMaxLength(150)
                    .HasColumnName("mn_link");

                entity.Property(e => e.MnName)
                    .HasMaxLength(100)
                    .HasColumnName("mn_name");

                entity.Property(e => e.MnOrder).HasColumnName("mn_order");

                entity.Property(e => e.MnParentid).HasColumnName("mn_parentid");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.RlId)
                    .HasName("role_pkey");

                entity.ToTable("role");

                entity.Property(e => e.RlId)
                    .HasColumnName("rl_id")
                    .HasDefaultValueSql("nextval('role_rl_id_seq1'::regclass)");

                entity.Property(e => e.RlCreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("rl_created_by");

                entity.Property(e => e.RlCreatedDate).HasColumnName("rl_created_date");

                entity.Property(e => e.RlDescription).HasColumnName("rl_description");

                entity.Property(e => e.RlName)
                    .HasMaxLength(250)
                    .HasColumnName("rl_name");

                entity.Property(e => e.RlStatus).HasColumnName("rl_status");

                entity.Property(e => e.RlUpdatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("rl_updated_by");

                entity.Property(e => e.RlUpdatedDate).HasColumnName("rl_updated_date");
            });

            modelBuilder.Entity<RoleDetail>(entity =>
            {
                entity.HasKey(e => e.RldId)
                    .HasName("role_detail_pkey");

                entity.ToTable("role_detail");

                entity.Property(e => e.RldId)
                    .HasColumnName("rld_id")
                    .HasDefaultValueSql("nextval('role_detail_rld_id_seq1'::regclass)");

                entity.Property(e => e.RldRlId).HasColumnName("rld_rl_id");

                entity.Property(e => e.RldRlmId).HasColumnName("rld_rlm_id");
            });

            modelBuilder.Entity<RoleDetailLog>(entity =>
            {
                entity.ToTable("role_detail_log");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.RldActionBy)
                    .HasMaxLength(50)
                    .HasColumnName("rld_action_by");

                entity.Property(e => e.RldId)
                    .HasColumnName("rld_id")
                    .HasDefaultValueSql("nextval('role_detail_rld_id_seq'::regclass)");

                entity.Property(e => e.RldLogAction)
                    .HasMaxLength(100)
                    .HasColumnName("rld_log_action");

                entity.Property(e => e.RldLogDate).HasColumnName("rld_log_date");

                entity.Property(e => e.RldLogid).HasColumnName("rld_logid");

                entity.Property(e => e.RldRlId).HasColumnName("rld_rl_id");

                entity.Property(e => e.RldRlmId).HasColumnName("rld_rlm_id");
            });

            modelBuilder.Entity<RoleLog>(entity =>
            {
                entity.ToTable("role_log");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.RlActionBy)
                    .HasMaxLength(50)
                    .HasColumnName("rl_action_by");

                entity.Property(e => e.RlDescription).HasColumnName("rl_description");

                entity.Property(e => e.RlId).HasColumnName("rl_id");

                entity.Property(e => e.RlLogAction)
                    .HasMaxLength(100)
                    .HasColumnName("rl_log_action");

                entity.Property(e => e.RlLogDate).HasColumnName("rl_log_date");

                entity.Property(e => e.RlLogid).HasColumnName("rl_logid");

                entity.Property(e => e.RlName)
                    .HasMaxLength(250)
                    .HasColumnName("rl_name");

                entity.Property(e => e.RlStatus).HasColumnName("rl_status");
            });

            modelBuilder.Entity<RoleMaster>(entity =>
            {
                entity.HasKey(e => e.RlmId)
                    .HasName("role_master_pk");

                entity.ToTable("role_master");

                entity.Property(e => e.RlmId)
                    .ValueGeneratedNever()
                    .HasColumnName("rlm_id");

                entity.Property(e => e.IdSeq)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id_seq");

                entity.Property(e => e.RlmCode)
                    .HasMaxLength(250)
                    .HasColumnName("rlm_code");

                entity.Property(e => e.RlmIsAction).HasColumnName("rlm_is_action");

                entity.Property(e => e.RlmLicenseModule)
                    .HasMaxLength(500)
                    .HasColumnName("rlm_license_module");

                entity.Property(e => e.RlmName)
                    .HasMaxLength(100)
                    .HasColumnName("rlm_name");

                entity.Property(e => e.RlmOrder).HasColumnName("rlm_order");

                entity.Property(e => e.RlmParentid).HasColumnName("rlm_parentid");
            });

            modelBuilder.Entity<RoleMaster20231128>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("role_master_20231128");

                entity.Property(e => e.RlmCode)
                    .HasMaxLength(250)
                    .HasColumnName("rlm_code");

                entity.Property(e => e.RlmId).HasColumnName("rlm_id");

                entity.Property(e => e.RlmIsAction).HasColumnName("rlm_is_action");

                entity.Property(e => e.RlmLicenseModule)
                    .HasMaxLength(500)
                    .HasColumnName("rlm_license_module");

                entity.Property(e => e.RlmName)
                    .HasMaxLength(100)
                    .HasColumnName("rlm_name");

                entity.Property(e => e.RlmOrder).HasColumnName("rlm_order");

                entity.Property(e => e.RlmParentid).HasColumnName("rlm_parentid");
            });

            modelBuilder.Entity<RoleMaster20242301>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("role_master_20242301");

                entity.Property(e => e.RlmCode)
                    .HasMaxLength(250)
                    .HasColumnName("rlm_code");

                entity.Property(e => e.RlmId).HasColumnName("rlm_id");

                entity.Property(e => e.RlmIsAction).HasColumnName("rlm_is_action");

                entity.Property(e => e.RlmLicenseModule)
                    .HasMaxLength(500)
                    .HasColumnName("rlm_license_module");

                entity.Property(e => e.RlmName)
                    .HasMaxLength(100)
                    .HasColumnName("rlm_name");

                entity.Property(e => e.RlmOrder).HasColumnName("rlm_order");

                entity.Property(e => e.RlmParentid).HasColumnName("rlm_parentid");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UsrId)
                    .HasName("users_pkey");

                entity.ToTable("users");

                entity.Property(e => e.UsrId).HasColumnName("usr_id");

                entity.Property(e => e.UsrAccessLevel)
                    .HasMaxLength(1000)
                    .HasColumnName("usr_access_level")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.UsrApprovedBy)
                    .HasMaxLength(50)
                    .HasColumnName("usr_approved_by");

                entity.Property(e => e.UsrApprovedStatus)
                    .HasMaxLength(50)
                    .HasColumnName("usr_approved_status");

                entity.Property(e => e.UsrBranch)
                    .HasMaxLength(1000)
                    .HasColumnName("usr_branch")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.UsrEfectiveDate)
                    .HasColumnType("date")
                    .HasColumnName("usr_efective_date");

                entity.Property(e => e.UsrEmail)
                    .HasMaxLength(100)
                    .HasColumnName("usr_email")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.UsrFailLogin).HasColumnName("usr_fail_login");

                entity.Property(e => e.UsrGroupAlias)
                    .HasMaxLength(3)
                    .HasColumnName("usr_group_alias");

                entity.Property(e => e.UsrImgProfile).HasColumnName("usr_img_profile");

                entity.Property(e => e.UsrIpAddress)
                    .HasMaxLength(50)
                    .HasColumnName("usr_ip_address")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.UsrIsLogin).HasColumnName("usr_is_login");

                entity.Property(e => e.UsrIsfirstLogin).HasColumnName("usr_isfirst_login");

                entity.Property(e => e.UsrLastAccess)
                    .HasColumnType("timestamp(6) with time zone")
                    .HasColumnName("usr_last_access");

                entity.Property(e => e.UsrLastAccessF)
                    .HasColumnType("date")
                    .HasColumnName("usr_last_access_f");

                entity.Property(e => e.UsrName)
                    .HasMaxLength(1000)
                    .HasColumnName("usr_name")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.UsrNip)
                    .HasMaxLength(50)
                    .HasColumnName("usr_nip")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.UsrNotlp)
                    .HasMaxLength(100)
                    .HasColumnName("usr_notlp")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.UsrPartnerAlias)
                    .HasMaxLength(3)
                    .HasColumnName("usr_partner_alias");

                entity.Property(e => e.UsrPosition)
                    .HasMaxLength(100)
                    .HasColumnName("usr_position")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.UsrPrivilege).HasColumnName("usr_privilege");

                entity.Property(e => e.UsrStatus).HasColumnName("usr_status");

                entity.Property(e => e.UsrSupervisor)
                    .HasMaxLength(100)
                    .HasColumnName("usr_supervisor")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.UsrTntAlias)
                    .HasMaxLength(3)
                    .HasColumnName("usr_tnt_alias");

                entity.Property(e => e.UsrUserid)
                    .HasMaxLength(25)
                    .HasColumnName("usr_userid")
                    .HasDefaultValueSql("NULL::character varying");
            });

            modelBuilder.Entity<UserReference>(entity =>
            {
                entity.HasKey(e => e.UsrId)
                    .HasName("user_reference_pkey");

                entity.ToTable("user_reference");

                entity.Property(e => e.UsrId).HasColumnName("usr_id");

                entity.Property(e => e.UsrSupervisorId)
                    .HasMaxLength(50)
                    .HasColumnName("usr_supervisor_id")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.UsrUpdateBy)
                    .HasMaxLength(50)
                    .HasColumnName("usr_update_by")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.UsrUpdateDate)
                    .HasColumnType("date")
                    .HasColumnName("usr_update_date");

                entity.Property(e => e.UsrUserId)
                    .HasMaxLength(50)
                    .HasColumnName("usr_user_id")
                    .HasDefaultValueSql("NULL::character varying");
            });

            modelBuilder.Entity<UsersLog>(entity =>
            {
                entity.HasKey(e => e.UsrId)
                    .HasName("users_log_pkey");

                entity.ToTable("users_log");

                entity.Property(e => e.UsrId).HasColumnName("usr_id");

                entity.Property(e => e.LogActionDate).HasColumnName("log_action_date");

                entity.Property(e => e.LogActionMode)
                    .HasMaxLength(50)
                    .HasColumnName("log_action_mode");

                entity.Property(e => e.LogId).HasColumnName("log_id");

                entity.Property(e => e.LogUsr)
                    .HasMaxLength(100)
                    .HasColumnName("log_usr");

                entity.Property(e => e.UsrAccessLevel)
                    .HasMaxLength(1000)
                    .HasColumnName("usr_access_level")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.UsrApprovedBy)
                    .HasMaxLength(50)
                    .HasColumnName("usr_approved_by");

                entity.Property(e => e.UsrApprovedStatus)
                    .HasMaxLength(50)
                    .HasColumnName("usr_approved_status");

                entity.Property(e => e.UsrBranch)
                    .HasMaxLength(1000)
                    .HasColumnName("usr_branch")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.UsrEfectiveDate)
                    .HasColumnType("date")
                    .HasColumnName("usr_efective_date");

                entity.Property(e => e.UsrEmail)
                    .HasMaxLength(100)
                    .HasColumnName("usr_email")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.UsrFailLogin).HasColumnName("usr_fail_login");

                entity.Property(e => e.UsrGroupAlias)
                    .HasMaxLength(5)
                    .HasColumnName("usr_group_alias");

                entity.Property(e => e.UsrImgProfile).HasColumnName("usr_img_profile");

                entity.Property(e => e.UsrIpAddress)
                    .HasMaxLength(50)
                    .HasColumnName("usr_ip_address")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.UsrIsLogin).HasColumnName("usr_is_login");

                entity.Property(e => e.UsrLastAccess)
                    .HasColumnType("date")
                    .HasColumnName("usr_last_access");

                entity.Property(e => e.UsrLastAccessF)
                    .HasColumnType("date")
                    .HasColumnName("usr_last_access_f");

                entity.Property(e => e.UsrName)
                    .HasMaxLength(1000)
                    .HasColumnName("usr_name")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.UsrNip)
                    .HasMaxLength(50)
                    .HasColumnName("usr_nip")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.UsrNotlp)
                    .HasMaxLength(100)
                    .HasColumnName("usr_notlp")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.UsrPartnerAlias)
                    .HasMaxLength(5)
                    .HasColumnName("usr_partner_alias");

                entity.Property(e => e.UsrPosition)
                    .HasMaxLength(100)
                    .HasColumnName("usr_position")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.UsrPrivilege).HasColumnName("usr_privilege");

                entity.Property(e => e.UsrStatus).HasColumnName("usr_status");

                entity.Property(e => e.UsrSupervisor)
                    .HasMaxLength(100)
                    .HasColumnName("usr_supervisor")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.UsrTntAlias)
                    .HasMaxLength(5)
                    .HasColumnName("usr_tnt_alias");

                entity.Property(e => e.UsrUserid)
                    .HasMaxLength(20)
                    .HasColumnName("usr_userid")
                    .HasDefaultValueSql("NULL::character varying");
            });

            modelBuilder.Entity<UsersLogin>(entity =>
            {
                entity.HasKey(e => e.UslId)
                    .HasName("users_login_pkey");

                entity.ToTable("users_login");

                entity.Property(e => e.UslId).HasColumnName("usl_id");

                entity.Property(e => e.UslChangedby)
                    .HasMaxLength(25)
                    .HasColumnName("usl_changedby")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.UslChangeddate).HasColumnName("usl_changeddate");

                entity.Property(e => e.UslCreatedby)
                    .HasMaxLength(25)
                    .HasColumnName("usl_createdby")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.UslCreateddate).HasColumnName("usl_createddate");

                entity.Property(e => e.UslPassHistory1)
                    .HasMaxLength(1000)
                    .HasColumnName("usl_pass_history1");

                entity.Property(e => e.UslPassHistory2)
                    .HasMaxLength(1000)
                    .HasColumnName("usl_pass_history2");

                entity.Property(e => e.UslPassHistory3)
                    .HasMaxLength(1000)
                    .HasColumnName("usl_pass_history3");

                entity.Property(e => e.UslPassHistory4)
                    .HasMaxLength(1000)
                    .HasColumnName("usl_pass_history4");

                entity.Property(e => e.UslPassHistory5)
                    .HasMaxLength(1000)
                    .HasColumnName("usl_pass_history5");

                entity.Property(e => e.UslPassword)
                    .HasMaxLength(1000)
                    .HasColumnName("usl_password")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.UslUsername)
                    .HasMaxLength(25)
                    .HasColumnName("usl_username")
                    .HasDefaultValueSql("NULL::character varying");
            });

            modelBuilder.Entity<UsersNotif>(entity =>
            {
                entity.ToTable("users_notif");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.UsrId)
                    .HasMaxLength(50)
                    .HasColumnName("usr_id");

                entity.Property(e => e.UsrTyp).HasColumnName("usr_typ");
            });

            modelBuilder.Entity<UsersPosition>(entity =>
            {
                entity.HasKey(e => e.UspId)
                    .HasName("users_position_pkey");

                entity.ToTable("users_position");

                entity.Property(e => e.UspId).HasColumnName("usp_id");

                entity.Property(e => e.UspCode)
                    .HasMaxLength(50)
                    .HasColumnName("usp_code")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.UspDescription)
                    .HasMaxLength(1000)
                    .HasColumnName("usp_description")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.UspStatus).HasColumnName("usp_status");

                entity.Property(e => e.UspValue)
                    .HasMaxLength(20)
                    .HasColumnName("usp_value")
                    .HasDefaultValueSql("NULL::character varying");
            });

            modelBuilder.HasSequence("level_master_id_seq");

            modelBuilder.HasSequence("level_user_lu_id_seq");

            modelBuilder.HasSequence("log_category_lgc_id_seq");

            modelBuilder.HasSequence("role_detail_rld_id_seq");

            modelBuilder.HasSequence("role_rl_id_seq");

            modelBuilder.HasSequence("user_reference_usr_id_seq");

            modelBuilder.HasSequence("users_position_usp_id_seq");

            modelBuilder.HasSequence("users_usr_id_seq");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
