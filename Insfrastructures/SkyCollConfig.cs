using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using sky.recovery.Entities;
using sky.recovery.Helper.Config;

namespace sky.recovery.Insfrastructures
{
    public class SkyCollConfig:DbContext
    {
        public DbSet<users> users { get; set; }
        public DbSet<role> role { get; set; } 

        private DbContextSettings _appsetting { get; set; }

        public SkyCollConfig(IOptions<DbContextSettings> appsetting)
        {
            _appsetting = appsetting.Value;
        }
        // public DbSet<branch> branch { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var builder = new ConfigurationBuilder()
            //     .SetBasePath(Directory.GetCurrentDirectory())
            //     .AddJsonFile("appsettings.json");
            //var config = builder.Build();
            //var Connstring= config.GetSection("DbContextSettings:ConnectionString_coll").Value.ToString();
            var SQLCons = "Host=103.53.197.67;Database=sky.coll;Username=postgres;Password=User123!";

            optionsBuilder.UseNpgsql(_appsetting.postgresql.ConnectionString_core);

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<users>()
.HasKey(e => e.usr_id);
            modelBuilder.Entity<role>()
.HasKey(e => e.rl_id);

            //            modelBuilder.Entity<branch>()
            //.HasKey(e => e.lbrc_id);
        }
    }
}
