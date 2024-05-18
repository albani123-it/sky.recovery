using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace sky.recovery.Entities
{
    [Table("masterflow", Schema = "RecoveryBusinessV2")]

    public class MasterFlow
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int id { get; set; }
        public int? orders { get; set; }
        public int? fiturid { get; set; }
        public string descriptions { get; set; }
        public int? roleid { get; set; }
        public string branchid { get; set; }
        public int? masterworkflowid { get; set; }
    }
}
