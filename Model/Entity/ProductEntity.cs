using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sky.recovery.Model.Entity
{
    [Table("rfproduct")]
    public class ProductEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int? Id { get; set; }

        [Column("code")]
        [StringLength(20)]
        public string? Code { get; set; }

        [Column("desc")]
        [StringLength(50)]
        public string? Desc { get; set; }

        [Column("core_code")]
        [StringLength(20)]
        public string? CoreCode { get; set; }

        [Column("prd_segment_id")]
        public int? ProductSegmentId { get; set; }

        [ForeignKey(nameof(ProductSegmentId))]
        public ProductSegmentEntity? ProductSegment { get; set; }

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