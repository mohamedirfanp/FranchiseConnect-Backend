using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FranchiseGRPCService.Models
{
    [Table("FranchiseGallery", Schema = "dbo")]
    public class FranchiseGalleryModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("franchise_gallery_id")]
        public int FranchiseGalleryId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("franchise_photo_url")]
        [StringLength(100)]
        public string FranchisePhotoUrl { get; set; }

        [Column("franchise_id")]
        [ForeignKey("franchiseId")]
        public int FranchiseId { get; set; }

        public virtual FranchiseModel franchiseId { get; set; }
    }
}
