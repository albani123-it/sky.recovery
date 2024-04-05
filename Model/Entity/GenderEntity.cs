using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sky.recovery.Model.Entity
{
    [Table("rfgender")]
    public class Gender
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int? Id { get; set; }

        [Column("code")]
        [StringLength(10)]
        public string? Code { get; set; }

        [Column("desc")]
        [StringLength(20)]
        public string? Desc { get; set; }

        [Column("core_code")]
        [StringLength(10)]
        public string? CoreCode { get; set; }

        [Column("create_date")]
        public DateTime? CreateDate { get; set; }

        [Column("update_date")]
        public DateTime? UpdateDate { get; set; }

        [Column("status_id")]
        public int? StatusId { get; set; }

        [ForeignKey(nameof(StatusId))]
        public StatusGeneral? Status { get; set; }
    }
}