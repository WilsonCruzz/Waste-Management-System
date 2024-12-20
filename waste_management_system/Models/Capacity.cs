using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace waste_management_system.Models
{
    public class Capacity
    {
        // FK
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CapacityId { get; set; }
        [Required]
        [MaxLength(10)]
        public string TipeOfWasteId { get; set; }
        [Required]
        public float MaxKg { get; set; }

        // child reference
        public List<Vehicle>? Vehicles { get; set; }
    }
}
