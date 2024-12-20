using waste_management_system;
using System.ComponentModel.DataAnnotations;

namespace waste_management_system.Models
{
    public class UserStatus
    {
        //PK
        [Key]
        public int UserStatuId { get; set; }
        [Required]
        [MaxLength(15)]
        public string TypeOfUser { get; set; }
        [Required]
        [MaxLength(15)]
        public string? Status { get; set; }
        //child reference
        public List<UserProfile>? UserProfiles { get; set; } 

    }
}
