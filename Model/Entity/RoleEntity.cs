using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sky.recovery.Model.Entity
{
    [Table("role")]
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int? Id { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("status_id")]
        public int? StatusId { get; set; }

        public virtual ICollection<RolePermissionEntity>? RolePermission { get; set; }

        public StatusGeneral? Status { get; set; }
    }
}