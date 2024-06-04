using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace sky.recovery.Insfrastructures.Scafolding.SkyColl.Rule
{
    public partial class SkyCollRuleDBContext : DbContext
    {
        public SkyCollRuleDBContext()
        {
        }

        public SkyCollRuleDBContext(DbContextOptions<SkyCollRuleDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Mastercondition> Masterconditions { get; set; }
        public virtual DbSet<Masterdatasource> Masterdatasources { get; set; }
        public virtual DbSet<Masterfield> Masterfields { get; set; }
        public virtual DbSet<Masterheaderruleaction> Masterheaderruleactions { get; set; }
        public virtual DbSet<Masteroptionruleaction> Masteroptionruleactions { get; set; }
        public virtual DbSet<Repository> Repositories { get; set; }
        public virtual DbSet<Sourcedatum> Sourcedata { get; set; }
        public virtual DbSet<Transactionrule> Transactionrules { get; set; }
        public virtual DbSet<Transactionruleaction> Transactionruleactions { get; set; }
        public virtual DbSet<Transactionruleactionbucket> Transactionruleactionbuckets { get; set; }
        public virtual DbSet<Transactionrulecondition> Transactionruleconditions { get; set; }

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

            modelBuilder.Entity<Mastercondition>(entity =>
            {
                entity.ToTable("mastercondition", "RuleEngine");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Condition).HasColumnName("condition");
            });

            modelBuilder.Entity<Masterdatasource>(entity =>
            {
                entity.ToTable("masterdatasource", "RuleEngine");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Datasource).HasColumnName("datasource");
            });

            modelBuilder.Entity<Masterfield>(entity =>
            {
                entity.ToTable("masterfield", "RuleEngine");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Field).HasColumnName("field");
            });

            modelBuilder.Entity<Masterheaderruleaction>(entity =>
            {
                entity.ToTable("masterheaderruleaction", "RuleEngine");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Action).HasColumnName("action");
            });

            modelBuilder.Entity<Masteroptionruleaction>(entity =>
            {
                entity.ToTable("masteroptionruleaction", "RuleEngine");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Headerid).HasColumnName("headerid");

                entity.Property(e => e.Options).HasColumnName("options");
            });

            modelBuilder.Entity<Repository>(entity =>
            {
                entity.ToTable("repository", "RuleEngine");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Datasourceid).HasColumnName("datasourceid");

                entity.Property(e => e.Exceptions).HasColumnName("exceptions");

                entity.Property(e => e.Ruleid).HasColumnName("ruleid");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Uploadedby).HasColumnName("uploadedby");

                entity.Property(e => e.Uploadeddated).HasColumnName("uploadeddated");

                entity.Property(e => e.Url).HasColumnName("url");

                entity.Property(e => e.Urlname).HasColumnName("urlname");
            });

            modelBuilder.Entity<Sourcedatum>(entity =>
            {
                entity.ToTable("sourcedata", "RuleEngine");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.Datasourceid).HasColumnName("datasourceid");

                entity.Property(e => e.Endtime).HasColumnName("endtime");

                entity.Property(e => e.Repositoryid).HasColumnName("repositoryid");

                entity.Property(e => e.Rulename).HasColumnName("rulename");

                entity.Property(e => e.Starttime).HasColumnName("starttime");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Transactionrule>(entity =>
            {
                entity.ToTable("transactionrule", "RuleEngine");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Createdby).HasColumnName("createdby");

                entity.Property(e => e.Createddated).HasColumnName("createddated");

                entity.Property(e => e.Isactive).HasColumnName("isactive");

                entity.Property(e => e.Rulecode).HasColumnName("rulecode");

                entity.Property(e => e.Rulename).HasColumnName("rulename");
            });

            modelBuilder.Entity<Transactionruleaction>(entity =>
            {
                entity.ToTable("transactionruleaction", "RuleEngine");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Actionoptions).HasColumnName("actionoptions");

                entity.Property(e => e.Actiontype).HasColumnName("actiontype");

                entity.Property(e => e.Rulecode).HasColumnName("rulecode");
            });

            modelBuilder.Entity<Transactionruleactionbucket>(entity =>
            {
                entity.ToTable("transactionruleactionbucket", "RuleEngine");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Bucketcode).HasColumnName("bucketcode");

                entity.Property(e => e.Bucketid).HasColumnName("bucketid");

                entity.Property(e => e.Rulecode).HasColumnName("rulecode");
            });

            modelBuilder.Entity<Transactionrulecondition>(entity =>
            {
                entity.ToTable("transactionrulecondition", "RuleEngine");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Condition).HasColumnName("condition");

                entity.Property(e => e.Field).HasColumnName("field");

                entity.Property(e => e.Rulecode).HasColumnName("rulecode");

                entity.Property(e => e.Value).HasColumnName("value");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
