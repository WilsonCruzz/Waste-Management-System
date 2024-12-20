using waste_management_system.Models;
using System.ComponentModel.DataAnnotations;

namespace waste_management_system.Models
{
    public class RequestStatus
    {
        //PK
        [Key]
        public int RequestStatusId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Status { get; set; }
        // child reference
        public List<PickUpRequest>? PickUpRequests { get; set; } 
    }
}
