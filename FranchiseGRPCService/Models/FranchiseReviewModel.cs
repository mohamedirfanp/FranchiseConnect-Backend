using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FranchiseGRPCService.Models
{
    [Table("FranchiseReview", Schema = "dbo")]
    public class FranchiseReviewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("franchise_review_id")]
        public int FranchiseReviewId { get; set; }

        [Required]
        [StringLength(250)]
        [Column("franchise_review")]
        public string FranchiseReview { get; set; }

        [Required]
        [StringLength(50)]
        [Column("franchise_rating")]
        public string FranchiseRating { get; set; }

        [Column("franchise_id")]
        [ForeignKey("franchiseId")]
        public int FranchiseId { get; set; }

        public virtual FranchiseModel franchiseId { get; set; }
    }
}
