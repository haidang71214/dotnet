using System.ComponentModel.DataAnnotations;

namespace UserManager.Models.Dto
{
    public class CreateUserDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ImagesUrl { get; set; }
    }
}
