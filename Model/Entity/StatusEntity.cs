using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sky.recovery.Model.Entity
{
    [Table("status")]
    public abstract class StatusEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int? Id { get; set; }

        [Column("name")]
        public string? Name { get; set; }
    }

    public class StatusGeneral : StatusEntity
    {

    }
    
    public class StatusRestruktur : StatusEntity
    {

    }
}