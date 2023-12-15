using System.ComponentModel.DataAnnotations;
using EisntFlix.Root.Base;

namespace EisntFlix.Models
{
    public abstract class Person : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Profile Picture")]
        [Required(ErrorMessage = "Profile Picture is required")]
        public string ProfilePictureURL { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Full Name must be between 3 and 50 chars")]
        public string FullName { get; set; }

        [Display(Name = "Biography")]
        [StringLength(300)]
        [Required(ErrorMessage = "Biography is required")]
        public string? Bio { get; set; }
    }
}