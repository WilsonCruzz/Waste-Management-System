using waste_management_system.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace waste_management_system.Models
{
    public class TypeOfWaste
    {
        //PK
        [Key]
        public int TypeOfWasteId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
        [Required]
        public float Value { get; set; }
        //FK
        [Required]
        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }
        // parent reference
        public Vehicle? Vehicles { get; set; } 
        // child reference
        public List<PickUpRequest>? PickUpRequests { get; set; }

    }
}
