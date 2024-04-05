using System;
using System.Reflection.Metadata;
using App.Metrics.Formatters.Prometheus;
using Microsoft.EntityFrameworkCore;
using sky.recovery.Model.Entity;

namespace sky.recovery.Model
{
    public class CollectiumDBContext : DbContext
    {

        public CollectiumDBContext(DbContextOptions<CollectiumDBContext> options) : base(options)
        {
        }
        
        // public virtual DbSet<RestructureE> Restructure { get; set; }
        //USER
        public virtual DbSet<Role> Role { get; set; }

        public virtual DbSet<StatusGeneral> StatusGeneral { get; set; }

        public virtual DbSet<UserEntity> User { get; set; }

        public virtual DbSet<UserBranchEntity> UserBranch { get; set; }

        public virtual DbSet<PermissionEntity> Permission { get; set; }

        public virtual DbSet<RolePermissionEntity> RolePermission { get; set; }

        //public virtual DbSet<RolePermissionRequest> RolePermissionRequest { get; set; }


        //MASTER
        public virtual DbSet<Action> Action { get; set; }

        public virtual DbSet<AreaEntity> Area { get; set; }

        public virtual DbSet<Branch> Branch { get; set; }

        public virtual DbSet<BranchTypeEntity> BranchType { get; set; }


        //MASTER TX
        public virtual DbSet<City> City { get; set; }

        public virtual DbSet<CustomerOccupationEntity> CustomerOccupation { get; set; }

        public virtual DbSet<CustomerTypeEntity> CustomerType { get; set; }

        public virtual DbSet<Gender> Gender { get; set; }

        public virtual DbSet<IdTypeEntity> IdType { get; set; }

        public virtual DbSet<IncomeTypeEntity> IncomeType { get; set; }

        public virtual DbSet<KecamatanEntity> Kecamatan { get; set; }

        public virtual DbSet<KelurahanEntity> Kelurahan { get; set; }

        public virtual DbSet<MaritalStatusEntity> MaritalStatus { get; set; }

        public virtual DbSet<NationalityEntity> Nationality { get; set; }

        public virtual DbSet<ProductEntity> Product { get; set; }

        public virtual DbSet<ProductSegmentEntity> ProductSegment { get; set; }

        public virtual DbSet<ProvinsiEntity> Provinsi { get; set; }

        public virtual DbSet<CustomerEntity> Customer { get; set; }

        public virtual DbSet<MasterLoanEntity> MasterLoan { get; set; }

        public virtual DbSet<CollectionAddContactEntity> CollectionAddContact { get; set; }

        public virtual DbSet<CollectionCallEntity> CollectionCall { get; set; }

        public virtual DbSet<CallResultEntity> CallResult { get; set; }

        public virtual DbSet<ReasonEntity> Reason { get; set; }

        public virtual DbSet<Counter> Counter { get; set; }

        public virtual DbSet<CollectionContactPhotoEntity> CollectionContactPhoto { get; set; }
        public virtual DbSet<PolaRestruktur> PolaRestruktur { get; set; }
        public virtual DbSet<PembayaranGp> PembayaranGp { get; set; }
        public virtual DbSet<StatusRestruktur> StatusRestruktur { get; set; }
        public virtual DbSet<RestructureEntity> Restructure { get; set; }
        public virtual DbSet<RestructureCashFlowEntity> RestructureCashFlow { get; set; }
        public virtual DbSet<RecoveryExecution> RecoveryExecution { get; set; }
        public virtual DbSet<RestructureApprovalEntity> RestructureApproval { get; set; }
        
        public virtual DbSet<RestructureDocumentEntity> RestructureDocument { get; set; }
    }
}