using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sky.recovery.Model.Entity
{
    [Table("rfresult")]
    public class CallResultEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int? Id { get; set; }

        [Column("rl_code")]
        [StringLength(10)]
        public string? Code { get; set; }

        [Column("rl_desc")]
        [StringLength(20)]
        public string? Description { get; set; }

        [Column("create_date")]
        public DateTime? CreateDate { get; set; }

        [Column("update_date")]
        public DateTime? UpdateDate { get; set; }

        [Column("status_id")]
        public int? StatusId { get; set; }

        [ForeignKey(nameof(StatusId))]
        public StatusGeneral? Status { get; set; }

        [Column("is_dc")]
        public int? isDC { get; set; }

        [Column("is_fc")]
        public int? isFC { get; set; }
    }
}