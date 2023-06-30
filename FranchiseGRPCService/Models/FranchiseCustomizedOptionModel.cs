

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FranchiseGRPCService.Models
{
    public class FranchiseCustomizedOptionModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("franchise_customized_option_id")]
        public int FranchiseCustomizedOptionId { get; set; }


        [Required]
        [StringLength(50)]
        [Column("franchise_customized_option_name")]
        public string FranchiseCustomizedOptionName { get; set; }



        public virtual ICollection<FranchiseSelectedService> FranchiseSelectedServices { get; set; }



    }
}
