using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using sky.recovery.Entities;
using sky.recovery.Helper.Config;

namespace sky.recovery.Insfrastructures
{
    public class SkyCoreConfig:DbContext
    {
        public DbSet<master_loan> master_loan { get; set; }
        public DbSet<restructure_cashflow> restructure_cashflow { get; set; }

        public DbSet<branch> branch { get; set; }
        public DbSet<rfproduct_segment> rfproduct_segment { get; set; }

        public DbSet<master_customer> master_customer { get; set; }
        public DbSet<collection_call> collection_call { get; set; }
        public DbSet<rfproduct> rfproduct { get; set; }
        public DbSet<collection_add_contact> collection_add_contact { get; set; }
        public DbSet<restructure> restructure { get; set; }
        public DbSet<status> status { get; set; }

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
            modelBuilder.Entity<branch>()
.HasKey(e => e.lbrc_id);
            modelBuilder.Entity<restructure_cashflow>()
.HasKey(e => e.rsc_id);
            modelBuilder.Entity<rfproduct_segment>()
.HasKey(e => e.prd_sgm_id);
            modelBuilder.Entity<restructure>()
.HasKey(e => e.rst_id);
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
   
 
 
        }
    }
}
