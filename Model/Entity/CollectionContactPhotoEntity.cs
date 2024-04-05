using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sky.recovery.Model.Entity
{
    [Table("collection_contact_photo")]
    public class CollectionContactPhotoEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int? Id { get; set; }

        [Column("coll_contact_id")]
        public int? CollectionContactId { get; set; }

        [ForeignKey("CollectionContactId")]
        public CollectionAddContactEntity? CollectionAddContact { get; set; }

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