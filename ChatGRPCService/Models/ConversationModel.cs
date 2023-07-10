using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatGRPCService.Models
{
    public class ConversationModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("conversation_id")]
        public int ConversationId { get; set; }

        [Required]
        [Column("franchisee_id")]
        public int FranchiseeId { get; set; }

        [Required]
        [Column("franchisor_id")]
        public int FranchisorId { get; set; }


        [Column("is_accepted")]
        public string IsAccepted { get; set; } = "Pending";

        [Required]
        [Column("franchisee_name")]
        public string FranchiseeName { get; set;}

        [Required]
        [Column("franchisor_franchise_name")]
        public string FranchisorFranchiseName { get; set; }

        [Required]
        [Column("franchisor_name")]
        public string FranchisorName { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set;}

    }
}
