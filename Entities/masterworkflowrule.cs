using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sky.recovery.Entities
{
    [Table("masterworkflowrule", Schema = "RecoveryBusinessV2")]
    public class masterworkflowrule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int id { get; set; }
       
       public int? masterworkflowid { get; set; }
        public string variabel { get; set; }
        public string operators { get; set; }
        public int? value { get; set; }
  
    }
}
