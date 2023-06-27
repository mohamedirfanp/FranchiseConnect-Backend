using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FranchiseGRPCService.Models
{
    [Table("Franchise", Schema = "dbo")]
        public class FranchiseModel
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            [Column("franchise_id")]
            public int FranchiseId { get; set; }

            [Required]
            [StringLength(50)]
            [Column("franchise_name")]
            public string FranchiseName { get; set; }

            [Required]
            [StringLength(50)]
            [Column("franchise_industry")]
            public string FranchiseIndustry { get; set; }

            [Required]
            [StringLength(50)]
            [Column("franchise_segment")]
            public string FranchiseSegment { get; set; }

            [Required]
            [StringLength(250)]
            [Column("franchise_about")]
            public string FranchiseAbout { get; set; }

            [Required]
            [StringLength(50)]
            [Column("franchise_investment")]
            public string FranchiseInvestment { get; set; }

            [Required]
            [StringLength(50)]
            [Column("franchise_fee")]
            public string FranchiseFee { get; set; }

            [Required]
            [StringLength(50)]
            [Column("franchise_space")]
            public string FranchiseSpace { get; set; }

            [Required]
            [StringLength(50)]
            [Column("franchise_current_count")]
            public string FranchiseCurrentCount { get; set; }

            [Required]
            [StringLength(50)]
            [Column("franchise_preferred_expansion_location")]
            public string FranchisePreferredExpansionLocation { get; set; }

            [Required]
            [Column("franchise_view_count")]
            public int FranchiseViewCount { get; set; }

            [Required]
            [Column("franchise_sample_box_option")]
            public bool FranchiseSampleBoxOption { get; set; }

            [Required]
            [Column("franchise_customized_option")]
            public bool FranchiseCustomizedOption { get; set; }

            [Column("franchise_customized_option_id")]
            public int FranchiseCustomizedOptionId { get; set; }

        
            [Column("franchise_social_id")]
            [ForeignKey("franchiseSocialId")]
            public int FranchiseSocialId { get; set; }

            [Required]
            [Column("franchise_owner_id")]
            public int FranchiseOwnerId { get; set; }

            public virtual FranchiseSocialModel franchiseSocialId { get; set; }

            public virtual FranchiseRequestModel franchiseRequestId { get; set; }

            public virtual ICollection<FranchiseServiceModel> franchiseServices { get; set; }

            public virtual ICollection<FranchiseReviewModel> franchiseReviews { get; set; }

            public virtual ICollection<FranchiseGalleryModel> franchiseGallery { get; set; }    


        }

}
