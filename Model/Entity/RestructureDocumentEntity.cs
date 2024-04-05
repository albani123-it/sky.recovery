using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sky.recovery.Model.Entity
{
    [Table("restructure_document")]
    public class RestructureDocumentEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int? Id { get; set; }

        [Column("restructure_id")]
        public int? RestructureId { get; set; }

        [ForeignKey("RestructureId")]
        public RestructureEntity? Restructure { get; set; }

        [Column("doc_type_id")]
        public int? DocumentResutrukturId { get; set; }

        [ForeignKey("DocumentResutrukturId")]
        public DocumentRestruktur? DocumentResutruktur { get; set; }

        [Column("title")]
        [StringLength(100)]
        public string? Title { get; set; }

        [Column("description")]
        [StringLength(255)]
        public string? Description { get; set; }

        [Column("url")]
        [StringLength(255)]
        public string? Url { get; set; }

        [Column("mime")]
        [StringLength(255)]
        public string? Mime { get; set; }

        [Column("lat")]
        public double? Lat { get; set; }

        [Column("lon")]
        public double? Lon { get; set; }

        [Column("user_id")]
        public int? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public UserEntity? User { get; set; }

        [Column("create_date")]
        public DateTime? CreateDate { get; set; }
    }
}