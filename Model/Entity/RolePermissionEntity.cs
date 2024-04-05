using System.ComponentModel.DataAnnotations.Schema;

namespace sky.recovery.Model.Entity
{
    [Table("role_permission")]
    public class RolePermissionEntity
    {

        [Column("role_id")]
        public int? RoleId { get; set; }

        public Role? Role { get; set; }

        [Column("permission_id")]
        public int PermissionId { get; set; }

        public PermissionEntity? Permission { get; set ; }

        [Column("save")]
        public int? Create { get; set; }

        [Column("read")]
        public int? Read { get; set; }

        [Column("update")]
        public int? Update { get; set; }

        [Column("delete")]
        public int? Delete { get; set; }

        [Column("approve")]
        public int? Approve { get; set; }

        [Column("assign")]
        public int? Assign { get; set; }
    }
}