using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using sky.recovery.Entities;
using sky.recovery.Entities.RecoveryConfig;
using sky.recovery.Helper.Config;

namespace sky.recovery.Insfrastructures
{
    public class SkyCoreConfig:DbContext
    {
        public DbSet<master_loan> master_loan { get; set; }
        public DbSet<restructure_cashflow> restructure_cashflow { get; set; }
        public DbSet<generic_param> generic_param { get; set; }
        public DbSet<ayda> ayda { get; set; }
        public DbSet<auction> auction { get; set; }

        public DbSet<branch> branch { get; set; }

        public DbSet<templatedetail> templatedetail { get; set; }
        public DbSet<masterdocumenttype> masterdocumenttype { get; set; }

        public DbSet<masterlayoutposition> masterlayoutposition { get; set; }
        public DbSet<mastertemplate> mastertemplate { get; set; }

        public DbSet<rfproduct_segment> rfproduct_segment { get; set; }

        public DbSet<workflowhistory> workflowhistory { get; set; }
        public DbSet<workflow> workflow { get; set; }
        public DbSet<MasterFlow> masterflow { get; set; }
        public DbSet<masterrepository> masterrepository { get; set; }

        public DbSet<GeneralParamDetail> generalparamdetail { get; set; }
        public DbSet<GeneralParamHeader> generalparamheader { get; set; }
        public DbSet<master_customer> master_customer { get; set; }
        public DbSet<collection_call> collection_call { get; set; }
        public DbSet<rfproduct> rfproduct { get; set; }
        public DbSet<collection_add_contact> collection_add_contact { get; set; }
        public DbSet<master_collateral> master_collateral { get; set; }

        public DbSet<restructure> restructure { get; set; }
        public DbSet<insurance> insurance { get; set; }

        public DbSet<status> status { get; set; }
        public DbSet<masterworkflowrule> masterworkflowrule { get; set; }
        public DbSet<masterworkflow> masterworkflow { get; set; }
        private DbContextSettings _appsetting { get; set; }
        public SkyCoreConfig(IOptions<DbContextSettings> appsetting)
        {
            _appsetting = appsetting.Value;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var builder = new ConfigurationBuilder()
            //     .SetBasePath(Directory.GetCurrentDirectory())
            //     .AddJsonFile("appsettings.json");
            //var config = builder.Build();
            //var Connstring= config.GetSection("DbContextSettings:ConnectionString_coll").Value.ToString();
            var SQLCons = "Host=103.53.197.67;Database=sky.coll;Username=postgres;Password=User123!";

            optionsBuilder.UseNpgsql(_appsetting.postgresql.ConnectionString_coll);

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<workflow>()
.HasKey(e => e.Id);
            modelBuilder.Entity<insurance>()
.HasKey(e => e.Id);
            modelBuilder.Entity<masterrepository>()
.HasKey(e => e.id);
            modelBuilder.Entity<masterworkflow>()
.HasKey(e => e.id);
            modelBuilder.Entity<MasterFlow>()
.HasKey(e => e.id);

            modelBuilder.Entity<branch>()
.HasKey(e => e.lbrc_id);
            modelBuilder.Entity<GeneralParamHeader>()
.HasKey(e => e.id);
            modelBuilder.Entity<GeneralParamDetail>()
.HasKey(e => e.Id);
            modelBuilder.Entity<generic_param>()
.HasKey(e => e.glp_id);
            modelBuilder.Entity<restructure_cashflow>()
.HasKey(e => e.rsc_id);
            modelBuilder.Entity<rfproduct_segment>()
.HasKey(e => e.prd_sgm_id);
            modelBuilder.Entity<restructure>()
.HasKey(e => e.id);
            modelBuilder.Entity<master_loan>()
              .HasKey(e => e.id);
            modelBuilder.Entity<master_customer>()
            .HasKey(e => e.Id);
            modelBuilder.Entity<status>()
            .HasKey(e => e.sts_id);
            modelBuilder.Entity<collection_call>()
        .HasKey(e => e.Id);
            modelBuilder.Entity<rfproduct>()
     .HasKey(e => e.prd_id);
            modelBuilder.Entity<collection_add_contact>()
    .HasKey(e => e.id);

            modelBuilder.Entity<masterworkflow>()
               .HasKey(e => e.id);
            modelBuilder.Entity<masterworkflowrule>()
    .HasKey(e => e.id);

            modelBuilder.Entity<mastertemplate>()
   .HasKey(e => e.id);
            modelBuilder.Entity<masterlayoutposition>()
   .HasKey(e => e.id);
            modelBuilder.Entity<masterdocumenttype>()
   .HasKey(e => e.id);
            modelBuilder.Entity<templatedetail>()
   .HasKey(e => e.id);
            modelBuilder.Entity<master_collateral>()
.HasKey(e => e.id);

        }
    }
}
