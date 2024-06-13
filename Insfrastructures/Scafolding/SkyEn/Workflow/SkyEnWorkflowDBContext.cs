using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyEn.Workflow
{
    public partial class SkyEnWorkflowDBContext : DbContext
    {
        public SkyEnWorkflowDBContext()
        {
        }

        public SkyEnWorkflowDBContext(DbContextOptions<SkyEnWorkflowDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppValueRac> AppValueRac { get; set; }
        public virtual DbSet<CancelRequestLog> CancelRequestLog { get; set; }
        public virtual DbSet<DecflowLogHistory> DecflowLogHistory { get; set; }
        public virtual DbSet<DecflowObject> DecflowObject { get; set; }
        public virtual DbSet<DecisionFlowResult> DecisionFlowResult { get; set; }
        public virtual DbSet<DecisionLog> DecisionLog { get; set; }
        public virtual DbSet<DeclowParameterResult> DeclowParameterResult { get; set; }
        public virtual DbSet<Flows> Flows { get; set; }
        public virtual DbSet<FlowsEdges> FlowsEdges { get; set; }
        public virtual DbSet<FlowsEdges20231016> FlowsEdges20231016 { get; set; }
        public virtual DbSet<FlowsEdges20231020> FlowsEdges20231020 { get; set; }
        public virtual DbSet<FlowsEdgesVersionLog> FlowsEdgesVersionLog { get; set; }
        public virtual DbSet<FlowsEdgesVersionLog20231016> FlowsEdgesVersionLog20231016 { get; set; }
        public virtual DbSet<FlowsNodes> FlowsNodes { get; set; }
        public virtual DbSet<FlowsNodes20231020> FlowsNodes20231020 { get; set; }
        public virtual DbSet<FlowsNodesVersionLog> FlowsNodesVersionLog { get; set; }
        public virtual DbSet<FlowsProcess> FlowsProcess { get; set; }
        public virtual DbSet<FlowsVersionLog> FlowsVersionLog { get; set; }
        public virtual DbSet<HistoricalTtable> HistoricalTtable { get; set; }
        public virtual DbSet<IrreversibleObject> IrreversibleObject { get; set; }
        public virtual DbSet<MasterForm> MasterForm { get; set; }
        public virtual DbSet<MasterObject> MasterObject { get; set; }
        public virtual DbSet<MasterRacResult> MasterRacResult { get; set; }
        public virtual DbSet<ObjectIntegration> ObjectIntegration { get; set; }
        public virtual DbSet<ObjectIntegrationParams> ObjectIntegrationParams { get; set; }
        public virtual DbSet<Privilege> Privilege { get; set; }
        public virtual DbSet<PrivilegeBranch> PrivilegeBranch { get; set; }
        public virtual DbSet<PrivilegeForm> PrivilegeForm { get; set; }
        public virtual DbSet<ProcessLog> ProcessLog { get; set; }
        public virtual DbSet<ProcessLogPic> ProcessLogPic { get; set; }
        public virtual DbSet<QuestionValues> QuestionValues { get; set; }
        public virtual DbSet<ResponseResult> ResponseResult { get; set; }
        public virtual DbSet<ReverseAppApproval> ReverseAppApproval { get; set; }
        public virtual DbSet<ReversibleObject> ReversibleObject { get; set; }
        public virtual DbSet<RptProcessLog> RptProcessLog { get; set; }
        public virtual DbSet<RptProcessLogPic> RptProcessLogPic { get; set; }
        public virtual DbSet<WfCancelRequest> WfCancelRequest { get; set; }
        public virtual DbSet<WorkflowDetail> WorkflowDetail { get; set; }
        public virtual DbSet<WorkflowDetailConnection> WorkflowDetailConnection { get; set; }
        public virtual DbSet<WorkflowDetailCustom> WorkflowDetailCustom { get; set; }
        public virtual DbSet<WorkflowDetailOutput> WorkflowDetailOutput { get; set; }
        public virtual DbSet<WorkflowDetailParaminput> WorkflowDetailParaminput { get; set; }
        public virtual DbSet<WorkflowHeader> WorkflowHeader { get; set; }
        public virtual DbSet<WorkflowLogHistory> WorkflowLogHistory { get; set; }
        public virtual DbSet<WorkflowPending> WorkflowPending { get; set; }
        public virtual DbSet<WorkflowProcess> WorkflowProcess { get; set; }
        public virtual DbSet<WorkflowProcessDtl> WorkflowProcessDtl { get; set; }
        public virtual DbSet<WorkflowRacResult> WorkflowRacResult { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=103.53.197.67;Database=sky.en;User Id=postgres;Password=User123!");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("tablefunc")
                .HasAnnotation("Relational:Collation", "English_United States.1252");

            modelBuilder.Entity<AppValueRac>(entity =>
            {
                entity.HasKey(e => e.AvrId)
                    .HasName("app_value_rac_pkey");

                entity.ToTable("app_value_rac", "workflow");

                entity.Property(e => e.AvrId).HasColumnName("avr_id");

                entity.Property(e => e.AvrKriteria)
                    .HasMaxLength(100)
                    .HasColumnName("avr_kriteria");

                entity.Property(e => e.AvrResult)
                    .HasMaxLength(50)
                    .HasColumnName("avr_result");

                entity.Property(e => e.AvrValueEntry)
                    .HasMaxLength(100)
                    .HasColumnName("avr_value_entry");

                entity.Property(e => e.AvrValueRecommendation)
                    .HasMaxLength(500)
                    .HasColumnName("avr_value_recommendation");

                entity.Property(e => e.RshId).HasColumnName("rsh_id");

                entity.Property(e => e.StartDate).HasColumnName("start_date");

                entity.Property(e => e.TmpTable)
                    .HasColumnType("character varying")
                    .HasColumnName("tmp_table");

                entity.Property(e => e.WorkflowCode)
                    .HasColumnType("character varying")
                    .HasColumnName("workflow_code");
            });

            modelBuilder.Entity<CancelRequestLog>(entity =>
            {
                entity.HasKey(e => e.CrlId)
                    .HasName("cancel_request_log_pkey");

                entity.ToTable("cancel_request_log", "workflow");

                entity.Property(e => e.CrlId).HasColumnName("crl_id");

                entity.Property(e => e.CrlDecisionCancel)
                    .HasMaxLength(50)
                    .HasColumnName("crl_decision_cancel")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.CrlLastDecision)
                    .HasMaxLength(50)
                    .HasColumnName("crl_last_decision")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.CrlLastNode)
                    .HasMaxLength(50)
                    .HasColumnName("crl_last_node")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.CrlNotes).HasColumnName("crl_notes");

                entity.Property(e => e.CrlRshid)
                    .HasMaxLength(50)
                    .HasColumnName("crl_rshid")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.CrlTimestamp)
                    .HasPrecision(6)
                    .HasColumnName("crl_timestamp");

                entity.Property(e => e.CrlUserid)
                    .HasMaxLength(150)
                    .HasColumnName("crl_userid")
                    .HasDefaultValueSql("NULL::character varying");
            });

            modelBuilder.Entity<DecflowLogHistory>(entity =>
            {
                entity.HasKey(e => e.DlhId)
                    .HasName("decflow_log_history_pkey");

                entity.ToTable("decflow_log_history", "workflow");

                entity.Property(e => e.DlhId).HasColumnName("dlh_id");

                entity.Property(e => e.AcctNewLoanL6mUnd).HasColumnName("acct_new_loan_l6m_und");

                entity.Property(e => e.AddrDukcapil)
                    .HasMaxLength(5000)
                    .HasColumnName("addr_dukcapil");

                entity.Property(e => e.Asa)
                    .HasPrecision(12, 12)
                    .HasColumnName("asa");

                entity.Property(e => e.AskDrawdown)
                    .HasPrecision(20, 2)
                    .HasColumnName("ask_drawdown");

                entity.Property(e => e.AskLoan)
                    .HasPrecision(18, 2)
                    .HasColumnName("ask_loan");

                entity.Property(e => e.AskTenor).HasColumnName("ask_tenor");

                entity.Property(e => e.AssignedLim1)
                    .HasPrecision(32)
                    .HasColumnName("assigned_lim1");

                entity.Property(e => e.BkAmtNewLoanL6mUnd)
                    .HasPrecision(19)
                    .HasColumnName("bk_amt_new_loan_l6m_und");

                entity.Property(e => e.BkBlacklistCode).HasColumnName("bk_blacklist_code");

                entity.Property(e => e.BkCategory)
                    .HasMaxLength(50)
                    .HasColumnName("bk_category");

                entity.Property(e => e.BkCategoryFlag)
                    .HasMaxLength(1)
                    .HasColumnName("bk_category_flag");

                entity.Property(e => e.BkCollectibility).HasColumnName("bk_collectibility");

                entity.Property(e => e.BkDbr)
                    .HasPrecision(10, 4)
                    .HasColumnName("bk_dbr");

                entity.Property(e => e.BkFlag).HasColumnName("bk_flag");

                entity.Property(e => e.BkFreqWoCc12).HasColumnName("bk_freq_wo_cc_12");

                entity.Property(e => e.BkFreqWoNoncc12).HasColumnName("bk_freq_wo_noncc_12");

                entity.Property(e => e.BkFreqXdpd6).HasColumnName("bk_freq_xdpd_6");

                entity.Property(e => e.BkLastXdpd).HasColumnName("bk_last_xdpd");

                entity.Property(e => e.BkLimitSecureLoan)
                    .HasPrecision(19, 2)
                    .HasColumnName("bk_limit_secure_loan");

                entity.Property(e => e.BkMaxDpd).HasColumnName("bk_max_dpd");

                entity.Property(e => e.BkMaxLimitCc)
                    .HasPrecision(32)
                    .HasColumnName("bk_max_limit_cc");

                entity.Property(e => e.BkMaxLimitMobCc).HasColumnName("bk_max_limit_mob_cc");

                entity.Property(e => e.BkMaxMob).HasColumnName("bk_max_mob");

                entity.Property(e => e.BkMaxMobCc).HasColumnName("bk_max_mob_cc");

                entity.Property(e => e.BkMonthlyCflIncome)
                    .HasPrecision(19, 2)
                    .HasColumnName("bk_monthly_cfl_income");

                entity.Property(e => e.BkMonthlyObligation)
                    .HasPrecision(32)
                    .HasColumnName("bk_monthly_obligation");

                entity.Property(e => e.BkOsDpd)
                    .HasPrecision(32, 2)
                    .HasColumnName("bk_os_dpd");

                entity.Property(e => e.BkOsUnsecuredNoncc)
                    .HasPrecision(32)
                    .HasColumnName("bk_os_unsecured_noncc");

                entity.Property(e => e.BkOsWoCc)
                    .HasPrecision(32)
                    .HasColumnName("bk_os_wo_cc");

                entity.Property(e => e.BkPefindoScore).HasColumnName("bk_pefindo_score");

                entity.Property(e => e.BkRestructureFlag).HasColumnName("bk_restructure_flag");

                entity.Property(e => e.BkTotAmt)
                    .HasPrecision(32)
                    .HasColumnName("bk_tot_amt");

                entity.Property(e => e.BkTotalLimitCc)
                    .HasPrecision(19, 2)
                    .HasColumnName("bk_total_limit_cc");

                entity.Property(e => e.BkUnsecuredExp)
                    .HasPrecision(32)
                    .HasColumnName("bk_unsecured_exp");

                entity.Property(e => e.BkUtilCc)
                    .HasPrecision(8, 2)
                    .HasColumnName("bk_util_cc");

                entity.Property(e => e.BkWorstDelq12).HasColumnName("bk_worst_delq_12");

                entity.Property(e => e.BkWorstDelq3).HasColumnName("bk_worst_delq_3");

                entity.Property(e => e.BkWorstDelq6).HasColumnName("bk_worst_delq_6");

                entity.Property(e => e.BureauGroup)
                    .HasMaxLength(50)
                    .HasColumnName("bureau_group");

                entity.Property(e => e.CRestrictedCom)
                    .HasMaxLength(20)
                    .HasColumnName("c_restricted_com");

                entity.Property(e => e.CRestrictedIndustry).HasColumnName("c_restricted_industry");

                entity.Property(e => e.CampaignId)
                    .HasMaxLength(500)
                    .HasColumnName("campaign_id");

                entity.Property(e => e.Cat)
                    .HasMaxLength(500)
                    .HasColumnName("cat");

                entity.Property(e => e.Category)
                    .HasMaxLength(500)
                    .HasColumnName("category");

                entity.Property(e => e.CcComVar1)
                    .HasMaxLength(20)
                    .HasColumnName("cc_com_var1");

                entity.Property(e => e.CcComVar2)
                    .HasMaxLength(20)
                    .HasColumnName("cc_com_var2");

                entity.Property(e => e.CcComVar3)
                    .HasMaxLength(20)
                    .HasColumnName("cc_com_var3");

                entity.Property(e => e.CcComVar4)
                    .HasMaxLength(20)
                    .HasColumnName("cc_com_var4");

                entity.Property(e => e.CcComVar5)
                    .HasMaxLength(20)
                    .HasColumnName("cc_com_var5");

                entity.Property(e => e.CcComVar6)
                    .HasMaxLength(10)
                    .HasColumnName("cc_com_var6");

                entity.Property(e => e.CcComVar7)
                    .HasMaxLength(20)
                    .HasColumnName("cc_com_var7");

                entity.Property(e => e.ChannelId)
                    .HasMaxLength(10)
                    .HasColumnName("channel_id");

                entity.Property(e => e.CheckStatus)
                    .HasMaxLength(20)
                    .HasColumnName("check_status");

                entity.Property(e => e.Citizenship)
                    .HasMaxLength(10)
                    .HasColumnName("citizenship");

                entity.Property(e => e.CoYears)
                    .HasPrecision(8, 2)
                    .HasColumnName("co_years");

                entity.Property(e => e.ComVar1)
                    .HasMaxLength(1)
                    .HasColumnName("com_var1");

                entity.Property(e => e.ComVar2)
                    .HasMaxLength(1)
                    .HasColumnName("com_var2");

                entity.Property(e => e.ComVar3)
                    .HasMaxLength(1)
                    .HasColumnName("com_var3");

                entity.Property(e => e.ComVar4)
                    .HasMaxLength(4)
                    .HasColumnName("com_var4");

                entity.Property(e => e.ComVar5)
                    .HasMaxLength(1)
                    .HasColumnName("com_var5");

                entity.Property(e => e.CstAddress)
                    .HasMaxLength(5000)
                    .HasColumnName("cst_address");

                entity.Property(e => e.CstAddressCity)
                    .HasMaxLength(50)
                    .HasColumnName("cst_address_city");

                entity.Property(e => e.CstAge).HasColumnName("cst_age");

                entity.Property(e => e.CstCif)
                    .HasMaxLength(100)
                    .HasColumnName("cst_cif");

                entity.Property(e => e.CstCompanyName)
                    .HasMaxLength(100)
                    .HasColumnName("cst_company_name");

                entity.Property(e => e.CstDetailedLoanPurpose)
                    .HasMaxLength(100)
                    .HasColumnName("cst_detailed_loan_purpose");

                entity.Property(e => e.CstDob)
                    .HasColumnType("date")
                    .HasColumnName("cst_dob");

                entity.Property(e => e.CstDocLegalBusnis)
                    .HasMaxLength(500)
                    .HasColumnName("cst_doc_legal_busnis");

                entity.Property(e => e.CstDocNpwp)
                    .HasMaxLength(500)
                    .HasColumnName("cst_doc_npwp");

                entity.Property(e => e.CstDocSupport)
                    .HasMaxLength(500)
                    .HasColumnName("cst_doc_support");

                entity.Property(e => e.CstEmail)
                    .HasMaxLength(100)
                    .HasColumnName("cst_email");

                entity.Property(e => e.CstEmergencyPhoneMobile)
                    .HasMaxLength(50)
                    .HasColumnName("cst_emergency_phone_mobile");

                entity.Property(e => e.CstEmergencyRelationship)
                    .HasMaxLength(50)
                    .HasColumnName("cst_emergency_relationship");

                entity.Property(e => e.CstEmploymentNumber)
                    .HasMaxLength(50)
                    .HasColumnName("cst_employment_number");

                entity.Property(e => e.CstFname)
                    .HasMaxLength(50)
                    .HasColumnName("cst_fname");

                entity.Property(e => e.CstImgDocEktp)
                    .HasMaxLength(500)
                    .HasColumnName("cst_img_doc_ektp");

                entity.Property(e => e.CstImgDocIncome)
                    .HasMaxLength(500)
                    .HasColumnName("cst_img_doc_income");

                entity.Property(e => e.CstImgDocKk)
                    .HasMaxLength(500)
                    .HasColumnName("cst_img_doc_kk");

                entity.Property(e => e.CstKtp)
                    .HasMaxLength(16)
                    .HasColumnName("cst_ktp");

                entity.Property(e => e.CstMaritalStatus)
                    .HasMaxLength(50)
                    .HasColumnName("cst_marital_status");

                entity.Property(e => e.CstMotherMaidenName)
                    .HasMaxLength(100)
                    .HasColumnName("cst_mother_maiden_name");

                entity.Property(e => e.CstNpwp)
                    .HasMaxLength(100)
                    .HasColumnName("cst_npwp");

                entity.Property(e => e.CstNumberOfChild).HasColumnName("cst_number_of_child");

                entity.Property(e => e.CstOtherBankCc)
                    .HasMaxLength(10)
                    .HasColumnName("cst_other_bank_cc");

                entity.Property(e => e.CstPhoneHome)
                    .HasMaxLength(50)
                    .HasColumnName("cst_phone_home");

                entity.Property(e => e.CstPhoneMobile)
                    .HasPrecision(30)
                    .HasColumnName("cst_phone_mobile");

                entity.Property(e => e.CstSex)
                    .HasMaxLength(20)
                    .HasColumnName("cst_sex");

                entity.Property(e => e.CstTypeOfCc)
                    .HasMaxLength(100)
                    .HasColumnName("cst_type_of_cc");

                entity.Property(e => e.CstWorkAddress)
                    .HasMaxLength(5000)
                    .HasColumnName("cst_work_address");

                entity.Property(e => e.CstWorkIncome)
                    .HasPrecision(18, 2)
                    .HasColumnName("cst_work_income");

                entity.Property(e => e.CstWorkPhone)
                    .HasMaxLength(50)
                    .HasColumnName("cst_work_phone");

                entity.Property(e => e.CstZipcode)
                    .HasMaxLength(50)
                    .HasColumnName("cst_zipcode");

                entity.Property(e => e.DbrCutOff)
                    .HasMaxLength(10)
                    .HasColumnName("dbr_cut_off");

                entity.Property(e => e.DbrTotal)
                    .HasPrecision(8, 4)
                    .HasColumnName("dbr_total");

                entity.Property(e => e.DecflowCode)
                    .HasColumnType("character varying")
                    .HasColumnName("decflow_code");

                entity.Property(e => e.Decision)
                    .HasMaxLength(500)
                    .HasColumnName("decision");

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("dob");

                entity.Property(e => e.DobDukcapil)
                    .HasColumnType("date")
                    .HasColumnName("dob_dukcapil");

                entity.Property(e => e.DtiWithout)
                    .HasPrecision(4, 4)
                    .HasColumnName("dti_without");

                entity.Property(e => e.EgibilityCom).HasColumnName("egibility_com");

                entity.Property(e => e.EmergencyContact)
                    .HasMaxLength(50)
                    .HasColumnName("emergency_contact");

                entity.Property(e => e.EmploymentType)
                    .HasMaxLength(500)
                    .HasColumnName("employment_type");

                entity.Property(e => e.FinalAssignedLimit)
                    .HasPrecision(32)
                    .HasColumnName("final_assigned_limit");

                entity.Property(e => e.FinalIncome)
                    .HasPrecision(32)
                    .HasColumnName("final_income");

                entity.Property(e => e.FlagCard)
                    .HasMaxLength(100)
                    .HasColumnName("flag_card");

                entity.Property(e => e.FlagDecision)
                    .HasMaxLength(50)
                    .HasColumnName("flag_decision");

                entity.Property(e => e.FlagPassGeneral)
                    .HasMaxLength(5)
                    .HasColumnName("flag_pass_general");

                entity.Property(e => e.FlagPassVerification)
                    .HasMaxLength(5)
                    .HasColumnName("flag_pass_verification");

                entity.Property(e => e.GenderDukcapil)
                    .HasMaxLength(100)
                    .HasColumnName("gender_dukcapil");

                entity.Property(e => e.Group1)
                    .HasMaxLength(4)
                    .HasColumnName("group1");

                entity.Property(e => e.Group2)
                    .HasMaxLength(4)
                    .HasColumnName("group2");

                entity.Property(e => e.HomeAddressCity)
                    .HasMaxLength(200)
                    .HasColumnName("home_address_city");

                entity.Property(e => e.HomeMonthlyRentalFee)
                    .HasPrecision(19)
                    .HasColumnName("home_monthly_rental_fee");

                entity.Property(e => e.HomeOwnershipMonth).HasColumnName("home_ownership_month");

                entity.Property(e => e.HomeOwnershipStatus)
                    .HasMaxLength(10)
                    .HasColumnName("home_ownership_status");

                entity.Property(e => e.IndustryCode)
                    .HasMaxLength(5)
                    .HasColumnName("industry_code");

                entity.Property(e => e.InstallmentAmount)
                    .HasPrecision(18, 2)
                    .HasColumnName("installment_amount");

                entity.Property(e => e.InterestCalculation)
                    .HasMaxLength(15)
                    .HasColumnName("interest_calculation");

                entity.Property(e => e.InterestRate)
                    .HasPrecision(8, 4)
                    .HasColumnName("interest_rate");

                entity.Property(e => e.InterestRule)
                    .HasMaxLength(10)
                    .HasColumnName("interest_rule");

                entity.Property(e => e.InterestType)
                    .HasMaxLength(100)
                    .HasColumnName("interest_type");

                entity.Property(e => e.KabIdDukcapil)
                    .HasMaxLength(10)
                    .HasColumnName("kab_id_dukcapil");

                entity.Property(e => e.KabNameDukcapil)
                    .HasMaxLength(500)
                    .HasColumnName("kab_name_dukcapil");

                entity.Property(e => e.KecIdDukcapil)
                    .HasMaxLength(10)
                    .HasColumnName("kec_id_dukcapil");

                entity.Property(e => e.KecNameDukcapil)
                    .HasMaxLength(100)
                    .HasColumnName("kec_name_dukcapil");

                entity.Property(e => e.KelIdDukcapil)
                    .HasMaxLength(10)
                    .HasColumnName("kel_id_dukcapil");

                entity.Property(e => e.KelNameDukcapil)
                    .HasMaxLength(100)
                    .HasColumnName("kel_name_dukcapil");

                entity.Property(e => e.Keputusan)
                    .HasMaxLength(100)
                    .HasColumnName("keputusan");

                entity.Property(e => e.LastEducation)
                    .HasMaxLength(10)
                    .HasColumnName("last_education");

                entity.Property(e => e.LengthOfEmployment).HasColumnName("length_of_employment");

                entity.Property(e => e.ListHomeOwnershipCf)
                    .HasMaxLength(500)
                    .HasColumnName("list_home_ownership_cf");

                entity.Property(e => e.ListKewarganegaraan)
                    .HasMaxLength(50)
                    .HasColumnName("list_kewarganegaraan");

                entity.Property(e => e.LiveYear)
                    .HasPrecision(8, 2)
                    .HasColumnName("live_year");

                entity.Property(e => e.LoanAmount)
                    .HasPrecision(30)
                    .HasColumnName("loan_amount");

                entity.Property(e => e.LoanDisbursed)
                    .HasPrecision(30)
                    .HasColumnName("loan_disbursed");

                entity.Property(e => e.LoanInterest)
                    .HasPrecision(18, 4)
                    .HasColumnName("loan_interest");

                entity.Property(e => e.LoanTenor)
                    .HasPrecision(18, 2)
                    .HasColumnName("loan_tenor");

                entity.Property(e => e.MaritalStatus)
                    .HasMaxLength(30)
                    .HasColumnName("marital_status");

                entity.Property(e => e.MaritalStsDukcapil)
                    .HasMaxLength(100)
                    .HasColumnName("marital_sts_dukcapil");

                entity.Property(e => e.MaxAssignedLoanAmtMue)
                    .HasPrecision(32)
                    .HasColumnName("max_assigned_loan_amt_mue");

                entity.Property(e => e.MaxDbr)
                    .HasPrecision(8, 2)
                    .HasColumnName("max_dbr");

                entity.Property(e => e.MaxInstallment)
                    .HasPrecision(32)
                    .HasColumnName("max_installment");

                entity.Property(e => e.MaxInterest)
                    .HasPrecision(8, 4)
                    .HasColumnName("max_interest");

                entity.Property(e => e.MaxLoan)
                    .HasPrecision(19)
                    .HasColumnName("max_loan");

                entity.Property(e => e.MaxMueInt1).HasColumnName("max_mue_int1");

                entity.Property(e => e.MaxMueInt2).HasColumnName("max_mue_int2");

                entity.Property(e => e.MaxTenor).HasColumnName("max_tenor");

                entity.Property(e => e.MinLoan)
                    .HasPrecision(19)
                    .HasColumnName("min_loan");

                entity.Property(e => e.MinLoanCutOff)
                    .HasMaxLength(5)
                    .HasColumnName("min_loan_cut_off");

                entity.Property(e => e.MotherNameDukcapil)
                    .HasMaxLength(100)
                    .HasColumnName("mother_name_dukcapil");

                entity.Property(e => e.MthSncLastAcOpenUnd).HasColumnName("mth_snc_last_ac_open_und");

                entity.Property(e => e.MueExternal)
                    .HasPrecision(5, 2)
                    .HasColumnName("mue_external");

                entity.Property(e => e.NameCust)
                    .HasMaxLength(200)
                    .HasColumnName("name_cust");

                entity.Property(e => e.NameDukcapil)
                    .HasMaxLength(50)
                    .HasColumnName("name_dukcapil");

                entity.Property(e => e.NameJaroVal)
                    .HasPrecision(6, 4)
                    .HasColumnName("name_jaro_val");

                entity.Property(e => e.Nik)
                    .HasMaxLength(16)
                    .HasColumnName("nik");

                entity.Property(e => e.NikDukcapil)
                    .HasMaxLength(100)
                    .HasColumnName("nik_dukcapil");

                entity.Property(e => e.NotesAnalyst)
                    .HasMaxLength(5000)
                    .HasColumnName("notes_analyst");

                entity.Property(e => e.NumUnsecuredLoan).HasColumnName("num_unsecured_loan");

                entity.Property(e => e.PlCategory)
                    .HasMaxLength(50)
                    .HasColumnName("pl_category");

                entity.Property(e => e.PobDukcapil)
                    .HasMaxLength(100)
                    .HasColumnName("pob_dukcapil");

                entity.Property(e => e.ProductCode)
                    .HasMaxLength(10)
                    .HasColumnName("product_code");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(100)
                    .HasColumnName("product_name");

                entity.Property(e => e.ProductVersion)
                    .HasMaxLength(50)
                    .HasColumnName("product_version");

                entity.Property(e => e.R001)
                    .HasMaxLength(4)
                    .HasColumnName("r001");

                entity.Property(e => e.R004)
                    .HasMaxLength(4)
                    .HasColumnName("r004");

                entity.Property(e => e.R006)
                    .HasMaxLength(4)
                    .HasColumnName("r006");

                entity.Property(e => e.R007)
                    .HasMaxLength(4)
                    .HasColumnName("r007");

                entity.Property(e => e.R008)
                    .HasMaxLength(4)
                    .HasColumnName("r008");

                entity.Property(e => e.R009)
                    .HasMaxLength(4)
                    .HasColumnName("r009");

                entity.Property(e => e.R010)
                    .HasMaxLength(100)
                    .HasColumnName("r010");

                entity.Property(e => e.R011)
                    .HasMaxLength(10)
                    .HasColumnName("r011");

                entity.Property(e => e.R012)
                    .HasMaxLength(10)
                    .HasColumnName("r012");

                entity.Property(e => e.R013)
                    .HasMaxLength(10)
                    .HasColumnName("r013");

                entity.Property(e => e.R014)
                    .HasMaxLength(10)
                    .HasColumnName("r014");

                entity.Property(e => e.R015)
                    .HasMaxLength(10)
                    .HasColumnName("r015");

                entity.Property(e => e.R016)
                    .HasMaxLength(4)
                    .HasColumnName("r016");

                entity.Property(e => e.R017)
                    .HasMaxLength(10)
                    .HasColumnName("r017");

                entity.Property(e => e.R048)
                    .HasMaxLength(10)
                    .HasColumnName("r048");

                entity.Property(e => e.R049)
                    .HasMaxLength(10)
                    .HasColumnName("r049");

                entity.Property(e => e.R060)
                    .HasMaxLength(4)
                    .HasColumnName("r060");

                entity.Property(e => e.R065)
                    .HasMaxLength(10)
                    .HasColumnName("r065");

                entity.Property(e => e.R072)
                    .HasMaxLength(4)
                    .HasColumnName("r072");

                entity.Property(e => e.R088)
                    .HasMaxLength(4)
                    .HasColumnName("r088");

                entity.Property(e => e.R090)
                    .HasMaxLength(4)
                    .HasColumnName("r090");

                entity.Property(e => e.R098)
                    .HasMaxLength(4)
                    .HasColumnName("r098");

                entity.Property(e => e.R770)
                    .HasMaxLength(20)
                    .HasColumnName("r770");

                entity.Property(e => e.RatingScore)
                    .HasPrecision(2, 2)
                    .HasColumnName("rating_score");

                entity.Property(e => e.RcDedupHr)
                    .HasMaxLength(20)
                    .HasColumnName("rc_dedup_hr");

                entity.Property(e => e.RcEmploymentLength)
                    .HasMaxLength(10)
                    .HasColumnName("rc_employment_length");

                entity.Property(e => e.RcMaxAge)
                    .HasMaxLength(10)
                    .HasColumnName("rc_max_age");

                entity.Property(e => e.RcMinAge)
                    .HasMaxLength(10)
                    .HasColumnName("rc_min_age");

                entity.Property(e => e.RcMinincome)
                    .HasMaxLength(10)
                    .HasColumnName("rc_minincome");

                entity.Property(e => e.RcWni)
                    .HasMaxLength(10)
                    .HasColumnName("rc_wni");

                entity.Property(e => e.ReScore).HasColumnName("re_score");

                entity.Property(e => e.RefId)
                    .HasMaxLength(20)
                    .HasColumnName("ref_id");

                entity.Property(e => e.ReferralCode)
                    .HasMaxLength(50)
                    .HasColumnName("referral_code");

                entity.Property(e => e.RejectCode)
                    .HasMaxLength(100)
                    .HasColumnName("reject_code");

                entity.Property(e => e.ReqAmount)
                    .HasPrecision(18, 2)
                    .HasColumnName("req_amount");

                entity.Property(e => e.ReqTenor)
                    .HasPrecision(18, 2)
                    .HasColumnName("req_tenor");

                entity.Property(e => e.RlosCodeSector)
                    .HasMaxLength(10)
                    .HasColumnName("rlos_code_sector");

                entity.Property(e => e.RshId).HasColumnName("rsh_id");

                entity.Property(e => e.SavingAccount)
                    .HasMaxLength(50)
                    .HasColumnName("saving_account");

                entity.Property(e => e.SavingAccountName)
                    .HasMaxLength(100)
                    .HasColumnName("saving_account_name");

                entity.Property(e => e.Score).HasColumnName("score");

                entity.Property(e => e.ScoreBand)
                    .HasMaxLength(1)
                    .HasColumnName("score_band");

                entity.Property(e => e.ScoreCrr)
                    .HasMaxLength(20)
                    .HasColumnName("score_crr");

                entity.Property(e => e.ScorecardCutoff)
                    .HasMaxLength(10)
                    .HasColumnName("scorecard_cutoff");

                entity.Property(e => e.Segment)
                    .HasMaxLength(500)
                    .HasColumnName("segment");

                entity.Property(e => e.ServiceAreaHome).HasColumnName("service_area_home");

                entity.Property(e => e.ServiceAreaWork).HasColumnName("service_area_work");

                entity.Property(e => e.StartDate)
                    .HasPrecision(6)
                    .HasColumnName("start_date");

                entity.Property(e => e.StateIdDukcapil)
                    .HasMaxLength(10)
                    .HasColumnName("state_id_dukcapil");

                entity.Property(e => e.StateNameDukcapil)
                    .HasMaxLength(100)
                    .HasColumnName("state_name_dukcapil");

                entity.Property(e => e.StatusDukcapil)
                    .HasMaxLength(20)
                    .HasColumnName("status_dukcapil");

                entity.Property(e => e.Testing1)
                    .HasPrecision(100, 10)
                    .HasColumnName("testing1");

                entity.Property(e => e.TmpTable)
                    .HasColumnType("character varying")
                    .HasColumnName("tmp_table");

                entity.Property(e => e.TotalMue).HasColumnName("total_mue");

                entity.Property(e => e.ValBureau)
                    .HasMaxLength(10)
                    .HasColumnName("val_bureau");

                entity.Property(e => e.ValCheckStatus)
                    .HasMaxLength(20)
                    .HasColumnName("val_check_status");

                entity.Property(e => e.ValDukcapil)
                    .HasMaxLength(10)
                    .HasColumnName("val_dukcapil");

                entity.Property(e => e.ValGrac)
                    .HasMaxLength(10)
                    .HasColumnName("val_grac");

                entity.Property(e => e.WorkAddressCity)
                    .HasMaxLength(20)
                    .HasColumnName("work_address_city");

                entity.Property(e => e.WorkBusinessPosition)
                    .HasMaxLength(20)
                    .HasColumnName("work_business_position");

                entity.Property(e => e.WorkCompanyType)
                    .HasMaxLength(20)
                    .HasColumnName("work_company_type");

                entity.Property(e => e.WorkEmploymentStatus)
                    .HasMaxLength(30)
                    .HasColumnName("work_employment_status");

                entity.Property(e => e.WorkGrossIncome)
                    .HasPrecision(19)
                    .HasColumnName("work_gross_income");

                entity.Property(e => e.WorkMonth).HasColumnName("work_month");

                entity.Property(e => e.WorkNumberOfEmployee)
                    .HasPrecision(2, 2)
                    .HasColumnName("work_number_of_employee");

                entity.Property(e => e.WorkProfession)
                    .HasMaxLength(50)
                    .HasColumnName("work_profession");

                entity.Property(e => e.WorkflowCode)
                    .HasColumnType("character varying")
                    .HasColumnName("workflow_code");
            });

            modelBuilder.Entity<DecflowObject>(entity =>
            {
                entity.HasKey(e => e.DeoId)
                    .HasName("decflow_object_pkey");

                entity.ToTable("decflow_object", "workflow");

                entity.Property(e => e.DeoId).HasColumnName("deo_id");

                entity.Property(e => e.DeoFieldName)
                    .HasMaxLength(255)
                    .HasColumnName("deo_field_name");

                entity.Property(e => e.DeoObjId)
                    .HasMaxLength(100)
                    .HasColumnName("deo_obj_id");

                entity.Property(e => e.DeoTimestamp)
                    .HasPrecision(6)
                    .HasColumnName("deo_timestamp");

                entity.Property(e => e.DeoType)
                    .HasMaxLength(255)
                    .HasColumnName("deo_type");
            });

            modelBuilder.Entity<DecisionFlowResult>(entity =>
            {
                entity.HasKey(e => e.DfrId)
                    .HasName("decision_flow_result_pkey");

                entity.ToTable("decision_flow_result", "workflow");

                entity.Property(e => e.DfrId).HasColumnName("dfr_id");

                entity.Property(e => e.DfrCounter).HasColumnName("dfr_counter");

                entity.Property(e => e.DfrDate)
                    .HasPrecision(6)
                    .HasColumnName("dfr_date");

                entity.Property(e => e.DfrLabel)
                    .HasMaxLength(255)
                    .HasColumnName("dfr_label");

                entity.Property(e => e.DfrResult).HasColumnName("dfr_result");

                entity.Property(e => e.DfrRshId).HasColumnName("dfr_rsh_id");

                entity.Property(e => e.DfrSource)
                    .HasMaxLength(255)
                    .HasColumnName("dfr_source");

                entity.Property(e => e.DfrWfId).HasColumnName("dfr_wf_id");
            });

            modelBuilder.Entity<DecisionLog>(entity =>
            {
                entity.ToTable("decision_log", "workflow");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FinishTime)
                    .HasPrecision(6)
                    .HasColumnName("finish_time");

                entity.Property(e => e.RshId).HasColumnName("rsh_id");

                entity.Property(e => e.StartTime)
                    .HasPrecision(6)
                    .HasColumnName("start_time");
            });

            modelBuilder.Entity<DeclowParameterResult>(entity =>
            {
                entity.HasKey(e => e.DprId)
                    .HasName("declow_parameter_result_pkey");

                entity.ToTable("declow_parameter_result", "workflow");

                entity.Property(e => e.DprId).HasColumnName("dpr_id");

                entity.Property(e => e.DprDecflowCode)
                    .HasMaxLength(255)
                    .HasColumnName("dpr_decflow_code")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.DprDecflowField)
                    .HasMaxLength(255)
                    .HasColumnName("dpr_decflow_field")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.DprTimestamp)
                    .HasPrecision(6)
                    .HasColumnName("dpr_timestamp");
            });

            modelBuilder.Entity<Flows>(entity =>
            {
                entity.HasKey(e => e.FlhId)
                    .HasName("flows_pkey");

                entity.ToTable("flows", "workflow");

                entity.Property(e => e.FlhId)
                    .ValueGeneratedNever()
                    .HasColumnName("flh_id");

                entity.Property(e => e.FlhApplied)
                    .HasMaxLength(150)
                    .HasColumnName("flh_applied");

                entity.Property(e => e.FlhApproverStatus)
                    .HasMaxLength(255)
                    .HasColumnName("flh_approver_status");

                entity.Property(e => e.FlhCategory)
                    .HasMaxLength(100)
                    .HasColumnName("flh_category");

                entity.Property(e => e.FlhCode)
                    .HasMaxLength(255)
                    .HasColumnName("flh_code");

                entity.Property(e => e.FlhDesc).HasColumnName("flh_desc");

                entity.Property(e => e.FlhName)
                    .HasMaxLength(255)
                    .HasColumnName("flh_name");

                entity.Property(e => e.FlhStatus)
                    .HasMaxLength(255)
                    .HasColumnName("flh_status");

                entity.Property(e => e.FlhType)
                    .HasMaxLength(255)
                    .HasColumnName("flh_type")
                    .HasComment("workflow atau decflow");

                entity.Property(e => e.FlhVersionCode)
                    .HasMaxLength(255)
                    .HasColumnName("flh_version_code");

                entity.Property(e => e.FlnIsdelete)
                    .HasColumnName("fln_isdelete")
                    .HasDefaultValueSql("false");
            });

            modelBuilder.Entity<FlowsEdges>(entity =>
            {
                entity.HasKey(e => e.FleId)
                    .HasName("flows_edges_pkey");

                entity.ToTable("flows_edges", "workflow");

                entity.Property(e => e.FleId)
                    .HasColumnName("fle_id")
                    .HasDefaultValueSql("nextval('workflow_edges_fle_id_seq'::regclass)");

                entity.Property(e => e.FleEdgesId)
                    .HasMaxLength(255)
                    .HasColumnName("fle_edges_id")
                    .HasComment("id");

                entity.Property(e => e.FleEdgesLabel)
                    .HasMaxLength(255)
                    .HasColumnName("fle_edges_label")
                    .HasComment("label");

                entity.Property(e => e.FleEdgesSource)
                    .HasMaxLength(255)
                    .HasColumnName("fle_edges_source")
                    .HasComment("source");

                entity.Property(e => e.FleEdgesSourcehandle)
                    .HasMaxLength(10)
                    .HasColumnName("fle_edges_sourcehandle");

                entity.Property(e => e.FleEdgesTarget)
                    .HasMaxLength(255)
                    .HasColumnName("fle_edges_target")
                    .HasComment("target");

                entity.Property(e => e.FleEdgesTargethandle)
                    .HasMaxLength(10)
                    .HasColumnName("fle_edges_targethandle");

                entity.Property(e => e.FleEdgesType)
                    .HasMaxLength(255)
                    .HasColumnName("fle_edges_type")
                    .HasComment("type");

                entity.Property(e => e.FleFlhId)
                    .HasColumnName("fle_flh_id")
                    .HasComment("relasi to flows.flh_id");
            });

            modelBuilder.Entity<FlowsEdges20231016>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("flows_edges_20231016", "workflow");

                entity.Property(e => e.FleEdgesId)
                    .HasMaxLength(255)
                    .HasColumnName("fle_edges_id");

                entity.Property(e => e.FleEdgesLabel)
                    .HasMaxLength(255)
                    .HasColumnName("fle_edges_label");

                entity.Property(e => e.FleEdgesSource)
                    .HasMaxLength(255)
                    .HasColumnName("fle_edges_source");

                entity.Property(e => e.FleEdgesTarget)
                    .HasMaxLength(255)
                    .HasColumnName("fle_edges_target");

                entity.Property(e => e.FleEdgesType)
                    .HasMaxLength(255)
                    .HasColumnName("fle_edges_type");

                entity.Property(e => e.FleFlhId).HasColumnName("fle_flh_id");

                entity.Property(e => e.FleId).HasColumnName("fle_id");
            });

            modelBuilder.Entity<FlowsEdges20231020>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("flows_edges_20231020", "workflow");

                entity.Property(e => e.FleEdgesId)
                    .HasMaxLength(255)
                    .HasColumnName("fle_edges_id");

                entity.Property(e => e.FleEdgesLabel)
                    .HasMaxLength(255)
                    .HasColumnName("fle_edges_label");

                entity.Property(e => e.FleEdgesSource)
                    .HasMaxLength(255)
                    .HasColumnName("fle_edges_source");

                entity.Property(e => e.FleEdgesSourcehandle)
                    .HasMaxLength(10)
                    .HasColumnName("fle_edges_sourcehandle");

                entity.Property(e => e.FleEdgesTarget)
                    .HasMaxLength(255)
                    .HasColumnName("fle_edges_target");

                entity.Property(e => e.FleEdgesTargethandle)
                    .HasMaxLength(10)
                    .HasColumnName("fle_edges_targethandle");

                entity.Property(e => e.FleEdgesType)
                    .HasMaxLength(255)
                    .HasColumnName("fle_edges_type");

                entity.Property(e => e.FleFlhId).HasColumnName("fle_flh_id");

                entity.Property(e => e.FleId).HasColumnName("fle_id");
            });

            modelBuilder.Entity<FlowsEdgesVersionLog>(entity =>
            {
                entity.HasKey(e => e.FevId)
                    .HasName("flows_edges_version_log_pkey");

                entity.ToTable("flows_edges_version_log", "workflow");

                entity.Property(e => e.FevId).HasColumnName("fev_id");

                entity.Property(e => e.FevTimestamp)
                    .HasPrecision(6)
                    .HasColumnName("fev_timestamp");

                entity.Property(e => e.FevUserid)
                    .HasMaxLength(50)
                    .HasColumnName("fev_userid")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.FevVersionCode)
                    .HasMaxLength(255)
                    .HasColumnName("fev_version_code");

                entity.Property(e => e.FleEdgesId)
                    .HasMaxLength(255)
                    .HasColumnName("fle_edges_id")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.FleEdgesLabel)
                    .HasMaxLength(255)
                    .HasColumnName("fle_edges_label")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.FleEdgesSource)
                    .HasMaxLength(255)
                    .HasColumnName("fle_edges_source")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.FleEdgesSourcehandle)
                    .HasMaxLength(10)
                    .HasColumnName("fle_edges_sourcehandle");

                entity.Property(e => e.FleEdgesTarget).HasColumnName("fle_edges_target");

                entity.Property(e => e.FleEdgesTargethandle)
                    .HasMaxLength(10)
                    .HasColumnName("fle_edges_targethandle");

                entity.Property(e => e.FleEdgesType)
                    .HasMaxLength(255)
                    .HasColumnName("fle_edges_type")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.FleFlhId).HasColumnName("fle_flh_id");

                entity.Property(e => e.FleId).HasColumnName("fle_id");
            });

            modelBuilder.Entity<FlowsEdgesVersionLog20231016>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("flows_edges_version_log_20231016", "workflow");

                entity.Property(e => e.FevId).HasColumnName("fev_id");

                entity.Property(e => e.FevTimestamp)
                    .HasPrecision(6)
                    .HasColumnName("fev_timestamp");

                entity.Property(e => e.FevUserid)
                    .HasMaxLength(50)
                    .HasColumnName("fev_userid");

                entity.Property(e => e.FevVersionCode)
                    .HasMaxLength(255)
                    .HasColumnName("fev_version_code");

                entity.Property(e => e.FleEdgesId)
                    .HasMaxLength(255)
                    .HasColumnName("fle_edges_id");

                entity.Property(e => e.FleEdgesLabel)
                    .HasMaxLength(255)
                    .HasColumnName("fle_edges_label");

                entity.Property(e => e.FleEdgesSource)
                    .HasMaxLength(255)
                    .HasColumnName("fle_edges_source");

                entity.Property(e => e.FleEdgesTarget).HasColumnName("fle_edges_target");

                entity.Property(e => e.FleEdgesType)
                    .HasMaxLength(255)
                    .HasColumnName("fle_edges_type");

                entity.Property(e => e.FleFlhId).HasColumnName("fle_flh_id");

                entity.Property(e => e.FleId).HasColumnName("fle_id");
            });

            modelBuilder.Entity<FlowsNodes>(entity =>
            {
                entity.HasKey(e => e.FlnId)
                    .HasName("flows_nodes_pkey");

                entity.ToTable("flows_nodes", "workflow");

                entity.Property(e => e.FlnId)
                    .HasColumnName("fln_id")
                    .HasDefaultValueSql("nextval('workflow_nodes_fln_id_seq'::regclass)");

                entity.Property(e => e.FlnFlhId)
                    .HasColumnName("fln_flh_id")
                    .HasComment("relasi to flows.flh_id");

                entity.Property(e => e.FlnNodesH)
                    .HasPrecision(16, 2)
                    .HasColumnName("fln_nodes_h")
                    .HasComment("h");

                entity.Property(e => e.FlnNodesId)
                    .HasColumnType("character varying")
                    .HasColumnName("fln_nodes_id")
                    .HasComment("id");

                entity.Property(e => e.FlnNodesLeft)
                    .HasPrecision(16, 2)
                    .HasColumnName("fln_nodes_left")
                    .HasComment("left");

                entity.Property(e => e.FlnNodesText)
                    .HasMaxLength(255)
                    .HasColumnName("fln_nodes_text")
                    .HasComment("text");

                entity.Property(e => e.FlnNodesTop)
                    .HasPrecision(16, 2)
                    .HasColumnName("fln_nodes_top")
                    .HasComment("top");

                entity.Property(e => e.FlnNodesType)
                    .HasMaxLength(255)
                    .HasColumnName("fln_nodes_type")
                    .HasComment("type");

                entity.Property(e => e.FlnNodesW)
                    .HasPrecision(16, 2)
                    .HasColumnName("fln_nodes_w")
                    .HasComment("w");
            });

            modelBuilder.Entity<FlowsNodes20231020>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("flows_nodes_20231020", "workflow");

                entity.Property(e => e.FlnFlhId).HasColumnName("fln_flh_id");

                entity.Property(e => e.FlnId).HasColumnName("fln_id");

                entity.Property(e => e.FlnNodesH)
                    .HasPrecision(16, 2)
                    .HasColumnName("fln_nodes_h");

                entity.Property(e => e.FlnNodesId)
                    .HasColumnType("character varying")
                    .HasColumnName("fln_nodes_id");

                entity.Property(e => e.FlnNodesLeft)
                    .HasPrecision(16, 2)
                    .HasColumnName("fln_nodes_left");

                entity.Property(e => e.FlnNodesText)
                    .HasMaxLength(255)
                    .HasColumnName("fln_nodes_text");

                entity.Property(e => e.FlnNodesTop)
                    .HasPrecision(16, 2)
                    .HasColumnName("fln_nodes_top");

                entity.Property(e => e.FlnNodesType)
                    .HasMaxLength(255)
                    .HasColumnName("fln_nodes_type");

                entity.Property(e => e.FlnNodesW)
                    .HasPrecision(16, 2)
                    .HasColumnName("fln_nodes_w");
            });

            modelBuilder.Entity<FlowsNodesVersionLog>(entity =>
            {
                entity.HasKey(e => e.FnvId)
                    .HasName("flows_nodes_version_log_pkey");

                entity.ToTable("flows_nodes_version_log", "workflow");

                entity.Property(e => e.FnvId).HasColumnName("fnv_id");

                entity.Property(e => e.FlnFlhId).HasColumnName("fln_flh_id");

                entity.Property(e => e.FlnId).HasColumnName("fln_id");

                entity.Property(e => e.FlnNodesH).HasColumnName("fln_nodes_h");

                entity.Property(e => e.FlnNodesId)
                    .HasMaxLength(255)
                    .HasColumnName("fln_nodes_id")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.FlnNodesLeft).HasColumnName("fln_nodes_left");

                entity.Property(e => e.FlnNodesText)
                    .HasMaxLength(255)
                    .HasColumnName("fln_nodes_text")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.FlnNodesTop).HasColumnName("fln_nodes_top");

                entity.Property(e => e.FlnNodesType).HasColumnName("fln_nodes_type");

                entity.Property(e => e.FlnNodesW).HasColumnName("fln_nodes_w");

                entity.Property(e => e.FnvTimestamp)
                    .HasPrecision(6)
                    .HasColumnName("fnv_timestamp");

                entity.Property(e => e.FnvUserid)
                    .HasMaxLength(50)
                    .HasColumnName("fnv_userid")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.FnvVersionCode)
                    .HasMaxLength(255)
                    .HasColumnName("fnv_version_code");
            });

            modelBuilder.Entity<FlowsProcess>(entity =>
            {
                entity.HasKey(e => e.FlpId)
                    .HasName("flows_process_pkey");

                entity.ToTable("flows_process", "workflow");

                entity.Property(e => e.FlpId).HasColumnName("flp_id");

                entity.Property(e => e.FlpAppId)
                    .HasMaxLength(225)
                    .HasColumnName("flp_app_id");

                entity.Property(e => e.FlpCreatedDate)
                    .HasPrecision(6)
                    .HasColumnName("flp_created_date");

                entity.Property(e => e.FlpFlhId).HasColumnName("flp_flh_id");

                entity.Property(e => e.FlpNodesH).HasColumnName("flp_nodes_h");

                entity.Property(e => e.FlpNodesId)
                    .HasMaxLength(225)
                    .HasColumnName("flp_nodes_id");

                entity.Property(e => e.FlpNodesLeft).HasColumnName("flp_nodes_left");

                entity.Property(e => e.FlpNodesText)
                    .HasMaxLength(225)
                    .HasColumnName("flp_nodes_text");

                entity.Property(e => e.FlpNodesTop).HasColumnName("flp_nodes_top");

                entity.Property(e => e.FlpNodesType)
                    .HasMaxLength(225)
                    .HasColumnName("flp_nodes_type");

                entity.Property(e => e.FlpNodesW).HasColumnName("flp_nodes_w");
            });

            modelBuilder.Entity<FlowsVersionLog>(entity =>
            {
                entity.HasKey(e => e.FvlId)
                    .HasName("flows_version_log_pkey");

                entity.ToTable("flows_version_log", "workflow");

                entity.Property(e => e.FvlId).HasColumnName("fvl_id");

                entity.Property(e => e.FlhApplied)
                    .HasMaxLength(150)
                    .HasColumnName("flh_applied");

                entity.Property(e => e.FlhCategory)
                    .HasMaxLength(100)
                    .HasColumnName("flh_category");

                entity.Property(e => e.FlhCode)
                    .HasMaxLength(255)
                    .HasColumnName("flh_code")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.FlhDesc).HasColumnName("flh_desc");

                entity.Property(e => e.FlhId).HasColumnName("flh_id");

                entity.Property(e => e.FlhName)
                    .HasMaxLength(255)
                    .HasColumnName("flh_name")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.FlhStatus)
                    .HasMaxLength(255)
                    .HasColumnName("flh_status")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.FlhType)
                    .HasMaxLength(255)
                    .HasColumnName("flh_type")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.FlhVersionCode)
                    .HasMaxLength(255)
                    .HasColumnName("flh_version_code")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.FlnIsdelete).HasColumnName("fln_isdelete");

                entity.Property(e => e.FvlTimestamp)
                    .HasPrecision(6)
                    .HasColumnName("fvl_timestamp");

                entity.Property(e => e.FvlUserid)
                    .HasMaxLength(50)
                    .HasColumnName("fvl_userid")
                    .HasDefaultValueSql("NULL::character varying");
            });

            modelBuilder.Entity<HistoricalTtable>(entity =>
            {
                entity.HasKey(e => e.HttId)
                    .HasName("historical_ttable_pkey");

                entity.ToTable("historical_ttable", "workflow");

                entity.Property(e => e.HttId).HasColumnName("htt_id");

                entity.Property(e => e.HttData).HasColumnName("htt_data");

                entity.Property(e => e.HttRshid)
                    .HasMaxLength(50)
                    .HasColumnName("htt_rshid")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.HttSourceId)
                    .HasMaxLength(150)
                    .HasColumnName("htt_source_id")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.HttTimestamp)
                    .HasPrecision(6)
                    .HasColumnName("htt_timestamp");

                entity.Property(e => e.HttTtable)
                    .HasMaxLength(250)
                    .HasColumnName("htt_ttable")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.HttUserid)
                    .HasMaxLength(150)
                    .HasColumnName("htt_userid")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.HttWfId)
                    .HasMaxLength(150)
                    .HasColumnName("htt_wf_id")
                    .HasDefaultValueSql("NULL::character varying");
            });

            modelBuilder.Entity<IrreversibleObject>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("irreversible_object", "workflow");

                entity.Property(e => e.IroCode)
                    .HasMaxLength(50)
                    .HasColumnName("iro_code")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.IroId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("iro_id");

                entity.Property(e => e.IroName)
                    .HasMaxLength(250)
                    .HasColumnName("iro_name")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.IroTimestamp).HasColumnName("iro_timestamp");

                entity.Property(e => e.IroType)
                    .HasMaxLength(250)
                    .HasColumnName("iro_type")
                    .HasDefaultValueSql("NULL::character varying");
            });

            modelBuilder.Entity<MasterForm>(entity =>
            {
                entity.HasKey(e => e.FumId)
                    .HasName("master_form_pkey");

                entity.ToTable("master_form", "workflow");

                entity.Property(e => e.FumId)
                    .HasColumnName("fum_id")
                    .HasDefaultValueSql("nextval('master_form_fum_id_seq'::regclass)");

                entity.Property(e => e.FumCode)
                    .HasMaxLength(255)
                    .HasColumnName("fum_code");

                entity.Property(e => e.FumName)
                    .HasMaxLength(255)
                    .HasColumnName("fum_name");

                entity.Property(e => e.FumSla)
                    .HasColumnName("fum_sla")
                    .HasComment("dalam minutes");

                entity.Property(e => e.FumTmpl)
                    .HasMaxLength(255)
                    .HasColumnName("fum_tmpl");

                entity.Property(e => e.FumType)
                    .HasMaxLength(255)
                    .HasColumnName("fum_type");

                entity.Property(e => e.FumUrl)
                    .HasMaxLength(255)
                    .HasColumnName("fum_url");

                entity.Property(e => e.FumUsed)
                    .HasMaxLength(10)
                    .HasColumnName("fum_used");
            });

            modelBuilder.Entity<MasterObject>(entity =>
            {
                entity.HasKey(e => e.MobId)
                    .HasName("master_object_pkey");

                entity.ToTable("master_object", "workflow");

                entity.Property(e => e.MobId).HasColumnName("mob_id");

                entity.Property(e => e.MobCode)
                    .HasMaxLength(20)
                    .HasColumnName("mob_code");

                entity.Property(e => e.MobCreatedat)
                    .HasPrecision(6)
                    .HasColumnName("mob_createdat");

                entity.Property(e => e.MobCreatedby)
                    .HasMaxLength(50)
                    .HasColumnName("mob_createdby");

                entity.Property(e => e.MobIsactive)
                    .HasColumnName("mob_isactive")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.MobName)
                    .HasMaxLength(100)
                    .HasColumnName("mob_name");

                entity.Property(e => e.MobUpdatedat)
                    .HasPrecision(6)
                    .HasColumnName("mob_updatedat");

                entity.Property(e => e.MobUpdatedby)
                    .HasMaxLength(50)
                    .HasColumnName("mob_updatedby");
            });

            modelBuilder.Entity<MasterRacResult>(entity =>
            {
                entity.HasKey(e => e.MrsId)
                    .HasName("master_rac_result_pkey");

                entity.ToTable("master_rac_result", "workflow");

                entity.Property(e => e.MrsId).HasColumnName("mrs_id");

                entity.Property(e => e.MrsFieldName)
                    .HasMaxLength(200)
                    .HasColumnName("mrs_field_name");

                entity.Property(e => e.MrsKriteria)
                    .HasMaxLength(200)
                    .HasColumnName("mrs_kriteria");

                entity.Property(e => e.MrsRejectCode)
                    .HasMaxLength(50)
                    .HasColumnName("mrs_reject_code");

                entity.Property(e => e.MrsValueRecommendation)
                    .HasMaxLength(500)
                    .HasColumnName("mrs_value_recommendation");
            });

            modelBuilder.Entity<ObjectIntegration>(entity =>
            {
                entity.HasKey(e => e.OinId)
                    .HasName("object_integration_pkey");

                entity.ToTable("object_integration", "workflow");

                entity.Property(e => e.OinId).HasColumnName("oin_id");

                entity.Property(e => e.OinApiModule)
                    .HasMaxLength(255)
                    .HasColumnName("oin_api_module");

                entity.Property(e => e.OinCode)
                    .HasMaxLength(100)
                    .HasColumnName("oin_code")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.OinEndcode)
                    .HasMaxLength(100)
                    .HasColumnName("oin_endcode")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.OinModule)
                    .HasMaxLength(100)
                    .HasColumnName("oin_module")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.OinName)
                    .HasMaxLength(250)
                    .HasColumnName("oin_name")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.OinProvider)
                    .HasMaxLength(100)
                    .HasColumnName("oin_provider")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.OinType)
                    .HasMaxLength(100)
                    .HasColumnName("oin_type")
                    .HasDefaultValueSql("NULL::character varying");
            });

            modelBuilder.Entity<ObjectIntegrationParams>(entity =>
            {
                entity.HasKey(e => e.OipId)
                    .HasName("object_integration_params_pkey");

                entity.ToTable("object_integration_params", "workflow");

                entity.Property(e => e.OipId).HasColumnName("oip_id");

                entity.Property(e => e.OipCode)
                    .HasMaxLength(100)
                    .HasColumnName("oip_code")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.OipKey)
                    .HasMaxLength(100)
                    .HasColumnName("oip_key")
                    .HasDefaultValueSql("NULL::character varying");
            });

            modelBuilder.Entity<Privilege>(entity =>
            {
                entity.HasKey(e => e.PriId)
                    .HasName("privilege_pkey");

                entity.ToTable("privilege", "workflow");

                entity.Property(e => e.PriId).HasColumnName("pri_id");

                entity.Property(e => e.PriFunction)
                    .HasMaxLength(255)
                    .HasColumnName("pri_function");

                entity.Property(e => e.PriPlafonForm)
                    .HasPrecision(255)
                    .HasColumnName("pri_plafon_form");

                entity.Property(e => e.PriPlafonTo)
                    .HasPrecision(255)
                    .HasColumnName("pri_plafon_to");

                entity.Property(e => e.PriRole).HasColumnName("pri_role");

                entity.Property(e => e.PriStatus)
                    .HasMaxLength(255)
                    .HasColumnName("pri_status");
            });

            modelBuilder.Entity<PrivilegeBranch>(entity =>
            {
                entity.HasKey(e => e.PrbId)
                    .HasName("privilege_branch_pkey");

                entity.ToTable("privilege_branch", "workflow");

                entity.Property(e => e.PrbId).HasColumnName("prb_id");

                entity.Property(e => e.PrbBranchCode)
                    .HasMaxLength(255)
                    .HasColumnName("prb_branch_code");

                entity.Property(e => e.PrbPriId).HasColumnName("prb_pri_id");
            });

            modelBuilder.Entity<PrivilegeForm>(entity =>
            {
                entity.HasKey(e => e.PrfId)
                    .HasName("privilege_form_pkey");

                entity.ToTable("privilege_form", "workflow");

                entity.Property(e => e.PrfId).HasColumnName("prf_id");

                entity.Property(e => e.PrfFormAccess)
                    .HasMaxLength(255)
                    .HasColumnName("prf_form_access");

                entity.Property(e => e.PrfFormCode)
                    .HasMaxLength(255)
                    .HasColumnName("prf_form_code");

                entity.Property(e => e.PrfFormType)
                    .HasMaxLength(255)
                    .HasColumnName("prf_form_type");

                entity.Property(e => e.PrfPriId).HasColumnName("prf_pri_id");
            });

            modelBuilder.Entity<ProcessLog>(entity =>
            {
                entity.HasKey(e => e.PolId)
                    .HasName("process_log_pkey");

                entity.ToTable("process_log", "workflow");

                entity.Property(e => e.PolId).HasColumnName("pol_id");

                entity.Property(e => e.PolActionDate)
                    .HasPrecision(6)
                    .HasColumnName("pol_action_date");

                entity.Property(e => e.PolAppId)
                    .HasMaxLength(255)
                    .HasColumnName("pol_app_id");

                entity.Property(e => e.PolData).HasColumnName("pol_data");

                entity.Property(e => e.PolEdgesId)
                    .HasMaxLength(255)
                    .HasColumnName("pol_edges_id");

                entity.Property(e => e.PolFinishDate)
                    .HasPrecision(6)
                    .HasColumnName("pol_finish_date");

                entity.Property(e => e.PolNotes).HasColumnName("pol_notes");

                entity.Property(e => e.PolSourceId)
                    .HasMaxLength(255)
                    .HasColumnName("pol_source_id");

                entity.Property(e => e.PolStatus)
                    .HasMaxLength(255)
                    .HasColumnName("pol_status");

                entity.Property(e => e.PolTtable)
                    .HasMaxLength(255)
                    .HasColumnName("pol_ttable");

                entity.Property(e => e.PolUsr)
                    .HasMaxLength(255)
                    .HasColumnName("pol_usr");

                entity.Property(e => e.PolWfId).HasColumnName("pol_wf_id");
            });

            modelBuilder.Entity<ProcessLogPic>(entity =>
            {
                entity.HasKey(e => e.PlpId)
                    .HasName("process_log_pic_pkey");

                entity.ToTable("process_log_pic", "workflow");

                entity.Property(e => e.PlpId).HasColumnName("plp_id");

                entity.Property(e => e.PlpAppNo)
                    .HasMaxLength(50)
                    .HasColumnName("plp_app_no");

                entity.Property(e => e.PlpCreatedDate)
                    .HasPrecision(6)
                    .HasColumnName("plp_created_date");

                entity.Property(e => e.PlpDecision)
                    .HasMaxLength(50)
                    .HasColumnName("plp_decision");

                entity.Property(e => e.PlpEnd)
                    .HasPrecision(6)
                    .HasColumnName("plp_end");

                entity.Property(e => e.PlpPic)
                    .HasMaxLength(50)
                    .HasColumnName("plp_pic");

                entity.Property(e => e.PlpProcess)
                    .HasMaxLength(50)
                    .HasColumnName("plp_process");

                entity.Property(e => e.PlpRshId)
                    .HasMaxLength(50)
                    .HasColumnName("plp_rsh_id");

                entity.Property(e => e.PlpSla).HasColumnName("plp_sla");

                entity.Property(e => e.PlpStart)
                    .HasPrecision(6)
                    .HasColumnName("plp_start");

                entity.Property(e => e.PlpStatus)
                    .HasMaxLength(50)
                    .HasColumnName("plp_status");
            });

            modelBuilder.Entity<QuestionValues>(entity =>
            {
                entity.HasKey(e => e.QsvId)
                    .HasName("question_values_pkey");

                entity.ToTable("question_values", "workflow");

                entity.Property(e => e.QsvId).HasColumnName("qsv_id");

                entity.Property(e => e.QsvCode)
                    .HasMaxLength(30)
                    .HasColumnName("qsv_code");

                entity.Property(e => e.QsvField)
                    .HasMaxLength(100)
                    .HasColumnName("qsv_field");
            });

            modelBuilder.Entity<ResponseResult>(entity =>
            {
                entity.HasKey(e => e.RreId)
                    .HasName("response_result_pkey");

                entity.ToTable("response_result", "workflow");

                entity.Property(e => e.RreId).HasColumnName("rre_id");

                entity.Property(e => e.RreObjectCode)
                    .HasMaxLength(255)
                    .HasColumnName("rre_object_code")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.RreResult).HasColumnName("rre_result");

                entity.Property(e => e.RreRshid)
                    .HasMaxLength(255)
                    .HasColumnName("rre_rshid")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.RreTimestamp)
                    .HasPrecision(6)
                    .HasColumnName("rre_timestamp");
            });

            modelBuilder.Entity<ReverseAppApproval>(entity =>
            {
                entity.HasKey(e => e.RaaId)
                    .HasName("reverse_app_approval_pkey");

                entity.ToTable("reverse_app_approval", "workflow");

                entity.Property(e => e.RaaId).HasColumnName("raa_id");

                entity.Property(e => e.RaaAplnumber)
                    .HasMaxLength(250)
                    .HasColumnName("raa_aplnumber")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.RaaAppid)
                    .HasMaxLength(250)
                    .HasColumnName("raa_appid")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.RaaApproval)
                    .HasPrecision(6)
                    .HasColumnName("raa_approval");

                entity.Property(e => e.RaaApprovedBy)
                    .HasMaxLength(250)
                    .HasColumnName("raa_approved_by")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.RaaLastSource)
                    .HasMaxLength(250)
                    .HasColumnName("raa_last_source")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.RaaLogid)
                    .HasMaxLength(250)
                    .HasColumnName("raa_logid")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.RaaNotes).HasColumnName("raa_notes");

                entity.Property(e => e.RaaRequest)
                    .HasPrecision(6)
                    .HasColumnName("raa_request");

                entity.Property(e => e.RaaRequestedBy)
                    .HasMaxLength(250)
                    .HasColumnName("raa_requested_by")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.RaaRevEdgeid)
                    .HasMaxLength(250)
                    .HasColumnName("raa_rev_edgeid")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.RaaRevObject)
                    .HasMaxLength(250)
                    .HasColumnName("raa_rev_object")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.RaaRevObjectType)
                    .HasMaxLength(250)
                    .HasColumnName("raa_rev_object_type")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.RaaStatus)
                    .HasMaxLength(250)
                    .HasColumnName("raa_status")
                    .HasDefaultValueSql("NULL::character varying");
            });

            modelBuilder.Entity<ReversibleObject>(entity =>
            {
                entity.HasKey(e => e.RroId)
                    .HasName("reversible_object_pkey");

                entity.ToTable("reversible_object", "workflow");

                entity.Property(e => e.RroId).HasColumnName("rro_id");

                entity.Property(e => e.RroCode)
                    .HasMaxLength(50)
                    .HasColumnName("rro_code")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.RroName)
                    .HasMaxLength(250)
                    .HasColumnName("rro_name")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.RroTimestamp)
                    .HasPrecision(6)
                    .HasColumnName("rro_timestamp");

                entity.Property(e => e.RroType)
                    .HasMaxLength(250)
                    .HasColumnName("rro_type")
                    .HasDefaultValueSql("NULL::character varying");
            });

            modelBuilder.Entity<RptProcessLog>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("rpt_process_log", "workflow");

                entity.Property(e => e.PolActionDate)
                    .HasPrecision(6)
                    .HasColumnName("pol_action_date");

                entity.Property(e => e.PolAppId).HasColumnName("pol_app_id");

                entity.Property(e => e.PolData).HasColumnName("pol_data");

                entity.Property(e => e.PolEdgesId)
                    .HasMaxLength(60)
                    .HasColumnName("pol_edges_id");

                entity.Property(e => e.PolFinishDate)
                    .HasPrecision(6)
                    .HasColumnName("pol_finish_date");

                entity.Property(e => e.PolId).HasColumnName("pol_id");

                entity.Property(e => e.PolNotes).HasColumnName("pol_notes");

                entity.Property(e => e.PolSourceId)
                    .HasMaxLength(40)
                    .HasColumnName("pol_source_id");

                entity.Property(e => e.PolStatus)
                    .HasMaxLength(100)
                    .HasColumnName("pol_status");

                entity.Property(e => e.PolTtable)
                    .HasMaxLength(100)
                    .HasColumnName("pol_ttable");

                entity.Property(e => e.PolUsr)
                    .HasMaxLength(100)
                    .HasColumnName("pol_usr");

                entity.Property(e => e.PolWfId).HasColumnName("pol_wf_id");
            });

            modelBuilder.Entity<RptProcessLogPic>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("rpt_process_log_pic", "workflow");

                entity.Property(e => e.PlpAppNo)
                    .HasMaxLength(50)
                    .HasColumnName("plp_app_no");

                entity.Property(e => e.PlpCreatedDate)
                    .HasPrecision(6)
                    .HasColumnName("plp_created_date");

                entity.Property(e => e.PlpDecision)
                    .HasMaxLength(50)
                    .HasColumnName("plp_decision");

                entity.Property(e => e.PlpEnd)
                    .HasPrecision(6)
                    .HasColumnName("plp_end");

                entity.Property(e => e.PlpId).HasColumnName("plp_id");

                entity.Property(e => e.PlpPic)
                    .HasMaxLength(50)
                    .HasColumnName("plp_pic");

                entity.Property(e => e.PlpProcess)
                    .HasMaxLength(50)
                    .HasColumnName("plp_process");

                entity.Property(e => e.PlpRshId).HasColumnName("plp_rsh_id");

                entity.Property(e => e.PlpSla).HasColumnName("plp_sla");

                entity.Property(e => e.PlpStart)
                    .HasPrecision(6)
                    .HasColumnName("plp_start");

                entity.Property(e => e.PlpStatus)
                    .HasMaxLength(50)
                    .HasColumnName("plp_status");
            });

            modelBuilder.Entity<WfCancelRequest>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("wf_cancel_request", "workflow");

                entity.Property(e => e.WcrApprdate)
                    .HasPrecision(6)
                    .HasColumnName("wcr_apprdate");

                entity.Property(e => e.WcrApprovedby)
                    .HasMaxLength(100)
                    .HasColumnName("wcr_approvedby");

                entity.Property(e => e.WcrDecisionCancel)
                    .HasMaxLength(150)
                    .HasColumnName("wcr_decision_cancel");

                entity.Property(e => e.WcrId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("wcr_id");

                entity.Property(e => e.WcrLastNode)
                    .HasMaxLength(150)
                    .HasColumnName("wcr_last_node");

                entity.Property(e => e.WcrNotes).HasColumnName("wcr_notes");

                entity.Property(e => e.WcrReqdate)
                    .HasPrecision(6)
                    .HasColumnName("wcr_reqdate");

                entity.Property(e => e.WcrRequestby)
                    .HasMaxLength(100)
                    .HasColumnName("wcr_requestby");

                entity.Property(e => e.WcrRshid)
                    .HasMaxLength(50)
                    .HasColumnName("wcr_rshid");
            });

            modelBuilder.Entity<WorkflowDetail>(entity =>
            {
                entity.HasKey(e => e.WfdId)
                    .HasName("workflow_detail_pkey");

                entity.ToTable("workflow_detail", "workflow");

                entity.Property(e => e.WfdId).HasColumnName("wfd_id");

                entity.Property(e => e.WfdHeaderid).HasColumnName("wfd_headerid");

                entity.Property(e => e.WfdObjCode)
                    .HasMaxLength(200)
                    .HasColumnName("wfd_obj_code");

                entity.Property(e => e.WfdObjOutput)
                    .HasColumnName("wfd_obj_output")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.WfdObjType)
                    .HasMaxLength(200)
                    .HasColumnName("wfd_obj_type");

                entity.Property(e => e.WfdObjX)
                    .HasMaxLength(200)
                    .HasColumnName("wfd_obj_x");

                entity.Property(e => e.WfdObjY)
                    .HasMaxLength(200)
                    .HasColumnName("wfd_obj_y");

                entity.Property(e => e.WfdOutputPos)
                    .HasMaxLength(250)
                    .HasColumnName("wfd_output_pos");

                entity.Property(e => e.WfdParentid).HasColumnName("wfd_parentid");

                entity.Property(e => e.WfdUpdatedat)
                    .HasPrecision(6)
                    .HasColumnName("wfd_updatedat");

                entity.Property(e => e.WfdUpdatedby)
                    .HasMaxLength(50)
                    .HasColumnName("wfd_updatedby");
            });

            modelBuilder.Entity<WorkflowDetailConnection>(entity =>
            {
                entity.HasKey(e => e.WfcId)
                    .HasName("workflow_detail_connection_pkey");

                entity.ToTable("workflow_detail_connection", "workflow");

                entity.Property(e => e.WfcId).HasColumnName("wfc_id");

                entity.Property(e => e.WfcDtlid).HasColumnName("wfc_dtlid");

                entity.Property(e => e.WfcHeaderid).HasColumnName("wfc_headerid");

                entity.Property(e => e.WfcObjConnection)
                    .HasMaxLength(200)
                    .HasColumnName("wfc_obj_connection");

                entity.Property(e => e.WfcParentNode)
                    .HasMaxLength(100)
                    .HasColumnName("wfc_parent_node");
            });

            modelBuilder.Entity<WorkflowDetailCustom>(entity =>
            {
                entity.HasKey(e => e.WcpId)
                    .HasName("workflow_detail_custom_pkey");

                entity.ToTable("workflow_detail_custom", "workflow");

                entity.Property(e => e.WcpId).HasColumnName("wcp_id");

                entity.Property(e => e.WcpCustomid).HasColumnName("wcp_customid");

                entity.Property(e => e.WcpDtlid).HasColumnName("wcp_dtlid");
            });

            modelBuilder.Entity<WorkflowDetailOutput>(entity =>
            {
                entity.HasKey(e => e.WfoId)
                    .HasName("workflow_detail_output_pkey");

                entity.ToTable("workflow_detail_output", "workflow");

                entity.Property(e => e.WfoId).HasColumnName("wfo_id");

                entity.Property(e => e.WfdObjOutput)
                    .HasMaxLength(200)
                    .HasColumnName("wfd_obj_output");

                entity.Property(e => e.WfoDtlid).HasColumnName("wfo_dtlid");
            });

            modelBuilder.Entity<WorkflowDetailParaminput>(entity =>
            {
                entity.HasKey(e => e.WdpId)
                    .HasName("workflow_detail_paraminput_pkey");

                entity.ToTable("workflow_detail_paraminput", "workflow");

                entity.Property(e => e.WdpId).HasColumnName("wdp_id");

                entity.Property(e => e.WdpDtlid).HasColumnName("wdp_dtlid");

                entity.Property(e => e.WdpLabel)
                    .HasMaxLength(250)
                    .HasColumnName("wdp_label");

                entity.Property(e => e.WdpMapid).HasColumnName("wdp_mapid");
            });

            modelBuilder.Entity<WorkflowHeader>(entity =>
            {
                entity.HasKey(e => e.WfhId)
                    .HasName("workflow_header_pkey");

                entity.ToTable("workflow_header", "workflow");

                entity.Property(e => e.WfhId).HasColumnName("wfh_id");

                entity.Property(e => e.WfhCreatedat)
                    .HasPrecision(6)
                    .HasColumnName("wfh_createdat");

                entity.Property(e => e.WfhCreatedby)
                    .HasMaxLength(50)
                    .HasColumnName("wfh_createdby");

                entity.Property(e => e.WfhDesc)
                    .HasMaxLength(200)
                    .HasColumnName("wfh_desc");

                entity.Property(e => e.WfhIsactive)
                    .HasColumnName("wfh_isactive")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.WfhName)
                    .HasMaxLength(200)
                    .HasColumnName("wfh_name");

                entity.Property(e => e.WfhParamStart)
                    .HasMaxLength(200)
                    .HasColumnName("wfh_param_start");

                entity.Property(e => e.WfhUpdatedat)
                    .HasPrecision(6)
                    .HasColumnName("wfh_updatedat");

                entity.Property(e => e.WfhUpdatedby)
                    .HasMaxLength(50)
                    .HasColumnName("wfh_updatedby");
            });

            modelBuilder.Entity<WorkflowLogHistory>(entity =>
            {
                entity.HasKey(e => e.WlhId)
                    .HasName("workflow_log_history_pkey");

                entity.ToTable("workflow_log_history", "workflow");

                entity.Property(e => e.WlhId).HasColumnName("wlh_id");

                entity.Property(e => e.AcctNewLoanL6mUnd).HasColumnName("acct_new_loan_l6m_und");

                entity.Property(e => e.AddrDukcapil)
                    .HasMaxLength(5000)
                    .HasColumnName("addr_dukcapil");

                entity.Property(e => e.Asa)
                    .HasPrecision(12, 12)
                    .HasColumnName("asa");

                entity.Property(e => e.AskDrawdown)
                    .HasPrecision(20, 2)
                    .HasColumnName("ask_drawdown");

                entity.Property(e => e.AskLoan)
                    .HasPrecision(18, 2)
                    .HasColumnName("ask_loan");

                entity.Property(e => e.AskTenor).HasColumnName("ask_tenor");

                entity.Property(e => e.AssignedLim1)
                    .HasPrecision(32)
                    .HasColumnName("assigned_lim1");

                entity.Property(e => e.BkAmtNewLoanL6mUnd)
                    .HasPrecision(19)
                    .HasColumnName("bk_amt_new_loan_l6m_und");

                entity.Property(e => e.BkBlacklistCode).HasColumnName("bk_blacklist_code");

                entity.Property(e => e.BkCategory)
                    .HasMaxLength(50)
                    .HasColumnName("bk_category");

                entity.Property(e => e.BkCategoryFlag)
                    .HasMaxLength(1)
                    .HasColumnName("bk_category_flag");

                entity.Property(e => e.BkCollectibility).HasColumnName("bk_collectibility");

                entity.Property(e => e.BkDbr)
                    .HasPrecision(10, 4)
                    .HasColumnName("bk_dbr");

                entity.Property(e => e.BkFlag).HasColumnName("bk_flag");

                entity.Property(e => e.BkFreqWoCc12).HasColumnName("bk_freq_wo_cc_12");

                entity.Property(e => e.BkFreqWoNoncc12).HasColumnName("bk_freq_wo_noncc_12");

                entity.Property(e => e.BkFreqXdpd6).HasColumnName("bk_freq_xdpd_6");

                entity.Property(e => e.BkLastXdpd).HasColumnName("bk_last_xdpd");

                entity.Property(e => e.BkLimitSecureLoan)
                    .HasPrecision(19, 2)
                    .HasColumnName("bk_limit_secure_loan");

                entity.Property(e => e.BkMaxDpd).HasColumnName("bk_max_dpd");

                entity.Property(e => e.BkMaxLimitCc)
                    .HasPrecision(32)
                    .HasColumnName("bk_max_limit_cc");

                entity.Property(e => e.BkMaxLimitMobCc).HasColumnName("bk_max_limit_mob_cc");

                entity.Property(e => e.BkMaxMob).HasColumnName("bk_max_mob");

                entity.Property(e => e.BkMaxMobCc).HasColumnName("bk_max_mob_cc");

                entity.Property(e => e.BkMonthlyCflIncome)
                    .HasPrecision(19, 2)
                    .HasColumnName("bk_monthly_cfl_income");

                entity.Property(e => e.BkMonthlyObligation)
                    .HasPrecision(32)
                    .HasColumnName("bk_monthly_obligation");

                entity.Property(e => e.BkOsDpd)
                    .HasPrecision(32, 2)
                    .HasColumnName("bk_os_dpd");

                entity.Property(e => e.BkOsUnsecuredNoncc)
                    .HasPrecision(32)
                    .HasColumnName("bk_os_unsecured_noncc");

                entity.Property(e => e.BkOsWoCc)
                    .HasPrecision(32)
                    .HasColumnName("bk_os_wo_cc");

                entity.Property(e => e.BkPefindoScore).HasColumnName("bk_pefindo_score");

                entity.Property(e => e.BkRestructureFlag).HasColumnName("bk_restructure_flag");

                entity.Property(e => e.BkTotAmt)
                    .HasPrecision(32)
                    .HasColumnName("bk_tot_amt");

                entity.Property(e => e.BkTotalLimitCc)
                    .HasPrecision(19, 2)
                    .HasColumnName("bk_total_limit_cc");

                entity.Property(e => e.BkUnsecuredExp)
                    .HasPrecision(32)
                    .HasColumnName("bk_unsecured_exp");

                entity.Property(e => e.BkUtilCc)
                    .HasPrecision(8, 2)
                    .HasColumnName("bk_util_cc");

                entity.Property(e => e.BkWorstDelq12).HasColumnName("bk_worst_delq_12");

                entity.Property(e => e.BkWorstDelq3).HasColumnName("bk_worst_delq_3");

                entity.Property(e => e.BkWorstDelq6).HasColumnName("bk_worst_delq_6");

                entity.Property(e => e.BureauGroup)
                    .HasMaxLength(50)
                    .HasColumnName("bureau_group");

                entity.Property(e => e.CRestrictedCom)
                    .HasMaxLength(20)
                    .HasColumnName("c_restricted_com");

                entity.Property(e => e.CRestrictedIndustry).HasColumnName("c_restricted_industry");

                entity.Property(e => e.CampaignId)
                    .HasMaxLength(500)
                    .HasColumnName("campaign_id");

                entity.Property(e => e.Cat)
                    .HasMaxLength(500)
                    .HasColumnName("cat");

                entity.Property(e => e.Category)
                    .HasMaxLength(500)
                    .HasColumnName("category");

                entity.Property(e => e.CcComVar1)
                    .HasMaxLength(20)
                    .HasColumnName("cc_com_var1");

                entity.Property(e => e.CcComVar2)
                    .HasMaxLength(20)
                    .HasColumnName("cc_com_var2");

                entity.Property(e => e.CcComVar3)
                    .HasMaxLength(20)
                    .HasColumnName("cc_com_var3");

                entity.Property(e => e.CcComVar4)
                    .HasMaxLength(20)
                    .HasColumnName("cc_com_var4");

                entity.Property(e => e.CcComVar5)
                    .HasMaxLength(20)
                    .HasColumnName("cc_com_var5");

                entity.Property(e => e.CcComVar6)
                    .HasMaxLength(10)
                    .HasColumnName("cc_com_var6");

                entity.Property(e => e.CcComVar7)
                    .HasMaxLength(20)
                    .HasColumnName("cc_com_var7");

                entity.Property(e => e.ChannelId)
                    .HasMaxLength(10)
                    .HasColumnName("channel_id");

                entity.Property(e => e.CheckStatus)
                    .HasMaxLength(20)
                    .HasColumnName("check_status");

                entity.Property(e => e.Citizenship)
                    .HasMaxLength(10)
                    .HasColumnName("citizenship");

                entity.Property(e => e.CoYears)
                    .HasPrecision(8, 2)
                    .HasColumnName("co_years");

                entity.Property(e => e.ComVar1)
                    .HasMaxLength(1)
                    .HasColumnName("com_var1");

                entity.Property(e => e.ComVar2)
                    .HasMaxLength(1)
                    .HasColumnName("com_var2");

                entity.Property(e => e.ComVar3)
                    .HasMaxLength(1)
                    .HasColumnName("com_var3");

                entity.Property(e => e.ComVar4)
                    .HasMaxLength(4)
                    .HasColumnName("com_var4");

                entity.Property(e => e.ComVar5)
                    .HasMaxLength(1)
                    .HasColumnName("com_var5");

                entity.Property(e => e.CstAddress)
                    .HasMaxLength(5000)
                    .HasColumnName("cst_address");

                entity.Property(e => e.CstAddressCity)
                    .HasMaxLength(50)
                    .HasColumnName("cst_address_city");

                entity.Property(e => e.CstAge).HasColumnName("cst_age");

                entity.Property(e => e.CstCif)
                    .HasMaxLength(100)
                    .HasColumnName("cst_cif");

                entity.Property(e => e.CstCompanyName)
                    .HasMaxLength(100)
                    .HasColumnName("cst_company_name");

                entity.Property(e => e.CstDetailedLoanPurpose)
                    .HasMaxLength(100)
                    .HasColumnName("cst_detailed_loan_purpose");

                entity.Property(e => e.CstDob)
                    .HasColumnType("date")
                    .HasColumnName("cst_dob");

                entity.Property(e => e.CstDocLegalBusnis)
                    .HasMaxLength(500)
                    .HasColumnName("cst_doc_legal_busnis");

                entity.Property(e => e.CstDocNpwp)
                    .HasMaxLength(500)
                    .HasColumnName("cst_doc_npwp");

                entity.Property(e => e.CstDocSupport)
                    .HasMaxLength(500)
                    .HasColumnName("cst_doc_support");

                entity.Property(e => e.CstEmail)
                    .HasMaxLength(100)
                    .HasColumnName("cst_email");

                entity.Property(e => e.CstEmergencyPhoneMobile)
                    .HasMaxLength(50)
                    .HasColumnName("cst_emergency_phone_mobile");

                entity.Property(e => e.CstEmergencyRelationship)
                    .HasMaxLength(50)
                    .HasColumnName("cst_emergency_relationship");

                entity.Property(e => e.CstEmploymentNumber)
                    .HasMaxLength(50)
                    .HasColumnName("cst_employment_number");

                entity.Property(e => e.CstFname)
                    .HasMaxLength(50)
                    .HasColumnName("cst_fname");

                entity.Property(e => e.CstImgDocEktp)
                    .HasMaxLength(500)
                    .HasColumnName("cst_img_doc_ektp");

                entity.Property(e => e.CstImgDocIncome)
                    .HasMaxLength(500)
                    .HasColumnName("cst_img_doc_income");

                entity.Property(e => e.CstImgDocKk)
                    .HasMaxLength(500)
                    .HasColumnName("cst_img_doc_kk");

                entity.Property(e => e.CstKtp)
                    .HasMaxLength(16)
                    .HasColumnName("cst_ktp");

                entity.Property(e => e.CstMaritalStatus)
                    .HasMaxLength(50)
                    .HasColumnName("cst_marital_status");

                entity.Property(e => e.CstMotherMaidenName)
                    .HasMaxLength(100)
                    .HasColumnName("cst_mother_maiden_name");

                entity.Property(e => e.CstNpwp)
                    .HasMaxLength(100)
                    .HasColumnName("cst_npwp");

                entity.Property(e => e.CstNumberOfChild).HasColumnName("cst_number_of_child");

                entity.Property(e => e.CstOtherBankCc)
                    .HasMaxLength(10)
                    .HasColumnName("cst_other_bank_cc");

                entity.Property(e => e.CstPhoneHome)
                    .HasMaxLength(50)
                    .HasColumnName("cst_phone_home");

                entity.Property(e => e.CstPhoneMobile)
                    .HasPrecision(30)
                    .HasColumnName("cst_phone_mobile");

                entity.Property(e => e.CstSex)
                    .HasMaxLength(20)
                    .HasColumnName("cst_sex");

                entity.Property(e => e.CstTypeOfCc)
                    .HasMaxLength(100)
                    .HasColumnName("cst_type_of_cc");

                entity.Property(e => e.CstWorkAddress)
                    .HasMaxLength(5000)
                    .HasColumnName("cst_work_address");

                entity.Property(e => e.CstWorkIncome)
                    .HasPrecision(18, 2)
                    .HasColumnName("cst_work_income");

                entity.Property(e => e.CstWorkPhone)
                    .HasMaxLength(50)
                    .HasColumnName("cst_work_phone");

                entity.Property(e => e.CstZipcode)
                    .HasMaxLength(50)
                    .HasColumnName("cst_zipcode");

                entity.Property(e => e.DbrCutOff)
                    .HasMaxLength(10)
                    .HasColumnName("dbr_cut_off");

                entity.Property(e => e.DbrTotal)
                    .HasPrecision(8, 4)
                    .HasColumnName("dbr_total");

                entity.Property(e => e.Decision)
                    .HasMaxLength(500)
                    .HasColumnName("decision");

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("dob");

                entity.Property(e => e.DobDukcapil)
                    .HasColumnType("date")
                    .HasColumnName("dob_dukcapil");

                entity.Property(e => e.DtiWithout)
                    .HasPrecision(4, 4)
                    .HasColumnName("dti_without");

                entity.Property(e => e.EgibilityCom).HasColumnName("egibility_com");

                entity.Property(e => e.EmergencyContact)
                    .HasMaxLength(50)
                    .HasColumnName("emergency_contact");

                entity.Property(e => e.EmploymentType)
                    .HasMaxLength(500)
                    .HasColumnName("employment_type");

                entity.Property(e => e.FinalAssignedLimit)
                    .HasPrecision(32)
                    .HasColumnName("final_assigned_limit");

                entity.Property(e => e.FinalIncome)
                    .HasPrecision(32)
                    .HasColumnName("final_income");

                entity.Property(e => e.FlagCard)
                    .HasMaxLength(100)
                    .HasColumnName("flag_card");

                entity.Property(e => e.FlagDecision)
                    .HasMaxLength(5)
                    .HasColumnName("flag_decision");

                entity.Property(e => e.FlagPassGeneral)
                    .HasMaxLength(5)
                    .HasColumnName("flag_pass_general");

                entity.Property(e => e.FlagPassVerification)
                    .HasMaxLength(5)
                    .HasColumnName("flag_pass_verification");

                entity.Property(e => e.GenderDukcapil)
                    .HasMaxLength(100)
                    .HasColumnName("gender_dukcapil");

                entity.Property(e => e.Group1)
                    .HasMaxLength(4)
                    .HasColumnName("group1");

                entity.Property(e => e.Group2)
                    .HasMaxLength(4)
                    .HasColumnName("group2");

                entity.Property(e => e.HomeAddressCity)
                    .HasMaxLength(200)
                    .HasColumnName("home_address_city");

                entity.Property(e => e.HomeMonthlyRentalFee)
                    .HasPrecision(19)
                    .HasColumnName("home_monthly_rental_fee");

                entity.Property(e => e.HomeOwnershipMonth).HasColumnName("home_ownership_month");

                entity.Property(e => e.HomeOwnershipStatus)
                    .HasMaxLength(10)
                    .HasColumnName("home_ownership_status");

                entity.Property(e => e.IndustryCode)
                    .HasMaxLength(5)
                    .HasColumnName("industry_code");

                entity.Property(e => e.InstallmentAmount)
                    .HasPrecision(18, 2)
                    .HasColumnName("installment_amount");

                entity.Property(e => e.InterestCalculation)
                    .HasMaxLength(15)
                    .HasColumnName("interest_calculation");

                entity.Property(e => e.InterestRate)
                    .HasPrecision(8, 4)
                    .HasColumnName("interest_rate");

                entity.Property(e => e.InterestRule)
                    .HasMaxLength(10)
                    .HasColumnName("interest_rule");

                entity.Property(e => e.InterestType)
                    .HasMaxLength(100)
                    .HasColumnName("interest_type");

                entity.Property(e => e.KabIdDukcapil)
                    .HasMaxLength(10)
                    .HasColumnName("kab_id_dukcapil");

                entity.Property(e => e.KabNameDukcapil)
                    .HasMaxLength(500)
                    .HasColumnName("kab_name_dukcapil");

                entity.Property(e => e.KecIdDukcapil)
                    .HasMaxLength(10)
                    .HasColumnName("kec_id_dukcapil");

                entity.Property(e => e.KecNameDukcapil)
                    .HasMaxLength(100)
                    .HasColumnName("kec_name_dukcapil");

                entity.Property(e => e.KelIdDukcapil)
                    .HasMaxLength(10)
                    .HasColumnName("kel_id_dukcapil");

                entity.Property(e => e.KelNameDukcapil)
                    .HasMaxLength(100)
                    .HasColumnName("kel_name_dukcapil");

                entity.Property(e => e.Keputusan)
                    .HasMaxLength(100)
                    .HasColumnName("keputusan");

                entity.Property(e => e.LastEducation)
                    .HasMaxLength(10)
                    .HasColumnName("last_education");

                entity.Property(e => e.LengthOfEmployment).HasColumnName("length_of_employment");

                entity.Property(e => e.ListHomeOwnershipCf)
                    .HasMaxLength(500)
                    .HasColumnName("list_home_ownership_cf");

                entity.Property(e => e.ListKewarganegaraan)
                    .HasMaxLength(50)
                    .HasColumnName("list_kewarganegaraan");

                entity.Property(e => e.LiveYear)
                    .HasPrecision(8, 2)
                    .HasColumnName("live_year");

                entity.Property(e => e.LoanAmount)
                    .HasPrecision(30)
                    .HasColumnName("loan_amount");

                entity.Property(e => e.LoanDisbursed)
                    .HasPrecision(30)
                    .HasColumnName("loan_disbursed");

                entity.Property(e => e.LoanInterest)
                    .HasPrecision(18, 4)
                    .HasColumnName("loan_interest");

                entity.Property(e => e.LoanTenor)
                    .HasPrecision(18, 2)
                    .HasColumnName("loan_tenor");

                entity.Property(e => e.MaritalStatus)
                    .HasMaxLength(30)
                    .HasColumnName("marital_status");

                entity.Property(e => e.MaritalStsDukcapil)
                    .HasMaxLength(100)
                    .HasColumnName("marital_sts_dukcapil");

                entity.Property(e => e.MaxAssignedLoanAmtMue)
                    .HasPrecision(32)
                    .HasColumnName("max_assigned_loan_amt_mue");

                entity.Property(e => e.MaxDbr)
                    .HasPrecision(8, 2)
                    .HasColumnName("max_dbr");

                entity.Property(e => e.MaxInstallment)
                    .HasPrecision(32)
                    .HasColumnName("max_installment");

                entity.Property(e => e.MaxInterest)
                    .HasPrecision(8, 4)
                    .HasColumnName("max_interest");

                entity.Property(e => e.MaxLoan)
                    .HasPrecision(19)
                    .HasColumnName("max_loan");

                entity.Property(e => e.MaxMueInt1).HasColumnName("max_mue_int1");

                entity.Property(e => e.MaxMueInt2).HasColumnName("max_mue_int2");

                entity.Property(e => e.MaxTenor).HasColumnName("max_tenor");

                entity.Property(e => e.MinLoan)
                    .HasPrecision(19)
                    .HasColumnName("min_loan");

                entity.Property(e => e.MinLoanCutOff)
                    .HasMaxLength(5)
                    .HasColumnName("min_loan_cut_off");

                entity.Property(e => e.MotherNameDukcapil)
                    .HasMaxLength(100)
                    .HasColumnName("mother_name_dukcapil");

                entity.Property(e => e.MthSncLastAcOpenUnd).HasColumnName("mth_snc_last_ac_open_und");

                entity.Property(e => e.MueExternal)
                    .HasPrecision(5, 2)
                    .HasColumnName("mue_external");

                entity.Property(e => e.NameCust)
                    .HasMaxLength(200)
                    .HasColumnName("name_cust");

                entity.Property(e => e.NameDukcapil)
                    .HasMaxLength(50)
                    .HasColumnName("name_dukcapil");

                entity.Property(e => e.NameJaroVal)
                    .HasPrecision(6, 4)
                    .HasColumnName("name_jaro_val");

                entity.Property(e => e.Nik)
                    .HasMaxLength(16)
                    .HasColumnName("nik");

                entity.Property(e => e.NikDukcapil)
                    .HasMaxLength(100)
                    .HasColumnName("nik_dukcapil");

                entity.Property(e => e.NotesAnalyst)
                    .HasMaxLength(5000)
                    .HasColumnName("notes_analyst");

                entity.Property(e => e.NumUnsecuredLoan).HasColumnName("num_unsecured_loan");

                entity.Property(e => e.PlCategory)
                    .HasMaxLength(50)
                    .HasColumnName("pl_category");

                entity.Property(e => e.PobDukcapil)
                    .HasMaxLength(100)
                    .HasColumnName("pob_dukcapil");

                entity.Property(e => e.ProductCode)
                    .HasMaxLength(10)
                    .HasColumnName("product_code");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(100)
                    .HasColumnName("product_name");

                entity.Property(e => e.ProductVersion)
                    .HasMaxLength(50)
                    .HasColumnName("product_version");

                entity.Property(e => e.R001)
                    .HasMaxLength(4)
                    .HasColumnName("r001");

                entity.Property(e => e.R004)
                    .HasMaxLength(4)
                    .HasColumnName("r004");

                entity.Property(e => e.R006)
                    .HasMaxLength(4)
                    .HasColumnName("r006");

                entity.Property(e => e.R007)
                    .HasMaxLength(4)
                    .HasColumnName("r007");

                entity.Property(e => e.R008)
                    .HasMaxLength(4)
                    .HasColumnName("r008");

                entity.Property(e => e.R009)
                    .HasMaxLength(4)
                    .HasColumnName("r009");

                entity.Property(e => e.R010)
                    .HasMaxLength(100)
                    .HasColumnName("r010");

                entity.Property(e => e.R011)
                    .HasMaxLength(10)
                    .HasColumnName("r011");

                entity.Property(e => e.R012)
                    .HasMaxLength(10)
                    .HasColumnName("r012");

                entity.Property(e => e.R013)
                    .HasMaxLength(10)
                    .HasColumnName("r013");

                entity.Property(e => e.R014)
                    .HasMaxLength(10)
                    .HasColumnName("r014");

                entity.Property(e => e.R015)
                    .HasMaxLength(10)
                    .HasColumnName("r015");

                entity.Property(e => e.R016)
                    .HasMaxLength(4)
                    .HasColumnName("r016");

                entity.Property(e => e.R017)
                    .HasMaxLength(10)
                    .HasColumnName("r017");

                entity.Property(e => e.R048)
                    .HasMaxLength(10)
                    .HasColumnName("r048");

                entity.Property(e => e.R049)
                    .HasMaxLength(10)
                    .HasColumnName("r049");

                entity.Property(e => e.R060)
                    .HasMaxLength(4)
                    .HasColumnName("r060");

                entity.Property(e => e.R065)
                    .HasMaxLength(10)
                    .HasColumnName("r065");

                entity.Property(e => e.R072)
                    .HasMaxLength(4)
                    .HasColumnName("r072");

                entity.Property(e => e.R088)
                    .HasMaxLength(4)
                    .HasColumnName("r088");

                entity.Property(e => e.R090)
                    .HasMaxLength(4)
                    .HasColumnName("r090");

                entity.Property(e => e.R098)
                    .HasMaxLength(4)
                    .HasColumnName("r098");

                entity.Property(e => e.R770)
                    .HasMaxLength(20)
                    .HasColumnName("r770");

                entity.Property(e => e.RatingScore)
                    .HasPrecision(2, 2)
                    .HasColumnName("rating_score");

                entity.Property(e => e.RcDedupHr)
                    .HasMaxLength(20)
                    .HasColumnName("rc_dedup_hr");

                entity.Property(e => e.RcEmploymentLength)
                    .HasMaxLength(10)
                    .HasColumnName("rc_employment_length");

                entity.Property(e => e.RcMaxAge)
                    .HasMaxLength(10)
                    .HasColumnName("rc_max_age");

                entity.Property(e => e.RcMinAge)
                    .HasMaxLength(10)
                    .HasColumnName("rc_min_age");

                entity.Property(e => e.RcMinincome)
                    .HasMaxLength(10)
                    .HasColumnName("rc_minincome");

                entity.Property(e => e.RcWni)
                    .HasMaxLength(10)
                    .HasColumnName("rc_wni");

                entity.Property(e => e.ReScore).HasColumnName("re_score");

                entity.Property(e => e.RefId)
                    .HasMaxLength(20)
                    .HasColumnName("ref_id");

                entity.Property(e => e.ReferralCode)
                    .HasMaxLength(50)
                    .HasColumnName("referral_code");

                entity.Property(e => e.RejectCode)
                    .HasMaxLength(100)
                    .HasColumnName("reject_code");

                entity.Property(e => e.ReqAmount)
                    .HasPrecision(18, 2)
                    .HasColumnName("req_amount");

                entity.Property(e => e.ReqTenor)
                    .HasPrecision(18, 2)
                    .HasColumnName("req_tenor");

                entity.Property(e => e.RlosCodeSector)
                    .HasMaxLength(10)
                    .HasColumnName("rlos_code_sector");

                entity.Property(e => e.RshId).HasColumnName("rsh_id");

                entity.Property(e => e.SavingAccount)
                    .HasMaxLength(50)
                    .HasColumnName("saving_account");

                entity.Property(e => e.SavingAccountName)
                    .HasMaxLength(100)
                    .HasColumnName("saving_account_name");

                entity.Property(e => e.Score).HasColumnName("score");

                entity.Property(e => e.ScoreBand)
                    .HasMaxLength(1)
                    .HasColumnName("score_band");

                entity.Property(e => e.ScoreCrr)
                    .HasMaxLength(20)
                    .HasColumnName("score_crr");

                entity.Property(e => e.ScorecardCutoff)
                    .HasMaxLength(10)
                    .HasColumnName("scorecard_cutoff");

                entity.Property(e => e.Segment)
                    .HasMaxLength(500)
                    .HasColumnName("segment");

                entity.Property(e => e.ServiceAreaHome).HasColumnName("service_area_home");

                entity.Property(e => e.ServiceAreaWork).HasColumnName("service_area_work");

                entity.Property(e => e.StartDate).HasColumnName("start_date");

                entity.Property(e => e.StateIdDukcapil)
                    .HasMaxLength(10)
                    .HasColumnName("state_id_dukcapil");

                entity.Property(e => e.StateNameDukcapil)
                    .HasMaxLength(100)
                    .HasColumnName("state_name_dukcapil");

                entity.Property(e => e.StatusDukcapil)
                    .HasMaxLength(20)
                    .HasColumnName("status_dukcapil");

                entity.Property(e => e.Testing1)
                    .HasPrecision(100, 10)
                    .HasColumnName("testing1");

                entity.Property(e => e.TmpTable)
                    .HasColumnType("character varying")
                    .HasColumnName("tmp_table");

                entity.Property(e => e.TotalMue).HasColumnName("total_mue");

                entity.Property(e => e.UpdateDate).HasColumnName("update_date");

                entity.Property(e => e.ValBureau)
                    .HasMaxLength(10)
                    .HasColumnName("val_bureau");

                entity.Property(e => e.ValCheckStatus)
                    .HasMaxLength(20)
                    .HasColumnName("val_check_status");

                entity.Property(e => e.ValDukcapil)
                    .HasMaxLength(10)
                    .HasColumnName("val_dukcapil");

                entity.Property(e => e.ValGrac)
                    .HasMaxLength(10)
                    .HasColumnName("val_grac");

                entity.Property(e => e.WorkAddressCity)
                    .HasMaxLength(20)
                    .HasColumnName("work_address_city");

                entity.Property(e => e.WorkBusinessPosition)
                    .HasMaxLength(20)
                    .HasColumnName("work_business_position");

                entity.Property(e => e.WorkCompanyType)
                    .HasMaxLength(20)
                    .HasColumnName("work_company_type");

                entity.Property(e => e.WorkEmploymentStatus)
                    .HasMaxLength(30)
                    .HasColumnName("work_employment_status");

                entity.Property(e => e.WorkGrossIncome)
                    .HasPrecision(19)
                    .HasColumnName("work_gross_income");

                entity.Property(e => e.WorkMonth).HasColumnName("work_month");

                entity.Property(e => e.WorkNumberOfEmployee)
                    .HasPrecision(2, 2)
                    .HasColumnName("work_number_of_employee");

                entity.Property(e => e.WorkProfession)
                    .HasMaxLength(50)
                    .HasColumnName("work_profession");

                entity.Property(e => e.WorkflowCode)
                    .HasColumnType("character varying")
                    .HasColumnName("workflow_code");
            });

            modelBuilder.Entity<WorkflowPending>(entity =>
            {
                entity.HasKey(e => e.WfpId)
                    .HasName("workflow_pending_pkey");

                entity.ToTable("workflow_pending", "workflow");

                entity.Property(e => e.WfpId).HasColumnName("wfp_id");

                entity.Property(e => e.WfpFlowid)
                    .HasMaxLength(10)
                    .HasColumnName("wfp_flowid")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.WfpGccode)
                    .HasMaxLength(500)
                    .HasColumnName("wfp_gccode")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.WfpInfo)
                    .HasMaxLength(255)
                    .HasColumnName("wfp_info")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.WfpMessage).HasColumnName("wfp_message");

                entity.Property(e => e.WfpNode)
                    .HasMaxLength(500)
                    .HasColumnName("wfp_node")
                    .HasDefaultValueSql("NULL::character varying");

                entity.Property(e => e.WfpPendingTime)
                    .HasPrecision(6)
                    .HasColumnName("wfp_pending_time");

                entity.Property(e => e.WfpProcessTime)
                    .HasPrecision(6)
                    .HasColumnName("wfp_process_time");

                entity.Property(e => e.WfpRshid)
                    .HasMaxLength(10)
                    .HasColumnName("wfp_rshid");

                entity.Property(e => e.WfpTtable)
                    .HasMaxLength(500)
                    .HasColumnName("wfp_ttable")
                    .HasDefaultValueSql("NULL::character varying");
            });

            modelBuilder.Entity<WorkflowProcess>(entity =>
            {
                entity.HasKey(e => e.WfpId)
                    .HasName("workflow_process_pkey");

                entity.ToTable("workflow_process", "workflow");

                entity.Property(e => e.WfpId).HasColumnName("wfp_id");

                entity.Property(e => e.WfpCreatedate)
                    .HasPrecision(6)
                    .HasColumnName("wfp_createdate");

                entity.Property(e => e.WfpEnddate)
                    .HasPrecision(6)
                    .HasColumnName("wfp_enddate");

                entity.Property(e => e.WfpMobilePhone)
                    .HasMaxLength(25)
                    .HasColumnName("wfp_mobile_phone");

                entity.Property(e => e.WfpProcessBy)
                    .HasMaxLength(25)
                    .HasColumnName("wfp_process_by");

                entity.Property(e => e.WfpProduct)
                    .HasMaxLength(25)
                    .HasColumnName("wfp_product");

                entity.Property(e => e.WfpStartdate)
                    .HasPrecision(6)
                    .HasColumnName("wfp_startdate");

                entity.Property(e => e.WfpStatus)
                    .HasMaxLength(25)
                    .HasColumnName("wfp_status");

                entity.Property(e => e.WfpTmpTable)
                    .HasMaxLength(250)
                    .HasColumnName("wfp_tmp_table");

                entity.Property(e => e.WfpWfid).HasColumnName("wfp_wfid");
            });

            modelBuilder.Entity<WorkflowProcessDtl>(entity =>
            {
                entity.HasKey(e => e.WfdId)
                    .HasName("workflow_process_dtl_pkey");

                entity.ToTable("workflow_process_dtl", "workflow");

                entity.Property(e => e.WfdId).HasColumnName("wfd_id");

                entity.Property(e => e.WfdHeaderid).HasColumnName("wfd_headerid");

                entity.Property(e => e.WfpEnddate)
                    .HasPrecision(6)
                    .HasColumnName("wfp_enddate");

                entity.Property(e => e.WfpObjid).HasColumnName("wfp_objid");

                entity.Property(e => e.WfpStartdate)
                    .HasPrecision(6)
                    .HasColumnName("wfp_startdate");

                entity.Property(e => e.WfpStatus)
                    .HasMaxLength(25)
                    .HasColumnName("wfp_status");
            });

            modelBuilder.Entity<WorkflowRacResult>(entity =>
            {
                entity.HasKey(e => e.WrsId)
                    .HasName("workflow_rac_result_pkey");

                entity.ToTable("workflow_rac_result", "workflow");

                entity.Property(e => e.WrsId).HasColumnName("wrs_id");

                entity.Property(e => e.AskLoan)
                    .HasPrecision(18, 2)
                    .HasColumnName("ask_loan");

                entity.Property(e => e.AskTenor).HasColumnName("ask_tenor");

                entity.Property(e => e.BkCategory)
                    .HasMaxLength(50)
                    .HasColumnName("bk_category");

                entity.Property(e => e.BkCollectibility).HasColumnName("bk_collectibility");

                entity.Property(e => e.BkMaxDpd).HasColumnName("bk_max_dpd");

                entity.Property(e => e.BkMonthlyObligation)
                    .HasPrecision(32)
                    .HasColumnName("bk_monthly_obligation");

                entity.Property(e => e.BkPefindoScore).HasColumnName("bk_pefindo_score");

                entity.Property(e => e.CRestrictedCom)
                    .HasMaxLength(20)
                    .HasColumnName("c_restricted_com");

                entity.Property(e => e.CRestrictedIndustry).HasColumnName("c_restricted_industry");

                entity.Property(e => e.Citizenship)
                    .HasMaxLength(10)
                    .HasColumnName("citizenship");

                entity.Property(e => e.CstAge).HasColumnName("cst_age");

                entity.Property(e => e.CstKtp)
                    .HasMaxLength(16)
                    .HasColumnName("cst_ktp");

                entity.Property(e => e.Decision)
                    .HasMaxLength(20)
                    .HasColumnName("decision");

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("dob");

                entity.Property(e => e.HomeAddressCity)
                    .HasMaxLength(20)
                    .HasColumnName("home_address_city");

                entity.Property(e => e.NameCust)
                    .HasMaxLength(50)
                    .HasColumnName("name_cust");

                entity.Property(e => e.R001)
                    .HasMaxLength(10)
                    .HasColumnName("r001");

                entity.Property(e => e.R004)
                    .HasMaxLength(10)
                    .HasColumnName("r004");

                entity.Property(e => e.R007)
                    .HasMaxLength(10)
                    .HasColumnName("r007");

                entity.Property(e => e.R010)
                    .HasMaxLength(10)
                    .HasColumnName("r010");

                entity.Property(e => e.R011)
                    .HasMaxLength(10)
                    .HasColumnName("r011");

                entity.Property(e => e.R012)
                    .HasMaxLength(10)
                    .HasColumnName("r012");

                entity.Property(e => e.R013)
                    .HasMaxLength(10)
                    .HasColumnName("r013");

                entity.Property(e => e.R014)
                    .HasMaxLength(10)
                    .HasColumnName("r014");

                entity.Property(e => e.R015)
                    .HasMaxLength(10)
                    .HasColumnName("r015");

                entity.Property(e => e.R017)
                    .HasMaxLength(10)
                    .HasColumnName("r017");

                entity.Property(e => e.R048)
                    .HasMaxLength(10)
                    .HasColumnName("r048");

                entity.Property(e => e.R049)
                    .HasMaxLength(10)
                    .HasColumnName("r049");

                entity.Property(e => e.R072)
                    .HasMaxLength(10)
                    .HasColumnName("r072");

                entity.Property(e => e.R088)
                    .HasMaxLength(10)
                    .HasColumnName("r088");

                entity.Property(e => e.R770)
                    .HasMaxLength(20)
                    .HasColumnName("r770");

                entity.Property(e => e.ReScore).HasColumnName("re_score");

                entity.Property(e => e.RejectCode)
                    .HasMaxLength(20)
                    .HasColumnName("reject_code");

                entity.Property(e => e.RlosCodeSector)
                    .HasMaxLength(10)
                    .HasColumnName("rlos_code_sector");

                entity.Property(e => e.RshId).HasColumnName("rsh_id");

                entity.Property(e => e.StartDate).HasColumnName("start_date");

                entity.Property(e => e.TmpTable)
                    .HasColumnType("character varying")
                    .HasColumnName("tmp_table");

                entity.Property(e => e.UpdateDate).HasColumnName("update_date");

                entity.Property(e => e.ValBureau)
                    .HasMaxLength(10)
                    .HasColumnName("val_bureau");

                entity.Property(e => e.ValGrac)
                    .HasMaxLength(10)
                    .HasColumnName("val_grac");

                entity.Property(e => e.WorkAddressCity)
                    .HasMaxLength(20)
                    .HasColumnName("work_address_city");

                entity.Property(e => e.WorkCompanyType)
                    .HasMaxLength(20)
                    .HasColumnName("work_company_type");

                entity.Property(e => e.WorkEmploymentStatus)
                    .HasMaxLength(30)
                    .HasColumnName("work_employment_status");

                entity.Property(e => e.WorkGrossIncome)
                    .HasPrecision(19)
                    .HasColumnName("work_gross_income");

                entity.Property(e => e.WorkMonth).HasColumnName("work_month");

                entity.Property(e => e.WorkflowCode)
                    .HasColumnType("character varying")
                    .HasColumnName("workflow_code");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
