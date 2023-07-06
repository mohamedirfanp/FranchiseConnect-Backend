
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatGRPCService.Models
{
    public class QueryModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("query_id")]
        public int QueryId { get; set; }

        [Required]
        [StringLength(50)]
        [Column("query_title")]
        public string QueryTitle { get; set; }

        [Required]
        [StringLength(30)]
        [Column("query_type")]
        public string QueryType { get; set; }

        [Required]
        [Column("query_description")]
        public string QueryDesciption { get; set; }

        public DateTime CreatedAt { get; set; }

        public int CreatedId { get; set; }

        public bool Status { get; set; }
    }
}
