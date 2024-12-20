using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace waste_management_system.Models
{
    public class Observation
    {
        //PK
        [Key]
        public int ObservationId { get; set; }
        [Required]
        [MaxLength(50)]
        public string AuthorName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime EntryDate { get; set; }
        // FK
        [ForeignKey("PickUpRequest")]
        public int PickUpRequestId { get; set; }
        // parent reference
        public PickUpRequest? PickUpRequests { get; set; } 

    }
}
