using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sky.recovery.Model.Entity
{
    [Table("generic_param")]
    public abstract class GenericParameterEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int? Id { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("code")]
        public string? Code { get; set; }
    }

    public class DocumentRestruktur : GenericParameterEntity
    {

    }

    public class PolaRestruktur : GenericParameterEntity
    {

    }
    
    public class PembayaranGp : GenericParameterEntity
    {

    }
    
    public class JenisPengurangan : GenericParameterEntity
    {

    }
    
    public class RecoveryExecution : GenericParameterEntity
    {

    }
}