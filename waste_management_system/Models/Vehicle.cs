using waste_management_system.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace waste_management_system.Models
{
    public class Vehicle
    {
        //PK
        [Key]
        public int VehicleId { get; set; }
        [Required]
        [MaxLength(30)]
        public string VehicleClass { get; set; }
        // FK
        [ForeignKey("Capacity")]
        public int CapacityId { get; set; }
        // parent reference
        public Capacity? Capacity { get; set; }
        // child reference
        public List<TypeOfWaste>? TypeOfWastes { get; set; } 
    }
}
