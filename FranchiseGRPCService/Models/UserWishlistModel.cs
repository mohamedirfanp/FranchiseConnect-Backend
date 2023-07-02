using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FranchiseGRPCService.Models
{
    public class UserWishlistModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserWishlistId { get; set; }

        [Required]
        [ForeignKey("franchiseId")]
        public int FranchiseId { get; set; }

        [Required]
        public int UserId { get; set; }


        public virtual FranchiseModel franchiseId { get; set; }
    }
}
