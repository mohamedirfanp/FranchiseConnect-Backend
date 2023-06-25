using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FranchiseGRPCService.Models
{
    [Table("FranchiseSocial", Schema = "dbo")]
    public class FranchiseSocialModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("franchise_social_id")]
        public int FranchiseSocialId { get; set; }

        [Required]
        [StringLength(50)]
        [Column("franchise_email")]
        public string FranchiseEmail { get; set; }

        [Column("franchise_website")]
        public string FranchiseWebsite { get; set; }

        [Column("franchise_facebook")]
        public string FranchiseFacebook { get; set; }

        [Column("franchise_twitter")]
        public string FranchiseTwitter { get; set; }

        [Column("franchise_instagram")]
        public string FranchiseInstagram { get; set; }


    }

}
