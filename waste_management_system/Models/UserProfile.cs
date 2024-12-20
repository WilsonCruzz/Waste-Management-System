using waste_management_system.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace waste_management_system.Models
{
    public class UserProfile
    {
        //PK
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserProfileId { get; set; }
        [Required]
        [MaxLength(100)]
        public string CompanyName { get; set; }
        [Required]
        [MaxLength(100)]
        public string Responsible { get; set; }
        [Required]
        [MaxLength(150)]
        public string Address { get; set; }
        [Required]
        [MaxLength(150)]
        public string Email { get; set; }
        [Required]
        [MaxLength(15)]
        public string PhoneContact { get; set; }
        [Required]
        [MaxLength(50)]
        public string passwordUser { get; set; }

        //FK
        [ForeignKey("UserStatus")]
        public int UserStatusId { get; set; }
        // parent reference
        public UserStatus? UserStatuses { get; set; }
        // child reference
        public List<PickUpRequest>? PickUpRequests { get; set; }

    }
}
