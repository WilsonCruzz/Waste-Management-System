using waste_management_system.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace waste_management_system.Models
{
    public class PickUpRequest
    {
        //PK
        [Key]
        public int PickUpRequestId { get; set; }
        [Required]
        [Display(Name = "Request Date")]
        public DateTime RequestDate { get; set; }
        [Required]
        [Display(Name = "Pickup Date")]
        public DateTime PickupDate { get; set; }


        //FK
        [ForeignKey("TypeOfWaste")]
        public int TypeOfWasteId { get; set; }
        [ForeignKey("RequestStatus")]
        public int RequestStatusId { get; set; }
        [ForeignKey("UserProfile")]
        public int UserProfileId { get; set; }

        // parent reference
        public TypeOfWaste? TypeOfWastes { get; set; }
        public RequestStatus? RequestStatuses { get; set; }
        public UserProfile? UserProfiles { get; set; }

        // child reference
        public List<Observation>? Observations { get; set; }
    }
}
