using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sky.recovery.Model.Entity
{
    [Table("user_branch")]
    public class UserBranchEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int? Id { get; set; }

        [Column("user_id")]
        public int? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public UserEntity? User{ get; set; }

        [Column("branch_id")]
        public int? BranchId { get; set; }

        [ForeignKey(nameof(BranchId))]
        public Branch? Branch { get; set; }

        [Column("status_id")]
        public int? StatusId { get; set; }

        [ForeignKey(nameof(StatusId))]
        public StatusGeneral? Status { get; set; }
    }
}